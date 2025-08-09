using Cargo.Interface.Base;
using House.Business.Cargo;
using House.Entity;
using House.Entity.Cargo.Interface;
using NPOI.HSSF.Record.Formula.Functions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace Cargo.Interface
{
    /// <summary>
    /// StockApi 的摘要说明
    /// </summary>
    public class StockApi : BaseApi
    {

        //public void ProcessRequest(HttpContext context)
        //{
        //    context.Response.ContentType = "text/plain";
        //    context.Response.Write("Hello World");
        //}

        protected override void HandleRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";

            string cmd = context.Request.Headers["apiName"];
            MethodInfo Method = this.GetType().GetMethod(cmd, BindingFlags.NonPublic | BindingFlags.Instance);//通过反射机制,直接对应到相应的方法
            if (Method != null)
            {
                Method.Invoke(this, new object[] { context });
            }
            else
            {
                context.Response.Write("传入参数不正确");
            }

        }
        private void WriteTextLog(string strMessage, DateTime time)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"System\Log\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string fileFullPath = path + time.ToString("yyyy-MM-dd") + ".System.txt";
            StringBuilder str = new StringBuilder();
            str.Append("Time:    " + time.ToString() + "\r\n");
            str.Append("Message: " + strMessage + "\r\n");
            str.Append("-----------------------------------------------------------\r\n\r\n");
            StreamWriter sw;
            if (!File.Exists(fileFullPath))
            {
                sw = File.CreateText(fileFullPath);
            }
            else
            {
                sw = File.AppendText(fileFullPath);
            }
            sw.WriteLine(str.ToString());
            sw.Close();
        }

        private void queryCargoStock(HttpContext context)
        {
            CargoInterfaceBus bus = new CargoInterfaceBus();

            Result1.Success = true;
            Result1.Message = "成功";
            using (StreamReader reader = new StreamReader(context.Request.InputStream))
            {
                string requestBody = reader.ReadToEnd();
                WriteTextLog(requestBody,DateTime.Now);
                if (!string.IsNullOrEmpty(requestBody))
                {
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    CargoHouseBus house = new CargoHouseBus();
                    ResponseEntity entity= js.Deserialize<ResponseEntity>(JSON.Encode(requestBody));
                    if (entity.DataList.Count>0)
                    {
                        int ContainerID = house.QueryCargoContainerGoodsContainerID(92);
                        foreach (var item in entity.DataList)
                        {
                            if (bus.ExistsGoodCode(item.GoodsCode.Trim()))
                            {
                                StockApiEntity stock = bus.QueryInventory(item.GoodsCode.Trim());
                                bus.UpdateInventory(new StockApiEntity()
                                {
                                    ContainerID= ContainerID,
                                    OID= stock.OID,
                                    StockNum= item.StockNum,
                                    GoodsCode= item.GoodsCode.Trim(),
                                    Batch= item.Batch,
                                    TypeID= item.TypeID,
                                    SalePrice= item.SalePrice,
                                    InCargoID= DateTime.Now.ToString("yyMMdd") + Common.GetRandomFourNumString(),
                                });
                                long ProductID = bus.QueryProductID(Convert.ToString(92), item.GoodsCode.Trim());
                                stock.SalePrice= item.SalePrice;
                                stock.StockNum= item.StockNum;
                                stock.InCargoID = DateTime.Now.ToString("yyMMdd") + Common.GetRandomFourNumString();
                                stock.ContainerID = ContainerID;
                                if (ProductID == 0)
                                {
                                    bus.AddProduct(stock);
                                }
                                else
                                {
                                    bus.UpdateContainerGoods(stock,Convert.ToString(ProductID));
                                }
                            }
                            else
                            {
                                continue;
                                //item.InCargoID = DateTime.Now.ToString("yyMMdd") + Common.GetRandomFourNumString();
                                //item.ContainerID = ContainerID;
                                //bus.AddProductData(item);
                            }
                        }
                    }
                    else
                    {
                        Result1.Success = false;
                        Result1.Message = "接口异常";
                    }
                }
                else
                {
                    Result1.Success = false;
                    Result1.Message = "接口异常";

                }
            }
        }


    }
}