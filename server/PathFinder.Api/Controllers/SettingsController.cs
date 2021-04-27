using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PathFinder.Domain;
using PathFinder.Domain.Interfaces;
using PathFinder.Domain.Models;
using PathFinder.Domain.Models.Algorithms.AStar;
using PathFinder.Domain.Models.States;
using PathFinder.Infrastructure;

namespace PathFinder.Api.Controllers
{
    [ApiController]
    [Route("settings")]
    public class SettingsController : Controller
    {
        private readonly IMazeService _mazeService;
        private readonly IAlgorithmsExecutor _algorithmsExecutor;

        public SettingsController(IMazeService mazeService, IAlgorithmsExecutor algorithmsExecutor)
        {
            _mazeService = mazeService;
            _algorithmsExecutor = algorithmsExecutor;
        }

        [HttpGet]
        public ActionResult<string> GetSettings()
        {
            var mazes = _mazeService.GetAvailableNames();
            var algorithms = _algorithmsExecutor.AvailableAlgorithmNames().ToArray();
            var res = new Dictionary<string, string[]>
            {
                ["mazes"] = mazes,
                ["algorithms"] = algorithms
            };
            return JsonConvert.SerializeObject(res, Formatting.Indented);
        }

        [HttpPost]
        [Route("execute")]
        public ActionResult<List<State>> Execute(ExecuteRequest req)
        {
            var startPoint = PointParser.Parse(req.Start);
            var goalPoint = PointParser.Parse(req.Goal);
            var algorithm = _algorithmsExecutor.Execute(req.Name, 
                new Grid(req.Grid),
                new Parameters(startPoint,
                    goalPoint,
                    req.AllowDiagonal));
            
            if (algorithm == null)
                return BadRequest("wrong");
            return algorithm;
        }
    }

    public class ExecuteRequest
    {
        public string Name { get; set; }
        public string Start { get; set; }
        public string Goal { get; set; }
        public bool AllowDiagonal { get; set; }
        public int[,] Grid { get; set; }
    }
}