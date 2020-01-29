using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api
{
    public static class Utils
    {
        public static bool ContainsStartWith(this IEnumerable<string> list,string start)
        {
            return list.Any(element => element.Trim().ToLower().StartsWith(start));
        }

    }
}
