using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Application.Validators
{
    public static class ConsistentKeysValidator
    {
        public static bool Must<T, TT>(params IDictionary<T, TT>[] dicts)
        {

            for (int i = 1; i < dicts.Length; i++)
            {
                var keys1 = dicts[i - 1].Keys.OrderBy(x => x).ToArray();
                var keys2 = dicts[i].Keys.OrderBy(k => k).ToArray();
                if (!keys2.SequenceEqual(keys1))
                    return false;
            }

            return true;
        }
        public static string Message => "All localized fields must have the same set of language codes (keys).";
    }
}