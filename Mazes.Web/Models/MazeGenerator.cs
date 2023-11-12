namespace Mazes.Web.Models
{
    public class MazeGenerator
    {
        private Random random;

        public MazeGenerator()
        {
            random = new Random();
        }

        public Maze GenerateMaze(int size)
        {
            Cell[,] maze = new Cell[size, size];

            // Initialize maze with walls
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

            // Start DFS from a upper left corner
            DFS(maze, 0, 0);

            return new Maze
            {
                Id = Guid.NewGuid().ToString(),
                Size = size,
                Board = maze.Cast<Cell>().ToArray()
            };
        }

        private void DFS(Cell[,] maze, int startX, int startY)
        {
            maze[startX, startY] = new Cell
            {
                X = startX,
                Y = startY,
                IsRightActive = true,
                IsLeftActive = true,
                IsUpperActive = true,
                IsLowerActive = true
            };

            // Define the order in which neighboring cells are visited
            int[] directions = { 0, 1, 2, 3 };
            Shuffle(directions);

            foreach (var direction in directions)
            {
                int newX = startX + DX[direction];
                int newY = startY + DY[direction];

                if (IsInBounds(newX, newY, maze) && maze[newX, newY] == null)
                {
                    // Carve a passage
                    switch (direction)
                    {
                        case 0:
                            maze[startX, startY].IsUpperActive = false;
                            maze[newX, newY].IsLowerActive = false;
                            break;
                        case 1:
                            maze[startX, startY].IsRightActive = false;
                            maze[newX, newY].IsLeftActive = false;
                            break;
                        case 2:
                            maze[startX, startY].IsLowerActive = false;
                            maze[newX, newY].IsUpperActive = false;
                            break;
                        case 3:
                            maze[startX, startY].IsLeftActive = false;
                            maze[newX, newY].IsRightActive = false;
                            break;
                    }

                    DFS(maze, newX, newY);
                }
            }
        }

        // Helper method to check if a cell is within bounds
        private bool IsInBounds(int x, int y, Cell[,] maze)
        {
            var width = maze.GetLength(0);
            var length = maze.GetLength(1);
            return x >= 0 && x < width && y >= 0 && y < length;
        }

        // Helper method to shuffle the directions array
        private void Shuffle(int[] array)
        {
            for (int i = array.Length - 1; i > 0; i--)
            {
                int j = random.Next(0, i + 1);
                int temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }
        }

        // Define directions for moving up, right, down, and left
        private int[] DX = { 0, 1, 0, -1 };
        private int[] DY = { -1, 0, 1, 0 };
    }
}
