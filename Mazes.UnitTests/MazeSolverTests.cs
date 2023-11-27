using Mazes.Web;
using Mazes.Web.Extensions;
using Mazes.Web.Models;
using Microsoft.Extensions.Logging.Console;

namespace Mazes.UnitTests;

public class MazeSolverTests
{
    private MazeSolver solver = null!;

    [SetUp]
    public void SetUp()
    {
        solver = new MazeSolver();
    }

    [Test]
    [Explicit]
    public void MazeSolver_SolvesSimpleMaze()
    {
        Maze simpleMaze = new Maze()
        {
            Id = "simple",
            Size = 2,
            Board = new Cell[]
        {
            new Cell { X = 0, Y = 0, IsLeftActive = true, IsRightActive = false, IsLowerActive = true, IsUpperActive = true },
            new Cell { X = 0, Y = 1, IsLeftActive = true, IsRightActive = false, IsLowerActive = true, IsUpperActive = true },
            new Cell { X = 1, Y = 0, IsLeftActive = false, IsRightActive = true, IsLowerActive = false, IsUpperActive = true },
            new Cell { X = 1, Y = 1, IsLeftActive = false, IsRightActive = true, IsLowerActive = true, IsUpperActive = false },
        }
        };

        var path = solver.GetMazeSolution(simpleMaze.Board.ToTwoDimensionalArray(simpleMaze.Size, simpleMaze.Size));
        var expectedPath = new Cell[] {
            new Cell { X = 0, Y = 0, IsLeftActive = true, IsRightActive = false, IsLowerActive = true, IsUpperActive = true },
            new Cell { X = 1, Y = 0, IsLeftActive = false, IsRightActive = true, IsLowerActive = false, IsUpperActive = true },
            new Cell { X = 1, Y = 1, IsLeftActive = false, IsRightActive = true, IsLowerActive = true, IsUpperActive = false },
        };

        IsValidPath(path, expectedPath);
    }

    [Test]
    [Explicit]
    public void MazeSolver_SolvesSimpleMazeWithDeadEnd()
    {
        Maze simpleMaze = new Maze()
        {
            Id = "simple with dead end",
            Size = 2,
            Board = new Cell[]
        {
            new Cell { X = 0, Y = 0, IsLeftActive = true, IsRightActive = false, IsLowerActive = false, IsUpperActive = true },
            new Cell { X = 0, Y = 1, IsLeftActive = true, IsRightActive = false, IsLowerActive = true, IsUpperActive = false },
            new Cell { X = 1, Y = 0, IsLeftActive = false, IsRightActive = true, IsLowerActive = true, IsUpperActive = true },
            new Cell { X = 1, Y = 1, IsLeftActive = false, IsRightActive = true, IsLowerActive = true, IsUpperActive = true },
        }
        };

        var path = solver.GetMazeSolution(simpleMaze.Board.ToTwoDimensionalArray(simpleMaze.Size, simpleMaze.Size));
        var expectedPath = new Cell[] {
            new Cell { X = 0, Y = 0, IsLeftActive = true, IsRightActive = false, IsLowerActive = false, IsUpperActive = true },
            new Cell { X = 0, Y = 1, IsLeftActive = true, IsRightActive = false, IsLowerActive = true, IsUpperActive = false },
            new Cell { X = 1, Y = 1, IsLeftActive = false, IsRightActive = true, IsLowerActive = true, IsUpperActive = true },
        };

        IsValidPath(path, expectedPath);
    }

    [Test]
    [Explicit]
    public void MazeSolver_SolvesMazeWithMultipleDeadEnds()
    {
        Maze simpleMaze = new Maze()
        {
            Id = "with multiple dead ends",
            Size = 3,
            Board = new Cell[]
        {
            new Cell { X = 0, Y = 0, IsLeftActive = true, IsRightActive = false, IsLowerActive = false, IsUpperActive = true },
            new Cell { X = 0, Y = 1, IsLeftActive = true, IsRightActive = false, IsLowerActive = false, IsUpperActive = false },
            new Cell { X = 0, Y = 2, IsLeftActive = true, IsRightActive = false, IsLowerActive = true, IsUpperActive = false },
            new Cell { X = 1, Y = 0, IsLeftActive = false, IsRightActive = false, IsLowerActive = true, IsUpperActive = true },
            new Cell { X = 1, Y = 1, IsLeftActive = false, IsRightActive = false, IsLowerActive = true, IsUpperActive = true },
            new Cell { X = 1, Y = 2, IsLeftActive = false, IsRightActive = false, IsLowerActive = true, IsUpperActive = true },
            new Cell { X = 2, Y = 0, IsLeftActive = false, IsRightActive = true, IsLowerActive = true, IsUpperActive = true },
            new Cell { X = 2, Y = 1, IsLeftActive = false, IsRightActive = true, IsLowerActive = true, IsUpperActive = true },
            new Cell { X = 2, Y = 2, IsLeftActive = false, IsRightActive = true, IsLowerActive = true, IsUpperActive = true },
        }
        };

        var path = solver.GetMazeSolution(simpleMaze.Board.ToTwoDimensionalArray(simpleMaze.Size, simpleMaze.Size));
        var expectedPath = new Cell[] {
            new Cell { X = 0, Y = 0, IsLeftActive = true, IsRightActive = false, IsLowerActive = false, IsUpperActive = true },
            new Cell { X = 0, Y = 1, IsLeftActive = true, IsRightActive = false, IsLowerActive = false, IsUpperActive = false },
            new Cell { X = 0, Y = 2, IsLeftActive = true, IsRightActive = false, IsLowerActive = true, IsUpperActive = false },
            new Cell { X = 1, Y = 2, IsLeftActive = false, IsRightActive = false, IsLowerActive = true, IsUpperActive = true },
            new Cell { X = 2, Y = 2, IsLeftActive = false, IsRightActive = true, IsLowerActive = true, IsUpperActive = true },
        };

        IsValidPath(path, expectedPath);
    }

