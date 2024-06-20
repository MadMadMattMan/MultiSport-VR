using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClayShooting : MonoBehaviour
{
    public float Platespeed = 30;
    public bool cooldown = false;
    [SerializeField] float cooldowntime;
    public GameObject ClayPrefab;
    Quaternion defaultRotation;
    [SerializeField] float minX, maxX, minY, maxY;

    private void Start()
    {
        defaultRotation = transform.rotation;
    }

    void Update()
    {
        if(cooldown == false)
        {
           var vertical = Random.Range(minX, maxX);
           var horizontal = Random.Range(minY, maxY);
           Quaternion Rotation =  Quaternion.Euler(transform.rotation.eulerAngles.x + vertical, transform.rotation.eulerAngles.y + horizontal, 90);
            transform.rotation = Rotation;
           var clay = Instantiate(ClayPrefab, transform.position, transform.rotation, transform);
           clay.GetComponent<Rigidbody>().velocity = transform.forward * Platespeed;
           cooldown = true;
            StartCoroutine(CoolDownPeriod());

            transform.rotation = defaultRotation;
            
            
        }
     

    }


    IEnumerator CoolDownPeriod()
    {
        yield return new WaitForSeconds(cooldowntime);
        cooldown= false;
    }
}
