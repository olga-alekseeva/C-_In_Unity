using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oliasca.Maze
{
    public sealed class MazePrefabs
    {
        public GameObject wall { get; private set; }
        public GameObject wallCross { get; private set; }
        public GameObject floor { get; private set; }

        public MazePrefabs()
        {
            wall = Resources.Load<GameObject>(ResourcesPathes.wallPrefab);
            wallCross = Resources.Load<GameObject>(ResourcesPathes.wallCrossPrefab);
            floor = Resources.Load<GameObject>(ResourcesPathes.floorPrefab);
        }
    }
}
