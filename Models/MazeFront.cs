using System.ComponentModel.DataAnnotations;

namespace TestReact.Models
{
    public class MazeFront  
    {
        public int Id { get; set; }
        public Cell[,] Board { get; set; }

        public MazeFront(int id, Cell[,] board)
        {
            Id = id;
            Board = board;
        }
    }
}
