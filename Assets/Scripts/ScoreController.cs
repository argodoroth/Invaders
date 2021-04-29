using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    private int score = 0;
    private TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        TextMeshProUGUI[] texts = FindObjectsOfType<TextMeshProUGUI>();
        for (int i =0; i< texts.Length; i++)
        {
            if (texts[i].name == "Score Text")
            {
                scoreText = texts[i];
            }
        }
        scoreText.text = score.ToString();
    }

    public void IncreaseScore(int increase)
    {
        score += increase;
        scoreText.text = score.ToString();
    }
}
