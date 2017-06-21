using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptEditor
{
    public static class StringExt
    {
        /// <summary>
        /// 文字列t のposition の位置に文字列s があればtrue を返し文字数分position を進める
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static bool SkipIfStartsWith(this string s, string t, ref int position)
        {
            int p = 0;
            while (p < s.Length  && position + p < t.Length)
            {
                if (s[p] != t[position + p]) return false;
                ++p;
            }
            position += p;
            return true;
        }
        /// <summary>
        /// 文字列s のposition の文字がc でなければtrue を返す
        /// </summary>
        /// <param name="s"></param>
        /// <param name="c"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static bool NotEqualsAt(this string s, int position, char c)
        {
            return position >= s.Length || s[position] != c;
        }
        /// <summary>
        /// 文字列s のposition の文字がc ならtrue を返す
        /// </summary>
        /// <param name="s"></param>
        /// <param name="c"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static bool EqualsAt(this string s, int position, char c)
        {
            return position < s.Length && s[position] == c;
        }
        /// <summary>
        /// 文字列s のposition から行末までの部分文字列を返し、次の行の先頭もしくはテキストの終端をposition に返す。
        /// </summary>
        /// <param name="s"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static string SubstringToLineEnd(this string s, ref int position)
        {
            int pos = position;
            while (pos < s.Length && s.NotEqualsAt(pos, '\n')) ++pos;
            string t = s.Substring(position, pos - position);
            if (s.EqualsAt(pos, '\n')) ++pos;
            position = pos;
            return t;
        }
    }
}
