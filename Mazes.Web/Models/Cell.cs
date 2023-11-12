namespace Mazes.Web.Models
{
    public class Cell
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsRightActive {  get; set; }
        public bool IsLeftActive { get; set; }
        public bool IsUpperActive { get; set; }
        public bool IsLowerActive {  get; set; }

    }
}
