using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] bool isGame;

    [SerializeField] int currentLevel;
    [SerializeField] int currentLives;
    [SerializeField] int currentScore;
    [SerializeField] MenuState currentMenu;

    private SceneLoader sceneLoader;
    private EnemyGroupController enemies;
    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameSession>().Length;
        if (gameStatusCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            enemies = FindObjectOfType<EnemyGroupController>();
        }
    }

    public void LoadMenu(MenuState state)
    {
        if (isGame)
        {
            currentLevel += 1;
            ScoreController score = FindObjectOfType<ScoreController>();
            LivesController lives = FindObjectOfType<LivesController>();
            currentScore = score.GetScore();
            currentLives = lives.GetLives();
        }
        sceneLoader = FindObjectOfType<SceneLoader>();
        currentMenu = state;
        sceneLoader.LoadMenu();
    }
    public int GetLives()
    {
        return currentLives;
    }

    public int GetLevel()
    {
        return currentLevel;
    }

    public int GetScore()
    {
        return currentScore;
    }

    public MenuState GetMenu()
    {
        return currentMenu;
    }

    public void SetGame(bool game)
    {
        isGame = game;
    }
}

