using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    public GameObject[] Levels;
    public GameObject LevelEndGrid;
    public GameObject LevelEndUI;
    public int CurrentLevel = 0;
    public bool useDebugScene;
    public int lastQuoteIndex;

    private GameObject loadedLevel;
    private GameObject loadedLevelEndGrid;
    private GameObject loadedLevelEndUI;
    private LevelScore lastScore;

    public enum PlayState
    {
        StartScreen,
        PlayingLevel,
        Pause,
        LevelEnd,
        LevelSelect
    }

    public LevelScore GetCurrentLevelScore()
    {
        if (loadedLevel != null)
            return loadedLevel.GetComponent<LevelScore>();
        else
            return lastScore;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        
    }

    // Use this for initialization
    void Start()
    {
        if (!useDebugScene)
        {
            if (loadedLevel != null)
                Destroy(loadedLevel);
            if (loadedLevelEndGrid != null)
                Destroy(loadedLevelEndGrid);
            if (loadedLevelEndUI != null)
                Destroy(loadedLevelEndUI);
            CurrentLevel = 0;
            loadedLevel = Instantiate(Levels[CurrentLevel]);
        }
    }

    internal void GoToNextLevel()
    {
        Destroy(loadedLevelEndGrid);
        Destroy(loadedLevelEndUI);
        CurrentLevel++;
        if (CurrentLevel > Levels.Length - 1) CurrentLevel = 0;
        loadedLevel = Instantiate(Levels[CurrentLevel]);
    }

    internal void PlayerKilled()
    {
        ResetCurrentLevel();
    }

    private void ResetCurrentLevel()
    {
        Debug.Log("Reset");
        Destroy(loadedLevel);
        loadedLevel = Instantiate(Levels[CurrentLevel]);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            Start();
    }

    internal void GoToEndLevel()
    {
        lastScore = loadedLevel.GetComponent<LevelScore>();
        Debug.Log(lastScore.Shots);
        Destroy(loadedLevel);
        loadedLevelEndUI = Instantiate(LevelEndUI);
        loadedLevelEndGrid = Instantiate(LevelEndGrid);
        loadedLevelEndUI.GetComponent<LevelEndUI>().Reset();
    }
}
