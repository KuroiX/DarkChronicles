using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu]
public class Dialogue : ScriptableObject
{
    public Speaker speaker;
    
    [TextArea(3, 10)]
    public string[] sentences;
    
    public AudioClip[] clips;

}
