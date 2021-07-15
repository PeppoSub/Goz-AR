using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetDifficulty : MonoBehaviour
{
    public int speedMultiplier = 3;    // percentage multiplier of basic enemy speed (1 to 10 = 10% to 1000%)
    public int spawnFrequency = 3;     // sets spawn frequency every 3 seconds (1 to 30 = 0.33Hz to 10Hz)
    public int missionGoal = 2;        // exponent in the power 3^(N-2) which multiplies the goal (1,2,3,4 = -1,0,1,2 = 0.33,1,3,9) 

    public Slider speedSlide;
    public Slider spawnSlide;
    public Slider goalSlide;

    public TextMeshProUGUI speedTxt;
    public TextMeshProUGUI spawnTxt;
    public TextMeshProUGUI goalTxt;

    //public float goalset =2;


    void Start()
    {
        //ResetDifficultyLevel();   // stack overflow :)

        speedMultiplier = PlayerPrefs.GetInt("speedMultiplier", -1);
        spawnFrequency = PlayerPrefs.GetInt("spawnFrequency", -1);
        missionGoal = PlayerPrefs.GetInt("missionGoal", -1);

        if (speedMultiplier < 0) { speedMultiplier = 3; }
        if (spawnFrequency < 0) { spawnFrequency = 1; }
        if (missionGoal < 0) { missionGoal = 2; }

        speedSlide.value = speedMultiplier;
        spawnSlide.value = spawnFrequency;
        goalSlide.value = missionGoal;

        //speedSlide.onValueChanged.AddListener((v) => { Speed((int)v); });
        //spawnSlide.onValueChanged.AddListener((v) => { Spawn((int)v); });
        //goalSlide.onValueChanged.AddListener((v) => { Goal((int)v); });
    }

    void Update()
    {
        int v = (int)speedSlide.value;
        int s = (int)spawnSlide.value;
        int g = (int)goalSlide.value;

        if (v != speedMultiplier) { Speed(v); }
        if (s != spawnFrequency) { Spawn(s); }
        if (g != missionGoal) { Goal(g); }

        speedTxt.text = (((float)speedMultiplier / 3f)).ToString("n2") + " m/s";    // see GameStatus about multipliers conventions
        spawnTxt.text = (((float)spawnFrequency / 2f)).ToString("n2") + " Hz";      // because the basic frequency is 1/2sec
        goalTxt.text = (missionGoal - 1).ToString() + " xp";                        // and this because ...

    }

    public void Speed(int v)
    {
        speedMultiplier = v;
        //speedTxt.text = ((int)(speedMultiplier / 3)).ToString() + " m/s";
        PlayerPrefs.SetInt("speedMultiplier", speedMultiplier);
        PlayerPrefs.Save();
    }

    public void Spawn(int s)
    {
        spawnFrequency = s;
        //spawnTxt.text = ((int)(spawnFrequency / 2)).ToString() + " Hz";
        PlayerPrefs.SetInt("spawnFrequency", spawnFrequency);
        PlayerPrefs.Save();
    }

    public void Goal(int g)
    {
        missionGoal = g;
        //goalTxt.text = (missionGoal-1).ToString() + " xp";
        PlayerPrefs.SetInt("missionGoal", missionGoal);
        PlayerPrefs.Save();
    }

    public void ResetDifficultyLevel()
    {
        PlayerPrefs.DeleteKey("speedMultiplier");
        PlayerPrefs.DeleteKey("spawnFrequency");
        PlayerPrefs.DeleteKey("missionGoal");
        PlayerPrefs.Save();

        Start();
    }

}
