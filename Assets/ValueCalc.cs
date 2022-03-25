using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueCalc : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float angleX = gameObject.transform.rotation.eulerAngles.x;
        float angleY = gameObject.transform.rotation.eulerAngles.y;
        float angleZ = gameObject.transform.rotation.eulerAngles.z;

        float angleXZ = (angleX + angleZ) % 360;

        int orientation = 0;
        
        if(FuzzyBounds(angleX, 90))
        {
            orientation = 2;
        }
        else if(FuzzyBounds(angleX, 270))
        {
            orientation = 5;
        }
        else if(FuzzyBounds(angleXZ, 0))
        {
            orientation = 3;
        }
        else if(FuzzyBounds(angleXZ, 90))
        {
            orientation = 6;
        }
        else if(FuzzyBounds(angleXZ, 180))
        {
            orientation = 4;
        }
        else if(FuzzyBounds(angleXZ, 270))
        {
            orientation = 1;
        }

        Debug.Log(orientation);
        
    }

    bool FuzzyBounds(float angle, int toCheck)
    {
        int margin = 6;
        int lowerBound = toCheck - margin;
        int upperBound = toCheck + margin;

        if (angle >= 360){
			angle = angle - 360;
		}

        if(lowerBound >= 0 && upperBound < 360){
            return angle >= lowerBound && angle <= upperBound;
        }

        if(lowerBound < 0){
            if(angle > upperBound){
                angle = angle - 360;
            }
            return angle >= lowerBound && angle <= upperBound;
        }

        if(angle < lowerBound){
            angle = angle + 360;
        }
        return angle >= lowerBound && angle <= upperBound;

    }
}