    //[Test]
    //[Explicit]
    //public void MazeSolver_SolvesBigWellKnownMaze()
    //{
    //    Maze simpleMaze = new Maze()
    //    {
    //        Id = "big wellknown",
    //        Size = 5,
    //        Board = new Cell[]
    //    {
    //        new Cell { X = 0, Y = 0, IsLeftActive = true, IsRightActive = false, IsLowerActive = false, IsUpperActive = true },
    //        new Cell { X = 0, Y = 1, IsLeftActive = true, IsRightActive = false, IsLowerActive = false, IsUpperActive = false },
    //        new Cell { X = 0, Y = 2, IsLeftActive = true, IsRightActive = false, IsLowerActive = true, IsUpperActive = false },
    //        new Cell { X = 0, Y = 3, IsLeftActive = true, IsRightActive = false, IsLowerActive = true, IsUpperActive = false },
    //        new Cell { X = 0, Y = 4, IsLeftActive = true, IsRightActive = false, IsLowerActive = true, IsUpperActive = false },
    //        new Cell { X = 1, Y = 0, IsLeftActive = false, IsRightActive = false, IsLowerActive = true, IsUpperActive = true },
    //        new Cell { X = 1, Y = 1, IsLeftActive = false, IsRightActive = false, IsLowerActive = true, IsUpperActive = true },
    //        new Cell { X = 1, Y = 2, IsLeftActive = false, IsRightActive = false, IsLowerActive = true, IsUpperActive = true },
    //        new Cell { X = 1, Y = 3, IsLeftActive = false, IsRightActive = false, IsLowerActive = true, IsUpperActive = true },
    //        new Cell { X = 1, Y = 4, IsLeftActive = false, IsRightActive = false, IsLowerActive = true, IsUpperActive = true },
    //        new Cell { X = 2, Y = 0, IsLeftActive = false, IsRightActive = true, IsLowerActive = true, IsUpperActive = true },
    //        new Cell { X = 2, Y = 1, IsLeftActive = false, IsRightActive = true, IsLowerActive = true, IsUpperActive = true },
    //        new Cell { X = 2, Y = 2, IsLeftActive = false, IsRightActive = true, IsLowerActive = true, IsUpperActive = true },
    //        new Cell { X = 2, Y = 3, IsLeftActive = false, IsRightActive = true, IsLowerActive = true, IsUpperActive = true },
    //        new Cell { X = 2, Y = 4, IsLeftActive = false, IsRightActive = true, IsLowerActive = true, IsUpperActive = true },
    //    }
    //    };

    //    var path = solver.GetMazeSolution(simpleMaze.Board.ToTwoDimensionalArray(simpleMaze.Size, simpleMaze.Size));
    //    var expectedPath = new Cell[] {
    //        new Cell { X = 0, Y = 0, IsLeftActive = true, IsRightActive = false, IsLowerActive = false, IsUpperActive = true },
    //        new Cell { X = 0, Y = 1, IsLeftActive = true, IsRightActive = false, IsLowerActive = false, IsUpperActive = false },
    //        new Cell { X = 0, Y = 2, IsLeftActive = true, IsRightActive = false, IsLowerActive = true, IsUpperActive = false },
    //        new Cell { X = 1, Y = 2, IsLeftActive = false, IsRightActive = false, IsLowerActive = true, IsUpperActive = true },
    //        new Cell { X = 2, Y = 2, IsLeftActive = false, IsRightActive = true, IsLowerActive = true, IsUpperActive = true },
    //    };

    //    IsValidPath(path, expectedPath);
    //}

    private void IsValidPath(Cell[] path, Cell[] expectedPath)
    {
        Assert.IsTrue(path.Length > 0, "Stack is empty");
        Assert.IsTrue(path[path.Length - 1].X == expectedPath[expectedPath.Length - 1].X
                        && path[path.Length - 1].Y == expectedPath[expectedPath.Length - 1].Y,
                        "Solver did not reach the exit.");
        Assert.AreEqual(expectedPath.Length, path.Length, "Path must be shortest.");
    }

    //[Test]
    //[Explicit]
    //public void MazeSolver_SolvesRandomMaze()
    //{
    //    var mazeGenerator = new MazeGenerator();
    //    Maze maze =  mazeGenerator.GenerateMaze(10);

    //    var path = solver.GetMazeSolution(maze.Board.ToTwoDimensionalArray(maze.Size, maze.Size));

    //    Assert.IsTrue(path.Length > 0, "Stack is empty");
    //    Assert.IsTrue(path[path.Length - 1].X == maze.Board.Last().X
    //                    && path[path.Length - 1].Y == maze.Board.Last().Y,
    //                    "Solver did not reach the exit.");
    //}
}
