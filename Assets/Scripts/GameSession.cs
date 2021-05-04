using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Carries data between scenes, and keeps information on the curent game session
public class GameSession : MonoBehaviour
{
    [SerializeField] bool isGame;

    [SerializeField] int currentLevel;
    [SerializeField] int currentLives;
    [SerializeField] int currentScore;
    [SerializeField] MenuState currentMenu;
    [SerializeField] int currentPlayer = 0;

    private SceneLoader sceneLoader;
    private EnemyGroupController enemies;

    //Singleton pattern
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

    //Will load back to menu screen and grab all data from the current scene before transitioning if not tutorial
    public void LoadMenu(MenuState state)
    {
        float wait = 6f;
        if (isGame)
        {
            currentLevel += 1;
            ScoreController score = FindObjectOfType<ScoreController>();
            LivesController lives = FindObjectOfType<LivesController>();
            currentScore = score.GetScore();
            currentLives = lives.GetLives();
            wait = 3f;
        }
        isGame = false;
        sceneLoader = FindObjectOfType<SceneLoader>();
        currentMenu = state;
        sceneLoader.LoadMenu(wait);
    }

    //Save values from current session
    public void SaveValues()
    {
        SaveData save = new SaveData();
        SaveData load = SaveGameSystem.LoadGame("Profile" + currentPlayer);
        save.highscore = Mathf.Max(load.highscore, currentScore);
        save.currentScore = currentScore;
        save.currentLives = currentLives;
        save.currentLevel = currentLevel;
        SaveGameSystem.SaveGame(save, "Profile" + currentPlayer);
    }

    //Check highscore and reset all other vals
    public void ResetSave()
    {
        SaveData save = new SaveData();
        SaveData load = SaveGameSystem.LoadGame("Profile" + currentPlayer);
        save.highscore = Mathf.Max(load.highscore, currentScore);
        save.currentScore = 0;
        save.currentLives = 5;
        save.currentLevel = 0;
        SaveGameSystem.SaveGame(save, "Profile" + currentPlayer);
    }

    //Load values from save into current session
    public void LoadValues()
    {
        SaveData load = SaveGameSystem.LoadGame("Profile" + currentPlayer);
        currentLevel = load.currentLevel;
        currentLives = load.currentLives;
        currentScore = load.currentScore;
    }

    //set which player profile is active
    public void SetPlayer(int i)
    {
        currentPlayer = i;
    }

    //Getters and setters
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

