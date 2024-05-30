using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfBall : MonoBehaviour
{
    [SerializeField] ClubPhysics clubs;
    Rigidbody rb;
    [SerializeField] float speed, timeAfterHit;

    public bool detectingMovement;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Detect_Speed();
        speed = rb.velocity.magnitude;
        timeAfterHit += Time.deltaTime;
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
    public float frictionConstant, delay;

    void Friction_Force()
    {
        if (timeAfterHit > delay)
        {
            if (speed < stopSpeedRange)
            {
                rb.velocity = Vector3.zero;
            }
            else if (speed < medianSpeedRange)
            {
                rb.velocity *= 0.5f;
            }
            else
            {
                //F = uN
                rb.AddRelativeForce(-rb.velocity.normalized * (frictionConstant * (9.81f * rb.mass)));
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        timeAfterHit = 0;
    }
}
