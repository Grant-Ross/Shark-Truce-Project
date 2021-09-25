using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionBar : MonoBehaviour
{
    [SerializeField] private GameObject buttonPrefab;
    [Serializable]public struct CharButton
    {
        public GameController.Character character;
        public Sprite buttonSprite;

    }

    [SerializeField] private List<CharButton> charButtons;
    private CharButton GetButton(GameController.Character character)
    {
        foreach (var c in charButtons) if(c.character == character) return c;
        throw new MissingReferenceException();
    }

    private List<GameObject> currentButtons = new List<GameObject>();

    private void Start()
    {
        foreach (var c in GameController.CurrentCharacters.Keys)
        {
            var button = Instantiate(buttonPrefab, (transform as RectTransform));
            button.GetComponent<CharacterButton>().SetUp(GetButton(c));
            currentButtons.Add(button);
        }

        for(int i = 0; i < currentButtons.Count; i++)
        {
            (currentButtons[i].transform as RectTransform).anchoredPosition = GetButtonPosition(i);
        }
    }

    private Vector2 GetButtonPosition(int num)
    {
        var segWidth = (transform as RectTransform).rect.width / currentButtons.Count;
        var xPos = segWidth * (num + 1) - (transform as RectTransform).rect.width/2 - segWidth/2;
        return new Vector2(xPos, (transform as RectTransform).anchoredPosition.y);
    }
}
