using System.Numerics;
using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct TurretData : IComponentData
{
    public Entity entityPrefab;
    public float fireRate;
    public float fireDistance;
    public float timer;
}