using Mazes.Web.Models;

namespace Mazes.Web
{
    public class WellKnown
    {
        public static readonly Maze Maze = new Maze
        {
            Id = "well-known",
            Size = 5,
            Board = new Cell[]
            {
                new Cell { X = 0, Y = 0, IsLeftActive = true, IsRightActive = false, IsLowerActive = true, IsUpperActive = true },
                new Cell { X = 0, Y = 1, IsLeftActive = true, IsRightActive = true, IsLowerActive = true, IsUpperActive = true },
                new Cell { X = 0, Y = 2, IsLeftActive = true, IsRightActive = true, IsLowerActive = true, IsUpperActive = true },
                new Cell { X = 0, Y = 3, IsLeftActive = true, IsRightActive = true, IsLowerActive = true, IsUpperActive = true },
                new Cell { X = 0, Y = 4, IsLeftActive = true, IsRightActive = true, IsLowerActive = true, IsUpperActive = true },
                new Cell { X = 1, Y = 0, IsLeftActive = false, IsRightActive = true, IsLowerActive = false, IsUpperActive = true },
                new Cell { X = 1, Y = 1, IsLeftActive = true, IsRightActive = false, IsLowerActive = true, IsUpperActive = false },
                new Cell { X = 1, Y = 2, IsLeftActive = true, IsRightActive = true, IsLowerActive = true, IsUpperActive = true },
                new Cell { X = 1, Y = 3, IsLeftActive = true, IsRightActive = true, IsLowerActive = true, IsUpperActive = true },
                new Cell { X = 1, Y = 4, IsLeftActive = true, IsRightActive = true, IsLowerActive = true, IsUpperActive = true },
                new Cell { X = 2, Y = 0, IsLeftActive = true, IsRightActive = true, IsLowerActive = true, IsUpperActive = true },
                new Cell { X = 2, Y = 1, IsLeftActive = false, IsRightActive = true, IsLowerActive = false, IsUpperActive = true },
                new Cell { X = 2, Y = 2, IsLeftActive = true, IsRightActive = false, IsLowerActive = true, IsUpperActive = false },
                new Cell { X = 2, Y = 3, IsLeftActive = true, IsRightActive = true, IsLowerActive = true, IsUpperActive = true },
                new Cell { X = 2, Y = 4, IsLeftActive = true, IsRightActive = true, IsLowerActive = true, IsUpperActive = true },
                new Cell { X = 3, Y = 0, IsLeftActive = true, IsRightActive = true, IsLowerActive = true, IsUpperActive = true },
                new Cell { X = 3, Y = 1, IsLeftActive = true, IsRightActive = true, IsLowerActive = true, IsUpperActive = true },
                new Cell { X = 3, Y = 2, IsLeftActive = false, IsRightActive = true, IsLowerActive = false, IsUpperActive = true },
                new Cell { X = 3, Y = 3, IsLeftActive = true, IsRightActive = false, IsLowerActive = true, IsUpperActive = false },
                new Cell { X = 3, Y = 4, IsLeftActive = true, IsRightActive = true, IsLowerActive = true, IsUpperActive = true },
                new Cell { X = 4, Y = 0, IsLeftActive = true, IsRightActive = true, IsLowerActive = true, IsUpperActive = true },
                new Cell { X = 4, Y = 1, IsLeftActive = true, IsRightActive = true, IsLowerActive = true, IsUpperActive = true },
                new Cell { X = 4, Y = 2, IsLeftActive = true, IsRightActive = true, IsLowerActive = true, IsUpperActive = true },
                new Cell { X = 4, Y = 3, IsLeftActive = false, IsRightActive = true, IsLowerActive = false, IsUpperActive = true },
                new Cell { X = 4, Y = 4, IsLeftActive = true, IsRightActive = true, IsLowerActive = true, IsUpperActive = false }
            }
        };
    }
}
