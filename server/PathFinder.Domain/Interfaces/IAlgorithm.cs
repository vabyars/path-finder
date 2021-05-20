using System;
using System.Collections.Generic;
using System.Drawing;
using PathFinder.Domain.Models;
using PathFinder.Domain.Models.States;

namespace PathFinder.Domain.Interfaces
{
    public interface IAlgorithm<out T> 
        where T : State
    {
        string Name { get; }
        IEnumerable<T> Run(IGrid grid, IParameters parameters);

        public Type GetParametersType() => typeof(Parameters);
    }
}