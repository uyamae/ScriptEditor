using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace ScriptEditor
{
    /// <summary>
    /// スクリプトコマンド
    /// </summary>
    public class ScriptCommand
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ScriptCommand()
        {
            Parameters = new ObservableCollection<ScriptParameter>();
        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ScriptCommand(string name)
        {
            CommandName = name;
            Parameters = new ObservableCollection<ScriptParameter>();
        }
        /// <summary>
        /// コマンド名
        /// </summary>
        public string CommandName { get; set; }
        /// <summary>
        /// パラメータリスト
        /// </summary>
        public ObservableCollection<ScriptParameter> Parameters { get; set; }
        /// <summary>
        /// インスタンスの複製
        /// </summary>
        /// <returns></returns>
        public ScriptCommand Clone()
        {
            ScriptCommand cmd = new ScriptCommand(this.CommandName);
            foreach (var param in this.Parameters)
            {
                cmd.Parameters.Add(param.Clone());
            }
            return cmd;
        }
        /// <summary>
        /// テキストを解析してインスタンスを生成する
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static ScriptCommand FromText(string text)
        {
            string[] result = text.Split(' ');
            if (result.Length > 2)
            {
                throw new ArgumentException("{0} : コマンドとパラメータリストの間はスペースで区切ります。", text);
            }
            ScriptCommand cmd = new ScriptCommand();
            cmd.CommandName = result[0].Trim();
            if (result.Length == 2)
            {
                result = result[1].Split(',');
                foreach (var param in result)
                {
                    cmd.Parameters.Add(new ScriptParameter(param.Trim()));
                }
            }
            return cmd;
        }
        /// <summary>
        /// テキストからインスタンス生成
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static ScriptCommand InstantiateFromText(string text)
        {
            // 解析
            var v = FromText(text);
            // インスタンス
            var inst = Clone(v.CommandName);
            // 値をコピー
            for (int i = 0; i < inst.Parameters.Count; ++i)
            {
                inst.Parameters[i].Value = v.Parameters[i].ParamName;
            }
            return inst;
        }
        private static Dictionary<string, ScriptCommand> table = new Dictionary<string, ScriptCommand>();
        /// <summary>
        /// 指定のコマンドの複製を得る
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static ScriptCommand Clone(string command)
        {
            if (table.ContainsKey(command))
            {
                return table[command].Clone();
            }
            return null;
        }
        /// <summary>
        /// テキストを解析してコマンドをコマンドテーブルに追加する
        /// </summary>
        /// <param name="text"></param>
        /// <param name="position"></param>
        public static bool LoadCommandTable(string text, ref int position)
        {
            int pos = position;
            // "コマンド定義" の行かどうか
            string header = "コマンド定義";
            if (!header.SkipIfStartsWith(text, ref pos))
            {
                return false;
            }
            // 改行
            if (text.NotEqualsAt(pos, '\n'))
            {
                return false;
            }
            ++pos;
            // １行ずつコマンド定義を解析
            while (text.NotEqualsAt(pos, '\n'))
            {
                var line = text.SubstringToLineEnd(ref pos);
                var cmd = FromText(line);
                table[cmd.CommandName] = cmd;
            }
            // 改行はスキップする
            if (text.EqualsAt(pos, '\n')) ++pos;
            // 現在の位置を戻す
            position = pos;
            return true;
        }
    }
}
