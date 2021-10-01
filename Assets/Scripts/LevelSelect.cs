using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    private void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = i <=  GameManager.LevelsCompleted;
        }
    }
}
