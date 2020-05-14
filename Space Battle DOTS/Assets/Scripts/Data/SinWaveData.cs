using Unity.Entities;

[GenerateAuthoringComponent]
public struct SinWaveData : IComponentData
{
    public float amplitude;
    public float xOffset;
    public float yOffset;
}
