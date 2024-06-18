using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClayShooting : MonoBehaviour
{
    public float Platespeed = 30;
    public bool cooldown = false;
    [SerializeField] float cooldowntime;
    public GameObject ClayPrefab;


    void Update()
    {
        if(cooldown == false)
        {
            var clay = Instantiate(ClayPrefab, transform.position, transform.rotation);
            clay.GetComponent<Rigidbody>().velocity = transform.forward * Platespeed;
            cooldown = true;
            StartCoroutine(CoolDownPeriod());

            
        }
     

    }


    IEnumerator CoolDownPeriod()
    {
        yield return new WaitForSeconds(cooldowntime);
        cooldown= false;
    }
}
