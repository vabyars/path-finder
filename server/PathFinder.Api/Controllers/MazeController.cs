using System;
using System.Drawing;
using Microsoft.AspNetCore.Mvc;
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
        public ActionResult<GridWithStartAndEnd> GetMaze(string name)
        {
            try
            {
                var maze = mazeService.Get(name);
                return Ok(maze);
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPut]
        public ActionResult<string> AddMaze(AddMazeRequest mazeRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();
                mazeService.Add(mazeRequest.Name, new GridWithStartAndEnd
                    { 
                        Maze = mazeRequest.Grid, 
                        Start = PointParser.Parse(mazeRequest.Start), 
                        End = PointParser.Parse(mazeRequest.End)
                    });
                return Ok();
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}