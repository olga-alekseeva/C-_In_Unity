using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oliasca.Maze
{
    public interface IProfileController
    {
        public IPlayerProfile playerProfile { get; set; }
        public Player player { get; set; }

        public void Save();

        public void Load();
    }
}
