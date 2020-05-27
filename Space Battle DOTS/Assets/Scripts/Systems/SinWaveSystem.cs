using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

// Derives from Component system, this will only use the main thread.
    /*
public class SinWaveSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref Translation a_trans, ref MoveSpeedData a_moveSpeedData, ref SinWaveData a_waveData) =>
        {
            float zPos = a_waveData.amplitude * math.sin((float)Time.ElapsedTime * a_moveSpeedData.Value
                + a_trans.Value.x * a_waveData.xOffset + a_trans.Value.y * a_waveData.yOffset);
            a_trans.Value = new float3(a_trans.Value.x, a_trans.Value.y, zPos);
        });
    }
}
    */
