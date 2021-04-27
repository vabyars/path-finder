using System;
using Microsoft.AspNetCore.Mvc;
using PathFinder.Domain.Interfaces;

namespace PathFinder.Api.Controllers
{
    [ApiController]
    [Route("maze")]
    public class MazeController : Controller
    {
        private readonly IMazeService _mazeService;

        public MazeController(IMazeService mazeService)
        {
            _mazeService = mazeService;
        }
        
        [HttpGet]
        [Route("/{name}")]
        public ActionResult<int[,]> GetMaze(string name)
        {
            try
            {
                return Ok(_mazeService.Get(name));
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        [Route("add")]
        public void AddMaze(AddMazeRequest mazeRequest)
        {
            _mazeService.Add(mazeRequest.Name, mazeRequest.Grid);
        }
    }
}