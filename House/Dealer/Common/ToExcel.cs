using System.Web;
using System.IO;
using NPOI;
using NPOI.HPSF;
using NPOI.HSSF;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.POIFS;
using NPOI.Util;
using System.Text.RegularExpressions;
using System.Data.OleDb;
using System.Data;
using System.Text;
using System;
using System.Collections.Generic;

namespace Dealer
{
    public class ToExcel
    {

        #region 导出EXCEL的公共方法(现有GridView,DataTable,Excel模板三种导出方法)

        /// <summary>DataTable导出Excel 
        ///   
        /// </summary>   
        /// <param name="dtSource">源DataTable</param>   
        /// <param name="strHeaderText">表头文本</param>   
        /// <param name="strFileName">文件名</param>    
        public static void DataTableToExcel(DataTable dtSource, string strHeaderText, string strFileName)
        {
            DownLoadExcel(Export(dtSource, strHeaderText), strFileName);
        }
        #endregion


        #region 导入EXCEL的公共方法(现有DataTable导入方法)
        /// <summary>读取excel导出DataTable   
        /// 默认第一行为标头   
        /// </summary>   
        /// <param name="strFileName">excel文档路径</param>   
        /// <returns></returns>   
        public static DataTable ImportToDataTable(string strFileName)
        {
            DataTable dt = new DataTable();

            HSSFWorkbook hssfworkbook;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                hssfworkbook = new HSSFWorkbook(file);
            }
            HSSFSheet sheet = hssfworkbook.GetSheetAt(0);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            HSSFRow headerRow = sheet.GetRow(0);
            int cellCount = headerRow.LastCellNum;

            for (int j = 0; j < cellCount; j++)
            {
                HSSFCell cell = headerRow.GetCell(j);
                dt.Columns.Add(cell.ToString());
            }

            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                HSSFRow row = sheet.GetRow(i);
                DataRow dataRow = dt.NewRow();

                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                        dataRow[j] = row.GetCell(j).ToString();
                }

