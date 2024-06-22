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
    int pastFallenPins;

    private void Awake()
    {
        fallenPins.Clear();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            BallBowled();
        }
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

        if (pinsFallen == 10)
            pinsFallenStr = "X";

        if (pastFallenPins + pinsFallen == 10)
            pinsFallenStr = "/";

        return pinsFallenStr;
    }

    public void PinFallen(GameObject pin)
    {
        Debug.Log("PinFallen()");
        //Match the pin to a row for reset
        fallenPins.Add(pin);
    }
}
