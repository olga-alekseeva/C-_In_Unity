using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oliasca.Maze
{
    public interface IPlayer
    {
        public float speed { get; set; }
        public int score { get; set; }
    }
}
