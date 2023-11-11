using Microsoft.EntityFrameworkCore;
using TestReact.Models;

namespace TestReact.Data
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
