using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cargo.Weixin
{
    public interface ITemplate
    {
        /// <summary>
        /// 模板
        /// </summary>
        string Template { get; }
        /// <summary>
        /// 生成内容
        /// </summary>
        /// <returns>string</returns>
        string GenerateContent();
    }
}
