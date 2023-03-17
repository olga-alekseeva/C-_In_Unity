using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oliasca.Maze
{
    public sealed class Maze
    {
        public enum ElementType { Cell, VerticalWall, HorizontalWall, WallCross, None}

        public int width { get; private set; }
        public int height { get; private set; }
        public int mapWidth { get; private set; }
        public int mapHeight { get; private set; }
        public ElementType[,] map { get; private set; }

        public Maze()
        {
            MazeSettings mazeSettings = new MazeSettings();
            width = mazeSettings.mazeWidth;
            height = mazeSettings.mazeHeight;
            mapWidth = width * 2 + 1;
            mapHeight = height * 2 + 1;
            map = new ElementType[mapWidth, mapHeight];
        }
    }
}
