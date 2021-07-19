using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrowseCanvas3D : MonoBehaviour
{
    public GameObject[] canvas3D;
    //public GameObject[] positions;
    public GameObject transition;
    //public Vector3 distance3d = new Vector3(0.2f, 0.2f, 0.2f);

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
            // positions[i] += i * distance3d;
            positions[i] = canvas3D[i].transform.position;

            //Debug.Log("positions[" + i + "] = " + positions[i].ToString());
        }

    }

    void Update()
    {
        
    }

    public void CycleCanvas()
    {
        posZero = NoutOfBound(posZero - 1);

        transition.SetActive(true);
        for(int i = 0; i<canvas3D.Length;i++)
        {
            int j = (posZero + i) % positions.Length;
            Debug.Log("Canvas[" + i + "] = " + j + " ... ");// + positions[j].ToString());

            canvas3D[i].transform.position = positions[j];
        }
    }

    public int NoutOfBound(int i)
    {
        while (i < 0) { i += canvas3D.Length; } ;
        while (i> canvas3D.Length) { i -= canvas3D.Length; }
        return i;
    }

   
}
