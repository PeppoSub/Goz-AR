using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountFPS : MonoBehaviour
{
    public TextMeshProUGUI fpsText;

    private float refreshRate = 1f;
    private float timer;

    private void Start()
    {
        timer = refreshRate;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            int fps = (int)(1f / Time.unscaledDeltaTime);
            fpsText.text = "FPS: " + fps.ToString();
            timer = refreshRate;
        }
    }
}