using System.Collections.Generic;
using System.Threading.Tasks;

namespace SubtextMatching.Source.Domain.Interfaces
{
    public interface IMatchingAlgorithm
    {
        Task<List<int>> FindAllOccurencesOfSubtext(string text, string subtext);
    }
}
