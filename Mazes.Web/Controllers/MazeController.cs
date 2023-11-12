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

            return _context.Maze.OrderBy(x => Guid.NewGuid().ToString()).First().ToFrontModel();
        }

        [HttpPost]
        public async Task Create(Maze maze)
        {
            _context.Add(maze);
            await _context.SaveChangesAsync();
        }

        [HttpPatch]
        public async Task Edit( Maze maze)
        {
            _context.Update(maze);
            await _context.SaveChangesAsync();
        }

    }
}
