using System.Threading;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Extensions;
using Unity.Physics.Systems;
using Unity.Transforms;
using UnityEngine;
using static Unity.Mathematics.math;

public class TurretSystem : SystemBase
{
    private BuildPhysicsWorld m_BuildPhysicsWorld;
    private EndSimulationEntityCommandBufferSystem commandBufferSystem;

    protected override void OnCreate()
    {
        m_BuildPhysicsWorld = World.GetOrCreateSystem<BuildPhysicsWorld>();
        commandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        CollisionWorld collisionWorld = m_BuildPhysicsWorld.PhysicsWorld.CollisionWorld;
        EntityCommandBuffer entityCommandBuffer = commandBufferSystem.CreateCommandBuffer();
        float deltaTime = Time.DeltaTime;

        Entities.ForEach((ref Translation a_trans, ref Rotation a_rot, ref TurretData a_turretData) =>
        {
            if (a_turretData.timer > a_turretData.fireRate)
            {
                uint layer = 1 << 1;

                var input = new RaycastInput
                {
                    // Create ray in front of the ship.
                    Start = a_trans.Value + float3(20, 0, 0),
                    End = math.forward(a_rot.Value) * 200 + a_trans.Value,
                    Filter = new CollisionFilter
                    {
                        BelongsTo = ~0u,
                        CollidesWith = layer,
                    }
                };

                Unity.Physics.RaycastHit raycastHit = new Unity.Physics.RaycastHit();

                if (collisionWorld.CastRay(input, out raycastHit))
                {
                    Entity entity = entityCommandBuffer.Instantiate(a_turretData.entityPrefab);
                    Translation newTranslation = a_trans;
                    newTranslation.Value = a_trans.Value + (math.forward(a_rot.Value) * 20);

                    entityCommandBuffer.SetComponent<Translation>(entity, newTranslation);
                    entityCommandBuffer.SetComponent<Rotation>(entity, a_rot);
                    
                    // Set turret on cooldown.
                    a_turretData.timer = 0;
                }
            }
            else
            {
                a_turretData.timer += deltaTime;
            }
            
        }).Run();

        commandBufferSystem.AddJobHandleForProducer(this.Dependency);
    }
}
