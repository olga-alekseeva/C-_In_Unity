using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oliasca.Maze
{
    public sealed class MazeSettings
    {
        public float cellWidth { get; }
        public float cellHeight { get; }

        public int mazeWidth { get; }
        public int mazeHeight { get; }

        public float mazeWallThickness { get; }
        public float mazeWallCrossSize { get; }
        public float mazeWallCrossHeight { get; }

        public float mazeWallHeight { get; }

        public MazeSettings()
        {
            cellWidth = 3f;
            cellHeight = 3f;
            mazeWidth = 10;
            mazeHeight = 10;
            mazeWallThickness = 0.3f;
            mazeWallCrossSize = 0.5f;
            mazeWallHeight = 1f;
            mazeWallCrossHeight = 1.1f;
        }
    }
}
