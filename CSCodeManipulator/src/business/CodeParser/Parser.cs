﻿using Shared;
using Shared.Models.CodeExpression;
using Shared.Models.CodeExpression.Expressions;
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
            new WhitespaceExpression()
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
                CodeExpressions.Add(expr);
                //code = code[pos..];
            }
            return Task.CompletedTask;
        }

        
    }
}
