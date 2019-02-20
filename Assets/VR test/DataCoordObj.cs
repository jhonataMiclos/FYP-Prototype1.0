using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCoordObj {

    public struct DataCoordinate
    {
        public int x;
        public int y;
        public int z;

        public DataCoordinate(int x, int y, int z)
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
