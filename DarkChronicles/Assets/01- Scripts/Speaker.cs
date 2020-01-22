using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu]
public class Speaker : ScriptableObject
{
    public string name;
    public Sprite sprite;
    public Color hair;
    public Color cloths;

    public Font font;
}
