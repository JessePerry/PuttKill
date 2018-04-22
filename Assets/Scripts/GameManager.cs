using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    public GameObject[] Levels;
    public GameObject LevelEndGrid;
    public GameObject LevelEndUI;
    public GameObject LevelPlayingUI;
    public GameObject EndGameUI;
    public int CurrentLevel = 0;
    public bool useDebugScene;
    public int lastQuoteIndex;

    private GameObject loadedLevel;
    private GameObject loadedLevelEndGrid;
    private GameObject loadedLevelEndUI;
    private GameObject loadedLevelPlayingUI;
    private GameObject loadedEndGameUI;
    private LevelScore lastScore;
    private List<LevelScore> allScores;

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

    public List<LevelScore> GetAllScores()
    {
        return allScores;
    }

    public void Reset()
    {
        Start();
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
            if (loadedLevelPlayingUI != null)
                Destroy(loadedLevelPlayingUI);
            if (loadedEndGameUI != null)
                Destroy(loadedEndGameUI);

            allScores = new List<LevelScore>();
            CurrentLevel = 0;
            loadedLevel = Instantiate(Levels[CurrentLevel]);
            loadedLevelPlayingUI = Instantiate(LevelPlayingUI);
        }
    }

    internal void GoToNextLevel()
    {
        Destroy(loadedLevelEndGrid);
        Destroy(loadedLevelEndUI);
        CurrentLevel++;
        if (CurrentLevel > Levels.Length - 1)
        {
            loadedEndGameUI = Instantiate(EndGameUI);
            return;
        }
        loadedLevel = Instantiate(Levels[CurrentLevel]);
        loadedLevelPlayingUI = Instantiate(LevelPlayingUI);
    }

    internal void PlayerKilled()
    {
        ResetCurrentLevel();
    }

    private void ResetCurrentLevel()
    {
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
        allScores.Add(lastScore);
        Destroy(loadedLevel);
        Destroy(loadedLevelPlayingUI);
        loadedLevelEndUI = Instantiate(LevelEndUI);
        loadedLevelEndGrid = Instantiate(LevelEndGrid);
        loadedLevelEndUI.GetComponent<LevelEndUI>().Reset();
    }
}
