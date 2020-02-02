using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostProjectile : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = new Vector3(40f,20f);
            Destroy(gameObject);
        }
    }
}
