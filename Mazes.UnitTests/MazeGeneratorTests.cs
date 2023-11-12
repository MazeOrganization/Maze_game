using Mazes.Web.Models;

namespace Mazes.UnitTests
{
    public class MazeGeneratorTests
    {
        private MazeGenerator generator = null!; 

        [SetUp]
        public void Setup()
        {
            generator = new MazeGenerator();
        }

        [Test]
        public void Maze_BoardIsCorrectSize()
        {
            var maze = generator.GenerateMaze(2);
            var board = maze.Board;

            foreach (var cell in board)
            {
                Assert.IsTrue(cell.X >= 0 && cell.X < maze.Size);
                Assert.IsTrue(cell.Y >= 0 && cell.Y < maze.Size);
            }
        }

        [Test]
        public void Maze_HasPathFromStartToFinish()
        {
            var maze = generator.GenerateMaze(3);

            var hasPath = HasPath(maze, (0, 0), (maze.Size - 1, maze.Size - 1));

            Assert.IsTrue(hasPath);
        }

        private static bool HasPath(Maze maze, (int, int) start, (int, int) finish)
        {
            var stack = new Stack<(int, int)>();
            var visited = new HashSet<(int, int)>();

            stack.Push(start);

            while (stack.Count > 0)
            {
                var current = stack.Pop();
                int x = current.Item1;
                int y = current.Item2;

                if (x == finish.Item1 && y == finish.Item2)
                {
                    return true;
                }

                if (x >= 0 && x < maze.Size && y >= 0 && y < maze.Size && !visited.Contains((x, y)))
                {
                    visited.Add((x, y));

                    var currentCell = maze.Board[y * maze.Size + x];

                    if (!currentCell.IsRightActive)
                    {
                        stack.Push((x + 1, y));
                    }

                    if (!currentCell.IsLeftActive)
                    {
                        stack.Push((x - 1, y));
                    }

                    if (!currentCell.IsUpperActive)
                    {
                        stack.Push((x, y - 1));
                    }

                    if (!currentCell.IsLowerActive)
                    {
                        stack.Push((x, y + 1));
                    }
                }
            }

            return false;
        }
    }
}