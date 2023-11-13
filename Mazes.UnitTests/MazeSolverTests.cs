using Mazes.Web;
using Mazes.Web.Extensions;
using Mazes.Web.Models;

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
    public void MazeSolver_SolvesMaze()
    {
        var maze = WellKnown.Maze;

        var solution = solver.GetMazeSolution(maze.Board.ToTwoDimensionalArray(maze.Size, maze.Size));

        Assert.IsTrue(solution.Length > 0);
    }
}
