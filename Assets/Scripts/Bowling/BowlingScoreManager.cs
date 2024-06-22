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
    public bool gameOver;

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
        score = 0;
        gameOver = false;
    }

    public void UpdateFrameText(string frameScore)
    {
        //If gameOver, don't run
        if (gameOver)
            return;

        bool strike = false;
        bool spare = false;

        //If strike
        if (frameScore == "X")
        {
            strike = true;

            if (currentFrame < 9)
            {
                currentBall++;
            }
        }

        //If spare
        if (frameScore == "/")
            spare = true;

        //If reach 2nd last frame and no strike or spare, end game
        if (currentBall == 20 && !strike && !spare || currentBall > 20)
        {
            UpdateScore(frameScore);
            Debug.Log("Game Ended");
            gameOver = true;
            return;
        }

        frameTexts[currentBall].text = (StringToInt(frameScore) - tempScore).ToString();
        currentBall++;

        //Checks if current ball is even, if so total the score
        if (currentBall % 2 == 0 && currentBall != 20)
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
        recentScore += recentScore;

        //if tempscore is more than 10 (not possible), set it to 10 (the max)
        if (tempScore > 10 && currentFrame < 9)
            tempScore = 10;

        //Display temp score as total
        totalTexts[currentFrame].text = (score + tempScore).ToString();

        //Reset temp score
        score += tempScore;
        tempScore = 0;

        //Resets the pins
        bowlingManager.ResetPins();
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
