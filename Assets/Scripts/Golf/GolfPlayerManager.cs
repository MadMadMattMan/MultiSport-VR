using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfPlayerManager : MonoBehaviour
{
    [SerializeField] Transform[] playerStartLocations;
    [SerializeField] int[] par;
    [SerializeField] List<int> scoreCard = new(8);
    Transform playerTF;
    GameObject putter;
    [SerializeField] int currentHits;
    [SerializeField] GameObject golfBall;

    private void Start()
    {
        playerTF = gameObject.transform;
        putter = GameObject.Find("golf_putter");
        putter.SetActive(false);
    }

    public void BeginCourse()
    {
        putter.SetActive(true);
        MovePlayerToHole(0);
    }

    
         
    public void HoleScored(int LevelNumber)
    {
        scoreCard[LevelNumber] = currentHits;
        currentHits = 0;
        MovePlayerToHole(LevelNumber++);
        SpawnBallAtHole(LevelNumber++);
    }

    public void MovePlayerToHole(int LevelNumber)
    {
        playerTF.position = playerStartLocations[LevelNumber].position;
    }

    void SpawnBallAtHole(int LevelNumber)
    {
        GameObject ball = Instantiate(golfBall, playerStartLocations[LevelNumber].position, golfBall.transform.rotation);
        ball.GetComponent<GolfBall>().manager = this;
        ball.GetComponent<GolfBall>().holeNumber = LevelNumber;
    }
}
