using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutCube : MonoBehaviour {
    public float x;
    public float y;
    public float z;
    public Vector3 normal;
    public Vector3 raw_coord;
    public Vector3 objPos;
    public DataCoordObj arrayOffsets = new DataCoordObj();
    //Direction dir = new Direction();
    VoxelData data = new VoxelData();
    float scale;
    float max;
    float offset;
	// Use this for initialization
	void Start () {
        VoxelRender voxelRender = GetComponent<VoxelRender>();
        scale = voxelRender.scale;
        max = voxelRender.blockDim;
        offset = voxelRender.adjustedScale;
        

    }
	public Vector3 FindIndex(Vector3 point, Vector3 normal )
    {
        this.raw_coord = point;
        this.normal = normal;
        this.objPos = GameObject.Find("MarbleBlock").transform.position;
         if ( CheckNormal(normal,arrayOffsets.offsets[(int)Direction.South]) || CheckNormal(normal, arrayOffsets.offsets[(int)Direction.West]) || CheckNormal(normal, arrayOffsets.offsets[(int)Direction.Down]))
         {
             this.x = Mathf.Floor(Mathf.Abs(((point.x - objPos.x) + offset) / scale));
             this.y = Mathf.Floor(Mathf.Abs(((point.y - objPos.y) + offset) / scale));
             this.z = Mathf.Floor(Mathf.Abs(((point.z - objPos.z) + offset) / scale));
         }
         else if ( CheckNormal(normal, arrayOffsets.offsets[(int)Direction.North]) || CheckNormal(normal, arrayOffsets.offsets[(int)Direction.East]) || CheckNormal(normal, arrayOffsets.offsets[(int)Direction.Up]))
         {
             this.x = Mathf.Floor(Mathf.Abs(((point.x - objPos.x) - (offset * normal.x)) / scale));
             this.y = Mathf.Floor(Mathf.Abs(((point.y - objPos.y) - (offset * normal.y)) / scale));
             this.z = Mathf.Floor(Mathf.Abs(((point.z - objPos.z) - (offset * normal.z)) / scale));
         }

        return new Vector3(this.x, this.y, this.z);
    }
    public bool CheckNormal(Vector3 normal, DataCoordObj.DataCoordinate coord) {
        return (normal.x == coord.x && normal.y == coord.y && normal.z == coord.z);
    }
}
