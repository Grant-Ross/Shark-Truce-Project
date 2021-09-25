using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance ? _instance : _instance = FindObjectOfType<GameManager>();
    
    [SerializeField] private GameObject gameController;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        LoadScene("LevelSelect");
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.sceneLoaded += OnLevelLoaded;
        LoadScene(levelName);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        
        SceneManager.sceneLoaded -= OnLevelLoaded;
        Instantiate(gameController);
    }
}
