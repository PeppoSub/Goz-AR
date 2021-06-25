using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissionSelect : MonoBehaviour
{
    public Camera theCamera;
    public TextMeshProUGUI theTargetText;

    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    private Vector2 touchPosition;
    private string loadMission;

    void Start()
    {
        // PlayerPrefs.SetInt("level0", 0);
        // PlayerPrefs.SetInt("level1", 0);
        // PlayerPrefs.SetInt("level2", 0);
        // PlayerPrefs.SetInt("level3", 0);
        // PlayerPrefs.Save();

        int level1 = PlayerPrefs.GetInt("level1");
        int level2 = PlayerPrefs.GetInt("level2");
        int level3 = PlayerPrefs.GetInt("level3");

        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);

        if (level1 == 1) { star1.SetActive(true); }
        if (level2 == 1) { star2.SetActive(true); }
        if (level3 == 1) { star3.SetActive(true); }

        Time.timeScale = 1f;
    }

    void Update()
    {
        if (!TryGetTouchPosition()) { return; }

        //if (Input.touchCount <= 0) return;
        //Touch touch = Input.GetTouch(index: 0);
        //if (touch.phase != TouchPhase.Ended) return;

        //Ray theRay = theCamera.ScreenPointToRay(touch.position);
        Ray theRay = theCamera.ScreenPointToRay(touchPosition);
        RaycastHit theHit;

        bool didHit = Physics.Raycast(theRay, out theHit, 100.0f);
        if (didHit)
        {
            loadMission = theHit.collider.gameObject.name;

            Debug.Log("levelToLoad = " + loadMission);
            
            JustLoad(loadMission);
            //if (loadMission.StartsWith("Lev")) 
            //{
            //    string numbersOnly = Regex.Replace(loadMission, "[^0-9]", "");
            //    missionNr = int.Parse(numbersOnly);
            //    Debug.Log("levelToLoad = " + missionNr.ToString());
            //    theTargetText.text = "Loading (Lev" + missionNr.ToString() + ") ...";
            //    SetCurrentMission(missionNr);
            //}
        }

    }

    bool TryGetTouchPosition()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            var mousePosition = Input.mousePosition;
            touchPosition = new Vector2(mousePosition.x, mousePosition.y);
            return true;
        }
#else
            if (Input.touchCount > 0) 
            {
                 Touch touch = Input.GetTouch(index: 0);
                 if (touch.phase == TouchPhase.Ended) 
                 {
                   touchPosition = Input.GetTouch(0).position;
                   return true;
                 }
            }
#endif
        touchPosition = Vector2.zero;
        return false;
    }

    public void JustLoad(string theParentName)
    {
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

    public void SetCurrentMission(int levelIndex)
    {
        string strlev = "currentMission";
        PlayerPrefs.SetInt(strlev, levelIndex);
        PlayerPrefs.Save();
        LevelLoader.LoadMission(levelIndex);    // no need the parameters
    }
}
