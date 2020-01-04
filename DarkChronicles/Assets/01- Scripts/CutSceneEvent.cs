using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

[System.Serializable]
public class CutSceneEvent
{
    public enum EventType
    {
        Dialogue,
        Playable
    }

    public EventType type;
    
    public Dialogue dialogue;
    public TimelineAsset playable;

    public CutSceneEvent(Dialogue dialogue)
    {
        type = EventType.Dialogue;
        this.dialogue = dialogue;
    }
    
    public CutSceneEvent(TimelineAsset playable)
    {
        type = EventType.Playable;
        this.playable = playable;
    }

}
