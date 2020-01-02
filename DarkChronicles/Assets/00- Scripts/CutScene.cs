using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScene : MonoBehaviour
{
    [SerializeField]
    public CutSceneEvent[] events;

    private int i = 0;
    
    public void Next()
    {
        if (i >= events.Length)
        {
            // call end function
            return;
        }
        var nextEvent = events[i++];
        nextEvent.Play(this);
    }

    // is going to be in trigger enter later
    void Start()
    {
        Next();
    }
}
