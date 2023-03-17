using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oliasca.Maze
{
    public sealed class BonusMatch
    {
        public IBonus bonus;
        public GameObject gameObject;
        public bool visible;

        public BonusMatch(IBonus bonus, GameObject gameObject)
        {
            this.bonus = bonus;
            this.gameObject = gameObject;
            visible = false;
        }
    }
}
