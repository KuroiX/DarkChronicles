using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class progress : MonoBehaviour
{
    public void EnableMissile()
    {
        ProgressManager.Manager.EnableMissile();
    }
    
    public void EnableMissileAndShrink()
    {
        ProgressManager.Manager.EnableMissileAndShrink();
    }
    
    public void DisableAbilites()
    {
        ProgressManager.Manager.DisableAbilites();
    }
    
    public void EndCutScene(int index)
    {
        ProgressManager.Manager.EndCutScene(index);
    }
}
