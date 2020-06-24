using System.Numerics;
using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct LifeTimeData : IComponentData
{
    public float secondsToDestroy;
    public float timeAlive;
}