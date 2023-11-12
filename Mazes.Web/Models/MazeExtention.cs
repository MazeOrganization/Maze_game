namespace Mazes.Web.Models
{
    public static class MazeExtensions
    {
        public static MazeFront ToFrontModel(this Maze maze)
        {
            var board = maze.Board;
            var newBoard = new Cell[maze.Size][];

            for(var i = 0; i < maze.Size; i++)
            {
                newBoard[i] = maze.Board.Where(x => x.Y == i).OrderBy(x => x.Y).ToArray();
            }

            return new MazeFront(maze.Id, newBoard);
        }
    }
} 
