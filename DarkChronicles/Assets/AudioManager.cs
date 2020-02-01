using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{

    [SerializeField] private AudioClip[] musicClips;
    [SerializeField] private AudioClip[] soundClips;
    
    
    private AudioSource _musicSource;
    [HideInInspector]
    public AudioSource soundSource;
    public static AudioManager Manager;
    
    void Start()
    {
        if (Manager == null)
        {
            Manager = this;
            DontDestroyOnLoad(gameObject);
            _musicSource = GetComponent<AudioSource>();
            soundSource = GetComponents<AudioSource>()[1];
            SceneManager.sceneLoaded += FadeIn;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void PlaySound(int index)
    {
        soundSource.clip = soundClips[index];
        soundSource.Play();
    }


    public void PlayMusic(int index)
    {
        float duration = 0.2f;
        if (index == 0) duration *= 5;
        StartCoroutine(FadeToNext(index, duration, 0));
    }
    
    IEnumerator FadeToNext(int index, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = _musicSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            _musicSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        
        _musicSource.clip = musicClips[index];
        _musicSource.volume = start;
        _musicSource.Play();

        //currentTime = 0;

        /*while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            _musicSource.volume = Mathf.Lerp(targetVolume, start, currentTime / duration);
            yield return null;
        }
        */
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutEnumerator());
    }

    IEnumerator FadeOutEnumerator()
    {
        float currentTime = 0;
        float start = _musicSource.volume;

        while (currentTime < 0.5f)
        {
            currentTime += Time.deltaTime;
            _musicSource.volume = Mathf.Lerp(start, 0, currentTime / 0.5f);
            yield return null;
        }
    }

    void FadeIn(Scene scene, LoadSceneMode mode)
    {
        switch (scene.buildIndex)
        {
            case 1:
                StartMusic(0);
                break;
            case 2:
                StartMusic(3);
                break;
            default:
                StartMusic(0);
                break;
            //TODO
        }
    }

    void StartMusic(int index)
    {
        _musicSource.clip = musicClips[index];
        _musicSource.Play();
    } 
}
