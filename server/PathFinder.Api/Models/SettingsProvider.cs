using System.Collections.Generic;
using PathFinder.Domain.Models.Algorithms.AlgorithmsController;
using PathFinder.Domain.Models.GridFolder;
using PathFinder.Domain.Models.Metrics;
using PathFinder.Domain.Services.MazeService;

namespace PathFinder.Api.Models
{
    public class SettingsProvider
    {
        private readonly IEnumerable<Dictionary<string, object>> algorithms;
        private readonly int height;
        private readonly int width;
        private readonly IEnumerable<string> metricsNames;
        private readonly IMazeService mazeService;

        public SettingsProvider(IMazeService mazeService, DomainAlgorithmsController algorithmsController, IMetricFactory metricFactory,
            GridConfigurationParameters mazeParameters)
        {
            width = mazeParameters.Width;
            height = mazeParameters.Height;
            algorithms = algorithmsController.GetInfoAboutAlgorithmsWithAvailableParams();
            metricsNames = metricFactory.GetAvailableMetricNames();
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