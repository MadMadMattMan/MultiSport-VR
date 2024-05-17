using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction.Input;
using System.Diagnostics.Tracing;
using System.ComponentModel;

public class ClubHandScript : MonoBehaviour
{
    Vector3 clubOffset, clubRotaion;
    [SerializeField] GameObject ClubPrefab, ClubCurrent = null;
    Transform PlayerReferance; 
    [SerializeField] GameObject TestHand;
    [SerializeField] Transform LeftController, RightController;
    [SerializeField] GameObject ClubL, ClubR;
    [SerializeField] GameObject LeftControllerObj, RightControllerObj;
    [SerializeField] bool gripL = false, gripR = false;
    [SerializeField] string clubSide = null;

    void Start()
    {
        if (clubSide == null && !OVRInput.connectedControllerTypes.Equals("None"))
        {
            clubSide = "r";
            ClubCurrent = ClubL;
            ClubR.SetActive(true);
            ClubL.SetActive(false);
            RightControllerObj.SetActive(false);
            LeftControllerObj.SetActive(true);
        }

        //TestClub();

        Debug.Log(OVRInput.connectedControllerTypes.ToString());
    }

    void Update()
    {
        gripL = OVRInput.Get(OVRInput.Button.PrimaryThumbstick);
        gripR = OVRInput.Get(OVRInput.Button.SecondaryThumbstick);

        if (clubSide != "l" && gripL)
        {
            gripL = false;
            clubSide = "l";
            ClubCurrent = ClubL;
            ClubR.SetActive(false);
            ClubL.SetActive(true);
            RightControllerObj.SetActive(true);
            LeftControllerObj.SetActive(false);
        }
        else if (clubSide != "r" && gripR)
        {
            gripR = false;
            clubSide = "r";
            ClubCurrent = ClubL;
            ClubR.SetActive(true);
            ClubL.SetActive(false);
            RightControllerObj.SetActive(false);
            LeftControllerObj.SetActive(true);

        }
        else if (gripR || gripL)
        {
            gripR = false;
            gripL = false;
        }
    }

    void TestClub()
    {
        clubSide = "pc";
        TestHand.SetActive(true);
        Destroy(ClubCurrent);
        ClubCurrent = Instantiate(ClubPrefab, TestHand.transform.position + clubOffset, Quaternion.Euler(TestHand.transform.rotation.eulerAngles + clubRotaion), TestHand.transform);

    }

    Vector3 ClubSpawnPos()
    {
        if (clubSide == "r")
        {
            return RightController.position + clubOffset;
        }
        else
        {
            return LeftController.position + clubOffset;
        }
    }

    Quaternion ClubSpawnRot()
    {
        Vector3 i;
        if (clubSide == "r")
        {
            i = RightController.transform.eulerAngles + clubRotaion + SnapTurnCorection();
        }
        else
        {
            i = LeftController.transform.eulerAngles + clubRotaion + SnapTurnCorection();
        }

        return Quaternion.Euler(i);
    }

    Vector3 SnapTurnCorection()
    {
        float i = PlayerReferance.rotation.y;

        return new Vector3(0, i, 0);
    }
}
