using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsCode : MonoBehaviour
{
    public void LoadBowling()
    {
        SceneManager.LoadScene(1);
        Debug.Log("Clicked Bowling");
    }
}
    