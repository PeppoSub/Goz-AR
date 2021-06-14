using System;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore;

public class TextMeshProHighlightHack : TextMeshPro
{
    private Vector3[] Vertices;
    private Vector2[] UVs;
    private Color32[] Colors32;
    private int[] Triangles;

    private MeshFilter HighlightMeshFiter;

    protected override void GenerateTextMesh()
    {
        Vertices = new Vector3[0];
        UVs = new Vector2[0];
        Colors32 = new Color32[0];
        Triangles = new int[0];

        base.GenerateTextMesh();

        var m = new Mesh
        {
            vertices = Vertices,
            uv = UVs,
            colors32 = Colors32,
            triangles = Triangles
        };

        if (HighlightMeshFiter == null)
        {
            MeshRenderer mr;
            Transform highlightTransform = transform.Find("Highlight");

            if (highlightTransform == null)
            {
                var go = new GameObject("Highlight") { hideFlags = HideFlags.DontSave };
                go.transform.SetParent(transform, false);
                HighlightMeshFiter = go.AddComponent<MeshFilter>();
                mr = go.AddComponent<MeshRenderer>();
            }
            else
            {
                HighlightMeshFiter = highlightTransform.gameObject.GetComponent<MeshFilter>();
                mr = highlightTransform.gameObject.GetComponent<MeshRenderer>();
            }

            // In order to support the 'color' attribute (ex: <mark color=#RRGGBBAA>)
            // you might want to use a Shader using vertex colors here
            mr.material = new Material(Shader.Find("Unlit/Color"));
        }

        HighlightMeshFiter.mesh = m;
    }

    protected override void DrawTextHighlight(Vector3 start, Vector3 end, ref int index, Color32 highlightColor)
    {
        start += new Vector3(-.3f, .2f, .1f);
        end += new Vector3(.3f, -.4f, .1f);

        Array.Resize(ref Vertices, Vertices.Length + 4);
        Array.Resize(ref UVs, UVs.Length + 4);
        Array.Resize(ref Colors32, Colors32.Length + 4);
        Array.Resize(ref Triangles, Triangles.Length + 6);

        int vertIndex = Vertices.Length - 4;

        // UNDERLINE VERTICES FOR (3) LINE SEGMENTS
        #region HIGHLIGHT VERTICES
        Vector3[] vertices = Vertices;

        // Front Part of the Underline
        vertices[vertIndex + 0] = start; // BL
        vertices[vertIndex + 1] = new Vector3(start.x, end.y, start.z); // TL
        vertices[vertIndex + 2] = end; // TR
        vertices[vertIndex + 3] = new Vector3(end.x, start.y, end.z); // BR
        #endregion

        // UNDERLINE UV0
        #region HANDLE UV0
        Vector2[] uvs0 = UVs;

        // UVs for the Quad
        uvs0[0 + vertIndex] = new Vector2(0.01f, 0.01f); // BL
        uvs0[1 + vertIndex] = new Vector2(0.01f, .99f); // TL
        uvs0[2 + vertIndex] = new Vector2(.99f, .99f); // TR
        uvs0[3 + vertIndex] = new Vector2(.99f, 0.01f); // BR
        #endregion

        // HIGHLIGHT VERTEX COLORS
        #region
        // Alpha is the lower of the vertex color or tag color alpha used.
        highlightColor.a = m_fontColor32.a < highlightColor.a ? m_fontColor32.a : highlightColor.a;

        Color32[] colors32 = Colors32;
        colors32[0 + vertIndex] = highlightColor;
        colors32[1 + vertIndex] = highlightColor;
        colors32[2 + vertIndex] = highlightColor;
        colors32[3 + vertIndex] = highlightColor;
        #endregion

        Array.Resize(ref Triangles, Triangles.Length + 6);

        Triangles = new int[6 * Vertices.Length / 4];

        int index_X6 = 0;
        int index_X4 = 0;
        while (index_X4 / 4 < Vertices.Length / 4)
        {
            Triangles[index_X6 + 0] = index_X4 + 0;
            Triangles[index_X6 + 1] = index_X4 + 1;
            Triangles[index_X6 + 2] = index_X4 + 2;
            Triangles[index_X6 + 3] = index_X4 + 2;
            Triangles[index_X6 + 4] = index_X4 + 3;
            Triangles[index_X6 + 5] = index_X4 + 0;

            index_X4 += 4;
            index_X6 += 6;
        }
    }
}
