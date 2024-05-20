using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction.Input;
using System.Diagnostics.Tracing;
using System.ComponentModel;
using UnityEngine.Rendering;

public class ClubHandScript : MonoBehaviour
{
    [SerializeField] GameObject putter;
    Rigidbody putterRb;
    FixedJoint putterFj;
    [SerializeField] GameObject controllerR, controllerL;
    [SerializeField] Transform playerOrigin, playerHead;
    float playerHeight;
    [SerializeField] bool isHoldingL, isHoldingR, isTouching;

    void Start()
    {
        putterRb = putter.GetComponent<Rigidbody>();
        putterFj = putter.GetComponent<FixedJoint>();
        playerHeight = playerHead.position.y - playerOrigin.position.y;
        SpawnClub();
    }

    void SpawnClub()
    {
        putter.SetActive(true);
        putter.GetComponent<Transform>().position = ClubTargetPosition();
    }
    
    Vector3 ClubTargetPosition()
    {
        return new Vector3(playerOrigin.position.x, (playerHeight * 0.75f), (playerOrigin.position.z + 1));
    }

    private void FixedUpdate()
    {
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) >= 0.75f)
            isHoldingL = true;
        else
            isHoldingL = false;

        if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) >= 0.75f)
            isHoldingR = true;
        else
            isHoldingR = false;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == controllerR && isHoldingR && putterFj.connectedBody == null)
        {
            putterFj.connectedBody = controllerR.GetComponent<Rigidbody>();    
            putter.GetComponent<MeshCollider>().isTrigger = false;
        }
        else if (other.gameObject == controllerL && isHoldingL && putterFj.connectedBody == null)
        {
            putterFj.connectedBody = controllerL.GetComponent<Rigidbody>();
            putter.GetComponent<MeshCollider>().isTrigger = false;
        }
    }
}
