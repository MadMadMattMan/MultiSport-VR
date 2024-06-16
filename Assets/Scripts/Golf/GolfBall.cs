using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GolfBall : MonoBehaviour
{
    [SerializeField] ClubPhysics currentClub;
    Rigidbody rb;
    public float speed;

    public bool detectingMovement;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Detect_Speed();
        speed = rb.velocity.magnitude;
        //timeAfterHit += Time.deltaTime;
    }

    void Detect_Speed()
    {
        //If set to detect movement and ball is not moving, disable detect movement and enable club physics
        if (detectingMovement && rb.velocity.magnitude <= 0.001f)
        {
            rb.velocity = Vector3.zero;
            detectingMovement = false;
            currentClub.Enable_Physics();
        }
        //else, if set to detect movement (and ball is moving), run friction calculations
        else if (detectingMovement)
        {
            Friction_Force();
        }
    }

    public float frictionBaseMultiplier = 100f;
    public float kineticFrictionConstant = 0.2f; //Default = Rubber on nylon
    public float gravityAcceleration = 9.81f; //Default = Earth gravity

    // Friction Equations:
    // Friction Force = u * Nomal force
    // Normal Force = mass * gravity
    // Acceleration = Force / mass

    void Friction_Force()
    {
        float stoppingForce = kineticFrictionConstant * (rb.mass * gravityAcceleration); //(0.5*1*9.81)
        Vector3 ballBase = transform.position - new Vector3(0, -0.02f, 0);

        float frictionMultiplier = Mathf.Pow(-frictionBaseMultiplier, -10 * rb.velocity.magnitude) + 1; //if speed is more than 1, no effect but as speed approches 0, friction multiplier approches 0

        rb.AddForceAtPosition(-rb.velocity / (stoppingForce * frictionMultiplier) , ballBase);
    }
}