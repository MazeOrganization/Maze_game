using Microsoft.AspNetCore.Mvc;
using Mazes.Web.Data;
using Mazes.Web.Models;
using System.Drawing;

namespace Mazes.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MazeController
    {
        private readonly ILogger<MazeController> _logger;
        private readonly MazeContext _context;
        public MazeController(ILogger<MazeController> logger, MazeContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public MazeFront Get()
        {
            //for(int i = 1; i < 19; i++)
            //{
            //    var generator = new MazeGenerator();
            //    var newMaze = generator.GenerateMaze(20);
            //    _context.Add(newMaze);
            //    _context.SaveChangesAsync();
            //}
            
            var randomMaze = _context.Maze.OrderBy(x => Guid.NewGuid().ToString()).First();
            var mazeModel = randomMaze.ToFrontModel();
            return mazeModel;
        }

        [HttpPost]
        public async Task Create()
        {

            var generator = new MazeGenerator();
            var newMaze = generator.GenerateMaze(3);
            _context.Add(newMaze);
            await _context.SaveChangesAsync();
        }

        [HttpPatch]
        public async Task Edit( Maze maze)
        {
            _context.Update(maze);
            await _context.SaveChangesAsync();
        }

        [HttpGet]
        [Route("{mazeId}/solve")]
        public Cell[]? Solve(string mazeId)
        {
            var maze = _context.Maze.First(x => x.Id == mazeId);
            var solver = new MazeSolver();

            Cell[,] newBoard = new Cell[maze.Size, maze.Size];

            // Populate the 2D array
            for (int i = 0; i < maze.Size; i++)
            {
                for (int j = 0; j < maze.Size; j++)
                {
                    newBoard[i, j] = maze.Board[i * maze.Size + j];
                }
            }

            var solution = solver.GetMazeSolution(newBoard);
           
            return solution.ToArray();
        }

    }
}
