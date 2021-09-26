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

    public void OnSelect()
    {
        (transform as RectTransform).localScale = new Vector3(1.2f,1.2f,1);
    }

    public void OnDeselect()
    {
        (transform as RectTransform).localScale = new Vector3(1,1,1);
    }
    
}
