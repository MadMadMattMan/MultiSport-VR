using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfPlayerManager : MonoBehaviour
{
    [SerializeField] Transform[] playerStartLocations;
    [SerializeField] int[] par;
    [SerializeField] List<int> scoreCard = new(8);
    Transform playerTF;
    [SerializeField] GameObject putter;
    [SerializeField] int currentHits, currentHole;
    [SerializeField] GameObject golfBall;

    [SerializeField] ClubPhysics putterPhysics;

    private void Start()
    {
        playerTF = gameObject.transform;
        putter.SetActive(false);

        currentHole = 0;
    }

    public void BeginCourse()
    {
        Debug.Log("BeginCourse() started");
        putter.SetActive(true);
        MovePlayerToHole(0);
        SpawnBallAtHole();
    }

    
         
    public void HoleScored()
    {
        Debug.Log("HoleScored() started");
        scoreCard[currentHole] = currentHits;
        currentHits = 0;
        currentHole++;
        MovePlayerToHole(currentHole);
        SpawnBallAtHole();
    }

    public void MovePlayerToHole(int LevelNumber)
    {
        Debug.Log("MovePlayerToHole() started");
        playerTF.position = playerStartLocations[LevelNumber].position;
    }

    void SpawnBallAtHole()
    {
        Debug.Log("SpawnBallAtHole() started");
        GameObject ball = Instantiate(golfBall, playerStartLocations[currentHole].position + new Vector3(0, 0.25f, 0), golfBall.transform.rotation);
        ball.GetComponent<GolfBall>().manager = this;
        ball.GetComponent<GolfBall>().holeNumber = currentHole;

        putterPhysics.ball = ball;
    }
}
