using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
public class ShootingCode : MonoBehaviour
{
    public GameObject palletprefab;

    public Transform PalletSpawn;

    public float palletspeed = 30;

    public float bullettime = 5;


    void Update()
    {
        float triggerPress = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
        float triggerPress2 = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);

        if (triggerPress >= 0.75f)
        {
            //Shoot gun
        }
    }
    //  private IEnumerator DestroyPalletAfterTime(GameObject pallet, float delay)

}
