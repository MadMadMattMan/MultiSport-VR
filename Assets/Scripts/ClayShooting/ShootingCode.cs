using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
public class ShootingCode : MonoBehaviour
{
    public GameObject PalletPrefab;

    public Transform PalletSpawnpoint;

    public float PalletSpeed = 30;

    public bool cooldown = false;
    [SerializeField] float cooldowntime;

    void Update()
    {
        float triggerPress = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
        float triggerPress2 = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);

        if (triggerPress >= 0.75f | triggerPress2 >= 0.75f && !cooldown)
        {
           var pallet = Instantiate(PalletPrefab, PalletSpawnpoint.position, PalletSpawnpoint.rotation);
           pallet.GetComponent<Rigidbody>().velocity = -PalletSpawnpoint.forward.normalized * PalletSpeed;
            cooldown = true;
            StartCoroutine(waitingperoid());
        }
      
    }

    IEnumerator waitingperoid()
    {
       yield return new WaitForSeconds((cooldowntime));
        cooldown = false;
    }

}
