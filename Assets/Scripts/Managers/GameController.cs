using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private static GameController _instance;
    public static GameController Instance => _instance ? _instance : _instance = FindObjectOfType<GameController>();
    
    public enum Character
    {
        None, Patty, Cheese, Tomato, Lettuce
    }

    private Dictionary<Character, CharacterController> _characterDict = new Dictionary<Character, CharacterController>();

    private Character _currentCharacter;

    public static event Action<Character> characterSwitchListener;

    private void Start()
    {
        _currentCharacter = Character.None;
        SwitchCharacter(Character.Patty);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) ResetLevel();
    }


    public void SwitchCharacter(Character character)
    {
        if (character == _currentCharacter || !_characterDict.ContainsKey(character)) return;
        _currentCharacter = character;
        characterSwitchListener?.Invoke(character);
    }
    
    public void AddCharacter(Character character, CharacterController charController)
    {
        _characterDict.Add(character, charController);
    }


    private void ResetCharacters()
    {
        _characterDict = new Dictionary<Character, CharacterController>();
    }

    private void ResetLevel()
    {
        GameManager.Instance.LoadLevel(SceneManager.GetActiveScene().name);
    }
}
