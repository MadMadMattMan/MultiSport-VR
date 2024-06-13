using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalletCode : MonoBehaviour
{
    public float PalletTime = 5;

    void Awake()
    {
        Destroy(gameObject,PalletTime);
    }
   void OnTriggerEnter(Collider collision)
    {
      // Destroy(collision.gameObject);
        //Destroy(gameObject);
    }
}
    