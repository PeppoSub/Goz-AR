using System.Collections;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit();
    }

    public void ResetAndQuit()
    {
        ResetProgress();
        Application.Quit();
    }

    public void ResetProgress()
    {
        PlayerPrefs.SetInt("level1", 0);
        PlayerPrefs.SetInt("level2", 0);
        PlayerPrefs.SetInt("level3", 0);
        PlayerPrefs.Save();
    }
}
