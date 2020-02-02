using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio : MonoBehaviour
{
    public void PlaySound(int index)
    {
        AudioManager.Manager.PlaySound(index);
    }
    
    public void PlayMusic(int index)
    {
        AudioManager.Manager.PlayMusic(index);
    }
}
