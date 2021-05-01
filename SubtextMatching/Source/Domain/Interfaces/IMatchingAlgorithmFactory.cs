
namespace SubtextMatching.Source.Domain.Interfaces
{
    using SubtextMatching.Source.Domain.Enums;

    public interface IMatchingAlgorithmFactory
    {
        IMatchingAlgorithm CreateInstance(MatchingAlgorithmType type);
    }
}
