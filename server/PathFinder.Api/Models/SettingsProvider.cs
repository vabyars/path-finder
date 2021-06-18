using System.Collections.Generic;
using System.Threading.Tasks;
using PathFinder.Domain.Models.Algorithms.AlgorithmsController;
using PathFinder.Domain.Models.GridFolder;
using PathFinder.Domain.Models.Metrics;
using PathFinder.Domain.Services.MazeService;

namespace PathFinder.Api.Models
{
    public class SettingsProvider
    {
        private readonly IEnumerable<string> algorithms;
        private readonly int height;
        private readonly int width;
        private readonly IEnumerable<string> metricsNames;
        private readonly IMazeService mazeService;

        public SettingsProvider(IMazeService mazeService, AlgorithmsHandler algorithmsHandler, IMetricFactory metricFactory,
            GridConfigurationParameters mazeParameters)
        {
            width = mazeParameters.Width;
            height = mazeParameters.Height;
            algorithms = algorithmsHandler.GetAvailableAlgorithmNames();
            metricsNames = metricFactory.GetAvailableMetricNames();
            this.mazeService = mazeService;
        }

        public async Task<object> GetSettings()
        {
            return new
            {
                mazes = await mazeService.GetAvailableNamesAsync(),
                algorithms,
                width,
                height,
                metrics = metricsNames,
            };
        }
    }
}