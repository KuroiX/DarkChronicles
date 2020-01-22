using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerC : MonoBehaviour
{
    public float maxX;
    public float minX;
    public float maxY;
    public float minY;

    private void Update()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;
        
        if (transform.position.x > maxX)
        {
            x = maxX;
        }
        else if (transform.position.x < minX)
        {
            x = minX;
        }
        
        
        if (transform.position.y > maxY)
        {
            y = maxY;
        }
        else if (transform.position.y < minY)
        { 
            y = minY;
        }
        
        
        transform.position = new Vector3(x, y, z);
    }
}
                