using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Infrastructure.Extensions
{
    public static partial class StringExtension
    {
        public static T ConvertTo<T>(this string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }
    }
}