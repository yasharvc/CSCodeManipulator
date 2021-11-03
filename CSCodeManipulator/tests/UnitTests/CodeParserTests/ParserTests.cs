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
            TestForRenderedCode(code, parser);
        }

        [Fact]
        public async void Should_Render_Import_Using_With_Updated_Library()
        {
            var code = "using \tSystem ;";
            var parser = new Parser();
            await parser.Compile(code);

            (parser.CodeExpressions[0] as ImportLibraryWithUsingExpression).ChangeLibrary("Shared.Models.CodeExpression.Expressions");
        }


        [Fact]
        public async void Should_Parse_Whitespace()
        {
            var code = "  using \tSystem ;";
            var parser = new Parser();
            await parser.Compile(code);

            parser.CodeExpressions.Count.ShouldBe(2);
            parser.CodeExpressions[0].ShouldBeOfType<WhitespaceExpression>();

            TestForRenderedCode(code, parser);
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

            TestForRenderedCode(code, parser);
        }

        [Fact]
        public async void Should_Parse_Two_Using()
        {
            var code = "  using \tSystem ;\r\nusing System.Text;";
            var parser = new Parser();
            await parser.Compile(code);

            parser.CodeExpressions.Count.ShouldBe(4);
            parser.CodeExpressions.First().ShouldBeOfType<WhitespaceExpression>();
            parser.CodeExpressions.Last().ShouldBeOfType<ImportLibraryWithUsingExpression>();
            var last = parser.CodeExpressions.Last() as ImportLibraryWithUsingExpression;

            last.LibraryName.ShouldBe("System.Text");

            TestForRenderedCode(code, parser);
        }

        [Fact]
        public async void Should_Parse_namespace()
        {
            var code = "namespace Test.Core{ var x= 10; }";
            var parser = new Parser();
            await parser.Compile(code);

            parser.CodeExpressions.Count.ShouldBe(1);
            parser.CodeExpressions.First().ShouldBeOfType<NamespaceExpression>();
            TestForRenderedCode(code, parser);
        }

        [Fact]
        public async void Should_Parse_namespace_With_Ignoring_Brakets_Inside_Strings()
        {
            //var code = "namespace Test.Core{ void code(){string str = \"}\";} }";
            var code = "namespace Test.Core{ var x= \"}\"; }";
            var parser = new Parser();
            await parser.Compile(code);

            parser.CodeExpressions.Count.ShouldBe(1);
            parser.CodeExpressions.First().ShouldBeOfType<NamespaceExpression>();
            TestForRenderedCode(code, parser);
        }

        private static void TestForRenderedCode(string code, Parser parser)
        {
            var res = "";

            foreach (var item in parser.CodeExpressions)
            {
                res += item.Render();
            }

            res.ShouldBe(code);
        }
    }
}
