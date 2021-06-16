using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelLoader : MonoBehaviour
{
    // // fade to black transition, see:  https://www.youtube.com/watch?v=CE9VOZivb3I&ab_channel=Brackeys
    // public Animator theTransition;
    // public float transitionTime = 1f;

    public static int missionScene = 1;     // this is the build index of the Mission Scene (0: start menu, 1: mission scene, 2: credits)

    void Start()
    {
        // Restore normal time
        Time.timeScale = 1f;

        // Disable screen dimming
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    static public void LoadStartScreen()
    {
        LoadLevel(0);
    }

//    static public void LoadNextLevel()
//    {
//        int current = SceneManager.GetActiveScene().buildIndex;
//        LoadLevel(current + 1);
//    }

    static public void ReloadThisLevel()
    {
        int current = SceneManager.GetActiveScene().buildIndex;
        LoadLevel(current);
    }

    static public void LoadLevel(int levelIndex)
    {
        // standard method: loads the scene with build index = levelIndex 
        SceneManager.LoadScene(levelIndex, LoadSceneMode.Single);
    }

    static public void LoadMission(int missionIdx)
    {
        // assume the current mission is already stored in 'PlayerPrefs' as "currentMission"
        // int missionIdx = PlayerPrefs.GetInt("currentMission");
        Debug.Log("LevelLoader.LoadMission(" + missionIdx + ") ... loading Scene n." + missionScene.ToString() + " with missionIdx = " + missionIdx.ToString());
        LevelLoader.LoadLevel(missionScene);
    }

    //IEnumerator DelayedLoad(int levelIndex)
    //{
    //    theTransition.SetTrigger("Start");
    //    yield return new WaitForSeconds(transitionTime);
    //    SceneManager.LoadScene(levelIndex);
    //
    // // error CS0120: An object reference is required for the non-static field, method, or property 'LevelLoader.DelayedLoad(int)'
    //}

}
