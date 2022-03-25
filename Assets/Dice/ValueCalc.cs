using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueCalc : MonoBehaviour
{
    public bool logToConsole;
    public int dieType;

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


        switch(dieType)
        {
            case 6:
                orientation = d6Val();
                break;
            case 8:
                orientation = d8Val();
                break;          
        }

        if(logToConsole){
            Debug.Log(gameObject.name + ": " + orientation);
        }        
    }

    bool FuzzyBounds(float angle, int toCheck)
    {
        int margin = 6;
        int lowerBound = toCheck - margin;
        int upperBound = toCheck + margin;

       if (lowerBound < 0 && angle > upperBound){
        angle = angle - 360;
        }
        else if (angle < lowerBound){
        angle = angle + 360;
        }
        return angle >= lowerBound && angle <= upperBound;

    }

    int d6Val()
    {
        float angleX = gameObject.transform.rotation.eulerAngles.x;
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

        return orientation;
    }

    int d8Val()
    {
        float angleX = gameObject.transform.rotation.eulerAngles.x;
        float angleZ = gameObject.transform.rotation.eulerAngles.z;
        float angleXZ = (angleX + angleZ) % 360;

        int orientation = 0;

        if(angleX <= 180)
        {
            if(FuzzyBounds(angleXZ, 80) || FuzzyBounds(angleXZ, 370))
            {
                orientation = 5;
            }
            else if(FuzzyBounds(angleXZ, 170) || FuzzyBounds(angleXZ, 100))
            {
                orientation = 3;
            } 
            else if(FuzzyBounds(angleXZ, 260) || FuzzyBounds(angleXZ, 190))
            {
                orientation = 7;
            } 
            else if(FuzzyBounds(angleXZ, 350) || FuzzyBounds(angleXZ, 280))
            {
                orientation = 8;
            } 
        }
        else
        {
            if(FuzzyBounds(angleXZ, 80) || FuzzyBounds(angleXZ, 370))
            {
                orientation = 2;
            }
            else if(FuzzyBounds(angleXZ, 170) || FuzzyBounds(angleXZ, 100))
            {
                orientation = 1;
            } 
            else if(FuzzyBounds(angleXZ, 260) || FuzzyBounds(angleXZ, 190))
            {
                orientation = 4;
            } 
            else if(FuzzyBounds(angleXZ, 350) || FuzzyBounds(angleXZ, 280))
            {
                orientation = 6;
            } 
        }

        return orientation;
    }
}
