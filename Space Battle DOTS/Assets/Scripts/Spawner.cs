using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Mathematics;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Mesh unitMesh = null;
    [SerializeField] private Material unitMaterial = null;

    // Start is called before the first frame update
    void Start()
    {
        MakeEntity();
    }

    private void MakeEntity()
    {
        // Get the Entity Manager from our current world.
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        EntityArchetype archetype = entityManager.CreateArchetype(
            typeof(Translation),
            typeof(Rotation),
            typeof(RenderMesh),
            typeof(RenderBounds),
            typeof(LocalToWorld)
            );

        Entity myEntity = entityManager.CreateEntity(archetype);

        entityManager.AddComponentData(myEntity, new Translation
        {
            Value = new float3(1f, -2f, -3f)
        });

        entityManager.AddSharedComponentData(myEntity, new RenderMesh
        {
            mesh = unitMesh,
            material = unitMaterial
        });
    }
}
