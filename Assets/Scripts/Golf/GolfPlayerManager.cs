using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GolfPlayerManager : MonoBehaviour
{
    [SerializeField] Transform[] playerStartLocations;
    [SerializeField] int[] par;
    [SerializeField] List<int> scoreCard = new(8);
    [SerializeField] List<TextMeshProUGUI> scoreCardText = new(10);
    Transform playerTF;
    [SerializeField] GameObject putter;
    [SerializeField] int currentHits, currentHole;
    [SerializeField] GameObject golfBall;

    [SerializeField] ClubPhysics putterPhysics;
    [SerializeField] TextMeshPro hitsText;

    private void Start()
    {
        playerTF = gameObject.transform;
        putter.SetActive(false);

        currentHole = 0;
        currentHits = 0;

        hitsText.text = "0";

        UpdateScoreCard();
    }

    public void BeginCourse()
    {
        Debug.Log("BeginCourse() started");
        putter.SetActive(true);
        MovePlayerToHole(0);
        SpawnBallAtHole();
    }

    public void BallOutOfBounds()
    {
        Destroy(putterPhysics.ball);
        putterPhysics.ball = null;

        Debug.Log("BallOutOfBounds tarted");
        BallHit();
        SpawnBallAtHole();
    }
         
    public void HoleScored()
    {
        Debug.Log("HoleScored() started");
        scoreCard[currentHole] = currentHits;
        currentHits = 0;
        currentHole++;
        hitsText.text = currentHits.ToString();

        if (currentHole < 8)
        {
            MovePlayerToHole(currentHole);
            SpawnBallAtHole();
            UpdateScoreCard();
        }
    }

    public void MovePlayerToHole(int LevelNumber)
    {
        Debug.Log("MovePlayerToHole() started");
        playerTF.position = playerStartLocations[LevelNumber].position;
    }

    GameObject currentBall;

    void SpawnBallAtHole()
    {
        putterPhysics.Enable_Physics();

        Debug.Log("SpawnBallAtHole() started");
        putterPhysics.ball = null;
        currentBall = Instantiate(golfBall, playerStartLocations[currentHole].position + new Vector3(0, 0.25f, 0), golfBall.transform.rotation);
        currentBall.GetComponent<GolfBall>().StartBall();

        currentBall.GetComponent<GolfBall>().manager = this;
        currentBall.GetComponent<GolfBall>().holeNumber = currentHole;

        putterPhysics.ball = currentBall;
    }

    public void BallHit()
    {
        currentHits++;
        hitsText.text = currentHits.ToString();
    }

    void UpdateScoreCard()
    {
        int totalScore = 0;
        for (int i = 0; i < scoreCard.Count; i++)
        {
            scoreCardText[i].text = scoreCard[i].ToString();

            if (scoreCard[i] == 0)
                scoreCardText[i].text = "-";

            totalScore += scoreCard[i];
        }

        scoreCardText[8].text = totalScore.ToString();

        if (totalScore == 0)
            scoreCardText[8].text = "-";

        scoreCardText[9].text = "(" + (totalScore-35).ToString() + ")";
    }
}
