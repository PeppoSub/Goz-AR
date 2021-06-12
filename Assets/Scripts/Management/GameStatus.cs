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
    public static float groundLevel;      // ground level (y)

    public int initlife = 5;              // lives at start
    public int goal = 30;                 // score goal
    public int timelimit = 60;            // timelimit (survival mode)

    public GameObject bloodyDamage;       // screen border when the player gets hit
    public GameObject brokenGlass;        // screen effect when game over
    public GameObject youWin;             // screen text when finish level
    public GameObject shootButton;        // buttons ...
    public GameObject restartButton;
    public GameObject backButton;
    public TextMeshProUGUI scoreText;     // hud text ...
    public TextMeshProUGUI lifeText;
    public TextMeshProUGUI timeText;

    private Button button;
    private float secondsCount;

    void Start()
    {
        button = shootButton.GetComponent<Button>();
        button.interactable = true;

        score = 0;
        life = initlife;
        gotHit = false;
        bossKill = false;
        Time.timeScale = 1f;
        secondsCount = 0f;
        mission = SceneManager.GetActiveScene().buildIndex ;

        groundLevel = 0f;
        GameObject[] spawner = GameObject.FindGameObjectsWithTag("Spawner");
        if(spawner.Length > 0) { groundLevel = spawner[0].transform.position.y; }

        var color = bloodyDamage.GetComponent<Image>().color;
        color.a = 0f;
        bloodyDamage.GetComponent<Image>().color = color;
    }

    void Update()
    {
        secondsCount += Time.deltaTime;
        string textbuffer;

        textbuffer = "Score: " + score.ToString() + "/";
        if (goal > 0) { textbuffer += goal.ToString(); } else { textbuffer += "-"; }
        scoreText.text = textbuffer;

        textbuffer = "Life: " + life.ToString() + "/";
        if (initlife > 0) { textbuffer += initlife.ToString(); } else { textbuffer += "-"; }
        lifeText.text = textbuffer;

        textbuffer = "Time: " + ((int)secondsCount).ToString() + "/";
        if (timelimit > 0) { textbuffer += timelimit.ToString(); } else { textbuffer += "-"; }
        timeText.text = textbuffer;

        if ((timelimit > 0) && (secondsCount >= timelimit))
        {
            // Time.timeScale *= Mathf.Exp((secondsCount - timelimit)/timelimit);  // :)
            YouWin();
        }

        if ((goal > 0) && (score >= goal))
        {
            YouWin();
        }

        if ((initlife > 0) && (life <= 0))
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
        string strlev = "level" + mission.ToString();
        PlayerPrefs.SetInt(strlev, 1);
        PlayerPrefs.Save();

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
        button.interactable = false;

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
        Time.timeScale = 0.1f;
    }

}
