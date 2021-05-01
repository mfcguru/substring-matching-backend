using System;

namespace SubtextMatching.Source.Infrastructure
{
    using SubtextMatching.Source.Domain.Enums;
    using SubtextMatching.Source.Domain.Interfaces;

    public class MatchingAlgorithmFactory : IMatchingAlgorithmFactory
    {
        public IMatchingAlgorithm CreateInstance(MatchingAlgorithmType type)
        {
            switch (type)
            {
                case MatchingAlgorithmType.OffsetBased:
                    return new OffsetBasedMatching();
                case MatchingAlgorithmType.RegexBased:
                    return new RegexBasedMatchng();
                default:
                    throw new ArgumentException("Invalid matching logic type.");
            }
        }
    }
}
