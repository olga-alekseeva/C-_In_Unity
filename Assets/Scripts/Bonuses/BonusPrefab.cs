using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oliasca.Maze
{
    public sealed class BonusPrefab
    {
        public GameObject bonus { get; private set; }

        public BonusPrefab()
        {
            bonus = Resources.Load<GameObject>(ResourcesPathes.bonusPrefab);
        }
    }
}
