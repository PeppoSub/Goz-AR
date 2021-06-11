using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowScript : MonoBehaviour
{
    public float startSize = 0.01f;           // size when spawned (small)
    public float maxSize = 0.2f;              // size it will grow to

    private Vector3 theSpawnPoint;

    void Start()
    {
        theSpawnPoint = this.transform.position;
        this.transform.localScale = startSize * Vector3.one;
    }

    void Update()
    {
        float distance = Vector3.Distance(theSpawnPoint, this.transform.position);
        float scale = 1 - (1 / (Mathf.Abs(distance + 1) * Mathf.Abs(distance + 1)));
        this.transform.localScale = (scale * maxSize) * Vector3.one;
    }

}
