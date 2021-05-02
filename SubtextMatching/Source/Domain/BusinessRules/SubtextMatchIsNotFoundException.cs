using System.Net;

namespace SubtextMatching.Source.Domain.BusinessRules
{
    public class SubtextMatchIsNotFoundException : BusinessRulesException
    {
        private const string message = "Business Rules Exception: Subtext match was not found.";

        public SubtextMatchIsNotFoundException() : base(HttpStatusCode.BadRequest, message) { }
    }
}

