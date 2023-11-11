using Microsoft.AspNetCore.Mvc;
using TestReact.Data;
using TestReact.Models;

namespace TestReact.Controllers
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
        public IEnumerable<Maze> Get()
        {
            return _context.Maze.ToList();
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
