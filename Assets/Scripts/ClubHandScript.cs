using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction.Input;
using System.Diagnostics.Tracing;
using System.ComponentModel;
using UnityEngine.Rendering;

public class ClubHandScript : MonoBehaviour
{
    Transform clubTarget;
    [SerializeField] GameObject putter;
    Rigidbody putterRb;
    [SerializeField] GameObject controllerR, controllerL;
    [SerializeField] Transform playerOrigin, playerEye;
    float height;
    bool holding;

    void Start()
    {
        height = playerEye.position.y - playerOrigin.position.y;
        SpawnClub();
    }

    void SpawnClub()
    {
        putter.SetActive(true);
        putter.GetComponent<Transform>().position = clubTarget.position;
        putter.GetComponent<Transform>().rotation = clubTarget.rotation;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == controllerR)
        {
            putter.GetComponent<FixedJoint>().connectedBody = controllerR.GetComponent<Rigidbody>();
            putter.GetComponent<MeshCollider>();
        }
        else if (other.gameObject == controllerL)
        {
            putter.GetComponent<FixedJoint>().connectedBody = controllerL.GetComponent<Rigidbody>();
        }
    }
}
