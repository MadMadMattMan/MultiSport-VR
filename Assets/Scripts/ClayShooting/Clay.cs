using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clay : MonoBehaviour
{
 

    // Update is called once per frame
    void Update()
    {
       if(transform.position.y < 0)
        {
            Destroy(gameObject);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag== "Pallet")
        {
            Destroy(collision.gameObject); 
            // Particle system
            Destroy(gameObject);


        }
    }
}
