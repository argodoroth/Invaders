using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadGame()
    {
        StartCoroutine(WaitThenLoad("Battle Scene", 2f));
    }

    public void LoadTutorial()
    {
        StartCoroutine(WaitThenLoad("Tutorial Scene", 2f));
    }

    public void LoadMenu(float wait)
    {
        StartCoroutine(WaitThenLoad("Main Menu", wait));
    }



    private IEnumerator WaitThenLoad(string scene, float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(scene);
    }
}
