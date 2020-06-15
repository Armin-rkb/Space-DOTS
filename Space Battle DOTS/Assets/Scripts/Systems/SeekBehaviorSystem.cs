
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Entities.UniversalDelegates;
using System.Numerics;
using UnityEngine;
using System;

// Derives from Component system, this will only use the main thread.
public class SeekBehaviorSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        float deltaTime = Time.DeltaTime;

        Entities.ForEach((ref Translation a_trans, ref Rotation a_rot, ref SeekBehaviorData a_seekData) =>
        {
            float3 desiredVelocity = a_seekData.targetPos - a_trans.Value;
            desiredVelocity = math.normalize(desiredVelocity) * a_seekData.maxVelocity;

            float3 steering = desiredVelocity - a_seekData.velocity;
            if (math.lengthsq(steering) > a_seekData.maxForce * a_seekData.maxForce)
            {
                steering = math.normalize(steering) * a_seekData.maxForce;
            }
            steering /= a_seekData.mass;

            if (math.lengthsq(a_seekData.velocity + steering) > a_seekData.maxForce * a_seekData.maxForce)
            {
                a_seekData.velocity = math.normalize(a_seekData.velocity) * a_seekData.maxForce;
            }
            else
            {
                a_seekData.velocity = a_seekData.velocity + steering;
            }
            a_trans.Value += a_seekData.velocity * deltaTime;

            a_rot.Value = quaternion.LookRotationSafe(math.normalize(a_seekData.velocity), math.up());

            if (TargetReached(in a_trans, in a_seekData))
            {
                GetNewTarget(ref a_seekData);
            }
        });
    }

    // Sets a new random target position.
    private static void GetNewTarget(ref SeekBehaviorData a_seekData)
    {
        Unity.Mathematics.Random rand = new Unity.Mathematics.Random((uint)UnityEngine.Random.Range(1, 1000000));

        a_seekData.targetPos = rand.NextFloat3(-a_seekData.targetMaxRange, a_seekData.targetMaxRange);
    }

    // Returns true when within close distance of target.
    private static bool TargetReached(in Translation a_trans, in SeekBehaviorData a_seekData)
    {
        if (math.distance(a_trans.Value, a_seekData.targetPos) < 50f)
        {
            return true;
        }
        return false;
    }
}

