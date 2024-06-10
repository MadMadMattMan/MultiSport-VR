using Oculus.Interaction;
using Oculus.Interaction.DebugTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClubPhysics : MonoBehaviour
{
    [SerializeField] GameObject ball, clubPhysicsHead, clubGhostHead;
    [SerializeField] ClubLength currentClubLength;

    private void Start()
    {
        currentClubLength = GetComponentInParent<ClubLength>();

        clubPhysicsHead = gameObject;
        clubGhostHead = GameObject.FindGameObjectWithTag("Ghost Head");
        ball = GameObject.FindGameObjectWithTag("Ball");    

        clubGhostHead.SetActive(false);
    }

    public void Disable_Physics()
    {
        //DebugLog
        Debug.Log("Set GhostHead");

        //Changes clubs head to ghost
        clubPhysicsHead.SetActive(false);
        clubGhostHead.SetActive(true);

        //Disables Height detection and length extention
        //currentClubLength.boolDetectHeight = false;
        ball.GetComponent<GolfBall>().detectingMovement = true;
    }
    
    public void Enable_Physics()
    {
        //DebugLog
        Debug.Log("Removed GhostHead");

        //Changes clubs head to physical
        clubPhysicsHead.SetActive(true);
        clubGhostHead.SetActive(false);

        //Enables Height detection and length extention
        //currentClubLength.boolDetectHeight = true;
    }

    [Header("Physics Caculations")]
    float clubMass = 0.33f;
    float contactTime = 0.000256f;
    [SerializeField] float clubPower, clubChip, clubSpin;

    [SerializeField] float pastX, pastY, pastZ;

    [SerializeField] Vector3 clubVelocity;

    private void FixedUpdate()
    {
        clubVelocity = Velocity_Calculation();
    }

    Vector3 Velocity_Calculation()
    {
        Transform tf = clubPhysicsHead.GetComponent<Transform>();
        Vector3 deltaDir = new Vector3();

        //v = d/t
        deltaDir.x = ((tf.position.x - pastX) * Time.deltaTime) * clubPower;
        deltaDir.y = ((tf.position.y - pastY) * Time.deltaTime) * clubChip;
        deltaDir.z = ((tf.position.z - pastZ) * Time.deltaTime) * clubPower;

        pastX = tf.position.x;
        pastY = tf.position.y;
        pastZ = tf.position.z;

        currentSpeed = deltaDir.magnitude;

        return deltaDir;
    }

    [SerializeField] float minCollisionSpeed = 0.5f;
    [SerializeField] float currentSpeed;

    void Physics_Calculation(Rigidbody Ball, Collision collision)
    {
        Vector3 collisionVelocity = Velocity_Calculation();

        //p = mv
        float clubMomentum = clubMass * collisionVelocity.magnitude;

        //F = p/t (* power for adjustments)
        //Directional force = direction vector * Force scalar (p/t)
        Vector3 ballForce = clubVelocity.normalized * (clubMomentum / contactTime);

        if (Ball.gameObject.GetComponent<GolfBall>().speed <= minCollisionSpeed)
        {
            //Add the force to the ball
            Ball.velocity = Vector3.Reflect(Ball.velocity, collision.contacts[0].normal).normalized * clubVelocity.magnitude * clubPower;

        }

        Debug.Log("ClubSpeedMag = " + clubVelocity.magnitude);
        Debug.Log("CollisionVelocityExit = " + Ball.velocity);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            Disable_Physics();
            Physics_Calculation(ball.GetComponent<Rigidbody>(), collision);
            Debug.Log("Collided with Ball");
        }
    }
}
