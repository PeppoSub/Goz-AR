// Attach this script to a GameObject with a Renderer component attached
// If the GameObject is visible to the camera ...
// ... it plays the AudioClip component attached to the Audio Source
// The audio played in this example comes from AudioClip and is called audioData.
//
// Basically, I just put together these 2 examples:
//  https://docs.unity3d.com/ScriptReference/Renderer-isVisible.html
//  https://docs.unity3d.com/ScriptReference/AudioSource.Play.html

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayWhenVisible : MonoBehaviour
{
    private Renderer m_Renderer;
    private AudioSource audioData;

    void Start()
    {
        m_Renderer = GetComponent<Renderer>();

        audioData = GetComponent<AudioSource>();
        audioData.Play(0);
        //Debug.Log("AudioData:  play started!");
    }

    void Update()
    {
        if (m_Renderer.isVisible)
        {
            audioData.UnPause();
            //Debug.Log("Object is visible:  playing sound!");
        }
        else
        {
            audioData.Pause();
            //Debug.Log("Object is no longer visible:  sound paused!");
        }
    }
}
