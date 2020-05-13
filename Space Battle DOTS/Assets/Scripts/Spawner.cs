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

    // Grid Spawn settings
    [SerializeField] private int rows = 10;
    [SerializeField] private int collums = 10;
    [Range(0.1f, 2.5f),SerializeField] private float offset = 1f;

    // Converted Object
    private Entity entityPrefab;

    // Manager and world
    private EntityManager entityManager;
    private World defaultWorld;

    // Start is called before the first frame update
    void Start()
    {
        defaultWorld = World.DefaultGameObjectInjectionWorld;
        entityManager = defaultWorld.EntityManager;

        GameObjectConversionSettings settings = GameObjectConversionSettings.FromWorld(defaultWorld, null);
        entityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(gameObjectPrefab, settings);

        InstantiateEntityGrid(rows, collums, offset);
    }

    private void InstantiateEntityGrid(int a_rows, int a_collums, float a_offset)
    {
        for (int x = 0; x < a_rows; x++)
        {
            for (int y = 0; y < a_collums; y++)
            {
                InstantiateEntity(new float3(x * a_offset, y * a_offset, 6f));
            }
        }
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
