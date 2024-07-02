using Oculus.Haptics;
using Oculus.Interaction;
using Oculus.Interaction.DebugTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClubPhysics : MonoBehaviour
{
    [SerializeField] GolfPlayerManager manager;
    [SerializeField] GameObject clubPhysicsHead, clubGhostHead;
    [SerializeField] ClubLength currentClubLength;
    public GameObject ball;

    private void Start()
    {
        currentClubLength = GetComponentInParent<ClubLength>();

        clubPhysicsHead = gameObject; 

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

            //Run_Haptics();
            manager.BallHit();
        }
    }

    [SerializeField] AnimationCurve hapticCurve;



    void Run_Haptics()
    {
        OVRInput.Controller activeController = OVRInput.GetActiveController();

        float strength = hapticCurve.Evaluate(clubVelocity.magnitude);
        StartCoroutine(HapticsRoutine(strength, activeController));
    }

    IEnumerator HapticsRoutine(float pitch, OVRInput.Controller controller)
    {
        OVRInput.SetControllerVibration(pitch * 0.5f, pitch * 0.2f, controller);
        yield return new WaitForSeconds(0.1f);
        OVRInput.SetControllerVibration(0, 0, controller);
    }
}
