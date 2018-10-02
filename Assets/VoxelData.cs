using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelData {

    int[,,] data;
    
      
    public void GenerateData(int val)
    {
        data = new int[val, val, val];
        for (int y = 0; y < val; y++)
        {
            for (int x = 0; x < val; x++)
            {
                for (int z = 0; z < val; z++)
                {
                    data[y, x, z] = 1;
                }
            }
        }
    }
    public int Hieght
    {
        get { return data.GetLength(0); }
    }
    public int Width
    {
       get { return data.GetLength(1); }
    }
    public int Depth
    {
        get { return data.GetLength(2); }
    }
    public int GetCell (int y,int x, int z)
    {
        return data[y, x, z];
    }
    public void SetCell (int y, int x, int z, int val)
    {
        data[y, x, z] = val;
    }
    public int GetNeighbor (int y, int x, int z, Direction dir)
    {
        DataCoordinate offsetToCheck = offsets[(int)dir];
        DataCoordinate neighborCoord = new DataCoordinate(x + offsetToCheck.x, y + offsetToCheck.y, z + offsetToCheck.z);
        
        if (neighborCoord.x < 0 || neighborCoord.x >= Width || neighborCoord.y < 0 || neighborCoord.y >= Hieght || neighborCoord.z < 0 || neighborCoord.z >= Depth)
        {
            return 0;
        }
        else
        {
            return GetCell(neighborCoord.y, neighborCoord.x, neighborCoord.z);
        }
    }
    public struct DataCoordinate
    {
        public int x;
        public int y;
        public int z;

        public DataCoordinate (int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
    public DataCoordinate[] offsets =
    {
        new DataCoordinate( 0, 0, 1),
        new DataCoordinate( 1, 0, 0),
        new DataCoordinate( 0, 0,-1),
        new DataCoordinate(-1, 0, 0),
        new DataCoordinate( 0, 1, 0),
        new DataCoordinate( 0, -1,0)
    };
}

public enum Direction
{
    North,
    East,
    South,
    West,
    Up,
    Down
}