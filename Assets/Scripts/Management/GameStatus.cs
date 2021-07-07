using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameStatus : MonoBehaviour
{
    public static int score;              // current score
    public static int life;               // current lives
    public static bool gotHit;            // becomes true when hit
    public static bool bossKill;          // becomes true when kill boss
    public static int mission;            // current mission
    public static int nBombs;             // bombs available
    public static int selectedWeapon;     // weapon in use
    public static float groundLevel;      // ground level (y)

    // make this into arrays, so I can dynamically select things based on level ... (portal: mission = 4)
    //public int[] weapons = { 0, 1, 2 , 3};        // weapon in use [for each mission] - object array is in WeaponScript.cs attached to PlayerHUD
    public int[] initlife = { 5, 5, 5, -1 };       // lives at start [of each mission]
    public int[] goal = { 30, -1, -1, -1 };       // score goal [of each mission]
    public int[] timelimit = { -1, 60, -1, -1 };  // timelimit (survival mode) [of each mission]
    public int[] initbombs = { 0, 3, 3, 0 };      // bombs at start [of each mission]

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

    private Button button;
    private Button bombutton;
    private float secondsCount;

    void Start()
    {
        //mission = SceneManager.GetActiveScene().buildIndex - 1;     // PlayerPrefs.GetInt("currentMission"); // 
        mission = PlayerPrefs.GetInt("currentMission") - 1;

        selectedWeapon = mission; // weapons[mission];  
        score = 0;
        life = initlife[mission];
        gotHit = false;
        bossKill = false;
        Time.timeScale = 1f;
        secondsCount = 0f;
        nBombs = initbombs[mission];

        groundLevel = 0f;
        GameObject[] spawner = GameObject.FindGameObjectsWithTag("Spawner");
        if(spawner.Length > 0) { groundLevel = spawner[0].transform.position.y; }

        button = shootButton.GetComponent<Button>();
        button.interactable = true;

        bombutton = bombButton.GetComponent<Button>();
    }

    void Update()
    {
        secondsCount += Time.deltaTime;
        string textbuffer;

        textbuffer = "" + score.ToString() + "/";
        if (goal[mission] > 0) { textbuffer += goal[mission].ToString(); } else { textbuffer += "-"; }
        scoreText.text = textbuffer;

        if((healthBar != null) && (initlife[mission] > 0))
        {
            for(int i = 0;i<healthBar.Length; i++)
            {
                if (i < life) { healthBar[i].SetActive(true); }
                else { healthBar[i].SetActive(false); }
            }
        }

        textbuffer = "" + ((int)secondsCount).ToString() + "/";
        if (timelimit[mission] > 0) { textbuffer += timelimit[mission].ToString(); } else { textbuffer += "-"; }
        timeText.text = textbuffer;

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

        if ((initlife[mission] > 0) && (life <= 0))
        {
            YouLose();
        }

        if (bossKill)
        {
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

        backButton.SetActive(true);
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
        Time.timeScale = 0.09f;
    }

    public void Score(int sc, int bd, int ld)
    {
        score += sc;
        if (scoreAnim != null) { scoreAnim.SetActive(true); }
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

}
