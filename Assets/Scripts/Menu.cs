using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public enum MenuState { MAINMENU, SECONDSTATE, GAMEOVER, CONTINUELEVEL, CREDITS}
public class Menu : MonoBehaviour
{
    //List of objects to appear for each state
    [SerializeField] GameObject[] mainElements;
    [SerializeField] GameObject[] secondStateElements;
    [SerializeField] GameObject[] gameOverElements;
    [SerializeField] GameObject[] continueElements;
    [SerializeField] GameObject[] creditsElements;

    //Accessible fields to display save data for profiles
    [SerializeField] TMP_Text[] profile1Display;
    [SerializeField] TMP_Text[] profile2Display;
    [SerializeField] TMP_Text[] profile3Display;


    private SceneLoader sceneLoader;
    private MenuState curStage = MenuState.MAINMENU;
    private GameSession session;

    private void Start()
    {
        session = FindObjectOfType<GameSession>();
        curStage = session.GetMenu();
        sceneLoader = FindObjectOfType<SceneLoader>();
        if (curStage == MenuState.GAMEOVER || curStage == MenuState.CONTINUELEVEL)
        {
            PopulateScore();
        }
        StartCoroutine(FullFadeIn(findList(curStage), 1.5f));
        DeactivateOtherStates();
    }
    
    public void TutorialButton()
    {
        StartCoroutine(FullFadeOut(mainElements, 1.5f));
        sceneLoader.LoadTutorial();
    }

    public void PlayButton()
    {
        session.SetGame(true);
        StartCoroutine(FullFadeOut(findList(curStage), 1.5f));
        sceneLoader.LoadGame();
    }

    public void ProfileButton(int i)
    {
        session.SetPlayer(i);
        session.LoadValues();
        session.SetGame(true);
        StartCoroutine(FullFadeOut(findList(curStage), 1.5f));
        sceneLoader.LoadGame();
    }

    public void SaveButton()
    {
        session.SetGame(true);
        session.SaveValues();
        StartCoroutine(FullFadeOut(findList(curStage), 1.5f));
        sceneLoader.LoadGame();
    }

    public void GameOver()
    {
        session.ResetSave();
        MainMenu();
    }

    public void SaveQuit()
    {
        session.SaveValues();
        MainMenu();
    }
    public void MainMenu()
    {
        SwitchStage(MenuState.MAINMENU);
    }

    public void ProfileSelect()
    {
        SwitchStage(MenuState.SECONDSTATE);
        PopulateProfile(1);
        PopulateProfile(2);
        PopulateProfile(3);
    }

    private void PopulateProfile(int i)
    {
        SaveData save = SaveGameSystem.LoadGame("Profile" + i);
        TMP_Text[] temp = null;
        if (i == 1)
        {
            temp = profile1Display;
        } else if (i == 2)
        {
            temp = profile2Display;
        } else
        {
            temp = profile3Display;
        }
            temp[0].text = "" + save.highscore;
            temp[1].text = "" + save.currentLevel;
            temp[2].text = "" + save.currentLives;
            temp[3].text = "" + save.currentScore;
    }

    public void ChangeAlpha(GameObject[] obj, float change)
    {
        for (int i = 0; i < obj.Length; i++)
        {

            if (obj[i].GetComponent<Image>())
            {
                Color col = obj[i].GetComponent<Image>().color;
                col = new Color(col.r, col.g, col.b, change);
                obj[i].GetComponent<Image>().color = col;

            } else if (obj[i].GetComponent<TMP_Text>())
            {
                float changeInt = Mathf.Floor(change * 255.0f);
                Color32 col = obj[i].GetComponent<TMP_Text>().color;
                col = new Color32(col.r, col.g, col.b, (byte)changeInt);
                obj[i].GetComponent<TMP_Text>().color = col;
            }
        }
    }

    public float CheckOpacity(GameObject[] obj)
    {
        float x = 0;
        if (!obj[0])
        {
            return x;
        }
        if (obj[0].GetComponent<Image>())
        {
            x = obj[0].GetComponent<Image>().color.a;

        }
        else if (obj[0].GetComponent<TMP_Text>())
        {
            x = obj[0].GetComponent<TMP_Text>().color.a / 255;
        }
        return x;
    }

    public void SwitchStage(MenuState tarStage)
    {
        GameObject[] startlist = findList(curStage);
        GameObject[] endlist = findList(tarStage);
        StartCoroutine(FullFadeOut(startlist, 1.5f));
        StartCoroutine(FullFadeIn(endlist, 1.5f));
        curStage = tarStage;
    }

    private GameObject[] findList(MenuState state)
    {
        GameObject[] list;
        switch (state)
        {
            case MenuState.MAINMENU:
                list = mainElements;
                break;
            case MenuState.SECONDSTATE:
                list = secondStateElements;
                break;
            case MenuState.GAMEOVER:
                list = gameOverElements;
                break;
            case MenuState.CONTINUELEVEL:
                list = continueElements;
                break;
            case MenuState.CREDITS:
                list = creditsElements;
                break;
            default:
                list = mainElements;
                break;
        }
        return list;
    }

    private void DeactivateOtherStates()
    {
        foreach (MenuState state in Enum.GetValues(typeof(MenuState)))
        {
            if (state != curStage)
            {
                GameObject[] list = findList(state);
                for (int i = 0; i < list.Length; i++)
                {
                    list[i].SetActive(false);
                }
            }
        }
    }
    private IEnumerator FullFadeOut(GameObject[] objs, float tranisitionTime)
    {
        for (float i = 0; i < tranisitionTime; i+= Time.deltaTime)
        {
            ChangeAlpha(objs, 1 - i/tranisitionTime);
            yield return null;
        }
        if (CheckOpacity(objs) <= 0.005f)
        {
            
            for (int i = 0; i < objs.Length; i++)
            {
                objs[i].SetActive(false);
            }
        }
    }

    private IEnumerator FullFadeIn(GameObject[] objs, float tranisitionTime)
    {
        for (int i = 0; i < objs.Length; i++)
        {
            objs[i].SetActive(true);
        }
        for (float i = 0; i < tranisitionTime; i += Time.deltaTime)
        {
            ChangeAlpha(objs, i/tranisitionTime);
            yield return null;
        }
    }

    public void PopulateScore()
    {
        TMP_Text[] texts = FindObjectsOfType<TMP_Text>();
        for (int i = 0; i < texts.Length; i++)
        {
            if (texts[i].name == "Score Field")
            {
                texts[i].text = "" + session.GetScore();
            }
        }
    }
}