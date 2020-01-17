using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    public PlayableDirector playableDirector;

    public List<String> nextText;

    public Text text;
    
    private int index = 0;
    
    private void Awake()
    {
        PlayNext();
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            PlayNext();
        }
    }

    public void PlayNext()
    {
        if (index < nextText.Count)
        {
            text.text = nextText[index];
            playableDirector.Play();
            index++;
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
    }
}
