using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class VoxelRender : MonoBehaviour {


    Mesh mesh;
    MeshCollider collider;
    CutCube cubeCutter;
    List<Vector3> vertices;
    List<int> triangles;
    VoxelData voxelData = new VoxelData();
    public int blockDim = 3;
    int value = 0;
    int fCount = 0;
    public float scale = 1f;

    public float adjustedScale;

    // Use this for initialization
    void Awake()
    {
        adjustedScale = scale * 0.5f;
        mesh = GetComponent<MeshFilter>().mesh;
        collider = GetComponent<MeshCollider>();
        cubeCutter = GetComponent<CutCube>();
    }

    // Update is called once per frame
    void Start()
    {
        voxelData.GenerateData(blockDim);
        GenerateVoxelMesh(voxelData);
        UpdateMesh();
    }
   
    void GenerateVoxelMesh(VoxelData data)
    {
        vertices = new List<Vector3>();
        triangles = new List<int>();
        for (int y = 0; y < data.Hieght; y++)
        {
            for (int z = 0; z < data.Depth; z++)
            {
                for (int x = 0; x < data.Width; x++)
                {
                    if (data.GetCell(y, x, z) == 0)
                    {
                        continue;
                    }
                    MakeCube(adjustedScale, new Vector3((float)x * scale, (float)y * scale, (float)z * scale), y , x, z);
                }
            }
        }
        
    }

    void MakeCube(float cubeScale, Vector3 cubePos, int y, int x, int z)
    {
        
        
        for (int i = 0; i < 6; i++)
        {
            if (voxelData.GetNeighbor(y, x, z, (Direction)i) == 0)
                MakeFace((Direction)i, cubeScale, cubePos);
        }
    }
    void MakeFace(Direction dir, float faceScale, Vector3 facePos)
    {
        vertices.AddRange(CubeMeshData.faceVertices(dir, faceScale, facePos));
        int vCount = vertices.Count;

        triangles.Add(vCount - 4);
        triangles.Add(vCount - 3);
        triangles.Add(vCount - 2);
        triangles.Add(vCount - 4);
        triangles.Add(vCount - 2);
        triangles.Add(vCount - 1);

    }
    public float AjustedScale
    {
        get { return adjustedScale; }
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000.0f))
            {
                Vector3 index = cubeCutter.FindIndex(hit.point, hit.normal);
                voxelData.SetCell((int)index.y, (int)index.x, (int)index.z, 0);
                GenerateVoxelMesh(voxelData);
                UpdateMesh();
            }
        }
        /*if (fCount == 100)
        {


            voxelData.SetCell((blockDim -1) , 1, 1, value);
            GenerateVoxelMesh(voxelData);
            UpdateMesh();
            if (value == 0)
            {
                value = 1;
            }
            else
            {
                value = 0;
            }
            fCount = 0;
        }
        fCount++;*/
    }
    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        collider.sharedMesh = mesh;
        mesh.RecalculateNormals();
    }
}
