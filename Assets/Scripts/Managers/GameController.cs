using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private static GameController _instance;
    public static GameController Instance => _instance ? _instance : _instance = FindObjectOfType<GameController>();
    
    public enum Character
    {
        None=0, Patty=1, Cheese=2, Tomato=3, Lettuce=4
    }

    public static Dictionary<Character, CharacterController> CurrentCharacters { get; private set; }

    private Character _currentCharacter;

    public static HashSet<Character> FinishedCharacters = new HashSet<Character>();

    public static event Action<Character> CharacterSwitchListener;
    public static event Action StageFinishedListener;


    private void Start()
    {
        CurrentCharacters = new Dictionary<Character, CharacterController>();
        _currentCharacter = Character.None;
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) ResetLevel();
        if (CurrentCharacters.Count > 0 && FinishedCharacters.Count == CurrentCharacters.Count) FinishLevel();
    }


    public void SwitchCharacter(Character character)
    {
        if (character == _currentCharacter || !CurrentCharacters.ContainsKey(character)) return;
        _currentCharacter = character;
        CharacterSwitchListener?.Invoke(character);
    }
    
    public void AddCharacter(Character character, CharacterController charController)
    {
        if(CurrentCharacters == null) CurrentCharacters = new Dictionary<Character, CharacterController>();
        CurrentCharacters.Add(character, charController);
        if(character == Character.Patty) SwitchCharacter(Character.Patty);
    }


    private void ResetCharacters()
    {
        CurrentCharacters = new Dictionary<Character, CharacterController>();
        FinishedCharacters = new HashSet<Character>();
    }

    private void ResetLevel()
    {
        ResetCharacters();
        GameManager.Instance.LoadLevel(SceneManager.GetActiveScene().name);
    }

    private void FinishLevel()
    {
        StageFinishedListener?.Invoke();
        ResetCharacters();
        GameManager.Instance.LoadScene("LevelSelect");
    }
    
}
