using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BowlingManager : MonoBehaviour
{
    [SerializeField] Transform[] pinLocations; //List[0] = null for simplicity

    [SerializeField] List<GameObject>[] listList  = new List<GameObject>[4];

    [SerializeField] List<GameObject> row1; //Full 1
    [SerializeField] List<GameObject> row2; //Full 2
    [SerializeField] List<GameObject> row3; //Full 3
    [SerializeField] List<GameObject> row4; //Full 4

    private void Awake()
    {
        FillActivePinLists();
        listList.SetValue(row1, 0);
        listList.SetValue(row2, 1);
        listList.SetValue(row3, 2);
        listList.SetValue(row4, 3);
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

        //Keep trying to destroy pin until it is destroyed (not moving)
        fallenPin.GetComponent<BowlingPins>().markForDesturction = true;
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
}
