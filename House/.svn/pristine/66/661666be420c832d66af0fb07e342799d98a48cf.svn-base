using System;
using House.Business;
using House.Business.Cargo;
using House.Entity;
using House.Entity.Cargo;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Senparc.Weixin.QY.CommonAPIs;
using Senparc.Weixin.QY.Entities;
using Senparc.Weixin.QY.AdvancedAPIs;
using Senparc.Weixin.QY.AdvancedAPIs.MailList;
using Senparc.Weixin.Entities;
using System.Text;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using House.Entity.House;
using Newtonsoft.Json;

namespace Cargo.QY
{
    public partial class wxQYServices : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string methodName = string.Empty;
            try
            {
                methodName = Request["method"];
                if (String.IsNullOrEmpty(methodName)) return;
                Type type = this.GetType();
                MethodInfo method = type.GetMethod(methodName);
                method.Invoke(this, null);
            }
            catch (Exception ex)
            {
                LogBus bus = new LogBus();
                LogEntity log = new LogEntity();
                log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
                log.Operate = "";
                log.Moudle = "";
                log.Status = "1";
                log.NvgPage = "";
                log.UserID = UserInfor.LoginName;
                log.Memo = methodName + " " + ex.Message + " " + ex.StackTrace;
                bus.InsertLog(log);
            }
        }
        /// <summary>
        /// 推送消息
        /// </summary>
        public void SendInfo()
        {

            QiyeBus qy = new QiyeBus();
            List<QyTagEntity> tag = qy.QueryTagList(new QyTagEntity { TagType = "0" });//查询所有用于推送下单信息的标签ID
            string strTag = string.Empty;
            if (tag != null && tag.Count > 0)
            {
                foreach (var it in tag)
                {
                    strTag += it.Id.ToString() + "|";
                }
                strTag = strTag.Substring(0, strTag.Length - 1);
            }
            string currDate = DateTime.Now.Year.ToString() + "年" + DateTime.Now.Month.ToString() + "月" + DateTime.Now.Day.ToString() + "日";
            QySendInfoEntity send = new QySendInfoEntity();
            send.title = "下单通知";
            send.msgType = msgType.textcard;
            send.toTag = strTag;
            //send.content = "<div>" + currDate + "</div><div></div><div>客户：" + ent.AcceptPeople + " 购买" + ent.Piece.ToString() + "条轮胎，金额：" + ent.TotalCharge.ToString() + "元</div><div>电话：" + ent.AcceptCellphone + "</div><div>地址：" + ent.AcceptAddress + "</div><div>业务员：" + ent.SaleManName + "</div><div></div><div class=\"highlight\">请尽快拣货出库发货！</div>";
            send.content = "下单通知发货";
            send.url = "dlt.neway5.com";
            WxQYSendHelper.PushInfo("0", "0", send);



            //string urlF = @"https://qyapi.weixin.qq.com/cgi-bin/message/send?access_token={0}";
            //AccessTokenResult token = CommonApi.GetToken(Common.GetQYCorpID(), "VkkRCESh5hxT8FStrYa0jWjIg0ux--M670SoFFyuimM");
            //string currDate = DateTime.Now.Year.ToString() + "年" + DateTime.Now.Month.ToString() + "月" + DateTime.Now.Day.ToString() + "日";
            //var data = new
            //{
            //    touser = "",
            //    toparty = "",
            //    totag = "1",
            //    msgtype = "textcard",
            //    agentid = 1000003,
            //    textcard = new
            //    {
            //        title = "下单通知",
            //        description = "<div>" + currDate + "</div><div></div><div>测试--客户：刘先生 购买100条轮胎，金额：26252元</div><div>电话：13254585565</div><div>地址：长沙市长沙县某某区</div><div>业务员：发发发</div><div></div><div class=\"highlight\">请尽快拣货出货！</div>",
            //        url = "http://dlt.neway5.com",
            //        btntxt = "详情"
            //    }
            //};
            //QyJsonResult res = CommonJsonSend.Send(token.access_token, urlF, data, Senparc.Weixin.CommonJsonSendType.POST);



            // WxJsonResult res = Senparc.Weixin.CommonAPIs.CommonJsonSend.Send(token.access_token, urlF, data, Senparc.Weixin.CommonJsonSendType.POST);
        }

        #region 部门管理

        /// <summary>
        /// 同步微信企业号部门数据到系统
        /// </summary>
        public void SyncDepart()
        {
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            try
            {
                List<QyDepartEntity> result = new List<QyDepartEntity>();

                AccessTokenResult token = CommonApi.GetToken(Common.GetQYCorpID(), Common.GetQYCorpSecret());

                GetDepartmentListResult depList = MailListApi.GetDepartmentList(token.access_token);

                string Url = "https://qyapi.weixin.qq.com/cgi-bin/user/list?";
                if (depList.department.Count > 0)
                {
                    foreach (var it in depList.department)
                    {
                        string BossID = "";
                        string Boss = "";
                        string returnJson = Get(Url + "access_token=" + token.access_token + "&department_id=" + it.id + "&fetch_child=" + 0);
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        DepartmentListReturnJson list = js.Deserialize<DepartmentListReturnJson>(returnJson);
                        string errcode = list.errcode;
                        string errmsg = list.errmsg;
                        List<userlist> orderdetail = list.userlist;
                        bool type = true;
                        foreach (var item in orderdetail)
                        {
                            if (type)
                            {
                                if (item.is_leader_in_dept.Count > 0 && item.is_leader_in_dept.Count == item.department.Count)
                                {
                                    for (int i = 0; i < item.is_leader_in_dept.Count; i++)
                                    {
                                        if (item.is_leader_in_dept[i] == 1 && item.department[i] == it.id)
                                        {
                                            BossID = item.userid;
                                            Boss = item.name;
                                            type = false;
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                        if (string.IsNullOrEmpty(BossID))
                        {
                            result.Add(new QyDepartEntity { Id = it.id, Name = it.name, Parentid = it.parentid, DepOrder = it.order.ToString() });
                        }
                        else
                        {
                            result.Add(new QyDepartEntity { Id = it.id, Name = it.name, Parentid = it.parentid, DepOrder = it.order.ToString(), Boss = Boss, BossID = Convert.ToInt32(BossID) });
                        }
                    }
                }

                QiyeBus bus = new QiyeBus();
                bus.SyncDepart(result);
                msg.Result = true;
                msg.Message = "成功";
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }

            //JSON
            String json = JSON.Encode(msg);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="url">请求url</param>
        /// <returns></returns>
        public static string Get(string url)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            if (req == null || req.GetResponse() == null)
                return string.Empty;

            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            if (resp == null)
                return string.Empty;

            using (Stream stream = resp.GetResponseStream())
            {
                //获取内容
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        private static string Post(string url, string postData)
        {
            string result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.Timeout = 15000;//设置请求超时时间，单位为毫秒
            req.ContentType = "application/json";
            byte[] data = Encoding.UTF8.GetBytes(postData);
            req.ContentLength = data.Length;
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();
            }
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            //获取响应内容
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }

        /// <summary>
        /// 部门数据查询
        /// </summary>
        public void queryDepart()
        {
            //QyDepartEntity queryEntity = new QyDepartEntity();
            //queryEntity.Name = Convert.ToString(Request["Name"]);
            ////分页
            //int pageIndex = Convert.ToInt32(Request["page"]);
            //int pageSize = Convert.ToInt32(Request["rows"]);
            //QiyeBus bus = new QiyeBus();
            //Hashtable list = bus.queryDepart(pageIndex, pageSize, queryEntity);

            ////JSON
            //String json = JSON.Encode(list);
            QiyeBus bus = new QiyeBus();
            List<QyDepartEntity> result = bus.queryDepart();
            string json = "[";
            foreach (var it in result)
            {
                if (it.Parentid.Equals(-1))
                {
                    continue;
                }
                if (it.Parentid.Equals(0))
                {
                    json += GetNode(result, it);
                }
            }

            json = json.Substring(0, json.Length - 1);
            json += "]";
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 根据ID返回节点
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private string GetNode(List<QyDepartEntity> orgList, QyDepartEntity org)
        {
            string result = string.Empty;
            List<QyDepartEntity> secList = orgList.FindAll(c => c.Parentid.Equals(org.Id));
            if (secList.Count > 0)
            {
                result += "{\"ID\": \"" + org.Id.ToString() + "\",\"Name\":\"" + org.Name + "\",\"Parentid\":\"" + org.Parentid.ToString() + "\",\"Boss\":\"" + org.Boss + "\",\"BossID\":\"" + org.BossID.ToString() + "\",\"Leader\":\"" + org.Leader + "\",\"LeaderID\":\"" + org.LeaderID.ToString() + "\",\"DepOrder\":\"" + org.DepOrder + "\",\"state\":\"closed\",\"children\":[";
                foreach (var it in secList)
                {
                    result += GetNode(orgList, it);
                }
                result = result.Substring(0, result.Length - 1);
                result += "]},";
            }
            else
            {
                result += "{\"ID\": \"" + org.Id.ToString() + "\",\"Name\":\"" + org.Name + "\",\"Parentid\":\"" + org.Parentid.ToString() + "\",\"Boss\":\"" + org.Boss + "\",\"BossID\":\"" + org.BossID.ToString() + "\",\"Leader\":\"" + org.Leader + "\",\"LeaderID\":\"" + org.LeaderID.ToString() + "\",\"DepOrder\":\"" + org.DepOrder + "\"},";
            }
            return result;
        }
        private string QueryNode(List<QyDepartEntity> orgList, QyDepartEntity org)
        {
            string result = string.Empty;
            List<QyDepartEntity> secList = orgList.FindAll(c => c.Parentid.Equals(org.Id));
            if (secList.Count > 0)
            {
                result += "{\"id\": \"" + org.Id.ToString() + "\",\"text\":\"" + org.Name.Trim() + "\",\"children\":[";
                foreach (var it in secList)
                {
                    result += QueryNode(orgList, it);
                }
                result = result.Substring(0, result.Length - 1);
                result += "]},";
            }
            else
            {
                result += "{\"id\": \"" + org.Id.ToString() + "\",\"text\":\"" + org.Name.Trim() + "\"},";
            }
            return result;
        }
        /// <summary>
        /// 查询所有部门数据
        /// </summary>
        public void QueryDepartList()
        {
            QyUserEntity queryEntity = new QyUserEntity();
            string Department = Request.QueryString["Department"];
            if (!string.IsNullOrEmpty(Department))
            {
                queryEntity.Department = Department;
            }
            else
            {
                queryEntity.Department = "0";
            }
            QiyeBus bus = new QiyeBus();
            List<QyUserEntity> list = bus.QueryDepartAllUserList(queryEntity);

            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);

        }
        /// <summary>
        /// 获取所有组织架构
        /// </summary>
        public void QueryAllOrganize()
        {
            QiyeBus bus = new QiyeBus();
            List<QyDepartEntity> list = bus.QueryDepartList();
            string res = "[";
            foreach (var it in list)
            {
                if (it.Parentid.Equals(-1))
                {
                    continue;
                }
                if (it.Parentid.Equals(0))
                {
                    res += QueryNode(list, it);
                }
            }

            res = res.Substring(0, res.Length - 1);
            res += "]";
            Response.Clear();
            Response.Write(res);
        }
        /// <summary>
        /// 保存微信企业号部门数据 
        /// </summary>
        public void SaveQYDepart()
        {
            QyDepartEntity ent = new QyDepartEntity();
            QiyeBus bus = new QiyeBus();
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "微信企业号管理";
            log.Status = "0";
            log.NvgPage = "部门管理";
            log.UserID = UserInfor.LoginName.Trim();
            String id = Request["Id"] != null ? Request["Id"].ToString() : "";
            try
            {
                ent.Name = Convert.ToString(Request["Name"]);
                ent.Parentid = Convert.ToInt32(Request["PId"]);
                AccessTokenResult token = CommonApi.GetToken(Common.GetQYCorpID(), Common.GetQYCorpSecret());
                if (id == "")
                {
                    log.Operate = "A";
                    CreateDepartmentResult cdr = MailListApi.CreateDepartment(token.access_token, ent.Name, ent.Parentid);
                    ent.Id = cdr.id;
                    Random rd = new System.Random();
                    ent.DepOrder = rd.Next(1, 100).ToString();
                    bus.AddQYDepart(ent, log);

                    msg.Result = true;
                    msg.Message = "成功";

                }
                else//修改
                {
                    log.Operate = "U";
                    ent.Id = Convert.ToInt32(id);
                    QyJsonResult cdr = MailListApi.UpdateDepartment(token.access_token, id, ent.Name, ent.Parentid);
                    bus.UpdateQYDepart(ent, log);
                    msg.Result = true;
                    msg.Message = "成功";
                }
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
        }
        /// <summary>
        /// 删除部门数据
        /// </summary>
        public void DelQYDepart()
        {
            String json = Request["data"];
            if (String.IsNullOrEmpty(json)) return;
            List<QyDepartEntity> list = new List<QyDepartEntity>();
            ArrayList rows = (ArrayList)JSON.Decode(json);
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            QiyeBus bus = new QiyeBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "微信企业号管理";
            log.Status = "0";
            log.NvgPage = "部门管理";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "D";
            AccessTokenResult token = CommonApi.GetToken(Common.GetQYCorpID(), Common.GetQYCorpSecret());
            try
            {
                foreach (Hashtable row in rows)
                {
                    list.Add(new QyDepartEntity
                    {
                        Id = Convert.ToInt32(row["ID"]),
                        Name = Convert.ToString(row["Name"]),
                        Parentid = Convert.ToInt32(row["Parentid"])
                    });

                    GetDepartmentMemberResult gmr = MailListApi.GetDepartmentMember(token.access_token, Convert.ToInt32(row["ID"]), 0, 0);
                    if (gmr.userlist.Count > 0)
                    {
                        msg.Result = false;
                        msg.Message = "不能删除含有子部门、成员的部门";
                        break;
                    }
                }
                if (msg.Result)
                {
                    bus.DelQYDepart(list, log);

                    foreach (var it in list)
                    {
                        QyJsonResult delInfo = MailListApi.DeleteDepartment(token.access_token, Convert.ToString(it.Id));
                    }

                    msg.Result = true;
                    msg.Message = "成功";
                }
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);

        }

        #endregion

        #region 用户管理
        /// <summary>
        /// 同步成员
        /// </summary>
        public void SyncUser()
        {
            String json = Request["data"];
            if (String.IsNullOrEmpty(json)) return;
            ArrayList rows = (ArrayList)JSON.Decode(json);
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            int type = 1;
            try
            {
                List<QyUserEntity> result = new List<QyUserEntity>();

                AccessTokenResult token = CommonApi.GetToken(Common.GetQYCorpID(), Common.GetQYCorpSecret());
                if (rows.Count <= 0)
                {
                    type = 0;
                    string Url = "https://qyapi.weixin.qq.com/cgi-bin/user/list?";
                    string returnJson = Get(Url + "access_token=" + token.access_token + "&department_id=1&fetch_child=1");
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    DepartmentListReturnJson list = js.Deserialize<DepartmentListReturnJson>(returnJson);
                    List<userlist> lists = list.userlist;

                    foreach (var item in lists)
                    {
                        ConvertToOpenIdResult ctor = new ConvertToOpenIdResult();
                        if (item.status.Equals(1))
                        {
                            ctor = CommonApi.ConvertToOpenId(token.access_token, item.userid);
                        }
                        string dep = string.Empty;
                        if (item.department.Count > 0)
                        {
                            for (int i = 0; i < item.department.Count; i++)
                            {
                                dep += item.department[i] + ",";
                            }
                            dep = dep.Substring(0, dep.Length - 1);
                        }
                        string asmall = string.Empty;
                        if (!string.IsNullOrEmpty(item.avatar))
                        {
                            if (item.avatar.Substring(item.avatar.Length - 1, 1).Equals("0"))
                            {
                                asmall = item.avatar.Substring(0, item.avatar.Length - 1) + "100";
                            }
                            else
                            {
                                asmall = item.avatar + "100";
                            }
                        }
                        result.Add(new QyUserEntity
                        {
                            UserID = item.userid,
                            WxName = item.name,
                            OpenID = ctor.openid,
                            //weixinID = item.weixinid,
                            MainDepartment = item.main_department,
                            CellPhone = item.mobile,
                            Department = dep,
                            Position = item.position,
                            Gender = item.gender.ToString(),
                            Email = item.email,
                            AvatarSmall = asmall,
                            AvatarBig = item.avatar
                        });
                    }
                    #region 旧的使用盛派SDK获取
                    //GetDepartmentMemberInfoResult userList = MailListApi.GetDepartmentMemberInfo(token.access_token, 1, 1, 0);
                    //foreach (var it in userList.userlist)
                    //{

                    //    ConvertToOpenIdResult ctor = new ConvertToOpenIdResult();
                    //    if (it.status.Equals(1))
                    //    {
                    //        ctor = CommonApi.ConvertToOpenId(token.access_token, it.userid);
                    //    }
                    //    string dep = string.Empty;
                    //    if (it.department.Length > 0)
                    //    {
                    //        for (int i = 0; i < it.department.Length; i++)
                    //        {
                    //            dep += it.department[i] + ",";
                    //        }
                    //        dep = dep.Substring(0, dep.Length - 1);
                    //    }
                    //    string asmall = string.Empty;
                    //    if (!string.IsNullOrEmpty(it.avatar))
                    //    {
                    //        if (it.avatar.Substring(it.avatar.Length - 1, 1).Equals("0"))
                    //        {
                    //            asmall = it.avatar.Substring(0, it.avatar.Length - 1) + "100";
                    //        }
                    //        else
                    //        {
                    //            asmall = it.avatar + "100";
                    //        }
                    //    }

                    //    result.Add(new QyUserEntity
                    //    {
                    //        UserID = it.userid,
                    //        WxName = it.name,
                    //        OpenID = ctor.openid,
                    //        weixinID = it.weixinid,
                    //        CellPhone = it.mobile,
                    //        Department = dep,
                    //        Position = it.position,
                    //        Gender = it.gender.ToString(),
                    //        Email = it.email,
                    //        AvatarSmall = asmall,
                    //        AvatarBig = it.avatar
                    //    });
                    //} 
                    #endregion
                }
                else
                {
                    foreach (Hashtable row in rows)
                    {
                        string Url = "https://qyapi.weixin.qq.com/cgi-bin/user/get?";
                        string returnJson = Get(Url + "access_token=" + token.access_token + "&userid=" + Convert.ToString(row["UserID"]));
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        userlist userRes = js.Deserialize<userlist>(returnJson);
                        ConvertToOpenIdResult ctor = new ConvertToOpenIdResult();
                        if (userRes.status.Equals(1))
                        {
                            ctor = CommonApi.ConvertToOpenId(token.access_token, userRes.userid);
                        }
                        string dep = string.Empty;
                        if (userRes.department.Count > 0)
                        {
                            for (int i = 0; i < userRes.department.Count; i++)
                            {
                                dep += userRes.department[i] + ",";
                            }
                            dep = dep.Substring(0, dep.Length - 1);
                        }
                        string asmall = string.Empty;
                        if (!string.IsNullOrEmpty(userRes.avatar))
                        {
                            if (userRes.avatar.Substring(userRes.avatar.Length - 1, 1).Equals("0"))
                            {
                                asmall = userRes.avatar.Substring(0, userRes.avatar.Length - 1) + "100";
                            }
                            else
                            {
                                asmall = userRes.avatar + "100";
                            }
                        }

                        #region 旧的使用盛派SDK获取
                        //GetMemberResult userRes = MailListApi.GetMember(token.access_token, Convert.ToString(row["UserID"]));
                        //ConvertToOpenIdResult ctor = new ConvertToOpenIdResult();
                        //if (userRes.status.Equals(1))
                        //{
                        //    ctor = CommonApi.ConvertToOpenId(token.access_token, userRes.userid);
                        //}
                        //string dep = string.Empty;
                        //if (userRes.department.Length > 0)
                        //{
                        //    for (int i = 0; i < userRes.department.Length; i++)
                        //    {
                        //        dep += userRes.department[i] + ",";
                        //    }
                        //    dep = dep.Substring(0, dep.Length - 1);
                        //}
                        //string asmall = string.Empty;
                        //if (!string.IsNullOrEmpty(userRes.avatar))
                        //{
                        //    if (userRes.avatar.Substring(userRes.avatar.Length - 1, 1).Equals("0"))
                        //    {
                        //        asmall = userRes.avatar.Substring(0, userRes.avatar.Length - 1) + "100";
                        //    }
                        //    else
                        //    {
                        //        asmall = userRes.avatar + "100";
                        //    }
                        //}
                        #endregion

                        result.Add(new QyUserEntity
                        {
                            UserID = userRes.userid,
                            WxName = userRes.name,
                            OpenID = ctor.openid,
                            //weixinID = userRes.weixinid,
                            MainDepartment = userRes.main_department,
                            CellPhone = userRes.mobile,
                            Department = dep,
                            Position = userRes.position,
                            Gender = userRes.gender.ToString(),
                            Email = userRes.email,
                            AvatarSmall = asmall,
                            AvatarBig = userRes.avatar
                        });
                    }
                }
                QiyeBus bus = new QiyeBus();
                bus.SyncUser(result, type);
                msg.Result = true;
                msg.Message = "成功";
            }
            catch (ApplicationException ex)
            {
                string str = ex.Message;
                string errorCode = str.Substring(str.IndexOf("错误代码：") + 5, str.IndexOf("，说明：") - (str.IndexOf("错误代码：") + 5));
                if (errorCode == "60111")
                {
                    msg.Message = "同步账户中包含已取关账户或UserID不存在";
                }
                else
                {
                    msg.Message = ex.Message;
                }
                msg.Result = false;
            }

            //JSON
            String res = JSON.Encode(msg);
            Response.Clear();
            Response.Write(res);
        }
        /// <summary>
        /// 查询微信用户数据
        /// </summary>
        public void queryUser()
        {
            QyUserEntity queryEntity = new QyUserEntity();
            queryEntity.WxName = Convert.ToString(Request["WxName"]);
            queryEntity.Department = Convert.ToString(Request["Depart"]);
            //分页
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]);
            QiyeBus bus = new QiyeBus();
            Hashtable list = bus.queryUser(pageIndex, pageSize, queryEntity);

            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);

        }
        /// <summary>
        /// 删除微信用户
        /// </summary>
        public void DelQYUser()
        {
            String json = Request["data"];
            if (String.IsNullOrEmpty(json)) return;
            List<QyUserEntity> list = new List<QyUserEntity>();
            ArrayList rows = (ArrayList)JSON.Decode(json);
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            QiyeBus bus = new QiyeBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "微信企业号管理";
            log.Status = "0";
            log.NvgPage = "用户管理";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "D";
            AccessTokenResult token = CommonApi.GetToken(Common.GetQYCorpID(), Common.GetQYCorpSecret());
            try
            {
                foreach (Hashtable row in rows)
                {
                    list.Add(new QyUserEntity
                    {
                        UserID = Convert.ToString(row["UserID"]),
                        WxName = Convert.ToString(row["WxName"]),
                        OpenID = Convert.ToString(row["OpenID"]),
                        CellPhone = Convert.ToString(row["CellPhone"])
                    });
                }
                if (msg.Result)
                {
                    bus.DelQYUser(list, log);

                    foreach (var it in list)
                    {
                        QyJsonResult gmr = MailListApi.DeleteMember(token.access_token, it.UserID);
                    }

                    msg.Result = true;
                    msg.Message = "成功";
                }
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
        }
        /// <summary>
        /// 保存用户数据
        /// </summary>
        public void SaveQYUser()
        {
            QyUserEntity ent = new QyUserEntity();
            QiyeBus bus = new QiyeBus();
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "微信企业号管理";
            log.Status = "0";
            log.NvgPage = "用户管理";
            log.UserID = UserInfor.LoginName.Trim();
            try
            {
                ent.UserID = Convert.ToString(Request["UserID"]);
                ent.WxName = Convert.ToString(Request["WxName"]);
                ent.Department = Convert.ToString(Request["Department"]);
                ent.CellPhone = Convert.ToString(Request["CellPhone"]);
                ent.Email = "";// Convert.ToString(Request["Email"]);
                ent.Position = Convert.ToString(Request["Position"]);
                ent.Tag = Convert.ToString(Request["Tag"]) == null ? "" : Convert.ToString(Request["Tag"]);
                ent.HouseID = Convert.ToString(Request["HouseID"]) == null || Convert.ToString(Request["HouseID"]) == "" ? 0 : Convert.ToInt32(Request["HouseID"]);
                ent.CheckRole = Convert.ToString(Request["CheckRole"]) == null ? "" : Convert.ToString(Request["CheckRole"]);
                ent.CheckHouseName = Convert.ToString(Request["CheckHouseName"]) == null ? "" : Convert.ToString(Request["CheckHouseName"]);
                ent.CheckHouseID = Convert.ToString(Request["CheckHouseID"]) == null || Convert.ToString(Request["CheckHouseID"]) == "" ? "" : Convert.ToString(Request["CheckHouseID"]);
                if (ent.CheckRole.StartsWith(","))
                {
                    ent.CheckRole = ent.CheckRole.Substring(1, ent.CheckRole.Length - 1);
                }
                if (ent.CheckHouseID.StartsWith(","))
                {
                    ent.CheckHouseID = ent.CheckHouseID.Substring(1, ent.CheckHouseID.Length - 1);
                }
                AccessTokenResult token = CommonApi.GetToken(Common.GetQYCorpID(), Common.GetQYCorpSecret());
                bool isexist = bus.IsExistQYUser(ent.UserID);
                if (!isexist)//新增
                {
                    log.Operate = "A";
                    //int[] depart = { Convert.ToInt32(ent.Department) };
                    string[] Department = ent.Department.Split(',');
                    int[] depart = new int[] { };
                    List<int> _depart = depart.ToList();
                    foreach (var item in Department)
                    {
                        _depart.Add(Convert.ToInt32(item));
                    }
                    depart = _depart.ToArray();
                    //调用微信接口保存用户数据
                    QyJsonResult cdr = MailListApi.CreateMember(token.access_token, ent.UserID, ent.WxName, depart, ent.Position, ent.CellPhone, ent.Email, null, null, null);
                    //QyJsonResult cdr = MailListApi.CreateMember(token.access_token, ent.UserID, ent.WxName, depart, null, ent.CellPhone, null, null, null, null);
                    //调用微信接口获取用户数据
                    GetMemberResult user = MailListApi.GetMember(token.access_token, ent.UserID);
                    //UserID互换为OpenID
                    if (user.status.Equals(1))
                    {
                        ConvertToOpenIdResult ctor = CommonApi.ConvertToOpenId(token.access_token, ent.UserID);
                        ent.OpenID = ctor.openid;
                    }

                    ent.UserID = user.userid;
                    ent.WxName = user.name;
                    ent.weixinID = user.weixinid;
                    ent.CellPhone = user.mobile;
                    ent.Department = ent.Department;
                    ent.MainDepartment = Convert.ToInt32(ent.Department);
                    ent.Position = user.position;
                    ent.Gender = user.gender.ToString();
                    ent.Email = user.email;
                    ent.AvatarSmall = string.IsNullOrEmpty(user.avatar) ? "" : user.avatar.Substring(0, user.avatar.Length - 1) + "100";
                    ent.AvatarBig = user.avatar;

                    bus.AddQYUser(ent, log);

                    msg.Result = true;
                    msg.Message = "成功";

                    if (!string.IsNullOrEmpty(ent.Tag))
                    {
                        string[] tagList = ent.Tag.Split(',');
                        if (tagList.Length > 0)
                        {
                            for (int i = 0; i < tagList.Length; i++)
                            {
                                if (!string.IsNullOrEmpty(tagList[i]))
                                {
                                    string[] userList = { ent.UserID };
                                    AddTagMemberResult amr = MailListApi.AddTagMember(token.access_token, Convert.ToInt32(tagList[i]), userList);
                                }
                            }
                        }
                    }
                }
                else//修改
                {
                    log.Operate = "U";
                    //int[] depart = { Convert.ToInt32(ent.Department) };
                    string[] Department = ent.Department.Split(',');
                    int[] depart = new int[] { };
                    List<int> _depart = depart.ToList();
                    foreach (var item in Department)
                    {
                        _depart.Add(Convert.ToInt32(item));
                    }
                    depart = _depart.ToArray();

                    //QyJsonResult cdr = MailListApi.UpdateMember(token.access_token, ent.UserID, ent.WxName, depart, ent.Position, ent.CellPhone, ent.Email, null, 1, null, null);
                    QyJsonResult cdr = MailListApi.UpdateMember(token.access_token, ent.UserID, ent.WxName, depart, null, null, null, null, 1, null, null);
                    bus.UpdateQYUser(ent, log);
                    msg.Result = true;
                    msg.Message = "成功";
                    string oldTag = Convert.ToString(Request["OldTag"]);
                    if (!string.IsNullOrEmpty(oldTag))
                    {
                        string[] tagList = oldTag.Split(',');
                        if (tagList.Length > 0)
                        {
                            for (int i = 0; i < tagList.Length; i++)
                            {
                                if (!string.IsNullOrEmpty(tagList[i]))
                                {
                                    string[] userList = { ent.UserID };
                                    DelTagMemberResult amr = MailListApi.DelTagMember(token.access_token, Convert.ToInt32(tagList[i]), userList);
                                }
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(ent.Tag))
                    {
                        string[] tagList = ent.Tag.Split(',');
                        if (tagList.Length > 0)
                        {
                            for (int i = 0; i < tagList.Length; i++)
                            {
                                if (!string.IsNullOrEmpty(tagList[i]))
                                {
                                    string[] userList = { ent.UserID };
                                    AddTagMemberResult amr = MailListApi.AddTagMember(token.access_token, Convert.ToInt32(tagList[i]), userList);
                                }
                            }
                        }
                    }
                }
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);

        }
        #endregion

        #region 标签管理
        /// <summary>
        /// 同步标签
        /// </summary>
        public void SyncTag()
        {
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            try
            {
                List<QyTagEntity> result = new List<QyTagEntity>();

                AccessTokenResult token = CommonApi.GetToken(Common.GetQYCorpID(), Common.GetQYCorpSecret());

                GetTagListResult tagList = MailListApi.GetTagList(token.access_token);
                if (tagList.taglist.Count > 0)
                {
                    foreach (var it in tagList.taglist)
                    {
                        result.Add(new QyTagEntity { Id = Convert.ToInt32(it.tagid), Name = it.tagname });
                    }
                }

                QiyeBus bus = new QiyeBus();
                bus.SyncTag(result);
                msg.Result = true;
                msg.Message = "成功";
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }

            //JSON
            String json = JSON.Encode(msg);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 新增和修改标签
        /// </summary>
        public void SaveQYTag()
        {
            QyTagEntity ent = new QyTagEntity();
            QiyeBus bus = new QiyeBus();
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "微信企业号管理";
            log.Status = "0";
            log.NvgPage = "标签管理";
            log.UserID = UserInfor.LoginName.Trim();
            String id = Request["Id"] != null ? Request["Id"].ToString() : "";
            try
            {
                ent.Name = Convert.ToString(Request["Name"]);
                ent.TagType = Convert.ToString(Request["TagType"]);
                ent.HouseID = Convert.ToInt32(Request["HouseID"]);
                AccessTokenResult token = CommonApi.GetToken(Common.GetQYCorpID(), Common.GetQYCorpSecret());
                if (id == "")
                {
                    log.Operate = "A";
                    GetTagListResult tagList = MailListApi.GetTagList(token.access_token);
                    if (tagList.taglist.Count > 0)
                    {
                        foreach (var it in tagList.taglist)
                        {
                            if (it.tagname.Equals(ent.Name))
                            {
                                msg.Message = "已经存在相同标签名称";
                                msg.Result = false;
                                break;
                            }
                        }
                    }
                    if (msg.Result)
                    {
                        CreateTagResult tagRes = MailListApi.CreateTag(token.access_token, ent.Name);
                        if (tagRes.errcode.ToString().Equals("请求成功"))
                        {
                            ent.Id = tagRes.tagid;
                        }

                        bus.AddQYTag(ent, log);

                        msg.Message = "成功";
                    }
                }
                else//修改
                {
                    log.Operate = "U";
                    ent.Id = Convert.ToInt32(id);
                    QyJsonResult cdr = MailListApi.UpdateTag(token.access_token, ent.Id, ent.Name);
                    bus.UpdateQYTag(ent, log);
                    msg.Result = true;
                    msg.Message = "成功";
                }
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);

        }
        /// <summary>
        /// 查询标签
        /// </summary>
        public void queryTag()
        {
            QyTagEntity queryEntity = new QyTagEntity();
            queryEntity.Name = Convert.ToString(Request["Name"]);
            queryEntity.CargoPermisID = Convert.ToString(Request["HID"]);
            //分页
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]);
            QiyeBus bus = new QiyeBus();
            Hashtable list = bus.queryTag(pageIndex, pageSize, queryEntity);

            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 查询所有标签
        /// </summary>
        public void QueryTagList()
        {
            QyTagEntity queryEntity = new QyTagEntity();
            QiyeBus bus = new QiyeBus();
            List<QyTagEntity> list = bus.QueryTagList(queryEntity);

            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);

        }
        /// <summary>
        /// 删除标签
        /// </summary>
        public void DelQYTag()
        {
            String json = Request["data"];
            if (String.IsNullOrEmpty(json)) return;
            List<QyTagEntity> list = new List<QyTagEntity>();
            ArrayList rows = (ArrayList)JSON.Decode(json);
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            QiyeBus bus = new QiyeBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "微信企业号管理";
            log.Status = "0";
            log.NvgPage = "标签管理";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "D";
            AccessTokenResult token = CommonApi.GetToken(Common.GetQYCorpID(), Common.GetQYCorpSecret());
            try
            {
                foreach (Hashtable row in rows)
                {
                    list.Add(new QyTagEntity
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        Name = Convert.ToString(row["Name"]),
                        TagType = Convert.ToString(row["TagType"]),
                        HouseName = Convert.ToString(row["HouseName"]),
                        HouseID = Convert.ToInt32(row["HouseID"])
                    });
                }
                if (msg.Result)
                {
                    bus.DelQYTag(list, log);

                    foreach (var it in list)
                    {
                        QyJsonResult gmr = MailListApi.DeleteTag(token.access_token, it.Id);
                    }

                    msg.Result = true;
                    msg.Message = "成功";
                }
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);

        }
        #endregion

        #region 企业号基础配置管理
        /// <summary>
        /// 查询
        /// </summary>
        public void QueryQyConfig()
        {
            QyConfigEntity queryEntity = new QyConfigEntity();
            queryEntity.WorkClass = Convert.ToString(Request["WorkClass"]);
            //分页
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]);
            QiyeBus bus = new QiyeBus();
            Hashtable list = bus.QueryQyConfig(pageIndex, pageSize, queryEntity);

            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 新增和修改基础设置
        /// </summary>
        public void SaveQYConfig()
        {
            QyConfigEntity ent = new QyConfigEntity();
            QiyeBus bus = new QiyeBus();
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "微信企业号管理";
            log.Status = "0";
            log.NvgPage = "配置管理";
            log.UserID = UserInfor.LoginName.Trim();
            String id = Request["ID"] != null ? Request["ID"].ToString() : "";
            try
            {
                ent.SendType = Convert.ToString(Request["SendType"]);
                ent.WorkClass = Convert.ToString(Request["WorkClass"]);
                ent.AgentID = Convert.ToString(Request["AgentID"]);
                ent.AppSecret = Convert.ToString(Request["AppSecret"]);
                ent.QYKind = Convert.ToString(Request["QYKind"]);
                if (id == "")
                {
                    log.Operate = "A";
                    if (msg.Result)
                    {

                        bus.AddQYConfig(ent, log);

                        msg.Message = "成功";
                    }
                }
                else//修改
                {
                    //log.Operate = "U";
                    //ent.Id = Convert.ToInt32(id);
                    //QyJsonResult cdr = MailListApi.UpdateTag(token.access_token, ent.Id, ent.Name);
                    //bus.UpdateQYTag(ent, log);
                    msg.Result = true;
                    msg.Message = "成功";
                }
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
        }
        /// <summary>
        /// 删除企业号应用配置
        /// </summary>
        public void DelQYConfig()
        {
            String json = Request["data"];
            if (String.IsNullOrEmpty(json)) return;
            List<QyConfigEntity> list = new List<QyConfigEntity>();
            ArrayList rows = (ArrayList)JSON.Decode(json);
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            QiyeBus bus = new QiyeBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "微信企业号管理";
            log.Status = "0";
            log.NvgPage = "配置管理";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "D";
            AccessTokenResult token = CommonApi.GetToken(Common.GetQYCorpID(), Common.GetQYCorpSecret());
            try
            {
                foreach (Hashtable row in rows)
                {
                    list.Add(new QyConfigEntity
                    {
                        ID = Convert.ToInt32(row["ID"]),
                        QYKind = Convert.ToString(row["QYKind"]),
                        AgentID = Convert.ToString(row["AgentID"]),
                        AppSecret = Convert.ToString(row["AppSecret"]),
                        SendType = Convert.ToString(row["SendType"]),
                        WorkClass = Convert.ToString(row["WorkClass"])
                    });
                }
                if (msg.Result)
                {
                    bus.DelQYConfig(list, log);
                    msg.Result = true;
                    msg.Message = "成功";
                }
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);

        }
        #endregion

        #region 考勤管理
        public void QueryCheckinData()
        {
            QYCheckinDataEntity queryEntity = new QYCheckinDataEntity();

            if (!string.IsNullOrEmpty(Request["CheckinType"]) && Request["CheckinType"] != "全部")
            {
                queryEntity.CheckinType = Request["CheckinType"].Trim();
            }

            if (!string.IsNullOrEmpty(Request["ExceptionType"]) && Request["ExceptionType"] != "全部")
            {
                queryEntity.ExceptionType = Request["ExceptionType"].Trim();
            }
            if (!string.IsNullOrEmpty(Request["UserName"]))
            {
                queryEntity.UserName = Request["UserName"].Trim();
            }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"])))
            {
                queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]);
            }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"])))
            {
                queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]);
            }
            //分页
            int pageIndex = Convert.ToInt32(Request["page"]).Equals(0) ? 1 : Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]).Equals(0) ? 1000 : Convert.ToInt32(Request["rows"]);
            QiyeBus bus = new QiyeBus();
            Hashtable list = bus.QueryCheckinData(pageIndex, pageSize, queryEntity);

            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);

        }


        /// <summary>
        /// 将dateTime格式转换为Unix时间戳
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static int DateTimeToUnixTime(DateTime dateTime)
        {
            return (int)(dateTime - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1))).TotalSeconds;
        }

        /// <summary>
        /// 将Unix时间戳转换为dateTime格式
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static DateTime UnixTimeToDateTime(int time)
        {
            if (time < 0)
            {
                throw new ArgumentOutOfRangeException("时间超出范围");
            }
            return TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)).AddSeconds(time);
        }
        /// <summary>
        /// 同步考勤
        /// </summary>
        public void SyncCheckin()
        {
            int type = Convert.ToInt32(Request["type"]);
            QiyeBus bus = new QiyeBus();
            QyDepartEntity departEntity = new QyDepartEntity();
            departEntity.Parentid = 15;
            List<QyDepartEntity> departList = bus.QueryDepartList(departEntity);
            string departStr = "";
            foreach (var item in departList)
            {
                departStr += "'" + item.Id + "',";
            }
            departStr = departStr.TrimEnd(',');
            List<QyUserEntity> userList = bus.QueryDepartAllUserList(departStr);
            string userStr = "[";
            foreach (var item in userList)
            {
                userStr += "\"" + item.UserID + "\",";
            }
            userStr = userStr.TrimEnd(',');
            userStr += "]";
            List<QYCheckinDataEntity> result = new List<QYCheckinDataEntity>();
            AccessTokenResult token = CommonApi.GetToken(Common.GetQYCorpID(), "MiwmXdN9HasI7x16VuTvZAosC2K8qVD9WoV0sQ_AsvQ");
            string Url = "https://qyapi.weixin.qq.com/cgi-bin/checkin/getcheckindata?access_token=";
            int startUnixTime = 0;
            int endUnixTime = 0;
            if (type == 0)
            {
                startUnixTime = DateTimeToUnixTime(DateTime.Now.AddDays(1 - DateTime.Now.Day).Date);
                endUnixTime = DateTimeToUnixTime(DateTime.Now.AddDays(1 - DateTime.Now.Day).Date.AddMonths(1).AddSeconds(-1));
            }
            else
            {
                startUnixTime = DateTimeToUnixTime(DateTime.Now.Date);
                endUnixTime = DateTimeToUnixTime(DateTime.Now);
            }
            string jsonData = "{\"opencheckindatatype\": 3,\"starttime\": " + startUnixTime + ",\"endtime\": " + endUnixTime + ",\"useridlist\": " + userStr + "}";
            string returnJson = Post(Url + token.access_token, jsonData);
            JavaScriptSerializer js = new JavaScriptSerializer();
            CheckintListReturnJson list = js.Deserialize<CheckintListReturnJson>(returnJson);
            string errcode = list.errcode;
            string errmsg = list.errmsg;
            List<checkindata> checkindata = list.checkindata;

            if (checkindata.Count > 0)
            {
                foreach (var item in checkindata)
                {
                    string MediaPath = item.mediaids.Count > 0 ? item.userid + UnixTimeToDateTime(item.checkin_time).ToString("yyyyMMddHHmmss") + ".png" : "";
                    if (item.mediaids.Count > 0)
                    {
                        SaveWxPicture(token.access_token, item.mediaids[0], MediaPath, System.Web.HttpContext.Current.Server.MapPath("/") + "CheckinImage\\");
                    }
                    result.Add(new QYCheckinDataEntity { UserID = item.userid, GroupName = item.groupname, CheckinType = item.checkin_type, ExceptionType = item.exception_type, CheckinTime = UnixTimeToDateTime(item.checkin_time), LocationTitle = item.location_title, LocationDetail = item.location_detail, WifiName = item.wifiname, Notes = item.notes, WifiMac = item.wifimac, MediaIDs = item.mediaids.Count > 0 ? item.mediaids[0] : "", MediaPath = MediaPath, Lat = item.lat, Lng = item.lng, DeviceID = item.deviceid, SchCheckinTime = UnixTimeToDateTime(item.sch_checkin_time), GroupID = item.groupid, TimelineID = item.timeline_id });

                }
                bus.SyncCheckinData(result);
            }
        }
        /// <summary>
        /// 下载微信考勤附件图片
        /// </summary>
        /// <param name="mediaID"></param>
        /// <param name="imgName"></param>
        /// <param name="imgPath"></param>
        public void SaveWxPicture(string token, string mediaID, string imgName, string imgPath)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    MediaApi.Get(token, mediaID, ms);

                    if (!Directory.Exists(imgPath))
                    {
                        Directory.CreateDirectory(imgPath);
                    }
                    //保存到文件
                    using (FileStream fs = new FileStream(imgPath + imgName, FileMode.Create))
                    {
                        ms.Position = 0;
                        byte[] buffer = new byte[1024];
                        int bytesRead = 0;
                        while ((bytesRead = ms.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            fs.Write(buffer, 0, bytesRead);
                        }
                        fs.Flush();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void SyncCheckinDayReport()
        {
            int day = DateTime.Now.Day;
            for (int i = 0; i < day; i++)
            {
                SyncCheckinDayReport1((DateTime.Now.Date.AddDays(-i).ToUniversalTime().Ticks - 621355968000000000) / 10000000);
            }
        }
        /// <summary>
        /// 同步考勤日报汇总
        /// </summary>
        public void SyncCheckinDayReport1(long UnixTime)
        {
            QiyeBus bus = new QiyeBus();
            QyDepartEntity departEntity = new QyDepartEntity();
            departEntity.Parentid = 15;
            List<QyDepartEntity> departList = bus.QueryDepartList(departEntity);
            string departStr = "";
            foreach (var item in departList)
            {
                departStr += "'" + item.Id + "',";
            }
            departStr = departStr.TrimEnd(',');
            List<QyUserEntity> userList = bus.QueryDepartAllUserList(departStr);
            string userStr = "[";
            foreach (var item in userList)
            {
                userStr += "\"" + item.UserID + "\",";
            }
            userStr = userStr.TrimEnd(',');
            userStr += "]";
            AccessTokenResult token = CommonApi.GetToken(Common.GetQYCorpID(), "MiwmXdN9HasI7x16VuTvZAosC2K8qVD9WoV0sQ_AsvQ");
            string Url = "https://qyapi.weixin.qq.com/cgi-bin/checkin/getcheckin_daydata?access_token=";
            int startUnixTime = DateTimeToUnixTime(DateTime.Now.Date);
            int endUnixTime = DateTimeToUnixTime(DateTime.Now);
            startUnixTime = Convert.ToInt32(UnixTime);
            endUnixTime = Convert.ToInt32(UnixTime);
            string jsonData = "{\"starttime\": " + startUnixTime + ",\"endtime\": " + endUnixTime + ",\"useridlist\": " + userStr + "}";
            string returnJson = Post(Url + token.access_token, jsonData);
            JavaScriptSerializer js = new JavaScriptSerializer();
            CheckintDayReportReturnJson list = js.Deserialize<CheckintDayReportReturnJson>(returnJson);
            string errcode = list.errcode;
            string errmsg = list.errmsg;
            List<day_datas> checkindata = list.datas;
            List<QYCheckinDayReportEntity> result = new List<QYCheckinDayReportEntity>();

            if (checkindata.Count > 0)
            {
                foreach (var item in checkindata)
                {
                    QYCheckinDayReportEntity entity = new QYCheckinDayReportEntity();
                    QYCheckinDayReportSpInfo spInfo = new QYCheckinDayReportSpInfo();
                    var base_info = item.base_info;
                    entity.Date = UnixTimeToDateTime(base_info.date);
                    entity.RecordType = base_info.record_type;
                    entity.Name = base_info.name;
                    entity.NameEx = base_info.name_ex;
                    entity.DepartsName = base_info.departs_name;
                    entity.UserID = base_info.acctid;
                    var rule_info = base_info.rule_info;
                    entity.Groupid = rule_info.groupid;
                    entity.GroupName = rule_info.groupname;
                    entity.ScheduleId = rule_info.scheduleid;
                    entity.ScheduleName = rule_info.schedulename;
                    var checkintime = rule_info.checkintime;
                    List<int> work_secList = new List<int>();
                    List<int> off_work_secList = new List<int>();
                    foreach (var check in checkintime)
                    {
                        work_secList.Add(check.work_sec);
                        off_work_secList.Add(check.off_work_sec);
                    }
                    if (work_secList.Count > 0 && off_work_secList.Count > 0)
                    {
                        entity.WorkSec = work_secList.Min();
                        entity.OffWorkSec = off_work_secList.Max();
                    }

                    var summary_info = item.summary_info;
                    entity.CheckinCount = summary_info.checkin_count;
                    entity.RegularWorkSec = summary_info.regular_work_sec;
                    entity.StandardWorkSec = summary_info.standard_work_sec;
                    entity.EarliestTime = summary_info.earliest_time;
                    entity.LastestTime = summary_info.lastest_time;

                    var holiday_infos = item.holiday_infos;
                    foreach (var holiday in holiday_infos)
                    {
                        var sp_description = holiday.sp_description;
                        var sp_title = holiday.sp_title;
                        spInfo.SpNumber = holiday.sp_number;
                        foreach (var description in sp_description.data)
                        {
                            spInfo.DescriptionLang = description.lang;
                            spInfo.DescriptionText = description.text;
                        }
                        foreach (var title in sp_title.data)
                        {
                            spInfo.TitleLang = title.lang;
                            spInfo.TitleText = title.text;
                        }
                    }
                    var exception_infos = item.exception_infos;
                    foreach (var eitem in exception_infos)
                    {
                        switch (eitem.exception)
                        {
                            case 1:
                                entity.LateCount = eitem.count;
                                entity.LateDuration = eitem.duration;
                                break;
                            case 2:
                                entity.LeaveEarlyCount = eitem.count;
                                entity.LeaveEarlyDuration = eitem.duration;
                                break;
                            case 3:
                                entity.AbsenceCount = eitem.count;
                                break;
                            case 4:
                                entity.AbsenteeismCount = eitem.count;
                                entity.AbsenteeismDuration = eitem.duration;
                                break;
                            case 5:
                                entity.LocationAbnormalCount = eitem.count;
                                break;
                            case 6:
                                entity.EquipmentAbnormalCount = eitem.count;
                                break;
                        }
                    }
                    var ot_info = item.ot_info;
                    entity.OtStatus = ot_info.ot_status;
                    entity.OtDuration = ot_info.ot_duration;
                    //entity.ExceptionDuration = ot_info.exception_duration;

                    var sp_items = item.sp_items;
                    foreach (var sitem in sp_items)
                    {
                        switch (sitem.type)
                        {
                            case 1:
                                switch (sitem.name)
                                {
                                    case "年假":
                                        entity.AnnualLeaveCount = sitem.count;
                                        entity.AnnualLeaveDuration = sitem.duration;
                                        entity.AnnualLeaveTimeType = sitem.time_type;
                                        break;
                                    case "事假":
                                        entity.CompassionateLeaveCount = sitem.count;
                                        entity.CompassionateLeaveDuration = sitem.duration;
                                        entity.CompassionateLeaveTimeType = sitem.time_type;
                                        break;
                                    case "病假":
                                        entity.SickLeaveCount = sitem.count;
                                        entity.SickLeaveDuration = sitem.duration;
                                        entity.SickLeaveTimeType = sitem.time_type;
                                        break;
                                    case "调休假":
                                        entity.CompensatoryLeaveCount = sitem.count;
                                        entity.CompensatoryLeaveDuration = sitem.duration;
                                        entity.CompensatoryLeaveTimeType = sitem.time_type;
                                        break;
                                    case "婚假":
                                        entity.MarriageHolidayCount = sitem.count;
                                        entity.MarriageHolidayDuration = sitem.duration;
                                        entity.MarriageHolidayTimeType = sitem.time_type;
                                        break;
                                    case "产假":
                                        entity.MaternityLeaveCount = sitem.count;
                                        entity.MaternityLeaveDuration = sitem.duration;
                                        entity.MaternityLeaveTimeType = sitem.time_type;
                                        break;
                                    case "陪产假":
                                        entity.PaternityLeaveCount = sitem.count;
                                        entity.PaternityLeaveDuration = sitem.duration;
                                        entity.PaternityLeaveTimeType = sitem.time_type;
                                        break;
                                    case "其他":
                                        entity.OtherLeaveCount = sitem.count;
                                        entity.OtherLeaveDuration = sitem.duration;
                                        entity.OtherLeaveTimeType = sitem.time_type;
                                        break;
                                }
                                break;
                            case 2:
                                entity.CardReplacementCount = sitem.count;
                                break;
                            case 3:
                                entity.BusinessCount = sitem.count;
                                entity.BusinessDuration = sitem.duration;
                                entity.BusinessTimeType = sitem.time_type;
                                break;
                            case 4:
                                entity.EgressCount = sitem.count;
                                entity.EgressDuration = sitem.duration;
                                entity.EgressTimeType = sitem.time_type;
                                break;
                            case 100:
                                entity.FieldCount = sitem.count;
                                entity.FieldDuration = sitem.duration;
                                entity.FieldTimeType = sitem.time_type;
                                break;
                        }
                    }
                    entity.spInfo = spInfo;
                    result.Add(entity);
                }
                bus.SyncCheckinDayReport(result);
            }
        }
        /// <summary>
        /// 同步月度汇总数据
        /// </summary>
        public void SyncCheckinReport()
        {
            QiyeBus bus = new QiyeBus();
            QyDepartEntity departEntity = new QyDepartEntity();
            departEntity.Parentid = 15;
            List<QyDepartEntity> departList = bus.QueryDepartList(departEntity);
            string departStr = "";
            foreach (var item in departList)
            {
                departStr += "'" + item.Id + "',";
            }
            departStr = departStr.TrimEnd(',');
            List<QyUserEntity> userList = bus.QueryDepartAllUserList(departStr);
            string userStr = "[";
            foreach (var item in userList)
            {
                userStr += "\"" + item.UserID + "\",";
            }
            userStr = userStr.TrimEnd(',');
            userStr += "]";

            AccessTokenResult token = CommonApi.GetToken(Common.GetQYCorpID(), "MiwmXdN9HasI7x16VuTvZAosC2K8qVD9WoV0sQ_AsvQ");
            int startUnixTime = DateTimeToUnixTime(DateTime.Now.AddDays(1 - DateTime.Now.Day).Date);
            int endUnixTime = DateTimeToUnixTime(DateTime.Now.AddDays(1 - DateTime.Now.Day).Date.AddMonths(1).AddSeconds(-1));
            string Url = "https://qyapi.weixin.qq.com/cgi-bin/checkin/getcheckin_monthdata?access_token=";
            string jsonData = "{\"starttime\": " + startUnixTime + ",\"endtime\": " + endUnixTime + ",\"useridlist\": " + userStr + "}";
            string returnJson = Post(Url + token.access_token, jsonData);
            JavaScriptSerializer js = new JavaScriptSerializer();
            CheckintMonthlyReportReturnJson list = js.Deserialize<CheckintMonthlyReportReturnJson>(returnJson);
            string errcode = list.errcode;
            string errmsg = list.errmsg;
            List<datas> checkindata = list.datas;
            List<QYCheckinMonthlyReportEntity> result = new List<QYCheckinMonthlyReportEntity>();

            if (checkindata.Count > 0)
            {
                foreach (var item in checkindata)
                {
                    QYCheckinMonthlyReportEntity entity = new QYCheckinMonthlyReportEntity();
                    var base_info = item.base_info;
                    var summary_info = item.summary_info;


                    entity.RecordType = base_info.record_type;
                    entity.Name = base_info.name;
                    entity.NameEx = base_info.name_ex;
                    entity.UserID = base_info.acctid;
                    entity.WorkDays = summary_info.work_days;
                    entity.RegularDays = summary_info.regular_days;
                    entity.ExceptDays = summary_info.except_days;
                    entity.RegularWorkSec = summary_info.regular_work_sec;
                    entity.StandardWorkSec = summary_info.standard_work_sec;


                    var exception_infos = item.exception_infos;
                    foreach (var eitem in exception_infos)
                    {
                        switch (eitem.exception)
                        {
                            case 1:
                                entity.LateCount = eitem.count;
                                entity.LateDuration = eitem.duration;
                                break;
                            case 2:
                                entity.LeaveEarlyCount = eitem.count;
                                entity.LeaveEarlyDuration = eitem.duration;
                                break;
                            case 3:
                                entity.AbsenceCount = eitem.count;
                                break;
                            case 4:
                                entity.AbsenteeismCount = eitem.count;
                                entity.AbsenteeismDuration = eitem.duration;
                                break;
                            case 5:
                                entity.LocationAbnormalCount = eitem.count;
                                break;
                            case 6:
                                entity.EquipmentAbnormalCount = eitem.count;
                                break;
                        }
                    }
                    var sp_items = item.sp_items;
                    foreach (var sitem in sp_items)
                    {
                        switch (sitem.type)
                        {
                            case 1:
                                switch (sitem.name)
                                {
                                    case "年假":
                                        entity.AnnualLeaveCount = sitem.count;
                                        entity.AnnualLeaveDuration = sitem.duration;
                                        entity.AnnualLeaveTimeType = sitem.time_type;
                                        break;
                                    case "事假":
                                        entity.CompassionateLeaveCount = sitem.count;
                                        entity.CompassionateLeaveDuration = sitem.duration;
                                        entity.CompassionateLeaveTimeType = sitem.time_type;
                                        break;
                                    case "病假":
                                        entity.SickLeaveCount = sitem.count;
                                        entity.SickLeaveDuration = sitem.duration;
                                        entity.SickLeaveTimeType = sitem.time_type;
                                        break;
                                    case "调休假":
                                        entity.CompensatoryLeaveCount = sitem.count;
                                        entity.CompensatoryLeaveDuration = sitem.duration;
                                        entity.CompensatoryLeaveTimeType = sitem.time_type;
                                        break;
                                    case "婚假":
                                        entity.MarriageHolidayCount = sitem.count;
                                        entity.MarriageHolidayDuration = sitem.duration;
                                        entity.MarriageHolidayTimeType = sitem.time_type;
                                        break;
                                    case "产假":
                                        entity.MaternityLeaveCount = sitem.count;
                                        entity.MaternityLeaveDuration = sitem.duration;
                                        entity.MaternityLeaveTimeType = sitem.time_type;
                                        break;
                                    case "陪产假":
                                        entity.PaternityLeaveCount = sitem.count;
                                        entity.PaternityLeaveDuration = sitem.duration;
                                        entity.PaternityLeaveTimeType = sitem.time_type;
                                        break;
                                    case "其他":
                                        entity.OtherLeaveCount = sitem.count;
                                        entity.OtherLeaveDuration = sitem.duration;
                                        entity.OtherLeaveTimeType = sitem.time_type;
                                        break;
                                }
                                break;
                            case 2:
                                entity.CardReplacementCount = sitem.count;
                                break;
                            case 3:
                                entity.BusinessCount = sitem.count;
                                entity.BusinessDuration = sitem.duration;
                                entity.BusinessTimeType = sitem.time_type;
                                break;
                            case 4:
                                entity.EgressCount = sitem.count;
                                entity.EgressDuration = sitem.duration;
                                entity.EgressTimeType = sitem.time_type;
                                break;
                            case 100:
                                entity.FieldCount = sitem.count;
                                entity.FieldDuration = sitem.duration;
                                entity.FieldTimeType = sitem.time_type;
                                break;
                        }
                    }
                    var overwork_info = item.overwork_info;

                    entity.WorkdayOverSec = overwork_info.workday_over_sec;
                    entity.HolidaysOverSec = overwork_info.holidays_over_sec;
                    entity.RestdaysOverSec = overwork_info.restdays_over_sec;
                    entity.ReportTime = DateTime.Now.ToString("yyyyMM");
                    result.Add(entity);
                }
                bus.SyncCheckinReport(result);
            }
        }
        /// <summary>
        /// 查询汇总数据
        /// </summary>
        public void QueryCheckinReportData()
        {
            QYCheckinMonthlyReportEntity queryEntity = new QYCheckinMonthlyReportEntity();

            QiyeBus bus = new QiyeBus();
            List<QYCheckinMonthlyReportEntity> list = new List<QYCheckinMonthlyReportEntity>();
            if (!string.IsNullOrEmpty(Request["ReportTime"]))
            {
                queryEntity.ReportTime = Request["ReportTime"].Trim();

                if (!string.IsNullOrEmpty(Convert.ToString(Request["UserID"])))
                {
                    queryEntity.UserID = Request["UserID"].Trim();
                }
                if (!string.IsNullOrEmpty(Convert.ToString(Request["Name"])))
                {
                    queryEntity.Name = Request["Name"].Trim();
                }
                if (queryEntity.ReportTime != DateTime.Now.Date.ToString("yyyyMM"))
                {
                    DateTime datetime = DateTime.ParseExact(queryEntity.ReportTime, "yyyyMM", System.Globalization.CultureInfo.CurrentCulture);
                    queryEntity.StartDate = datetime.AddDays(1 - datetime.Day).Date;
                    queryEntity.EndDate = datetime.AddDays(1 - datetime.Day).AddMonths(1).AddDays(-1);
                }
                else
                {
                    queryEntity.StartDate = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date;
                    queryEntity.EndDate = DateTime.Now.Date;
                }
                list = bus.QueryCheckinReportData(queryEntity);
                if (list.Count > 0)
                {
                    CheckinReportForExport = list;
                    TimeSpan sp = queryEntity.EndDate.Subtract(queryEntity.StartDate);
                    CheckinReportExportDay = sp.Days;
                }
            }
            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);

        }
        /// <summary>
        /// 导出
        /// </summary>
        public void QueryCheckinForExport()
        {
            QiyeBus bus = new QiyeBus();
            QYCheckinDataEntity queryEntity = new QYCheckinDataEntity();
            List<QYCheckinDataEntity> list = new List<QYCheckinDataEntity>();

            if (!string.IsNullOrEmpty(Request["CheckinType"]) && Request["CheckinType"] != "全部")
            {
                queryEntity.CheckinType = Request["CheckinType"].Trim();
            }

            if (!string.IsNullOrEmpty(Request["ExceptionType"]) && Request["ExceptionType"] != "全部")
            {
                queryEntity.ExceptionType = Request["ExceptionType"].Trim();
            }
            if (!string.IsNullOrEmpty(Request["UserName"]))
            {
                queryEntity.UserName = Request["UserName"].Trim();
            }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"])))
            {
                queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]);
            }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"])))
            {
                queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]);
            }
            list.AddRange(bus.QueryCheckinList(queryEntity));

            string err = "OK";
            if (list.Count > 0) { CheckinExport = list; }
            else { err = "没有数据可以进行导出，请重新查询！"; }
            //JSON
            String json = JSON.Encode(err);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 导出实体
        /// </summary>
        public List<QYCheckinDataEntity> CheckinExport
        {
            get
            {
                if (Session["CheckinExport"] == null)
                {
                    Session["CheckinExport"] = new List<QYCheckinDataEntity>();
                }
                return (List<QYCheckinDataEntity>)(Session["CheckinExport"]);
            }
            set
            {
                Session["CheckinExport"] = value;
            }
        }

        /// <summary>
        /// 导出
        /// </summary>
        public void QueryCheckinReportForExport()
        {
            string err = "OK";
            if (CheckinReportForExport.Count <= 0) { err = "没有数据可以进行导出，请重新查询！"; }
            //JSON
            String json = JSON.Encode(err);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 导出实体
        /// </summary>
        public List<QYCheckinMonthlyReportEntity> CheckinReportForExport
        {
            get
            {
                if (Session["CheckinReportForExport"] == null)
                {
                    Session["CheckinReportForExport"] = new List<QYCheckinMonthlyReportEntity>();
                }
                return (List<QYCheckinMonthlyReportEntity>)(Session["CheckinReportForExport"]);
            }
            set
            {
                Session["CheckinReportForExport"] = value;
            }
        }
        /// <summary>
        /// 导出实体
        /// </summary>
        public int CheckinReportExportDay
        {
            get
            {
                if (Session["CheckinReportExportDay"] == null)
                {
                    Session["CheckinReportExportDay"] = new int();
                }
                return (int)(Session["CheckinReportExportDay"]);
            }
            set
            {
                Session["CheckinReportExportDay"] = value;
            }
        }

        #endregion

        #region 考勤规则
        /// <summary>
        /// 获取所有部门和成员
        /// </summary>
        public void QueryAllOrganizeAndUser()
        {
            QiyeBus bus = new QiyeBus();
            List<QyDepartEntity> DepartList = bus.QueryDepartList();
            List<QyUserEntity> UserList = bus.QueryAllUserList();
            string res = "[";
            foreach (var it in DepartList)
            {
                if (it.Parentid.Equals(-1))
                {
                    continue;
                }
                if (it.Parentid.Equals(0))
                {
                    res += GetPersonnelNode(DepartList, UserList, it);
                }
            }

            res = res.Substring(0, res.Length - 1);
            res += "]";
            Response.Clear();
            Response.Write(res);
        }
        private string GetPersonnelNode(List<QyDepartEntity> DepartList, List<QyUserEntity> UserList, QyDepartEntity org)
        {
            string result = string.Empty;
            List<QyDepartEntity> secList = DepartList.FindAll(c => c.Parentid.Equals(org.Id));
            List<QyUserEntity> useList = UserList.FindAll(c => c.MainDepartment.Equals(org.Id));
            if (secList.Count > 0)
            {
                result += "{\"id\": \"⚑" + org.Id.ToString() + "\",\"text\":\"" + org.Name.Trim() + "\",\"children\":[";
                if (useList.Count > 0)
                {
                    foreach (var user in useList)
                    {
                        result += "{\"id\": \"" + user.UserID.ToString() + "\",\"text\":\"" + user.WxName.Trim() + "\"},";
                    }
                }
                foreach (var it in secList)
                {
                    result += GetPersonnelNode(DepartList, UserList, it);
                }
                result = result.Substring(0, result.Length - 1);
                result += "]},";
            }
            else
            {
                if (useList.Count > 0)
                {
                    result += "{\"id\": \"⚑" + org.Id.ToString() + "\",\"text\":\"" + org.Name.Trim() + "\",\"children\":[";
                    foreach (var user in useList)
                    {
                        result += "{\"id\": \"" + user.UserID.ToString() + "\",\"text\":\"" + user.WxName.Trim() + "\"},";
                    }
                    result = result.Substring(0, result.Length - 1);
                    result += "]},";
                }
                else
                {
                    result += "{\"id\": \"⚑" + org.Id.ToString() + "\",\"text\":\"" + org.Name.Trim() + "\"},";
                }
            }
            return result;
        }
        public void QueryCheckinRule()
        {
            CheckinRuleEntity queryEntity = new CheckinRuleEntity();
            queryEntity.RuleName = Convert.ToString(Request["RuleName"]);
            if (!string.IsNullOrEmpty(Request["RuleType"]))
            {
                queryEntity.RuleType = Convert.ToInt32(Request["RuleType"]);
            }
            else
            {
                queryEntity.RuleType = -1;
            }
            queryEntity.CheckinLocation = Convert.ToString(Request["CheckinLocation"]);
            //分页
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]);
            QiyeBus bus = new QiyeBus();
            Hashtable list = bus.QueryCheckinRule(pageIndex, pageSize, queryEntity);

            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }
        public void SaveCheckinRule()
        {
            CheckinRuleEntity ent = new CheckinRuleEntity();
            QiyeBus bus = new QiyeBus();
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "企业微信号";
            log.NvgPage = "考勤规则";
            log.Status = "0";
            log.UserID = UserInfor.LoginName.Trim();
            String id = Request["RuleID"] != null ? Request["RuleID"].ToString() : "";
            if (!string.IsNullOrEmpty(Request["RuleTimeID"]) || !string.IsNullOrEmpty(Request["RulePersonnelID"]))
            {
                msg.Message = "请求数据不完整，请重试";
                msg.Result = false;
            }
            try
            {
                ent.RuleName = Convert.ToString(Request["RuleName"]);
                ent.RuleType = Convert.ToInt32(Request["RuleType"]);
                ent.CheckinLocation = Convert.ToString(Request["CheckinLocation"]);
                ent.CheckinCoordinate = Convert.ToString(Request["CheckinCoordinate"]).Replace(",不设置", "");
                ent.CheckinScope = Convert.ToInt32(Request["CheckinScope"]);
                ent.ScopeOuterType = Convert.ToInt32(Request["ScopeOuterType"]);
                ent.AdditionalType = Convert.ToInt32(Request["AdditionalType"]);
                ent.AdditionalTime = Convert.ToInt32(Request["AdditionalTime"]);
                ent.AdditionalCount = Convert.ToInt32(Request["AdditionalCount"]);
                ent.WorkWeek = Convert.ToString(Request["WorkWeek"]);
                ent.ToWorkTime = Convert.ToString(Request["ToWorkTime"]);
                ent.OffWorkTime = Convert.ToString(Request["OffWorkTime"]);
                ent.ToWorkStartTime = Convert.ToString(Request["ToWorkStartTime"]);
                ent.ToWorkEndTime = Convert.ToString(Request["ToWorkEndTime"]);
                ent.OffWorkStartTime = Convert.ToString(Request["OffWorkStartTime"]);
                ent.OffWorkEndTime = Convert.ToString(Request["OffWorkEndTime"]);
                ent.BreakTimeType = Convert.ToInt32(Request["BreakTimeType"]);
                ent.BreakStartTime = Convert.ToString(Request["BreakStartTime"]);
                ent.BreakFinishTime = Convert.ToString(Request["BreakFinishTime"]);
                ent.FlexibleWorkType = Convert.ToInt32(Request["FlexibleWorkType"]);
                ent.AllowLateTime = Convert.ToInt32(Request["AllowLateTime"]);
                ent.AllowLeaveTime = Convert.ToInt32(Request["AllowLeaveTime"]);
                ent.EarlyArrivalDeparture = Convert.ToInt32(Request["EarlyArrivalDeparture"]);
                ent.LateArrivalDeparture = Convert.ToInt32(Request["LateArrivalDeparture"]);
                ent.LateOffToWorkType = Convert.ToInt32(Request["LateOffToWorkType"]);
                ent.LeaveLateTime = Convert.ToInt32(Request["LeaveLateTime"]);
                ent.ArriveLateTime = Convert.ToInt32(Request["ArriveLateTime"]);
                ent.OffWorkCheckinType = Convert.ToInt32(Request["OffWorkCheckinType"]);
                string[] Personnels = Convert.ToString(Request["Personnel"]).Split(',');
                foreach (var Personnel in Personnels)
                {
                    if (Personnel.IndexOf('⚑') == -1)
                    {
                        ent.CheckinUser += Personnel + ",";
                    }
                }
                ent.CheckinUser = ent.CheckinUser.Substring(0, ent.CheckinUser.Length - 1);
                if (msg.Result)
                {
                    if (id == "")
                    {
                        log.Operate = "A";
                        bus.AddCheckinRule(ent, log);
                        msg.Result = true;
                        msg.Message = "成功";
                    }
                    else
                    {
                        ent.RuleTimeID = Convert.ToInt32(Request["RuleTimeID"]);
                        ent.RulePersonnelID = Convert.ToInt32(Request["RulePersonnelID"]);
                        log.Operate = "U";
                        ent.RuleID = Convert.ToInt32(id);
                        bus.UpdateCheckinRule(ent, log);
                        msg.Result = true;
                        msg.Message = "成功";
                    }
                }
            }
            catch (Exception ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
        }
        public void DeleteCheckinRule()
        {
            String json = Request["data"];
            if (String.IsNullOrEmpty(json)) return;
            List<CheckinRuleEntity> list = new List<CheckinRuleEntity>();
            QiyeBus bus = new QiyeBus();
            ArrayList rows = (ArrayList)JSON.Decode(json);
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "企业微信号";
            log.NvgPage = "考勤规则";
            log.Status = "0";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "D";
            try
            {
                foreach (Hashtable row in rows)
                {
                    list.Add(new CheckinRuleEntity
                    {
                        ID = Convert.ToInt32(row["ID"]),
                        RuleName = Convert.ToString(row["RuleName"]),
                        CheckinLocation = Convert.ToString(row["CheckinLocation"]),
                        CheckinScope = Convert.ToInt32(row["CheckinScope"])
                    });
                }
                bus.DeleteCheckinRule(list, log);
                msg.Result = true;
                msg.Message = "成功";
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
        }
        #endregion
    }
}