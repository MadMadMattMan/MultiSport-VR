using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BowlingManager : MonoBehaviour
{
    [SerializeField] Transform[] pinLocations; //List[0] = null for simplicity

    [SerializeField] List<GameObject>[] listList  = new List<GameObject>[4];

    [SerializeField] List<GameObject> row1; int maxRow1 = 1; //Full 1
    [SerializeField] List<GameObject> row2; int maxRow2 = 2; //Full 2
    [SerializeField] List<GameObject> row3; int maxRow3 = 3; //Full 3
    [SerializeField] List<GameObject> row4; int maxRow4 = 4; //Full 4

    [SerializeField] List<GameObject> balls; //Full 5

    [SerializeField] List<string> score;
    
    List<int> maxPinLength;
    List<GameObject> fallenPins;

    int currentBall;
    int currentFrame;

    private void Awake()
    {
        FillActivePinLists();
        listList.SetValue(row1, 0);
        listList.SetValue(row2, 1);
        listList.SetValue(row3, 2);
        listList.SetValue(row4, 3);

        ResetMaxPinList();
    }

    
    void FillActivePinLists() 
    {
        row1.Add(GameObject.Find("Bowling Pin 1"));
        
        row2.Add(GameObject.Find("Bowling Pin 2"));
        row2.Add(GameObject.Find("Bowling Pin 3"));
        
        row3.Add(GameObject.Find("Bowling Pin 4"));
        row3.Add(GameObject.Find("Bowling Pin 5"));
        row3.Add(GameObject.Find("Bowling Pin 6"));
        
        row4.Add(GameObject.Find("Bowling Pin 7"));
        row4.Add(GameObject.Find("Bowling Pin 8"));
        row4.Add(GameObject.Find("Bowling Pin 9"));
        row4.Add(GameObject.Find("Bowling Pin 10"));
    }

    public void PinFallen(GameObject pin)
    {
        //Match the pin to a row for reset
        GameObject fallenPin = PinMatch(pin);
        fallenPins.Add(fallenPin);

        //Keep trying to destroy pin until it is destroyed (not moving)
        fallenPin.GetComponent<BowlingPins>().pinStatus = true;
    }

    public void BallBowled()
    {
        //Destroys fallen pins
        RemovePins();

        //Checks score and notes it down
        score.Add(scoreCheck());
        
        //Progresses the ball number
        currentBall++;

        //Progresses the frame if ball number is more than 2 balls bowled (unless last frame)
        if (currentBall >= 3 && currentFrame < 10)
        {
            currentBall = 0;
            currentFrame++;
        }
        else if (currentFrame >= 10)
        {
            //End game
        }
    }

    void RemovePins()
    {
        for (int i = 0; fallenPins.Count > 0; i++)
        {
            Destroy(fallenPins[i]);
        }
    }

    string scoreCheck()
    {
        int pinsFallen = 0;

        for (int i = 0; i < listList.Length; i++)
        {
            //pinsFallen += maxPins - listList.Count();
            pinsFallen += (i + 1) - listList[i].Count();
        }

        return pinsFallen.ToString();
    }

    GameObject PinMatch(GameObject pin)
    {
        for (int i = 0; i < listList.Length; i++)
        {
            for (int j = 0; j < listList[i].Count; j++)
            {
                if (listList[i][j] == pin)
                {
                    listList[i].Remove(pin);
                    return pin;
                }
            }
        }

        Debug.LogError("Fallen pin has no match");
        return null;
    }

    void ResetMaxPinList()
    {
        maxPinLength[0] = 1;
        maxPinLength[1] = 2;
        maxPinLength[2] = 3;
        maxPinLength[3] = 4;
    }

}
