﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public enum MenuState { MAINMENU, SECONDSTATE, GAMEOVER, CONTINUELEVEL, CREDITS}
public class Menu : MonoBehaviour
{
    [SerializeField] GameObject[] mainElements;
    [SerializeField] GameObject[] secondStateElements;
    [SerializeField] GameObject[] gameOverElements;
    [SerializeField] GameObject[] continueElements;
    [SerializeField] GameObject[] creditsElements;
    [SerializeField] int num;

    SceneLoader sceneLoader;
    MenuState curStage = MenuState.MAINMENU;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        StartCoroutine(FullFadeIn(findList(curStage), 1.5f));
    }
    
    public void TutorialButton()
    {
        StartCoroutine(FullFadeOut(mainElements, 1.5f));
        sceneLoader.LoadTutorial();
    }

    public void PlayButton()
    {
        SwitchStage(MenuState.SECONDSTATE);
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
}