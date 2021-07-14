using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDifficulty : MonoBehaviour
{
    public int speedMultiplier = 3;    // percentage multiplier of basic enemy speed (1 to 10 = 10% to 1000%)
    public int spawnFrequency = 3;     // sets spawn frequency every 3 seconds (1 to 30 = 0.33Hz to 10Hz)
    public int missionGoal = 1;        // exponent in the power 3^(N-2) which multiplies the goal (1,2,3,4 = -1,0,1,2 = 0.33,1,3,9) 

    public float goalset =2;

    void Start()
    {
        speedMultiplier = PlayerPrefs.GetInt("speedMultiplier", -1);
        spawnFrequency = PlayerPrefs.GetInt("spawnFrequency", -1);
        missionGoal = PlayerPrefs.GetInt("missionGoal", -1);

        if (speedMultiplier < 0) { speedMultiplier = 3; }
        if (spawnFrequency < 0) { spawnFrequency = 3; }
        if (missionGoal < 0) { missionGoal = 1; }
    }

    void Update()
    {
        
    }

    public void Speed(int s)
    {
        speedMultiplier = s;
        PlayerPrefs.SetInt("speedMultiplier", speedMultiplier);
        PlayerPrefs.Save();
    }

    public void Spawn(int s)
    {
        spawnFrequency = s;
        PlayerPrefs.SetInt("spawnFrequency", spawnFrequency);
        PlayerPrefs.Save();
    }

    public void Goal(int g)
    {
        missionGoal = g;
        PlayerPrefs.SetInt("missionGoal", missionGoal);
        PlayerPrefs.Save();
    }

    public void ResetDifficultyLevel()
    {
        PlayerPrefs.SetInt("speedMultiplier", -1);
        PlayerPrefs.SetInt("spawnFrequency", -1);
        PlayerPrefs.SetInt("missionGoal", -1);
        PlayerPrefs.Save();

        Start();
    }

}
