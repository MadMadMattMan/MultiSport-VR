using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GolfButtons : MonoBehaviour
{
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
        Debug.Log("Clicked Menu");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void StartGame()
    {
        Debug.Log("Game Started");
        SceneManager.LoadScene(0);
    }

}
