using System;
using System.Collections.Generic;
using System.Text;

namespace Jinhe.CoreFx
{
    public abstract class Dict<TKey, TValue>
    {
        public string DictCode { set; get; }

        public Dict(string code)
        {
            this.DictCode = code;
        }
        
        /// <summary>
        /// 有效的字典代码
        /// </summary>
        protected string VaildDictCode { get;  set; }

        public int Count { get; protected set; }

        protected virtual IDictionary<TKey, TValue> Items { get; set; }

        protected abstract bool Validator(TKey key);

        public TValue this[TKey key]
        {
            set
            {
                if (value != null && Validator(key))
                {
                    if (Items == null) Items = new Dictionary<TKey, TValue>();
                    Items[key] = value;
                    Count++;
                }
            }
            get
            {
                return Items[key];
            }
        }

        public IEnumerable<TKey> GetKeys()
        {
            return Items.Keys;
        }

        public IEnumerable<TValue> GetValues()
        {
            return Items.Values;
        }
    } 
}
