using System.Numerics;
using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct SeekBehaviorData : IComponentData
{
    public float mass;
    public float maxVelocity;
    public float maxForce;

    public float3 velocity;
    public float3 targetPos;
    public float targetMaxRange;
}
