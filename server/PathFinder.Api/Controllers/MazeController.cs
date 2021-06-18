using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PathFinder.Api.Models;
using PathFinder.Domain.Services.MazeService;

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
        public async Task<ActionResult<int[,]>> GetMaze(string name)
        {
            try
            {
                var maze = await mazeService.GetAsync(name);
                return Ok(maze);
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<string>> AddMaze(AddMazeRequest mazeRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();
                await mazeService.AddAsync(mazeRequest.Name, mazeRequest.Grid);
                return Ok();
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}