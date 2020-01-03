using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

[System.Serializable]
public class CutSceneEvent
{
    public enum EventType
    {
        Text,
        Animation
    }

    public EventType type;
    
    public Dialogue dialogue;
    public TimelineAsset animation;
    
    [HideInInspector]
    public bool connected;
}
