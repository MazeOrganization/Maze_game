using System.ComponentModel.DataAnnotations;

namespace TestReact.Models
{
    public class Maze
    {
        public int Id { get; set; }
        public string PerformersName { get; set; }
        public Cell[,] board { get; set; }
        [DataType(DataType.Date)]
        public DateTime time { get; set; }
        public bool Done { get; set; }
    }
}
