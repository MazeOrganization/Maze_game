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
            Dictionary<string, int[]> directions = new Dictionary<string, int[]>();
            directions.Add("Down", new[] { 0, -1 });
            directions.Add("Up", new[] { 0, 1 });
            directions.Add("Left", new[] { -1, 0 });
            directions.Add("Right", new[] { 1, 0 });

            var randDirections = directions.Keys.ToArray();
            Shuffle(randDirections);

            foreach (var move in randDirections)
            {
                int newX = startX + directions[move][0];
                int newY = startY + directions[move][1];

                if (IsInBounds(newX, newY, maze))
                {
                    // Carve a passage
                    switch (move)
                    {
                        case "Up":
                            maze[startX, startY].IsUpperActive = false;
                            maze[newX, newY].IsLowerActive = false;
                            break;
                        case "Right":
                            maze[startX, startY].IsRightActive = false;
                            maze[newX, newY].IsLeftActive = false;
                            break;
                        case "Down":
                            maze[startX, startY].IsLowerActive = false;
                            maze[newX, newY].IsUpperActive = false;
                            break;
                        case "Left":
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
            return x >= 0 && x < Size && y >= 0 && y < Size;
        }

        // Helper method to shuffle the directions array
        private void Shuffle(string[] array)
        {
            for (int i = array.Length - 1; i > 0; i--)
            {
                int j = random.Next(0, i + 1);
                string temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }
        }
    }
}
