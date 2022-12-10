using Core.Domain.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Shared.Core
{
    public interface IDBParam
    {
        /// <summary>
        /// Tên tham số trong function
        /// </summary>
        string ParameterName { get; set; }

        /// <summary>
        /// Kiểu dữ liệu của tham số trong db
        /// </summary>
        string DataTypeName { get; set; }

        /// <summary>
        /// giá trị
        /// </summary>
        object Value { get; set; }
    }
    public class DBParam
    {
        #region Declaration
        DBDataType _DBDataType = DBDataType.none;

        public DBParam(DBDataType dBDataType, string parameterName, object value)
        {
            _DBDataType = dBDataType;
            ParameterName = parameterName;
            Value = value;
        }

        public DBParam(string parameterName, string dataTypeName, object value)
        {
            ParameterName = parameterName;
            DataTypeName = dataTypeName;
            Value = value;
        }

        #endregion

        #region properties
        /// <summary>
        /// Tên tham số trong function
        /// </summary>
        public string ParameterName { get; set; }

        /// <summary>
        /// Kiểu dữ liệu của tham số trong db
        /// </summary>
        public string DataTypeName { get; set; }

        /// <summary>
        /// giá trị
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Có phải dữ liệu kiểu mảng ko
        /// </summary>
        public bool IsArray { get; set; }
        #endregion

        public DBDataType DBDataType
        {
            get
            {
                return _DBDataType;
            }

            set
            {
                _DBDataType = value;
                switch(_DBDataType)
                {
                    case DBDataType.uuid: DataTypeName = DBTypeName.uuid;break; 
                    case DBDataType.numeric: DataTypeName = DBTypeName.numeric; break;
                    case DBDataType.boolean: DataTypeName = DBTypeName.boolean; break;
                    case DBDataType.timestamp: DataTypeName = DBTypeName.timestamp; break;
                    case DBDataType.timestamp_with_time_zone: DataTypeName = DBTypeName.timestamp_with_time_zone; break;
                    case DBDataType.integer: DataTypeName = DBTypeName.integer; break;
                    case DBDataType.bigint: DataTypeName = DBTypeName.bigint; break;
                    case DBDataType.date: DataTypeName = DBTypeName.date; break;
                    case DBDataType.text: DataTypeName = DBTypeName.text; break;
                    case DBDataType.uuid_arr: DataTypeName = DBTypeName.uuid_arr; break;
                    case DBDataType.numeric_arr: DataTypeName = DBTypeName.numeric_arr; break;
                    case DBDataType.boolean_arr: DataTypeName = DBTypeName.boolean_arr; break;
                    case DBDataType.timestamp_arr: DataTypeName = DBTypeName.timestamp_arr; break;
                    case DBDataType.integer_arr: DataTypeName = DBTypeName.integer_arr; break;
                    case DBDataType.bigint_arr: DataTypeName = DBTypeName.bigint_arr; break;
                    case DBDataType.date_arr: DataTypeName = DBTypeName.date_arr; break;
                    case DBDataType.text_arr: DataTypeName = DBTypeName.text_arr; break;
                    case DBDataType.smallint: DataTypeName = DBTypeName.smallint; break;
                    case DBDataType.smallint_arr: DataTypeName = DBTypeName.smallint_arr; break;
                }
            }
        }
    }
}
