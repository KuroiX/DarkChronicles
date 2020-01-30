using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadTrigger : MonoBehaviour
{
    [SerializeField] private int levelIndex;
    [SerializeField] private int spawnId;
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Player"))
        {
            LevelLoader.Manager.spawnId = spawnId;
            LevelLoader.Manager.LoadScene(levelIndex);
        }
    }
}