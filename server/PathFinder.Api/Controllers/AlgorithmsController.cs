using System;
using Microsoft.AspNetCore.Mvc;
using PathFinder.Api.Models;
using PathFinder.Domain.Models.Algorithms;
using PathFinder.Domain.Models.Algorithms.AlgorithmsController;
using PathFinder.Domain.Models.GridFolder;
using PathFinder.Domain.Models.Parameters;
using PathFinder.Infrastructure;

namespace PathFinder.Api.Controllers
{
    [ApiController]
    [Route("algorithm")]
    public class AlgorithmsController : Controller
    {
        private readonly IAlgorithmsHandler algorithmsHandler;

        public AlgorithmsController(IAlgorithmsHandler algorithmsHandler)
        {
            this.algorithmsHandler = algorithmsHandler;
        }
        
        [HttpPost]
        [Route("execute")]
        public ActionResult<IAlgorithmReport> Execute(ExecuteAlgorithmRequest req)
        {
            var start = PointParser.Parse(req.Start);
            var goal = PointParser.Parse(req.Goal);
            try
            {
                var algorithmResult = algorithmsHandler.ExecuteAlgorithm(req.Name, 
                    new Grid(req.Grid),
                    new Parameters(start,
                        goal,
                        req.AllowDiagonal,
                        req.Metric));
                return Ok(algorithmResult);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}