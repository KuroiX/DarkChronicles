using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader manager;

    void Start()
    {
        manager = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}