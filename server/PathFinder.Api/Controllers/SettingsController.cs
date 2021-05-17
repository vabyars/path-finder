using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PathFinder.Domain;
using PathFinder.Domain.Interfaces;
using PathFinder.Domain.Models.Metrics;

namespace PathFinder.Api.Controllers
{
    [ApiController]
    [Route("settings")]
    public class SettingsController : Controller
    {
        private readonly IMazeService mazeService;
        private readonly IAlgorithmsExecutor algorithmsExecutor;
        private readonly IMetricFactory metricFactory;
        private readonly GridConfigurationParameters mazeParameters;

        public SettingsController(IMazeService mazeService, IAlgorithmsExecutor algorithmsExecutor, IMetricFactory metricFactory,
            GridConfigurationParameters mazeParameters)
        {
            this.mazeService = mazeService;
            this.algorithmsExecutor = algorithmsExecutor;
            this.metricFactory = metricFactory;
            this.mazeParameters = mazeParameters;
        }

        [HttpGet]
        public ActionResult<string> GetSettings()
        {
            var mazes = mazeService.GetAvailableNames().ToArray();
            var algorithms = algorithmsExecutor.AvailableAlgorithmNames().ToArray();
            var metrics = metricFactory.GetAvailableMetricNames().ToArray();
            var res = new Dictionary<string, object>
            {
                ["mazes"] = mazes,
                ["algorithms"] = algorithms,
                ["metrics"] = metrics,
                ["width"] = mazeParameters.Width,
                ["height"] = mazeParameters.Height
            };
            return JsonConvert.SerializeObject(res, Formatting.Indented);
        }
    }
}