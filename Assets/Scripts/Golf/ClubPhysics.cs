using Oculus.Interaction.DebugTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClubPhysics : MonoBehaviour
{
    public GameObject ball, clubPhysicsHead, clubGhostHead;
    [SerializeField] ClubLength currentClubLength;

    public void Disable_Physics()
    {
        //DebugLog
        Debug.Log("Set GhostHead");

        //Changes clubs head to ghost
        clubPhysicsHead.SetActive(false);
        clubGhostHead.SetActive(true);

        //Disables Height detection and length extention
        currentClubLength.boolDetectHeight = false;
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
    [SerializeField] float clubWeight = 1;
    float contactTime = 0.000256f;
    [SerializeField] float clubPower, clubChip, clubSpin;

    [SerializeField] float pastX, pastY, pastZ;

    [SerializeField] Vector3 clubVelocity;

    private void Update()
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

        return deltaDir;
    }

    void Physics_Calculation(Rigidbody Ball)
    {
        //p = mv
        float clubMomentum = clubVelocity.magnitude * clubWeight;
        //Vector3 angularVelocity = Vector3.zero;


        //F = p/t (* power for adjustments)
        Vector3 ballForce = clubVelocity.normalized * (clubMomentum / contactTime);
        Ball.AddForce(ballForce, ForceMode.Impulse);
        //Ball.AddRelativeTorque(angularVelocity.normalized * clubSpin, ForceMode.Impulse);

        Debug.Log("ClubSpeed = " + clubVelocity);
        Debug.Log("ClubSpeedMag = " + clubVelocity.magnitude);
        Debug.Log("Force = " + ballForce);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            Disable_Physics();
            Physics_Calculation(ball.GetComponent<Rigidbody>());

        }
    }
}
