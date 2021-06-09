using System;
using PathFinder.Domain.Models.Algorithms;
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

        protected abstract RenderedState RenderState(ResultPathState state);

        protected abstract RenderedState RenderState(CurrentPointState state);

        protected abstract RenderedState RenderState(CandidateToPrepareState state);

        public abstract IAlgorithmReport GetReport();
    }
}