using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SubtextMatching.Source.Domain.UseCases.FindMatch
{
    using SubtextMatching.Source.Domain.BusinessRules;
    using SubtextMatching.Source.Domain.Enums;
    using SubtextMatching.Source.Domain.Interfaces;

    public class FindMatchCommand : IRequest<FindMatchCommandResultDto>
    {
        private FindMatchCommandDto dto;
        public FindMatchCommand(FindMatchCommandDto dto) => this.dto = dto;

        public class FindMatchCommandHandler : IRequestHandler<FindMatchCommand, FindMatchCommandResultDto>
        {
            private IMatchingAlgorithmFactory factory;

            public FindMatchCommandHandler(IMatchingAlgorithmFactory factory) => this.factory = factory;

            public async Task<FindMatchCommandResultDto> Handle(FindMatchCommand request, CancellationToken cancellationToken)
            {
                if (request.dto.Subtext.Length > request.dto.Text.Length)
                {
                    throw new SubtextMustNotBeLongerThanTextException();
                }

                var instance = factory.CreateInstance(request.dto.MatchingAlgorithmType);
                var positions = await instance.FindAllOccurencesOfSubtext(request.dto.Text.ToLower(), request.dto.Subtext.ToLower());
                if (positions.Count == 0)
                {
                    throw new SubtextMatchIsNotFoundException();
                }

                var result = new FindMatchCommandResultDto 
                { 
                    SelectedMatchingAlgorithmType = Enum.GetName(typeof(MatchingAlgorithmType), request.dto.MatchingAlgorithmType), 
                    Positions = positions 
                };

                return result;
            }
        }
    }
}
