using System.Collections.Generic;
using PathFinder.Domain.Interfaces;
using PathFinder.Domain.Models.Algorithms;
using PathFinder.Domain.Models.Algorithms.AStar;
using PathFinder.Domain.Models.States;

namespace PathFinder.Domain.Models.Renders
{
    public interface IRender
    {
        RenderedState RenderState(IState state);

        IAlgorithmReport GetReport();
    }
}