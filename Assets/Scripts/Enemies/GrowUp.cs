using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowUp : MonoBehaviour
{
    public float startScale = 0.1f;            // size when spawned (small)
    public float finalScale = 1f;              // size it will grow to (1=original prefab size)
    public float growthDistance = 1f;          // the growth happens within this distance from the spawnig point
    public float growthPower = 1f;             // it controls the profile of growth speed (1=linear)

    private Vector3 theSpawnPoint;             // where it is spawned into existence
    private Vector3 originalScale;             // original prefab scale (x,y,z)
    private float scale;                       // current size

    void Start()
    {
        theSpawnPoint = this.transform.position;
        originalScale = this.transform.localScale;

        scale = startScale;
        this.transform.localScale = scale * originalScale;
    }

    void Update()
    {
        if (scale < finalScale)
        { 
            scale = startScale;
            float distance = Vector3.Distance(theSpawnPoint, this.transform.position);
            scale += finalScale * Mathf.Pow(distance / growthDistance, growthPower);
            if (scale >= finalScale) { scale = finalScale; }

            this.transform.localScale = scale * originalScale;
        }
    }
}
