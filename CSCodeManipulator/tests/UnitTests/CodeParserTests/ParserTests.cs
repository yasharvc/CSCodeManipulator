using CodeParser;
using Shared.Models.CodeExpression.Expressions;
using Shared.Models.CodeTag.Tags;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.CodeParserTests
{
    public class ParserTests
    {
        [Fact]
        public async void Should_Parse_Import_Using()
        {
            var code = "using \tSystem ;";
            var parser = new Parser();
            await parser.Compile(code);

            parser.CodeExpressions.Count.ShouldBe(1);
            parser.CodeExpressions[0].Render().ShouldBe(code);
        }

        [Fact]
        public async void Should_Render_Import_Using_With_Updated_Library()
        {
            var code = "using \tSystem ;";
            var parser = new Parser();
            await parser.Compile(code);

            (parser.CodeExpressions[0] as ImportLibraryWithUsingExpression).ChangeLibrary("Shared.Models.CodeExpression.Expressions");
            parser.CodeExpressions[0].Render().ShouldBe("using \tShared.Models.CodeExpression.Expressions ;");
        }


        [Fact]
        public async void Should_Parse_Whitespace()
        {
            var code = "  using \tSystem ;";
            var parser = new Parser();
            await parser.Compile(code);

            parser.CodeExpressions.Count.ShouldBe(2);
            parser.CodeExpressions[0].ShouldBeOfType<WhitespaceExpression>();
            var res = "";

            foreach (var item in parser.CodeExpressions)
            {
                res += item.Render();
            }

            res.ShouldBe(code);
        }


        [Fact]
        public async void Should_Parse_NewLine_At_EOL_As_Whitespace()
        {
            var code = "  using \tSystem ;\r\n";
            var parser = new Parser();
            await parser.Compile(code);

            parser.CodeExpressions.Count.ShouldBe(3);
            parser.CodeExpressions.First().ShouldBeOfType<WhitespaceExpression>();
            parser.CodeExpressions.Last().ShouldBeOfType<WhitespaceExpression>();
            var res = "";

            foreach (var item in parser.CodeExpressions)
            {
                res += item.Render();
            }

            res.ShouldBe(code);
        }
    }
}
