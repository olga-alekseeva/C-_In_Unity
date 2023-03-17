using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oliasca.Maze
{
    public sealed class MazeGenerator
    {
        private Maze maze;

        private struct Cell
        {
            public int x;
            public int y;

            public Cell(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        public MazeGenerator(Maze maze)
        {
            this.maze = maze;
        }

        public void Generate()
        {
            ResetMaze();

            bool[,] arrayReachableCells = GetStartArrayReachableCells();
            List<Cell> nonReachableCells = GetStartListNonReachableCells();

            //ѕерва€ точка
            arrayReachableCells[0, 0] = true;
            nonReachableCells.Remove(new Cell(0, 0));

            while (nonReachableCells.Count > 0)
            {
                //—лучайна€ недоступна€ €чейка
                Cell cell = nonReachableCells[Random.Range(0, nonReachableCells.Count)];

                //—писок доступных соседних €чеек
                List<Cell> listNeighbourReachableCells = GetNeighbourReachableCells(arrayReachableCells, cell);
                if (listNeighbourReachableCells.Count == 0) continue;

                //ѕометим текущую недоступную €чейку доступной
                nonReachableCells.Remove(cell);
                arrayReachableCells[cell.x, cell.y] = true;

                //—лучайна€ доступна€ соседн€€ €чейка
                Cell neighbourCell = listNeighbourReachableCells[Random.Range(0, listNeighbourReachableCells.Count)];

                //”берем между ними стену. —редн€€ координата между €чеками лабиринта
                int wallX = ((cell.x * 2 + 1) + (neighbourCell.x * 2 + 1)) / 2;
                int wallY = ((cell.y * 2 + 1) + (neighbourCell.y * 2 + 1)) / 2;
                
                maze.map[wallX, wallY] = Maze.ElementType.None;
            }
        }

        private List<Cell> GetNeighbourReachableCells(bool[,] arrayReachableCells, Cell cell)
        {
            List<Cell> listReachableCells = new List<Cell>();
            if (IsCorrect(cell.x - 1, cell.y) && arrayReachableCells[cell.x - 1, cell.y]) listReachableCells.Add(new Cell(cell.x - 1, cell.y));
            if (IsCorrect(cell.x + 1, cell.y) && arrayReachableCells[cell.x + 1, cell.y]) listReachableCells.Add(new Cell(cell.x + 1, cell.y));
            if (IsCorrect(cell.x, cell.y - 1) && arrayReachableCells[cell.x, cell.y - 1]) listReachableCells.Add(new Cell(cell.x, cell.y - 1));
            if (IsCorrect(cell.x, cell.y + 1) && arrayReachableCells[cell.x, cell.y + 1]) listReachableCells.Add(new Cell(cell.x, cell.y + 1));
            return listReachableCells;
        }

        private bool IsCorrect(int x, int y)
        {
            return x >= 0 && x < maze.width && y >= 0 && y < maze.height;
        }
        private List<Cell> GetStartListNonReachableCells()
        {
            List<Cell> nonReachableCells = new List<Cell>();

            for (int x = 0; x < maze.width; x++)
            {
                for (int y = 0; y < maze.height; y++)
                {
                    nonReachableCells.Add(new Cell(x, y));
                }
            }
            return nonReachableCells;
        }

        private bool[,] GetStartArrayReachableCells()
        {
            bool[,] reachableCells = new bool[maze.width, maze.height];
            for (int x = 0; x < maze.width; x++)
            {
                for (int y = 0; y < maze.height; y++)
                {
                    reachableCells[x, y] = false;
                }
            }
            return reachableCells;
        }

        private bool IsEven(int value)
        {
            return ((value % 2) == 0);
        }

        private void ResetMaze()
        {
            int sizeX = maze.mapWidth;
            int sizeY = maze.mapHeight;
            Maze.ElementType[,] map = maze.map;

            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    if (IsEven(x))
                    {
                        if (IsEven(y))
                        {
                            map[x, y] = Maze.ElementType.WallCross;
                        }
                        else
                        {
                            map[x, y] = Maze.ElementType.VerticalWall;
                        }
                    }
                    else
                    {
                        if (IsEven(y))
                        {
                            map[x, y] = Maze.ElementType.HorizontalWall;
                        }
                        else
                        {
                            map[x, y] = Maze.ElementType.Cell;
                        }
                    }
                }
            }
        }
    }
}
