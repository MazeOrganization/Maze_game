using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Linq;

namespace Mazes.Web.Models
{
    public class MazeGenerator
    {
        public Maze GenerateMaze(int size)
        {
            Cell[,] maze = new Cell[size, size];
            // Initialize cells
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    maze[i, j] = new Cell
                    {
                        X = i,
                        Y = j,
                        IsRightActive = true,
                        IsLeftActive = true,
                        IsUpperActive = true,
                        IsLowerActive = true
                    };
                }
            }

            Stack<Cell> solution = new Stack<Cell>();
            Stack<(int,int)> visited = new Stack<(int,int)>();

            var curCell = maze[0, 0];

            while (visited.Count != maze.Length)
            {
                var neighbours = FindUnvisitedNeighbors(curCell, visited, maze.GetLength(0));

                if (neighbours.Count() == 0)
                {
                    curCell = solution.Pop();
                }
                else
                {
                    visited.Push((curCell.X, curCell.Y));

                    var nextCell = FindRandomNeighbour(neighbours, maze);
                    ConnectCells(curCell, nextCell);

                    curCell = nextCell;
                    visited.Push((curCell.X, curCell.Y));

                    if(!visited.Contains((maze[0, size - 1].X, maze[0, size - 1].Y)))
                    {
                        solution.Push(maze[0, size - 1]);
                    }
                }
            }

            return new Maze
            {
                Id = Guid.NewGuid().ToString(),
                Size = size,
                Board = maze.Cast<Cell>().ToArray()
            };
        }

        private Cell FindRandomNeighbour(List<(int, int)> neighbours, Cell[,] maze)
        {
            Random rng = new Random();
            int n = neighbours.Count;
            var neighCoord = neighbours[rng.Next(n)];
            return maze[neighCoord.Item1, neighCoord.Item2];
        }

        private List<(int,int)> FindUnvisitedNeighbors(Cell cell, Stack<(int,int)> visited, int size)
        {
            (int, int)[] directions = { (0, 1), (1, 0), (0, -1), (-1, 0) };
            var unvisitedNeighbors = new List<(int, int)>();

            foreach (var direction in directions)
            {
                var newX = cell.X + direction.Item1;
                var newY = cell.Y + direction.Item2;

                if (IsInBounds(newX, newY, size)
                && !visited.Contains((newX, newY)))
                {
                    unvisitedNeighbors.Add((newX, newY));
                }
            }
            return unvisitedNeighbors;
        }
        
        private void ConnectCells(Cell currentCell, Cell nextCell)
        {
            var direction = (nextCell.X - currentCell.X, nextCell.Y - currentCell.Y);

            switch (direction)
            {
                case (0, 1):
                    currentCell.IsLowerActive = false;
                    nextCell.IsUpperActive = false;
                    break;
                case (1, 0):
                    currentCell.IsRightActive = false;
                    nextCell.IsLeftActive = false;
                    break;
                case (0, -1):
                    currentCell.IsUpperActive = false;
                    nextCell.IsLowerActive = false;
                    break;
                case (-1, 0):
                    currentCell.IsLeftActive = false;
                    nextCell.IsRightActive = false;
                    break;
            }
        }

        private bool IsInBounds(int x, int y, int size)
        {
            return x >= 0 && x < size && y >= 0 && y < size;
        }
    }
}