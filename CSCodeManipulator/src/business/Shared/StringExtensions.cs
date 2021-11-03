using System;
using System.Collections.Generic;
using System.Linq;

namespace Shared
{
    public static class StringExtensions
    {
        static string Punctuation = "<>{}[]()";
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

            if (char.IsWhiteSpace(code[pos]))
                return GetNextWhiteSpace(code, pos);
            else if (code[pos] == '\'')
                return GetNextCharacter(code, pos);
            else if (code[pos] == '"')
                return GetNextString(code, pos);
            else if (code[pos] == '$' && code[pos + 1] == '\"')
                return GetNextStringWithDoller(code, pos);
            else if (code[pos] == '$' && code[pos + 1] == '\"')
                return GetNextStringWihAtSign(code, pos);
            else if (char.IsDigit(code[pos]))
                return GetNextNumber(code, pos);
            else
                return GetNextExpression(code, pos);

            //var stringChars = "'\"";
            //var isInString = false;
            //var charBefore = ' ';
            //var startingStringChar = ' ';
            //var res = $"{code[pos]}";
            //for (int i = pos + 1; i < code.Length; i++)
            //{
            //    var temp = code[i];
            //    if (stringChars.Contains(temp) && !isInString && charBefore != '\\')
            //    {
            //        isInString = true;
            //        startingStringChar = temp;
            //        charBefore = temp;
            //        res += temp;
            //        continue;
            //    }
            //    if (isInString && temp != startingStringChar)
            //        continue;
            //    else if(isInString && temp == startingStringChar && charBefore != '\\')
            //        isInString = false;

            //    if (char.IsWhiteSpace(temp) ^ isWhitespace)
            //        return res;
            //    res += temp;
            //}
            //return res;
        }

        private static string GetNextExpression(string code, int pos)
        {
            var puctCounts = new List<int>();
            for (int i = 0; i < Punctuation.Length; i++)
            {
                puctCounts.Add(0);
            }

            var res = "";
            var canContinue = true;
            while (canContinue)
            {
                char currentChar = code[pos++];
                if (Punctuation.Contains(currentChar))
                    puctCounts[Punctuation.IndexOf(currentChar)]++;
                if (char.IsWhiteSpace(currentChar) && IsPunctuationDone())
                    break;
                res += currentChar;
                canContinue &= pos < code.Length;
            }
            return res;

            bool IsPunctuationDone()
            {
                var res = 0;
                for (int i = 0; i < Punctuation.Length / 2; i++)
                {
                    res += puctCounts[i * 2] - puctCounts[i * 2 + 1];
                }
                return res == 0;
            }
        }

        private static string GetNextNumber(string code, int pos)
        {
            var res = "";
            while (char.IsDigit(code[pos]))
            {
                res += code[pos++];
            }
            return res;
        }

        private static string GetNextStringWihAtSign(string code, int pos)
            => $"{code[pos]}{GetNextString(code, pos + 1)}";

        private static string GetNextStringWithDoller(string code, int pos) 
            => $"{code[pos]}{GetNextString(code, pos + 1)}";

        private static string GetNextString(string code, int pos)
        {
            var res = $"{code[pos++]}";
            var charBefore = ' ';
            while (!(code[pos] == '"' && (charBefore != '\\' || charBefore != '"')))
            {
                charBefore = code[pos];
                res += code[pos++];
            }
            res += code[pos];
            return res;
        }

        private static string GetNextCharacter(string code, int pos)
        {
            var res = "";
            res += code[pos++];
            if (code[pos] == '\\')
                res += code[pos++];
            res += code[pos++];
            res += code[pos];
            return res;
        }

        public static string GetNextWhiteSpace(this string code, int pos = 0)
        {
            var res = "";
            while (pos < code.Length && char.IsWhiteSpace(code[pos]))
            {
                res += code[pos++];
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
