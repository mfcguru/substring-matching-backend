using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SubtextMatching.Source.Infrastructure
{
    using SubtextMatching.Source.Domain.Interfaces;

    public class RegexBasedMatchng : IMatchingAlgorithm
    {
        public async Task<List<int>> FindAllOccurencesOfSubtext(string text, string subtext)
        {
            return await Task.Run(() =>
            {
                return Regex.Matches(text, subtext).Cast<Match>().Select(m => m.Index).ToList();
            });
        }
    }
}
