using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ClayObject : MonoBehaviour
{
    Clay Manager;

    private void Start()
    {
        Manager=GameObject.Find("ClayCode").GetComponent<Clay>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Pallet")
        {
            Manager.Hit(gameObject, collision.gameObject);

        }
    }
}
