using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingBallCode : MonoBehaviour
{
    [SerializeField] Transform spawnLocation;
    [SerializeField] BowlingManager bowlingManager;

    void Update()
    {
        if (transform.localPosition.y < -7.4)
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

        //Update everything
        bowlingManager.BallBowled();
    }
}
