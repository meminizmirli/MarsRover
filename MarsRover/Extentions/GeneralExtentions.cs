using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Extentions
{
    public static class GeneralExtentions
    {
        public static Array GetEnumValues<TEnum>()
        {
            var enumerationType = typeof(TEnum);

            if (!enumerationType.IsEnum)
            {
                throw new ArgumentException("Exception.");
            }

            return Enum.GetValues(enumerationType);
        }
        public static Dictionary<int, string> EnumSelectAsDictionary<TEnum>()
        {
            var enumValues = GetEnumValues<TEnum>();
            var dictionary = new Dictionary<int, string>();

            foreach (int key in enumValues)
            {
                var value = enumValues.GetValue(key).ToString();
                dictionary.Add(key, value);
            }

            return dictionary;
        }
    }
}
