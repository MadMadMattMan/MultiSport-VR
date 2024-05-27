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

    void Physics_Collision()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ball")
        {
            Disable_Physics();

        }
    }
}
