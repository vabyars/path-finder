using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PathFinder.Api.Models;
using PathFinder.Domain.Services.MazeService;
using PathFinder.Infrastructure;

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
        public async Task<ActionResult<GridWithStartAndEnd>> GetMaze(string name)
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

                await mazeService.AddAsync(mazeRequest.Name, new GridWithStartAndEnd
                {
                    Maze = mazeRequest.Grid,
                    Start = PointParser.Parse(mazeRequest.Start),
                    End = PointParser.Parse(mazeRequest.End)
                });
                return Ok();
            }
            catch (ArgumentException e)
            {// done to synchronize with asp errors
                return BadRequest(JsonConvert.SerializeObject(new { errors = new { Name = new[] { e.Message } } }));
            }
        }
    }
}