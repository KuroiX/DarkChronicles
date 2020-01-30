using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader Manager;

    public int spawnId;

    void Start()
    {
        if (Manager == null)
        {
            Manager = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (spawnId != 0)
        {
            Vector3 spawnPos = GameObject.Find("Spawn" + spawnId).transform.position;

            GameObject.Find("Character").transform.position = spawnPos;
            GameObject.Find("Main Camera").transform.position = spawnPos+ new Vector3(0, 0.5f, 0);
            
            spawnId = 0;
        }
    }
}