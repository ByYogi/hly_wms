using House.Entity;
using House.Entity.House;
using House.Manager;
using House.Manager.House;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace House.Business.House
{
    /// <summary>
    /// 基础数据业务逻辑层
    /// </summary>
    public class SystemBus
    {
        private SystemManager man = new SystemManager();

        #region 系统管理界面操作方法集合
        /// <summary>
        /// 系统查询
        /// </summary>
        /// <param name="pIndex">当前页数</param>
        /// <param name="pNum">每页多少条数据</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="strCountWhere">查询总数的查询条件</param>
        /// <returns></returns>
        public Hashtable QueryHouse(int pIndex, int pNum, SystemSetEntity entity)
        {
            return man.QueryHouse(pIndex, pNum, entity);
        }
        /// <summary>
        /// 新增系统管理数据
        /// </summary>
        /// <param name="entity"></param>
        public void AddHouse(SystemSetEntity entity, LogEntity log)
        {
            LogWrite<SystemSetEntity> lw = new LogWrite<SystemSetEntity>();
            try
            {
                man.AddHouse(entity);
                log.Memo = "新增系统管理数据";
                lw.WriteLog(entity, log);
            }
            catch (ApplicationException ex)
            {
                log.Status = "1";
                log.Memo = "新增系统管理数据，失败信息：" + ex.Message;
                lw.WriteLog(log);
                throw;
            }
        }
        /// <summary>
        /// 删除系统数据
        /// </summary>
        /// <param name="entity"></param>
        public void DelHouse(List<SystemSetEntity> entity, LogEntity log)
        {
            LogWrite<SystemSetEntity> lw = new LogWrite<SystemSetEntity>();
            try
            {
                man.DelHouse(entity);
                foreach (var it in entity)
                {
                    log.Memo = "作废系统：系统ID：" + it.HouseID.ToString() + "，系统名称：" + it.HouseName.Trim() + "，系统代码：" + it.HouseCode + "，联系人：" + it.Person.Trim() + "，联系手机：" + it.Cellphone.Trim() + "，QQ：" + it.QQ.ToString() + "，微信：" + it.Weixin + "，邮箱：" + it.Email;
                    lw.WriteLog(log);
                }
            }
            catch (ApplicationException ex)
            {
                log.Status = "1";
                log.Memo = "作废系统，失败信息：" + ex.Message;
                lw.WriteLog(log);
            }
        }

        /// <summary>
        /// 修改系统数据
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateHouse(SystemSetEntity entity, LogEntity log)
        {
            LogWrite<SystemSetEntity> lw = new LogWrite<SystemSetEntity>();
            try
            {
                man.UpdateHouse(entity);
                log.Memo = "修改系统数据";
                lw.WriteLog(entity, log);
            }
            catch (ApplicationException ex)
            {
                log.Status = "1";
                log.Memo = "修改系统数据，失败信息：" + ex.Message;
                lw.WriteLog(log);
                throw;
            }
        }
        /// <summary>
        /// 查询所有系统
        /// </summary>
        /// <returns></returns>
        public List<SystemSetEntity> QueryALLHouse()
        {
            return man.QueryALLHouse();
        }
        /// <summary>
        /// 判断系统代码是否存在 true:表示存在
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>true:表示存在</returns>
        public bool IsExistHouse(SystemSetEntity entity)
        {
            return man.IsExistHouse(entity);
        }
        #endregion

        #region 角色界面操作方法集合
        /// <summary>
        /// 角色查询
        /// </summary>
        /// <param name="pIndex">当前页数</param>
        /// <param name="pNum">每页多少条数据</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="strCountWhere">查询总数的查询条件</param>
        /// <returns></returns>
        public Hashtable QueryRole(int pIndex, int pNum, SystemRoleEntity entity)
        {

            return man.QueryRole(pIndex, pNum, entity);
        }
        /// <summary>
        /// 新增角色数据
        /// </summary>
        /// <param name="entity"></param>
        public void AddRole(SystemRoleEntity entity, LogEntity log)
        {

            LogWrite<SystemRoleEntity> lw = new LogWrite<SystemRoleEntity>();
            try
            {
                man.AddRole(entity);
                log.Memo = "新增角色数据";
                lw.WriteLog(entity, log);
            }
            catch (ApplicationException ex)
            {
                log.Status = "1";
                log.Memo = "新增角色数据，失败信息：" + ex.Message;
                lw.WriteLog(log);
                throw;
            }
        }
        /// <summary>
        /// 删除角色数据
        /// </summary>
        /// <param name="entity"></param>
        public void DelRole(List<SystemRoleEntity> entity, LogEntity log)
        {

            LogWrite<SystemRoleEntity> lw = new LogWrite<SystemRoleEntity>();
            try
            {
                man.DelRole(entity);
                foreach (var it in entity)
                {
                    log.Memo = "作废角色：角色ID：" + it.RoleID.ToString();
                    lw.WriteLog(log);
                }
            }
            catch (ApplicationException ex)
            {
                log.Status = "1";
                log.Memo = "作废角色数据，失败信息：" + ex.Message;
                lw.WriteLog(log);
                throw;
            }
        }
        /// <summary>
        /// 按RoleID查询角色数据
        /// </summary>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        public SystemRoleEntity GetRoleByID(int RoleID)
        {

            return man.GetRoleByID(RoleID);
        }
        /// <summary>
        /// 修改角色数据
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateRole(SystemRoleEntity entity, LogEntity log)
        {

            LogWrite<SystemRoleEntity> lw = new LogWrite<SystemRoleEntity>();
            try
            {
                man.UpdateRole(entity);
                log.Memo = "修改角色数据";
                lw.WriteLog(entity, log);
            }
            catch (ApplicationException ex)
            {
                log.Status = "1";
                log.Memo = "修改角色数据，失败信息：" + ex.Message;
                lw.WriteLog(log);
                throw;
            }
        }
        /// <summary>
        /// 查询所有角色名
        /// </summary>
        /// <returns></returns>
        public List<SystemRoleEntity> QueryALLRole()
        {

            return man.QueryALLRole();
        }
        #endregion

        #region 角色导航关联表操作方法集合
        /// <summary>
        /// 新增和修改均采用此方法
        /// </summary>
        /// <param name="entity"></param>
        public void AddRoleItem(List<SystemRoleItemEntity> entity, LogEntity log)
        {

            LogWrite<SystemRoleItemEntity> lw = new LogWrite<SystemRoleItemEntity>();
            try
            {
                man.AddRoleItem(entity);
                foreach (var it in entity)
                {
                    log.Memo = "新增角色导航：角色ID：" + it.RoleID.ToString() + "，导航ID：" + it.ItemID.ToString();
                    lw.WriteLog(log);
                }
            }
            catch (ApplicationException ex)
            {
                log.Status = "1";
                log.Memo = "新增角色导航，失败信息：" + ex.Message;
                lw.WriteLog(log);
                throw;
            }
        }
        /// <summary>
        /// 通过RoleID获取角色所具有的导航权限信息
        /// </summary>
        /// <param name="RoleID"></param>
        /// <returns></returns>
        public List<SystemRoleItemEntity> GetItemByRoleID(int RoleID)
        {

            return man.GetItemByRoleID(RoleID);
        }
        #endregion

        #region 导航界面操作方法集合

        /// <summary>
        /// 导航查询
        /// </summary>
        /// <param name="pIndex">当前页数</param>
        /// <param name="pNum">每页多少条数据</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="strCountWhere">查询总数的查询条件</param>
        /// <returns></returns>
        public Hashtable SystemItemQuery(int pIndex, int pNum, SystemItemEntity entity)
        {

            return man.SystemItemQuery(pIndex, pNum, entity);
        }
        /// <summary>
        /// 查询一级菜单名
        /// </summary>
        /// <returns></returns>
        public List<SystemItemEntity> ParentItemQuery(int houseID)
        {

            return man.ParentItemQuery(houseID);
        }
        /// <summary>
        /// 按ItemID查询导航数据
        /// </summary>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        public SystemItemEntity GetSystemItemByID(int ItemID)
        {

            return man.GetSystemItemByID(ItemID);
        }
        /// <summary>
        /// 新增导航链接
        /// </summary>
        /// <param name="entity"></param>
        public void SystemItemAdd(SystemItemEntity entity, LogEntity log)
        {

            LogWrite<SystemItemEntity> lw = new LogWrite<SystemItemEntity>();
            try
            {
                man.SystemItemAdd(entity);
                log.Memo = "新增导航数据";
                lw.WriteLog(entity, log);
            }
            catch (ApplicationException ex)
            {
                log.Status = "1";
                log.Memo = "新增导航数据，失败信息：" + ex.Message;
                lw.WriteLog(log);
                throw;
            }
        }
        /// <summary>
        /// 修改导航链接数据
        /// </summary>
        /// <param name="entity"></param>
        public void SystemItemUpdate(SystemItemEntity entity, LogEntity log)
        {

            LogWrite<SystemItemEntity> lw = new LogWrite<SystemItemEntity>();
            try
            {
                man.SystemItemUpdate(entity);
                log.Memo = "修改导航数据";
                lw.WriteLog(entity, log);
            }
            catch (ApplicationException ex)
            {
                log.Status = "1";
                log.Memo = "修改导航数据，失败信息：" + ex.Message;
                lw.WriteLog(log);
                throw;
            }
        }
        /// <summary>
        /// 删除导航链接
        /// </summary>
        /// <param name="entity"></param>
        public void SystemItemDel(List<SystemItemEntity> entity, LogEntity log)
        {

            LogWrite<SystemItemEntity> lw = new LogWrite<SystemItemEntity>();
            try
            {
                man.SystemItemDel(entity);
                foreach (var it in entity)
                {
                    log.Memo = "作废导航：导航ID：" + it.ItemID.ToString() + "，中文名称：" + it.CName.Trim() + "英文名称：" + it.EName.Trim() + "，导航链接：" + it.ItemSrc.Trim() + "，导航标识：" + it.DelFlag;
                    lw.WriteLog(log);
                }
            }
            catch (ApplicationException ex)
            {
                log.Status = "1";
                log.Memo = "作废导航，失败信息：" + ex.Message;
                lw.WriteLog(log);
                throw;
            }
        }
        /// <summary>
        /// 修改更新导航链接的排序
        /// </summary>
        /// <param name="entity"></param>
        public void SystemItemSortUpdate(List<SystemItemEntity> entity, LogEntity log)
        {

            LogWrite<SystemItemEntity> lw = new LogWrite<SystemItemEntity>();
            try
            {
                man.SystemItemSortUpdate(entity);
                log.Memo = "修改导航排序成功";
                lw.WriteLog(log);
            }
            catch (ApplicationException ex)
            {
                log.Status = "1";
                log.Memo = "修改导航排序失败，失败信息：" + ex.Message;
                lw.WriteLog(log);
                throw;
            }
        }
        /// <summary>
        /// 查询所有导航链接并格式化
        /// </summary>
        public List<SystemItemEntity> QueryItemFormat(int hid)
        {
            return man.QueryItemFormat(hid);
        }

        /// <summary>
        /// 按用户登陆名查询所有导航链接并格式化
        /// </summary>
        public List<SystemItemEntity> QueryItemFormat(string LoginName, string HouseCode)
        {

            return man.QueryItemFormat(LoginName, HouseCode);
        }
        #endregion

        #region 组织架构操作方法集合
        /// <summary>
        /// 查询组织架构
        /// </summary>
        /// <returns></returns>
        public List<SystemOrganizeEntity> SystemOrganizeQuery()
        {
            return man.SystemOrganizeQuery();
        }
        /// <summary>
        /// 查询组织架构
        /// </summary>
        /// <returns></returns>
        public List<SystemOrganizeEntity> SystemOrganizeQuery(SystemOrganizeEntity entity)
        {
            return man.SystemOrganizeQuery(entity);
        }
        /// <summary>
        /// 删除组织架构
        /// </summary>
        /// <param name="entity"></param>
        public void SystemOrganizeDel(List<SystemOrganizeEntity> entity, LogEntity log)
        {
            LogWrite<SystemOrganizeEntity> lw = new LogWrite<SystemOrganizeEntity>();
            try
            {
                man.SystemOrganizeDel(entity);
                foreach (var it in entity)
                {
                    log.Memo = "删除组织架构：ID：" + it.ID.ToString() + "，组织名称：" + it.Name.Trim() + "组织代码：" + it.Code.Trim();
                    lw.WriteLog(log);
                }
            }
            catch (ApplicationException ex)
            {
                log.Status = "1";
                log.Memo = "删除组织架构，失败信息：" + ex.Message;
                lw.WriteLog(log);
                throw;
            }
        }
        /// <summary>
        /// 新增组织架构
        /// </summary>
        /// <param name="entity"></param>
        public void SystemOrganizeAdd(SystemOrganizeEntity entity, LogEntity log)
        {
            LogWrite<SystemOrganizeEntity> lw = new LogWrite<SystemOrganizeEntity>();
            try
            {
                man.SystemOrganizeAdd(entity);
                log.Memo = "新增组织架构";
                lw.WriteLog(entity, log);
            }
            catch (ApplicationException ex)
            {
                log.Status = "1";
                log.Memo = "新增组织架构，失败信息：" + ex.Message;
                lw.WriteLog(log);
                throw;
            }
        }
        /// <summary>
        /// 修改组织架构
        /// </summary>
        /// <param name="entity"></param>
        public void SystemOrganizeUpdate(SystemOrganizeEntity entity, LogEntity log)
        {
            LogWrite<SystemOrganizeEntity> lw = new LogWrite<SystemOrganizeEntity>();
            try
            {
                man.SystemOrganizeUpdate(entity);
                log.Memo = "修改组织架构";
                lw.WriteLog(entity, log);
            }
            catch (ApplicationException ex)
            {
                log.Status = "1";
                log.Memo = "修改组织架构，失败信息：" + ex.Message;
                lw.WriteLog(log);
                throw;
            }
        }
        /// <summary>
        /// 判断是否存在相同的组织架构代码
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool isExistOrganizeCode(SystemOrganizeEntity entity)
        {
            return man.isExistOrganizeCode(entity);
        }
        #endregion

        #region 用户界面操作方法集合
        /// <summary>
        /// 用户查询
        /// </summary>
        /// <param name="pIndex">当前页数</param>
        /// <param name="pNum">每页多少条数据</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="strCountWhere">查询总数的查询条件</param>
        /// <returns></returns>
        public Hashtable QueryUsers(int pIndex, int pNum, SystemUserEntity entity)
        {
            return man.QueryUsers(pIndex, pNum, entity);
        }
        /// <summary>
        /// 新增用户数据
        /// </summary>
        /// <param name="entity"></param>
        public int AddUsers(SystemUserEntity entity, LogEntity log)
        {
            LogWrite<SystemUserEntity> lw = new LogWrite<SystemUserEntity>();
            try
            {
                int i = man.AddUsers(entity);
                switch (i)
                {
                    case 0:
                        log.Memo = "新增用户数据";
                        lw.WriteLog(entity, log);
                        break;
                    case 1:
                        log.Status = "1";
                        log.Memo = "用户已存在";
                        lw.WriteLog(log);
                        break;
                    default:
                        break;
                }
                return i;
            }
            catch (ApplicationException ex)
            {
                log.Status = "1";
                log.Memo = "新增用户数据失败，失败信息：" + ex.Message;
                lw.WriteLog(log);
                throw;
            }
        }
        /// <summary>
        /// 删除用户数据
        /// </summary>
        /// <param name="entity"></param>
        public void DelUsers(List<SystemUserEntity> entity, LogEntity log)
        {
            SystemManager man = new SystemManager();
            LogWrite<SystemUserEntity> lw = new LogWrite<SystemUserEntity>();
            try
            {
                man.DelUsers(entity);
                foreach (var it in entity)
                {
                    log.Memo = "作废用户：用户ID：" + it.UserID.ToString() + "，用户登陆名：" + it.LoginName.Trim() + "，用户名：" + it.UserName.Trim();
                    lw.WriteLog(log);
                }
            }
            catch (ApplicationException ex)
            {
                log.Status = "1";
                log.Memo = "作废用户，失败信息：" + ex.Message;
                lw.WriteLog(log);
            }
        }
        /// <summary>
        /// 按UserID查询用户数据
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public SystemUserEntity GetUserByID(int UserID)
        {
            SystemManager man = new SystemManager();
            return man.GetUserByID(UserID);
        }
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateUsers(SystemUserEntity entity, LogEntity log)
        {
            SystemManager man = new SystemManager();
            LogWrite<SystemUserEntity> lw = new LogWrite<SystemUserEntity>();
            try
            {
                man.UpdateUsers(entity);
                log.Memo = "修改用户数据";
                lw.WriteLog(entity, log);
            }
            catch (ApplicationException ex)
            {
                log.Status = "1";
                log.Memo = "修改用户数据，失败信息：" + ex.Message;
                lw.WriteLog(log);
                throw;
            }
        }
        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="entity"></param>
        public int UpdateUserPwd(SystemUserEntity entity, LogEntity log)
        {
            LogWrite<SystemUserEntity> lw = new LogWrite<SystemUserEntity>();
            SystemManager man = new SystemManager();
            try
            {
                int i = man.UpdateUserPwd(entity);
                switch (i)
                {
                    case 0:
                        log.Memo = "修改用户密码";
                        lw.WriteLog(entity, log);
                        break;
                    case 1:
                        log.Status = "1";
                        log.Memo = "用户不存在";
                        lw.WriteLog(log);
                        break;
                    default:
                        break;
                }
                return i;
            }
            catch (ApplicationException ex)
            {
                log.Status = "1";
                log.Memo = "修改用户密码失败，失败信息：" + ex.Message;
                lw.WriteLog(log);
                throw;
            }
        }

        /// <summary>
        /// 根据部门ID查询该部门下的所有用户
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<SystemUserEntity> GetUserByDepID(string DepID)
        {
            SystemManager man = new SystemManager();
            return man.GetUserByDepID(DepID);
        }
        /// <summary>
        /// 通过部门代码查询该部门下的所有用户
        /// </summary>
        /// <param name="DepCode"></param>
        /// <returns></returns>
        public List<SystemUserEntity> GetUserByDepCode(string DepCode, int HouseID)
        {
            return man.GetUserByDepCode(DepCode, HouseID);
        }
        /// <summary>
        /// 根据分公司代码查询该公司下面所有员工
        /// </summary>
        /// <param name="DepCode"></param>
        /// <returns></returns>
        public List<SystemUserEntity> GetUserByDepCode(string DepCode)
        {
            return man.GetUserByDepCode(DepCode);
        }
        /// <summary>
        /// 查询所有用户名
        /// </summary>
        /// <returns></returns>
        public List<SystemUserEntity> QueryALLUser()
        {
            return man.QueryALLUser();
        }
        /// <summary>
        /// 查找最大的登陆ID
        /// </summary>
        /// <returns></returns>
        public int GetMaxUserLoginID()
        {
            SystemManager man = new SystemManager();
            return man.GetMaxUserLoginID();
        }
        /// <summary>
        /// 判断用户名是否存在 true:表示存在
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>true:表示存在</returns>
        public bool IsExistUser(SystemUserEntity entity)
        {
            return man.IsExistUser(entity);
        }
        /// <summary>
        /// 通过登陆账号查询用户姓名
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public SystemUserEntity ReturnUserName(string loginName)
        {
            return man.ReturnUserName(loginName);
        }
        #endregion

        #region 用户登陆判断方法集合

        /// <summary>
        /// 检查用户登陆
        /// 0：验证成功
        /// 1：用户名不正确
        /// 2：SSO统一认证
        /// 3：密码不正确
        /// 4：所属系统停用
        /// </summary>
        /// <param name="UserID">用户登陆ID</param>
        /// <param name="UserPWD">用户登陆密码，在此处进行加密处理后与数据库密码进行比对</param>
        /// <returns></returns>
        public int CheckUserLogin(string UserID, string UserPWD, out SystemUserEntity userInfo)
        {
            return man.CheckUserLogin(UserID, UserPWD, out userInfo);
        }
        #endregion
        #region 日志界面操作方法集合
        /// <summary>
        /// 日志查询
        /// </summary>
        /// <param name="pIndex"></param>
        /// <param name="pNum"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Hashtable LogQuery(int pIndex, int pNum, LogEntity entity)
        {
            return man.LogQuery(pIndex, pNum, entity);
        }
        /// <summary>
        /// 通过ID获取日志详细信息
        /// </summary>
        /// <param name="bid"></param>
        /// <returns></returns>
        public LogEntity GetLogByID(Int64 bid)
        {
            return man.GetLogByID(bid);
        }
        #endregion
    }
}
