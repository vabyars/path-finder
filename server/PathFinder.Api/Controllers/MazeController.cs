using System;
using Microsoft.AspNetCore.Mvc;
using PathFinder.Api.Models;
using PathFinder.Domain.Interfaces;

namespace PathFinder.Api.Controllers
{
    [ApiController]
    [Route("maze")]
    public class MazeController : Controller
    {
        private readonly IMazeService mazeService;

        public MazeController(IMazeService mazeService)
        {
            this.mazeService = mazeService;
        }
        
        [HttpGet]
        [Route("{name}")]
        public ActionResult<int[,]> GetMaze(string name)
        {
            try
            {
                return Ok(mazeService.Get(name));
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        [Route("add")]
        public ActionResult<string> AddMaze(AddMazeRequest mazeRequest)
        {
            try
            {
                mazeService.Add(mazeRequest.Name, mazeRequest.Grid);
                return Ok();
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}