using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics.Authoring;
using Unity.Transforms;

public class ProjectileSystem : SystemBase
{
    protected override void OnCreate()
    {
    }

    protected override void OnUpdate()
    {
        float deltaTime = Time.DeltaTime;

        Entities.ForEach((ref Translation a_trans, in Rotation a_rot, in ProjectileData a_projectileData) =>
        {
            //a_projectileData.direction = math.forward(a_rot.Value);
            a_trans.Value += (math.forward(a_rot.Value) * a_projectileData.projectileSpeed) * deltaTime;
        }).Schedule();
    }
}