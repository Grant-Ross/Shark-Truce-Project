using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterButton : MonoBehaviour
{
    [SerializeField] private Image charImage;
    [SerializeField] private Toggle toggle;
    private GameController.Character _character;
    
    public void SetUp(ToggleGroup group, SelectionBar.CharButton buttonInfo)
    {
        toggle.group = group;
        charImage.sprite = buttonInfo.buttonSprite;
        _character = buttonInfo.character;
        
    }
    public void OnValueChanged(bool value)
    {
        if (value)
        {
            SwapCharacter();
            (transform as RectTransform).localScale = new Vector2(1.2f,1.2f);
        }
        else
        {
            (transform as RectTransform).localScale = new Vector2(1,1);
        }
        
    }
    
    private void SwapCharacter()
    {
        GameController.Instance.SwitchCharacter(_character);
    }
}
