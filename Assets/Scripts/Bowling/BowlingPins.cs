using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingPins : MonoBehaviour
{
    [SerializeField] BowlingManager bowlingManager;

    [SerializeField] float yFallThreshold = 0.2f, yValue;

    public bool markForDesturction = false;


    private void Update()
    {
        yValue = transform.position.y;

        if (transform.position.y < yFallThreshold)
        {
            bowlingManager.PinFallen(gameObject);
            Debug.Log("Pin Fallen");
        }
        
        //If  pin isn't moving and marked fr distrctuion, destory it.
        if (GetComponent<Rigidbody>().velocity != Vector3.zero && GetComponent<Rigidbody>().angularVelocity != Vector3.zero && markForDesturction)
        {
            Destroy(gameObject);
        }
        
    }
}
