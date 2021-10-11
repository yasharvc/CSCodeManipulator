using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public static class StringExtensions
    {
        static string Punctuation = "<>{}[]()";
        //static string Space = " \t\r\n";
        public static string NextWord(this string text,uint startPos = 0)
        {
            var isSpace = char.IsWhiteSpace(text[(int)startPos]);
            var res = "";

            foreach (var ch in text[(int)startPos..])
            {
                if(char.IsWhiteSpace(ch) ^ isSpace)
                    return res;
                res += ch;
                isSpace = char.IsWhiteSpace(ch);
            }
            return res;
        }

        public static string GetNextToken(this string code, int pos = 0)
        {
            if (string.IsNullOrEmpty(code))
                return "";
            if (string.IsNullOrWhiteSpace(code))
                return code;
            if (pos >= code.Length)
                return "";
            var res = $"{code[pos]}";
            var isWhitespace = char.IsWhiteSpace(code[pos]);
            for (int i = pos + 1; i < code.Length; i++)
            {
                var temp = code[i];
                if (char.IsWhiteSpace(temp) ^ isWhitespace)
                    return res;
                res += temp;
            }
            return res;
        }
        public static string StopWhenFind(this string exp, string stopCharsInString, int startPos = 0)
        {
            startPos = startPos < 0 ? 0 : startPos;
            if (startPos >= exp.Length)
                return "";

            var res = "";
            while (startPos < exp.Length)
            {
                if (stopCharsInString.Contains(exp[startPos]))
                    return res;
                res += exp[startPos++];
            }
            return res;
        }
        public static string StopWhenFind(this string exp, params char[] stopChars) => exp.StopWhenFind(new string(stopChars), 0);
    }
}
