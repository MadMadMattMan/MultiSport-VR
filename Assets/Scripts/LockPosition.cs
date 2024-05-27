using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPosition : MonoBehaviour
{
    [SerializeField] Vector3 targetPos, targetRot;

    void FixedUpdate()
    {
        GetComponent<Transform>().localPosition = targetPos;
        GetComponent<Transform>().localRotation = Quaternion.Euler(targetRot);
    }
}
