using Microsoft.AspNetCore.Mvc;
using Mazes.Web.Data;
using Mazes.Web.Models;

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
            var generator = new MazeGenerator();
            var newMaze = generator.GenerateMaze(3);
            _context.Add(newMaze);
            _context.SaveChangesAsync();

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

            var solution = new List<Cell>();
            for (var i = 0; i < maze.Size; i++)
            {
                solution.Add(new Cell { X = i, Y = i });
                solution.Add(new Cell { X = i + 1, Y = i });
            }
            return solution.ToArray();
        }

    }
}
