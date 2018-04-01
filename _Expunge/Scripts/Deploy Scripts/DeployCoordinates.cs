using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralCave
{
    public class DeployCoordinates
    {
        public int Count;
        public List<int> coordX;
        public List<int> coordY;
        public float wallHeight;

        public DeployCoordinates()
        {
            // Vector2 would have worked better
            coordX = new List<int>();
            coordY = new List<int>();
        }

        public void Add(int x, int y)
        {
            coordX.Add(x);
            coordY.Add(y);

            Count = coordX.Count;
        }
    }
}
