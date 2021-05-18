using System;
using System.Collections.Generic;
using System.Drawing;
using PathFinder.Domain.Models;

namespace PathFinder.Domain.Interfaces
{
    public interface IAlgorithm<out T> 
    {
        string Name { get; }
        IEnumerable<T> Run(IGrid grid, IParameters parameters);

        public Type GetParametersType() => typeof(Parameters);
    }
}