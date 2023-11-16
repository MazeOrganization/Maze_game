using Microsoft.IdentityModel.Tokens;
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

            Stack<Cell> stack = new Stack<Cell>();
            List<Cell> visited = new List<Cell>();
            stack.Push(maze[0, 0]);
            visited.Add(maze[0, 0]);

            while (!stack.IsNullOrEmpty())
            {
                var curCell = stack.Pop();

                var nextCell = GetRandomNeighbor(curCell, maze, visited);
                if (curCell == nextCell) continue;


                ConnectCells(curCell, nextCell);

                visited.Add(nextCell);
                stack.Push(nextCell);
            }

            return new Maze
            {
                Id = Guid.NewGuid().ToString(),
                Size = size,
                Board = maze.Cast<Cell>().ToArray()
            };
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

        private Cell GetRandomNeighbor(Cell cell, Cell[,] maze, List<Cell> visited)
        {
            (int, int)[] directions = { (0, 1), (1, 0), (0, -1), (-1, 0)};

            Random random = new Random();
            Shuffle(directions, random);

            foreach (var direction in directions)
            {
                if (IsInBounds(cell.X + direction.Item1,
                               cell.Y + direction.Item2,
                               maze.GetLength(0)) 
                && !visited.Contains(maze[cell.X + direction.Item1, 
                                     cell.Y + direction.Item2]))
                {
                    return maze[cell.X + direction.Item1, 
                                cell.Y + direction.Item2];
                }
            }

            return cell;
        }

        private void Shuffle<T>(T[] array, Random random)
        {
            for (int i = array.Length - 1; i > 0; i--)
            {
                int j = random.Next(0, i + 1);
                T temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }
        }

        private bool IsInBounds(int x, int y, int size)
        {
            return x >= 0 && x < size && y >= 0 && y < size;
        }
    }
}