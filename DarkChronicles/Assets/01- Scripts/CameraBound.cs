using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBound : MonoBehaviour
{
    [SerializeField]
    private Vector2 xAxis;
    [SerializeField]
    private Vector2 yAxis;
    
    void Update()
    {
        SetCameraPosition();
    }

    void SetCameraPosition()
    {
        Vector3 current = transform.position;

        if (current.x < xAxis.x)
        {
            if (current.y < yAxis.x)
            {
                transform.position = new Vector3(xAxis.x, yAxis.x, current.z);
            } 
            else if (current.y > yAxis.y)
            {
                transform.position = new Vector3(xAxis.x, yAxis.y, current.z);
            }
            else
            {
                transform.position = new Vector3(xAxis.x, current.y, current.z);
            }
        }
        else if (current.x > xAxis.y)
        {
            if (current.y < yAxis.x)
            {
                transform.position = new Vector3(xAxis.y, yAxis.x, current.z);
            } 
            else if (current.y > yAxis.y)
            {
                transform.position = new Vector3(xAxis.y, yAxis.y, current.z);
            }
            else
            {
                transform.position = new Vector3(xAxis.y, current.y, current.z);
            }
        }
        else
        {
            if (current.y < yAxis.x)
            {
                transform.position = new Vector3(current.x, yAxis.x, current.z);
            } 
            else if (current.y > yAxis.y)
            {
                transform.position = new Vector3(current.x, yAxis.y, current.z);
            }
        }
    }
}
