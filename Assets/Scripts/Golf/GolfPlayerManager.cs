using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfPlayerManager : MonoBehaviour
{
    [SerializeField] Transform[] playerStartLocations;
    Transform playerTF;

    private void Start()
    {
        playerTF = gameObject.transform;
    }


    public void MovePlayerTo(int LevelNumber)
    {
        //Takes imput number and -1 (as level 1 is start and 0 is start of array) and moves player to that level
        playerTF.position = playerStartLocations[LevelNumber - 1].position;
    }
         

}
