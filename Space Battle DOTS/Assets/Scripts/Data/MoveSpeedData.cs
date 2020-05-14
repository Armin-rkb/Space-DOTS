using Unity.Entities;

/// <summary>
/// Data always needs to be a struct and derive from IComponentData.
/// Because we don't use MonoBehavior we need to add "GenerateAuthoringComponent" to- 
/// this script for Unity to attach it do a game object.
/// </summary>

[GenerateAuthoringComponent]
public struct MoveSpeedData : IComponentData
{
    // No functions, Components hold data only so there is no logic involved.
    public float Value;
}
