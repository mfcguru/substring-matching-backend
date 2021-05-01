using System.Net;

namespace SubtextMatching.Source.Domain.BusinessRules
{
    public class SubstringMatchIsNotFoundException : BusinessRulesException
    {
        private const string message = "Business Rules Exception: Subtext match was not found.";

        public SubstringMatchIsNotFoundException() : base(HttpStatusCode.BadRequest, message) { }
    }
}

