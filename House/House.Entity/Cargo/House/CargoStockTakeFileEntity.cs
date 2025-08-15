using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace House.Entity.Cargo
{
    [Serializable]
    public class CargoStockTakeFileEntity
    {
        public long ID { get; set; }
        public int StockID { get; set; }
        public string Raw_FileName { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public long FileSize { get; set; }
        public string FileType { get; set; }
        public DateTime OP_DATE { get; set; }
        public string UploadUserID { get; set; }
        public string UploadUserName { get; set; }

        //其它字段
        public string FileUrl { get; set; }

        /// <summary>
        /// 去NULL,替换危险字符
        /// </summary>
        public void EnSafe()
        {
            PropertyInfo[] pSource = this.GetType().GetProperties();

            foreach (PropertyInfo s in pSource)
            {
                if (s.PropertyType.Name.ToUpper().Contains("STRING"))
                {
                    if (s.GetValue(this, null) == null)
                        s.SetValue(this, "", null);
                    else
                        s.SetValue(this, s.GetValue(this, null).ToString().Replace("'", "’"), null);
                }
            }
        }
    }
}
