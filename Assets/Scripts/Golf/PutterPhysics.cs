using Oculus.Interaction.DebugTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutterPhysics : MonoBehaviour
{
    public GameObject ball, putterHead, putterGhost;
    [SerializeField] ClubLength currentClubLength;

    public void Disable_GhostHead()
    {
        //DebugLog
        Debug.Log("Set GhostHead");

        //Changes clubs head to ghost
        putterHead.SetActive(false);
        putterGhost.SetActive(true);

        //Disables Height detection and length extention
        currentClubLength.boolDetectHeight = false;
    }
    
    public void Enable_PhysicsHead()
    {
        //DebugLog
        Debug.Log("Removed GhostHead");

        //Changes clubs head to physical
        putterHead.SetActive(true);
        putterGhost.SetActive(false);

        //Enables Height detection and length extention
        currentClubLength.boolDetectHeight = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("ball"))
        {

        }
    }
}
