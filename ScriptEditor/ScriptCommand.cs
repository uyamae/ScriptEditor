using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptEditor
{
    public class ScriptCommand
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ScriptCommand()
        {
            Parameters = new List<ScriptParameter>();
        }
        /// <summary>
        /// コマンド名
        /// </summary>
        public string CommandName { get; set; }
        /// <summary>
        /// パラメータリスト
        /// </summary>
        public List<ScriptParameter> Parameters { get; set; }
    }
}
