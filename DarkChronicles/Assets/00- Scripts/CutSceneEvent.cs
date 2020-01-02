using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CutSceneEvent
{
    enum EventType {text, animation}

    [SerializeField]
    private EventType type;

    public void Play(CutScene scene)
    {
        if (type == EventType.animation)
        {
            // play animation
            // the animation calls scene.Next()
        } else if (type == EventType.text)
        {
            // start IEnumerator that goes through text
            // the last text calls scene.Next()
        }
        Debug.Log(type);
        scene.Next();
    }
}
