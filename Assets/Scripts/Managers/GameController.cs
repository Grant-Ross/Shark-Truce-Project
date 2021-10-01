using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    [SerializeField] private RectTransform endImage;
   

    private static bool _levelFinished = false;


    public static void UpdateFinishedChars(Character c, bool finished)
    {
        if (_levelFinished) return;
        if (finished && !FinishedCharacters.Contains(c)) FinishedCharacters.Add(c);
        else if (!finished && FinishedCharacters.Contains(c)) FinishedCharacters.Remove(c);
    }
    
    private void Awake()
    {
        _currentCharacter = Character.None;
        _levelFinished = false;
        endImage.DOAnchorPosY(0, 2).SetEase(Ease.OutBounce);

    }

    private void Update()
    {
        if (_levelFinished) return;
        if (Input.GetKeyDown(KeyCode.R)) ResetLevel();
        var finished = true;
        foreach (var c in CurrentCharacters.Keys)
        {
            if (!FinishedCharacters.Contains(c)) finished = false;
        }

        if (finished)
        {
            FinishedCharacters = new HashSet<Character>();
            FinishLevel();
        }
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
        _levelFinished = false;
    }

    public void ResetLevel()
    {
        ResetCharacters();
        GameManager.Instance.LoadLevel(SceneManager.GetActiveScene().name);
    }

    private void FinishLevel()
    {
        if (_levelFinished) return;
        _levelFinished = true;
        StageFinishedListener?.Invoke();
        ResetCharacters();
        StartCoroutine(FinishSequence());
    }

    private IEnumerator FinishSequence()
    {
        
        yield return new WaitForSeconds(.6f);
        FindObjectOfType<FinishPlate>().StartFinishSequence();
        yield return new WaitForSeconds(2.5f);
        //endImage.DOAnchorPosY(0, 2).SetEase(Ease.OutBounce);
        yield return new WaitForSeconds(3);
        GameManager.Instance.LevelFinished();
    }
    
}
