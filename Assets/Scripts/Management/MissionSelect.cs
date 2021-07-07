using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissionSelect : MonoBehaviour
{
    public Camera theCamera;
    public TextMeshProUGUI theTargetText;

    public GameObject lev1;
    public GameObject lev2;
    public GameObject lev3;
    public GameObject lev4;

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

        lev1.SetActive(true);
        lev2.SetActive(true);
        lev3.SetActive(false);
        lev4.SetActive(false);

        if ((level1 == 1) && (level2 == 1)) { lev3.SetActive(true); }
        if (level3 == 1) { lev4.SetActive(true); }

        Time.timeScale = 1f;
    }

    void Update()
    {
        if (!TryGetTouchPosition()) { return; }

        Ray theRay = theCamera.ScreenPointToRay(touchPosition);
        RaycastHit theHit;

        bool didHit = Physics.Raycast(theRay, out theHit, 100.0f);
        if (didHit)
        {
            loadMission = theHit.collider.gameObject.name;
            Debug.Log("levelToLoad = " + loadMission);
            
            JustLoad(loadMission);
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
