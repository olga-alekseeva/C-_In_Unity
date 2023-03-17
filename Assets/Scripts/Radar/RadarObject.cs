using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oliasca.Maze
{
    public sealed class RadarObject
    {
        public GameObject radarObject;
        public GameObject radarImage;

        public RadarObject(GameObject radarObject, GameObject radarImage)
        {
            this.radarObject = radarObject;
            this.radarImage = radarImage;
        }
    }
}
