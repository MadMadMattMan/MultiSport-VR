using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BowlingBallCode : MonoBehaviour
{
    [SerializeField] Transform spawnLocation;
    [SerializeField] BowlingManager bowlingManager;
    [SerializeField] TextMeshProUGUI speedText;

    bool bowlingReturn = false;

    void Update()
    {
        if (transform.localPosition.y < -7.4 && !bowlingReturn)
        {
            bowlingReturn = true;
            StartCoroutine(ReturnBall());
        }
    }

    IEnumerator ReturnBall()
    {
        yield return new WaitForSeconds(1);

        //Stop all movement
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        //Update everything
        bowlingManager.BallBowled();

        yield return new WaitForSeconds(0.5f);

        //Move Ball
        GetComponent<Transform>().position = spawnLocation.position;
        bowlingReturn = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Speed Zone")
        {
            Debug.Log("Ball Speed Updated");
            speedText.text = "Speed: " + (GetComponent<Rigidbody>().velocity.magnitude * 3.6).ToString("F2") + " km/h";
        }
    }
}
