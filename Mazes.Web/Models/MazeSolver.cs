namespace Mazes.Web.Models
{
    public class MazeSolver
    {
        public Cell[] GetMazeSolution(Cell[,] Board)
        {
            var x = BreadthSearch(Board);
            var res = new Cell[x.Length];

            while(x.Previous != null)
            {
                res.push
            }

        }
        private LinkedList<Cell> BreadthSearch(Cell[,] Board)
        {
            var visited = new HashSet<Cell>();
            var queue = new Queue<LinkedList<Cell>>();


            visited.Add(Board[0, 0]);
            queue.Enqueue(new LinkedList<Cell>(Board[0, 0]));

            while (queue.Count != 0)
            {
                var cell = queue.Dequeue();
                var neighbours = FindNeighbours(cell);

                foreach (var curNeighbour in neighbours.Where(p => !visited.Contains(p)))
                {
                    var path = new LinkedList<Cell>(curNeighbour, cell);

                    visited.Add(curNeighbour);
                    queue.Enqueue(path);

                    if (curNeighbour == Board[Board.GetLength(0), Board.GetLength(1)])
                    {
                        return path;
                    }
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
