using System.Collections;
using System.Collections.Generic;
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
            //Friction_Force();
        }
    }

    [SerializeField] float frictionChangeSpeed = 1f;
    public float staticFrictionConstant = 0.6f; //Default = Rubber on nylon
    public float kineticFrictionConstant = 0.5f; //Default = Rubber on nylon
    public float gravityAcceleration = 9.81f; //Default = Earth gravity

    void Friction_Force()
    {
        //Friction force value (F = uN) where N = normal force (= to gravity force), Gravity force = mg
        //Force of friction {As Vector} = Vector3 direction {Normalized} * Friction force 

        //Store horizontal movement direction as a vector for applying a force
        Vector3 currentHorizontalDir = new Vector3(rb.velocity.normalized.x, 0, rb.velocity.normalized.x);

        //If ball is moving slow enough, run static friction calculation
        if (speed < frictionChangeSpeed)
        {
            //Force = (Opposite of movement dircection) * (u {static} * N {mass * gravity} 
            ///As static friction is scaled by movement speed {which is graphed by 1/speed to create exponentally more friction as speed decreases,
            float staticFrictionScalar = (1 / rb.velocity.magnitude);

            rb.AddForce(-currentHorizontalDir * (staticFrictionConstant * (gravityAcceleration * rb.mass)) * staticFrictionScalar);

            //To prevent ball rolling backward, if trajectory direction in horizontal movement axis is not same to actual direction (ie movement path has changed), stop the ball
            if (rb.velocity.normalized.x != currentHorizontalDir.x || rb.velocity.normalized.z != currentHorizontalDir.z)
            {
                rb.velocity = Vector3.zero;
            }
        }
        else //If ball is moving fast, run kinetic friction calculation
        {
            //Force = (Opposite of movement dircection) * (u {kinetic} * N {mass * gravity}
            rb.AddForce(-currentHorizontalDir * (kineticFrictionConstant * (gravityAcceleration * rb.mass)));
        }
    }
}
