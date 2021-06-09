using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using PathFinder.Domain.Models.Renders;
using PathFinder.Domain.Models.States;
using PathFinder.Domain.Models.States.CandidateToPrepare;
using PathFinder.Domain.Models.States.PreparedPoint;
using PathFinder.Domain.Models.States.ResultPath;

namespace PathFinder.Domain.Models.Algorithms.Realizations.JPS
{
    public class JpsRender : AbstractRender
    {
        private readonly List<RenderedState> states = new ();
        private int pathLength;
        private int pointsPrepared;

        private int currentPointRedValue = 118;
        private int candidatePointBlueValue = 10;
        
        public override RenderedState RenderState(IState state)
        {
            var renderedState = base.RenderState(state);
            states.Add(renderedState);
            return renderedState;
        }
        protected override RenderedState RenderState(ResultPathState state)
        {
            pathLength = state.Path.Count();
            return new RenderedPathState {Color = Color.Pink, Path = state.Path};
        }

        protected override RenderedState RenderState(CurrentPointState state)
        {
            pointsPrepared++;
            currentPointRedValue += 10;
            return new RenderedPreparedPointState
            {
                RenderedPoint = state.PreparedPoint,
                Color = Color.FromArgb(currentPointRedValue, 0, 255),
                SecondColor = Color.Blue
            };
        }

        protected override RenderedState RenderState(CandidateToPrepareState state)
        {
            candidatePointBlueValue += 5;
            return new RenderedCandidateState
            {
                RenderedPoint = state.Candidate,
                Color = Color.FromArgb(0, 255, candidatePointBlueValue),
                SecondColor = Color.Chocolate
            };
        }

        public override IAlgorithmReport GetReport()
        {
            return new JpsReport
            {
                RenderedStates = states,
                PathLength = pathLength,
                PointsPrepared = pointsPrepared
            };
        }
    }
}