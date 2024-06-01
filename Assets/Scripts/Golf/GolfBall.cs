using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfBall : MonoBehaviour
{
    [SerializeField] ClubPhysics currentClub;
    Rigidbody rb;
    [SerializeField] float speed, timeAfterHit;

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

    [SerializeField] float frictionChangeSpeed, hitDelay;;
    [SerializeField] float fastSpeedFrictionMultiplier, slowSpeedFrictionMultiplier;
    public float staticFrictionConstant = 0.6f; //Default = Rubber on nylon
    public float kineticFrictionConstant = 0.5f; //Default = Rubber on nylon
    public float gravityAcceleration = 9.81f; //Default = Earth gravity

    void Friction_Force()
    {
        //Friction force value (F = uN) where N = normal force (= to gravity force), Gravity force = mg
        //Force of friction {As Vector} = Vector3 direction {Normalized} * Friction force 

        //If ball is moving slow enough, run static friction calculation
        if (speed < frictionChangeSpeed)
        {
            Vector3 currentDir = rb.velocity.normalized;

            //Force = (Opposite of movement dircection) * (u {static} * N {mass * gravity} 
            ///As static friction is scaled by movement speed {which is graphed by 1/speed to create exponentally more friction as speed decreases,
            float staticFrictionScalar = (1 / rb.velocity.magnitude);

            rb.AddRelativeForce(-rb.velocity.normalized * (staticFrictionConstant * (gravityAcceleration * rb.mass)) * staticFrictionScalar);



            //To prevent ball rolling backward, if trajectory direction in all 3 axis is not same to actual direction (ie movement path has changed), stop the ball
            if (rb.velocity.normalized.x != currentDir.x || rb.velocity.normalized.y != currentDir.y || rb.velocity.normalized.z != currentDir.z)
            {
                rb.velocity = Vector3.zero;
            }
        }
        else
        {
            //Force = (Opposite of movement dircection) * (u {kinetic} * N {mass * gravity}
            rb.AddRelativeForce(-rb.velocity.normalized * (kineticFrictionConstant * (gravityAcceleration * rb.mass)));
        }
    }
}
