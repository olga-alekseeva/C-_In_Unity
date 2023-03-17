using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oliasca.Maze
{
    public sealed class MazeController
    {
        private Maze maze;
        private MazeSettings mazeSettings;

        public MazeController(Maze maze)
        {
            this.maze = maze;
            mazeSettings = new MazeSettings();
        }

        public void Generate()
        {
            MazeGenerator mazeGenerator = new MazeGenerator(maze);
            mazeGenerator.Generate();
        }

        private Vector3 GetWorldPosition(int x, int y)
        {
            return new Vector3((float)x * mazeSettings.cellWidth / 2f, 0f, (float)y * mazeSettings.cellHeight / 2f);
        }
        public void ShowMaze()
        {
            MazePrefabs mazePrefabs = new MazePrefabs();

            GameObject parent = new GameObject("Maze");

            for (int x = 0; x < maze.mapWidth; x++)
            {
                for (int y = 0; y < maze.mapHeight; y++)
                {
                    Vector3 position = GetWorldPosition(x, y);
                    GameObject element = null;
                    
                    if (maze.map[x, y] == Maze.ElementType.Cell)
                    {
                        element = GameObject.Instantiate(mazePrefabs.floor, position, Quaternion.identity);
                        element.transform.localScale = new Vector3(mazeSettings.cellWidth, 1f, mazeSettings.cellHeight);
                    }
                    if (maze.map[x, y] == Maze.ElementType.HorizontalWall)
                    {
                        element = GameObject.Instantiate(mazePrefabs.wall, position, Quaternion.identity);
                        element.transform.localScale = new Vector3(mazeSettings.cellWidth, mazeSettings.mazeWallHeight, mazeSettings.mazeWallThickness);
                    }
                    if (maze.map[x, y] == Maze.ElementType.VerticalWall)
                    {
                        element = GameObject.Instantiate(mazePrefabs.wall, position, Quaternion.identity);
                        element.transform.localScale = new Vector3(mazeSettings.mazeWallThickness, mazeSettings.mazeWallHeight, mazeSettings.cellHeight);
                    }
                    if (maze.map[x, y] == Maze.ElementType.WallCross)
                    {
                        element = GameObject.Instantiate(mazePrefabs.wallCross, position, Quaternion.identity);
                        element.transform.localScale = new Vector3(mazeSettings.mazeWallCrossSize, mazeSettings.mazeWallCrossHeight, mazeSettings.mazeWallCrossSize);
                    }

                    element?.transform.SetParent(parent.transform);
                }
            }
        }
    }
}
