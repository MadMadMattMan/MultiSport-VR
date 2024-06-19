using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingBallCode : MonoBehaviour
{
    [SerializeField] Transform spawnLocation;
    [SerializeField] GameObject bowlingManager;

    void Update()
    {
        if (transform.position.y < -1)
        {
            ReturnBall();
        }
    }

    void ReturnBall()
    {
        //Stop all movement
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        //Move Ball
        GetComponent<Transform>().position = spawnLocation.position;
    }
}
