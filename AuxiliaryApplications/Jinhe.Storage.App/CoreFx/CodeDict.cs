using System;
using System.Collections.Generic;
using System.Text;

namespace Jinhe.CoreFx
{
    /// <summary>
    /// 代码字典
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public class CodeDict<TValue> : Dict<String, TValue>
    {
        private CodeFormatter _codeFormatter = null;
        /// <summary>
        /// 默认9位的code作为key值
        /// </summary>
        public CodeDict(string code) : this(code, 3, 3, 3)
        {
        }

        public CodeDict(string code, params int[] codeFormater) : base(code)
        {
            _codeFormatter = new CodeFormatter('0', codeFormater);
            VaildDictCode = _codeFormatter.VaildCode(code);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected override bool Validator(string key)
        {
            if (key.StartsWith(VaildDictCode))
            {
                if (_codeFormatter.Validate(key))
                {
                    throw new ArgumentException($"输入字典的key格式不合法，要求key的长度为{_codeFormatter.Length}");
                }
                return true;
            }
            else
            {
                throw new ArgumentException("字典项的键值必须以字典代码作为开始字符");
            }
        }
    }
}
