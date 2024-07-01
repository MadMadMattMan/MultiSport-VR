using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BowlingStats : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI lastSpeedText, topSpeedText, lowSpeedText;
    float topSpeedf, lowSpeedf;
    string unit = " km/h";

    private void Start()
    {
        topSpeedf = 0.0f;
        lowSpeedf = 0.0f;
    }

    //Updates speed stat value and displays it with 2 decimal places .ToString("F2")
    public void UpdateSpeed(float speed)
    {
        lastSpeedText.text = "Speed: " + speed.ToString("F2") + unit;


        if (speed > topSpeedf)
            topSpeedText.text = "Top Speed: " + speed.ToString("F2") + unit;

        if (speed < lowSpeedf)
            lowSpeedText.text = "Low Speed: " + speed.ToString("F2") + unit;
    }
}
