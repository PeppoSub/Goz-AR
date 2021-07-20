using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrowseCanvas3D : MonoBehaviour
{
    public GameObject[] canvas3D;
    public GameObject transition;
    public int wobbleFor = 1;
    public float wobbleAmp = 0.01f;

    private Vector3 initPos;
    private int nCanvas;
    private int posZero;
    private Vector3[] positions;

    void Start()
    {
        nCanvas = canvas3D.Length;
        posZero = 0;
        positions = new Vector3[nCanvas];

        for (int i = 0; i < nCanvas; i++)
        {
            positions[i] = new Vector3(0f,0f,0f);
            positions[i] = canvas3D[i].transform.position;
            //Debug.Log("positions[" + i + "] = " + positions[i].ToString());
        }

        // wobbleAmp = positions[0].y - positions[nCanvas - 1].y;
    }

    void Update()
    {
    }

    public void CycleCanvas()
    {
        posZero = NoutOfBound(posZero - 1);

        transition.SetActive(true);
        for(int i = 0; i< nCanvas; i++)
        {
            int j = (posZero + i) % nCanvas;
            // Debug.Log("Canvas[" + i + "] = " + j + " ... ");
            canvas3D[i].transform.position = positions[j];
        }

        StartCoroutine(DoWobble());
    }

    public int NoutOfBound(int i)
    {
        while (i < 0) { i += nCanvas; } ;
        while (i> nCanvas) { i -= nCanvas; }
        return i;
    }

    //public void DoWobble()
    //{
    //    // float seconds = time
    //    for (int i = 0; i < nCanvas; i++)
    //    {
    //        int j = NoutOfBound(i);
    //        float omega = 2 * Mathf.PI * wobbleFrequency * seconds + j/10f;
    //        float amplitude = Mathf.Sin(omega);
    //        Vector3 canvasPos = canvas3D[j].transform.position;
    //        canvasPos.y += amplitude;
    //        canvas3D[j].transform.position = canvasPos;
    //    }

    //    if(seconds >= wobbleFor) { doWobble = false; }
    //}

    IEnumerator DoWobble()
    {
        Vector3[] wobbled = new Vector3[nCanvas]; ;
        for (int i = 0; i < nCanvas; i++)
        {
            int j = (posZero + i) % nCanvas;
            wobbled[j] = new Vector3(0f, 0f, 0f);
            wobbled[j] = canvas3D[j].transform.position;
        }

        float seconds = 0;
        while (seconds < wobbleFor)
        {
            for (int i = 0; i < nCanvas; i++)
            {
                int j = (posZero + i) % nCanvas;
                float omega = (2 * Mathf.PI * seconds / wobbleFor) + (0.3f * Mathf.PI * i / nCanvas);
                float amplitude = wobbleAmp * Mathf.Sin(omega);

                //Vector3 canvasPos = canvas3D[j].transform.position;
                wobbled[j].y += amplitude;
                canvas3D[j].transform.position = wobbled[j];
            }

            seconds += Time.deltaTime;
            yield return null;
        }
    }
}
