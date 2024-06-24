using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Clay : MonoBehaviour
{
    public int score = 0;
    [SerializeField] TextMeshProUGUI scoretext;

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
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag== "Pallet")
        {
            Destroy(collision.gameObject); 
            // Particle system
            Destroy(gameObject);
            score = score+1;
            scoretext.text = "Score: " + score.ToString();

        }
    }
}
