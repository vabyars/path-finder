using System;
using System.Collections.Generic;
using System.Drawing;
using PathFinder.Domain.Models.Renders;
using PathFinder.Domain.Models.States;
using PathFinder.Domain.Models.States.CandidateToPrepare;
using PathFinder.Domain.Models.States.PreparedPoint;
using PathFinder.Domain.Models.States.ResultPath;
using PathFinder.Infrastructure;

namespace PathFinder.Domain.Models.Algorithms.Realizations.AStar
{
    public class AStarRender : IRender
    {
        private readonly List<RenderedState> states = new ();
        private static readonly string DefaultCurrentPointColor = Color.Blue.ToHex();
        
        private int index;
        private static readonly List<Color> ColorsToNeighbors = new()
        {
            Color.FromArgb(74, 28, 53),
            Color.FromArgb(99, 40 ,73),
            Color.FromArgb(122, 51, 90),
            Color.FromArgb(148, 64, 110),
            //Color.Purple, Color.Fuchsia, Color.Green, Color.Lime, Color.Black, Color.Violet, Color.Brown,
            Color.Chartreuse,
        };
        
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

        public IAlgorithmReport GetReport()
            => new AlgorithmReport(states);

        private RenderedState RenderState(ResultPathState state)
        {
            return new RenderedPathState
            {
                Color = Color.Yellow.ToHex(),
                Path = state.Path
            };
        }

        private RenderedState RenderState(CurrentPointState state)
        {
            index = 0;
            return new RenderedPreparedPointState
            {
                Color = DefaultCurrentPointColor,
                SecondColor = Color.Red.ToHex(),
                RenderedPoint = state.PreparedPoint
            };
        }

        private RenderedState RenderState(CandidateToPrepareState state)
        {
            var color = ColorsToNeighbors[index++ % ColorsToNeighbors.Count];
            Console.WriteLine($"{color}         {index}");
            return new RenderedCandidateState
            {
                Color = ColorsToNeighbors[index++ % ColorsToNeighbors.Count].ToHex(),//color.ToHex(),
                RenderedPoint = state.Candidate,
                SecondColor = Color.Blue.ToHex()
            };
        }
    }
}