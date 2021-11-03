using Shared.Models.CodeTag;
using Shared.Models.CodeTag.Tags;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Shared.Models.CodeExpression.Expressions
{
    public class VarSimpleAssignExpression : Expression
    {
        public string VariableName { get; set; }
        public string RightSide { get; set; }
        public override int Compile(string code)
        {
            var len = "var".Length;
            
            var spacesAfterVar = code.GetNextToken(len);
            len += spacesAfterVar.Length;

            VariableName = code[len..(code.IndexOf("=", len))].Trim();
            len += VariableName.Length;

            var spaceAfterVarName = code.GetNextWhiteSpace(len);
            len += spaceAfterVarName.Length + "=".Length;

            var spaceAfterEqualSign = code.GetNextWhiteSpace(len);
            len += spaceAfterEqualSign.Length;

            RightSide = code.GetNextToken(len);
            len += RightSide.Length;

            var spaceAfterValue = code.GetNextWhiteSpace(len);
            len += spaceAfterValue.Length + 1;
            

            AddPrintTag(new KeywordTag { Body = "var" });
            AddPrintTag(new WhitespaceTag { Body = spacesAfterVar });
            AddPrintTag(new VariableNameTag { Body = VariableName });
            if(spaceAfterVarName.Length > 0)
                AddPrintTag(new WhitespaceTag { Body = spaceAfterVarName });
            AddPrintTag(new EqualSignTag());
            if (spaceAfterEqualSign.Length > 0)
                AddPrintTag(new WhitespaceTag { Body = spaceAfterEqualSign});
            //AddPrintTag(new ExpressionRenderTag { ExpressionKey = "var value", Body = RightSide });
            AddPrintTag(new RawTag("value") { Body = RightSide});
            if(spaceAfterValue.Length > 0)
                AddPrintTag(new WhitespaceTag { Body = spaceAfterValue });
            AddPrintTag(new SemicolonTag());

            return len;
        }

        public override bool IsCodeContinuesEligible(string code) => IsCodeStartingEligable(code) && code.Contains(";") && code.Contains("=") && code.IndexOf("=") < code.IndexOf(";");

        public override bool IsCodeStartingEligable(string code) => code.StartsWith("var");
    }
}