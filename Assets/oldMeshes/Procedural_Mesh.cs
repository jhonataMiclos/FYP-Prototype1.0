﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter))]
public class Procedural_Mesh : MonoBehaviour {

    Mesh mesh;
    Vector3[] vertices;
    int[] triagles;
    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }
	// Use this for initialization
	void Start () {
        MakeMeshData();
        CreateMesh();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void MakeMeshData()
    {
        vertices = new Vector3[] { new Vector3(0,0,0), new Vector3(0,0,1),new Vector3(1,0,0),new Vector3(1,0,1) };
        triagles = new int[] { 0, 1, 2, 2, 1, 3 };

    }
    void CreateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triagles;

        mesh.RecalculateNormals();

    }
}
