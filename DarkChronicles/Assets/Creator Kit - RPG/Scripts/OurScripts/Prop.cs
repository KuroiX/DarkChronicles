using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Prop : MonoBehaviour
{
    // Start is called before the first frame update

    public Sprite dead;

    private SpriteRenderer renderer;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Attack"))
        {
            if (GetComponent<SpriteRenderer>().sprite != dead)
                GetComponent<SpriteRenderer>().sprite = dead;
        }
    }
}