using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance ? _instance : _instance = FindObjectOfType<GameManager>();
    
    [SerializeField] private GameObject gameController;
    [SerializeField] private GameObject transitionObject;

    private static HashSet<string> finishedLevels = new HashSet<string>();

    private bool _sceneReady = false;

    public static int LevelsCompleted = 0;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        
    }

    private bool firstLoad = true;
    private void Start()
    {
        LoadScene("TitleScreen");
    }

    public void LoadLevel(string levelName)
    {
        _sceneReady = false;
        var tween = MoveTransition();
        SceneManager.sceneLoaded += OnLevelLoaded;
        firstLoad = false;
        StartCoroutine(WaitForLoad(tween, levelName, AudioManager.Instance.GetMusic("Level")));
    }

    public void LoadScene(string sceneName)
    {
        _sceneReady = false;
        var tween = MoveTransition();
        SceneManager.sceneLoaded += OnSceneLoaded;
        if (firstLoad) StartCoroutine(WaitForLoad(tween, sceneName, AudioManager.Instance.GetMusic("Title")));
        else if(AudioManager.CurrentMusic != "Title") StartCoroutine(WaitForLoad(tween, sceneName, AudioManager.Instance.GetMusic("Title No Intro")));
    }

    private Tween MoveTransition()
    {
        (transitionObject.transform as RectTransform).anchoredPosition =
            new Vector2(-(transitionObject.transform as RectTransform).sizeDelta.x,0);
        return (transitionObject.transform as RectTransform).DOAnchorPosX(0, .4f).SetEase(Ease.OutExpo);
       
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        _sceneReady = true;
    }

    
    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        
        SceneManager.sceneLoaded -= OnLevelLoaded;
        _sceneReady = true;
        Instantiate(gameController);
    }

    private IEnumerator WaitForLoad(Tween tween, string sceneName, Music music)
    {
        
        while (tween.IsPlaying()) yield return null;
        SceneManager.LoadScene(sceneName);
        while (!_sceneReady) yield return null;
        (transitionObject.transform as RectTransform).DOAnchorPosX((transitionObject.transform as RectTransform).sizeDelta.x, .4f).SetEase(Ease.OutExpo);
        AudioManager.Instance.PlayMusic(music.soundName);
    }
    
    public void LevelFinished()
    {
        if (!finishedLevels.Contains(SceneManager.GetActiveScene().name))
        {
            LevelsCompleted += 1;
            finishedLevels.Add(SceneManager.GetActiveScene().name);
        }
        LoadScene("LevelSelect");
    }
}
