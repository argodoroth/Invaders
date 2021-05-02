using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public enum MenuState { BASESTATE, SECONDSTATE, THIRDSTATE}
public class Menu : MonoBehaviour
{
    [SerializeField] GameObject[] baseStateElements;
    [SerializeField] GameObject[] secondStateElements;
    [SerializeField] GameObject[] thirdStateElements;
    [SerializeField] int num;
    MenuState curStage = MenuState.BASESTATE;
    MenuState tarStage = MenuState.SECONDSTATE;

    private void Start()
    {
        StartCoroutine(FullFadeIn(baseStateElements, 1.5f));
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("Battle Scene");
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

    public void SwitchStage()
    {
        GameObject[] startlist;
        GameObject[] endlist;
        switch (curStage)
        {
            case MenuState.BASESTATE:
                break;
            case MenuState.SECONDSTATE:
                break;
            case MenuState.THIRDSTATE:
                break;
        }
        switch (tarStage)
        {
            case MenuState.BASESTATE:
                break;
            case MenuState.SECONDSTATE:
                break;
            case MenuState.THIRDSTATE:
                break;
        }
        StartCoroutine(FullFadeOut(baseStateElements, 1.5f));
        StartCoroutine(FullFadeIn(secondStateElements, 1.5f));
    }

    private IEnumerator FullFadeOut(GameObject[] objs, float tranisitionTime)
    {
        for (float i = 0; i < tranisitionTime; i+= Time.deltaTime)
        {
            ChangeAlpha(objs, 1 - i/tranisitionTime);
            Debug.Log(i);
            yield return null;
        }
    }

    private IEnumerator FullFadeIn(GameObject[] objs, float tranisitionTime)
    {
        for (float i = 0; i < tranisitionTime; i += Time.deltaTime)
        {
            ChangeAlpha(objs, i/tranisitionTime);
            Debug.Log(i);
            yield return null;
        }
    }
}