using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class JobExample : MonoBehaviour
{
    void Start()
    {
        DoExample();
    }

    void DoExample()
    {
        // To perform the Job system, it needs to be handeled in these steps.
        // Instantiate the job.
        FirstJob firstJob = new FirstJob();
        SecondJob secondJob = new SecondJob();

        // Initialize the job.
        // When initializing the values of the native array we need to also set our method of allocation.
        // Allocator.Temp = for 1 frame, Allocator.TempJob up to 4 frames, Allocator.Persistent = as long as needed.
        firstJob.a = 5;
        firstJob.arrayA = new NativeArray<float>(1, Allocator.TempJob);

        secondJob.arrayB = firstJob.arrayA;
        // Schedule the job.
        // Now here you would schedule other task you want to run parallel.
        JobHandle firstHandle = firstJob.Schedule();
        JobHandle secondHandle = secondJob.Schedule(firstHandle);

        // We pass in the first handle to the second job making it an depencency
        // for it to complete first making the line below here obsolete.
        //firstHandle.Complete();
        secondHandle.Complete();
        Debug.Log(firstJob.arrayA[0]);

        // Lastly remove all native data which has been allocated.
        firstJob.arrayA.Dispose();
    }

    private struct FirstJob : IJob
    {
        public float a;
        public NativeArray<float> arrayA;

        public void Execute()
        {
            arrayA[0] = a;
        }
    }
    
    private struct SecondJob : IJob
    {
        public NativeArray<float> arrayB;

        public void Execute()
        {
            arrayB[0] = arrayB[0] + 1;
        }
    }
}
