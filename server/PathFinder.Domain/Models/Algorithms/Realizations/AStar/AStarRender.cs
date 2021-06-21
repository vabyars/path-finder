using System.Collections.Generic;
using System.Drawing;
using PathFinder.Domain.Models.Renders;
using PathFinder.Domain.Models.States;
using PathFinder.Domain.Models.States.CandidateToPrepare;
using PathFinder.Domain.Models.States.PreparedPoint;
using PathFinder.Domain.Models.States.ResultPath;

namespace PathFinder.Domain.Models.Algorithms.Realizations.AStar
{
    public class AStarRender : AbstractRender
    {
        private readonly List<RenderedState> states = new ();
        private static readonly Color DefaultCurrentPointColor = Color.Aqua;
        
        private int index;
        private static readonly List<Color> ColorsToNeighbors = new()
        {
            Color.Aquamarine, Color.Azure, Color.Beige, Color.Bisque, Color.Black, Color.Blue, Color.Brown,
            Color.Chartreuse,
        };
        
        public override RenderedState RenderState(IState state)
        {
            var renderedState = base.RenderState(state);
            states.Add(renderedState);
            return renderedState;
        }

        public override IAlgorithmReport GetReport()
            => new AStarAlgorithmReport {RenderedStates = states};

        protected override RenderedState RenderState(ResultPathState state)
        {
            return new RenderedPathState
            {
                Color = Color.Yellow,
                Path = state.Path
            };
        }

        protected override RenderedState RenderState(CurrentPointState state)
        {
            index = 0;
            return new RenderedPreparedPointState
            {
                Color = DefaultCurrentPointColor,
                SecondColor = Color.Bisque,
                RenderedPoint = state.PreparedPoint
            };
        }

        protected override RenderedState RenderState(CandidateToPrepareState state)
        {
            return new RenderedCandidateState
            {
                Color = ColorsToNeighbors[index++ % ColorsToNeighbors.Count],
                RenderedPoint = state.Candidate,
                SecondColor = Color.Purple
            };
        }
    }
}