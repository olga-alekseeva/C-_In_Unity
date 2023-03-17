using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oliasca.Maze
{
    public sealed class CameraPrefab
    {
        public GameObject cameraGameObject { get; private set; }

        public CameraPrefab()
        {
            cameraGameObject = Resources.Load<GameObject>(ResourcesPathes.cameraPrefab);
        }
    }
}
