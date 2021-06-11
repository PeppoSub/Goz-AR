using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeGrow : MonoBehaviour
{
    public float MaxArea = 0.2f;
    public float MaxHeight = 0.05f;

    private Vector3 theSpawnPoint;

    void Start()
    {
        theSpawnPoint = transform.position;
        transform.localScale = new Vector3(0, 0, 0);
    }

    void Update()
    {
        float distance = Vector3.Distance(theSpawnPoint, transform.position);
        float scale = 1 - (1 / (Mathf.Abs(distance + 1) * Mathf.Abs(distance + 1)));
        transform.localScale = scale * new Vector3(MaxArea, MaxHeight, MaxArea);
    }

}