                dt.Rows.Add(dataRow);
            }
            return dt;


        }

        /// <summary>
        /// 读取excel导出DataTable 
        /// </summary>
        /// <param name="strFileName">文件名</param>
        /// /// <param name="titleRow">标题行</param>
        /// <param name="startRow">开始行</param>
        /// <returns></returns>
        public static DataTable ImportToDataTable(string strFileName, int titleRow, int startRow)
        {
            DataTable dt = new DataTable();

            HSSFWorkbook hssfworkbook;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                hssfworkbook = new HSSFWorkbook(file);
            }
            HSSFSheet sheet = hssfworkbook.GetSheetAt(0);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            HSSFRow headerRow = sheet.GetRow(titleRow);
            int cellCount = headerRow.LastCellNum;

            for (int j = 0; j < cellCount; j++)
            {
                HSSFCell cell = headerRow.GetCell(j);
                dt.Columns.Add(cell.ToString());
            }

            for (int i = startRow; i <= sheet.LastRowNum; i++)
            {
                HSSFRow row = sheet.GetRow(i);
                DataRow dataRow = dt.NewRow();

                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                        dataRow[j] = row.GetCell(j).ToString();
                }

                dt.Rows.Add(dataRow);
            }
            return dt;


        }

        #endregion


        #region Excel列字母与数字的转换
        /// <summary>由Excel中的列字母转换为数字
        /// 
        /// </summary>
        /// <param name="columnName">列字母</param>
        /// <returns></returns>
        public static int ToIndex(string columnName)
        {
            if (!Regex.IsMatch(columnName.ToUpper(), @"[A-Z]+")) { throw new Exception("invalid parameter"); }

            int index = 0;
            char[] chars = columnName.ToUpper().ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                index += ((int)chars[i] - (int)'A' + 1) * (int)Math.Pow(26, chars.Length - i - 1);
            }
            return index - 1;
        }

        /// <summary>由数字转换为Excel中的列字母
        /// 
        /// </summary>
        /// <param name="index">数字</param>
        /// <returns></returns>
        public static string ToName(int index)
        {
            if (index < 0) { throw new Exception("invalid parameter"); }

            List<string> chars = new List<string>();
            do
            {
                if (chars.Count > 0) index--;
                chars.Insert(0, ((char)(index % 26 + (int)'A')).ToString());
                index = (int)((index - index % 26) / 26);
            } while (index > 0);

            return String.Join(string.Empty, chars.ToArray());
        }

        #endregion


        #region 私有方法,不可调用
        /// <summary>Excel下载
        /// 
        /// </summary>
        /// <param name="fileMenory">Excel文件的MemoryStream</param>
        /// <param name="fileName">文件名,不包括后缀名</param>
        private static void DownLoadExcel(MemoryStream fileMenory, string fileName)
        {

            // 设置编码和附件格式  
            HttpContext curContext = HttpContext.Current;
            curContext.Response.Clear();
            curContext.Response.ClearHeaders();
            curContext.Response.Buffer = false;
            curContext.Response.ContentType = "application/vnd.ms-excel";
            curContext.Response.ContentEncoding = Encoding.UTF8;
            curContext.Response.Charset = "";
            curContext.Response.AppendHeader("Content-Disposition",
                "attachment;filename=" + HttpUtility.UrlEncode(fileName + ".xls", Encoding.UTF8));
            curContext.Response.BinaryWrite(fileMenory.GetBuffer());
            curContext.Response.End();
            //  curContext.Response.Close();
        }

        /// <summary>
        /// 插入单元格数据(供ExamplesToExcel方法使用)
        /// </summary>
        /// <param name="sheet1"></param>
        /// <param name="strInsertCells"></param>
        private static void GenerateData(HSSFWorkbook workbook, HSSFSheet sheet1, DataSet strInsertCells, int startRowIndex)
        {
            int sIndex = startRowIndex;
            HSSFCellStyle percentageStyle = workbook.CreateCellStyle();
            HSSFCellStyle dateStyle = workbook.CreateCellStyle();
            HSSFDataFormat format = workbook.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat("yyyy-MM-dd");
            percentageStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00%");//format.GetFormat("0.000%");
            foreach (DataTable dt in strInsertCells.Tables)
            {
                sIndex = startRowIndex;
                foreach (DataRow it in dt.Rows)
                {
                    HSSFRow row = sheet1.CreateRow(sIndex - 1);

                    foreach (DataColumn dc in dt.Columns)
                    {
                        HSSFCell cell = row.CreateCell(dc.Ordinal);

                        string dcValue = it[dc].ToString();

                        switch (dc.DataType.ToString())
                        {
                            case "System.String"://字符串类型   
                                if (dcValue.EndsWith("%"))
                                {
                                    var parsedValue = .0;
                                    double.TryParse(dcValue.Replace("%", ""), out parsedValue);
                                    cell.SetCellValue(parsedValue / 100);
                                    cell.CellStyle = percentageStyle;
                                }
                                else if (dc.ColumnName == "TodayAcLoad" || dc.ColumnName == "LastAcLoad" || dc.ColumnName == "DailyQuota" || dc.ColumnName == "MonthQuota" || dc.ColumnName == "BorderDevote")
                                {
                                    if (dcValue.Equals("-"))
                                    {
                                        cell.SetCellValue(dcValue);
                                    }
                                    else
                                    {
                                        double V = 0;
                                        double.TryParse(dcValue, out V);
                                        cell.SetCellValue(V);
                                    }
                                }
                                else
                                {
                                    cell.SetCellValue(dcValue);
                                }
                                break;
                            case "System.DateTime"://日期类型   
                                DateTime dateV;
                                DateTime.TryParse(dcValue, out dateV);
                                cell.SetCellValue(dateV);
                                cell.CellStyle = dateStyle;//格式化显示   
                                break;
                            case "System.Boolean"://布尔型   
                                bool boolV = false;
                                bool.TryParse(dcValue, out boolV);
                                cell.SetCellValue(boolV);
                                break;
                            case "System.Int16"://整型   
                            case "System.Int32":
                            case "System.Int64":
                            case "System.Byte":
                                int intV = 0;
                                int.TryParse(dcValue, out intV);
                                cell.SetCellValue(intV);
                                break;
                            case "System.Decimal"://浮点型   
                            case "System.Double":
                                double doubV = 0;
                                double.TryParse(dcValue, out doubV);
                                cell.SetCellValue(doubV);
                                break;
                            case "System.DBNull"://空值处理   
                                cell.SetCellValue("");
                                break;
                            default:
                                cell.SetCellValue("");
                                break;
                        }
                    }
                    sIndex++;
                }

                startRowIndex += 9;
            }

        }

        ///// <summary>
        ///// 插入单元格数据(供ExamplesToExcel方法使用)
        ///// </summary>
        ///// <param name="sheet1"></param>
        ///// <param name="strInsertCells"></param>
        //private static void GenerateData(HSSFSheet sheet1, List<string> strInsertCells)
        //{
        //    HSSFRow row = sheet1.CreateRow(startRowIndex);

        //    foreach (string strInsertCell in strInsertCells)
        //    {
        //        string[] strIce = strInsertCell.Split('|');

        //        HSSFCell cell = row.CreateCell(ToIndex(strIce[0]));

        //        cell.SetCellValue(strIce[2]);
        //        //sheet1.GetRow(int.Parse(strIce[1]) - 1).GetCell(ToIndex(strIce[0])).SetCellValue(strIce[2]);
        //    }
        //}

        /// <summary>
        /// DataTable导出到Excel的MemoryStream 
        /// </summary>   
        /// <param name="dtSource">源DataTable</param>   
        /// <param name="strHeaderText">表头文本</param>    
        /// 
        private static MemoryStream Export(DataTable dtSource, string strHeaderText)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            HSSFSheet sheet = workbook.CreateSheet();

            #region 右击文件 属性信息
            {
                DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                dsi.Company = "";
                workbook.DocumentSummaryInformation = dsi;

                SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                si.Author = "By OA"; //填加xls文件作者信息   
                si.ApplicationName = "By OA"; //填加xls文件创建程序信息   
                si.LastAuthor = "By OA"; //填加xls文件最后保存者信息   
                si.Comments = "By OA"; //填加xls文件作者信息   
                si.Title = strHeaderText; //填加xls文件标题信息   
                si.Subject = strHeaderText;//填加文件主题信息   
                si.CreateDateTime = DateTime.Now;
                workbook.SummaryInformation = si;
            }
            #endregion

            HSSFCellStyle dateStyle = workbook.CreateCellStyle();
            HSSFDataFormat format = workbook.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");

            //取得列宽   
            int[] arrColWidth = new int[dtSource.Columns.Count];
            foreach (DataColumn item in dtSource.Columns)
            {
                arrColWidth[item.Ordinal] = Encoding.GetEncoding(936).GetBytes(item.ColumnName.ToString()).Length;
            }
            for (int i = 0; i < dtSource.Rows.Count; i++)
            {
                for (int j = 0; j < dtSource.Columns.Count; j++)
                {
                    int intTemp = Encoding.GetEncoding(936).GetBytes(dtSource.Rows[i][j].ToString()).Length;
                    if (intTemp > arrColWidth[j])
                    {
                        arrColWidth[j] = intTemp;
                    }
                }
            }



            int rowIndex = 0;

            foreach (DataRow row in dtSource.Rows)
            {
                #region 新建表，填充表头，填充列头，样式
                if (rowIndex == 65535 || rowIndex == 0)
                {
                    if (rowIndex != 0)
                    {
                        sheet = workbook.CreateSheet();
                    }

                    int columnIndex = 0;
                    #region 表头及样式
                    if (!string.IsNullOrEmpty(strHeaderText))
                    {
                        string[] sh = strHeaderText.Split('/');
                        HSSFRow headerRow = sheet.CreateRow(0);
                        headerRow.HeightInPoints = 25;
                        headerRow.CreateCell(0).SetCellValue(sh[0]);

                        HSSFCellStyle headStyle = workbook.CreateCellStyle();
                        headStyle.Alignment = CellHorizontalAlignment.CENTER;
                        HSSFFont font = workbook.CreateFont();
                        font.FontHeightInPoints = 20;
                        font.Boldweight = 700;
                        headStyle.SetFont(font);

                        headerRow.GetCell(0).CellStyle = headStyle;

                        sheet.AddMergedRegion(new Region(0, 0, 0, dtSource.Columns.Count - 1));
                        headerRow.Dispose();

                        columnIndex = 1;
                        if (sh.Length > 1)
                        {
                            HSSFRow headerR = sheet.CreateRow(columnIndex);
                            headerR.HeightInPoints = 25;
                            headerR.CreateCell(0).SetCellValue(sh[1]);

                            HSSFCellStyle headS = workbook.CreateCellStyle();
                            headS.Alignment = CellHorizontalAlignment.CENTER;
                            HSSFFont fonts = workbook.CreateFont();
                            fonts.FontHeightInPoints = 15;
                            fonts.Boldweight = 700;
                            headS.SetFont(fonts);

                            headerR.GetCell(0).CellStyle = headS;

                            sheet.AddMergedRegion(new Region(1, 0, 1, dtSource.Columns.Count - 1));
                            headerR.Dispose();
                            columnIndex = 2;
                        }
                    }
                    #endregion

                    #region 列头及样式
                    {
                        HSSFRow headerRow = sheet.CreateRow(columnIndex);


                        HSSFCellStyle headStyle = workbook.CreateCellStyle();
                        headStyle.Alignment = CellHorizontalAlignment.CENTER;
                        HSSFFont font = workbook.CreateFont();
                        font.FontHeightInPoints = 10;
                        font.Boldweight = 700;
                        headStyle.SetFont(font);


                        foreach (DataColumn column in dtSource.Columns)
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                            headerRow.GetCell(column.Ordinal).CellStyle = headStyle;

                            //设置列宽   
                            sheet.SetColumnWidth(column.Ordinal, (arrColWidth[column.Ordinal] + 1) * 256);

                        }
                        headerRow.Dispose();
                    }
                    #endregion

                    rowIndex = columnIndex + 1;
                }
                #endregion


                #region 填充内容
                HSSFRow dataRow = sheet.CreateRow(rowIndex);
                foreach (DataColumn column in dtSource.Columns)
                {
                    HSSFCell newCell = dataRow.CreateCell(column.Ordinal);

                    string drValue = row[column].ToString();

                    switch (column.DataType.ToString())
                    {
                        case "System.String"://字符串类型   
                            newCell.SetCellValue(drValue);
                            break;
                        case "System.DateTime"://日期类型   
                            DateTime dateV;
                            DateTime.TryParse(drValue, out dateV);
                            newCell.SetCellValue(dateV);

                            newCell.CellStyle = dateStyle;//格式化显示   
                            break;
                        case "System.Boolean"://布尔型   
                            bool boolV = false;
                            bool.TryParse(drValue, out boolV);
                            newCell.SetCellValue(boolV);
                            break;
                        case "System.Int16"://整型   
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV = 0;
                            int.TryParse(drValue, out intV);
                            newCell.SetCellValue(intV);
                            break;
                        case "System.Decimal"://浮点型   
                        case "System.Double":
                            double doubV = 0;
                            double.TryParse(drValue, out doubV);
                            newCell.SetCellValue(doubV);
                            break;
                        case "System.DBNull"://空值处理   
                            newCell.SetCellValue("");
                            break;
                        default:
                            newCell.SetCellValue("");
                            break;
                    }

                }
                #endregion

                rowIndex++;
            }


            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;

                sheet.Dispose();
                //workbook.Dispose();//一般只用写这一个就OK了，他会遍历并释放所有资源，但当前版本有问题所以只释放sheet   
                return ms;
            }

        }
        #endregion

        #region 20120601 新增Excel导入方法(OLEDB包括2003和2007以后的Excel版本)
        /// <summary>
        /// 导入Excel表数据
        /// </summary>
        /// <param name="fileName">文件的绝对路径</param>
        /// <returns></returns>
        public static DataTable ImportToDataTable(string fileName, string StartColumn, string EndColumn)
        {
            string strConnect = GetOledbConnectionString(fileName);
            string sheetName = GetSheetNames(fileName)[0];
            string strSelect = string.Format("select * from[{0}{1}:{2}]", sheetName, StartColumn, EndColumn);  //在模板设定的列数加多一列
            //string strSelect = string.Format("select * from[{0}]", sheetName);
            OleDbConnection execelConn = new OleDbConnection(strConnect);
            execelConn.Open();
            OleDbDataAdapter excelDa = new OleDbDataAdapter(strSelect, execelConn);
            DataSet ds = new DataSet("resDS");
            excelDa.Fill(ds);
            execelConn.Close();
            DataTable dt = ds.Tables[0];

            #region 清除空白的多余数据行或列
            //清除表头的值为空的列
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].ColumnName.Trim() == "" || dt.Columns[i].ColumnName.Trim() == "F" + (i + 1).ToString())
                {
                    dt.Columns.RemoveAt(i);
                    i--;
                }
            }

            //清除整行中所有列都是空的数据
            bool isAllEmpty = true;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                isAllEmpty = true;
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (dt.Rows[i][j].ToString().Trim() != "")
                    {
                        isAllEmpty = false;
                        break;
                    }
                }
                if (isAllEmpty)
                {
                    dt.Rows.RemoveAt(i);
                    i--;
                }
            }
            #endregion
            return ds.Tables[0];
            //  return dt;
        }
        /// <summary>
        /// 返回OLEDB连接字符串
        /// </summary>
        /// <returns></returns>
        private static string GetOledbConnectionString(string fileName)
        {
            string s_OLEDBConnection = string.Empty;

            if (Path.GetExtension(fileName).Equals(".xlsx"))
            {
                s_OLEDBConnection = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + fileName + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';";
                return s_OLEDBConnection;
            }

            if (File.Exists(fileName))
            {
                s_OLEDBConnection = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1;';";
                try
                {
                    // 对于Office2003及以下的Office使用Jet provider
                    using (OleDbConnection conn = new OleDbConnection(s_OLEDBConnection))
                    {
                        conn.Open();
                    }
                }
                catch (InvalidOperationException)
                {
                    // 对于Office2007及以上的Office版本使用Ace provider
                    s_OLEDBConnection = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + fileName + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';";
                    using (OleDbConnection conn = new OleDbConnection(s_OLEDBConnection))
                    {
                        conn.Open();
                    }
                }
            }

            return s_OLEDBConnection;
        }
        /// <summary>
        /// 获取工作表名
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static string[] GetSheetNames(string fileName)
        {
            string strConnect = GetOledbConnectionString(fileName);
            OleDbConnection conn = new OleDbConnection(strConnect);
            conn.Open();
            DataTable dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            conn.Close();
            int sheetsNum = dt.Rows.Count;
            string[] strSheetsName = new string[sheetsNum];
            for (int i = 0; i < sheetsNum; i++)
            {
                strSheetsName[i] = dt.Rows[i][2].ToString();
            }
            return strSheetsName; ;
        }
        #endregion
        /// <summary>
        /// 导入Excel数据 
        /// </summary>
        /// <param name="files"></param>
        public static DataTable ImportExcelData(HttpFileCollection files)
        {
            DataTable data = new DataTable();
            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFile file = files[i] as System.Web.HttpPostedFile;
                if (file.ContentLength == 0) continue;
                string fileName = file.FileName;
                string extenstion = fileName.Substring(fileName.LastIndexOf(".") + 1);//后缀名
                if (extenstion.Equals("xls") || extenstion.Equals("xlsx"))
                {
                    //string sheetName = "sheet1";
                    string sheetName = null;
                    bool isFirstRowColumn = true;
                    HSSFWorkbook workbook = null;
                    
                    HSSFSheet sheet = null;
                    int startRow = 0;

                    //workbook = WorkbookFactory.Create(file.InputStream);
                    workbook = new HSSFWorkbook(file.InputStream);
                    if (sheetName != null)
                    {
                        sheet = workbook.GetSheet(sheetName);
                    }
                    else
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                    if (sheet != null)
                    {
                        HSSFRow firstRow = sheet.GetRow(0);
                        int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数

                        if (isFirstRowColumn)
                        {
                            for (int j = firstRow.FirstCellNum; j < cellCount; ++j)
                            {
                                if (firstRow.GetCell(j) != null)
                                {
                                    DataColumn column = new DataColumn(firstRow.GetCell(j).StringCellValue);
                                    data.Columns.Add(column);
                                }
                                else 
                                {
                                    j--;
                                    cellCount--;
                                }
                            }
                            startRow = sheet.FirstRowNum + 1;
                        }
                        else
                        {
                            startRow = sheet.FirstRowNum;
                        }

                        //最后一列的标号
                        int rowCount = sheet.LastRowNum;
                        for (int j = startRow; j <= rowCount; ++j)
                        {
                            HSSFRow row = sheet.GetRow(j);
                            if (row == null) continue; //没有数据的行默认是null　　　　　　　

                            DataRow dataRow = data.NewRow();
                            for (int k = row.FirstCellNum; k < cellCount; ++k)
                            {
                                //同理，没有数据的单元格都默认是null
                                if (row.GetCell(k) != null)
                                {
                                    dataRow[k] = row.GetCell(k).ToString();
                                }
                            }
                            data.Rows.Add(dataRow);
                        }
                    }

                }
                else
                {
                    string msg = "导入文件的格式不正确,请先下载模板!";
                    break;
                }

            }
            return data;
        }
    }
}