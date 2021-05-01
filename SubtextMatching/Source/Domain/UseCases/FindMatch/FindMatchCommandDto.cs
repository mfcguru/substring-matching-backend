using System.ComponentModel.DataAnnotations;

namespace SubtextMatching.Source.Domain.UseCases.FindMatch
{
    using SubtextMatching.Source.Domain.Enums;

    public class FindMatchCommandDto
    {
        [Required]
        public string Text { get; set; }

        [Required]
        public string Subtext { get; set; }

        [Required]
        [Range(0, 1)]
        public MatchingAlgorithmType MatchingAlgorithmType { get; set; }
    }
}
