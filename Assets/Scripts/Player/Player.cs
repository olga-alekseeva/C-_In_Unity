using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oliasca.Maze
{
    public sealed class Player : IPlayer
    {
        public float speed { get ; set ; }
        public int score { get; set; }

        public Player()
        {
            speed = 1f;
            score = 0;
        }
        public Player(float speed, int score)
        {
            this.speed = speed;
            this.score = score;
        }
    }
}
