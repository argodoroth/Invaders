using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//Manages the players lives and lives text
public class LivesController : MonoBehaviour
{
    private int lives = 3;
    private TextMeshProUGUI livesText;
    void Start()
    {
        TextMeshProUGUI[] texts = FindObjectsOfType<TextMeshProUGUI>();
        for (int i = 0; i < texts.Length; i++)
        {
            if (texts[i].name == "Lives Text")
            {
                livesText = texts[i];
            }
        }
        livesText.text = "x" + lives.ToString();
    }

    public void IncreaseLives(int increase)
    {
        lives += increase;
        livesText.text = "x" + lives.ToString();
    }

    public void DecreaseLives(int increase)
    {
        lives -= increase;
        livesText.text = "x" + lives.ToString();
        if (lives <=0)
        {
            FindObjectOfType<PlayerControl>().GetComponent<DestructionComponent>().Die();
        }
    }
}
