using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfBall : MonoBehaviour
{
    [SerializeField] ClubPhysics clubs;
    public bool detectingMovement;
    void Update()
    {
        if (detectingMovement && GetComponent<Rigidbody>().velocity.normalized == Vector3.zero)
        {
            detectingMovement = false;
            clubs.Enable_Physics();
        }
    }
}
