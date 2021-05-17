using System;

namespace PathFinder.Domain.Models.Algorithms
{
    public class AlgorithmSelectableAttribute : Attribute
    {
        public string Name { get; }
        public string[] PossibleValues { get; }

        public AlgorithmSelectableAttribute(string name, params string[] possibleValues)
        {
            Name = name;
            PossibleValues = possibleValues;
        }
    }
}