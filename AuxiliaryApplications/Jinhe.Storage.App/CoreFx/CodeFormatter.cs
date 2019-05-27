using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Jinhe.CoreFx
{
    public class CodeFormatter
    {
        private int _maxLength;
        private int[] _countOfSegments;
        private string _code = string.Empty;
        private char _paddingChar;
        private int _segmentCount;

        public int Length
        {
            get
            {
                return _maxLength;
            }
        }

        /// <summary>
        /// 代码的分段长度
        /// </summary>
        /// <param name="paddingChar">默认用0填充</param>
        /// <param name="countOfSegments"></param>
        public CodeFormatter(char paddingChar = '0', params int[] countOfSegments)
        {
            _paddingChar = paddingChar;
            _countOfSegments = countOfSegments;
            if (_countOfSegments == null || _countOfSegments.Length == 0)
            {
                throw new ArgumentException($"没有指定代码的分段长度规则");
            }
            _segmentCount = _countOfSegments.Length;
            _maxLength = countOfSegments.AsEnumerable().Sum();
        }

        public string VaildCode(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return null;
            }
            if (code.Length > _maxLength)
            {
                throw new ArgumentOutOfRangeException($"给定的code({code})长度超出允许的长度范围${_maxLength}");
            }
            code = Format(code);
            var segments = _countOfSegments.Reverse();
            for (var index = 0; index < _segmentCount; index++)
            {
                var currentSegmentLength = segments.ElementAt(index);
                var end = GetPadding(currentSegmentLength);
                if (!code.EndsWith(end))
                {
                    break;
                }
                code = code.TrimEnd(_paddingChar, currentSegmentLength);
            }
            return code;
        }

        public IEnumerable<string> SplitCode(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                yield break;
            }
            var currentLength = 0;
            for (var i = 0; i < _segmentCount; i++)
            {
                currentLength += _countOfSegments[i];
                yield return code.Substring(0, currentLength);
            }
        }

        public string GetParentCode(string code)
        {
            code = Format(code);
            var parentLevel = MaxLevel(code) - 1;
            string result = GetCode(code, parentLevel);
            return Format(result);
        }

        public string GetCode(string code, int level)
        {
            return SplitCode(code).ElementAt(level);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public int MaxLevel(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return 0;
            }

            code = VaildCode(code);
            var length = code.Length;
            var currentLength = 0;
            for (var i = 0; i < _segmentCount; i++)
            {
                currentLength += _countOfSegments[i];
                if (length == currentLength)
                {
                    return i;
                }
            }
            throw new Exception("格式非法，不能格式化");
        }

        /// <summary>
        /// 代码格式化
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string Format(string code)
        {
            if (code == null)
            {
                return null;
            }
            return code.PadRight(_maxLength, _paddingChar).Substring(0, _maxLength);
        }

        public bool Validate(string code)
        {
            if (code == null || code.Length != _maxLength)
            {
                return false;
            }

            return true;
        }

        protected string GetPadding(int length)
        {
            return "".PadRight(length, _paddingChar);
        }

    }
}
