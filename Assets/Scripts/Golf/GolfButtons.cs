using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GolfButtons : MonoBehaviour
{
    [SerializeField] GolfPlayerManager player;

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
        SceneManager.UnloadSceneAsync(2);
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
        player.BeginCourse();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
    }
}
