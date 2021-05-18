using System;

namespace PathFinder.Domain.Interfaces
{
    public interface IHasOwnParameters
    {
        Type GetParametersType();
    }
}