using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    private int score = 0;
    private TextMeshProUGUI scoreText;
    private GameSession session;
    // Start is called before the first frame update
    void Start()
    {
        session = FindObjectOfType<GameSession>();
        score = session.GetScore();
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

    public int GetScore()
    {
        return score;
    }
}
