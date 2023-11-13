using Mazes.Web.Models;

namespace Mazes.Web.Extensions;

public static class CellExtensions
{
    public static Cell[,] ToTwoDimensionalArray(this Cell[] cells, int width, int height)
    {
        var result = new Cell[height, width];
        for (int i = 0; i < cells.Length; i++)
        {
            var cell = cells[i];
            result[cell.Y, cell.X] = cell;
        }
        return result;
    }
}
