using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PathFinder.Domain.Interfaces;
using PathFinder.Domain.Models.Metrics;

namespace PathFinder.Api.Controllers
{
    [ApiController]
    [Route("settings")]
    public class SettingsController : Controller
    {
        private readonly IMazeService _mazeService;
        private readonly IAlgorithmsExecutor _algorithmsExecutor;
        private readonly IMetricFactory _metricFactory;

        public SettingsController(IMazeService mazeService, IAlgorithmsExecutor algorithmsExecutor, IMetricFactory metricFactory)
        {
            _mazeService = mazeService;
            _algorithmsExecutor = algorithmsExecutor;
            this._metricFactory = metricFactory;
        }

        [HttpGet]
        public ActionResult<string> GetSettings()
        {
            var mazes = _mazeService.GetAvailableNames().ToArray();
            var algorithms = _algorithmsExecutor.AvailableAlgorithmNames().ToArray();
            var metrics = _metricFactory.GetAvailableMetricNames().ToArray();
            var res = new Dictionary<string, string[]>
            {
                ["mazes"] = mazes,
                ["algorithms"] = algorithms,
                ["metrics"] = metrics
            };
            return JsonConvert.SerializeObject(res, Formatting.Indented);
        }
    }
}