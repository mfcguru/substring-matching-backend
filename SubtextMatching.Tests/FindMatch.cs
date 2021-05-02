using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SubtextMatching.Tests
{
    using SubtextMatching.Source.Domain.BusinessRules;
    using SubtextMatching.Source.Domain.Enums;
    using SubtextMatching.Source.Domain.Interfaces;
    using SubtextMatching.Source.Domain.UseCases.FindMatch;
    using SubtextMatching.Source.Infrastructure;

    [TestClass]
    public class FindMatch
    {
        [TestMethod]
        [ExpectedException(typeof(SubtextMatchIsNotFoundException))]
        public async Task SubtextMatchIsNotFound()
        {
            // setup mock algorithm to
            // return an empty list of positions
            var mockAlogirthm = new Mock<IMatchingAlgorithm>();
            mockAlogirthm.Setup(o => o.FindAllOccurencesOfSubtext(It.IsAny<string>(), It.IsAny<string>()))
                .Returns<string, string>((text, subtext) => Task.FromResult(new List<int>()));

            // setup mock factory for dependency injection
            var mockFactory = new Mock<IMatchingAlgorithmFactory>();
            mockFactory.Setup(o => o.CreateInstance(It.IsAny<MatchingAlgorithmType>()))
                .Returns<MatchingAlgorithmType>((type) => mockAlogirthm.Object);

            // execute use case
            var dto = new FindMatchCommandDto { Text = "AAAA", Subtext = "xxxx", MatchingAlgorithmType = MatchingAlgorithmType.OffsetBased };
            var command = new FindMatchCommand(dto);
            var handler = new FindMatchCommand.FindMatchCommandHandler(mockFactory.Object);
            await handler.Handle(command, new CancellationToken());
        }

        [TestMethod]
        [ExpectedException(typeof(SubtextMustNotBeLongerThanTextException))]
        public async Task SubtextMustNotBeLongerThanText()
        {
            // setup mock factory for dependency injection
            var mockFactory = new Mock<IMatchingAlgorithmFactory>();
            var dto = new FindMatchCommandDto { Text = "AAAA", Subtext = "XXXXXXXX", MatchingAlgorithmType = MatchingAlgorithmType.OffsetBased };
            var command = new FindMatchCommand(dto);
            var handler = new FindMatchCommand.FindMatchCommandHandler(mockFactory.Object);

            // execute use case
            await handler.Handle(command, new CancellationToken());
        }

        [TestMethod]
        public async Task Success_OffsetBasedMatching()
        {
            // setup mock factory for dependency injection
            // this will use OffsetBased matching algorithm
            var mockFactory = new Mock<IMatchingAlgorithmFactory>();
            mockFactory.Setup(o => o.CreateInstance(It.IsAny<MatchingAlgorithmType>()))
                .Returns<MatchingAlgorithmType>((type) => new OffsetBasedMatching());

            // execute use case
            var dto = new FindMatchCommandDto { Text = "AAAA", Subtext = "a", MatchingAlgorithmType = MatchingAlgorithmType.OffsetBased };
            var command = new FindMatchCommand(dto);
            var handler = new FindMatchCommand.FindMatchCommandHandler(mockFactory.Object);
            var result = await handler.Handle(command, new CancellationToken());

            // assert returned result contain valida data
            Assert.AreEqual(result.Positions.Count, 4);
            Assert.IsTrue(result.SelectedMatchingAlgorithmType == "OffsetBased");
        }

        [TestMethod]
        public async Task Success_RegexBasedMatching()
        {
            // setup mock factory for dependency injection
            // this will use RegexBased matching algorithm
            var mockFactory = new Mock<IMatchingAlgorithmFactory>();
            mockFactory.Setup(o => o.CreateInstance(It.IsAny<MatchingAlgorithmType>()))
                .Returns<MatchingAlgorithmType>((type) => new RegexBasedMatchng());

            // execute use case
            var dto = new FindMatchCommandDto { Text = "AAAA", Subtext = "a", MatchingAlgorithmType = MatchingAlgorithmType.RegexBased };
            var command = new FindMatchCommand(dto);
            var handler = new FindMatchCommand.FindMatchCommandHandler(mockFactory.Object);
            var result = await handler.Handle(command, new CancellationToken());

            // assert returned result contain valida data
            Assert.AreEqual(result.Positions.Count, 4);
            Assert.IsTrue(result.SelectedMatchingAlgorithmType == "RegexBased");
        }
    }
}
