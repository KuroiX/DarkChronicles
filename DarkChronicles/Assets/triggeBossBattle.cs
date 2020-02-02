using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggeBossBattle : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            AudioManager.Manager.PlayMusic(8);
        }
    }
}
