namespace TestReact.Models
{
    public class Cell
    {
        public int[] Coordinate { get; set; }
        public bool IsRightActive {  get; set; }
        public bool IsLeftActive { get; set; }
        public bool IsUpperActive { get; set; }
        public bool IsLowerActive {  get; set; }
    }
}
