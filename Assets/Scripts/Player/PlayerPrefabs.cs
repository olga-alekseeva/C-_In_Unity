using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oliasca.Maze
{
    public sealed class PlayerPrefabs
    {
        public GameObject player { get; private set; }
        public PlayerPrefabs()
        {
            player = Resources.Load<GameObject>(ResourcesPathes.playerPrefab);
        }
    }
}
