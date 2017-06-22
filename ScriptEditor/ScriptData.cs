using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptEditor
{
    /// <summary>
    /// 一連のスクリプト
    /// </summary>
    public class ScriptData
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ScriptData()
        {
            CommandList = new ObservableCollection<ScriptCommand>();
        }
        /// <summary>
        /// スクリプトID
        /// </summary>
        public string ScriptId { get; set; }
        /// <summary>
        /// スクリプトの説明
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// コマンドリスト
        /// </summary>
        public ObservableCollection<ScriptCommand> CommandList { get; set; }
        /// <summary>
        /// テキストを解析してインスタンスを生成する
        /// </summary>
        /// <param name="script"></param>
        /// <returns></returns>
        public static ScriptData FromText(string script, ref int position)
        {
            int pos = position;
            // ID から始まる
            if (!"ID".SkipIfStartsWith(script, ref pos)) return null;
            if (script.NotEqualsAt(pos, ':')) return null;
            ++pos;
            string id = script.SubstringToLineEnd(ref pos);
            // Desc から始まる
            if (!"Desc".SkipIfStartsWith(script, ref pos)) return null;
            if (script.NotEqualsAt(pos, ':')) return null;
            ++pos;
            string desc = script.SubstringToLineEnd(ref pos);

            ScriptData data = new ScriptData();
            data.ScriptId = id;
            data.Description = desc;
            // 空行までを解析してコマンドを追加する
            while (pos < script.Length && script.NotEqualsAt(pos, '\n'))
            {
                var line = script.SubstringToLineEnd(ref pos);
                data.CommandList.Add(ScriptCommand.InstantiateFromText(line));
            }
            if (script.EqualsAt(pos, '\n')) ++pos;
            position = pos;
            return data;
        }
    }
}
