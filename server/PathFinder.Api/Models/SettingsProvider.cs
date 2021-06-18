using System.Collections.Generic;
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

        public SettingsProvider(IMazeService mazeService, AlgorithmsHandler algorithmsHandler, GridConfigurationParameters mazeParameters)
        {
            width = mazeParameters.Width;
            height = mazeParameters.Height;
            algorithms = algorithmsHandler.GetAvailableAlgorithmNames();
            metricsNames = MetricExtensions.AvailableNames();
            this.mazeService = mazeService;
        }

        public object GetSettings()
        {
            return new
            {
                mazes = mazeService.GetAvailableNames(),
                algorithms,
                width,
                height,
                metrics = metricsNames,
            };
        }
    }
}