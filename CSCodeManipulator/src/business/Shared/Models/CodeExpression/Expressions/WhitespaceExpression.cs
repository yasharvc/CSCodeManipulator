using Shared.Models.CodeTag.Tags;
using System;
using System.Linq;

namespace Shared.Models.CodeExpression.Expressions
{
    public class WhitespaceExpression : Expression
    {
        public override int Compile(string code)
        {
            var spaces = code.GetNextToken();
            AddPrintTag(new WhitespaceTag { Body = spaces });
            return spaces.Length;
        }

        public override bool IsCodeContinuesEligible(string code)
        {
            var spaces = code.GetNextToken();
            return spaces.All(c=>char.IsWhiteSpace(c));
        }

        public override bool IsCodeStartingEligable(string code) => code.All(c => char.IsWhiteSpace(c));
        private static WhitespaceExpression newLine = null;
        public static WhitespaceExpression NewLine()
        {
            if (newLine == null)
            {
                newLine = new WhitespaceExpression();
                newLine.Compile(Environment.NewLine);
            }
            return newLine;
        }
    }
}
