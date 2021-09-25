using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionBar : MonoBehaviour
{
    [Serializable]public struct CharButton
    {
        public GameController.Character character;
        public Sprite buttonSprite;

    }

    [SerializeField] private List<CharButton> charButtons;
    private CharButton GetButton(GameController.Character character)
    {
        foreach (var c in charButtons) if(c.character == character) return c;
    }

    private List<CharButton> currentButtons = new List<CharButton>();

    private void Start()
    {
        foreach (var c in GameController.CurrentCharacters.Keys)
        {
            currentButtons.Add(GetButton(c));
        }
    }
}
