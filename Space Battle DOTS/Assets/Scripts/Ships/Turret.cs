using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField, Header("Amount of guns and the offset of its position.")]
    private Transform[] gunsNozzle = null;

    [SerializeField]
    private GameObject laserPrefab = null;

    [SerializeField, Range(0, 10)]
    private float fireRate = 1;

    // Start the timer at the max firerate so we can fire immediately.
    private float timer = 10;

    void Update()
    {
        CheckEnemyInSight();
    }

    private void CheckEnemyInSight()
    {
        RaycastHit hit;

        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            timer += Time.deltaTime;
            if (timer > fireRate)
            {
                CreateBullets();
                timer = 0;
            }

            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000000000, Color.white);
            Debug.Log("Did not Hit");
        }
    }

    private void CreateBullets()
    {
        for (int i = 0; i < gunsNozzle.Length; i++)
        {
            GameObject tempObj;
            //Instantiate/Create Bullet

            tempObj = Instantiate(laserPrefab) as GameObject;

            //Set position  of the bullet in front of the player
            tempObj.transform.position = gunsNozzle[i].position;
            tempObj.transform.rotation = transform.rotation;
        }
    }
}
