using Shared;
using Shared.Models.CodeExpression;
using Shared.Models.CodeExpression.Expressions;
using Shared.Models.CodeTag;
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
            new EmptyExpression(),
            new ImportLibraryWithUsingExpression(),
            new NamespaceExpression(),
            //new RawExpression(),
            new VarSimpleAssignExpression(),
            new WhitespaceExpression(),
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
                foreach (var item in expr.InnerExpressions)
                {
                    var tagIndex = expr.PrintTags.FindIndex(m => m is ExpressionRenderTag && (m as ExpressionRenderTag).Properties.First().Value == item.Key);
                    if (tagIndex != -1)
                    {
                        var tag = expr.PrintTags[tagIndex] as ExpressionRenderTag;

                        var res = new List<Tag>();
                        res.AddRange(expr.PrintTags.Take(tagIndex));

                        var x = new Parser();
                        x.Compile(tag.Body);
                        //replace expression with x.Expressions
                        //replace printags with x.PrintTags
                        x.CodeExpressions.ForEach(x => res.AddRange(x.PrintTags));

                        res.AddRange(expr.PrintTags.Skip(tagIndex+1));
                        expr.PrintTags = res;
                    }
                }
            }
        }
    }
}
