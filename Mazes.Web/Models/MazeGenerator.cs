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

            // Generate maze
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    var brokenWallDirection = GetRandomDirection(x, y, size);

                    int newX = x + brokenWallDirection.Item1;
                    int newY = y + brokenWallDirection.Item2;

                    // Ensure that walls are not removed from the exit
                    if (x != size - 1 || y != size - 1)
                    {
                        // Ensure that the new position is within bounds
                        if (IsInBounds(newX, newY, size))
                        {
                            if (brokenWallDirection == (0, 1))
                            {
                                maze[x, y].IsLowerActive = false;
                                maze[newX, newY].IsUpperActive = false;
                            }
                            else
                            {
                                maze[x, y].IsRightActive = false;
                                maze[newX, newY].IsLeftActive = false;
                            }
                        }
                    }
                }
            }

            // Validate maze by checking for a path from (0,0) to (size-1, size-1)
            if (!HasPath(maze, size))
            {
                // Regenerate the maze if there is no valid path
                return GenerateMaze(size);
            }

            return new Maze
            {
                Id = Guid.NewGuid().ToString(),
                Size = size,
                Board = maze.Cast<Cell>().ToArray()
            };
        }

        private bool HasPath(Cell[,] maze, int size)
        {
            bool[,] visited = new bool[size, size];
            return DepthFirstSearch(maze, 0, 0, size - 1, size - 1, visited);
        }

        private bool DepthFirstSearch(Cell[,] maze, int x, int y, int targetX, int targetY, bool[,] visited)
        {
            if (x == targetX && y == targetY)
                return true;

            visited[x, y] = true;

            // Check neighbors
            foreach (var direction in new[] { (0, 1), (1, 0), (0, -1), (-1, 0) })
            {
                int newX = x + direction.Item1;
                int newY = y + direction.Item2;

                if (IsInBounds(newX, newY, maze.GetLength(0)) && !visited[newX, newY] &&
                    HasWall(direction, maze[x, y]) == false &&
                    DepthFirstSearch(maze, newX, newY, targetX, targetY, visited))
                {
                    return true;
                }
            }

            return false;
        }
        public static bool HasWall((int, int) direction, Cell cell)
        {
            int dx = direction.Item1;
            int dy = direction.Item2;

            // Check the appropriate wall based on the direction
            if (dx == 0 && dy == 1)
            {
                // Check lower wall
                return cell.IsLowerActive;
            }
            else if (dx == 1 && dy == 0)
            {
                // Check right wall
                return cell.IsRightActive;
            }
            else if (dx == 0 && dy == -1)
            {
                // Check upper wall
                return cell.IsUpperActive;
            }
            else if (dx == -1 && dy == 0)
            {
                // Check left wall
                return cell.IsLeftActive;
            }

            // Invalid direction, return false
            return false;
        }
        private bool IsInBounds(int x, int y, int size)
        {
            return x >= 0 && x < size && y >= 0 && y < size;
        }

        private (int, int) GetRandomDirection(int x, int y, int size)
        {
            (int, int)[] directions = { (0, 1), (1, 0) };

            Random random = new Random();
            Shuffle(directions, random);

            foreach (var next in directions)
            {
                if (IsInBounds(x + next.Item1, y + next.Item2, size))
                {
                    return next;
                }
            }

            return (0, 0);
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
    }
}
    //private Random random;

    //public MazeGenerator()
    //{
    //    random = new Random();
    //}

    //public Maze GenerateMaze(int size)
    //{
    //    Cell[,] maze = new Cell[size, size];
    //    HashSet<(int,int)> visited = new HashSet<(int, int)>();

    //    // Initialize maze with walls
    //    for (int i = 0; i < size; i++)
    //    {
    //        for (int j = 0; j < size; j++)
    //        {
    //            maze[i, j] = new Cell
    //            {
    //                X = i,
    //                Y = j,
    //                IsRightActive = true,
    //                IsLeftActive = true,
    //                IsUpperActive = true,
    //                IsLowerActive = true
    //            };
    //        }
    //    }

    //    // Start DFS from a upper left corner
    //    DFS(maze, 0, 0, visited);

    //    return new Maze
    //    {
    //        Id = Guid.NewGuid().ToString(),
    //        Size = size,
    //        Board = maze.Cast<Cell>().ToArray()
    //    };
    //}

    //private void DFS(Cell[,] maze, int startX, int startY, HashSet<(int,int)> visited )
    //{
    //    if (startX == maze.GetLength(0) - 1 && startY == maze.GetLength(1) - 1)
    //    {
    //        visited.Add((startX, startY));
    //        return;
    //    }

    //    // Define the order in which neighboring cells are visited
    //    Dictionary<string, int[]> directions = new Dictionary<string, int[]>();
    //    directions.Add("Down", new[] { 0, 1 });
    //    directions.Add("Up", new[] { 0, -1 });
    //    directions.Add("Left", new[] { -1, 0 });
    //    directions.Add("Right", new[] { 1, 0 });

    //    var randDirections = directions.Keys.ToArray();
    //    Shuffle(randDirections);

    //    foreach (var move in randDirections)
    //    {
    //        int newX = startX + directions[move][0];
    //        int newY = startY + directions[move][1];

    //        if (IsInBounds(newX, newY, maze) && !visited.Contains((newX, newY)))
    //        {
    //            // Carve a passage
    //            switch (move)
    //            {
    //                case "Up":
    //                    maze[startX, startY].IsUpperActive = false;
    //                    maze[newX, newY].IsLowerActive = false;
    //                    break;
    //                case "Right":
    //                    maze[startX, startY].IsRightActive = false;
    //                    maze[newX, newY].IsLeftActive = false;
    //                    break;
    //                case "Down":
    //                    maze[startX, startY].IsLowerActive = false;
    //                    maze[newX, newY].IsUpperActive = false;
    //                    break;
    //                case "Left":
    //                    maze[startX, startY].IsLeftActive = false;
    //                    maze[newX, newY].IsRightActive = false;
    //                    break;
    //            }
    //            visited.Add((startX, startY));
    //            DFS(maze, newX, newY, visited);
    //        }
    //    }
    //}

    //// Helper method to check if a cell is within bounds
    //private bool IsInBounds(int x, int y, Cell[,] maze)
    //{
    //    return x >= 0 && x < maze.GetLength(0) && y >= 0 && y < maze.GetLength(1);
    //}

    //// Helper method to shuffle the directions array
    //private void Shuffle(string[] array)
    //{
    //    for (int i = array.Length - 1; i > 0; i--)
    //    {
    //        int j = random.Next(0, i + 1);
    //        string temp = array[i];
    //        array[i] = array[j];
    //        array[j] = temp;
    //    }
    //}
