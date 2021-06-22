﻿using System;
using System.Collections.Generic;
using System.Drawing;
using PathFinder.Domain.Models.Renders;
using PathFinder.Domain.Models.States;
using PathFinder.Domain.Models.States.CandidateToPrepare;
using PathFinder.Domain.Models.States.PreparedPoint;
using PathFinder.Domain.Models.States.ResultPath;

namespace PathFinder.Domain.Models.Algorithms.Realizations.AStar
{
    public class AStarRender : IRender
    {
        private readonly List<RenderedState> states = new ();
        private static readonly string DefaultCurrentPointColor = Color.Blue.ToHex();
        
        private int index;
        private static readonly List<Color> ColorsToNeighbors = new()
        {
            Color.Aquamarine, Color.Azure, Color.Beige, Color.Bisque, Color.Black, Color.Blue, Color.Brown,
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
                Color = Color.Blue.ToHex(),
                Path = state.Path
            };
        }

        private RenderedState RenderState(CurrentPointState state)
        {
            index = 0;
            return new RenderedPreparedPointState
            {
                Color = DefaultCurrentPointColor,
                SecondColor = Color.Yellow.ToHex(),
                RenderedPoint = state.PreparedPoint
            };
        }

        private RenderedState RenderState(CandidateToPrepareState state)
        {
            return new RenderedCandidateState
            {
                Color = ColorsToNeighbors[index++ % ColorsToNeighbors.Count].ToHex(),
                RenderedPoint = state.Candidate,
                SecondColor = Color.Blue.ToHex()
            };
        }
    }
    
    public static class ColorExtensions
    {
        public static string ToHex(this Color c) 
            => "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
    }
}