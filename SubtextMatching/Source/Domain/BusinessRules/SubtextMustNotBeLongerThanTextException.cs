using System.Net;

namespace SubtextMatching.Source.Domain.BusinessRules
{
    public class SubtextMustNotBeLongerThanTextException : BusinessRulesException
    {
        private const string message = "Business Rules Exception: Subtext field cannot be longer than text field.";

        public SubtextMustNotBeLongerThanTextException() : base(HttpStatusCode.BadRequest, message) { }
    }
}
