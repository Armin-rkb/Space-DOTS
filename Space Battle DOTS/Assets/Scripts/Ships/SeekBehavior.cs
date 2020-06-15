using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekBehavior : MonoBehaviour
{
    [SerializeField] 
    private float Mass = 15;
    [SerializeField] 
    private float MaxVelocity = 3;
    [SerializeField] 
    private float MaxForce = 15;

    public Vector3 velocity;

    public Vector3 targetPos;
    private const float targetMaxRange = 500;

    private void Start()
    {
        velocity = Vector3.zero;
    }

    private void Update()
    {
        Vector3 desiredVelocity = targetPos - transform.position;
        desiredVelocity = desiredVelocity.normalized * MaxVelocity; //"(70.1, 70.1, 335.6)"

        Vector3 steering = desiredVelocity - velocity;  //"(70.1, 70.1, 335.6)"
        steering = Vector3.ClampMagnitude(steering, MaxForce); //"(40.1, 40.1, 191.8)"
        steering /= Mass; //"(2.7, 2.7, 12.8)"

        velocity = Vector3.ClampMagnitude(velocity + steering, MaxVelocity); //"(2.7, 2.7, 12.8)"
        transform.position += velocity * Time.deltaTime; //"(0.1, 0.1, -378.3)"
        transform.forward = velocity.normalized; //"(0.2, 0.2, 1.0)"

        if (TargetReached())
        {
            GetNewTarget();
        }
    }

    // Sets a new random target position.
    private void GetNewTarget()
    {
        targetPos = Random.insideUnitSphere * targetMaxRange;
    }

    // Returns true when within close distance of target.
    private bool TargetReached()
    {
        if (Vector3.Distance(transform.position, targetPos) < 50f)
        {
            return true;
        }
        return false;
    }
}
