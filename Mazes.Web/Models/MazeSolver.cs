
using System.Collections.Generic;
using System.ComponentModel;

namespace Mazes.Web.Models
{
    public class MazeSolver
    {
        public Cell[] GetMazeSolution(Cell[,] maze)
        {
            Stack<Cell> path = new Stack<Cell>();
            var visited = new List<(int, int)>();

            Cell curCell = maze[0, 0];
            visited.Add((curCell.X, curCell.Y));
            path.Push(curCell);

            while (curCell != maze[maze.GetLength(0) - 1, maze.GetLength(1) - 1])
            {
                var nextCell = FindNextCell(curCell, maze, visited);

                if (nextCell == null)
                {
                    GoBackToLastIntersection(maze, visited, path);
                    nextCell = FindNextCell(path.First(), maze, visited);
                }

                visited.Add((nextCell.X, nextCell.Y));
                path.Push(nextCell);
                curCell = nextCell;
            }
            return path.Reverse().ToArray();
        }

        private void GoBackToLastIntersection(Cell[,] maze, List<(int, int)> visited, Stack<Cell> path)
        {
            while (path.Count > 0)
            {
                Cell curCell = path.Pop();
                Cell nextCell = FindNextCell(curCell, maze, visited);

                if (nextCell != null)
                {
                    path.Push(curCell);
                    break;
                }
            }
        }
        private Cell FindNextCell(Cell curCell, Cell[,] maze, List<(int, int)> visited)
        {
            (int, int)[] prioritedDirections = { (1, 0), (0, 1), (-1, 0), (0, -1) };

            foreach (var direction in prioritedDirections)
            {
                if (!IsInBounds(curCell.X + direction.Item1,
                               curCell.Y + direction.Item2,
                               maze.GetLength(0)))
                {
                    continue;
                }
                var next = maze[curCell.X + direction.Item1, curCell.Y + direction.Item2];

                switch (direction)
                {
                    case (1, 0):
                        if (!curCell.IsRightActive
                            && !next.IsLeftActive
                            && !visited.Contains((next.X, next.Y)))
                        {
                            return next;
                        }
                        break;
                    case (0, 1):
                        if (!curCell.IsLowerActive
                            && !next.IsUpperActive
                            && !visited.Contains((next.X, next.Y)))
                        {
                            return next;
                        }
                        break;
                    case (-1, 0):
                        if (!curCell.IsLeftActive
                            && !next.IsRightActive
                            && !visited.Contains((next.X, next.Y)))
                        {
                            return next;
                        }
                        break;
                    case (0, -1):
                        if (!curCell.IsUpperActive
                            && !next.IsLowerActive
                            && !visited.Contains((next.X, next.Y)))
                        {
                            return next;
                        }
                        break;
                }
            }

            return null;
        }
        private bool IsInBounds(int x, int y, int size)
        {
            return x >= 0 && x < size && y >= 0 && y < size;
        }
    }
}
