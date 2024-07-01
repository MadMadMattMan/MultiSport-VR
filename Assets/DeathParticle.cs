using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathParticle : MonoBehaviour
{
    public AudioSource ClayBreaking;

    private void Awake()
    {
        ClayBreaking.Play();

        //Destroy when done
    }
    
}
