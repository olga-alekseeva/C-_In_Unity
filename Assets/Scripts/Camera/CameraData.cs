using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oliasca.Maze
{
    public sealed class CameraData : ICameraData
    {
        public float speed { get; set; }

        public CameraData()
        {
            speed = 1f;
        }
    }
}
