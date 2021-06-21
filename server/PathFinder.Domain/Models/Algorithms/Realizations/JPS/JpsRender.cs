﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using PathFinder.Domain.Models.Algorithms.Realizations.AStar;
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
            return new RenderedPathState
            {
                Color = Color.Pink.ToHex(), 
                Path = state.Path
            };
        }

        protected override RenderedState RenderState(CurrentPointState state)
        {
            pointsPrepared++;
            currentPointRedValue = Math.Min(currentPointRedValue + 10, 255);
            return new RenderedPreparedPointState
            {
                RenderedPoint = state.PreparedPoint,
                Color = Color.FromArgb(currentPointRedValue, 0, 255).ToHex(),
                SecondColor = Color.Blue.ToHex()
            };
        }
        
        protected override RenderedState RenderState(CandidateToPrepareState state)
        {
            candidatePointBlueValue = Math.Min(candidatePointBlueValue + 5, 255);
            return new RenderedCandidateState
            {
                RenderedPoint = state.Candidate,
                Color = Color.FromArgb(0, 255, candidatePointBlueValue).ToHex(),
                SecondColor = Color.Chocolate.ToHex()
            };
        }

        public override IAlgorithmReport GetReport()
            => new AlgorithmReport(states);
    }
}