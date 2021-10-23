using Shared.Models.CodeTag;
using Shared.Models.CodeTag.Tags;
using System.IO;
using System.Linq;

namespace Shared.Models.CodeExpression.Expressions
{
    public class NamespaceExpression : Expression
    {
        public string NamespaceName { get; set; }
        public override int Compile(string code)
        {
            var codeTillBlockStart = code.StopWhenFind('{');
            var namespaceStr = codeTillBlockStart.GetNextToken();
            var whitespaceStr = codeTillBlockStart.GetNextToken(namespaceStr.Length);
            NamespaceName = codeTillBlockStart.GetNextToken(namespaceStr.Length+whitespaceStr.Length);
            var whitespaceStrAfterlibraryStr = codeTillBlockStart.GetNextToken(namespaceStr.Length + whitespaceStr.Length + NamespaceName.Length);

            var body = "";
            var cnt = 0;

            var charBefore = ' ';
            var isInString = false;
            var stringChars = "'\"";
            var startingStringChar = ' ';

            foreach (var item in code[codeTillBlockStart.Length..])
            {
                body += item;
                if (stringChars.Contains(item) && !isInString && charBefore != '\\') {
                    isInString = true;
                    startingStringChar = item;
                    continue;
                }
                if (isInString && item != startingStringChar)
                    continue;

                isInString = false;

                if (item == '{')
                    cnt++;
                else if (item == '}')
                    cnt--;
                if (cnt == 0)
                    break;
                charBefore = item;
            }

            AddPrintTag(new KeywordTag { Body = "namespace" });
            AddPrintTag(new WhitespaceTag { Body = whitespaceStr });
            AddPrintTag(new NamespaceNameTag { Body = NamespaceName });
            AddPrintTag(new WhitespaceTag { Body = whitespaceStrAfterlibraryStr });
            AddPrintTag(new BlockStartTag());
            AddPrintTag(new ExpressionRenderTag { ExpressionKey = "namespace body", Body = body[1..^1] });
            AddPrintTag(new BlockStartTag());
            return codeTillBlockStart.Length + body.Length;
        }

        public override bool IsCodeContinuesEligible(string code) => IsCodeStartingEligable(code);

        public override bool IsCodeStartingEligable(string code) => code.StartsWith("namespace");
    }
}