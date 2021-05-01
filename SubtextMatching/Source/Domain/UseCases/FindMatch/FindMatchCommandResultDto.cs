using System.Collections.Generic;

namespace SubtextMatching.Source.Domain.UseCases.FindMatch
{
    public class FindMatchCommandResultDto
    {
        public string SelectedMatchingAlgorithmType { get; set; }
        public List<int> Positions { get; set; }
    }
}
