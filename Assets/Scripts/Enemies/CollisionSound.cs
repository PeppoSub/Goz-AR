using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSound : MonoBehaviour
{
    //public AudioClip soundToPlay;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //audioSource.clip = soundToPlay;

    }

    void OnCollisionEnter(Collision col)
    {
        audioSource.Play();
        Debug.Log("Pop pop pop ...");
    }

}
