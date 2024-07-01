using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Oculus.Interaction;

public class Clay : MonoBehaviour
{
    public int score = 0;
    [SerializeField] TextMeshProUGUI scoretext;
    [SerializeField] GameObject DeathEffect;

    // Update is called once per frame

    void Start()
    {
        score = 0;
    }
    void Update()
    {
       if(transform.position.y < 0)
        {
            Destroy(gameObject);
        }

    }

    public  void Hit(GameObject Clay, GameObject pallet)
    {
        Destroy(pallet.gameObject);
        Debug.Log("Pallet destroyed");
        
        Instantiate(DeathEffect, Clay.transform.position, Clay.transform.rotation);

        Destroy(Clay.gameObject);
        Debug.Log("Clay Has been Destroyed");

        score = score + 1;
        scoretext.text = "Score: " + score.ToString();
        Debug.Log("Score Increased");
    }

}
