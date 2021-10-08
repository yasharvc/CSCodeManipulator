using Shared.Models.CodeTag;
using Shared.Models.CodeTag.Tags;
using System.IO;
using System.Linq;

namespace Shared.Models.CodeExpression.Expressions
{
    public class ImportLibraryWithUsingExpression : Expression
    {
        public string LibraryName { get; set; }
        public override int Compile(string code)
        {
            var codeTillSemicolon = code.StopWhenFind(';');
            var usingStr = codeTillSemicolon.GetNextToken();
            var whitespaceStr = codeTillSemicolon.GetNextToken(usingStr.Length);
            LibraryName = codeTillSemicolon.GetNextToken(usingStr.Length+whitespaceStr.Length);
            var whitespaceStrAfterlibraryStr = codeTillSemicolon.GetNextToken(usingStr.Length + whitespaceStr.Length + LibraryName.Length);

            AddPrintTag(new KeywordTag { Body = "using" });
            AddPrintTag(new WhitespaceTag { Body = whitespaceStr });
            AddPrintTag(new NamespaceNameTag { Body = LibraryName });
            AddPrintTag(new WhitespaceTag { Body = whitespaceStrAfterlibraryStr });
            AddPrintTag(new KeywordTag { Body = ";" });
            return codeTillSemicolon.Length + 1;
        }

        public override bool IsCodeContinuesEligible(string code)
        {
            var codeTillSemicolon = code.StopWhenFind(';');
            if (codeTillSemicolon.Contains("=") || codeTillSemicolon.Contains("static"))
                return false;
            return true;
        }

        public override bool IsCodeStartingEligable(string code) => code.StartsWith("using");

        public void ChangeLibrary(string newLibrary)
        {
            if (PrintTags.Any())
            {
                LibraryName = newLibrary;
                var tag = PrintTags.Single(m => m is NamespaceNameTag);
                tag.Body = LibraryName;
            }
        }

        public static ImportLibraryWithUsingExpression Create(string libraryName)
        {
            var res = new ImportLibraryWithUsingExpression();
            res.Compile($"using {libraryName};");
            return res;
        }
    }
}