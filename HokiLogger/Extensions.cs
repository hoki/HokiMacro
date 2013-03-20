using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChapterRelics;

namespace HokiMacroLib
{
    public static class Extensions
    {
        public static void Register(this IDictionary<key, IList<Action<KeyArgs>>> dictionary, key key, Action<KeyArgs> action)
        {
            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, new List<Action<KeyArgs>>());
            }
            dictionary[key].Add(action);
        }
    }
}
