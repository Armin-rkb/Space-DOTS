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
        desiredVelocity = desiredVelocity.normalized * MaxVelocity;

        Vector3 steering = desiredVelocity - velocity;
        steering = Vector3.ClampMagnitude(steering, MaxForce);
        steering /= Mass;

        velocity = Vector3.ClampMagnitude(velocity + steering, MaxVelocity);
        transform.position += velocity * Time.deltaTime;
        transform.forward = velocity.normalized;

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
