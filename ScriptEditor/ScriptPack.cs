using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptEditor
{
    public class ScriptPack
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ScriptPack()
        {
            ScriptList = new ObservableCollection<ScriptData>();
        }
        /// <summary>
        /// スクリプトリスト
        /// </summary>
        public ObservableCollection<ScriptData> ScriptList { get; set; }
        /// <summary>
        /// 文字列を解析してスクリプトパックを作成する
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static ScriptPack FromText(string text)
        {
            int position = 0;
            // コマンド定義
            if (!ScriptCommand.LoadCommandTable(text, ref position))
            {
                return null;
            }
            // スクリプト
            ScriptPack pack = new ScriptPack();
            while (position < text.Length)
            {
                var data = ScriptData.FromText(text, ref position);
                pack.ScriptList.Add(data); 
            }
            return pack;
        }
    }
}
