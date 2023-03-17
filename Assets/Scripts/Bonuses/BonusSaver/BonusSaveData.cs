using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oliasca.Maze
{
    [Serializable]
    public sealed class BonusSaveData
    {
        public float x;
        public float y;
        public BonusType bonusType;
        public int score;
        public float time;

        public BonusSaveData(IBonus bonus)
        {
            x = bonus.x;
            y = bonus.y;
            bonusType = bonus.bonusType;
            score = bonus.score;
            time = bonus.time;
        }

        public Bonus GetBonus()
        {
            Bonus bonus = new Bonus();
            bonus.x = x;
            bonus.y = y;
            bonus.bonusType = bonusType;
            bonus.score = score;
            bonus.time = time;

            return bonus;
        }
    }
}
