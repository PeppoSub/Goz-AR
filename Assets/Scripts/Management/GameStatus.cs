using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameStatus : MonoBehaviour
{
    // mission items into arrays, so I can dynamically select things based on level ... (portal: mission = 4)
    //private int[] weapons = { 0, 1, 2 , 3};      // weapon in use [for each mission] - object array is in WeaponScript.cs attached to PlayerHUD
    private int[] goal = { 30, -1, -1, -1 };       // score goal [of each mission]
    private int[] timelimit = { -1, 60, -1, -1 };  // timelimit (survival mode) [of each mission]
    private int[] initbombs = { 0, 0, 1, 0 };      // bombs at start [of each mission]
    private int initlife = 3;                      // lives at start (plus completion bonus)

    public static int score;              // current score
    public static int life;               // current lives
    public static bool gotHit;            // becomes true when hit
    public static bool bossKill;          // becomes true when kill boss
    public static int mission;            // current mission (from PlayerPrefs)
    public static int nBombs;             // bombs available (from PlayerPrefs)
    public static int selectedWeapon;     // weapon in use (from PlayerPrefs)
    public static float groundLevel;      // ground level (y)
    public static int bossHealth;         // current boss health
    public static int bossMaxHealth;      // boss max health

    public static int nCompletions;       // how many time killed the boss (from PlayerPrefs)

    public static int speedMultiplier;    // 1/3 multiplier of basic enemy speed (1 to 15 = 0.33 to 5 m/s, normal = 3)
    public static int spawnFrequency;     // sets spawn frequency every (1 to 20 = 0.5Hz to 10 Hz, normal = 2)
    public static int missionGoal;        // exponent in the power 3^(N-2) which multiplies the goal (1,2,3,4,5 = -1,0,1,2,3 = 0.33,1,3,9,27 normal = 2) 

    public GameObject bloodyDamage;       // screen border when the player gets hit
    public GameObject brokenGlass;        // screen effect when game over
    public GameObject youWin;             // screen text when finish level
    public GameObject shootButton;        // buttons ...
    public GameObject bombButton;
    public GameObject restartButton;
    public GameObject backButton;
    public GameObject[] healthBar;        // 1 heart = 1 life
    public GameObject scoreAnim;          // animation for scoring (star flies to score counter)
    public GameObject bombAnim;           // ...
    public GameObject lifeAnim;           // ...
    public TextMeshProUGUI scoreText;     // hud text ...
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI bossText;
    public GameObject timerDisplay;       // timer icon and counter
    public GameObject bossDisplay;        // boss health

    private Button button;
    private Button bombutton;
    private float secondsCount;

    void Start()
    {
        // load playerprefs stuff  
        speedMultiplier = PlayerPrefs.GetInt("speedMultiplier",-1) ;     // affects GroundMove & AirMove
        spawnFrequency = PlayerPrefs.GetInt("spawnFrequency", -1);       // affects BubbleSpawn
        missionGoal = PlayerPrefs.GetInt("missionGoal", -1);             // affects this & BossStatic
        mission = PlayerPrefs.GetInt("currentMission", 1) - 1;
#if UNITY_EDITOR
        mission = 0;
        //mission = SceneManager.GetActiveScene().buildIndex - 1;     // PlayerPrefs.GetInt("currentMission"); // 
#endif
        nCompletions = PlayerPrefs.GetInt("nCompletion", 0);            // 

        if (missionGoal > 0)   // GameStatus.missionGoal
        { 
            for(int i = 0;i< goal.Length;i++)
            {
                goal[i] = (int)(goal[i] * Mathf.Pow(3, missionGoal-2));
                timelimit[i] = (int)(timelimit[i] * Mathf.Pow(3, missionGoal-2));
                // boss health implemented in BossStatic.cs
            }
        }

        selectedWeapon = mission; // weapons[mission];  
        score = 0;

        life = initlife + nCompletions; if(life>5) { life = 5; }  // maximum 5 lives

        gotHit = false;
        bossKill = false;
        Time.timeScale = 1f;
        secondsCount = 0f;
        nBombs = initbombs[mission];

        bossHealth = -1;
        bossMaxHealth = -1;

        groundLevel = 0f;
        GameObject[] spawner = GameObject.FindGameObjectsWithTag("Spawner");
        if(spawner.Length > 0) { groundLevel = spawner[0].transform.position.y; }

        button = shootButton.GetComponent<Button>();
        button.interactable = true;

        bombutton = bombButton.GetComponent<Button>();

        for (int i = 0; i < healthBar.Length; i++)
        {
            healthBar[i].SetActive(false);
        }

        timerDisplay.SetActive(false);
        bossDisplay.SetActive(false);

    }

    void Update()
    {
        secondsCount += Time.deltaTime;
        string textbuffer;

        textbuffer = "" + score.ToString() + "/";
        if (goal[mission] > 0) { textbuffer += goal[mission].ToString(); } else { textbuffer += "-"; }
        scoreText.text = textbuffer;

        if(healthBar.Length > 0)
        {
            for(int i = 0;i<healthBar.Length; i++)
            {
                if (i < life) { healthBar[i].SetActive(true); }
                else { healthBar[i].SetActive(false); }
            }
        }

        if((mission > 0) && (timerDisplay != null))
        {
            timerDisplay.SetActive(true);
            textbuffer = "" + ((int)secondsCount).ToString() + "/";
            if (timelimit[mission] > 0) { textbuffer += timelimit[mission].ToString(); } else { textbuffer += "-"; }
            timeText.text = textbuffer;
        }

        if ((mission>1) && (bossDisplay != null))
        {
            bossDisplay.SetActive(true);
            textbuffer = "" + bossHealth.ToString() + "/";
            if (bossMaxHealth > 0) { textbuffer += bossMaxHealth.ToString(); } else { textbuffer += "-"; }
            bossText.text = textbuffer;
        }

        if (nBombs > 0) { bombButton.SetActive(true); }
        else { bombButton.SetActive(false); }

        if ((timelimit[mission] > 0) && (secondsCount >= timelimit[mission]))
        {
            // Time.timeScale *= Mathf.Exp((secondsCount - timelimit)/timelimit);  // :)
            YouWin();
        }

        if ((goal[mission] > 0) && (score >= goal[mission]))
        {
            YouWin();
        }

        if ((initlife > 0) && (life <= 0))
        {
            YouLose();
        }

        if (bossKill)
        {
            nCompletions += 1;
            PlayerPrefs.SetInt("nCompletion", nCompletions);
            YouWin();
        }

        if (gotHit)
        {
            bloodyDamage.SetActive(true);
            gotHit = false;
        }
    }

    public void YouWin()
    {
        // set current mission as completed
        int missionNr = mission + 1;
        string strlev = "level" + (missionNr).ToString();
        PlayerPrefs.SetInt(strlev, 1);
        PlayerPrefs.Save();
        Debug.Log("Saving Mission " + missionNr + ":  " + strlev + " = 1");

        youWin.SetActive(true);
        EndGame();
    }

    public void YouLose()
    {
        brokenGlass.SetActive(true);
        EndGame();
    }

    public void EndGame()
    {
        gotHit = false;
        bloodyDamage.SetActive(false);
        shootButton.SetActive(false);
        bombButton.SetActive(false);
        bombAnim.SetActive(false);

        // backButton.SetActive(true);   // always active
        restartButton.SetActive(true);

        // disable movement and hit effect on all enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            if (enemy.GetComponent<Damaged>() != null)
            {
                enemy.GetComponent<Damaged>().enabled = false;
            }
            if (enemy.GetComponent<AirMove>() != null)
            {
                enemy.GetComponent<AirMove>().enabled = false;
            }
            if (enemy.GetComponent<GroundMove>() != null)
            {
                enemy.GetComponent<GroundMove>().enabled = false;
            }
        }

        // slow down time
        Time.timeScale = 0.05f;
    }

    public void Score(int sc, int bd, int ld)
    {
        score += sc;
        if (sc > 1)    // animation only for score > 2
        {
            if (scoreAnim != null) { scoreAnim.SetActive(true); }
        }
        if (bd>0)
        {
            nBombs += bd;
            if (bombAnim != null) { bombAnim.SetActive(true); }
        }
        if (ld > 0)
        {
            life += ld;
            if (lifeAnim != null) { lifeAnim.SetActive(true); }
        }
    }

    public static void BossHealth(int health, int maxHealth)
    {
        bossHealth = health;  
        bossMaxHealth = maxHealth;
        
        // //if (bossMission)
        //string textbuffer = "" + ((int)health).ToString() + "/";
        //if(maxHealth > 0) { textbuffer += maxHealth.ToString(); } else { textbuffer += "-"; }
        //bossText.text = textbuffer;
    }

    public static void ReloadDifficultySettings()
    { 
        speedMultiplier = PlayerPrefs.GetInt("speedMultiplier",-1) ;     // affects GroundMove & AirMove
        spawnFrequency = PlayerPrefs.GetInt("spawnFrequency", -1);       // affects BubbleSpawn
        missionGoal = PlayerPrefs.GetInt("missionGoal", -1);             // affects this & BossStatic

        Debug.Log("speedMultiplier = " + speedMultiplier.ToString() + ", spawnFrequency = " + spawnFrequency.ToString() + ", missionGoal = " + missionGoal.ToString());
    }

}
