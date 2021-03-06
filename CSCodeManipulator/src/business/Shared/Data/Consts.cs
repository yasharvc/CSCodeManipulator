using Shared.Models;
using System.Collections.Generic;

namespace Shared.Data
{
    public sealed class Consts
    {
        public static IEnumerable<TypeDescription> GetBaseTypeNames() => new List<TypeDescription>
            {
                new TypeDescription("string"),
                new TypeDescription("char"),
                new TypeDescription("sbyte"),
                new TypeDescription("byte"),
                new TypeDescription("short"),
                new TypeDescription("ushort"),
                new TypeDescription("int"),
                new TypeDescription("uint"),
                new TypeDescription("long"),
                new TypeDescription("ulong"),
                new TypeDescription("float"),
                new TypeDescription("double"),
                new TypeDescription("decimal"),
                new TypeDescription("bool"),
                new TypeDescription("object"),
                new TypeDescription("dynamic")
            };

        public static IEnumerable<TypeDescription> GetDotNetBaseTypeNames() => new List<TypeDescription>
            {
                new TypeDescription("System","String"),
                new TypeDescription("System","Char"),
                new TypeDescription("System","SByte"),
                new TypeDescription("System","Byte"),
                new TypeDescription("System","Int16"),
                new TypeDescription("System","UInt16"),
                new TypeDescription("System","Int32"),
                new TypeDescription("System","UInt32"),
                new TypeDescription("System","Int64"),
                new TypeDescription("System","UInt64"),
                new TypeDescription("System","Single"),
                new TypeDescription("System","Double"),
                new TypeDescription("System","Decimal"),
                new TypeDescription("System","Boolean"),
                new TypeDescription("System","Object")
            };

        public static IEnumerable<string> LanguageKeywords() => new List<string>
        {
            "abstract",
            "as",
            "base",
            "break",
            "case",
            "catch",
            "checked",
            "class",
            "const",
            "continue",
            "default",
            "delegate",
            "do",
            "else",
            "enum",
            "event",
            "explicit",
            "extern",
            "false",
            "finally",
            "fixed",
            "for",
            "foreach",
            "goto",
            "if",
            "implicit",
            "in",
            "interface",
            "internal",
            "is",
            "lock",
            "namespace",
            "new",
            "null",
            "operator",
            "out",
            "override",
            "params",
            "private",
            "protected",
            "public",
            "readonly",
            "ref",
            "return",
            "sealed",
            "sizeof",
            "stackalloc",
            "static",
            "struct",
            "switch",
            "this",
            "throw",
            "true",
            "try",
            "typeof",
            "unchecked",
            "unsafe",
            "using",
            "virtual",
            "void",
            "volatile",
            "while",
            "nameof",
            "dynamic",
            "partial",
            "async",
            "await",
            "get",
            "set",
            "var",
            "when",
            "yield"
        };
    }
}
