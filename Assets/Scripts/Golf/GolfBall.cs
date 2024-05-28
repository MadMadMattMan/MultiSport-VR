using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfBall : MonoBehaviour
{
    [SerializeField] ClubPhysics clubs;
    Rigidbody rb;
    [SerializeField] float speed;

    public bool detectingMovement;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Detect_Speed();
        speed = rb.velocity.magnitude;
    }

    void Detect_Speed()
    {
        if (detectingMovement && GetComponent<Rigidbody>().velocity.normalized == Vector3.zero)
        {
            detectingMovement = false;
            clubs.Enable_Physics();
        }
        else if (detectingMovement)
        {
            Friction_Force();
        }
    }

    [SerializeField] float medianSpeedRange, stopSpeedRange;
    [SerializeField] float fastSpeedFrictionMultiplier, slowSpeedFrictionMultiplier;

    void Friction_Force()
    {
        if (rb.velocity.magnitude < stopSpeedRange)
        {
            rb.velocity = Vector3.zero;
        }        
    }
}
