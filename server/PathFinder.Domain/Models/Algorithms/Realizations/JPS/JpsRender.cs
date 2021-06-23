using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using PathFinder.Domain.Models.Algorithms.Realizations.AStar;
using PathFinder.Domain.Models.Renders;
using PathFinder.Domain.Models.States;
using PathFinder.Domain.Models.States.CandidateToPrepare;
using PathFinder.Domain.Models.States.PreparedPoint;
using PathFinder.Domain.Models.States.ResultPath;
using PathFinder.Infrastructure;

namespace PathFinder.Domain.Models.Algorithms.Realizations.JPS
{
    public class JpsRender : IRender
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
        
        private RenderedState RenderState(ResultPathState state)
        {
            return new RenderedPathState
            {
                Color = Color.Pink.ToHex(), 
                Path = state.Path
            };
        }

        private RenderedState RenderedState(InformativeState state)
        {
            return new RenderedInformativeState
            {
                Color = GetColorFromInformation(state).ToHex(),
                RenderedPoint = state.CurrentPoint
            };
        }

        private Color GetColorFromInformation(InformativeState state) =>
            state.JumpPointInformation switch
            {
                JumpPointInformation.Vertical => Color.Lime,
                JumpPointInformation.Horizontal => Color.Blue,
                JumpPointInformation.Diagonal => Color.Yellow,
                JumpPointInformation.HorizontalOrVerticalJumpPoints => Color.Coral,
                JumpPointInformation.Goal => Color.Cornsilk,
                _ => throw new ArgumentException($"Can't render this {state.JumpPointInformation}")
            };

        public IAlgorithmReport GetReport()
            => new AlgorithmReport(states);
    }
}