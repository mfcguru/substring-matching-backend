using System.Collections.Generic;
using System.Threading.Tasks;

namespace SubtextMatching.Source.Infrastructure
{
    using SubtextMatching.Source.Domain.Interfaces;

    public class OffsetBasedMatching : IMatchingAlgorithm
    {
        public async Task<List<int>> FindAllOccurencesOfSubtext(string text, string subtext)
        {
            const int NOTFOUND = -1;
            const bool INFINITE = true;

            return await Task.Run(() =>
            {
                int offset = 0;
                var result = new List<int>();

                while (INFINITE)
                {
                    int position = text.IndexOf(subtext, offset);
                    if (position == NOTFOUND)
                        break;

                    offset = position + 1;
                    result.Add(position);
                }

                return result;
            });
        }
    }
}
