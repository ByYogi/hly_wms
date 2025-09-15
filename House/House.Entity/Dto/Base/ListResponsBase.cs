using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Entity.Dto
{
    public abstract class ListResponsBase<T> : PaginationBase
    {
        public T QueryEntity { get; set; }
        public List<T> Data { get; set; }
        public string ToJSON()
        {
            const string DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss";

            if (Data == null || Data.ToString() == "null")
                return null;

            if (Data is string strData)
                return strData;

            var dtConverter = new IsoDateTimeConverter { DateTimeFormat = DateTimeFormat };

            var obj = new
            {
                rows = Data,
                total = DataTotal
            };

            return JsonConvert.SerializeObject(obj, dtConverter);
        }
    }
}
