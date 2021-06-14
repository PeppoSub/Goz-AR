using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashText : MonoBehaviour
{
    public float duration = 1f;

    private float seconds;
    void Start()
    {
        seconds = 0f;
    }

    void Update()
    {
        if (this.gameObject.activeSelf) { seconds += Time.deltaTime; }
        if (seconds >= duration)
        {
            this.gameObject.SetActive(false);
        }
    }
}
