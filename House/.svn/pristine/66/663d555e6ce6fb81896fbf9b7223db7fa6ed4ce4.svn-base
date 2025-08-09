using House.Business.House;
using House.Entity;
using House.Entity.House;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace House
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

                //invoke method
                Type type = this.GetType();
                MethodInfo method = type.GetMethod(methodName);
                method.Invoke(this, null);
            }
            catch (Exception ex)
            {

            }
        }

        #region 系统管理方法集合
        /// <summary>
        /// 查询所有系统数据
        /// </summary>
        public void QueryHouse()
        {
            SystemSetEntity queryEntity = new SystemSetEntity();
            queryEntity.HouseName = Convert.ToString(Request["HouseName"]);
            queryEntity.HouseCode = Convert.ToString(Request["HouseCode"]);
            if (Request["DelFlag"] != "-1") { queryEntity.DelFlag = Convert.ToString(Request["DelFlag"]); }
            //分页
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]);
            SystemBus bus = new SystemBus();

            Hashtable list = bus.QueryHouse(pageIndex, pageSize, queryEntity);

            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 保存系统数据信息
        /// </summary>
        public void saveHouse()
        {
            SystemSetEntity ent = new SystemSetEntity();
            SystemBus bus = new SystemBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "系统管理";
            log.NvgPage = "系统管理";
            log.UserID = UserInfor.LoginName.Trim();
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            log.Status = "0";
            msg.Result = false;
            String id = Request["HouseID"] != null ? Request["HouseID"].ToString() : "";
            try
            {
                ent.HouseName = Convert.ToString(Request["HouseName"]);
                ent.HouseCode = Convert.ToString(Request["HouseCode"]);
                ent.Person = Convert.ToString(Request["Person"]);
                ent.Cellphone = Convert.ToString(Request["Cellphone"]);
                ent.QQ = Convert.ToString(Request["QQ"]);
                ent.Weixin = Convert.ToString(Request["Weixin"]);
                ent.Email = Convert.ToString(Request["Email"]);
                ent.Remark = Convert.ToString(Request["Remark"]);
                ent.DelFlag = Convert.ToString(Request["DelFlag"]);
                if (id == "")  //新增：id为空，或_state为added
                {
                    log.Operate = "A";
                    bus.AddHouse(ent, log);
                }
                else
                {
                    ent.HouseID = Convert.ToInt32(Request["HouseID"]);
                    log.Operate = "U";
                    bus.UpdateHouse(ent, log);
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
        /// 删除系统数据
        /// </summary>
        public void DelHouse()
        {
            String json = Request["data"];
            if (String.IsNullOrEmpty(json)) return;
            List<SystemSetEntity> list = new List<SystemSetEntity>();
            ArrayList rows = (ArrayList)JSON.Decode(json);
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "系统管理";
            log.NvgPage = "系统管理";
            log.UserID = UserInfor.LoginName.Trim();
            log.Status = "0";
            log.Operate = "D";
            foreach (Hashtable row in rows)
            {
                list.Add(new SystemSetEntity
                {
                    HouseID = Convert.ToInt32(row["HouseID"]),
                    HouseName = Convert.ToString(row["HouseName"]),
                    HouseCode = Convert.ToString(row["HouseCode"]),
                    Person = Convert.ToString(row["Person"]),
                    Email = Convert.ToString(row["Email"]),
                    Cellphone = Convert.ToString(row["Cellphone"]),
                    QQ = Convert.ToString(row["QQ"]),
                    Weixin = Convert.ToString(row["Weixin"]),
                    Remark = Convert.ToString(row["Remark"]),
                    DelFlag = Convert.ToString(row["DelFlag"])
                });
            }
            SystemBus bus = new SystemBus();
            try
            {
                bus.DelHouse(list, log);
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
        /// 查询所有系统
        /// </summary>
        /// <returns></returns>
        public void QueryALLHouse()
        {
            SystemSetEntity queryEntity = new SystemSetEntity();
            SystemBus bus = new SystemBus();
            List<SystemSetEntity> list = bus.QueryALLHouse();
            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }
        #endregion

        #region 角色界面操作方法集合
        /// <summary>
        /// 角色查询
        /// </summary>
        public void QueryRoles()
        {
            SystemRoleEntity queryEntity = new SystemRoleEntity();
            queryEntity.CName = Convert.ToString(Request["RoleName"]);
            if (Request["dFlag"] != "-1") { queryEntity.DelFlag = Convert.ToString(Request["dFlag"]); }
            //分页
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]);
            SystemBus bus = new SystemBus();
            Hashtable list = bus.QueryRole(pageIndex, pageSize, queryEntity);

            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 保存角色数据
        /// </summary>
        public void SaveRoles()
        {
            SystemRoleEntity ent = new SystemRoleEntity();
            SystemBus bus = new SystemBus();
            ErrMessage msg = new ErrMessage(); msg.Message = "";

            msg.Result = false;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "系统管理";
            log.NvgPage = "角色管理";
            log.Status = "0";
            log.UserID = UserInfor.LoginName.Trim();
            String id = Request["RoleID"] != null ? Request["RoleID"].ToString() : "";
            try
            {
                ent.CName = Convert.ToString(Request["CName"]);
                ent.IsAdmin = Convert.ToString(Request["IsAdmin"]);
                ent.Remark = Convert.ToString(Request["Remark"]);
                ent.DelFlag = Convert.ToString(Request["DelFlag"]);
                if (id == "")
                {
                    log.Operate = "A";
                    bus.AddRole(ent, log);
                }
                else
                {
                    log.Operate = "U";
                    ent.RoleID = Convert.ToInt32(id);
                    bus.UpdateRole(ent, log);
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
        /// 删除角色数据
        /// </summary>
        public void DelRole()
        {
            String json = Request["data"];
            if (String.IsNullOrEmpty(json)) return;
            List<SystemRoleEntity> list = new List<SystemRoleEntity>();
            ArrayList rows = (ArrayList)JSON.Decode(json);
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            SystemBus bus = new SystemBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "系统管理";
            log.Status = "0";
            log.NvgPage = "角色管理";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "D";
            try
            {
                foreach (Hashtable row in rows)
                {
                    list.Add(new SystemRoleEntity
                    {
                        RoleID = Convert.ToInt32(row["RoleID"]),
                        CName = Convert.ToString(row["CName"]),
                        Remark = Convert.ToString(row["Remark"]),
                        DelFlag = Convert.ToString(row["DelFlag"])
                    });
                }
                bus.DelRole(list, log);
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
        /// 按RoleID查询角色数据
        /// </summary>
        public void GetRoleByID()
        {
            int id = Convert.ToInt32(Request["id"]);
            SystemBus bus = new SystemBus();
            SystemRoleEntity result = bus.GetRoleByID(id);
            String json = JSON.Encode(result);
            Response.Write(json);
        }
        /// <summary>
        /// 查询所有角色
        /// </summary>
        public void QueryALLRole()
        {
            SystemBus bus = new SystemBus();
            List<SystemRoleEntity> list = bus.QueryALLRole();

            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 保存角色权限
        /// </summary>
        public void SaveRoleItem()
        {
            String pnodes = Request["pnodes"];
            String cnodes = Request["cnodes"];
            int RoleID = Convert.ToInt32(Request["RID"]);
            List<SystemRoleItemEntity> oEn = new List<SystemRoleItemEntity>();
            //父节点
            if (!string.IsNullOrEmpty(pnodes))
            {
                string[] pstr = pnodes.Split(',');
                foreach (var ps in pstr)
                {
                    oEn.Add(new SystemRoleItemEntity { RoleID = RoleID, ItemID = Convert.ToInt32(ps), OP_DATE = DateTime.Now });
                }
            }
            //子节点
            if (!string.IsNullOrEmpty(cnodes))
            {
                string[] pstr = cnodes.Split(',');
                foreach (var ps in pstr)
                {
                    oEn.Add(new SystemRoleItemEntity { RoleID = RoleID, ItemID = Convert.ToInt32(ps), OP_DATE = DateTime.Now });
                }
            }
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "系统管理";
            log.Status = "0";
            log.NvgPage = "角色管理";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "A";
            try
            {
                SystemBus bus = new SystemBus();
                bus.AddRoleItem(oEn, log);
                msg.Result = true;
                msg.Message = "保存成功";
            }
            catch (ApplicationException ex) { msg.Result = false; msg.Message = ex.Message; }
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
        }
        /// <summary>
        /// 按RoleID查询角色导航权限数据
        /// </summary>
        public void GetItemByRoleID()
        {
            int id = Convert.ToInt32(Request["id"]);
            SystemBus bus = new SystemBus();
            string resitem = string.Empty;
            List<SystemRoleItemEntity> result = bus.GetItemByRoleID(id);
            foreach (var it in result)
            {
                resitem += it.ItemID.ToString() + ",";
            }
            if (!string.IsNullOrEmpty(resitem))
            {
                resitem = resitem.Substring(0, resitem.Length - 1);
            }
            String json = JSON.Encode(resitem);
            Response.Write(json);
        }
        #endregion

        #region 导航界面操作方法集合
        /// <summary>
        /// 导航链接查询
        /// </summary>
        public void SystemItemQuery()
        {
            SystemItemEntity queryEntity = new SystemItemEntity();
            queryEntity.CName = Request["CName"];
            if (!string.IsNullOrEmpty(Convert.ToString(Request["ParentID"])))
            {
                queryEntity.ParentID = Convert.ToInt32(Request["ParentID"]);
            }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["HouseID"])))
            {
                queryEntity.HouseID = Convert.ToInt32(Request["HouseID"]);
            }
            if (Request["dFlag"] != "-1")
            {
                queryEntity.DelFlag = Convert.ToString(Request["dFlag"]);
            }
            //分页
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]);
            SystemBus bus = new SystemBus();
            Hashtable list = bus.SystemItemQuery(pageIndex, pageSize, queryEntity);

            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 按ItemID查询导航数据
        /// </summary>
        public void GetSystemItemByID()
        {
            int id = Convert.ToInt32(Request["id"]);
            SystemBus bus = new SystemBus();
            SystemItemEntity result = bus.GetSystemItemByID(id);
            String json = JSON.Encode(result);
            Response.Write(json);
        }
        /// <summary>
        /// 查询第一级菜单名
        /// </summary>
        public void ParentItemQuery()
        {
            SystemItemEntity queryEntity = new SystemItemEntity();

            SystemBus bus = new SystemBus();
            List<SystemItemEntity> list = bus.ParentItemQuery(0);

            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 根据系统ID查询该系统下的所有导航链接
        /// </summary>
        public void ParentItemQueryByHouseID()
        {
            int id = Convert.ToInt32(Request["id"]);
            SystemItemEntity queryEntity = new SystemItemEntity();

            SystemBus bus = new SystemBus();
            List<SystemItemEntity> list = bus.ParentItemQuery(id);

            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 保存导航链接数据
        /// </summary>
        public void SystemItemSave()
        {
            SystemItemEntity ent = new SystemItemEntity();
            SystemBus bus = new SystemBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "系统管理";
            log.NvgPage = "导航管理";
            log.Status = "0";
            log.UserID = UserInfor.LoginName.Trim();
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            String id = Request["ItemID"] != null ? Request["ItemID"].ToString() : "";
            try
            {
                ent.ItemIcon = Convert.ToString(Request["ItemIcon"]);
                ent.HouseID = Convert.ToInt32(Request["HouseID"]);
                ent.HouseName = Convert.ToString(Request["AHouseName"]);
                ent.HouseCode = Convert.ToString(Request["AHouseCode"]);
                ent.CName = Convert.ToString(Request["CName"]);
                ent.EName = Convert.ToString(Request["EName"]);
                ent.ItemSrc = Convert.ToString(Request["ItemSrc"]);
                if (!string.IsNullOrEmpty(Convert.ToString(Request["ParentID"])))
                {
                    ent.ParentID = Convert.ToInt32(Request["ParentID"]);
                }
                else { ent.ParentID = 0; }
                ent.DelFlag = Convert.ToString(Request["DelFlag"]);
                ent.ItemSort = Convert.ToString("0");
                if (!ent.ParentID.Equals(0))
                {
                    ent.ItemLevel = Convert.ToString("2");
                }
                else
                {
                    ent.ItemLevel = Convert.ToString("1");
                }
                if (id == "")//新增：id为空，或_state为added
                {
                    log.Operate = "A";
                    bus.SystemItemAdd(ent, log);
                }
                else
                {
                    ent.ItemID = Convert.ToInt32(id);
                    log.Operate = "U";
                    bus.SystemItemUpdate(ent, log);
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
        /// 保存组织结构
        /// </summary>
        public void ItemSortSave()
        {
            String json = Request["data"];

            //获得树形数据
            ArrayList tree = (ArrayList)JSON.Decode(json);

            //首先，将旧的树删除掉
            List<SystemItemEntity> oEn = new List<SystemItemEntity>();
            string err = "OK";
            try
            {
                for (int i = 0; i < tree.Count; i++)
                {
                    Hashtable task = (Hashtable)tree[i];
                    if (task["children"] != null)
                    {
                        ArrayList al = (ArrayList)task["children"];
                        for (int j = 0; j < al.Count; j++)
                        {
                            Hashtable child = (Hashtable)al[j];
                            oEn.Add(new SystemItemEntity
                            {
                                ItemID = Convert.ToInt32(child["id"]),
                                CName = Convert.ToString(child["text"]),
                                ItemSort = j.ToString()
                            });
                        }
                    }
                    oEn.Add(new SystemItemEntity
                    {
                        ItemID = Convert.ToInt32(task["id"]),
                        CName = Convert.ToString(task["text"]),
                        ItemSort = i.ToString()
                    });
                }

                LogEntity log = new LogEntity();
                log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
                log.Moudle = "系统管理";
                log.NvgPage = "导航管理";
                log.UserID = UserInfor.LoginName.Trim();
                log.Status = "0";
                log.Operate = "A";

                SystemBus bus = new SystemBus();
                bus.SystemItemSortUpdate(oEn, log);
            }
            catch (ApplicationException ex)
            {

                err = ex.Message;
            }
            Response.Clear();
            Response.Write(err);
        }
        /// <summary>
        /// 删除导航链接
        /// </summary>
        public void SystemItemDel()
        {
            String json = Request["data"];
            if (String.IsNullOrEmpty(json)) return;
            List<SystemItemEntity> list = new List<SystemItemEntity>();
            ArrayList rows = (ArrayList)JSON.Decode(json);
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            SystemBus bus = new SystemBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "系统管理";
            log.Status = "0";
            log.NvgPage = "导航管理";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "D";
            try
            {
                foreach (Hashtable row in rows)
                {
                    list.Add(new SystemItemEntity
                    {
                        ItemID = Convert.ToInt32(row["ItemID"]),
                        CName = Convert.ToString(row["CName"]),
                        EName = Convert.ToString(row["EName"]),
                        ItemSrc = Convert.ToString(row["ItemSrc"]),
                        DelFlag = Convert.ToString(row["DelFlag"])
                    });
                }
                bus.SystemItemDel(list, log);
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

        #region 组织架构操作方法集合
        /// <summary>
        /// 查询组织架构
        /// </summary>
        public void SystemOrganizeQuery()
        {
            SystemBus bus = new SystemBus();
            List<SystemOrganizeEntity> result = bus.SystemOrganizeQuery();
            string res = "[";
            foreach (var it in result)
            {
                if (it.ParentID.Equals(-1))
                {
                    continue;
                }
                if (it.ParentID.Equals(0))
                {
                    res += GetNode(result, it);
                }
            }

            res = res.Substring(0, res.Length - 1);
            res += "]";
            Response.Clear();
            Response.Write(res);
        }
        /// <summary>
        /// 根据ID返回节点
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private string GetNode(List<SystemOrganizeEntity> orgList, SystemOrganizeEntity org)
        {
            string result = string.Empty;
            List<SystemOrganizeEntity> secList = orgList.FindAll(c => c.ParentID.Equals(org.ID));
            if (secList.Count > 0)
            {
                result += "{\"ID\": \"" + org.ID.ToString() + "\",\"Name\":\"" + org.Name.Trim() + "\",\"Code\":\"" + org.Code.Trim() + "\",\"Boss\":\"" + org.Boss.Trim() + "\",\"Remark\":\"" + org.Remark.Trim() + "\",\"state\":\"closed\",\"children\":[";
                foreach (var it in secList)
                {
                    result += GetNode(orgList, it);
                }
                result = result.Substring(0, result.Length - 1);
                result += "]},";
            }
            else
            {
                result += "{\"ID\": \"" + org.ID.ToString() + "\",\"Name\":\"" + org.Name.Trim() + "\",\"Code\":\"" + org.Code.Trim() + "\",\"Boss\":\"" + org.Boss.Trim() + "\",\"Remark\":\"" + org.Remark.Trim() + "\"},";
            }
            return result;
        }
        /// <summary>
        /// 删除组织架构
        /// </summary>
        public void SystemOrganizeDel()
        {
            String json = Request["data"];
            if (String.IsNullOrEmpty(json)) return;
            List<SystemOrganizeEntity> list = new List<SystemOrganizeEntity>();
            ArrayList rows = (ArrayList)JSON.Decode(json);
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "系统管理";
            log.NvgPage = "组织架构";
            log.UserID = UserInfor.LoginName.Trim();
            log.Status = "0";
            log.Operate = "D";
            foreach (Hashtable row in rows)
            {
                list.Add(new SystemOrganizeEntity
                {
                    ID = Convert.ToInt32(row["ID"]),
                    Code = Convert.ToString(row["Code"]),
                    Name = Convert.ToString(row["Name"])
                });
            }
            SystemBus bus = new SystemBus();
            try
            {
                bus.SystemOrganizeDel(list, log);
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
        /// 保存组织架构
        /// </summary>
        public void SystemOrganizeSave()
        {
            SystemOrganizeEntity ent = new SystemOrganizeEntity();
            SystemBus bus = new SystemBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "系统管理";
            log.NvgPage = "组织架构";
            log.Status = "0";
            log.UserID = UserInfor.LoginName.Trim();
            ErrMessage msg = new ErrMessage();
            msg.Result = true;
            msg.Message = "成功";
            String id = Request["ID"] != null ? Request["ID"].ToString() : "";
            try
            {
                ent.Name = Convert.ToString(Request["Name"]);
                ent.Code = Convert.ToString(Request["Code"]);
                ent.Boss = Convert.ToString(Request["Boss"]);
                ent.Remark = Convert.ToString(Request["Remark"]);

                if (!string.IsNullOrEmpty(Convert.ToString(Request["PID"])))
                {
                    ent.ParentID = Convert.ToInt32(Request["PID"]);
                }
                ent.Sort = 1;

                if (id == "")//新增：id为空，或_state为added
                {
                    log.Operate = "A";
                    if (bus.isExistOrganizeCode(ent))
                    {
                        msg.Message = "已经存在相同的组织架构代码";
                        msg.Result = false;
                    }
                    else
                    {
                        bus.SystemOrganizeAdd(ent, log);
                    }
                }
                else
                {
                    ent.ID = Convert.ToInt32(id);
                    log.Operate = "U";
                    bus.SystemOrganizeUpdate(ent, log);
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
        /// 获取所有组织架构
        /// </summary>
        public void QueryAllOrganize()
        {
            SystemBus bus = new SystemBus();
            List<SystemOrganizeEntity> result = bus.SystemOrganizeQuery();
            string res = "[";
            foreach (var it in result)
            {
                if (it.ParentID.Equals(-1))
                {
                    continue;
                }
                if (it.ParentID.Equals(0))
                {
                    res += QueryNode(result, it);
                }
            }

            res = res.Substring(0, res.Length - 1);
            res += "]";
            Response.Clear();
            Response.Write(res);
        }
        /// <summary>
        /// 根据ID返回节点
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private string QueryNode(List<SystemOrganizeEntity> orgList, SystemOrganizeEntity org)
        {
            string result = string.Empty;
            List<SystemOrganizeEntity> secList = orgList.FindAll(c => c.ParentID.Equals(org.ID));
            if (secList.Count > 0)
            {
                result += "{\"id\": \"" + org.ID.ToString() + "\",\"text\":\"" + org.Name.Trim() + "\",\"children\":[";
                foreach (var it in secList)
                {
                    result += QueryNode(orgList, it);
                }
                result = result.Substring(0, result.Length - 1);
                result += "]},";
            }
            else
            {
                result += "{\"id\": \"" + org.ID.ToString() + "\",\"text\":\"" + org.Name.Trim() + "\"},";
            }
            return result;
        }
        #endregion

        #region 用户界面操作方法集合
        /// <summary>
        /// 用户查询
        /// </summary>
        public void QueryUsers()
        {
            SystemUserEntity queryEntity = new SystemUserEntity();
            queryEntity.LoginName = Convert.ToString(Request["LoginName"]);
            queryEntity.UserName = Convert.ToString(Request["UserName"]);
            queryEntity.CellPhone = Convert.ToString(Request["CellPhone"]);
            if (!string.IsNullOrEmpty(Convert.ToString(Request["DepID"])))
            {
                queryEntity.DepID = Convert.ToInt32(Request["DepID"]);
            }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["RoleID"])))
            {
                queryEntity.RoleID = Convert.ToInt32(Request["RoleID"]);
            }
            if (Request["dFlag"] != "-1") { queryEntity.DelFlag = Convert.ToString(Request["dFlag"]); }
            //分页
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]);
            SystemBus bus = new SystemBus();
            Hashtable list = bus.QueryUsers(pageIndex, pageSize, queryEntity);
            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 用户数据导出
        /// </summary>
        public void QueryUsersForExport()
        {
            SystemUserEntity queryEntity = new SystemUserEntity();
            //查询条件
            string key = Request["key"];
            if (string.IsNullOrEmpty(key)) { Response.Clear(); Response.Write(JSON.Encode(queryEntity)); return; }
            string[] arr = key.Split(',');
            for (int i = 0; i < arr.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        if (!string.IsNullOrEmpty(arr[i])) { queryEntity.LoginName = arr[i].Trim(); } break;
                    case 1:
                        if (!string.IsNullOrEmpty(arr[i])) { } break;
                    case 2:
                        if (!string.IsNullOrEmpty(arr[i])) { queryEntity.DepID = Convert.ToInt32(arr[i].Trim()); } break;
                    case 3:
                        if (!string.IsNullOrEmpty(arr[i])) { queryEntity.RoleID = Convert.ToInt32(arr[i].Trim()); } break;
                    case 4:
                        if (!string.IsNullOrEmpty(arr[i])) { queryEntity.CellPhone = arr[i].Trim(); } break;
                    case 5:
                        if (!arr[i].Equals("-1")) { } break;
                    case 6:
                        if (!arr[i].Equals("-1")) { queryEntity.DelFlag = Convert.ToString(arr[i]); } break;
                    default:
                        break;
                }
            }
            //分页
            int pageIndex = Convert.ToInt32(Request["page"]) == 0 ? 1 : Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]) == 0 ? 1000 : Convert.ToInt32(Request["rows"]);
            SystemBus bus = new SystemBus();
            Hashtable list = bus.QueryUsers(pageIndex, pageSize, queryEntity);
            string err = "OK";
            List<SystemUserEntity> awbList = (List<SystemUserEntity>)list["rows"];
            if (awbList.Count > 0)
            {
                // UserList = awbList;
            }
            else
            {
                err = "没有数据可以进行导出，请重新查询！";
            }
            //JSON
            String json = JSON.Encode(err);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 新增用户信息
        /// </summary>
        public void SaveUsers()
        {
            SystemUserEntity ent = new SystemUserEntity();
            SystemBus bus = new SystemBus();
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "系统管理";
            log.NvgPage = "用户管理";
            log.Status = "0";
            log.UserID = UserInfor.LoginName.Trim();
            String id = Request["UserID"] != null ? Request["UserID"].ToString() : "";
            try
            {
                ent.LoginName = Convert.ToString(Request["LoginName"]);
                ent.UserName = Convert.ToString(Request["UserName"]);
                ent.Sex = Convert.ToString(Request["Sex"]);
                if (!string.IsNullOrEmpty(Convert.ToString(Request["Age"])))
                {
                    ent.Age = Convert.ToInt32(Request["Age"]);
                }
                ent.UserIDNum = Convert.ToString(Request["UserIDNum"]);
                if (!string.IsNullOrEmpty(Convert.ToString(Request["RoleID"])))
                {
                    ent.RoleID = Convert.ToInt32(Request["RoleID"]);
                }
                if (!string.IsNullOrEmpty(Convert.ToString(Request["DepID"])))
                {
                    ent.DepID = Convert.ToInt32(Request["DepID"]);
                }
                ent.UserJob = Convert.ToString(Request["UserJob"]);
                ent.AddressPhone = Convert.ToString(Request["AddressPhone"]);
                ent.CompanyPhone = Convert.ToString(Request["CompanyPhone"]);
                ent.CellPhone = Convert.ToString(Request["CellPhone"]);
                ent.Email = Convert.ToString(Request["Email"]);
                ent.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
                ent.DelFlag = Convert.ToString(Request["DelFlag"]);
                ent.CargoPermisID = Convert.ToString(Request["CargoPermisID"]);
                ent.CargoPermisName = Convert.ToString(Request["CargoPermisName"]);
                if (!string.IsNullOrEmpty(Convert.ToString(Request["HouseID"])))
                {
                    ent.HouseID = Convert.ToInt32(Request["HouseID"]);
                    ent.HouseName = Convert.ToString(Request["HouseName"]);
                }
                if (id == "")
                {
                    log.Operate = "A";
                    if (!string.IsNullOrEmpty(Convert.ToString(Request["LoginPwd"])))
                    {
                        ent.LoginPwd = Common.EncodePassword(Convert.ToString(Request["LoginPwd"]));
                    }
                    int red = bus.AddUsers(ent, log);
                    msg.Result = true;
                    msg.Message = "成功";
                    if (red.Equals(1))
                    {
                        msg.Result = false;
                        msg.Message = "已经存在用户登陆名";
                    }
                }
                else
                {
                    log.Operate = "U";
                    ent.UserID = Convert.ToInt32(Request["UserID"]);
                    bus.UpdateUsers(ent, log);
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
        /// 修改密码
        /// </summary>
        public void UpdateUserPwd()
        {
            SystemUserEntity ent = new SystemUserEntity();
            SystemBus bus = new SystemBus();
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "公司内部管理";
            log.NvgPage = "密码管理";
            log.UserID = UserInfor.LoginName.Trim();
            log.Status = "0";
            log.Operate = "U";
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
                    ent.LoginName = Convert.ToString(Request["LoginName"]);
                    ent.LoginPwd = Common.EncodePassword(Convert.ToString(Request["Password"]));
                    if (ent.LoginName.Trim().ToUpper().Equals(UserInfor.LoginName.Trim().ToUpper()))
                    {
                        ent.UserID = UserInfor.UserID;
                    }
                    int re = bus.UpdateUserPwd(ent, log);
                    if (re.Equals(1))
                    {
                        msg.Message = "用户名不存在";
                        msg.Result = false;
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
        /// 删除用户
        /// </summary>
        public void DelUsers()
        {
            String idStr = Request["data"];
            if (String.IsNullOrEmpty(idStr)) return;
            List<SystemUserEntity> list = new List<SystemUserEntity>();
            ArrayList rows = (ArrayList)JSON.Decode(idStr);
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "公司内部管理";
            log.NvgPage = "用户管理";
            log.UserID = UserInfor.LoginName.Trim();
            log.Status = "0";
            log.Operate = "D";

            foreach (Hashtable row in rows)
            {
                list.Add(new SystemUserEntity
                {
                    UserID = Convert.ToInt32(row["UserID"]),
                    LoginName = Convert.ToString(row["LoginName"]),
                    UserName = Convert.ToString(row["UserName"]),
                    DelFlag = Convert.ToString(row["DelFlag"])
                });
            }
            SystemBus bus = new SystemBus();
            try
            {
                bus.DelUsers(list, log);
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
        /// 通过UserID查询该用户的数据
        /// </summary>
        public void GetUserByID()
        {
            int id = Convert.ToInt32(Request["id"]);
            SystemBus bus = new SystemBus();
            SystemUserEntity result = bus.GetUserByID(id);
            String json = JSON.Encode(result);
            Response.Write(json);
        }
        /// <summary>
        /// 查询用户列表
        /// </summary>
        public void AutoCompleteUser()
        {
            SystemUserEntity queryEntity = new SystemUserEntity();
            queryEntity.DelFlag = "0";
            //分页
            int pageIndex = 1;
            int pageSize = 1000;
            SystemBus bus = new SystemBus();
            Hashtable list = bus.QueryUsers(pageIndex, pageSize, queryEntity);
            //JSON
            String json = JSON.Encode(list["rows"]);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 查询所有用户
        /// </summary>
        public void QueryALLUser()
        {
            SystemBus bus = new SystemBus();
            List<SystemUserEntity> list = bus.QueryALLUser();
            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }

        /// <summary>
        /// 根据部门ID查询该部门下的所有用户
        /// </summary>
        public void GetUserByDepID()
        {
            //查询条件
            string id = Convert.ToString(Request["id"]);
            SystemBus bus = new SystemBus();
            List<SystemUserEntity> list = bus.GetUserByDepID(id);
            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 按登陆名查询所有导航链接并格式化
        /// </summary>
        public void QueryItemByLoginName(string loginName)
        {
            SystemBus bus = new SystemBus();
            List<SystemItemEntity> result = bus.QueryItemFormat(loginName, "");
            string res = "{basic:[";
            foreach (var it in result)
            {
                if (it.ParentID.Equals(-1))
                {
                    continue;
                }
                if (it.ParentID.Equals(0))
                {
                    res += "{'icon': '" + it.ItemIcon.Trim() + "', 'menuname': '" + it.CName.Trim() + "','menus':[";
                    List<SystemItemEntity> secList = result.FindAll(c => c.ParentID.Equals(it.ItemID));
                    foreach (var e in secList)
                    {
                        res += "{'url': '" + e.ItemSrc.ToString() + "', 'menuname': '" + e.CName.Trim() + "', 'icon': '" + e.ItemIcon.Trim() + "','menuid':'" + e.CName.Trim() + "'},";
                    }
                    res = res.Substring(0, res.Length - 1);
                    res += "]},";
                }
            }
            res = res.Substring(0, res.Length - 1);
            res += "]}";
            Response.Clear();
            Response.Write(res);
        }
        /// <summary>
        /// 查询所有导航链接并格式化
        /// </summary>
        public void QueryItemFormat()
        {
            //查询条件
            int id = Convert.ToInt32(Request["hid"]);
            SystemBus bus = new SystemBus();
            List<SystemItemEntity> result = bus.QueryItemFormat(id);
            string res = "[";
            foreach (var it in result)
            {
                if (it.ParentID.Equals(-1))
                {
                    continue;
                }
                if (it.ParentID.Equals(0))
                {
                    res += "{\"id\": \"" + it.ItemID.ToString() + "\",\"text\": \"" + it.CName.Trim() + "\",\"state\":\"closed\",\"children\":[";
                    List<SystemItemEntity> secList = result.FindAll(c => c.ParentID.Equals(it.ItemID));
                    foreach (var e in secList)
                    {
                        res += "{\"id\": \"" + e.ItemID.ToString() + "\",\"text\":\"" + e.CName.Trim() + "\"},";
                    }
                    res = res.Substring(0, res.Length - 1);
                    res += "]},";
                }
            }

            res = res.Substring(0, res.Length - 1);
            res += "]";
            Response.Clear();
            Response.Write(res);
        }
        #endregion

        #region 日志界面操作方法集合

        /// <summary>
        /// 日志查询
        /// </summary>
        public void LogQuery()
        {
            LogEntity queryEntity = new LogEntity();
            queryEntity.UserID = Convert.ToString(Request["UserID"]);
            if (Convert.ToString(Request["status"]) != "-1")
            {
                queryEntity.Status = Convert.ToString(Request["status"]);
            }
            queryEntity.Memo = Convert.ToString(Request["MainKey"]);
            queryEntity.Moudle = Convert.ToString(Request["Moudle"]);
            if (Convert.ToString(Request["OperType"]) != "-1")
            {
                queryEntity.Operate = Convert.ToString(Request["OperType"]);
            }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }
            //分页
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]);

            SystemBus bus = new SystemBus();
            Hashtable list = bus.LogQuery(pageIndex, pageSize, queryEntity);
            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }

        /// <summary>
        /// 获取日志详细信息
        /// </summary>
        public void GetLogByID()
        {
            Int64 id = Convert.ToInt64(Request["id"]);

            SystemBus bus = new SystemBus();
            LogEntity user = bus.GetLogByID(id);
            String json = JSON.Encode(user);
            Response.Write(json);
        }
        #endregion
    }
}