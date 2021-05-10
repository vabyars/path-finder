using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PathFinder.Api.Models;
using PathFinder.Domain;
using PathFinder.Domain.Interfaces;
using PathFinder.Domain.Models;
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
            var mazes = _mazeService.GetAvailableNames().ToArray();
            var algorithms = _algorithmsExecutor.AvailableAlgorithmNames().ToArray();
            var res = new Dictionary<string, string[]>
            {
                ["mazes"] = mazes,
                ["algorithms"] = algorithms
            };
            return JsonConvert.SerializeObject(res, Formatting.Indented);
        }
    }
}