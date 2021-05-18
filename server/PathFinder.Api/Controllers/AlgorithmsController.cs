using Microsoft.AspNetCore.Mvc;
using PathFinder.Api.Models;
using PathFinder.Domain;
using PathFinder.Domain.Models;
using PathFinder.Domain.Models.Algorithms;
using PathFinder.Domain.Models.Metrics;
using PathFinder.Domain.Models.States;
using PathFinder.Infrastructure;

namespace PathFinder.Api.Controllers
{
    [ApiController]
    [Route("algorithm")]
    public class AlgorithmsController : Controller
    {
        private readonly DomainAlgorithmsController algorithmsController;
        private readonly IMetricFactory metricFactory;

        public AlgorithmsController(DomainAlgorithmsController algorithmsController, IMetricFactory metricFactory)
        {
            this.algorithmsController = algorithmsController;
            this.metricFactory = metricFactory;
        }
        
        [HttpPost]
        [Route("execute")]
        public ActionResult<AlgorithmExecutionInfo> Execute(ExecuteAlgorithmRequest req)
        {
            var start = PointParser.Parse(req.Start);
            var goal = PointParser.Parse(req.Goal);
            var metric = metricFactory.GetMetric(req.MetricName);
            if (metric == null)
                return BadRequest($"metric {req.MetricName} was not found");
            var algorithm = algorithmsController.ExecuteAlgorithm(req.Name, 
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