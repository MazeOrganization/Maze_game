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

            HashSet<(int, int)> visited = new HashSet<(int, int)>();
            Stack<Cell> stack = new Stack<Cell>();

            var curCell = maze[0, 0];
            visited.Add((curCell.X,curCell.Y));

            while (visited.Count != maze.Length)
            {
                var neighbours = GetUnvisitedNeighbors(curCell, visited, size);
                if (neighbours.Count() != 0)
                {
                    var neighbourCell = FindRandomNeighbour(neighbours, maze);
                    stack.Push(neighbourCell);
                    ConnectCells(curCell, neighbourCell);
                    curCell = neighbourCell;
                    visited.Add((curCell.X, curCell.Y));
                }
                else if (stack.Count() > 0) 
                { 
                    curCell = stack.Pop();
                }
                else
                {
                    var unvisitedCells = GetUnvisitedCells(visited, maze);
                    var rand = new Random();
                    curCell = unvisitedCells[rand.Next(0, unvisitedCells.Count())];
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

        private List<(int, int)> GetUnvisitedNeighbors(Cell cell, HashSet<(int, int)> visited, int size)
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

        private List<Cell> GetUnvisitedCells(HashSet<(int,int)> visited, Cell[,] maze)
        {
            List<Cell> unvisitedCells = new List<Cell>();
            foreach (var cell in maze)
            {
                if(!visited.Contains((cell.X,cell.Y)))
                {
                    unvisitedCells.Add(cell);
                }
            }
            return unvisitedCells;
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