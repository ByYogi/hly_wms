using Cargo.APP;
using House.Business;
using House.Business.Cargo;
using House.Business.House;
using House.Entity;
using House.Entity.Cargo;
using House.Entity.House;
using Memcached.ClientLibrary;
using NPOI.HSSF.Record.Formula.Functions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cargo.systempage
{
    public partial class sysService : BasePage
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
                log.UserID = UserInfor.LoginName.Trim();
                log.Memo = methodName + " " + ex.Message + " " + ex.StackTrace;
                bus.InsertLog(log);
            }
        }
        #region 申请审批操作方法集合
        /// <summary>
        /// 查询我的申请
        /// </summary>
        public void QueryMyApplication()
        {
            CargoApproveEntity queryEntity = new CargoApproveEntity();
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }
            queryEntity.Title = Convert.ToString(Request["Title"]);
            if (Convert.ToString(Request["ApplyType"]) != "-1")
            {
                queryEntity.ApplyType = Convert.ToString(Request["ApplyType"]);
            }
            if (Convert.ToString(Request["ApplyStatus"]) != "-1")
            {
                queryEntity.ApplyStatus = Convert.ToString(Request["ApplyStatus"]);
            }
            if (Convert.ToString(Request["IsMe"]).Equals("1"))
            {
                //查询自己的申请
                queryEntity.ApplyID = UserInfor.LoginName;
            }
            //分页
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]);
            CargoStaticBus obj = new CargoStaticBus();
            Hashtable list = obj.QueryMyApplication(pageIndex, pageSize, queryEntity);
            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);

        }
        /// <summary>
        /// 删除我的申请
        /// </summary>
        public void DelMyApplication()
        {
            String idStr = Request["data"];
            if (String.IsNullOrEmpty(idStr)) return;
            ArrayList rows = (ArrayList)JSON.Decode(idStr);
            List<CargoApproveEntity> list = new List<CargoApproveEntity>();
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "我的工作台";
            log.NvgPage = "我的申请";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "D";
            log.Status = "0";
            log.Memo = "删除申请单成功";
            CargoStaticBus bus = new CargoStaticBus();
            try
            {
                foreach (Hashtable row in rows)
                {
                    CargoApproveEntity Ent = bus.QueryApproveEntity(new CargoApproveEntity { ID = Convert.ToInt64(row["ID"]) });
                    if (string.IsNullOrEmpty(Ent.ApplyStatus) || !Ent.ApplyStatus.Equals("0"))
                    {
                        msg.Result = false;
                        msg.Message = "申请单号：" + Convert.ToString(row["ID"]) + "不是待审状态";
                        break;
                    }
                    list.Add(new CargoApproveEntity
                    {
                        ID = Convert.ToInt64(row["ID"]),
                        Title = Convert.ToString(row["Title"]),
                        ApplyID = Convert.ToString(row["ApplyID"]),
                        ApproveName = Convert.ToString(row["ApproveName"]),
                        AppSetID = Convert.ToInt32(row["AppSetID"]),
                        ApplyType = Convert.ToString(row["ApplyType"]),
                        Memo = Convert.ToString(row["Memo"])
                    });
                }
                if (msg.Result)
                {
                    bus.DelMyApprove(list, log);
                    msg.Message = "成功";
                    msg.Result = true;
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
        /// 删除 申请文件
        /// </summary>
        public void DelApproveFile()
        {
            String idStr = Request["ID"];
            if (String.IsNullOrEmpty(idStr)) return;
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "我的工作台";
            log.NvgPage = "我的申请";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "D";
            log.Status = "0";
            log.Memo = "删除申请附件成功";
            CargoStaticBus bus = new CargoStaticBus();

            try
            {
                CargoApproveFileEntity file = new CargoApproveFileEntity();
                file.ID = Convert.ToInt64(idStr);
                file.FileName = Convert.ToString(Request["FileName"]);

                bus.DelApproveFile(file, log);
                msg.Message = "成功";
                msg.Result = true;

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
        /// 查询到我的审批
        /// </summary>
        public void QueryMyCheck()
        {
            CargoApproveEntity queryEntity = new CargoApproveEntity();
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }
            queryEntity.Title = Convert.ToString(Request["Title"]);
            if (Convert.ToString(Request["ApplyType"]) != "-1")
            {
                queryEntity.ApplyType = Convert.ToString(Request["ApplyType"]);
            }
            if (Convert.ToString(Request["ApplyStatus"]) != "-1")
            {
                queryEntity.ApplyStatus = Convert.ToString(Request["ApplyStatus"]);
            }

            //查询到我的审批
            queryEntity.CheckUserID = UserInfor.LoginName;

            //分页
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]);
            CargoStaticBus obj = new CargoStaticBus();
            Hashtable list = obj.QueryMyCheck(pageIndex, pageSize, queryEntity);
            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 审批和拒审
        /// </summary>
        public void setApplicationCheck()
        {
            long ID = Convert.ToInt64(Request["ID"]);
            int ty = Convert.ToInt32(Request["ty"]);
            String DenyReason = Request["reason"];
            List<CargoApproveEntity> list = new List<CargoApproveEntity>();
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "我的工作台";
            log.NvgPage = "我的审批";
            log.UserID = UserInfor.LoginName.Trim();
            log.Status = "0";
            log.Operate = "U";
            CargoFinanceBus f = new CargoFinanceBus();
            CargoStaticBus bus = new CargoStaticBus();
            QiyeBus q = new QiyeBus();
            CargoOrderBus o = new CargoOrderBus();
            QyUserEntity qyU = q.QueryUser(new QyUserEntity { UserID = UserInfor.LoginName });
            CargoApproveEntity ApproveEntity = bus.QueryApproveEntity(new CargoApproveEntity { ID = ID });
            CargoApproveEntity result = new CargoApproveEntity();
            result.ID = ID;
            result.DenyReason = DenyReason;
            result.CurrID = UserInfor.LoginName;//当前审批人
            result.CurrName = UserInfor.UserName;//当前审批人姓名
            result.CheckTime = DateTime.Now;
            result.ApplyID = ApproveEntity.ApplyID;
            result.ApplyName = ApproveEntity.ApplyName;
            result.Title = ApproveEntity.Title;
            result.Memo = ApproveEntity.Memo;
            result.ApplyType = ApproveEntity.ApplyType;
            string copy = string.Empty;
            bool IsEnd = false;
            List<QyUserEntity> qyUser = new List<QyUserEntity>();
            QySendInfoEntity send = new QySendInfoEntity();
            send.url = "http://dlt.neway5.com/QY/qyApplicationCheck.aspx?ApproveID=" + ApproveEntity.ID.ToString();
            send.msgType = msgType.textcard;
            send.agentID = "1000010";//申请审批应用
            CargoApproveSetEntity appSet = f.QueryApproveSetByID(ApproveEntity.AppSetID);
            try
            {
                if (ApproveEntity.ApplyStatus.Equals("3"))
                {
                    msg.Result = false;
                    msg.Message = "审批结束的不允许审批";
                    goto ERR;
                }
                if (ty.Equals(1))
                {
                    #region 拒审
                    log.Memo = "拒审成功 ";

                    //查询审批流程数据
                    if (!qyU.CheckRole.Contains(ApproveEntity.NextCheckID))
                    {
                        msg.Result = false;
                        msg.Message = "申请单号:" + ApproveEntity.ID.ToString() + "未到您审批";
                        goto ERR;
                    }
                    result.ApplyStatus = "2";
                    result.NextCheckID = "";
                    result.NextCheckName = "";
                    list.Add(result);
                    bus.setApplicationCheck(list, ty, log);
                    qyUser.Add(new QyUserEntity { UserID = ApproveEntity.ApplyID, WxName = ApproveEntity.ApplyName });
                    if (ApproveEntity.ApplyType.Equals("2"))
                    {
                        //轮胎外调申请审批
                        List<CargoApproveRelateEntity> aRelate = bus.QueryApproveThrowRelateList(new CargoApproveRelateEntity { ApproveID = ApproveEntity.ID, ApplyType = ApproveEntity.ApplyType });
                        int totalNum = 0; decimal totalCharge = 0.00M;
                        foreach (var it in aRelate)
                        {
                            totalNum += it.ThrowNum;
                            totalCharge += it.ThrowNum * it.ThrowCharge;
                        }
                        send.title = "申请单：" + ApproveEntity.ID.ToString() + "审批拒审";

                        send.content = "<div></div><div>申请标题：" + ApproveEntity.Title + "</div><div>外调仓库：" + ApproveEntity.ThrowHouseName + " " + ApproveEntity.ClientName + "</div><div>外调数量：" + totalNum.ToString() + "条</div><div>外调金额：" + totalCharge.ToString("F2") + "元</div><div>审批人：" + UserInfor.UserName + "</div><div>拒审意见：" + result.DenyReason + "</div><div></div><div class=\"highlight\">请重新提交审批！</div>";
                    }
                    else if (ApproveEntity.ApplyType.Equals("4"))
                    {
                        send.title = "申请单：" + ApproveEntity.ID.ToString() + "审批拒审";
                        send.content = "<div></div><div>申请类型：加班申请</div><div>申请内容：" + ApproveEntity.Memo + "</div><div>加班时间：" + ApproveEntity.OStartTime.ToString("yyyy-MM-dd HH:mm") + "--" + ApproveEntity.OEndTime.ToString("yyyy-MM-dd HH:mm") + "</div><div>加班时长：" + ApproveEntity.OTime.ToString("F1") + "小时</div><div>审批人：" + UserInfor.UserName + "</div><div>拒审意见：" + result.DenyReason + "</div><div></div><div class=\"highlight\">请点击本通知进行审批！</div>";
                    }

                    foreach (var it in qyUser)
                    {
                        send.toUser = it.UserID;
                        WxQYSendHelper.QiyePushInfo(send);
                        Common.WriteTextLog("推送成功：" + it.WxName + ApproveEntity.ID.ToString());
                    }

                    #endregion
                }
                else
                {
                    log.Memo = "审批成功 ";
                    //查询审批流程数据
                    if (!qyU.CheckRole.Contains(ApproveEntity.NextCheckID))
                    {
                        msg.Result = false;
                        msg.Message = "申请单号:" + ApproveEntity.ID.ToString() + "未到您审批";
                        goto ERR;
                    }

                    string NextCheckID = ApproveEntity.NextCheckID;//下一审批人
                    if (appSet.OneCheckID.Equals(NextCheckID))
                    {
                        #region 第一级
                        send.title = appSet.OneCheckName + "审批通过";
                        result.ApplyStatus = "1";
                        result.NextCheckID = appSet.TwoCheckID;
                        result.NextCheckName = appSet.TwoCheckName;
                        if (string.IsNullOrEmpty(appSet.TwoCheckID))
                        {
                            send.title = "申请单：" + ApproveEntity.ID.ToString() + "审批结束";
                            //表示是最后一级
                            IsEnd = true;
                            result.ApplyStatus = "3";
                            result.NextCheckID = "";// AppSet.OneCheckID;
                            result.NextCheckName = "";// AppSet.OneCheckName;
                        }
                        else
                        {
                            //判断下一级是不是分公司领导，如果是则查询申请人的上级领导
                            if (appSet.TwoCheckID.Equals("3"))
                            {
                                List<SystemUserEntity> Bosslist = o.QueryUserBossLoginName(new SystemUserEntity { LoginName = ApproveEntity.ApplyID });
                                if (Bosslist.Count > 0)
                                {
                                    if (!string.IsNullOrEmpty(Bosslist[0].LoginName))
                                    {
                                        qyUser.Add(new QyUserEntity { UserID = Bosslist[0].LoginName, WxName = Bosslist[0].UserName });
                                    }
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(appSet.ThreeCheckID))
                                        {
                                            qyUser = q.QueryUserList(new QyUserEntity { CheckRole = appSet.ThreeCheckID, CheckHouseID = appSet.HouseID.ToString() });
                                        }
                                        else
                                        {
                                            //结束 
                                            send.title = "申请单：" + ApproveEntity.ID.ToString() + "审批结束";
                                            //表示是最后一级
                                            IsEnd = true;
                                            result.ApplyStatus = "3";
                                            result.NextCheckID = "";// AppSet.OneCheckID;
                                            result.NextCheckName = "";// AppSet.OneCheckName;
                                        }
                                    }
                                }
                            }
                            else if (appSet.TwoCheckID.Equals("7"))
                            {
                                qyUser = q.QueryUserList(new QyUserEntity { CheckRole = appSet.TwoCheckID, HouseID = ApproveEntity.HouseID, CheckHouseID = appSet.HouseID.ToString() });
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(appSet.TwoCheckID))
                                {
                                    qyUser = q.QueryUserList(new QyUserEntity { CheckRole = appSet.TwoCheckID, CheckHouseID = appSet.HouseID.ToString() });
                                }
                            }
                        }
                        #endregion
                    }
                    else if (appSet.TwoCheckID.Equals(NextCheckID))
                    {
                        #region 第二级
                        send.title = appSet.TwoCheckName + "审批通过";
                        result.ApplyStatus = "1";
                        result.NextCheckID = appSet.ThreeCheckID;
                        result.NextCheckName = appSet.ThreeCheckName;
                        if (string.IsNullOrEmpty(appSet.ThreeCheckID))
                        {
                            send.title = "申请单：" + ApproveEntity.ID.ToString() + "审批结束";
                            //表示是最后一级
                            IsEnd = true;
                            result.ApplyStatus = "3";
                            result.NextCheckID = "";// AppSet.OneCheckID;
                            result.NextCheckName = "";// AppSet.OneCheckName;
                        }
                        else
                        {
                            qyUser = q.QueryUserList(new QyUserEntity { CheckRole = appSet.ThreeCheckID, CheckHouseID = appSet.HouseID.ToString() });
                        }
                        #endregion
                    }
                    else if (appSet.ThreeCheckID.Equals(NextCheckID))
                    {
                        #region 第三级
                        send.title = appSet.ThreeCheckName + "审批通过";
                        result.ApplyStatus = "1";
                        result.NextCheckID = appSet.FourCheckID;
                        result.NextCheckName = appSet.FourCheckName;
                        if (string.IsNullOrEmpty(appSet.FourCheckID))
                        {
                            send.title = "申请单：" + ApproveEntity.ID.ToString() + "审批结束";
                            //表示是最后一级
                            IsEnd = true;
                            result.ApplyStatus = "3";
                            result.NextCheckID = "";// AppSet.OneCheckID;
                            result.NextCheckName = "";// AppSet.OneCheckName;
                        }
                        else
                        {
                            qyUser = q.QueryUserList(new QyUserEntity { CheckRole = appSet.FourCheckID, CheckHouseID = appSet.HouseID.ToString() });
                        }
                        #endregion
                    }
                    else if (appSet.FourCheckID.Equals(NextCheckID))
                    {
                        #region 第四级
                        send.title = appSet.FourCheckName + "审批通过";
                        result.ApplyStatus = "1";
                        result.NextCheckID = appSet.FiveCheckID;
                        result.NextCheckName = appSet.FiveCheckName;
                        if (string.IsNullOrEmpty(appSet.FiveCheckID))
                        {
                            send.title = "申请单：" + ApproveEntity.ID.ToString() + "审批结束";
                            //表示是最后一级
                            IsEnd = true;
                            result.ApplyStatus = "3";
                            result.NextCheckID = "";// AppSet.OneCheckID;
                            result.NextCheckName = "";// AppSet.OneCheckName;
                        }
                        else
                        {
                            qyUser = q.QueryUserList(new QyUserEntity { CheckRole = appSet.FiveCheckID, CheckHouseID = appSet.HouseID.ToString() });
                        }
                        #endregion
                    }
                    else if (appSet.FiveCheckID.Equals(NextCheckID))
                    {
                        #region 第五级
                        send.title = appSet.FiveCheckName + "审批通过";
                        result.ApplyStatus = "1";
                        result.NextCheckID = appSet.SixCheckID;
                        result.NextCheckName = appSet.SixCheckName;
                        if (string.IsNullOrEmpty(appSet.SixCheckID))
                        {
                            send.title = "申请单：" + ApproveEntity.ID.ToString() + "审批结束";
                            //表示是最后一级
                            IsEnd = true;
                            result.ApplyStatus = "3";
                            result.NextCheckID = "";// AppSet.OneCheckID;
                            result.NextCheckName = "";// AppSet.OneCheckName;
                        }
                        else
                        {
                            qyUser = q.QueryUserList(new QyUserEntity { CheckRole = appSet.SixCheckID, CheckHouseID = appSet.HouseID.ToString() });
                        }
                        #endregion
                    }
                    else if (appSet.SixCheckID.Equals(NextCheckID))
                    {
                        #region 第六级
                        send.title = appSet.SixCheckName + "审批通过";
                        result.ApplyStatus = "1";
                        result.NextCheckID = appSet.SevenCheckID;
                        result.NextCheckName = appSet.SevenCheckName;
                        if (string.IsNullOrEmpty(appSet.SevenCheckID))
                        {
                            send.title = "申请单：" + ApproveEntity.ID.ToString() + "审批结束";
                            //表示是最后一级
                            IsEnd = true;
                            result.ApplyStatus = "3";
                            result.NextCheckID = "";// AppSet.OneCheckID;
                            result.NextCheckName = "";// AppSet.OneCheckName;
                        }
                        else
                        {
                            qyUser = q.QueryUserList(new QyUserEntity { CheckRole = appSet.SevenCheckID, CheckHouseID = appSet.HouseID.ToString() });
                        }
                        #endregion
                    }
                    else if (appSet.SevenCheckID.Equals(NextCheckID))
                    {
                        #region 第七级
                        send.title = appSet.SevenCheckName + "审批通过";
                        result.ApplyStatus = "1";
                        result.NextCheckID = appSet.EightCheckID;
                        result.NextCheckName = appSet.EightCheckName;
                        if (string.IsNullOrEmpty(appSet.EightCheckID))
                        {
                            send.title = "申请单：" + ApproveEntity.ID.ToString() + "审批结束";
                            //表示是最后一级
                            IsEnd = true;
                            result.ApplyStatus = "3";
                            result.NextCheckID = "";// AppSet.OneCheckID;
                            result.NextCheckName = "";// AppSet.OneCheckName;
                        }
                        else
                        {
                            qyUser = q.QueryUserList(new QyUserEntity { CheckRole = appSet.EightCheckID, CheckHouseID = appSet.HouseID.ToString() });
                        }
                        #endregion
                    }
                    list.Add(result);
                    bus.setApplicationCheck(list, ty, log);
                    qyUser.Add(new QyUserEntity { UserID = ApproveEntity.ApplyID, WxName = ApproveEntity.ApplyName });
                }
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
                goto ERR;
            }

            if (IsEnd)
            {
                #region 抄送审批角色和审批人
                List<QyUserEntity> cQyU = new List<QyUserEntity>();
                if (!string.IsNullOrEmpty(appSet.CopyCheck))
                {
                    cQyU = q.QueryUserList(new QyUserEntity { CheckRole = appSet.CopyCheck });
                }
                foreach (var c in cQyU)
                {
                    //添加申请单与审批人关系数据
                    if (!f.IsExistApproveCheck(new CargoApproveCheckEntity { ApproveID = ApproveEntity.ID, CheckUserID = c.UserID, ApproveType = ApproveEntity.ApplyType }))
                    {
                        f.AddApproveCheck(new CargoApproveCheckEntity
                        {
                            ApproveID = ApproveEntity.ID,
                            CheckUserID = c.UserID,
                            CheckName = c.WxName,
                            CheckType = "1",
                            ReadStatus = "1",
                            ApproveType = ApproveEntity.ApplyType
                        }, log);
                    }
                    if (!copy.Contains(c.UserID))
                    {
                        copy += c.UserID + ",";
                    }
                }
                if (!string.IsNullOrEmpty(appSet.CopyUserID) && !string.IsNullOrEmpty(appSet.CopyCheckName))
                {
                    string[] uS = appSet.CopyUserID.Split(',');
                    string[] uName = appSet.CopyUserName.Split(',');
                    if (uS.Length > 0)
                    {
                        for (int i = 0; i < uS.Length; i++)
                        {
                            if (!string.IsNullOrEmpty(uS[i]))
                            {
                                //添加申请单与审批人关系数据
                                if (!f.IsExistApproveCheck(new CargoApproveCheckEntity { ApproveID = ApproveEntity.ID, CheckUserID = uS[i], ApproveType = ApproveEntity.ApplyType }))
                                {
                                    f.AddApproveCheck(new CargoApproveCheckEntity
                                    {
                                        ApproveID = ApproveEntity.ID,
                                        CheckUserID = uS[i],
                                        CheckName = uName[i],
                                        CheckType = "1",
                                        ReadStatus = "1",
                                        ApproveType = ApproveEntity.ApplyType
                                    }, log);
                                }
                                if (!copy.Contains(uS[i]))
                                {
                                    copy += uS[i] + ",";
                                }
                            }
                        }
                    }
                }
                #endregion
                if (ApproveEntity.ApplyType.Equals("8"))
                {
                    //客户资料审批结束，修改客户的状态 为可用 并修改透支额度为申请 额度
                    //轮胎外调申请审批
                    List<CargoApproveRelateEntity> aRelate = bus.QueryApproveThrowRelateList(new CargoApproveRelateEntity { ApproveID = ApproveEntity.ID, ApplyType = "8" });
                    if (aRelate.Count > 0)
                    {
                        CargoClientBus cBus = new CargoClientBus();
                        cBus.UpdateCargoClientBookCheck(new CargoClientEntity { ClientID = aRelate[0].RelateID, BookCheck = "1", LimitMoney = ApproveEntity.LimitMoney }, log);
                    }
                }
            }
            if (ApproveEntity.ApplyType.Equals("2"))
            {
                //轮胎外调申请审批
                List<CargoApproveRelateEntity> aRelate = bus.QueryApproveThrowRelateList(new CargoApproveRelateEntity { ApproveID = ApproveEntity.ID, ApplyType = "2" });
                int totalNum = 0; decimal totalCharge = 0.00M;
                foreach (var it in aRelate)
                {
                    totalNum += it.ThrowNum;
                    totalCharge += it.ThrowNum * it.ThrowCharge;
                }

                send.content = "<div></div><div>申请类型：轮胎外调</div><div>申请标题：" + ApproveEntity.Title + "</div><div>外调仓库：" + ApproveEntity.ThrowHouseName + " " + ApproveEntity.ClientName + "</div><div>外调数量：" + totalNum.ToString() + "条</div><div>外调金额：" + totalCharge.ToString("F2") + "元</div><div>审批人：" + UserInfor.UserName + "</div><div>审批意见：" + result.DenyReason + "</div><div></div><div class=\"highlight\">请点击本通知进行审批！</div>";
            }
            else if (ApproveEntity.ApplyType.Equals("4"))
            {
                //加班申请审批
                //send.title = "加班申请";
                send.content = "<div></div><div>申请人：" + ApproveEntity.ApplyName + "</div><div>申请内容：" + ApproveEntity.Memo + "</div><div>加班时间：" + ApproveEntity.OStartTime.ToString("yyyy-MM-dd HH:mm") + "--" + ApproveEntity.OEndTime.ToString("yyyy-MM-dd HH:mm") + "</div><div>加班时长：" + ApproveEntity.OTime.ToString("F1") + "小时</div><div></div><div class=\"highlight\">请点击本通知进行审批！</div>";
            }
            else if (ApproveEntity.ApplyType.Equals("8"))
            {
                send.content = "<div></div><div>申请类型：客户资料审批</div><div>申请标题：" + ApproveEntity.Title + "</div><div>申请额度：" + ApproveEntity.LimitMoney.ToString("F2") + "</div><div>申请内容：" + ApproveEntity.Memo + "</div><div>审批人：" + UserInfor.UserName + "</div><div>审批意见：" + result.DenyReason + "</div><div></div><div class=\"highlight\">请点击本通知进行审批！</div>";
            }
            foreach (var it in qyUser)
            {
                try
                {
                    send.toUser = it.UserID;
                    if (!f.IsExistApproveCheck(new CargoApproveCheckEntity { ApproveID = ApproveEntity.ID, ApproveType = ApproveEntity.ApplyType, CheckUserID = it.UserID, CheckType = "0" }))
                    {
                        f.AddApproveCheck(new CargoApproveCheckEntity { ApproveID = ApproveEntity.ID, CheckUserID = it.UserID, CheckName = it.WxName, ReadStatus = "1", CheckType = "0", ApproveType = ApproveEntity.ApplyType }, log);
                    }
                    WxQYSendHelper.QiyePushInfo(send);
                    Common.WriteTextLog("推送成功：" + it.WxName + ApproveEntity.ID.ToString());
                }
                catch (ApplicationException ex)
                {
                    continue;
                }
            }
            if (!string.IsNullOrEmpty(copy))
            {
                copy = copy.Substring(0, copy.Length - 1);
                try
                {
                    send.toUser = copy;
                    WxQYSendHelper.QiyePushInfo(send);
                }
                catch (Exception ex)
                {
                    msg.Result = true;
                }
            }


        ERR:
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
        }
        /// <summary>
        /// 保存申请
        /// </summary>
        public void SaveApplication()
        {
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "我的工作台";
            log.NvgPage = "起草申请";
            log.UserID = UserInfor.LoginName.Trim();
            log.Status = "0"; log.Operate = "A";
            string formData = Request["formData"];
            string Relate = Request["Relate"];
            List<QyUserEntity> qyUser = new List<QyUserEntity>();
            CargoApproveEntity entity = new CargoApproveEntity();
            CargoFinanceBus fin = new CargoFinanceBus();
            QiyeBus q = new QiyeBus();
            CargoOrderBus b = new CargoOrderBus();
            CargoHouseBus h = new CargoHouseBus();
            CargoStaticBus statis = new CargoStaticBus();
            string ClientName = string.Empty;
            int totalNum = 0; decimal totalCharge = 0.00M;
            #region 处理申请
            if (!string.IsNullOrEmpty(formData))
            {
                ArrayList FormRows = (ArrayList)JSON.Decode("[" + formData + "]");
                foreach (Hashtable row in FormRows)
                {
                    entity.ID = string.IsNullOrEmpty(Convert.ToString(row["ID"])) ? 0 : Convert.ToInt64(row["ID"]);
                    entity.Title = Convert.ToString(row["Title"]).Trim();
                    entity.ApplyID = UserInfor.LoginName;
                    entity.ApplyName = UserInfor.UserName;
                    entity.ApplyDate = Convert.ToDateTime(row["ApproveDate"]);
                    entity.Memo = Convert.ToString(row["Memo"]);
                    entity.ApplyType = Convert.ToString(row["ApplyType"]);
                    List<CargoApproveSetEntity> aset = fin.QueryApproveSet(new CargoApproveSetEntity { ApproveType = Convert.ToString(row["ApplyType"]), DelFlag = "0" });
                    entity.CurrID = UserInfor.LoginName;
                    entity.CurrName = UserInfor.UserName;
                    entity.CheckTime = DateTime.Now;
                    entity.ApplyStatus = "0";//待审
                    entity.HouseID = UserInfor.HouseID;
                    entity.ThrowHouse = Convert.ToInt32(row["HouseID"]);
                    CargoHouseEntity houseEnt = h.QueryCargoHouseByID(Convert.ToInt32(row["HouseID"]));
                    entity.ThrowHouseName = houseEnt.Name;
                    entity.ClientID = Convert.ToInt64(row["ClientID"]);
                    ClientName = Convert.ToString(row["ClientName"]);
                    entity.ClientName = ClientName;
                    CargoApproveSetEntity AppSet = new CargoApproveSetEntity();
                    if (aset.Count > 0) { AppSet = aset[0]; }
                    entity.AppSetID = AppSet.ID;
                    //2.审批流程的每一级和当前人的审批角色相匹配
                    #region 审批流程
                    if (!string.IsNullOrEmpty(AppSet.OneCheckID))
                    {
                        entity.NextCheckID = AppSet.OneCheckID;//下一审批人
                        entity.NextCheckName = AppSet.OneCheckName;
                        //判断如果是分公司领导就查找该 分公司领导是谁
                        if (AppSet.OneCheckID.Equals("3"))
                        {
                            List<SystemUserEntity> Bosslist = b.QueryUserBossLoginName(new SystemUserEntity { LoginName = UserInfor.LoginName });
                            if (Bosslist.Count > 0)
                            {
                                if (!string.IsNullOrEmpty(Bosslist[0].LoginName))
                                {
                                    qyUser.Add(new QyUserEntity { UserID = Bosslist[0].LoginName, WxName = Bosslist[0].UserName });
                                }
                                else
                                {
                                    entity.NextCheckID = AppSet.TwoCheckID;//下一审批人
                                    entity.NextCheckName = AppSet.TwoCheckName;
                                    qyUser = q.QueryUserList(new QyUserEntity { CheckRole = AppSet.TwoCheckID, CheckHouseID = UserInfor.HouseID.ToString() });
                                }
                            }
                        }
                        else if (AppSet.OneCheckID.Equals("7"))
                        {
                            //分公司财务
                            qyUser = q.QueryUserList(new QyUserEntity { CheckRole = AppSet.OneCheckID, HouseID = UserInfor.HouseID, CheckHouseID = UserInfor.HouseID.ToString() });
                            if (qyUser.Count <= 0)
                            {
                                entity.NextCheckID = AppSet.TwoCheckID;//下一审批人
                                entity.NextCheckName = AppSet.TwoCheckName;
                                if (AppSet.TwoCheckID.Equals("3"))
                                {
                                    List<SystemUserEntity> Bosslist = b.QueryUserBossLoginName(new SystemUserEntity { LoginName = UserInfor.LoginName });
                                    if (Bosslist.Count > 0)
                                    {
                                        if (!string.IsNullOrEmpty(Bosslist[0].LoginName))
                                        {
                                            qyUser.Add(new QyUserEntity { UserID = Bosslist[0].LoginName, WxName = Bosslist[0].UserName });
                                        }
                                    }
                                }
                                else
                                {
                                    qyUser = q.QueryUserList(new QyUserEntity { CheckRole = AppSet.TwoCheckID, CheckHouseID = UserInfor.HouseID.ToString() });
                                }
                            }
                        }
                        else
                        {
                            qyUser = q.QueryUserList(new QyUserEntity { CheckRole = AppSet.OneCheckID, CheckHouseID = UserInfor.HouseID.ToString() });
                        }
                    }
                    #endregion
                    List<CargoApproveRelateEntity> relist = new List<CargoApproveRelateEntity>();
                    if (!string.IsNullOrEmpty(Relate))
                    {
                        ArrayList rel = (ArrayList)JSON.Decode(Relate);
                        foreach (Hashtable la in rel)
                        {
                            relist.Add(new CargoApproveRelateEntity
                            {
                                RelateID = Convert.ToInt64(la["ID"]),
                                ThrowNum = Convert.ToInt32(la["InPiece"]) - Convert.ToInt32(la["Piece"]),
                                ThrowCharge = Convert.ToDecimal(la["ActSalePrice"]),
                                ApplyType = entity.ApplyType
                            });
                            totalNum += Convert.ToInt32(la["InPiece"]) - Convert.ToInt32(la["Piece"]);
                            totalCharge += (Convert.ToInt32(la["InPiece"]) - Convert.ToInt32(la["Piece"])) * Convert.ToDecimal(la["ActSalePrice"]);
                        }
                    }
                    entity.RelateList = relist;
                }
            }
            #endregion

            #region 处理照片
            List<CargoApproveFileEntity> fil = new List<CargoApproveFileEntity>();
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFile imgFile = Request.Files[i];
                if (string.IsNullOrEmpty(imgFile.FileName)) { continue; }
                string imgname = string.Empty;
                Common.SaveComputerPic(imgFile, ref imgname, true);
                CargoApproveFileEntity f = new CargoApproveFileEntity();
                f.FileName = imgname;
                f.FilePath = "../upload/Approve/" + imgname;

                string[] fs = imgFile.FileName.Split('.');
                // 保存图片类型
                switch (fs[fs.Length - 1].ToUpper().Trim())
                {
                    case "WPS": f.FileType = "2"; f.FileExtension = "wps"; break;
                    case "TXT": f.FileType = "2"; f.FileExtension = "txt"; break;
                    case "DOC": f.FileType = "2"; f.FileExtension = "doc"; break;
                    case "DOCX": f.FileType = "2"; f.FileExtension = "docx"; break;
                    case "XLS": f.FileType = "2"; f.FileExtension = "xls"; break;
                    case "XLSX": f.FileType = "2"; f.FileExtension = "xlsx"; break;
                    case "PPT": f.FileType = "2"; f.FileExtension = "ppt"; break;
                    case "PPTX": f.FileType = "2"; f.FileExtension = "pptx"; break;
                    case "PDF": f.FileType = "2"; f.FileExtension = "pdf"; break;
                    case "JPG": f.FileType = "1"; f.FileExtension = "jpg"; break;
                    case "GIF": f.FileType = "1"; f.FileExtension = "gif"; break;
                    case "PNG": f.FileType = "1"; f.FileExtension = "png"; break;
                    case "BMP": f.FileType = "1"; f.FileExtension = "bmp"; break;
                    case "JPEG": f.FileType = "1"; f.FileExtension = "jpeg"; break;
                    case "ZIP": f.FileType = "2"; f.FileExtension = "zip"; break;
                    case "RAR": f.FileType = "2"; f.FileExtension = "rar"; break;
                    default: f.FileType = "2"; break;
                }
                fil.Add(f);
            }
            #endregion

            entity.FileList = fil;
            //新增申请
            long AID = statis.AddApplication(entity, log);
            msg.Result = true;
            msg.Message = "新增成功";
            entity.ID = AID;
            try
            {
                //推送企业号通知 推送申请审批通知
                QySendInfoEntity send = new QySendInfoEntity();
                send.agentID = "1000010";//申请审批应用
                send.msgType = msgType.textcard;
                send.url = "http://dlt.neway5.com/QY/qyApplicationCheck.aspx?ApproveID=" + entity.ID.ToString();
                switch (entity.ApplyType)
                {
                    case "2":
                        send.title = "轮胎外调申请";
                        send.content = "<div></div><div>申请类型：轮胎外调</div><div>申请标题：" + entity.Title + "</div><div>外调仓库：" + entity.ThrowHouseName + " " + ClientName + "</div><div>外调数量：" + totalNum.ToString() + "条</div><div>外调金额：" + totalCharge.ToString("F2") + "元</div><div>申请内容：" + entity.Memo + "</div><div></div><div class=\"highlight\">请点击本通知进行审批！</div>";
                        break;
                    default:
                        break;
                }
                foreach (var it in qyUser)
                {
                    send.toUser = it.UserID;
                    if (!fin.IsExistApproveCheck(new CargoApproveCheckEntity { ApproveID = entity.ID, ApproveType = entity.ApplyType, CheckUserID = it.UserID, CheckType = "0" }))
                    {
                        fin.AddApproveCheck(new CargoApproveCheckEntity { ApproveID = entity.ID, CheckUserID = it.UserID, CheckName = it.WxName, ReadStatus = "1", CheckType = "0", ApproveType = entity.ApplyType }, log);
                    }
                    WxQYSendHelper.QiyePushInfo(send);
                    Common.WriteTextLog("推送成功：" + it.WxName + entity.ID.ToString());
                }

            }
            catch (ApplicationException ex)
            {
                msg.Message = "成功";
                msg.Result = true;
            }
            String dd = JSON.Encode(msg);
            Response.Clear();
            Response.Write(dd);
        }
        /// <summary>
        /// 修改申请
        /// </summary>
        public void UpdateApplication()
        {
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "我的工作台";
            log.NvgPage = "我的申请";
            log.UserID = UserInfor.LoginName.Trim();
            log.Status = "0"; log.Operate = "U";
            string formData = Request["formData"];
            string Relate = Request["Relate"];
            List<QyUserEntity> qyUser = new List<QyUserEntity>();
            CargoApproveEntity entity = new CargoApproveEntity();
            CargoFinanceBus fin = new CargoFinanceBus();
            QiyeBus q = new QiyeBus();
            CargoOrderBus b = new CargoOrderBus();
            CargoHouseBus h = new CargoHouseBus();
            CargoStaticBus statis = new CargoStaticBus();
            string ClientName = string.Empty;
            int totalNum = 0; decimal totalCharge = 0.00M;
            #region 处理申请
            if (!string.IsNullOrEmpty(formData))
            {
                ArrayList FormRows = (ArrayList)JSON.Decode("[" + formData + "]");
                foreach (Hashtable row in FormRows)
                {
                    entity.ID = string.IsNullOrEmpty(Convert.ToString(row["ID"])) ? 0 : Convert.ToInt64(row["ID"]);
                    entity.Title = Convert.ToString(row["Title"]).Trim();
                    entity.ApplyID = UserInfor.LoginName;
                    entity.ApplyName = UserInfor.UserName;
                    entity.ApplyDate = Convert.ToDateTime(row["ApplyDate"]);
                    entity.Memo = Convert.ToString(row["Memo"]);
                    entity.ApplyType = Convert.ToString(row["ApplyType"]);
                    List<CargoApproveSetEntity> aset = fin.QueryApproveSet(new CargoApproveSetEntity { ApproveType = Convert.ToString(row["ApplyType"]), DelFlag = "0" });
                    entity.CurrID = UserInfor.LoginName;
                    entity.CurrName = UserInfor.UserName;
                    entity.CheckTime = DateTime.Now;
                    entity.ApplyStatus = "0";//待审
                    entity.HouseID = UserInfor.HouseID;
                    entity.ThrowHouse = Convert.ToInt32(row["ThrowHouse"]);
                    CargoHouseEntity houseEnt = h.QueryCargoHouseByID(Convert.ToInt32(row["ThrowHouse"]));
                    entity.ThrowHouseName = houseEnt.Name;
                    entity.ClientID = Convert.ToInt64(row["ClientID"]);
                    ClientName = Convert.ToString(row["ClientName"]);
                    entity.ClientName = ClientName;
                    CargoApproveSetEntity AppSet = new CargoApproveSetEntity();
                    if (aset.Count > 0) { AppSet = aset[0]; }
                    entity.AppSetID = AppSet.ID;
                    //2.审批流程的每一级和当前人的审批角色相匹配
                    #region 审批流程
                    if (!string.IsNullOrEmpty(AppSet.OneCheckID))
                    {
                        entity.NextCheckID = AppSet.OneCheckID;//下一审批人
                        entity.NextCheckName = AppSet.OneCheckName;
                        //判断如果是分公司领导就查找该 分公司领导是谁
                        if (AppSet.OneCheckID.Equals("3"))
                        {
                            List<SystemUserEntity> Bosslist = b.QueryUserBossLoginName(new SystemUserEntity { LoginName = UserInfor.LoginName });
                            if (Bosslist.Count > 0)
                            {
                                if (!string.IsNullOrEmpty(Bosslist[0].LoginName))
                                {
                                    qyUser.Add(new QyUserEntity { UserID = Bosslist[0].LoginName, WxName = Bosslist[0].UserName });
                                }
                                else
                                {
                                    entity.NextCheckID = AppSet.TwoCheckID;//下一审批人
                                    entity.NextCheckName = AppSet.TwoCheckName;
                                    qyUser = q.QueryUserList(new QyUserEntity { CheckRole = AppSet.TwoCheckID, CheckHouseID = AppSet.HouseID.ToString() });
                                }
                            }
                        }
                        else if (AppSet.OneCheckID.Equals("7"))
                        {
                            //分公司财务
                            qyUser = q.QueryUserList(new QyUserEntity { CheckRole = AppSet.OneCheckID, HouseID = UserInfor.HouseID, CheckHouseID = AppSet.HouseID.ToString() });
                            if (qyUser.Count <= 0)
                            {
                                entity.NextCheckID = AppSet.TwoCheckID;//下一审批人
                                entity.NextCheckName = AppSet.TwoCheckName;
                                if (AppSet.TwoCheckID.Equals("3"))
                                {
                                    List<SystemUserEntity> Bosslist = b.QueryUserBossLoginName(new SystemUserEntity { LoginName = UserInfor.LoginName });
                                    if (Bosslist.Count > 0)
                                    {
                                        if (!string.IsNullOrEmpty(Bosslist[0].LoginName))
                                        {
                                            qyUser.Add(new QyUserEntity { UserID = Bosslist[0].LoginName, WxName = Bosslist[0].UserName });
                                        }
                                    }
                                }
                                else
                                {
                                    qyUser = q.QueryUserList(new QyUserEntity { CheckRole = AppSet.TwoCheckID, CheckHouseID = AppSet.HouseID.ToString() });
                                }
                            }
                        }
                        else
                        {
                            qyUser = q.QueryUserList(new QyUserEntity { CheckRole = AppSet.OneCheckID, CheckHouseID = AppSet.HouseID.ToString() });
                        }
                    }
                    #endregion
                    List<CargoApproveRelateEntity> relist = new List<CargoApproveRelateEntity>();
                    if (!string.IsNullOrEmpty(Relate))
                    {
                        ArrayList rel = (ArrayList)JSON.Decode(Relate);
                        foreach (Hashtable la in rel)
                        {
                            if (string.IsNullOrEmpty(Convert.ToString(la["InPiece"])))
                            {
                                //未修改的
                                relist.Add(new CargoApproveRelateEntity
                                {
                                    ApproveID = entity.ID,
                                    RelateID = Convert.ToInt64(la["RelateID"]),
                                    ThrowNum = Convert.ToInt32(la["Piece"]),
                                    ThrowCharge = Convert.ToDecimal(la["ActSalePrice"]),
                                    ApplyType = entity.ApplyType
                                });
                                totalNum += Convert.ToInt32(la["Piece"]);
                                totalCharge += Convert.ToInt32(la["Piece"]) * Convert.ToDecimal(la["ActSalePrice"]);
                            }
                            else
                            {
                                relist.Add(new CargoApproveRelateEntity
                                {
                                    ApproveID = entity.ID,
                                    RelateID = Convert.ToInt64(la["ID"]),
                                    ThrowNum = Convert.ToInt32(la["InPiece"]) - Convert.ToInt32(la["Piece"]),
                                    ThrowCharge = Convert.ToDecimal(la["ActSalePrice"]),
                                    ApplyType = entity.ApplyType
                                });
                                totalNum += Convert.ToInt32(la["InPiece"]) - Convert.ToInt32(la["Piece"]);
                                totalCharge += (Convert.ToInt32(la["InPiece"]) - Convert.ToInt32(la["Piece"])) * Convert.ToDecimal(la["ActSalePrice"]);
                            }
                        }
                    }
                    entity.RelateList = relist;
                }
            }
            #endregion

            #region 处理照片
            List<CargoApproveFileEntity> fil = new List<CargoApproveFileEntity>();
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFile imgFile = Request.Files[i];
                if (string.IsNullOrEmpty(imgFile.FileName)) { continue; }
                string imgname = string.Empty;
                Common.SaveComputerPic(imgFile, ref imgname, true);
                CargoApproveFileEntity f = new CargoApproveFileEntity();
                f.FileName = imgname;
                f.FilePath = "../upload/Approve/" + imgname;
                f.ApproveID = entity.ID;
                string[] fs = imgFile.FileName.Split('.');
                // 保存图片类型
                switch (fs[fs.Length - 1].ToUpper().Trim())
                {
                    case "WPS": f.FileType = "2"; f.FileExtension = "wps"; break;
                    case "TXT": f.FileType = "2"; f.FileExtension = "txt"; break;
                    case "DOC": f.FileType = "2"; f.FileExtension = "doc"; break;
                    case "DOCX": f.FileType = "2"; f.FileExtension = "docx"; break;
                    case "XLS": f.FileType = "2"; f.FileExtension = "xls"; break;
                    case "XLSX": f.FileType = "2"; f.FileExtension = "xlsx"; break;
                    case "PPT": f.FileType = "2"; f.FileExtension = "ppt"; break;
                    case "PPTX": f.FileType = "2"; f.FileExtension = "pptx"; break;
                    case "PDF": f.FileType = "2"; f.FileExtension = "pdf"; break;
                    case "JPG": f.FileType = "1"; f.FileExtension = "jpg"; break;
                    case "GIF": f.FileType = "1"; f.FileExtension = "gif"; break;
                    case "PNG": f.FileType = "1"; f.FileExtension = "png"; break;
                    case "BMP": f.FileType = "1"; f.FileExtension = "bmp"; break;
                    case "JPEG": f.FileType = "1"; f.FileExtension = "jpeg"; break;
                    case "ZIP": f.FileType = "2"; f.FileExtension = "zip"; break;
                    case "RAR": f.FileType = "2"; f.FileExtension = "rar"; break;
                    default: f.FileType = "2"; break;
                }
                fil.Add(f);
            }
            #endregion

            entity.FileList = fil;
            //新增申请
            statis.UpdateApplication(entity, log);
            msg.Result = true;
            msg.Message = "修改成功";
            try
            {
                //推送企业号通知 推送申请审批通知
                QySendInfoEntity send = new QySendInfoEntity();
                send.agentID = "1000010";//申请审批应用
                send.msgType = msgType.textcard;
                send.url = "http://dlt.neway5.com/QY/qyApplicationCheck.aspx?ApproveID=" + entity.ID.ToString();
                switch (entity.ApplyType)
                {
                    case "2":
                        send.title = "轮胎外调申请";
                        send.content = "<div></div><div>申请类型：轮胎外调</div><div>申请标题：" + entity.Title + "</div><div>外调仓库：" + entity.ThrowHouseName + " " + ClientName + "</div><div>外调数量：" + totalNum.ToString() + "条</div><div>外调金额：" + totalCharge.ToString("F2") + "元</div><div>申请内容：" + entity.Memo + "</div><div></div><div class=\"highlight\">请点击本通知进行审批！</div>";
                        break;
                    default:
                        break;
                }
                foreach (var it in qyUser)
                {
                    send.toUser = it.UserID;
                    if (!fin.IsExistApproveCheck(new CargoApproveCheckEntity { ApproveID = entity.ID, ApproveType = entity.ApplyType, CheckUserID = it.UserID, CheckType = "0" }))
                    {
                        fin.AddApproveCheck(new CargoApproveCheckEntity { ApproveID = entity.ID, CheckUserID = it.UserID, CheckName = it.WxName, ReadStatus = "1", CheckType = "0", ApproveType = entity.ApplyType }, log);
                    }
                    WxQYSendHelper.QiyePushInfo(send);
                    Common.WriteTextLog("推送成功：" + it.WxName + entity.ID.ToString());
                }

            }
            catch (ApplicationException ex)
            {
                msg.Message = "成功";
                msg.Result = true;
            }
            String dd = JSON.Encode(msg);
            Response.Clear();
            Response.Write(dd);
        }
        /// <summary>
        /// 查询申请关联数据
        /// </summary>
        public void QueryApproveRelateList()
        {
            CargoApproveRelateEntity queryEntity = new CargoApproveRelateEntity();
            queryEntity.ApproveID = Convert.ToInt64(Request["ApproveID"]);
            queryEntity.ApplyType = Convert.ToString(Request["ApplyType"]);

            CargoStaticBus bus = new CargoStaticBus();
            List<CargoApproveRelateEntity> list = new List<CargoApproveRelateEntity>();
            if (queryEntity.ApplyType.Equals("2")) { list = bus.QueryApproveContainerGoodRelateList(queryEntity); }
            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 根据申请ID查询该申请的审批流程图
        /// </summary>
        public void QueryApproveRout()
        {
            string exid = Request["id"];
            CargoExpenseApproveRoutEntity queryEntity = new CargoExpenseApproveRoutEntity();
            queryEntity.ExID = Convert.ToInt64(exid);
            queryEntity.ApproveType = Convert.ToString(Request["ApproveType"]);
            CargoFinanceBus bus = new CargoFinanceBus();
            List<CargoExpenseApproveRoutEntity> result = bus.QueryExpenseRout(queryEntity);

            string res = "";
            if (result.Count > 0)
            {
                res = "<table class='commTblStyle_8' border='0' width='100%' style='border-spacing:0;border-collapse:collapse;font-size:14px;'><tbody><tr><th>处理人</th><th>操作</th><th>处理时间</th><th>结果</th></tr>";
                foreach (var it in result)
                {
                    string op = "通过";
                    if (it.Opera.Equals("1")) { op = "拒审"; }
                    else if (it.Opera.Equals("2")) { op = "结束"; }
                    else if (it.Opera.Equals("3")) { op = "提交申请"; }
                    res += "<tr><td>" + it.UserName + "</td><td>" + op + "</td><td>" + it.OperaDate.ToString("yyyy-MM-dd HH:mm:ss") + "</td><td>" + it.Result + "</td></tr>";
                }
                res += "</tbody><table>";
            }

            String json = JSON.Encode(res);
            Response.Write(json);
        }
        /// <summary>
        /// 保存加班申请
        /// </summary>
        public void saveOverTime()
        {
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "我的工作台";
            log.NvgPage = "加班申请";
            log.UserID = UserInfor.LoginName.Trim();
            log.Status = "0"; log.Operate = "A";
            string formData = Request["formData"];
            List<QyUserEntity> qyUser = new List<QyUserEntity>();
            CargoApproveEntity entity = new CargoApproveEntity();
            CargoFinanceBus fin = new CargoFinanceBus();
            QiyeBus q = new QiyeBus();
            CargoOrderBus b = new CargoOrderBus();
            CargoStaticBus statis = new CargoStaticBus();
            #region 处理申请
            if (!string.IsNullOrEmpty(formData))
            {
                ArrayList FormRows = (ArrayList)JSON.Decode("[" + formData + "]");
                foreach (Hashtable row in FormRows)
                {
                    entity.ID = string.IsNullOrEmpty(Convert.ToString(row["ID"])) ? 0 : Convert.ToInt64(row["ID"]);
                    entity.Title = "加班申请";
                    entity.Memo = Convert.ToString(row["Memo"]).Trim();
                    entity.OStartTime = Convert.ToDateTime(row["OStartTime"]);
                    entity.OEndTime = Convert.ToDateTime(row["OEndTime"]);
                    entity.OTime = Convert.ToDecimal(row["OTime"]);
                    entity.ApplyID = UserInfor.LoginName;
                    entity.ApplyName = UserInfor.UserName;
                    entity.ApplyDate = DateTime.Now;
                    //entity.Memo = Convert.ToString(row["Memo"]);
                    entity.ApplyType = "4";//加班申请
                    List<CargoApproveSetEntity> aset = fin.QueryApproveSet(new CargoApproveSetEntity { ApproveType = entity.ApplyType, DelFlag = "0" });
                    entity.CurrID = UserInfor.LoginName;
                    entity.CurrName = UserInfor.UserName;
                    entity.CheckTime = DateTime.Now;
                    entity.ApplyStatus = "0";//待审
                    CargoApproveSetEntity AppSet = new CargoApproveSetEntity();
                    if (aset.Count > 0) { AppSet = aset[0]; }
                    entity.AppSetID = AppSet.ID;
                    //2.审批流程的每一级和当前人的审批角色相匹配
                    #region 审批流程
                    if (!string.IsNullOrEmpty(AppSet.OneCheckID))
                    {
                        entity.NextCheckID = AppSet.OneCheckID;//下一审批人
                        entity.NextCheckName = AppSet.OneCheckName;
                        //判断如果是分公司领导就查找该 分公司领导是谁
                        if (AppSet.OneCheckID.Equals("3") || AppSet.OneCheckID.Equals("8"))
                        {
                            List<SystemUserEntity> Bosslist = b.QueryUserBossLoginName(new SystemUserEntity { LoginName = UserInfor.LoginName });
                            if (Bosslist.Count > 0)
                            {
                                if (!string.IsNullOrEmpty(Bosslist[0].LoginName))
                                {
                                    qyUser.Add(new QyUserEntity { UserID = Bosslist[0].LoginName, WxName = Bosslist[0].UserName });
                                }
                                else
                                {
                                    entity.NextCheckID = AppSet.TwoCheckID;//下一审批人
                                    entity.NextCheckName = AppSet.TwoCheckName;
                                    qyUser = q.QueryUserList(new QyUserEntity { CheckRole = AppSet.TwoCheckID, CheckHouseID = UserInfor.HouseID.ToString() });
                                }
                            }
                        }
                        else if (AppSet.OneCheckID.Equals("7"))
                        {
                            //分公司财务
                            qyUser = q.QueryUserList(new QyUserEntity { CheckRole = AppSet.OneCheckID, HouseID = UserInfor.HouseID, CheckHouseID = UserInfor.HouseID.ToString() });
                            if (qyUser.Count <= 0)
                            {
                                entity.NextCheckID = AppSet.TwoCheckID;//下一审批人
                                entity.NextCheckName = AppSet.TwoCheckName;
                                if (AppSet.TwoCheckID.Equals("3"))
                                {
                                    List<SystemUserEntity> Bosslist = b.QueryUserBossLoginName(new SystemUserEntity { LoginName = UserInfor.LoginName });
                                    if (Bosslist.Count > 0)
                                    {
                                        if (!string.IsNullOrEmpty(Bosslist[0].LoginName))
                                        {
                                            qyUser.Add(new QyUserEntity { UserID = Bosslist[0].LoginName, WxName = Bosslist[0].UserName });
                                        }
                                    }
                                }
                                else
                                {
                                    qyUser = q.QueryUserList(new QyUserEntity { CheckRole = AppSet.TwoCheckID, CheckHouseID = UserInfor.HouseID.ToString() });
                                }
                            }
                        }
                        else
                        {
                            qyUser = q.QueryUserList(new QyUserEntity { CheckRole = AppSet.OneCheckID, CheckHouseID = UserInfor.HouseID.ToString() });
                        }
                    }
                    #endregion

                }
            }
            #endregion
            //新增申请
            long AID = statis.AddApplication(entity, log);
            msg.Result = true;
            msg.Message = "新增成功";
            entity.ID = AID;
            try
            {
                //推送企业号通知 推送申请审批通知
                QySendInfoEntity send = new QySendInfoEntity();
                send.agentID = "1000010";//申请审批应用
                send.msgType = msgType.textcard;
                send.url = "http://dlt.neway5.com/QY/qyApplicationCheck.aspx?ApproveID=" + entity.ID.ToString() + "&ty=0";
                switch (entity.ApplyType)
                {
                    case "4":
                        send.title = "加班申请";
                        send.content = "<div></div><div>申请人：" + entity.ApplyName + "</div><div>申请内容：" + entity.Memo + "</div><div>加班时间：" + entity.OStartTime.ToString("yyyy-MM-dd HH:mm") + "--" + entity.OEndTime.ToString("yyyy-MM-dd HH:mm") + "</div><div>加班时长：" + entity.OTime.ToString("F1") + "小时</div><div></div><div class=\"highlight\">请点击本通知进行审批！</div>";
                        break;
                    default:
                        break;
                }
                foreach (var it in qyUser)
                {
                    send.toUser = it.UserID;
                    if (!fin.IsExistApproveCheck(new CargoApproveCheckEntity { ApproveID = entity.ID, ApproveType = entity.ApplyType, CheckUserID = it.UserID, CheckType = "0" }))
                    {
                        fin.AddApproveCheck(new CargoApproveCheckEntity { ApproveID = entity.ID, CheckUserID = it.UserID, CheckName = it.WxName, ReadStatus = "1", CheckType = "0", ApproveType = entity.ApplyType }, log);
                    }
                    WxQYSendHelper.QiyePushInfo(send);
                    Common.WriteTextLog("推送成功：" + it.WxName + entity.ID.ToString());
                }

            }
            catch (ApplicationException ex)
            {
                msg.Message = "成功";
                msg.Result = true;
            }
            String dd = JSON.Encode(msg);
            Response.Clear();
            Response.Write(dd);
        }
        /// <summary>
        /// 优惠券发放申请
        /// </summary>
        public void SaveCouponApprove()
        {
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "我的工作台";
            log.NvgPage = "起草申请";
            log.UserID = UserInfor.LoginName.Trim();
            log.Status = "0"; log.Operate = "A";
            string formData = Request["formData"];
            List<QyUserEntity> qyUser = new List<QyUserEntity>();
            CargoApproveEntity entity = new CargoApproveEntity();
            CargoFinanceBus fin = new CargoFinanceBus();
            QiyeBus q = new QiyeBus();
            CargoOrderBus b = new CargoOrderBus();
            CargoHouseBus h = new CargoHouseBus();
            CargoStaticBus statis = new CargoStaticBus();
            decimal totalCharge = 0.00M;
            CargoHouseEntity houseEnt = new CargoHouseEntity();
            #region 处理申请
            if (!string.IsNullOrEmpty(formData))
            {
                ArrayList FormRows = (ArrayList)JSON.Decode("[" + formData + "]");
                foreach (Hashtable row in FormRows)
                {
                    entity.ID = string.IsNullOrEmpty(Convert.ToString(row["ID"])) ? 0 : Convert.ToInt64(row["ID"]);
                    entity.Title = Convert.ToString(row["Title"]).Trim();
                    entity.ApplyID = UserInfor.LoginName;
                    entity.ApplyName = UserInfor.UserName;
                    entity.ApplyDate = Convert.ToDateTime(row["ApproveDate"]);
                    entity.Memo = Convert.ToString(row["Memo"]);
                    entity.ApplyType = Convert.ToString(row["ApplyType"]);
                    entity.CurrID = UserInfor.LoginName;
                    entity.CurrName = UserInfor.UserName;
                    entity.CheckTime = DateTime.Now;
                    entity.ApplyStatus = "0";//待审
                    //entity.ThrowHouse = Convert.ToInt32(row["HouseID"]);
                    entity.LimitMoney = string.IsNullOrEmpty(Convert.ToString(row["LimitMoney"])) ? 0 : Convert.ToDecimal(row["LimitMoney"]);
                    houseEnt = h.QueryCargoHouseByID(Convert.ToInt32(row["HouseID"]));
                    entity.HouseID = houseEnt.HouseID;
                    entity.ClientID = Convert.ToInt64(row["ClientID"]);
                    entity.ClientName = Convert.ToString(row["ClientName"]);
                    List<CargoApproveSetEntity> aset = fin.QueryApproveSet(new CargoApproveSetEntity { ApproveType = Convert.ToString(row["ApplyType"]), DelFlag = "0", HouseID = houseEnt.HouseID });
                    CargoApproveSetEntity AppSet = new CargoApproveSetEntity();
                    if (aset.Count > 0) { AppSet = aset[0]; }
                    entity.AppSetID = AppSet.ID;
                    //2.审批流程的每一级和当前人的审批角色相匹配
                    #region 审批流程
                    if (!string.IsNullOrEmpty(AppSet.OneCheckID))
                    {
                        entity.NextCheckID = AppSet.OneCheckID;//下一审批人
                        entity.NextCheckName = AppSet.OneCheckName;
                        //判断如果是分公司领导就查找该 分公司领导是谁
                        if (AppSet.OneCheckID.Equals("3"))
                        {
                            List<SystemUserEntity> Bosslist = b.QueryUserBossLoginName(new SystemUserEntity { LoginName = UserInfor.LoginName });
                            if (Bosslist.Count > 0)
                            {
                                if (!string.IsNullOrEmpty(Bosslist[0].LoginName))
                                {
                                    qyUser.Add(new QyUserEntity { UserID = Bosslist[0].LoginName, WxName = Bosslist[0].UserName });
                                }
                                else
                                {
                                    entity.NextCheckID = AppSet.TwoCheckID;//下一审批人
                                    entity.NextCheckName = AppSet.TwoCheckName;
                                    qyUser = q.QueryUserList(new QyUserEntity { CheckRole = AppSet.TwoCheckID, CheckHouseID = UserInfor.HouseID.ToString() });
                                }
                            }
                        }
                        else if (AppSet.OneCheckID.Equals("7"))
                        {
                            //分公司财务
                            qyUser = q.QueryUserList(new QyUserEntity { CheckRole = AppSet.OneCheckID, HouseID = UserInfor.HouseID, CheckHouseID = UserInfor.HouseID.ToString() });
                            if (qyUser.Count <= 0)
                            {
                                entity.NextCheckID = AppSet.TwoCheckID;//下一审批人
                                entity.NextCheckName = AppSet.TwoCheckName;
                                if (AppSet.TwoCheckID.Equals("3"))
                                {
                                    List<SystemUserEntity> Bosslist = b.QueryUserBossLoginName(new SystemUserEntity { LoginName = UserInfor.LoginName });
                                    if (Bosslist.Count > 0)
                                    {
                                        if (!string.IsNullOrEmpty(Bosslist[0].LoginName))
                                        {
                                            qyUser.Add(new QyUserEntity { UserID = Bosslist[0].LoginName, WxName = Bosslist[0].UserName });
                                        }
                                    }
                                }
                                else
                                {
                                    qyUser = q.QueryUserList(new QyUserEntity { CheckRole = AppSet.TwoCheckID, CheckHouseID = UserInfor.HouseID.ToString() });
                                }
                            }
                        }
                        else
                        {
                            qyUser = q.QueryUserList(new QyUserEntity { CheckRole = AppSet.OneCheckID, CheckHouseID = UserInfor.HouseID.ToString() });
                        }
                    }
                    #endregion

                }
            }
            #endregion
            //新增申请
            long AID = statis.AddApplication(entity, log);
            msg.Result = true;
            msg.Message = "新增成功";
            entity.ID = AID;
            try
            {
                //推送企业号通知 推送申请审批通知
                QySendInfoEntity send = new QySendInfoEntity();
                send.agentID = "1000010";//申请审批应用
                send.msgType = msgType.textcard;
                send.url = "http://dlt.neway5.com/QY/qyApplicationCheck.aspx?ApproveID=" + entity.ID.ToString();
                switch (entity.ApplyType)
                {
                    case "15":
                        send.title = "优惠券发放申请";
                        send.content = "<div></div><div>申请类型：优惠券发放</div><div>申请标题：" + entity.Title + "</div><div>所属仓库：" + houseEnt.Name + " " + entity.ClientName + "</div><div>申请金额：" + totalCharge.ToString("F2") + "元</div><div>申请内容：" + entity.Memo + "</div><div></div><div class=\"highlight\">请点击本通知进行审批！</div>";
                        break;
                    default:
                        break;
                }
                foreach (var it in qyUser)
                {
                    send.toUser = it.UserID;
                    if (!fin.IsExistApproveCheck(new CargoApproveCheckEntity { ApproveID = entity.ID, ApproveType = entity.ApplyType, CheckUserID = it.UserID, CheckType = "0" }))
                    {
                        fin.AddApproveCheck(new CargoApproveCheckEntity { ApproveID = entity.ID, CheckUserID = it.UserID, CheckName = it.WxName, ReadStatus = "1", CheckType = "0", ApproveType = entity.ApplyType }, log);
                    }
                    WxQYSendHelper.QiyePushInfo(send);
                    Common.WriteTextLog("推送成功：" + it.WxName + entity.ID.ToString());
                }

            }
            catch (ApplicationException ex)
            {
                msg.Message = "成功";
                msg.Result = true;
            }
            String dd = JSON.Encode(msg);
            Response.Clear();
            Response.Write(dd);
        }
        #endregion
        #region 城市界面操作方法集合
        /// <summary>
        /// 删除城市
        /// </summary>
        public void DelCity()
        {
            String json = Request["data"];
            if (String.IsNullOrEmpty(json)) return;
            List<CargoCityManagerEntity> list = new List<CargoCityManagerEntity>();
            ArrayList rows = (ArrayList)JSON.Decode(json);
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "静态数据管理";
            log.NvgPage = "城市管理";
            log.UserID = UserInfor.LoginName.Trim();
            log.Status = "0";
            log.Operate = "D";
            foreach (Hashtable row in rows)
            {
                list.Add(new CargoCityManagerEntity
                {
                    CID = Convert.ToInt32(row["CID"]),
                    CityName = Convert.ToString(row["CityName"]),
                    DelFlag = Convert.ToString(row["DelFlag"])
                });
            }
            CargoStaticBus bus = new CargoStaticBus();
            try
            {
                bus.DelCity(list, log);
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
        /// <summary>
        /// 新增和修改城市
        /// </summary>
        public void SaveCity()
        {
            CargoCityManagerEntity ent = new CargoCityManagerEntity();
            CargoStaticBus bus = new CargoStaticBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "静态数据管理";
            log.NvgPage = "城市管理";
            log.Status = "0";

            log.UserID = UserInfor.LoginName.Trim();
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            String id = Request["CID"] != null ? Request["CID"].ToString() : "";
            try
            {
                ent.CityName = Convert.ToString(Request["CityName"]);
                ent.DelFlag = Convert.ToString(Request["DelFlag"]);
                if (ent.DelFlag.Equals("1") || id == "")
                {
                    if (bus.IsExistCity(new CargoCityManagerEntity { CityName = ent.CityName }))
                    {
                        msg.Message = "已经存在相同的城市名称";
                    }
                }

                if (string.IsNullOrEmpty(msg.Message))
                {
                    if (id == "")//新增：id为空，或_state为added
                    {
                        log.Operate = "A";
                        bus.AddCity(ent, log);
                    }
                    else
                    {
                        ent.CID = Convert.ToInt32(id);
                        log.Operate = "U";
                        bus.UpdateCity(ent, log);
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
        /// 城市查询
        /// </summary>
        public void QueryCity()
        {
            CargoCityManagerEntity queryEntity = new CargoCityManagerEntity();
            queryEntity.CityName = Request["CityName"];
            if (Request["dFlag"] != "-1")
            {
                queryEntity.DelFlag = Convert.ToString(Request["dFlag"]);
            }
            //分页
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]);
            CargoStaticBus bus = new CargoStaticBus();
            Hashtable list = bus.QueryCity(pageIndex, pageSize, queryEntity);

            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 查询所有的城市数据
        /// </summary>
        public void QueryAllCity()
        {
            CargoCityManagerEntity entity = new CargoCityManagerEntity();
            //entity.CityName = Request["City"];
            CargoStaticBus bus = new CargoStaticBus();
            List<CargoCityManagerEntity> list = bus.QueryAllCity(entity);
            //String json = JSON.Encode(list);
            //Response.Clear();
            //Response.Write(json);
            string json = "[";
            foreach (var it in list)
            {
                json += "{\"CityName\":\"" + it.CityName.Trim() + "\",\"CityName\":\"" + it.CityName.Trim() + "\"},";

            }
            json = json.Substring(0, json.Length - 1);
            json += "]";
            String jsons = JSON.Encode(json);
            Response.Clear();
            Response.Write(jsons);
        }
        /// <summary>
        /// 查询自己所拥有的城市
        /// </summary>
        public void QueryOnlyCity()
        {
            string json = "[";
            string[] cc = null;// UserInfor.CityCode.Split(',');
            for (int i = 0; i < cc.Length; i++)
            {
                json += "{\"CityName\":\"" + cc[i].Trim() + "\",\"CityName\":\"" + cc[i].Trim() + "\"},";
            }
            json = json.Substring(0, json.Length - 1);
            json += "]";
            String jsons = JSON.Encode(json);
            Response.Clear();
            Response.Write(jsons);
        }
        #endregion
        #region 物流公司操作方法集合
        /// <summary>
        /// 删除物流公司
        /// </summary>
        public void DelLogistic()
        {
            String json = Request["data"];
            if (String.IsNullOrEmpty(json)) return;
            List<CargoLogisticEntity> list = new List<CargoLogisticEntity>();
            ArrayList rows = (ArrayList)JSON.Decode(json);
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "静态数据管理";
            log.NvgPage = "物流公司管理";
            log.UserID = UserInfor.LoginName.Trim();
            log.Status = "0";
            log.Operate = "D";
            foreach (Hashtable row in rows)
            {
                list.Add(new CargoLogisticEntity
                {
                    ID = Convert.ToInt32(row["ID"]),
                    LogisticName = Convert.ToString(row["LogisticName"]),
                    DelFlag = Convert.ToString(row["DelFlag"])
                });
            }
            CargoStaticBus bus = new CargoStaticBus();
            try
            {
                bus.DelLogistic(list, log);
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
        /// <summary>
        /// 新增和修改物流公司
        /// </summary>
        public void SaveLogistic()
        {
            CargoLogisticEntity ent = new CargoLogisticEntity();
            CargoStaticBus bus = new CargoStaticBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "静态数据管理";
            log.NvgPage = "物流公司管理";
            log.Status = "0";

            log.UserID = UserInfor.LoginName.Trim();
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            String id = Request["ID"] != null ? Request["ID"].ToString() : "";
            try
            {
                ent.LogisticName = Convert.ToString(Request["LogisticName"]);
                ent.DelFlag = Convert.ToString(Request["DelFlag"]);
                if (ent.DelFlag.Equals("1") || id == "")
                {
                    if (bus.IsExistLogistic(new CargoLogisticEntity { LogisticName = ent.LogisticName }))
                    {
                        msg.Message = "已经存在相同的物流公司名称";
                    }
                }

                if (string.IsNullOrEmpty(msg.Message))
                {
                    if (id == "")//新增：id为空，或_state为added
                    {
                        log.Operate = "A";
                        bus.AddLogistic(ent, log);
                    }
                    else
                    {
                        ent.ID = Convert.ToInt32(id);
                        log.Operate = "U";
                        bus.UpdateLogistic(ent, log);
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
        /// 物流公司查询
        /// </summary>
        public void QueryLogistic()
        {
            CargoLogisticEntity queryEntity = new CargoLogisticEntity();
            queryEntity.LogisticName = Request["LogisticName"];
            if (Request["dFlag"] != "-1")
            {
                queryEntity.DelFlag = Convert.ToString(Request["dFlag"]);
            }
            //分页
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]);
            CargoStaticBus bus = new CargoStaticBus();
            Hashtable list = bus.QueryLogistic(pageIndex, pageSize, queryEntity);

            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 查询所有的物流公司数据
        /// </summary> 
        public void QueryAllLogistic()
        {
            CargoLogisticEntity entity = new CargoLogisticEntity();
            //entity.LogisticName = Request["City"];
            CargoStaticBus bus = new CargoStaticBus();
            List<CargoLogisticEntity> list = bus.QueryAllLogistic(entity);
            //String json = JSON.Encode(list);
            //Response.Clear();
            //Response.Write(json);
            string json = "[";
            foreach (var it in list)
            {
                json += "{\"ID\":\"" + it.ID.ToString() + "\",\"LogisticName\":\"" + it.LogisticName.Trim() + "\"},";

            }
            json = json.Substring(0, json.Length - 1);
            json += "]";
            String jsons = JSON.Encode(json);
            Response.Clear();
            Response.Write(jsons);
        }
        #endregion
        #region 修改密码
        /// <summary>
        /// 修改密码
        /// </summary>
        public void UpdateUserPwd()
        {
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            try
            {
                if (msg.Result)
                {
                    if (string.IsNullOrEmpty(Convert.ToString(Request["LoginName"])))
                    {
                        msg.Message = "登陆名不能为空";
                        msg.Result = false;
                    }
                }
                if (msg.Result)
                {
                    if (string.IsNullOrEmpty(Convert.ToString(Request["Password"])))
                    {
                        msg.Message = "密码不能为空";
                        msg.Result = false;
                    }
                }
                if (msg.Result)
                {
                    if (string.IsNullOrEmpty(Convert.ToString(Request["NewPwd"])))
                    {
                        msg.Message = "确认密码不能为空";
                        msg.Result = false;
                    }
                }
                if (msg.Result)
                {
                    if (Convert.ToString(Request["Password"]).Length < 8)
                    {
                        msg.Message = "密码长度不足8位";
                        msg.Result = false;
                    }
                }
                if (msg.Result)
                {
                    if (Convert.ToString(Request["NewPwd"]).Length < 8)
                    {
                        msg.Message = "确认密码长度不足8位";
                        msg.Result = false;
                    }
                }
                if (msg.Result)
                {
                    if (!Convert.ToString(Request["Password"]).Trim().ToUpper().Equals(Convert.ToString(Request["NewPwd"]).Trim().ToUpper()))
                    {
                        msg.Message = "两次输入的密码必须一致";
                        msg.Result = false;
                    }
                }
                if (msg.Result)
                {

                    string cqd = string.Format("cmd={0}&loginName={1}&loginPwd={2}", "UpdatePassword", Convert.ToString(Request["LoginName"]), Convert.ToString(Request["NewPwd"]).Trim());
                    string result = wxHttpUtility.SendHttpRequest(Common.GethouseAPI(), cqd);
                    userAPIMessage rows = Newtonsoft.Json.JsonConvert.DeserializeObject<userAPIMessage>(result);
                    if (rows.Result)
                    {
                        msg.Result = true;
                        msg.Message = "修改成功";
                    }
                    else
                    {
                        msg.Result = false;
                        msg.Message = rows.Message;
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
        /// <summary>
        /// 切换仓库
        /// </summary>
        public void ChangeHouse()
        {
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            string hid = Convert.ToString(Request["hid"]);
            string hname = Convert.ToString(Request["hname"]);
            string cqd = string.Format("cmd={0}&loginName={1}&houseID={2}&houseName={3}", "ChangeHouse", UserInfor.LoginName, hid, hname);
            string result = wxHttpUtility.SendHttpRequest(Common.GethouseAPI(), cqd);
            userAPIMessage rows = Newtonsoft.Json.JsonConvert.DeserializeObject<userAPIMessage>(result);
            if (rows.Result)
            {
                SystemUserEntity us = (SystemUserEntity)Session["user"];
                us.HouseID = Convert.ToInt32(hid);
                us.HouseName = hname;
                if (!string.IsNullOrEmpty(hid))
                {
                    CargoHouseBus house = new CargoHouseBus();
                    CargoHouseEntity houseEnt = house.QueryCargoHouseByID(Convert.ToInt32(hid));
                    us.DepCity = houseEnt.DepCity;
                    us.PickTitle = houseEnt.PickTitle;
                    us.SendTitle = houseEnt.SendTitle;
                }
                msg.Result = true;
                msg.Message = "切换成功";
            }
            else
            {
                msg.Result = false;
                msg.Message = rows.Message;
            }
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
        }
        #endregion
        #region 会计科目管理
        public void QueryFinance()
        {
            FinanceSubjectEntity queryEntity = new FinanceSubjectEntity();
            if (!string.IsNullOrEmpty(Request["ZID"]))
            {
                queryEntity.ZID = Convert.ToInt32(Request["ZID"]);
            }
            else
            {
                queryEntity.ZID = -1;
            }
            if (!string.IsNullOrEmpty(Request["FID"]))
            {
                queryEntity.FID = Convert.ToInt32(Request["FID"]);
            }
            else
            {
                queryEntity.FID = -1;
            }
            if (!string.IsNullOrEmpty(Request["Name"]))
            {
                queryEntity.ZName = Convert.ToString(Request["Name"]);
            }
            //分页
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]);
            CargoStaticBus bus = new CargoStaticBus();
            Hashtable list = bus.QueryFinance(pageIndex, pageSize, queryEntity);

            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }

        public void QueryAllZeroSubject()
        {
            CargoStaticBus bus = new CargoStaticBus();

            List<FinanceSubjectEntity> list = new List<FinanceSubjectEntity>();

            list = bus.QueryAllZeroSubject(-1);

            //string type = Request.QueryString["type"];

            //if (type == null)
            //{
            //    list.Insert(0, new FinanceSubjectEntity
            //    {
            //        ZID = -1,
            //        ZName = "全部"
            //    });
            //}

            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }
        public void QueryAllFirstSubject()
        {
            CargoStaticBus bus = new CargoStaticBus();
            List<FinanceSubjectEntity> list = new List<FinanceSubjectEntity>();
            int ZID = Convert.ToInt32(Request["ZID"]);
            list = bus.QueryAllFirstSubject(ZID);

            //string type = Request.QueryString["type"];

            //if (type == null)
            //{
            //    list.Insert(0, new FinanceSubjectEntity
            //    {
            //        FID = -1,
            //        FName = "全部"
            //    });
            //}

            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }
        public void QueryAllSecondSubject()
        {
            CargoStaticBus bus = new CargoStaticBus();
            List<FinanceSubjectEntity> list = new List<FinanceSubjectEntity>();
            int FID = Convert.ToInt32(Request["FID"]);
            list = bus.QueryAllSecondSubject(FID);

            string type = Request.QueryString["type"];

            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }

        public void SaveFinanceData()
        {
            FinanceSubjectEntity ent = new FinanceSubjectEntity();
            CargoStaticBus bus = new CargoStaticBus();
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "静态数据管理";
            log.NvgPage = "会计科目";
            log.Status = "0";
            log.UserID = UserInfor.LoginName.Trim();
            string editZID = Request["editZID"];
            try
            {
                ent.ZID = Convert.ToInt32(Request["ZID"]);
                ent.ZName = Convert.ToString(Request["ZName"]).Trim();
                if (string.IsNullOrEmpty(Request["FID"]))
                {
                    if (!string.IsNullOrEmpty(editZID))
                    {
                        ent.FID = Convert.ToInt32(Request["OldFID"]);
                    }
                    else
                    {
                        ent.FID = -1;
                    }
                }
                else
                {
                    ent.FID = Convert.ToInt32(Request["FID"]);
                }
                ent.FName = Convert.ToString(Request["FName"]).Trim();
                if (string.IsNullOrEmpty(Request["SID"]))
                {
                    if (!string.IsNullOrEmpty(editZID))
                    {
                        ent.SID = Convert.ToInt32(Request["OldSID"]);
                    }
                    else
                    {
                        ent.SID = -1;
                    }
                }
                else
                {
                    ent.SID = Convert.ToInt32(Request["SID"]);
                }
                ent.SName = Convert.ToString(Request["SName"]).Trim();


                if (!string.IsNullOrEmpty(editZID))
                {
                    ent.OldZID = Convert.ToInt32(Request["OldZID"]);
                    ent.OldFID = Convert.ToInt32(Request["OldFID"]);
                    ent.OldSID = Convert.ToInt32(Request["OldSID"]);
                    ent.OldFName = Convert.ToString(Request["OldFName"]).Trim();
                    ent.OldSName = Convert.ToString(Request["OldSName"]).Trim();

                    if (ent.OldFName != ent.FName)
                    {
                        if (bus.IsExistFinance(Convert.ToString(Request["FName"]).Trim(), "FirstSubject"))
                        {
                            msg.Message = "已经存在相同的二级科目名称";
                            Response.Write(JSON.Encode(msg));
                            return;
                        }
                    }
                    if (ent.OldSName != ent.SName)
                    {
                        if (bus.IsExistFinance(Convert.ToString(Request["SName"]).Trim(), "SecondSubject"))
                        {
                            msg.Message = "已经存在相同的三级科目名称";
                            Response.Write(JSON.Encode(msg));
                            return;
                        }
                    }

                    log.Operate = "U";
                    bus.UpdateFinanceData(ent, log);
                    msg.Result = true;
                    msg.Message = "成功";
                }
                else
                {
                    if (string.IsNullOrEmpty(Request["FID"]))
                    {
                        if (bus.IsExistFinance(Convert.ToString(Request["FName"]).Trim(), "FirstSubject"))
                        {
                            msg.Message = "已经存在相同的二级科目名称";
                            Response.Write(JSON.Encode(msg));
                            return;
                        }
                    }
                    if (ent.OldSName != ent.SName)
                    {
                        if (bus.IsExistFinance(Convert.ToString(Request["SName"]).Trim(), "SecondSubject"))
                        {
                            msg.Message = "已经存在相同的三级科目名称";
                            Response.Write(JSON.Encode(msg));
                            return;
                        }
                    }
                    log.Operate = "A";
                    bus.InsertFinanceData(ent, log);
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
        /// 删除数据
        /// </summary>
        public void DeleteFinanceData()
        {
            String json = Request["data"];
            if (String.IsNullOrEmpty(json)) return;
            List<FinanceSubjectEntity> list = new List<FinanceSubjectEntity>();
            CargoStaticBus bus = new CargoStaticBus();
            ArrayList rows = (ArrayList)JSON.Decode(json);
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "静态数据管理";
            log.NvgPage = "会计科目";
            log.Status = "0";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "D";
            try
            {
                foreach (Hashtable row in rows)
                {
                    list.Add(new FinanceSubjectEntity
                    {
                        ZID = Convert.ToInt32(row["ZID"]),
                        ZName = Convert.ToString(row["ZName"]),
                        FID = Convert.ToInt32(row["FID"]),
                        FName = Convert.ToString(row["FName"]),
                        SID = Convert.ToInt32(row["SID"]),
                        SName = Convert.ToString(row["SName"])
                    });
                }
                bus.DeleteFinanceData(list, log);
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
        #region 公告管理
        /// <summary>
        /// 删除公告
        /// </summary>
        public void DelCargoNotice()
        {
            String json = Request["data"];
            if (String.IsNullOrEmpty(json)) return;
            List<CargoNoticeEntity> list = new List<CargoNoticeEntity>();
            ArrayList rows = (ArrayList)JSON.Decode(json);
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "静态数据管理";
            log.NvgPage = "公告管理";
            log.UserID = UserInfor.LoginName.Trim();
            log.Status = "0";
            log.Operate = "D";
            foreach (Hashtable row in rows)
            {
                list.Add(new CargoNoticeEntity
                {
                    ID = Convert.ToInt32(row["ID"]),
                    Title = Convert.ToString(row["Title"]),
                    NoticeType = Convert.ToString(row["NoticeType"]),
                    HouseName = Convert.ToString(row["HouseName"]),
                    DelFlag = Convert.ToString(row["DelFlag"])
                });
            }
            CargoStaticBus bus = new CargoStaticBus();
            try
            {
                bus.DelCargoNotice(list, log);
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
        /// <summary>
        /// 新增或修改公告
        /// </summary>
        public void SaveCargoNotice()
        {
            CargoNoticeEntity ent = new CargoNoticeEntity();
            CargoStaticBus bus = new CargoStaticBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "静态数据管理";
            log.NvgPage = "公告管理";
            log.Status = "0";
            log.UserID = UserInfor.LoginName.Trim();
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            String id = Request["ID"] != null ? Request["ID"].ToString() : "";
            try
            {
                ent.Title = Convert.ToString(Request["Title"]);
                ent.NoticeType = Convert.ToString(Request["NoticeType"]);
                //ent.Memo = Convert.ToString(Request["Memo"]);
                ent.Memo = Server.UrlDecode(Convert.ToString(Request["editor"]));
                ent.OPName = UserInfor.UserName;
                ent.HouseID = Convert.ToInt32(Request["HouseID"]);
                ent.DelFlag = Convert.ToString(Request["DelFlag"]);

                if (string.IsNullOrEmpty(msg.Message))
                {
                    if (id == "")//新增：id为空，或_state为added
                    {
                        log.Operate = "A";
                        bus.AddNotice(ent, log);
                    }
                    else
                    {
                        ent.ID = Convert.ToInt32(id);
                        log.Operate = "U";
                        bus.UpdateNotice(ent, log);
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
        /// 查询公告
        /// </summary>
        public void QueryCargoNotice()
        {
            CargoNoticeEntity queryEntity = new CargoNoticeEntity();
            queryEntity.Title = Request["Title"];
            if (Request["DelFlag"] != "-1")
            {
                queryEntity.DelFlag = Convert.ToString(Request["DelFlag"]);
            }
            if (Request["NoticeType"] != "-1")
            {
                queryEntity.NoticeType = Convert.ToString(Request["NoticeType"]);
            }
            if (!string.IsNullOrEmpty(Request["HID"]))
            {
                queryEntity.HouseID = Convert.ToInt32(Request["HID"]);
            }

            //分页
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]);
            CargoStaticBus bus = new CargoStaticBus();
            Hashtable list = bus.QueryCargoNotice(pageIndex, pageSize, queryEntity);

            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 查询公告
        /// </summary>
        public void QueryNoticeByID()
        {
            CargoNoticeEntity queryEntity = new CargoNoticeEntity();
            //查询条件
            string key = Request["ID"];
            if (string.IsNullOrEmpty(key)) { Response.Clear(); Response.Write(JSON.Encode(queryEntity)); return; }
            queryEntity.ID = Convert.ToInt32(key);
            CargoStaticBus bus = new CargoStaticBus();
            CargoNoticeEntity list = bus.QueryNoticeByID(queryEntity);
            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }
        #endregion
        #region 消息管理
        /// <summary>
        /// 查询公告消息列表
        /// </summary>
        public void QueryCargoNoticeUser()
        {
            CargoNoticeEntity queryEntity = new CargoNoticeEntity();
            queryEntity.Title = Request["Title"];
            if (Request["DelFlag"] != "-1")
            {
                queryEntity.DelFlag = Convert.ToString(Request["DelFlag"]);
            }
            if (Request["NoticeType"] != "-1")
            {
                queryEntity.NoticeType = Convert.ToString(Request["NoticeType"]);
            }
            if (!string.IsNullOrEmpty(Request["HouseID"]))
            {
                queryEntity.HouseID = Convert.ToInt32(Request["HouseID"]);
            }
            if (!string.IsNullOrEmpty(Request["ReadStatus"]))
            {
                queryEntity.ReadStatus = Convert.ToInt32(Request["ReadStatus"]);
            }
            //分页
            int pageIndex = Convert.ToInt32(Request["page"]) == 0 ? 1 : Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]) == 0 ? 20 : Convert.ToInt32(Request["rows"]);
            CargoStaticBus bus = new CargoStaticBus();

            //queryEntity.LoginName = UserInfor.LoginName;
            Hashtable list = bus.QueryCargoNoticeUser(pageIndex, pageSize, queryEntity);

            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }

        /// <summary>
        /// 查询是否有未读信息
        /// </summary>
        /// </summary>
        /// <returns></returns>
        public void QueryUnreadMessage()
        {
            ErrMessage msg = new ErrMessage();
            msg.Result = false;
            msg.Message = "false";

            CargoStaticBus bus = new CargoStaticBus();
            List<CargoNoticeEntity> entNoticeList = bus.QueryUnreadMessage(new CargoNoticeEntity() { LoginName = UserInfor.LoginName.Trim() });
            if (entNoticeList.Count > 0)
            {
                msg.Result = true;
                msg.Message = "true";
            }

            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
        }
        /// <summary>
        /// 一键已读
        /// </summary>
        public void OneClickNoticeUserRead()
        {
            //CargoNoticeUserViewEntity ent = new CargoNoticeUserViewEntity();
            CargoStaticBus bus = new CargoStaticBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "静态数据管理";
            log.NvgPage = "消息管理";
            log.Status = "0";
            log.UserID = UserInfor.LoginName.Trim();
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            String id = Request["ID"] != null ? Request["ID"].ToString() : "";
            try
            {
                //查询用户所有未读消息
                List<CargoNoticeEntity> entNoticeList = bus.QueryUnreadMessage(new CargoNoticeEntity() { LoginName = UserInfor.LoginName.Trim() });
                //赋值
                List<CargoNoticeUserViewEntity> entList = new List<CargoNoticeUserViewEntity>();
                foreach (CargoNoticeEntity item in entNoticeList)
                {
                    CargoNoticeUserViewEntity data = new CargoNoticeUserViewEntity();
                    if (!string.IsNullOrEmpty(item.NoticeUserID)) data.ID = Convert.ToInt32(item.NoticeUserID);
                    data.MessageID = item.ID;
                    data.Title = item.Title;
                    data.MessageType = 0;
                    data.ReadStatus = 1;
                    data.LoginName = UserInfor.LoginName.Trim();
                    data.UserName = UserInfor.UserName.Trim();
                    entList.Add(data);
                }
                //新增用户查看消息数据
                foreach (var item in entList)
                {

                    if (string.IsNullOrEmpty(item.ID.ToString()))//新增：id为空，或_state为added
                    {
                        log.Operate = "A";
                        bus.AddNoticeUserView(item, log);
                    }
                    else
                    {
                        log.Operate = "U";
                        bus.UpdateNoticeUserView(item, log);
                    }
                }
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
        /// <summary>
        /// 修改消息数据
        /// </summary>
        public void SaveCargoNoticeUserView()
        {
            CargoNoticeUserViewEntity ent = new CargoNoticeUserViewEntity();
            CargoStaticBus bus = new CargoStaticBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "静态数据管理";
            log.NvgPage = "消息管理";
            log.Status = "0";
            log.UserID = UserInfor.LoginName.Trim();
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            String id = Request["ID"] != null ? Request["ID"].ToString() : "";
            try
            {

                int update = Convert.ToInt32(Request["update"]);
                //修改该公告所有的已读改为未读
                if (update == 1 && !string.IsNullOrEmpty(id))
                {

                    bus.UpdateNoticeUserView(new CargoNoticeUserViewEntity() { MessageID = Convert.ToInt32(Request["ID"]), Title = Convert.ToString(Request["Title"]), ReadStatus = 0 }, log);
                }

                if (string.IsNullOrEmpty(msg.Message))
                {
                    //赋值基础数据
                    ent.MessageID = !string.IsNullOrEmpty(Request["ID"]) ? Convert.ToInt32(Request["ID"]) : -1;
                    ent.Title = Convert.ToString(Request["Title"]);
                    ent.ReadStatus = (byte)Convert.ToInt32(Request["ReadStatus"]);
                    ent.MessageType = Convert.ToInt32(Request["MessageType"]);
                    ent.LoginName = UserInfor.LoginName.Trim();
                    ent.UserName = UserInfor.UserName.Trim();
                    //新增公告时查询最近一条的公告ID
                    if (ent.MessageID == -1)
                    {
                        CargoNoticeEntity dataNotice = bus.QueryUnreadMessage(new CargoNoticeEntity() { LoginName = UserInfor.LoginName.Trim() }).FirstOrDefault();
                        ent.MessageID = dataNotice.ID;
                    }
                    //查询用户
                    List<CargoNoticeUserViewEntity> data = bus.QueryNoticeUserView(new CargoNoticeUserViewEntity() { MessageID = ent.MessageID, LoginName = UserInfor.LoginName.Trim() });
                    if (data.Count() == 0)//新增：id为空，或_state为added
                    {
                        log.Operate = "A";
                        bus.AddNoticeUserView(ent, log);
                    }
                    else
                    {
                        log.Operate = "U";
                        bus.UpdateNoticeUserView(ent, log);
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
        #region 我的日报方法集合
        /// <summary>
        /// 查询公告消息列表
        /// </summary>
        public void QueryCargoDailyReports()
        {
            CargoDailyReportsEntity queryEntity = new CargoDailyReportsEntity();
            queryEntity.Title = Request["Title"];
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }

            //分页
            int pageIndex = Convert.ToInt32(Request["page"]) == 0 ? 1 : Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]) == 0 ? 20 : Convert.ToInt32(Request["rows"]);
            CargoStaticBus bus = new CargoStaticBus();

            queryEntity.LoginName = UserInfor.LoginName;
            Hashtable list = bus.QueryCargoDailyReports(pageIndex, pageSize, queryEntity);

            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 查询可批复日报
        /// </summary>
        public void QueryCanDailyReports()
        {
            CargoDailyReportsEntity queryEntity = new CargoDailyReportsEntity();
            CargoStaticBus bus = new CargoStaticBus();

            queryEntity.LoginName = UserInfor.LoginName;

            queryEntity.IsReply = Convert.ToInt32(Request["IsReply"]);
            List<CargoDailyReportsEntity> list = bus.QueryCanDailyReports(queryEntity);

            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 查询看批复人员
        /// </summary>
        public void QueryUserReportComments()
        {
            CargoUserReportCommentsEntity queryEntity = new CargoUserReportCommentsEntity();
            CargoStaticBus bus = new CargoStaticBus();

            List<CargoUserReportCommentsEntity> list = bus.QueryUserReportComments(queryEntity);
            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }

        /// <summary>
        /// 查询日报
        /// </summary>
        public void QueryDailyReportsByID()
        {
            CargoDailyReportsEntity queryEntity = new CargoDailyReportsEntity();
            //查询条件
            string key = Request["ID"];
            if (string.IsNullOrEmpty(key)) { Response.Clear(); Response.Write(JSON.Encode(queryEntity)); return; }
            queryEntity.ID = Convert.ToInt32(key);
            CargoStaticBus bus = new CargoStaticBus();
            CargoDailyReportsEntity list = bus.QueryDailyReportsByID(queryEntity).FirstOrDefault();
            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }

        /// <summary>
        /// 新增/修改我的日报
        /// </summary>
        public void SaveCargoDailyReports()
        {
            CargoDailyReportsEntity ent = new CargoDailyReportsEntity();
            CargoStaticBus bus = new CargoStaticBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "静态数据管理";
            log.NvgPage = "我的日报";
            log.Status = "0";
            log.UserID = UserInfor.LoginName.Trim();
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            String id = Request["ID"] != null ? Request["ID"].ToString() : "";
            try
            {
                //赋值基础数据
                ent.Title = Convert.ToString(Request["Title"]);
                ent.Content = Server.UrlDecode(Convert.ToString(Request["editor"]));
                ent.LoginName = UserInfor.LoginName.Trim();
                ent.UserName = UserInfor.UserName.Trim();
                ent.LoginReportName = Convert.ToString(Request["LoginReportName"]);
                ent.UserReportName = Convert.ToString(Request["UserReportName"]);

                if (string.IsNullOrEmpty(msg.Message))
                {
                    if (id == "")//新增：id为空，或_state为added
                    {
                        log.Operate = "A";
                        bus.AddDailyReports(ent, log);
                    }
                    else
                    {
                        ent.ID = Convert.ToInt32(id);
                        log.Operate = "U";
                        bus.UpdateDailyReports(ent, log);
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
        /// 删除我的日报
        /// </summary>
        public void DelCargoDailyReports()
        {
            String json = Request["data"];
            if (String.IsNullOrEmpty(json)) return;
            List<CargoDailyReportsEntity> list = new List<CargoDailyReportsEntity>();
            ArrayList rows = (ArrayList)JSON.Decode(json);
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "静态数据管理";
            log.NvgPage = "我的日报";
            log.UserID = UserInfor.LoginName.Trim();
            log.Status = "0";
            log.Operate = "D";
            foreach (Hashtable row in rows)
            {
                list.Add(new CargoDailyReportsEntity
                {
                    ID = Convert.ToInt32(row["ID"]),
                    Title = Convert.ToString(row["Title"]),
                    LoginName = UserInfor.LoginName.Trim(),
                    UserName = UserInfor.UserName.Trim(),
                    LoginReportName = Convert.ToString(Request["LoginReportName"]),
                    UserReportName = Convert.ToString(Request["UserReportName"])
                });
            }
            CargoStaticBus bus = new CargoStaticBus();
            try
            {
                bus.DelCargoDailyReports(list, log);
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
        /// <summary>
        /// 查看评论
        /// </summary>
        public void QueryReportComments()
        {
            CargoReportCommentsEntity queryEntity = new CargoReportCommentsEntity();
            //查询条件
            string key = Request["ID"];
            if (string.IsNullOrEmpty(key)) { Response.Clear(); Response.Write(JSON.Encode(queryEntity)); return; }

            queryEntity.Report_id = Convert.ToInt32(key);

            CargoStaticBus bus = new CargoStaticBus();

            List<CargoReportCommentsEntity> list = bus.QueryReportComments(queryEntity);
            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 添加我的日报评论
        /// </summary>
        public void AddReportComments()
        {
            CargoReportCommentsEntity ent = new CargoReportCommentsEntity();
            CargoStaticBus bus = new CargoStaticBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "静态数据管理";
            log.NvgPage = "我的日报";
            log.Status = "0";
            log.UserID = UserInfor.LoginName.Trim();
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            String id = Request["ID"] != null ? Request["ID"].ToString() : "";
            try
            {
                //赋值基础数据
                ent.Report_id = Convert.ToInt32(Request["ID"]);
                ent.Content = Server.UrlDecode(Convert.ToString(Request["ReportContent"]));
                ent.LoginName = UserInfor.LoginName.Trim();
                ent.UserName = UserInfor.UserName.Trim();

                if (string.IsNullOrEmpty(msg.Message))
                {

                    log.Operate = "A";
                    bus.AddReportComments(ent, log);

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
        /// 删除我的日报批复
        /// </summary>
        public void DelCargoReportCommentsByReportId()
        {
            List<CargoReportCommentsEntity> list = new List<CargoReportCommentsEntity>();
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "静态数据管理";
            log.NvgPage = "我的日报";
            log.UserID = UserInfor.LoginName.Trim();
            log.Status = "0";
            log.Operate = "D";

            list.Add(new CargoReportCommentsEntity
            {
                ID = Convert.ToInt32(Request["ID"]),
                LoginName = UserInfor.LoginName.Trim(),
                UserName = UserInfor.UserName.Trim(),
                Report_id = Convert.ToInt32(Request["Report_id"]),
            });

            CargoStaticBus bus = new CargoStaticBus();
            try
            {
                bus.DelCargoReportCommentsByReportId(list, log);
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
        #region 物流订阅操作方法集合
        /// <summary>
        /// 查询
        /// </summary>
        public void QueryHouseLogisPoll()
        {
            CargoHouseLogisPollEntity queryEntity = new CargoHouseLogisPollEntity();
            queryEntity.LogisticName = Convert.ToString(Request["LogisticName"]);
            if (!string.IsNullOrEmpty(Request["HID"]))
            {
                queryEntity.HouseID = Convert.ToInt32(Request["HID"]);
            }
            //分页
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]);
            CargoStaticBus bus = new CargoStaticBus();
            Hashtable list = bus.QueryHouseLogisPoll(pageIndex, pageSize, queryEntity);

            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 删除
        /// </summary>
        public void DelHouseLogisPoll()
        {
            String json = Request["data"];
            if (String.IsNullOrEmpty(json)) return;
            List<CargoHouseLogisPollEntity> list = new List<CargoHouseLogisPollEntity>();
            ArrayList rows = (ArrayList)JSON.Decode(json);
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            CargoStaticBus bus = new CargoStaticBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "物流管理";
            log.Status = "0";
            log.NvgPage = "物流公司订阅";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "D";
            try
            {
                foreach (Hashtable row in rows)
                {
                    list.Add(new CargoHouseLogisPollEntity
                    {
                        ID = Convert.ToInt32(row["ID"]),
                        DelFlag = Convert.ToString(row["DelFlag"]),
                        LogisticName = Convert.ToString(row["LogisticName"]),
                        HouseName = Convert.ToString(row["HouseName"])
                    });
                }
                bus.DelHouseLogisPoll(list, log);
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
        /// <summary>
        /// 保存
        /// </summary>
        public void SaveHouseLogisPoll()
        {
            CargoHouseLogisPollEntity ent = new CargoHouseLogisPollEntity();
            CargoStaticBus bus = new CargoStaticBus();
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "物流管理";
            log.NvgPage = "物流公司订阅";
            log.Status = "0";
            log.UserID = UserInfor.LoginName.Trim();
            String id = Request["ID"] != null ? Request["ID"].ToString() : "";
            try
            {
                ent.LogisID = Convert.ToInt32(Request["LogisID"]);
                ent.LogisticName = Convert.ToString(Request["LogisticName"]).Trim();
                ent.ComCode = Convert.ToString(Request["ComCode"]).Trim();
                ent.HouseID = Convert.ToInt32(Request["HouseID"]);
                ent.OP_ID = UserInfor.LoginName;
                if (id == "")
                {
                    //2000-06-18取消相同收货人判断
                    if (bus.IsExistHouseLogisPoll(ent))
                    {
                        msg.Result = false;
                        msg.Message = "已经存在订阅";
                    }
                    else
                    {
                        log.Operate = "A";
                        bus.AddHouseLogisPoll(ent, log);
                        msg.Result = true;
                        msg.Message = "成功";
                    }
                }
                else
                {
                    ent.ID = Convert.ToInt32(id);
                    log.Operate = "U";
                    bus.UpdateHouseLogisPoll(ent, log);
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
        #region 客户回访操作方法集合
        /// <summary>
        /// 客户回访数据查询
        /// </summary>
        public void QueryCargoFeedBack()
        {
            CargoFeedBackEntity queryEntity = new CargoFeedBackEntity();
            queryEntity.CompanyName = Convert.ToString(Request["CompanyName"]);
            queryEntity.FeedBackName = Convert.ToString(Request["FeedBackName"]);

            if (!string.IsNullOrEmpty(Request["HID"]))
            {
                queryEntity.HouseID = Convert.ToInt32(Request["HID"]);
            }
            if (Convert.ToString(Request["ResultType"]) != "-1")
            {
                queryEntity.ResultType = Convert.ToString(Request["ResultType"]);
            }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }
            //分页
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]);
            CargoStaticBus bus = new CargoStaticBus();
            Hashtable list = bus.QueryCargoFeedBack(pageIndex, pageSize, queryEntity);

            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 
        /// </summary>
        public void QueryCargoFeedBackImport()
        {
            string err = "OK";
            CargoFeedBackEntity queryEntity = new CargoFeedBackEntity();
            queryEntity.CompanyName = Convert.ToString(Request["CompanyName"]);
            queryEntity.FeedBackName = Convert.ToString(Request["FeedBackName"]);

            if (!string.IsNullOrEmpty(Request["HID"]))
            {
                queryEntity.HouseID = Convert.ToInt32(Request["HID"]);
            }
            if (Convert.ToString(Request["ResultType"]) != "-1")
            {
                queryEntity.ResultType = Convert.ToString(Request["ResultType"]);
            }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }
            //分页
            int pageIndex = 1;
            int pageSize = 100000;
            CargoStaticBus bus = new CargoStaticBus();
            Hashtable list = bus.QueryCargoFeedBack(pageIndex, pageSize, queryEntity);

            List<CargoFeedBackEntity> awbList = list["rows"] as List<CargoFeedBackEntity>;
            if (awbList.Count > 0) { CargoFeedBackExportEntity = awbList; } else { err = "没有数据可以进行导出，请重新查询！"; }
            //JSON
            String json = JSON.Encode(err);
            Response.Clear();
            Response.Write(json);
        }
        public List<CargoFeedBackEntity> CargoFeedBackExportEntity
        {
            get
            {
                if (Session["CargoFeedBackExportEntity"] == null) { Session["CargoFeedBackExportEntity"] = new List<CargoFeedBackEntity>(); }
                return (List<CargoFeedBackEntity>)(Session["CargoFeedBackExportEntity"]);
            }
            set
            {
                Session["CargoFeedBackExportEntity"] = value;
            }
        }
        #endregion
    }
}