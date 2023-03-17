using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oliasca.Maze
{
    public interface IFixedUpdate : IUpdatable
    {
        public void FixedUpdate(float fixedDeltatime);
    }
}
