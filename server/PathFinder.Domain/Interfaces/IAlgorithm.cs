using System;
using System.Collections.Generic;
using System.Drawing;
using PathFinder.Domain.Models;
using PathFinder.Domain.Models.States;

namespace PathFinder.Domain.Interfaces
{
    public interface IAlgorithm
    {
        string Name { get; }
        IEnumerable<IState> Run(IGrid grid, IParameters parameters);

        public Type GetParametersType() => typeof(Parameters);
    }
}