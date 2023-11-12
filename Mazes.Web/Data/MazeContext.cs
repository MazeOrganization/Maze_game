using Microsoft.EntityFrameworkCore;
using Mazes.Web.Models;

namespace Mazes.Web.Data
{
    public class MazeContext : DbContext
    {
        public MazeContext(DbContextOptions<MazeContext> options)
            : base(options)
        {
        }

        public DbSet<Maze> Maze { get; set; }
    }
}
