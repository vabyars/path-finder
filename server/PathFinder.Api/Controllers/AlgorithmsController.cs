using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PathFinder.Api.Models;
using PathFinder.Domain;
using PathFinder.Domain.Interfaces;
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
}