using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oliasca.Maze
{
    [Serializable]
    public sealed class BonusSaveList
    {
        public List<BonusSaveData> list;

        public BonusSaveList()
        {
            list = new List<BonusSaveData>();
        }
    }
}
