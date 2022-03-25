using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public void updatePositionForward()
    {
        transform.Translate(0.0f, 0.0f, 0.01f);
    }

    public void updatePositionBackward()
    {
        transform.Translate(0.0f, 0.0f, -0.01f);
    }

    public void updatePositionLeft()
    {
        transform.Translate(-0.01f, 0.0f, 0.0f);
    }

    public void updatePositionRight()
    {
        transform.Translate(0.01f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
