using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Mathematics;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Mesh unitMesh = null;
    [SerializeField] private Material unitMaterial = null;

    // Normal GameObject
    [SerializeField] private GameObject gameObjectPrefab = null;
    
    // Converted Object
    private Entity entityPrefab;

    private EntityManager entityManager;
    private World defaultWorld;

    // Start is called before the first frame update
    void Start()
    {
        defaultWorld = World.DefaultGameObjectInjectionWorld;
        entityManager = defaultWorld.EntityManager;

        GameObjectConversionSettings settings = GameObjectConversionSettings.FromWorld(defaultWorld, null);
        entityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(gameObjectPrefab, settings);

        InstantiateEntity(new float3(2f, -5f, -6f));
    }


    private void InstantiateEntity(float3 position)
    {
        Entity myEntity = entityManager.Instantiate(entityPrefab);
        entityManager.SetComponentData(myEntity, new Translation
        {
            Value = position
        });
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
