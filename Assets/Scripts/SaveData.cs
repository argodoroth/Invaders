using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Simple class to hold save data
[System.Serializable]
public class SaveData
{
    public int currentLevel = 0;
    public int currentLives = 5;
    public int currentScore = 0;
    public int highscore = 0;
}
