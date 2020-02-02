﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAndSet : MonoBehaviour
{
    [SerializeField] private bool animator;

    void Start()
    {
        if (!animator) 
        {
            GetComponent<Animator>().enabled = animator;
        }
    }
}
