using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterButton : MonoBehaviour
{
    [SerializeField] private Image charImage;

    private GameController.Character _character;
    
    public void SetUp(SelectionBar.CharButton buttonInfo)
    {
        charImage.sprite = buttonInfo.buttonSprite;
        _character = buttonInfo.character;
        
    }

    public void OnSelect()
    {
        SwapCharacter();
        (transform as RectTransform).localScale = new Vector2(1.2f,1.2f);
    }

    public void OnDeselect()
    {
        (transform as RectTransform).localScale = new Vector2(1,1);
    }
    
    private void SwapCharacter()
    {
        GameController.Instance.SwitchCharacter(_character);
    }
}
