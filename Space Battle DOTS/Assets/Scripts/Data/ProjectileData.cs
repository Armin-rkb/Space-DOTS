using System.Numerics;
using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct ProjectileData : IComponentData
{
    public float projectileSpeed;
    public float3 direction;
}