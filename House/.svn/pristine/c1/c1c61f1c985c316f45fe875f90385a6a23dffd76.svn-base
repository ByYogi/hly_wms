using House.Business.Cargo;
using House.Entity.Cargo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Cargo.Interface
{
    /// <summary>
    /// Kuaidi100 的摘要说明
    /// </summary>
    public class Kuaidi100 : IHttpHandler
    {
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
        /// <summary>
        /// 快递100新陆程中转运单的回调接口，保存新陆程中转单和迪乐泰订单的物流跟踪状态
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            CargoInterfaceBus nwBus = new CargoInterfaceBus();
            ReturnMessage rm = new ReturnMessage();
            rm.result = true;
            rm.returnCode = "200";
            rm.message = "成功";
            //中转单号/订单号
            string awbno = context.Request["TransitID"];
            if (string.IsNullOrEmpty(awbno))
            {
                rm.result = false;
                rm.returnCode = "500";
                rm.message = "返回订单号为空";
            }
            if (rm.result)
            {
                if (awbno.Length.Equals(11))
                {
                    //迪乐泰订单
                    string param = context.Request["param"];
                    ArrayList rows = (ArrayList)JSON.Decode("[" + param + "]");
                    WriteTextLog(awbno + "&" + param, DateTime.Now);
                    foreach (Hashtable item in rows)
                    {
                        ArrayList lr = (ArrayList)JSON.Decode("[" + JSON.Encode(item["lastResult"]) + "]");
                        foreach (Hashtable it in lr)
                        {
                            CargoKD100Entity track = new CargoKD100Entity();
                            track.TransitNo = Convert.ToString(it["nu"]);//承运单号物流公司单号
                            track.state = Convert.ToString(it["state"]);
                            track.ComCode = Convert.ToString(it["com"]);
                            track.OrderNo = Convert.ToString(awbno);//订单号
                            //track.city = Convert.ToString(it["to"]);
                            List<CargoKD100AwbStatusEntity> awbList = new List<CargoKD100AwbStatusEntity>();
                            ArrayList dt = (ArrayList)it["data"];
                            foreach (Hashtable dtt in dt)
                            {
                                CargoKD100AwbStatusEntity awb = new CargoKD100AwbStatusEntity();
                                awb.context = Convert.ToString(dtt["context"]);
                                awb.time = Convert.ToDateTime(dtt["time"]);
                                awb.ftime = Convert.ToDateTime(dtt["ftime"]);
                                awbList.Add(awb);
                            }
                            if (awbList.Count <= 0)
                            {
                                break;
                            }
                            track.awbStatusList = awbList;
                            WriteTextLog("开始写迪乐泰订单物流状态", DateTime.Now);
                            nwBus.SaveDLTOrderTrack(track);
                            break;
                        }
                    }
                }
                else
                {
                    //新陆程中转单
                    string param = context.Request["param"];
                    ArrayList rows = (ArrayList)JSON.Decode("[" + param + "]");
                    WriteTextLog(awbno + "&" + param, DateTime.Now);
                    foreach (Hashtable item in rows)
                    {
                        ArrayList lr = (ArrayList)JSON.Decode("[" + JSON.Encode(item["lastResult"]) + "]");
                        foreach (Hashtable it in lr)
                        {
                            CargoKD100Entity track = new CargoKD100Entity();
                            track.TransitNo = Convert.ToString(it["nu"]);//承运单号
                            track.state = Convert.ToString(it["state"]);
                            track.ComCode = Convert.ToString(it["com"]);
                            track.TransitID = Convert.ToInt64(awbno);//中转单号
                                                                     //track.city = Convert.ToString(it["to"]);
                            List<CargoKD100AwbStatusEntity> awbList = new List<CargoKD100AwbStatusEntity>();
                            ArrayList dt = (ArrayList)it["data"];
                            foreach (Hashtable dtt in dt)
                            {
                                CargoKD100AwbStatusEntity awb = new CargoKD100AwbStatusEntity();
                                awb.context = Convert.ToString(dtt["context"]);
                                awb.time = Convert.ToDateTime(dtt["time"]);
                                awb.ftime = Convert.ToDateTime(dtt["ftime"]);
                                awbList.Add(awb);
                            }
                            if (awbList.Count <= 0)
                            {
                                break;
                            }
                            track.awbStatusList = awbList;
                            WriteTextLog("开始写物流状态", DateTime.Now);
                            nwBus.SaveNwTransitTrack(track);
                            break;
                        }
                    }
                }
            }
            WriteTextLog("2", DateTime.Now);
            //一定要返回成功，不返回就是失败
            String json = JSON.Encode(rm);
            context.Response.ContentType = "application/json;charset=utf-8";
            context.Response.Write(json);
            context.Response.End();

        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}