using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptEditor
{
    /// <summary>
    /// スクリプトパラメータ
    /// </summary>
    public class ScriptParameter
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ScriptParameter()
        {
            Value = string.Empty;
        }
        /// <summary>
        /// パラメータ名を指定するコンストラクタ
        /// </summary>
        public ScriptParameter(string name)
        {
            ParamName = name;
            Value = string.Empty;
        }
        /// <summary>
        /// パラメータ名
        /// </summary>
        public string ParamName { get; set; }
        /// <summary>
        /// パラメータの値
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// インスタンスを複製する
        /// </summary>
        /// <returns></returns>
        public ScriptParameter Clone()
        {
            ScriptParameter param = new ScriptParameter(this.ParamName);
            param.Value = this.Value;
            return param;
        }
    }
}
