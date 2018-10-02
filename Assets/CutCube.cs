using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutCube : MonoBehaviour {
    public float x;
    public float y;
    public float z;
    public Vector3 normal;
    public Vector3 raw_coord;
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
        if ( CheckNormal(normal,arrayOffsets.offsets[(int)Direction.South]) || CheckNormal(normal, arrayOffsets.offsets[(int)Direction.West]) || CheckNormal(normal, arrayOffsets.offsets[(int)Direction.Down]))
        {
            
            this.x = Mathf.Floor(Mathf.Abs((point.x + offset) / scale));
            this.y = Mathf.Floor(Mathf.Abs((point.y + offset) / scale));
            this.z = Mathf.Floor(Mathf.Abs((point.z + offset) / scale));
            //  this.x += 0.5f ;
            // this.y += 0.5f;
            // this.z += 0.5f;
        }
        else if ( CheckNormal(normal, arrayOffsets.offsets[(int)Direction.North]) || CheckNormal(normal, arrayOffsets.offsets[(int)Direction.East]) || CheckNormal(normal, arrayOffsets.offsets[(int)Direction.Up]))
        {
            //this.raw_coord = point;
            //this.normal = normal;
            this.x = Mathf.Floor(Mathf.Abs((point.x ) / scale));
            this.y = Mathf.Floor(Mathf.Abs((point.y ) / scale));
            this.z = Mathf.Floor(Mathf.Abs((point.z ) / scale));
            //  this.x += 0.5f ;
            // this.y += 0.5f;
            // this.z += 0.5f;
        }
        
        return new Vector3(this.x, this.y, this.z);
    }
    // Update is called once per frame
    /*void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit, 1000.0f))
            {
                DeleteCube(hit.point);
            }
        }
		
	}*/
    public bool CheckNormal(Vector3 normal, DataCoordObj.DataCoordinate coord) {
        return (normal.x == coord.x && normal.y == coord.y && normal.z == coord.z);
    }
}
