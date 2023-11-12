using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace Mazes.Web.Models
{
    public class MazeFront  
    {
        public string Id { get; set; }
        public Cell[][] Board { get; set; }

        public MazeFront(string id, Cell[][] board)
        {
            Id = id;
            Board = board;
        }


        public class LinkedList<T> : IEnumerable<T>
        {
            public readonly T Value;
            public readonly LinkedList<T> Previous;
            public readonly int Length;

            public LinkedList(T value, LinkedList<T> previous = null)
            {
                Value = value;
                Previous = previous;
                Length = previous?.Length + 1 ?? 1;
            }

            public IEnumerator<T> GetEnumerator()
            {
                yield return Value;
                var pathItem = Previous;
                while (pathItem != null)
                {
                    yield return pathItem.Value;
                    pathItem = pathItem.Previous;
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        public Cell[] GetMazeSolution(Cell[,] Board)
        {
            var visited = new HashSet<Cell>();
            var queue = new Queue<LinkedList<Cell>>();


            visited.Add(Board[0,0]);
            queue.Enqueue(new LinkedList<Cell>(Board[0, 0]));

            while (queue.Count != 0)
            {
                var cell = queue.Dequeue();
                var neighbours = FindNeighbours(cell);

                foreach (var curNeighbour in neighbours.Where(p => !visited.Contains(p)))
                {

                }

            }
        }

        private Cell[] FindNeighbours(LinkedList<Cell> curCell)
        {
            var res = new List<Cell>();
            var currentCell = curCell.Value;

            if (!currentCell.IsUpperActive) res.Add(currentCell);
            if (!currentCell.IsLowerActive) res.Add(currentCell);
            if (!currentCell.IsLeftActive) res.Add(currentCell);
            if (!currentCell.IsRightActive) res.Add(currentCell);

            return res.ToArray();
        }
    }
}
