using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Oculus.Platform.Models;
using System;

public class BowlingScoreManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI[] frameTexts;
    public TextMeshProUGUI[] totalTexts;
    public int score;

    [SerializeField] int currentBall, currentFrame, tempScore;
    [SerializeField] BowlingManager bowlingManager;

    private void Start()
    {
        for (int i = 0; i < frameTexts.Length; i++)
            frameTexts[i].text = "";

        for (int i = 0; i < totalTexts.Length; i++)
            totalTexts[i].text = "";

        nameText.text = "Player 1";

        currentBall = 0;
        currentFrame = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpdateFrameText("1".ToString());
        }
    }

    public void UpdateFrameText(string frameScore)
    {
        bool strike = false;
        bool spare = false;

        //If strike
        if (frameScore == "X")
            strike = true;

        //If spare
        if (frameScore == "/")
            spare = true;

        //If reach 2nd last frame and no strike or spare, end game
        if (currentBall == 20 && !strike && !spare)
        {
            Debug.Log("Game Ended");
            Destroy(bowlingManager.gameObject);
            return;
        }
        else if (currentBall > 20)
        {
            Debug.Log("Game Ended");
            Destroy(bowlingManager.gameObject);
            return;
        }

        frameTexts[currentBall].text = frameScore;
        currentBall++;
        if (currentBall == 3 || currentBall == 5 || currentBall == 7 || currentBall == 9 || currentBall == 11 || currentBall == 13 || currentBall == 15 || currentBall == 17 || currentBall == 19)
        {
            UpdateScore(frameScore);
            currentFrame++;
        }
        else
            tempScore += StringToInt(frameScore);
    }

    void UpdateScore(string frameScore)
    {
        //Get ball score
        int recentScore = StringToInt(frameScore);
        
        //Add it to temp score
        tempScore += recentScore;

        //if tempscore is more than 10 (not possible), set it to 10 (the max)
        if (tempScore > 10)
            tempScore = 10;

        //Display temp score as total
        totalTexts[currentFrame].text = (score + tempScore).ToString();

        //Reset temp score
        tempScore = 0;
    }

    int StringToInt(string convertee)
    {
        //If Strike
        if (convertee == "X")
            return 10;

        //If spare
        else if (convertee == "/")
            return 10;

        //If no score
        else if (convertee == "-")
            return 0;

        else
        {
            for (int i = 1; i <= 10; i++)
            {
                if (convertee == i.ToString())
                {
                    return i;
                }   
            }
        }

        return 0;
    }
}
