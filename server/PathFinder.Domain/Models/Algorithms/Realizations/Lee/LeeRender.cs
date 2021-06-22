using System;
using System.Collections.Generic;
using System.Drawing;
using PathFinder.Domain.Models.Algorithms.Realizations.AStar;
using PathFinder.Domain.Models.Renders;
using PathFinder.Domain.Models.States;
using PathFinder.Domain.Models.States.CandidateToPrepare;
using PathFinder.Domain.Models.States.PreparedPoint;
using PathFinder.Domain.Models.States.ResultPath;
using PathFinder.Infrastructure;

namespace PathFinder.Domain.Models.Algorithms.Realizations.Lee
{
    public class LeeRender : IRender
    {
        private readonly List<RenderedState> states = new ();
        public RenderedState RenderState(IState state)
        {
            var renderedState = state switch
            {
                CurrentPointState s => RenderState(s),
                CandidateToPrepareState s => RenderState(s),
                ResultPathState s => RenderState(s),
                _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
            };
            states.Add(renderedState);
            return renderedState;
        }

        public RenderedState RenderState(CurrentPointState state)
        {
            return new RenderedPreparedPointState
            {
                Color = Color.Blue.ToHex(),
                SecondColor = Color.Yellow.ToHex(),
                RenderedPoint = state.PreparedPoint
            };
        }
        
        private RenderedState RenderState(CandidateToPrepareState state)
        {
            return new RenderedCandidateState
            {
                Color = Color.Gold.ToHex(),
                RenderedPoint = state.Candidate,
                SecondColor = Color.Blue.ToHex()
            };
        }
        
        private RenderedState RenderState(ResultPathState state)
        {
            return new RenderedPathState
            {
                Color = Color.Green.ToHex(),
                Path = state.Path
            };
        }

        public IAlgorithmReport GetReport() =>
            new AlgorithmReport(states);
    }
}