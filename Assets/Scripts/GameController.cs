using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController _instance;
    public static GameController Instance => _instance ? _instance : _instance = FindObjectOfType<GameController>();
    
    public enum Character
    {
        None, Patty, Cheese
    }

    private Dictionary<Character, CharacterController> _characterDict = new Dictionary<Character, CharacterController>();

    private Character _currentCharacter;

    private void Start()
    {
        _currentCharacter = Character.None;
        SwitchCharacter(Character.Patty);
    }


    public void SwitchCharacter(Character character)
    {
        if (character == _currentCharacter || !_characterDict.ContainsKey(character)) return;
        if(_currentCharacter != Character.None) _characterDict[_currentCharacter].enabled = false;
        _characterDict[character].enabled = true;
        _currentCharacter = character;
    }
    
    public void AddCharacter(Character character, CharacterController charController)
    {
        _characterDict.Add(character, charController);
    }


    private void ResetCharacters()
    {
        _characterDict = new Dictionary<Character, CharacterController>();
    }
}
