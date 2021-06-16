using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneButton : MonoBehaviour
{
    public int missionIdx = 0;

    public void click()
    {
        StartCoroutine(LoadSceneAsynch());
    }

    IEnumerator LoadSceneAsynch()
    {
        // the current scene must be uploaded once the cache has been moved to the next
        Scene current = SceneManager.GetActiveScene();

        // load the scene asynchronously
        AsyncOperation asynchLoad = SceneManager.LoadSceneAsync(missionIdx);
        DebugUtility.log("loading scene:" + missionIdx.ToString());

        // Just wait till the load is done
        while (!asynchLoad.isDone) yield return null;
    }
}
