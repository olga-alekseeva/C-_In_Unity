using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oliasca.Maze
{
    public interface IUnscaledUpdate : IUpdatable
    {
        public void UnscaledUpdate(float unscaledDeltaTime);
    }
}
