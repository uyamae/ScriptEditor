using System;
using System.Collections.Generic;
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
            CommandList = new List<ScriptCommand>();
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
        public List<ScriptCommand> CommandList { get; set; }
    }
}
