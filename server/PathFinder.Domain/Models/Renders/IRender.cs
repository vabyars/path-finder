using System.Collections.Generic;
using PathFinder.Domain.Interfaces;
using PathFinder.Domain.Models.Algorithms;
using PathFinder.Domain.Models.Algorithms.AStar;
using PathFinder.Domain.Models.States;
using PathFinder.Domain.Models.States.CandidateToPrepare;
using PathFinder.Domain.Models.States.PreparedPoint;
using PathFinder.Domain.Models.States.ResultPath;

namespace PathFinder.Domain.Models.Renders
{
    public interface IRender
    {
        RenderedState RenderState(ResultPathState state);
        RenderedState RenderState(CurrentPointState state);
        RenderedState RenderState(CandidateToPrepareState state);
        RenderedState RenderState(IState state);

        IAlgorithmReport GetReport();
    }
}