using System;
using System.Linq;
using PathFinder.Domain.Interfaces;
using PathFinder.Domain.Models.States;

namespace PathFinder.Domain.Models.Renders
{
    public class RenderProvider
    {
        private readonly Render[] _renders;

        public RenderProvider(Render[] renders)
        {
            _renders = renders;
        }
        
        public Render GetRender(IAlgorithm<State> algorithm)
        {
            var algorithmName = algorithm.GetType().Name;
            var render = _renders.FirstOrDefault(x => x.SupportingAlgorithms.Contains(algorithmName));
            if (render == null)
                throw new ArgumentException($"{algorithmName} has not render");
            return render;
        }
    }
}