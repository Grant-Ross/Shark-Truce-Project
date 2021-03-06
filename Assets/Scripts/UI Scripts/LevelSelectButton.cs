using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectButton : MonoBehaviour
{
    public string level;
    
    public void LoadLevel()
    {
        GameManager.Instance.LoadLevel(level);
    }

    public void OnClick()
    {
        AudioManager.PlaySound("Menu Select");
    }

    public void OnSelect()
    {
        AudioManager.PlaySound("Menu Hover");
        (transform as RectTransform).localScale = new Vector3(1.2f,1.2f,1);
    }

    public void OnDeselect()
    {
        (transform as RectTransform).localScale = new Vector3(1,1,1);
    }
    
}
