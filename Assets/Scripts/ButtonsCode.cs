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

    public void LoadGolf()
    {
        SceneManager.LoadScene(2);
        Debug.Log("Clicked Golf");
    }
    public void LoadClayShooting()
    {
        SceneManager.LoadScene(3);
        Debug.Log("Clicked ClayShooting");
    }
    public void LoadControls()
    {
        SceneManager.LoadScene(4);
        Debug.Log("Clicked Controls");
    }
    public void LoadGolfControls()
    {
        SceneManager.LoadScene(5);
        Debug.Log("Clicked Golf Controls");
    }
    public void LoadQuit()
    {
       Application.Quit();
    }
}
    