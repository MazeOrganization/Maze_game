using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace Mazes.Web.Models
{
    public class Maze
    {
        public Maze() { }

        public Maze(string id, int size, Cell[] board)
        {
            Id = id;
            Size = size;
            Board = board;
        }

        public string Id { get; set; } = null!;
        public int Size { get; set; }

        [NotMapped]
        public Cell[] Board
        {
            get
            {
                return DeserializeBoard(BoardData);
            }
            set
            {
                BoardData = SerializeBoard(value);
            }
        }

        public string BoardData { get; set; } = null!;

        private static string SerializeBoard(Cell[] board)
        {
            return JsonSerializer.Serialize(board);
        }

        private static Cell[] DeserializeBoard(string boardData)
        {
            return JsonSerializer.Deserialize<Cell[]>(boardData) ?? Array.Empty<Cell>();
        }
    }
}
