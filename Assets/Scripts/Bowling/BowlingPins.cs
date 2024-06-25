using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingPins : MonoBehaviour
{
    [SerializeField] BowlingManager bowlingManager;

    [SerializeField] float yFallThreshold = 0.2f, yValue;

    AudioSource pinSound;

    public bool pinStatus;

    private void Start()
    {
        pinSound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!pinStatus)
        {
            yValue = transform.position.y;

            if (yValue < yFallThreshold)
            {
                pinStatus = true;
                Debug.Log("Pin Fallen");
                bowlingManager.PinFallen(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        pinSound.Play();
    }
}
