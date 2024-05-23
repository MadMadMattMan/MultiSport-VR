using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClubLength : MonoBehaviour
{
    [SerializeField] Transform handleEnd, baseTransform;
    [SerializeField] float clubHeight;
    public bool boolDetectHeight;
    float pastDistance;
    float range = 0.7171f;

    private void LateUpdate()
    {
        if (boolDetectHeight)
        {
            clubHeight = DetectHeight();
            baseTransform.localPosition = new Vector3(0, -clubHeight, 0);
        }
    }

    float DetectHeight()
    {
        //Forms ray conditions
        RaycastHit hit;
        Ray ray = new Ray(handleEnd.position, -handleEnd.up);
        
        //Gets Data (if raycast hits an object within range & colliders gameobject is apart of Ground layer)
        if (Physics.Raycast(ray, out hit, range) )
        {
            //Saves Data
            float distance = hit.distance;

            //Debugs
            Debug.DrawLine(handleEnd.position, hit.point, Color.red);
            Debug.Log("Club Height = " + distance);
            
            //Returns Data
            pastDistance = distance;
            return distance;
        }
        else //else returns previous distance to keep club same length
        {
            Debug.DrawRay(handleEnd.position, -handleEnd.up, Color.red);
            Debug.Log("Max Height");
            return pastDistance;
        }

        //0.199
        //0.8441 - 0.127
    }
}
