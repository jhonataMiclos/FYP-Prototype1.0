using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent (typeof(MeshFilter),typeof(MeshRenderer))]
public class ProceduralGrid : MonoBehaviour {
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    public float cellSize = 1;
    public Vector3 gridOffset;
    public int gridSize;


	// Use this for initialization
	void Awake () {
        mesh = GetComponent<MeshFilter> ().mesh;
	}
	
	// Update is called once per frame
	void Start () {
        MakeContinuesProceduralGrid ();
        UpdateMesh ();
	}

    void MakeDiscreteProceduralGrid ()
    {
        vertices = new Vector3[gridSize * gridSize * 4];
        triangles = new int[gridSize * gridSize * 6];
        float vertexOffset = cellSize * 0.5f;
        int v = 0;
        int t = 0;

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                Vector3 cellOffset = new Vector3(x * cellSize, 0, y * cellSize);

                vertices[v] = new Vector3(-vertexOffset, 0, -vertexOffset) + cellOffset + gridOffset;
                vertices[v + 1] = new Vector3(-vertexOffset, 0, vertexOffset) + cellOffset + gridOffset;
                vertices[v + 2] = new Vector3(vertexOffset, 0, -vertexOffset) + cellOffset + gridOffset;
                vertices[v + 3] = new Vector3(vertexOffset, 0, vertexOffset) + cellOffset + gridOffset;

                triangles[t] = v;
                triangles[t + 1] = triangles[t + 4] = v + 1;
                triangles[t + 2] = triangles[t + 3] = v + 2;
                triangles[t + 5] = v + 3;

                v += 4;
                t += 6;
            }
        }
       

    }
    void MakeContinuesProceduralGrid()
    {
        vertices = new Vector3[(gridSize + 1) * (gridSize + 1)];
        triangles = new int[gridSize * gridSize * 6];
        float vertexOffset = cellSize * 0.5f;
        int v = 0;
        int t = 0;

        for (int x = 0; x <= gridSize; x++)
        {
            for (int y = 0; y <= gridSize; y++)
            {
                vertices[v] = new Vector3((x * cellSize) - vertexOffset, 0, (y * cellSize) - vertexOffset);
                v++;
            }
        }
        v = 0;
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                triangles[t] = v;
                triangles[t + 1] = triangles[t + 4] = v + 1;
                triangles[t + 2] = triangles[t + 3] = v + (gridSize + 1);
                triangles[t + 5] = v + (gridSize + 2);

                v++;
                t += 6;

            }
            v++;
        }
    }
    void UpdateMesh ()
    {
        mesh.Clear ();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
       
        mesh.RecalculateNormals();
    }

}
