using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsCode : MonoBehaviour
{
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
        SceneManager.UnloadSceneAsync(1);
        SceneManager.UnloadSceneAsync(3);
        Debug.Log("Clicked Menu");
    }
    public void LoadBowling()
    {
      
        SceneManager.LoadScene(1);
        SceneManager.UnloadSceneAsync(0);
        Debug.Log("Clicked Bowling");
    }

    public void LoadGolf()
    {
        SceneManager.LoadScene(2);
        SceneManager.UnloadSceneAsync(0);
        Debug.Log("Clicked Golf");
    }
    public void LoadClayShooting()
    {
        SceneManager.LoadScene(3);
        SceneManager.UnloadSceneAsync(0);
        Debug.Log("Clicked ClayShooting");
    }
    public void LoadQuit()
    {
       Application.Quit();
    }
}
    