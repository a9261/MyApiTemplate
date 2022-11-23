using System.Text;
using System.Text.RegularExpressions;

namespace Domain.ExtensionMethod
{
    public static partial class StringExtension
    {
        public static string ToSimpleSqlAnd(this string data, object parameter)
        {
            if (!data.Contains("1=1"))
            {
                throw new Exception("SQL Text need contain WHERE 1=1");
            }
            var properties = parameter.GetType().GetProperties();
            foreach (var property in properties)
            {
                if (property.GetValue(parameter) != null)
                {
                    data += $" AND {property.Name}=@{property.Name}";
                }
            }
            return data;
        }

        /// <summary>
        /// 將字串雜湊為MD5
        /// </summary>
        /// <param name="data"></param>
        /// <param name="encode">Default is UTF8</param>
        /// <returns></returns>
        public static string ToMD5(this string data, Encoding? encode = null)
        {
            if (encode == null)
            {
                encode = Encoding.UTF8;
            }
            byte[] bytes = encode.GetBytes(data);
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] result = md5.ComputeHash(bytes);
            return System.BitConverter.ToString(result).Replace("-", "");
        }

        /// <summary>
        /// 清除字串中的不需要字元
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ToCleanString(this string data, string replaceString = "")
        {
            return data.Trim().Replace(replaceString, "");
        }

        /// <summary>
        /// 是否為空字串
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool IsEmpty(this string data)
        {
            return string.IsNullOrEmpty(data);
        }

        /// <summary>
        /// 大小寫數字 ， 有包含上列以外的回傳 FALSE
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool IsNumberAndEnglish(this string data)
        {
            string pattern = @"^[a-zA-Z0-9]+$";
            return Regex.Match(data, pattern).Success;
        }

        /// <summary>
        /// 大小寫數字以及底線 中線 小數點  ， 有包含上列以外的回傳 FALSE
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool IsNumberAndEnglishAndSafeSymbol(this string data)
        {
            string pattern = @"^[a-zA-Z0-9-_.]+$";
            return Regex.Match(data, pattern).Success;
        }

        /// <summary>
        /// 是否為GBP Time
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool IsGBPTime(this string data)
        {
            if (data.Length < 14)
            {
                return false;
            }
            data = data.Insert(4, "-").Insert(7, "-").Insert(10, " ").Insert(13, ":").Insert(16, ":");
            return DateTime.TryParse(data, out DateTime result);
        }

        /// <summary>
        /// GBP Time 轉回 dateTime
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static DateTime GBPTimeToDatatime(this string data)
        {
            if (data.Length < 14)
            {
                return DateTime.MinValue;
            }

            data = data.Insert(4, "-").Insert(7, "-").Insert(10, " ").Insert(13, ":").Insert(16, ":");
            DateTime.TryParse(data, out DateTime result);
            return result;
        }
    }
}