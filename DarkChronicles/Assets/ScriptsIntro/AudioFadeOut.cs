using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
 
public class AudioFadeOut: MonoBehaviour {
 
    public static IEnumerator FadeOut (AudioSource audioSource, float FadeTime)
    {

        float currentVolume = audioSource.volume;
        while (audioSource.volume > 0) {
            audioSource.volume -= currentVolume * Time.deltaTime / FadeTime;
 
            yield return null;
        }
 
        audioSource.Stop ();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);

    }
 
}