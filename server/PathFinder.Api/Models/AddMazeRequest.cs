using System.ComponentModel.DataAnnotations;

namespace PathFinder.Api.Models
{
    public class AddMazeRequest
    {
        [Required(ErrorMessage = "Field Name can not be empty")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "String length must be from 4 to 20")]
        public string Name { get; set; }
        
        [Required]
        public int[,] Grid { get; set; }
    }
}