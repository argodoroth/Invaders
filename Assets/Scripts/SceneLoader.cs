using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene("Battle Scene");
    }

    public void LoadTutorial()
    {
        StartCoroutine(WaitThenLoad("Tutorial Scene", 2f));
    }

    public void LoadMenu(MenuState state)
    {
        StartCoroutine(WaitThenLoad("Main Menu", 6f));
    }

    private IEnumerator WaitThenLoad(string scene, float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(scene);
    }
}
