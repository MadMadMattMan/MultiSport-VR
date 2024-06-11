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
    [SerializeField] float clubPower, clubChip;

    [SerializeField] float pastX, pastY, pastZ;

    [SerializeField] Vector3 clubVelocity;

    private void FixedUpdate()
    {
        clubVelocity = Velocity_Calculation();
    }

    Vector3 Velocity_Calculation()
    {
        Vector3 initialPos = clubPhysicsHead.GetComponent<Transform>().position;
        Vector3 newVelocity;

        //v = d/t
        newVelocity.x = (initialPos.x - pastX) / Time.deltaTime;
        newVelocity.y = ((initialPos.y - pastY) / Time.deltaTime) + clubChip;
        newVelocity.z = (initialPos.z - pastZ) / Time.deltaTime;

        pastX = initialPos.x;
        pastY = initialPos.y;
        pastZ = initialPos.z;

        return newVelocity.normalized;
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            Disable_Physics();
            //Physics_Calculation(ball.GetComponent<Rigidbody>(), collision);
            //Physics_Calculation(ball.GetComponent<Rigidbody>());
            ball.GetComponent<Rigidbody>().velocity = clubVelocity * clubPower;
            Debug.Log("Collided with Ball");
            Debug.Log("Gave ball a velocity with speed " + clubVelocity.magnitude);
        }
    }
}
