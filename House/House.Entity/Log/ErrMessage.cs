using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace House.Entity
{
    [Serializable]
    public class ErrMessage
    {
        public bool Result { get; set; }
        public string Message { get; set; }
    }
}
