using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photographers
{
    class TagTransformer
    {
        public static string Transform(string value)
        {
            if (!value.StartsWith("#"))
            {
                value = "#" + value;
                
            }

            if (value.Contains(" "))
            {
                value = value.Replace(" ", string.Empty);
                
            }

            if (value.Contains("\t"))
            {
                value = value.Replace("\t", string.Empty);

            }

            if (value.Length > 20)
            {
                value = value.Substring(0, 20);
            }

            return value;
        }
    }
}
