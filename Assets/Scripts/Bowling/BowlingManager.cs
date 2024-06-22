using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BowlingManager : MonoBehaviour
{
    [SerializeField] BowlingScoreManager scoreManager;

    [SerializeField] Transform[] pinLocations;

    [SerializeField] List<GameObject> balls; //Full 5

    [SerializeField] List<GameObject> fallenPins = new(10);
    [SerializeField] List<GameObject> allPins = new(10);
    [SerializeField] int pastFallenPins;

    private void Awake()
    {
        fallenPins.Clear();
    }  

    public void ResetPins()
    {
        pastFallenPins = 0;

        for (int i = 0; i < allPins.Count; i++)
        {
            //Reset pin
            allPins[i].GetComponent<Transform>().localPosition = Vector3.zero;
            allPins[i].GetComponent<Transform>().localRotation = Quaternion.Euler(Vector3.zero);
            allPins[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
            allPins[i].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            allPins[i].GetComponent<BowlingPins>().pinStatus = false;

            //Enable the pin again
            allPins[i].SetActive(true);
        }

        fallenPins.Clear();

    }

    public void BallBowled()
    {
        Debug.Log("Ball was bowled");

        //Destroys fallen pins
        RemovePins();

        //Checks score and notes it down
        scoreManager.UpdateFrameText(ScoreCheck());
    }

    void RemovePins()
    {
        Debug.Log("RemovePins()");

        if (fallenPins.Count != 0)
        {
            for (int i = 0; i < fallenPins.Count; i++)
            {
                fallenPins[i].gameObject.SetActive(false);
            }
        }
    }

    string ScoreCheck()
    {
        Debug.Log("ScoreCheck()");

        int pinsFallen = fallenPins.Count;

        string pinsFallenStr = pinsFallen.ToString();

        if (pinsFallen == 0)
            pinsFallenStr = "-";

        else if (pinsFallen == 10)
            pinsFallenStr = "X";

        else if (pastFallenPins + pinsFallen == 10)
            pinsFallenStr = "/";

        pastFallenPins = pinsFallen;
        fallenPins.Clear();
        return pinsFallenStr;
    }

    public void PinFallen(GameObject pin)
    {
        Debug.Log("PinFallen()");
        //Match the pin to a row for reset
        fallenPins.Add(pin);
    }



    //Order of Operations:
    //bowlingManager.PinFallen() [No Issues]

    //bowlingBallCode.ReturnBall() [No Issues]
    //bowlingManager.BallBowled() [No Issues]
    //bowlingManager.RemovePins() [No Issues]

    //scoreManager.UpdateFrameText(ScoreCheck()) 
    //scoreManager.UpdateScore()
}
