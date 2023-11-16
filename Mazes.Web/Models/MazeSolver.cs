
using System.Collections.Generic;
using System.ComponentModel;

namespace Mazes.Web.Models
{
    public class MazeSolver
    {
       public Cell[] GetMazeSolution(Cell[,] maze)
       {
            Stack<Cell> path = new Stack<Cell>();
            var visited = new List<Cell>();

            Cell curCell = maze[0, 0];
            visited.Add(curCell);
            path.Push(curCell);

            while(curCell != maze[maze.GetLength(0) - 1, maze.GetLength(1)-1])
            {
                var nextCell = FindNextCell(curCell, maze, visited);

                if(nextCell == curCell)
                {
                    nextCell = GoBackToLastIntersection(maze, visited, path, curCell);
                }

                visited.Add(nextCell);
                path.Push(nextCell);
                curCell = nextCell;
            }
            return path.ToArray();
       }
        
        private Cell GoBackToLastIntersection(Cell[,] maze, List<Cell> visited, Stack<Cell> path, Cell cell)
        {
            var curCell = cell;
            var nextCell = curCell;

            while (true)
            {
                nextCell = FindNextCell(curCell, maze, visited);
                if (nextCell == curCell)
                {
                    curCell = path.Pop();
                }
                else
                {
                    break;
                }
            }
            return nextCell;
        }
        private Cell FindNextCell(Cell curCell, Cell[,] maze, List<Cell> visited)
        {
            (int,int)[] prioritedDirections = {(1,0), (0,1), (-1,0), (0,-1) };

            foreach(var direction in prioritedDirections) 
            {
                var next = maze[curCell.X + direction.Item1, curCell.Y + direction.Item2];

                switch (direction) 
                {
                    case (1, 0): 
                        if (!curCell.IsRightActive 
                            && !visited.Contains(maze[curCell.X + direction.Item1, 
                                                      curCell.Y + direction.Item2])) 
                        {  
                            return maze[curCell.X + direction.Item1, curCell.Y + direction.Item2]; 
                        } 
                        break; 
                    case (0, 1):
                        if (!curCell.IsLowerActive
                            
                            && !visited.Contains(maze[curCell.X + direction.Item1,
                                                      curCell.Y + direction.Item2]))
                        {
                            return maze[curCell.X + direction.Item1, curCell.Y + direction.Item2];
                        }
                        break; 
                    case (-1, 0):
                        if (!curCell.IsLeftActive 
                            && visited.Contains(maze[curCell.X + direction.Item1, 
                                                     curCell.Y + direction.Item2]))
                        {
                            return maze[curCell.X + direction.Item1, curCell.Y + direction.Item2];
                        }
                        break;
                    case (0, -1):
                        if (!curCell.IsUpperActive 
                            && visited.Contains(maze[curCell.X + direction.Item1, 
                                                     curCell.Y + direction.Item2]))
                        {
                            return maze[curCell.X + direction.Item1, curCell.Y + direction.Item2];
                        }
                        break;
                }
            }

            return curCell;
        }
       
    }
}
