using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using static Unity.Mathematics.math;
using Unity.Physics;
using Unity.Physics.Systems;

public class ProjectileCollisionSystem : JobComponentSystem
{
    private BuildPhysicsWorld buildPhysicsWorld;
    private StepPhysicsWorld stepPhysicsWorld;

    protected override void OnCreate()
    {
        base.OnCreate();
        buildPhysicsWorld = World.GetOrCreateSystem<BuildPhysicsWorld>();
        stepPhysicsWorld = World.GetOrCreateSystem<StepPhysicsWorld>();
    }

    [BurstCompile]
    struct ProjectileCollisionSystemJob : ICollisionEventsJob
    {
        [ReadOnly] public ComponentDataFromEntity<SpaceshipTag> spaceshipGroup;
        [ReadOnly] public ComponentDataFromEntity<ProjectileTag> projectileGroup;

        public ComponentDataFromEntity<HealthData> healthGroup;
        public void Execute(CollisionEvent collisionEvent)
        {
            Entity entityA = collisionEvent.Entities.EntityA;
            Entity entityB = collisionEvent.Entities.EntityB;

            if (spaceshipGroup.Exists(entityA) && projectileGroup.Exists(entityB))
            {
                HealthData modifiedHealth = healthGroup[entityA];
                modifiedHealth.isDead = true;
                healthGroup[entityA] = modifiedHealth;
            }
            
            if (projectileGroup.Exists(entityA) && spaceshipGroup.Exists(entityB))
            {
                HealthData modifiedHealth = healthGroup[entityB];
                modifiedHealth.isDead = true;
                healthGroup[entityB] = modifiedHealth;
            }    
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDependencies)
    {
        var job = new ProjectileCollisionSystemJob();
        job.spaceshipGroup = GetComponentDataFromEntity<SpaceshipTag>(true);
        job.projectileGroup = GetComponentDataFromEntity<ProjectileTag>(true);
        job.healthGroup = GetComponentDataFromEntity<HealthData>(false);

        JobHandle jobHandle = job.Schedule(stepPhysicsWorld.Simulation, ref buildPhysicsWorld.PhysicsWorld, inputDependencies);
        jobHandle.Complete();
        
        return jobHandle;
    }
}