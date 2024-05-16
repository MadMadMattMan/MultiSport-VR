using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction.Input;

public class ClubHandScript : MonoBehaviour
{
    [SerializeField] Vector3 clubOffset;
    [SerializeField] GameObject ClubPrefab;
    [SerializeField] GameObject ClubCurrent = null;
    [SerializeField] Transform LeftController, RightController;
    [SerializeField] GameObject LeftControllerObj, RightControllerObj;
    [SerializeField] bool gripL;
    [SerializeField] bool gripR;
    [SerializeField] string clubSide = "r";

    void Update()
    {
        gripL = OVRInput.Get(OVRInput.Button.PrimaryThumbstick, OVRInput.Controller.LTouch);
        gripR = OVRInput.Get(OVRInput.Button.PrimaryThumbstick, OVRInput.Controller.RTouch);

        if (clubSide == "r" && gripL)
        {
            gripL = false;
            clubSide = "l";
            Destroy(ClubCurrent); LeftControllerObj.SetActive(false); RightControllerObj.SetActive(true); 
            ClubCurrent = Instantiate(ClubPrefab, LeftController);
        }
        else if (clubSide == "l" && gripR)
        {
            gripR = false;
            clubSide = "r";
            Destroy(ClubCurrent); LeftControllerObj.SetActive(true); RightControllerObj.SetActive(false);
            ClubCurrent = Instantiate(ClubPrefab, RightController);
        }
        else if (gripR || gripL)
        {
            gripR = false;
            gripL = false;
        }
    }
}
