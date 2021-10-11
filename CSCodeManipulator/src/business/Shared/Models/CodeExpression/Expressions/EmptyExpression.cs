using Shared.Models.CodeTag.Tags;

namespace Shared.Models.CodeExpression.Expressions
{
    public class EmptyExpression : Expression
    {
        public override int Compile(string code)
        {
            AddPrintTag(new KeywordTag { Body = ";" });
            return 1;
        }

        public override bool IsCodeContinuesEligible(string code) => IsCodeStartingEligable(code);

        public override bool IsCodeStartingEligable(string code) => code[0] == ';';
    }
}