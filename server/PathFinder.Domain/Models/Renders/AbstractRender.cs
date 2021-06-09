using System;
using PathFinder.Domain.Interfaces;
using PathFinder.Domain.Models.Algorithms;
using PathFinder.Domain.Models.Algorithms.AStar;
using PathFinder.Domain.Models.States;
using PathFinder.Domain.Models.States.CandidateToPrepare;
using PathFinder.Domain.Models.States.PreparedPoint;
using PathFinder.Domain.Models.States.ResultPath;

namespace PathFinder.Domain.Models.Renders
{
    public abstract class AbstractRender : IRender
    {
        public virtual RenderedState RenderState(IState state)
        {
            return state switch
            {
                CurrentPointState s => RenderState(s),
                CandidateToPrepareState s => RenderState(s),
                ResultPathState s => RenderState(s),
                _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
            };
        }

        public abstract RenderedState RenderState(ResultPathState state);

        public abstract RenderedState RenderState(CurrentPointState state);

        public abstract RenderedState RenderState(CandidateToPrepareState state);

        public abstract IAlgorithmReport GetReport();
    }
}