namespace PathFinder.Api.Controllers
{
    public class AddMazeRequest
    {
        public string Name { get; set; }
        public int[,] Grid { get; set; }
        
    }
}