namespace PathFinder.Api.Models
{
    public class AddMazeRequest
    {
        public string Name { get; set; }
        public int[,] Grid { get; set; }
        
    }
}