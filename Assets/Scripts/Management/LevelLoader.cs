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

    static public void LoadNextLevel()
    {
        int current = SceneManager.GetActiveScene().buildIndex;
        LoadLevel(current + 1);
    }

    static public void ReloadThisLevel()
    {
        int current = SceneManager.GetActiveScene().buildIndex;
        LoadLevel(current);
    }

    static public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex, LoadSceneMode.Single);

        // StartCoroutine(DelayedLoad(levelIndex));
        // // error CS0120: An object reference is required for the non-static field, method, or property 'LevelLoader.DelayedLoad(int)'
    }

    //IEnumerator DelayedLoad(int levelIndex)
    //{
    //    theTransition.SetTrigger("Start");
    //    yield return new WaitForSeconds(transitionTime);
    //    SceneManager.LoadScene(levelIndex);
    //}

}
