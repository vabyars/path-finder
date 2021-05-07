using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PathFinder.Api.Models;
using PathFinder.Domain;
using PathFinder.Domain.Interfaces;
using PathFinder.Domain.Metrics;
using PathFinder.Domain.Models;
using PathFinder.Domain.Models.States;
using PathFinder.Infrastructure;

namespace PathFinder.Api.Controllers
{
    [ApiController]
    [Route("algorithm")]
    public class AlgorithmsController : Controller
    {
        private readonly IAlgorithmsExecutor _algorithmsExecutor;

        public AlgorithmsController(IAlgorithmsExecutor algorithmsExecutor)
        {
            _algorithmsExecutor = algorithmsExecutor;
        }
        
        [HttpPost]
        [Route("execute")]
        public ActionResult<List<State>> Execute(ExecuteAlgorithmRequest req)
        {
            var start = PointParser.Parse(req.Start);
            var goal = PointParser.Parse(req.Goal);
            var metric = MetricFactory.GetMetric(req.MetricName);
            if (metric == null)
                return BadRequest($"metric {req.MetricName} was not found");
            var algorithm = _algorithmsExecutor.Execute(req.Name, 
                new Grid(req.Grid),
                new Parameters(start,
                    goal,
                    req.AllowDiagonal,
                    metric));
            
            if (algorithm == null)
                return BadRequest($"algorithm {req.Name} was not found");
            return algorithm;
        }
    }
}