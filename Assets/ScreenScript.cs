using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScreenScript : MonoBehaviour
{
    [SerializeField] Material crackedScreen;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
            gameObject.GetComponent<MeshRenderer>().materials.SetValue(crackedScreen, 1);
    }
}
