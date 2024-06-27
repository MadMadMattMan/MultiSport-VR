using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BowlingBallCode : MonoBehaviour
{
    [SerializeField] Transform spawnLocation;
    [SerializeField] BowlingManager bowlingManager;
    [SerializeField] TextMeshProUGUI speedText;
    [SerializeField] BowlingStats statManager;

    bool bowlingReturn = false;
    AudioSource ballSound, machineSound;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        ballSound = GetComponent<AudioSource>();
        machineSound = GameObject.Find("Return Machine").GetComponent<AudioSource>();
    }

    void Update()
    {
        //If ball fallen down end or stopped on the lane, run ball return code
        if (transform.localPosition.y < -7.4 && !bowlingReturn || rb.velocity.magnitude <= 0.01f && !bowlingReturn && transform.localPosition.x < -25f)
        {
            bowlingReturn = true;
            StartCoroutine(ReturnBall());
        }
    }

    IEnumerator ReturnBall()
    {
        yield return new WaitForSeconds(1);

        //Stop all movement
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        //Update everything
        bowlingManager.BallBowled();

        yield return new WaitForSeconds(0.5f);

        //Move Ball
        GetComponent<Transform>().position = spawnLocation.position;
        bowlingReturn = false;

        //Play sound
        machineSound.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Speed Zone")
        {
            Debug.Log("Ball Speed Updated");
            //Sets speed text to string "Speed: " + the current speed of the ball (converted from m/s by * 3.6 to km/h))
            float speed = GetComponent<Rigidbody>().velocity.magnitude * 3.6f;

            statManager.UpdateSpeed(speed);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Bowling Plane")
        {
            ballSound.Play();
        }
    }
}
