using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oliasca.Maze
{
    public interface IBonus
    {
        public float x { get; set; }
        public float y { get; set; }

        public BonusType bonusType { get; set; }

        public int score { get; set; }
        public float time { get; set; }
    }
}
