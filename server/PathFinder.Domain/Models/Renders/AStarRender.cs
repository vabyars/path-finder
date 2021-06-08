using System;
using System.Collections.Generic;
using System.Drawing;
using PathFinder.Domain.Models.Algorithms.AStar;
using PathFinder.Domain.Models.Algorithms.IDA;
using PathFinder.Domain.Models.Algorithms.JPS;
using PathFinder.Domain.Models.States;

namespace PathFinder.Domain.Models.Renders
{
    public class AStarRender : Render
    {
        public AStarRender() : base(new[] {nameof(AStarAlgorithm), nameof(JpsDiagonal), nameof(IDA)})
        {
        }
    }

    public class RenderedState
    {
        public Color Color { get; set; }
    }

    public class PreparedPointRenderedState : RenderedState
    {
        public Color SecondColor { get; set; }
        public Point RenderedPoint { get; set; }
    }
    
    public class RenderedPathState : RenderedState
    {
        public IEnumerable<Point> Path { get; set; }
    }

    public class CandidateToRenderState : RenderedState
    {
        public Color SecondColor { get; set; }
        public Point RenderedPoint { get; set; }
    }

    public interface IRender
    {
        List<RenderedState> States { get; set; }
        RenderedState RenderState(ResultPathState state);
        RenderedState RenderState(CurrentPointState state);
        RenderedState RenderState(CandidateToPrepareState state);
        void RenderState(State state);
    }

    public class AStarRenderNew : IRender
    {
        public List<RenderedState> States { get; set; } = new ();
        private static readonly Color DefaultCurrentPointColor = Color.Aqua;

        private int index;
        private static readonly List<Color> ColorsToNeighbors = new()
        {
            Color.Aquamarine, Color.Azure, Color.Beige, Color.Bisque, Color.Black, Color.Blue, Color.Brown,
            Color.Chartreuse,
        };

        public RenderedState RenderState(ResultPathState state)
        {
            return new RenderedPathState
            {
                Color = Color.Yellow,
                Path = state.Path
            };
        }

        public RenderedState RenderState(CurrentPointState state)
        {
            index = 0;
            return new PreparedPointRenderedState
            {
                Color = DefaultCurrentPointColor,
                SecondColor = Color.Bisque,
                RenderedPoint = state.PreparedPoint
            };
        }

        public RenderedState RenderState(CandidateToPrepareState state)
        {
            return new CandidateToRenderState
            {
                Color = ColorsToNeighbors[index++ % ColorsToNeighbors.Count],
                RenderedPoint = state.Candidate,
                SecondColor = Color.Purple
            };
        }

        public void RenderState(State state)
        {
            States.Add(state switch
            {
                CurrentPointState s => RenderState(s),
                CandidateToPrepareState s => RenderState(s),
                ResultPathState s => RenderState(s),
                _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
            });
        }
    }


    public class CurrentPointState : State
    {
        public Point PreparedPoint { get; set; }
    }

    public class CandidateToPrepareState : State
    {
        public Point Candidate { get; set; }
    }

    public class ResultPathState : State
    {
        public IEnumerable<Point> Path { get; set; }
    }
}