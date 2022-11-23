using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Infrastructure.Extensions
{
    public static class DapperExtensionMethod
    {
        public static DynamicParameters ToDynamicParameters(this object item)
        {
            var parameters = new DynamicParameters();
            var properties = item.GetType().GetProperties();
            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(item);

                if (propertyValue != null)
                {
                    if (propertyValue is string strValue)
                    {
                        parameters.Add(property.Name, new DbString()
                        {
                            Value = strValue,
                            IsAnsi = true
                        });
                    }
                    else
                    {
                        parameters.Add(property.Name, propertyValue);
                    }
                }
            }
            return parameters;
        }

        /// <summary>
        ///     Length of the string is default 4000
        /// </summary>
        public static DbString ToVarchar(this string me)
        {
            if (string.IsNullOrEmpty(me))
            {
                return null;
            }
            return new DbString { Value = me, IsAnsi = true };
        }

        /// <summary>
        ///     Length of the string -1 for max
        /// </summary>
        public static DbString ToVarchar(this string me, int length)
        {
            if (string.IsNullOrEmpty(me))
            {
                return null;
            }
            return new DbString { Value = me, Length = length, IsAnsi = true };
        }

        /// <summary>
        ///     Length of the string is default 4000
        /// </summary>
        public static DbString ToChar(this string me)
        {
            if (string.IsNullOrEmpty(me))
            {
                return null;
            }
            return new DbString { Value = me, IsAnsi = true, IsFixedLength = true };
        }

        /// <summary>
        ///     Length of the string -1 for max
        /// </summary>
        public static DbString ToChar(this string me, int length)
        {
            if (string.IsNullOrEmpty(me))
            {
                return null;
            }
            return new DbString { Value = me, Length = length, IsAnsi = true, IsFixedLength = true };
        }

        /// <summary>
        ///     Length of the string is default 4000
        /// </summary>
        public static DbString ToNVarchar(this string me)
        {
            if (string.IsNullOrEmpty(me))
            {
                return null;
            }
            return new DbString { Value = me };
        }

        /// <summary>
        ///     Length of the string -1 for max
        /// </summary>
        public static DbString ToNVarchar(this string me, int length)
        {
            if (string.IsNullOrEmpty(me))
            {
                return null;
            }
            return new DbString { Value = me, Length = length };
        }

        /// <summary>
        ///     Length of the string is default 4000
        /// </summary>
        public static DbString ToNChar(this string me)
        {
            if (string.IsNullOrEmpty(me))
            {
                return null;
            }
            return new DbString { Value = me, IsFixedLength = true };
        }

        /// <summary>
        ///     Length of the string -1 for max
        /// </summary>
        public static DbString ToNChar(this string me, int length)
        {
            if (string.IsNullOrEmpty(me))
            {
                return null;
            }
            return new DbString { Value = me, Length = length, IsFixedLength = true };
        }
    }
}