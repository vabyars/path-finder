using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PathFinder.Api.Models;
using PathFinder.Domain.Models.Algorithms;
using PathFinder.Domain.Models.Algorithms.AlgorithmsController;
using PathFinder.Domain.Models.GridFolder;
using PathFinder.Domain.Models.Metrics;
using PathFinder.Domain.Models.Parameters;
using PathFinder.Infrastructure;

namespace PathFinder.Api.Controllers
{
    [ApiController]
    [Route("algorithm")]
    public class AlgorithmsController : Controller
    {
        private readonly AlgorithmsHandler algorithmsHandler;
        private readonly IMetricFactory metricFactory;

        public AlgorithmsController(AlgorithmsHandler algorithmsHandler, IMetricFactory metricFactory)
        {
            this.algorithmsHandler = algorithmsHandler;
            this.metricFactory = metricFactory;
        }
        
        [HttpPost]
        [Route("execute")]
        public async Task<ActionResult<IAlgorithmReport>> Execute(ExecuteAlgorithmRequest req)
        {
            var start = PointParser.Parse(req.Start);
            var goal = PointParser.Parse(req.Goal);
            var metric = metricFactory.GetMetric(req.MetricName);
            if (metric == null)
                return BadRequest($"metric {req.MetricName} was not found");
            var algorithmResult = algorithmsHandler.ExecuteAlgorithm(req.Name, 
                new Grid(req.Grid),
                new Parameters(start,
                    goal,
                    req.AllowDiagonal,
                    metric));
            
            if (algorithmResult == null)
                return BadRequest($"algorithm {req.Name} was not found");
            return Ok(algorithmResult);
        }
    }
}