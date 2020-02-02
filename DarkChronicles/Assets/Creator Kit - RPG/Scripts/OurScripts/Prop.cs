using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Audio;

public class Prop : MonoBehaviour
{
    [SerializeField]
    private Sprite dead;
    [SerializeField] 
    private GameObject nextCutscene;

    private bool _isDead;

    private SpriteRenderer renderer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Attack"))
        {
            if (!_isDead)
            {
                _isDead = true;
                GetComponent<SpriteRenderer>().sprite = dead;
                GetComponent<Collider2D>().enabled = false;
                TriggerCutscene();
            }
        }
    }

    void TriggerCutscene()
    {
        if (!nextCutscene.Equals(null))
        {
            nextCutscene.SetActive(true);
        }
    }
}