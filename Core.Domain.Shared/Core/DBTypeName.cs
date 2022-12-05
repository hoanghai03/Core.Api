using Core.Domain.Shared.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Shared.Core
{
    public class DBTypeName
    {   /// <summary>
        /// trường hợp kiểu dữ liệu null
        /// </summary>
        public const string none = "";
        public const string uuid = "uuid";
        public const string numeric = "numeric";
        public const string boolean = "boolean";
        public const string timestamp = "timestamp";
        public const string timestamp_with_time_zone = "timestamp with time zone";
        public const string timestamp_without_time_zone = "timestamp without time zone";
        public const string integer = "integer";
        public const string bigint = "bigint";
        public const string date = "date";
        public const string text = "text";
        public const string uuid_arr = "uuid[]";
        public const string numeric_arr = "numeric[]";
        public const string boolean_arr = "boolean[]";
        public const string timestamp_arr = "timestamp[]";
        public const string integer_arr = "integer[]";
        public const string bigint_arr = "bigint[]";
        public const string date_arr = "date[]";
        public const string text_arr = "text[]";
        public const string smallint = "smallint";
        public const string smallint_arr = "smallint[]";

        public static string GetDBTypeName(object value)
        {
            string dataTypeName;
            if(value != null)
            {
                Type typeName = value.GetType();
                dataTypeName = GetDBTypeName(typeName, value);
            } else
            {
                throw new Exception($"DEV DBDataType/GetDBTypeName not support value is null");
            }
            return dataTypeName;
        }

        public static string GetDBTypeName(Type dataType,object value = null)
        {
            string dataTypeName = DBTypeName.none;
            if(dataType != null && dataType.IsArray)
            {
                Type subType;
                if(value != null && value is IList)
                {
                    subType = ((IList)value)[0].GetType();
                } else
                {
                    // chỗ này đang không lấy được type trong trường hợp mảng object
                    subType = dataType.GetElementType();
                }

                dataTypeName = $"{GetDBDataType(subType).ToString()}[]";
            } else
            {
                dataTypeName = GetDBDataType(dataType).ToString();
            }
            return dataTypeName;
        }

        public static DBDataType GetDBDataType(Type dataType)
        {
            DBDataType dataTypeName = DBDataType.none;
            if(dataType != null)
            {
                if(dataType == typeof(long) || dataType == typeof(long?))
                {
                    dataTypeName = DBDataType.bigint;
                } else if (dataType == typeof(Int32) || dataType == typeof(Int32?))
                {
                    dataTypeName = DBDataType.integer;
                }
                else if (dataType == typeof(Int16) || dataType == typeof(Int16?))
                {
                    dataTypeName = DBDataType.smallint;
                }
                else if (dataType == typeof(Int64) || dataType == typeof(Int64?))
                {
                    dataTypeName = DBDataType.bigint;
                }
                else if (dataType == typeof(Guid) || dataType == typeof(Guid?))
                {
                    dataTypeName = DBDataType.uuid;
                }
                else if (dataType == typeof(Boolean) || dataType == typeof(Boolean?))
                {
                    dataTypeName = DBDataType.boolean;
                }
                else if (dataType == typeof(Decimal) || dataType == typeof(Decimal?) || dataType == typeof(Double) || dataType == typeof(Double?))
                {
                    dataTypeName = DBDataType.numeric;
                }
                else if (dataType == typeof(DateTime) || dataType == typeof(DateTime?))
                {
                    dataTypeName = DBDataType.timestamp;
                }
                else if (dataType == typeof(string) || dataType == typeof(String))
                {
                    dataTypeName = DBDataType.text;
                }
                else if (dataType.IsEnum || (Nullable.GetUnderlyingType(dataType) != null && Nullable.GetUnderlyingType(dataType).IsEnum))
                {
                    dataTypeName = DBDataType.integer;
                } else
                {
                    throw new Exception($"DEV : DBTypeName/GetDBDataType not support data type {dataType.Name}");
                }
            } else
            {
                throw new Exception($"DEV : DBTypeName/GetDBDataType not support data type null");
            }
            return dataTypeName;
        }
    }
}
