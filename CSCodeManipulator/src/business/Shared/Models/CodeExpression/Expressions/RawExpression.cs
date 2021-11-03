using Shared.Models.CodeTag;

namespace Shared.Models.CodeExpression.Expressions
{
    public class RawExpression : Expression
    {
        public override int Compile(string code)
        {
            AddPrintTag(new RawTag("code") { Body = code });
            return 1;
        }

        public override bool IsCodeContinuesEligible(string code) => IsCodeStartingEligable(code);

        public override bool IsCodeStartingEligable(string code) => true;
    }
}