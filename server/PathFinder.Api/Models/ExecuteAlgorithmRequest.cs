﻿using PathFinder.Domain.Models.Metrics;

namespace PathFinder.Api.Models
{
    public class ExecuteAlgorithmRequest
    {
        public string Name { get; set; }
        public string Start { get; set; }
        public string Goal { get; set; }
        public bool AllowDiagonal { get; set; }

        public Metric Metric { get; set; } = Metric.Euclidean;
        public int[,] Grid { get; set; }
    }
}