using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float laserSpeed = 10000;

    [SerializeField]
    private Rigidbody laserRigidBody = null;
  
    // Move the laser forward with its given speed.
    void Start()
    {
        laserRigidBody.velocity = gameObject.transform.forward * laserSpeed;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Ship")
        {
            Destroy(col.gameObject);
            /// TODO:
            // Add an explosion or something
            // Subtract HP from hit ship.
            Destroy(gameObject);
        }
    }

}
