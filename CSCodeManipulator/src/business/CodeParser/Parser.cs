using Shared;
using Shared.Models.CodeExpression;
using Shared.Models.CodeExpression.Expressions;
using Shared.Models.CodeTag.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeParser
{
    public class Parser
    {
        List<Expression> AvailableExpressions => new()
        {
            new ImportLibraryWithUsingExpression(),
            new WhitespaceExpression(),
            new EmptyExpression(),
            new NamespaceExpression()
        };

        public List<Expression> CodeExpressions { get; private set; }

        public Parser()
        {
        }

        public Task Compile(string code)
        {
            CodeExpressions = new List<Expression>();
            var pos = 0;
            while (pos < code.Length)
            {
                var word = code[pos..].GetNextToken();
                
                var selectedExpressions = AvailableExpressions.Where(x => x.IsCodeStartingEligable(word));
                selectedExpressions = selectedExpressions.AsParallel().Where(x => x.IsCodeContinuesEligible(code[pos..])).ToList();
                if (selectedExpressions.Count() != 1)
                    throw new NotImplementedException();
                var expr = selectedExpressions.First();
                pos += expr.Compile(code[pos..]);
                ProcessInnerExpressions(expr);
                CodeExpressions.Add(expr);
                //code = code[pos..];
            }
            return Task.CompletedTask;
        }

        private void ProcessInnerExpressions(Expression expr)
        {
            if (expr.InnerExpressions.Any())
            {
                var lst = expr.PrintTags.Single(m=>m is ExpressionRenderTag && (m as ExpressionRenderTag).Properties.First().Value == expr.)
            }
        }
    }
}
