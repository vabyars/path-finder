using System;
using System.Collections.Generic;
using System.Linq;
using PathFinder.Domain.Interfaces;
using PathFinder.Domain.Models.States;

namespace PathFinder.Domain.Models.Renders
{
    public class RenderProvider
    {
        private readonly IEnumerable<Render> renders;

        public RenderProvider(IEnumerable<Render> renders)
        {
            this.renders = renders;
        }
        
        public Render GetRender(IAlgorithm<State> algorithm)
        {
            var algorithmName = algorithm.GetType().Name;
            var render = renders.FirstOrDefault(x => x.SupportingAlgorithms.Contains(algorithmName));
            if (render == null)
                throw new ArgumentException($"{algorithmName} has not render");
            return render;
        }
    }
}