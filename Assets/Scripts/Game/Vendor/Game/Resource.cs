using System.Collections.Generic;
using MVP.Framework.Core;

namespace Vendor.Game
{
    public class Resource
    {
        private static Dictionary<string, List<string>> map = new Dictionary<string, List<string>>();
        public static List<string> Level1 = new List<string>()
        {
            "Stages/Level1",
        };
        public static List<string> Level2 = new List<string>()
        {
            "Stages/Level2",
        };

        public static List<string> Get(string name)
        {
            if (map.ContainsKey(name)) return map[name];

            var list = Reflection.GetStaticFieldRef<List<string>, Resource>(name);
            if (list == null) list = new List<string>();

            map.Add(name, list);
            return list;
        }
    }
}