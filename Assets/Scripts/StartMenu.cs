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

    void Start()
    {
        // enable this to reset level completion on each play (and for testing)
        //PlayerPrefs.SetInt("level1", 0);
        //PlayerPrefs.SetInt("level2", 0);
        //PlayerPrefs.SetInt("level3", 0);
        //PlayerPrefs.Save();

        // this will better become an array 
        int level1 = PlayerPrefs.GetInt("level1");
        int level2 = PlayerPrefs.GetInt("level2");
        int level3 = PlayerPrefs.GetInt("level3");

        // if we want to save other stuff, such as player name or game volume setting ...
        //string player = PlayerPrefs.GetString("username");
        //float volume = PlayerPrefs.GetFloat("volume");

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

    // for some reason this has stopped working ... fuck Unity!
    IEnumerator DelayedLoad(string theParentName)
    {
        int levelToLoad = 0;

        yield return new WaitForSeconds(theWaitingTime / 2f);

        if (theParentName == "Level1")
        {
            theTargetText.text = "Loading (Lev1) ...";
            levelToLoad = 1;
        }
        else if (theParentName == "Level2")
        {
            theTargetText.text = "Loading (Lev2) ...";
            levelToLoad = 2;
        }
        else if (theParentName == "Level3")
        {
            theTargetText.text = "Loading (Level3) ...";
            levelToLoad = 3;
        }
        else if (theParentName == "Menu")
        {
            theTargetText.text = "Loading (ShowCase) ...";
            levelToLoad = 4;
        }
        else if (theParentName == "Portal")
        {
            theTargetText.text = "Loading (AR Portal) ...";
            levelToLoad = 5;
        }
        else
        {
            theTargetText.text = "...";
        }
        Debug.Log("levelToLoad (delayed) = " + levelToLoad);

        yield return new WaitForSeconds(theWaitingTime / 2f);

        LevelLoader.LoadLevel(levelToLoad);
    }

    public void JustLoad(string theParentName)
    {
        int levelToLoad = 0;

        if (theParentName == "Level1")
        {
            theTargetText.text = "Loading (Lev1) ...";
            levelToLoad = 1;
        }
        else if (theParentName == "Level2")
        {
            theTargetText.text = "Loading (Lev2) ...";
            levelToLoad = 2;
        }
        else if (theParentName == "Level3")
        {
            theTargetText.text = "Loading (Level3) ...";
            levelToLoad = 3;
        }
        else if (theParentName == "Menu")
        {
            theTargetText.text = "Loading (ShowCase) ...";
            levelToLoad = 4;
        }
        else if (theParentName == "Portal")
        {
            theTargetText.text = "Loading (AR Portal) ...";
            levelToLoad = 5;
        }
        else
        {
            theTargetText.text = "...";
        }

        //Debug.Log("levelToLoad = " + levelToLoad);
        LevelLoader.LoadLevel(levelToLoad);
    }

    public void TestLev1()
    {
        theTargetText.text = "Button is pushed ...";
        DelayedLoad("Level1");
    }
}
