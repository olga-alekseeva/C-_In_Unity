using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oliasca.Maze
{
    [Serializable]
    public sealed class PlayerSaveData
    {
        public float speed;
        public int score;

        public PlayerSaveData()
        {

        }
        public PlayerSaveData(IPlayer player)
        {
            speed = player.speed;
            score = player.score;
        }
    }
}
