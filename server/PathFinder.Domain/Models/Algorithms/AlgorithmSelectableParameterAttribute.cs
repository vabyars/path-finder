using System;

namespace PathFinder.Domain.Models.Algorithms
{
    [AttributeUsage(AttributeTargets.Property)]
    public class AlgorithmSelectableParameterAttribute : Attribute
    {
        public string DisplayName { get; }
        public string[] PossibleValues { get; }

        public AlgorithmSelectableParameterAttribute(string displayName, params string[] possibleValues)
        {
            DisplayName = displayName;
            PossibleValues = possibleValues;
        }
    }
}