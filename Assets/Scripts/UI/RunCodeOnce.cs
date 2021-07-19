using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// singleton script from:  https://stackoverflow.com/questions/53735926/run-specific-code-only-once-per-session-on-app-start-in-unity

public class RunCodeOnce : MonoBehaviour
{
    public static RunCodeOnce Instance;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }   // stops dups running
        DontDestroyOnLoad(gameObject);                           // keep me forever
        Instance = this;                                         // set the reference to it

        // things to execute only once per game session ...
        SkyBoxScript skBx = this.gameObject.GetComponent<SkyBoxScript>();
        // skBx.ChangeSkyOnce(); // now is private ...
    }
}
