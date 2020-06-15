using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Entities.UniversalDelegates;

// Derives from JobComponent system, this will divide the job over several threads.
public class SinWaveSystem_2 : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float elapsedTime = (float)Time.ElapsedTime;

        JobHandle jobHandle = Entities.ForEach((ref Translation a_trans, in MoveSpeedData a_moveSpeedData, in SinWaveData a_waveData) =>
        {
            float zPos = a_waveData.amplitude * math.sin(elapsedTime * a_moveSpeedData.Value
                + a_trans.Value.x * a_waveData.xOffset + a_trans.Value.y * a_waveData.yOffset);

            a_trans.Value = new float3(a_trans.Value.x, a_trans.Value.y, zPos);
        }).Schedule(inputDeps);

        return jobHandle;
    }
}

