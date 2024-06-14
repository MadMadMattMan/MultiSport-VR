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
        if (detectingMovement && rb.velocity.normalized == Vector3.zero)
        {
            detectingMovement = false;
            currentClub.Enable_Physics();
        }
        //else, if set to detect movement (and ball is moving), run friction calculations
        else if (detectingMovement)
        {
            Friction_Force();
        }
    }

    [SerializeField] float frictionChangeSpeed = 1f;
    public float staticFrictionConstant = 0.6f; //Default = Rubber on nylon
    public float kineticFrictionConstant = 0.5f; //Default = Rubber on nylon
    public float gravityAcceleration = 9.81f; //Default = Earth gravity

    // Friction Math:
    // Friction Force = u * Nomal force
    // Normal Force = mass * gravity
    // Acceleration = Force / mass
    // Friction Decelleration = u*g
    // a = v/d

    void Friction_Force()
    {
        float stoppingForce = kineticFrictionConstant * rb.mass * gravityAcceleration;
        Vector3 ballBase = transform.position - new Vector3(0, -0.02f, 0);

        rb.AddForceAtPosition(-rb.velocity / stoppingForce, ballBase, ForceMode.Impulse);
    }
}