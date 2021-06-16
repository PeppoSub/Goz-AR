using System.Collections;
using UnityEngine;
using TMPro;

public class StartMenu : MonoBehaviour
{
    public TextMeshProUGUI theTargetText;
    public GameObject ball1;
    public GameObject ball2;
    public GameObject ball3;

    public float theWaitingTime = 3f;
    public bool doClean = false;

    void Start()
    {
        // enable this to reset level completion on each play (and for testing)
        if(doClean)
        {
            PlayerPrefs.SetInt("level0", 0);
            PlayerPrefs.SetInt("level1", 0);
            PlayerPrefs.SetInt("level2", 0);
            PlayerPrefs.SetInt("level3", 0);
            PlayerPrefs.Save();
        }

        // this will better become an array 
        int level0 = PlayerPrefs.GetInt("level0");
        int level1 = PlayerPrefs.GetInt("level1");
        int level2 = PlayerPrefs.GetInt("level2");
        int level3 = PlayerPrefs.GetInt("level3");

        // if we want to save other stuff, such as player name or game volume setting ...
        //string player = PlayerPrefs.GetString("username");
        //float volume = PlayerPrefs.GetFloat("volume");

        // no ball0 (this is the portal demo)
        ball1.SetActive(false);
        ball2.SetActive(false);
        ball3.SetActive(false);

        if (level1 == 1) { ball1.SetActive(true); }
        if (level2 == 1) { ball2.SetActive(true); }
        if (level3 == 1) { ball3.SetActive(true); }

        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(index: 0);
            string theParentName = "";

            if (touch.phase == TouchPhase.Began)
            {
                Ray theRay = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit theHit;

                if (Physics.Raycast(theRay, out theHit))
                {
                    if (theHit.collider != null)
                    {
                        theParentName = theHit.collider.gameObject.transform.parent.gameObject.name;
                        theTargetText.text = "# " + theParentName;
                        DebugUtility.log("StartMenu " + theTargetText.text);
                        // StartCoroutine(DelayedLoad(theParentName));
                        JustLoad(theParentName);
                    }
                }
            }
        }
    }

    public void JustLoad(string theParentName)
    {
        //int levelToLoad = 0;
        if (theParentName == "Level1")
        {
            theTargetText.text = "Loading (Lev1) ...";
            SetCurrentMission(1);
        }
        else if (theParentName == "Level2")
        {
            theTargetText.text = "Loading (Lev2) ...";
            SetCurrentMission(2);
        }
        else if (theParentName == "Level3")
        {
            theTargetText.text = "Loading (Level3) ...";
            SetCurrentMission(3);
        }
        else if (theParentName == "Portal")
        {
            theTargetText.text = "Loading (AR Portal) ...";
            SetCurrentMission(4);
        }
        else if (theParentName == "Menu")
        {
            theTargetText.text = "Loading (Credits) ...";
            LevelLoader.LoadLevel(2);     // scene index of the credit scene or menu
        }
        else
        {
            theTargetText.text = "...";
        }

        //Debug.Log("levelToLoad = " + levelToLoad);
        // LevelLoader.LoadLevel(levelToLoad);
    }

    public void TestLev1()
    {
        theTargetText.text = "Button is pushed ...";
        //DelayedLoad("Level1");
    }

    public void SetCurrentMission(int levelIndex)
    {
        string strlev = "currentMission";
        PlayerPrefs.SetInt(strlev, levelIndex);
        PlayerPrefs.Save();
        LevelLoader.LoadMission(levelIndex);    // no need the parameters
    }

}
