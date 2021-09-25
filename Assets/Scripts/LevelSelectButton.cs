using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelSelectButton : MonoBehaviour
{
    public SceneAsset level;
    
    public void LoadLevel()
    {
        GameManager.Instance.LoadLevel(level.name);
    }
}
