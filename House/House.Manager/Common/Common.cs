using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Manager.Common
{
    public static class Common
    {
        /// <summary>
        /// 遍历DataTable，null转换
        /// </summary>
        /// <param name="dataTable"></param>
        public static void ReplaceDBNullWithDefault(this DataTable dataTable)
        {
            // 遍历所有行
            foreach (DataRow row in dataTable.Rows)
            {
                // 遍历所有列 
                foreach (DataColumn column in dataTable.Columns)
                {
                    // 检查是否为 DBNull
                    if (row[column] == DBNull.Value)
                    {
                        // 根据列数据类型替换
                        switch (Type.GetTypeCode(column.DataType))
                        {
                            case TypeCode.String:
                                row[column] = string.Empty;  // 字符串替换为空
                                break;
                            case TypeCode.Int32:
                            case TypeCode.Double:
                            case TypeCode.Decimal:
                                row[column] = 0;  // 数值类型替换为0
                                break;
                            // 其他类型可扩展
                            default:
                                row[column] = string.Empty;  // 默认替换为空 
                                break;
                        }
                    }
                }
            }
        }
    }
}
