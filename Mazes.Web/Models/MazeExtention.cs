namespace Mazes.Web.Models
{
    public static class MazeExtensions
    {
        public static MazeFront ToFrontModel(this Maze maze)
        {
            var board = maze.Board;
            var newBoard = new Cell[maze.Size, maze.Size];

            foreach (var cell in board)
            {
                newBoard[cell.X, cell.Y] = cell;
            }

            return new MazeFront(maze.Id, newBoard);
        }
    }
} 
