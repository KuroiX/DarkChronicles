using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    public AudioSource song;
    public PlayableDirector playableDirector;
    public List<String> nextText;
    public Text text;
    public Text anyButton;
    
    private int index = 0;
    
    private void Awake()
    {
        song.Play();
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
            anyButton.text = "";
            text.text = "";
            StartCoroutine(AudioFadeOut.FadeOut(song, 3f));
  
        }
    }


}
