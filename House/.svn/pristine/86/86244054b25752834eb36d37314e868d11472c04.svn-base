using House.DataAccess;
using House.Entity;
using House.Entity.House;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace House.Manager.House
{
    /// <summary>
    /// 系统访问数据实现类
    /// </summary>
    public class SystemManager
    {
        private SqlHelper conn = new SqlHelper();

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
            List<SystemItemEntity> result = new List<SystemItemEntity>();
            Hashtable resHT = new Hashtable();
            try
            {
                string strSQL = @" SELECT TOP " + pNum + " * from (select ROW_NUMBER() OVER (ORDER BY b.OP_DATE DESC) AS RowNumber, b.*,c.CName AS ParentCName,c.EName as ParentEName,d.HouseName FROM Tbl_SysItems AS b LEFT JOIN (select ItemID,CName,EName from Tbl_SysItems ) as c on b.ParentID=c.ItemID left join Tbl_SysHouse as d on b.HouseID=d.HouseID WHERE (1=1)";
                if (!string.IsNullOrEmpty(entity.DelFlag))
                {
                    strSQL += " and b.DelFlag = '" + entity.DelFlag + "'";
                }
                if (!entity.HouseID.Equals(0))
                {
                    strSQL += " and b.HouseID=" + entity.HouseID;
                }
                //以中文名称为查询条件
                if (!string.IsNullOrEmpty(entity.CName))
                {
                    strSQL += " and b.CName like '%" + entity.CName + "%'";
                }
                //以父ID为查询条件
                if (!entity.ParentID.Equals(0))
                {
                    strSQL += " and b.ParentID=" + entity.ParentID;
                }
                strSQL += " ) as A where RowNumber > (" + pNum + "* (" + pIndex + "-1))";
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dd = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dd.Rows)
                        {
                            result.Add(new SystemItemEntity
                            {
                                ItemID = Convert.ToInt32(idr["ItemID"]),
                                HouseID = Convert.ToInt32(idr["HouseID"]),
                                HouseCode = Convert.ToString(idr["HouseCode"]).Trim(),
                                HouseName = Convert.ToString(idr["HouseName"]).Trim(),
                                CName = Convert.ToString(idr["CName"]).Trim(),
                                EName = Convert.ToString(idr["EName"]).Trim(),
                                ItemSrc = Convert.ToString(idr["ItemSrc"]).Trim(),
                                ParentID = Convert.ToInt32(idr["ParentID"]),
                                ItemLevel = Convert.ToString(idr["ItemLevel"]).Trim(),
                                ItemSort = Convert.ToString(idr["ItemSort"]).Trim(),
                                DelFlag = Convert.ToString(idr["DelFlag"]).Trim(),
                                ParentCName = Convert.ToString(idr["ParentCName"]).Trim(),
                                ItemIcon = Convert.ToString(idr["ItemIcon"]).Trim(),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"])
                            });
                        }
                    }
                }

                resHT["rows"] = result;

                string strCount = @"Select Count(*) as TotalCount from Tbl_SysItems Where (1=1)";
                if (!string.IsNullOrEmpty(entity.DelFlag))
                {
                    strCount += " and DelFlag = '" + entity.DelFlag + "'";
                }
                if (!entity.HouseID.Equals(0))
                {
                    strCount += " and HouseID=" + entity.HouseID;
                }
                //以中文名称为查询条件
                if (!string.IsNullOrEmpty(entity.CName))
                {
                    strCount += " and CName like '%" + entity.CName + "%'";
                }
                //以父ID为查询条件
                if (!entity.ParentID.Equals(0))
                {
                    strCount += " and ParentID=" + entity.ParentID;
                }
                using (DbCommand cmd = conn.GetSqlStringCommond(strCount))
                {
                    using (DataTable idrCount = conn.ExecuteDataTable(cmd))
                    {
                        if (idrCount.Rows.Count > 0)
                        {
                            resHT["total"] = Convert.ToInt32(idrCount.Rows[0]["TotalCount"]);
                        }
                    }

                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return resHT;
        }
        /// <summary>
        /// 按ItemID查询导航数据
        /// </summary>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        public SystemItemEntity GetSystemItemByID(int ItemID)
        {


            SystemItemEntity result = new SystemItemEntity();
            try
            {
                string strSQL = @"SELECT * from Tbl_SysItems Where ItemID=@ItemID";
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@ItemID", DbType.Int32, ItemID);
                    using (DataTable dt = conn.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            result = new SystemItemEntity
                            {
                                ItemID = Convert.ToInt32(idr["ItemID"]),
                                HouseID = Convert.ToInt32(idr["HouseID"]),
                                HouseCode = Convert.ToString(idr["HouseCode"]).Trim(),
                                CName = Convert.ToString(idr["CName"]).Trim(),
                                EName = Convert.ToString(idr["EName"]).Trim(),
                                ItemSrc = Convert.ToString(idr["ItemSrc"]).Trim(),
                                ParentID = Convert.ToInt32(idr["ParentID"]),
                                ItemIcon = Convert.ToString(idr["ItemIcon"]).Trim(),
                                DelFlag = Convert.ToString(idr["DelFlag"]).Trim()
                            };
                        }
                    }

                }
                return result;
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// 查询一级菜单名
        /// </summary>
        /// <returns></returns>
        public List<SystemItemEntity> ParentItemQuery(int houseID)
        {
            List<SystemItemEntity> result = new List<SystemItemEntity>();
            string strSQL = @"SELECT ItemID,CName,ItemIcon FROM Tbl_SysItems WHERE DelFlag=0 and ParentID=0";
            if (!houseID.Equals(0))
            {
                strSQL += " and HouseID=@HouseID";
            }
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    if (!houseID.Equals(0))
                    {
                        conn.AddInParameter(cmd, "@HouseID", DbType.Int32, houseID);
                    }
                    using (DataTable dt = conn.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idrCount in dt.Rows)
                        {
                            result.Add(new SystemItemEntity
                            {
                                ItemID = Convert.ToInt32(idrCount["ItemID"]),
                                ItemIcon = Convert.ToString(idrCount["ItemIcon"]).Trim(),
                                CName = Convert.ToString(idrCount["CName"]).Trim()
                            });
                        }
                    }

                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
            return result;
        }
        /// <summary>
        /// 新增导航链接
        /// </summary>
        /// <param name="entity"></param>
        public void SystemItemAdd(SystemItemEntity entity)
        {

            string strSQL = @"INSERT INTO Tbl_SysItems(HouseID,HouseCode,CName,EName ,ItemSrc ,ParentID ,ItemLevel ,ItemSort ,DelFlag,ItemIcon) VALUES  (@HouseID,@HouseCode, @CName ,@EName ,@ItemSrc ,@ParentID ,@ItemLevel ,@ItemSort ,@DelFlag,@ItemIcon)";
            try
            {

                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@HouseID", DbType.Int32, entity.HouseID);
                    conn.AddInParameter(cmd, "@HouseCode", DbType.String, entity.HouseCode);
                    conn.AddInParameter(cmd, "@CName", DbType.String, entity.CName);
                    conn.AddInParameter(cmd, "@EName", DbType.String, entity.EName);
                    conn.AddInParameter(cmd, "@ItemSrc", DbType.String, entity.ItemSrc);
                    conn.AddInParameter(cmd, "@ParentID", DbType.Int32, entity.ParentID);
                    conn.AddInParameter(cmd, "@ItemLevel", DbType.String, entity.ItemLevel);
                    conn.AddInParameter(cmd, "@ItemSort", DbType.String, entity.ItemSort);
                    conn.AddInParameter(cmd, "@DelFlag", DbType.String, entity.DelFlag);
                    conn.AddInParameter(cmd, "@ItemIcon", DbType.String, entity.ItemIcon);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 修改导航链接数据
        /// </summary>
        /// <param name="entity"></param>
        public void SystemItemUpdate(SystemItemEntity entity)
        {


            string strSQL = @"UPDATE Tbl_SysItems SET CName=@CName,EName=@EName,ParentID=@ParentID,ItemSrc=@ItemSrc,DelFlag=@DelFlag,ItemLevel=@ItemLevel,OP_DATE=@OP_DATE,ItemIcon=@ItemIcon,HouseID=@HouseID,HouseCode=@HouseCode WHERE ItemID=@ItemID";

            try
            {

                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@HouseID", DbType.Int32, entity.HouseID);
                    conn.AddInParameter(cmd, "@HouseCode", DbType.String, entity.HouseCode);
                    conn.AddInParameter(cmd, "@CName", DbType.String, entity.CName);
                    conn.AddInParameter(cmd, "@EName", DbType.String, entity.EName);
                    conn.AddInParameter(cmd, "@ItemSrc", DbType.String, entity.ItemSrc);
                    conn.AddInParameter(cmd, "@ParentID", DbType.Int32, entity.ParentID);
                    conn.AddInParameter(cmd, "@DelFlag", DbType.String, entity.DelFlag);
                    conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                    conn.AddInParameter(cmd, "@ItemID", DbType.Int32, entity.ItemID);
                    conn.AddInParameter(cmd, "@ItemLevel", DbType.String, entity.ItemLevel);
                    conn.AddInParameter(cmd, "@ItemIcon", DbType.String, entity.ItemIcon);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 删除导航链接
        /// </summary>
        /// <param name="entity"></param>
        public void SystemItemDel(List<SystemItemEntity> entity)
        {

            try
            {
                foreach (var it in entity)
                {
                    string strSQL = @"UPDATE Tbl_SysItems SET DelFlag='1' WHERE ItemID=@ItemID";
                    if (it.DelFlag.Equals("1"))//彻底删除
                    {
                        strSQL = @"Delete from Tbl_SysItems where ItemID=@ItemID ";
                    }
                    using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                    {
                        conn.AddInParameter(cmd, "@ItemID", DbType.Int32, it.ItemID);
                        conn.ExecuteNonQuery(cmd);
                    }
                    //删除角色与导航关联数据表
                    if (it.DelFlag.Equals("1"))
                    {
                        DelRoleItem(it.ItemID);
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 查询所有导航链接并格式化
        /// </summary>
        public List<SystemItemEntity> QueryItemFormat(int hid)
        {

            List<SystemItemEntity> result = new List<SystemItemEntity>();
            string strSQL = @"SELECT ItemID,CName,ItemSrc,ParentID,ItemIcon FROM Tbl_SysItems Where DelFlag=0 ";
            strSQL += " and HouseID=@HouseID";
            strSQL += " ORDER BY ItemLevel ASC,ItemSort ASC ";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@HouseID", DbType.Int32, hid);
                    using (DataTable dt = conn.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idrCount in dt.Rows)
                        {
                            result.Add(new SystemItemEntity
                            {
                                ItemID = Convert.ToInt32(idrCount["ItemID"]),
                                ItemSrc = Convert.ToString(idrCount["ItemSrc"]).Trim(),
                                ParentID = Convert.ToInt32(idrCount["ParentID"]),
                                CName = Convert.ToString(idrCount["CName"]).Trim(),
                                ItemIcon = Convert.ToString(idrCount["ItemIcon"]).Trim()
                            });
                        }
                    }
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
            return result;
        }
        /// <summary>
        /// 按用户登陆名查询所有导航链接并格式化
        /// </summary>
        public List<SystemItemEntity> QueryItemFormat(string LoginName, string HouseCode)
        {

            List<SystemItemEntity> result = new List<SystemItemEntity>();
            string strSQL = @"SELECT a.LoginName,a.UserName,c.ItemID,c.CName,c.ParentID,c.ItemSort,c.ItemSrc,c.ItemIcon,c.HouseCode,d.Remark as HouseRemark FROM Tbl_SysUser AS a LEFT JOIN Tbl_SysRoleItem AS b ON a.RoleID=b.RoleID LEFT JOIN Tbl_SysItems AS c ON b.ItemID=c.ItemID and c.HouseCode=@HouseCode left join Tbl_SysHouse as d on c.HouseID=d.HouseID WHERE a.LoginName=@LoginName AND c.DelFlag=0 ORDER BY c.ItemLevel ASC,c.ItemSort ASC  ";

            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@LoginName", DbType.String, LoginName.Trim());
                    conn.AddInParameter(cmd, "@HouseCode", DbType.String, HouseCode.Trim());

                    using (DataTable dt = conn.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idrCount in dt.Rows)
                        {
                            result.Add(new SystemItemEntity
                            {
                                ItemID = Convert.ToInt32(idrCount["ItemID"]),
                                ItemSrc = Convert.ToString(idrCount["ItemSrc"]).Trim(),
                                ParentID = Convert.ToInt32(idrCount["ParentID"]),
                                CName = Convert.ToString(idrCount["CName"]).Trim(),
                                HouseCode = Convert.ToString(idrCount["HouseCode"]).Trim(),
                                HouseRemark = Convert.ToString(idrCount["HouseRemark"]).Trim(),
                                ItemIcon = Convert.ToString(idrCount["ItemIcon"]).Trim()
                            });
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }

        /// <summary>
        /// 修改更新导航链接的排序
        /// </summary>
        /// <param name="entity"></param>
        public void SystemItemSortUpdate(List<SystemItemEntity> entity)
        {


            try
            {
                foreach (var it in entity)
                {
                    string strSQL = @"UPDATE Tbl_SysItems SET ItemSort=@ItemSort WHERE ItemID=@ItemID";
                    using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                    {
                        conn.AddInParameter(cmd, "@ItemID", DbType.Int32, it.ItemID);
                        conn.AddInParameter(cmd, "@ItemSort", DbType.String, it.ItemSort);

                        conn.ExecuteNonQuery(cmd);

                    }
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        #endregion
        #region 系统管理界面操作方法集合
        /// <summary>
        /// 单位查询
        /// </summary>
        /// <param name="pIndex">当前页数</param>
        /// <param name="pNum">每页多少条数据</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="strCountWhere">查询总数的查询条件</param>
        /// <returns></returns>
        public Hashtable QueryHouse(int pIndex, int pNum, SystemSetEntity entity)
        {
            List<SystemSetEntity> result = new List<SystemSetEntity>();

            Hashtable resHT = new Hashtable();
            try
            {
                string strSQL = @" SELECT TOP " + pNum + " * FROM (select ROW_NUMBER() OVER (ORDER BY HouseID DESC) AS RowNumber,* from Tbl_SysHouse WHERE (1=1)";
                if (!string.IsNullOrEmpty(entity.DelFlag)) { strSQL += " and DelFlag = '" + entity.DelFlag + "'"; }
                //以系统名称为查询条件
                if (!string.IsNullOrEmpty(entity.HouseName))
                {
                    strSQL += " and HouseName like '%" + entity.HouseName + "%'";
                }
                if (!string.IsNullOrEmpty(entity.HouseCode)) { strSQL += " and HouseCode = '" + entity.HouseCode + "'"; }
                strSQL += " ) as A where RowNumber > (" + pNum + "* (" + pIndex + "-1))";
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            result.Add(new SystemSetEntity
                            {
                                HouseID = Convert.ToInt32(idr["HouseID"]),
                                HouseName = Convert.ToString(idr["HouseName"]),
                                HouseCode = Convert.ToString(idr["HouseCode"]).Trim(),
                                Cellphone = Convert.ToString(idr["Cellphone"]).Trim(),
                                Email = Convert.ToString(idr["Email"]).Trim(),
                                Person = Convert.ToString(idr["Person"]).Trim(),
                                Remark = Convert.ToString(idr["Remark"]).Trim(),
                                DelFlag = Convert.ToString(idr["DelFlag"]).Trim(),
                                QQ = Convert.ToString(idr["QQ"]).Trim(),
                                Weixin = Convert.ToString(idr["Weixin"]).Trim(),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"])
                            });
                        }
                    }
                }
                resHT["rows"] = result;
                string strCount = @"Select Count(*) as TotalCount from Tbl_SysHouse Where (1=1)";
                if (!string.IsNullOrEmpty(entity.DelFlag)) { strCount += " and DelFlag = '" + entity.DelFlag + "'"; }
                //以系统名称为查询条件
                if (!string.IsNullOrEmpty(entity.HouseName)) { strCount += " and HouseName like '%" + entity.HouseName + "%'"; }
                if (!string.IsNullOrEmpty(entity.HouseCode)) { strCount += " and HouseCode = '" + entity.HouseCode + "'"; }
                using (DbCommand cmd = conn.GetSqlStringCommond(strCount))
                {
                    using (DataTable idrCount = conn.ExecuteDataTable(cmd))
                    {
                        if (idrCount.Rows.Count > 0)
                        {
                            resHT["total"] = Convert.ToInt32(idrCount.Rows[0]["TotalCount"]);
                        }
                    }
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
            return resHT;
        }
        /// <summary>
        /// 新增单位数据
        /// </summary>
        /// <param name="entity"></param>
        public void AddHouse(SystemSetEntity entity)
        {
            string strSQL = @"INSERT INTO Tbl_SysHouse(HouseName,HouseCode,Person,Cellphone,QQ,Weixin,Email,Remark,DelFlag) VALUES (@HouseName,@HouseCode,@Person,@Cellphone,@QQ,@Weixin,@Email,@Remark,@DelFlag)";
            try
            {

                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@HouseName", DbType.String, entity.HouseName);
                    conn.AddInParameter(cmd, "@HouseCode", DbType.String, entity.HouseCode);
                    conn.AddInParameter(cmd, "@Person", DbType.String, entity.Person);
                    conn.AddInParameter(cmd, "@Cellphone", DbType.String, entity.Cellphone);
                    conn.AddInParameter(cmd, "@QQ", DbType.String, entity.QQ);
                    conn.AddInParameter(cmd, "@Weixin", DbType.String, entity.Weixin);
                    conn.AddInParameter(cmd, "@Email", DbType.String, entity.Email);
                    conn.AddInParameter(cmd, "@Remark", DbType.String, entity.Remark);
                    conn.AddInParameter(cmd, "@DelFlag", DbType.String, entity.DelFlag);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// 删除单位数据
        /// </summary>
        /// <param name="entity"></param>
        public void DelHouse(List<SystemSetEntity> entity)
        {
            try
            {
                foreach (var it in entity)
                {
                    string strSQL = @"UPDATE Tbl_SysHouse SET DelFlag='1' WHERE HouseID=@HouseID";
                    if (it.DelFlag.Equals("1"))//彻底删除
                    {
                        strSQL = @"Delete from Tbl_SysHouse where HouseID=@HouseID ";
                    }
                    using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                    {
                        conn.AddInParameter(cmd, "@HouseID", DbType.Int32, it.HouseID);
                        conn.ExecuteNonQuery(cmd);
                    }
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// 按ItemID查询单位数据
        /// </summary>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        public SystemUnitEntity GetUnitByID(int UnitID)
        {

            SystemUnitEntity result = new SystemUnitEntity();
            try
            {
                string strSQL = @"SELECT * from Tbl_SysUnit Where UnitID=@UnitID";
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@UnitID", DbType.Int32, UnitID);
                    using (DataTable dt = conn.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            result = new SystemUnitEntity
                            {
                                UnitID = Convert.ToInt32(idr["UnitID"]),
                                FirmID = Convert.ToInt32(idr["FirmID"]),
                                CName = Convert.ToString(idr["CName"]).Trim(),
                                Address = Convert.ToString(idr["Address"]).Trim(),
                                Boss = Convert.ToString(idr["Boss"]).Trim(),
                                phone = Convert.ToString(idr["phone"]).Trim(),
                                Remark = Convert.ToString(idr["Remark"]).Trim(),
                                CellPhone = Convert.ToString(idr["CellPhone"]).Trim(),
                                DelFlag = Convert.ToString(idr["DelFlag"]).Trim()
                            };
                        }
                    }
                }
                return result;
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        /// <summary>
        /// 修改导航单位数据
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateHouse(SystemSetEntity entity)
        {
            string strSQL = @"UPDATE Tbl_SysHouse SET HouseName=@HouseName,HouseCode=@HouseCode,Person=@Person,Cellphone=@Cellphone,QQ=@QQ,Weixin=@Weixin,Remark=@Remark,DelFlag=@DelFlag,Email=@Email WHERE HouseID=@HouseID";
            try
            {

                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@HouseName", DbType.String, entity.HouseName);
                    conn.AddInParameter(cmd, "@HouseCode", DbType.String, entity.HouseCode);
                    conn.AddInParameter(cmd, "@Person", DbType.String, entity.Person);
                    conn.AddInParameter(cmd, "@Cellphone", DbType.String, entity.Cellphone);
                    conn.AddInParameter(cmd, "@QQ", DbType.String, entity.QQ);
                    conn.AddInParameter(cmd, "@Weixin", DbType.String, entity.Weixin);
                    conn.AddInParameter(cmd, "@Email", DbType.String, entity.Email);
                    conn.AddInParameter(cmd, "@Remark", DbType.String, entity.Remark);
                    conn.AddInParameter(cmd, "@DelFlag", DbType.String, entity.DelFlag);
                    conn.AddInParameter(cmd, "@HouseID", DbType.Int32, entity.HouseID);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// 查询所有系统
        /// </summary>
        /// <returns></returns>
        public List<SystemSetEntity> QueryALLHouse()
        {

            List<SystemSetEntity> result = new List<SystemSetEntity>();
            string strSQL = @"SELECT HouseID,HouseName,HouseCode FROM Tbl_SysHouse WHERE DelFlag=0";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = conn.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idrCount in dt.Rows)
                        {
                            result.Add(new SystemSetEntity
                            {
                                HouseID = Convert.ToInt32(idrCount["HouseID"]),
                                HouseName = Convert.ToString(idrCount["HouseName"]).Trim(),
                                HouseCode = Convert.ToString(idrCount["HouseCode"]).Trim()
                            });
                        }
                    }

                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
            return result;
        }
        /// <summary>
        /// 判断系统代码是否存在 true:表示存在
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>true:表示存在</returns>
        public bool IsExistHouse(SystemSetEntity entity)
        {
            string strQ = @"Select HouseCode from Tbl_SysHouse Where HouseCode=@HouseCode and DelFlag=0";
            using (DbCommand cmdQ = conn.GetSqlStringCommond(strQ))
            {
                conn.AddInParameter(cmdQ, "@HouseCode", DbType.String, entity.HouseCode);
                using (DataTable idr = conn.ExecuteDataTable(cmdQ))
                {
                    if (idr == null)
                    {
                        return false;
                    }
                    if (idr.Rows.Count <= 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion
        #region 部门界面操作方法集合

        /// <summary>
        /// 部门查询
        /// </summary>
        /// <param name="pIndex">当前页数</param>
        /// <param name="pNum">每页多少条数据</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="strCountWhere">查询总数的查询条件</param>
        /// <returns></returns>
        public Hashtable QueryDepart(int pIndex, int pNum, SystemDepartEntity entity)
        {
            List<SystemDepartEntity> result = new List<SystemDepartEntity>();


            Hashtable resHT = new Hashtable();
            try
            {

                string strSQL = @" SELECT TOP " + pNum + " * from (select ROW_NUMBER() OVER (ORDER BY b.DepID DESC) AS RowNumber , b.*,c.CName AS UnitName FROM Tbl_SysDepart AS b LEFT JOIN Tbl_SysUnit AS c ON b.UnitID=c.UnitID WHERE 1=1";
                if (!string.IsNullOrEmpty(entity.DelFlag))
                {
                    strSQL += " and b.DelFlag = '" + entity.DelFlag + "'";
                }
                //以中文名称为查询条件
                if (!string.IsNullOrEmpty(entity.CName))
                {
                    strSQL += " and b.CName like '%" + entity.CName + "%'";
                }
                //以所属单位ID为查询条件
                if (!entity.UnitID.Equals(0))
                {
                    strSQL += " and b.UnitID=" + entity.UnitID;
                }
                strSQL += " ) as A where RowNumber > (" + pNum + "* (" + pIndex + "-1))";

                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            result.Add(new SystemDepartEntity
                            {
                                DepID = Convert.ToInt32(idr["DepID"]),
                                UnitID = Convert.ToInt32(idr["UnitID"]),
                                CName = Convert.ToString(idr["CName"]).Trim(),
                                UnitName = Convert.ToString(idr["UnitName"]).Trim(),
                                Remark = Convert.ToString(idr["Remark"]).Trim(),
                                DelFlag = Convert.ToString(idr["DelFlag"]).Trim(),
                                People = Convert.ToString(idr["People"]).Trim(),
                                Address = Convert.ToString(idr["Address"]).Trim(),
                                Telephone = Convert.ToString(idr["Telephone"]).Trim(),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"])
                            });
                        }
                    }
                }
                resHT["rows"] = result;
                string strCount = @"Select Count(*) as TotalCount from Tbl_SysDepart Where (1=1)";
                if (!string.IsNullOrEmpty(entity.DelFlag))
                {
                    strCount += " and DelFlag = '" + entity.DelFlag + "'";
                }
                //以中文名称为查询条件
                if (!string.IsNullOrEmpty(entity.CName))
                {
                    strCount += " and CName like '%" + entity.CName + "%'";
                }
                //以所属单位ID为查询条件
                if (!entity.UnitID.Equals(0))
                {
                    strCount += " and UnitID=" + entity.UnitID;
                }
                using (DbCommand cmd = conn.GetSqlStringCommond(strCount))
                {
                    using (DataTable idrCount = conn.ExecuteDataTable(cmd))
                    {
                        if (idrCount.Rows.Count > 0)
                        {
                            resHT["total"] = Convert.ToInt32(idrCount.Rows[0]["TotalCount"]);
                        }
                    }

                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
            return resHT;
        }
        /// <summary>
        /// 新增部门数据
        /// </summary>
        /// <param name="entity"></param>
        public int AddDepart(SystemDepartEntity entity)
        {
            int result = 0;

            string strSQL = @"INSERT INTO Tbl_SysDepart(CName,UnitID,Remark,DelFlag,People,Address,Telephone) VALUES (@CName,@UnitID,@Remark,@DelFlag,@People,@Address,@Telephone) SELECT @@IDENTITY AS returnID";
            try
            {

                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@CName", DbType.String, entity.CName);
                    conn.AddInParameter(cmd, "@UnitID", DbType.Int32, entity.UnitID);
                    conn.AddInParameter(cmd, "@Remark", DbType.String, entity.Remark);
                    conn.AddInParameter(cmd, "@DelFlag", DbType.String, entity.DelFlag);
                    conn.AddInParameter(cmd, "@People", DbType.String, entity.People);
                    conn.AddInParameter(cmd, "@Address", DbType.String, entity.Address);
                    conn.AddInParameter(cmd, "@Telephone", DbType.String, entity.Telephone);

                    result = Convert.ToInt32(conn.ExecuteScalar(cmd));

                }
                return result;
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// 删除部门数据
        /// </summary>
        /// <param name="entity"></param>
        public void DelDepart(List<SystemDepartEntity> entity)
        {


            try
            {
                foreach (var it in entity)
                {
                    string strSQL = @" UPDATE Tbl_SysDepart SET DelFlag='1' WHERE DepID=@DepID";
                    using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                    {
                        conn.AddInParameter(cmd, "@DepID", DbType.Int32, it.DepID);
                        conn.ExecuteNonQuery(cmd);
                    }
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        /// <summary>
        /// 按DepID查询部门数据
        /// </summary>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        public SystemDepartEntity GetDepartByID(int DepID)
        {

            SystemDepartEntity result = new SystemDepartEntity();
            try
            {
                string strSQL = @"SELECT * from Tbl_SysDepart Where DepID=@DepID";
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@DepID", DbType.Int32, DepID);
                    using (DataTable dt = conn.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            result = new SystemDepartEntity
                            {
                                DepID = Convert.ToInt32(idr["DepID"]),
                                UnitID = Convert.ToInt32(idr["UnitID"]),
                                CName = Convert.ToString(idr["CName"]).Trim(),
                                Remark = Convert.ToString(idr["Remark"]).Trim(),
                                DelFlag = Convert.ToString(idr["DelFlag"]).Trim(),
                                People = Convert.ToString(idr["People"]).Trim(),
                                Address = Convert.ToString(idr["Address"]).Trim(),
                                Telephone = Convert.ToString(idr["Telephone"]).Trim()
                            };
                        }
                    }

                }
                return result;
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        /// <summary>
        /// 修改导航部门数据
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateDepart(SystemDepartEntity entity)
        {


            string strSQL = @"UPDATE Tbl_SysDepart SET CName=@CName,UnitID=@UnitID,DelFlag=@DelFlag,People=@People,Address=@Address,Telephone=@Telephone,OP_DATE=@OP_DATE WHERE DepID=@DepID";

            try
            {


                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@CName", DbType.String, entity.CName);
                    conn.AddInParameter(cmd, "@UnitID", DbType.String, entity.UnitID);
                    conn.AddInParameter(cmd, "@Remark", DbType.String, entity.Remark);
                    conn.AddInParameter(cmd, "@DelFlag", DbType.String, entity.DelFlag);
                    conn.AddInParameter(cmd, "@Telephone", DbType.String, entity.Telephone);
                    conn.AddInParameter(cmd, "@Address", DbType.String, entity.Address);
                    conn.AddInParameter(cmd, "@People", DbType.String, entity.People);
                    conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                    conn.AddInParameter(cmd, "@DepID", DbType.Int32, entity.DepID);

                    conn.ExecuteNonQuery(cmd);

                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }

        }
        /// <summary>
        /// 根据单位ID查询该单位下的所有部门数据
        /// </summary>
        /// <param name="UnitID"></param>
        /// <returns></returns>
        public List<SystemDepartEntity> GetDeptByUnitID(string UnitID, int DepID)
        {

            List<SystemDepartEntity> result = new List<SystemDepartEntity>();
            string strSQL = @"SELECT DepID,CName,People,Telephone,Address FROM Tbl_SysDepart WHERE DelFlag=0";
            if (!string.IsNullOrEmpty(UnitID)) { strSQL += " and UnitID in (" + UnitID + ")"; }
            if (!DepID.Equals(0)) { strSQL += " and DepID =" + DepID + ""; }
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = conn.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idrCount in dt.Rows)
                        {
                            result.Add(new SystemDepartEntity
                            {
                                DepID = Convert.ToInt32(idrCount["DepID"]),
                                CName = Convert.ToString(idrCount["CName"]).Trim(),
                                People = Convert.ToString(idrCount["People"]).Trim(),
                                Address = Convert.ToString(idrCount["Address"]).Trim(),
                                Telephone = Convert.ToString(idrCount["Telephone"]).Trim()
                            });
                        }
                    }

                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
            return result;

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
            List<SystemRoleEntity> result = new List<SystemRoleEntity>();
            Hashtable resHT = new Hashtable();
            try
            {

                string strSQL = @" SELECT TOP " + pNum + " * from (select ROW_NUMBER() OVER (ORDER BY RoleID DESC) AS RowNumber,* FROM Tbl_SysRole WHERE 1=1 ";
                if (!string.IsNullOrEmpty(entity.DelFlag))
                {
                    strSQL += " and DelFlag = '" + entity.DelFlag + "'";
                }
                //以中文名称为查询条件
                if (!string.IsNullOrEmpty(entity.CName))
                {
                    strSQL += " and CName like '%" + entity.CName + "%'";
                }
                strSQL += " ) as A where RowNumber > (" + pNum + "* (" + pIndex + "-1))";
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            result.Add(new SystemRoleEntity
                            {
                                RoleID = Convert.ToInt32(idr["RoleID"]),
                                CName = Convert.ToString(idr["CName"]).Trim(),
                                IsAdmin = Convert.ToString(idr["IsAdmin"]).Trim(),
                                Remark = Convert.ToString(idr["Remark"]).Trim(),
                                DelFlag = Convert.ToString(idr["DelFlag"]).Trim(),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"])
                            });
                        }
                    }
                }
                resHT["rows"] = result;
                string strCount = @"Select Count(*) as TotalCount from Tbl_SysRole Where (1=1)";
                if (!string.IsNullOrEmpty(entity.DelFlag))
                {
                    strCount += " and DelFlag = '" + entity.DelFlag + "'";
                }
                //以中文名称为查询条件
                if (!string.IsNullOrEmpty(entity.CName))
                {
                    strCount += " and CName like '%" + entity.CName + "%'";
                }
                using (DbCommand cmd = conn.GetSqlStringCommond(strCount))
                {
                    using (DataTable idrCount = conn.ExecuteDataTable(cmd))
                    {
                        if (idrCount.Rows.Count > 0)
                        {
                            resHT["total"] = Convert.ToInt32(idrCount.Rows[0]["TotalCount"]);
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return resHT;
        }
        /// <summary>
        /// 新增角色数据
        /// </summary>
        /// <param name="entity"></param>
        public void AddRole(SystemRoleEntity entity)
        {
            string strSQL = @"INSERT INTO Tbl_SysRole(CName,IsAdmin,Remark,DelFlag) VALUES (@CName,@IsAdmin,@Remark,@DelFlag)";
            try
            {

                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@CName", DbType.String, entity.CName);
                    conn.AddInParameter(cmd, "@IsAdmin", DbType.String, entity.IsAdmin);
                    conn.AddInParameter(cmd, "@Remark", DbType.String, entity.Remark);
                    conn.AddInParameter(cmd, "@DelFlag", DbType.String, entity.DelFlag);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 删除角色数据
        /// </summary>
        /// <param name="entity"></param>
        public void DelRole(List<SystemRoleEntity> entity)
        {
            try
            {
                foreach (var it in entity)
                {
                    string strSQL = @"UPDATE Tbl_SysRole SET DelFlag='1' WHERE RoleID=@RoleID";
                    if (it.DelFlag.Equals("1"))//彻底删除
                    {
                        strSQL = @"Delete from Tbl_SysRole where RoleID=@RoleID ";
                    }
                    using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                    {
                        conn.AddInParameter(cmd, "@RoleID", DbType.Int32, it.RoleID);
                        conn.ExecuteNonQuery(cmd);
                    }
                    //删除角色与导航的关联数据
                    if (it.DelFlag.Equals("1"))
                    {
                        DelRoleItem(it);
                    }
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// 按RoleID查询角色数据
        /// </summary>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        public SystemRoleEntity GetRoleByID(int RoleID)
        {
            SystemRoleEntity result = new SystemRoleEntity();
            try
            {
                string strSQL = @"SELECT * from Tbl_SysRole Where RoleID=@RoleID";
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@RoleID", DbType.Int32, RoleID);
                    using (DataTable dt = conn.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            result = new SystemRoleEntity
                            {
                                RoleID = Convert.ToInt32(idr["RoleID"]),
                                CName = Convert.ToString(idr["CName"]).Trim(),
                                IsAdmin = Convert.ToString(idr["IsAdmin"]).Trim(),
                                Remark = Convert.ToString(idr["Remark"]).Trim(),
                                DelFlag = Convert.ToString(idr["DelFlag"]).Trim()
                            };
                        }
                    }

                }
                return result;
            }
            catch (ApplicationException ex)
            {

                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// 修改角色数据
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateRole(SystemRoleEntity entity)
        {
            string strSQL = @"UPDATE Tbl_SysRole SET CName=@CName,IsAdmin=@IsAdmin,Remark=@Remark,DelFlag=@DelFlag WHERE RoleID=@RoleID";
            try
            {

                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@CName", DbType.String, entity.CName);
                    conn.AddInParameter(cmd, "@IsAdmin", DbType.String, entity.IsAdmin);
                    conn.AddInParameter(cmd, "@Remark", DbType.String, entity.Remark);
                    conn.AddInParameter(cmd, "@DelFlag", DbType.String, entity.DelFlag);
                    conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                    conn.AddInParameter(cmd, "@RoleID", DbType.Int32, entity.RoleID);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 查询所有角色名
        /// </summary>
        /// <returns></returns>
        public List<SystemRoleEntity> QueryALLRole()
        {
            List<SystemRoleEntity> result = new List<SystemRoleEntity>();
            string strSQL = @"SELECT RoleID,CName FROM Tbl_SysRole WHERE DelFlag=0";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = conn.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idrCount in dt.Rows)
                        {
                            result.Add(new SystemRoleEntity
                            {
                                RoleID = Convert.ToInt32(idrCount["RoleID"]),
                                CName = Convert.ToString(idrCount["CName"]).Trim()
                            });
                        }
                    }
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
            return result;
        }
        #endregion
        #region 角色导航关联表操作方法集合
        /// <summary>
        /// 新增和修改均采用此方法
        /// </summary>
        /// <param name="entity"></param>
        public void AddRoleItem(List<SystemRoleItemEntity> entity)
        {

            try
            {
                string strDel = @"DELETE FROM Tbl_SysRoleItem WHERE RoleID=@RoleID";
                using (DbCommand cmd = conn.GetSqlStringCommond(strDel))
                {
                    conn.AddInParameter(cmd, "@RoleID", DbType.Int32, entity[0].RoleID);
                    conn.ExecuteNonQuery(cmd);
                }

                foreach (var it in entity)
                {
                    string strAdd = @"INSERT INTO Tbl_SysRoleItem(RoleID,ItemID) VALUES (@RoleID,@ItemID)";
                    using (DbCommand cmdAdd = conn.GetSqlStringCommond(strAdd))
                    {
                        conn.AddInParameter(cmdAdd, "@RoleID", DbType.Int32, it.RoleID);
                        conn.AddInParameter(cmdAdd, "@ItemID", DbType.Int32, it.ItemID);
                        conn.ExecuteNonQuery(cmdAdd);
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 通过RoleID获取角色所具有的导航权限信息
        /// </summary>
        /// <param name="RoleID"></param>
        /// <returns></returns>
        public List<SystemRoleItemEntity> GetItemByRoleID(int RoleID)
        {
            List<SystemRoleItemEntity> result = new List<SystemRoleItemEntity>();


            try
            {
                string strDel = @"SELECT * FROM Tbl_SysRoleItem WHERE RoleID=@RoleID and ItemID not in (select ItemID from Tbl_SysItems where ParentID=0 and DelFlag=0)";
                using (DbCommand cmd = conn.GetSqlStringCommond(strDel))
                {
                    conn.AddInParameter(cmd, "@RoleID", DbType.Int32, RoleID);
                    DataTable dt = conn.ExecuteDataTable(cmd);
                    if (dt != null)
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            result.Add(new SystemRoleItemEntity
                            {
                                ItemID = Convert.ToInt32(idr["ItemID"]),
                                RoleID = Convert.ToInt32(idr["RoleID"]),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"]),
                            });
                        }
                    }
                }
                return result;
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 删除角色的关联导航数据
        /// </summary>
        /// <param name="entity"></param>
        private void DelRoleItem(SystemRoleEntity entity)
        {
            string strSQL = @"delete from Tbl_SysRoleItem where RoleID=@RoleID ";

            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@RoleID", DbType.Int32, entity.RoleID);
                conn.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 根据导航ID删除角色导航表数据
        /// </summary>
        /// <param name="itemID"></param>
        private void DelRoleItem(int itemID)
        {
            string strSQL = @"delete from Tbl_SysRoleItem where ItemID=@ItemID ";

            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@ItemID", DbType.Int32, itemID);
                conn.ExecuteNonQuery(cmd);
            }
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
            List<SystemUserEntity> result = new List<SystemUserEntity>();

            Hashtable resHT = new Hashtable();
            try
            {
                //(" + pNum + "*(" + pIndex + "-1)

                string strSQL = @" SELECT TOP " + pNum + " * from (select ROW_NUMBER() OVER (ORDER BY b.OP_DATE DESC) AS RowNumber, b.*,c.CName AS RoleCName,e.Name AS DepCName FROM  Tbl_SysUser AS b  LEFT JOIN Tbl_SysRole AS c ON b.RoleID=c.RoleID  LEFT JOIN Tbl_SysOrganize AS e ON b.DepID=e.ID  where (1=1)";
                if (!string.IsNullOrEmpty(entity.DelFlag))
                {
                    strSQL += " and b.DelFlag = '" + entity.DelFlag + "'";
                }
                //以中文名称为查询条件
                if (!string.IsNullOrEmpty(entity.LoginName))
                {
                    strSQL += " and b.LoginName like '%" + entity.LoginName + "%'";
                }
                if (!string.IsNullOrEmpty(entity.UserName))
                {
                    strSQL += " and b.UserName like '%" + entity.UserName + "%'";
                }
                if (!entity.DepID.Equals(0))
                {
                    strSQL += " and b.DepID = " + entity.DepID + "";
                }
                if (!entity.RoleID.Equals(0))
                {
                    strSQL += " and b.RoleID = " + entity.RoleID + "";
                }
                if (!string.IsNullOrEmpty(entity.CellPhone))
                {
                    strSQL += " and b.CellPhone like '%" + entity.CellPhone + "%'";
                }
                strSQL += " ) as A where RowNumber > (" + pNum + "* (" + pIndex + "-1))";
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            result.Add(new SystemUserEntity
                            {
                                UserID = Convert.ToInt32(idr["UserID"]),
                                LoginName = Convert.ToString(idr["LoginName"]).Trim(),
                                UserName = Convert.ToString(idr["UserName"]).Trim(),
                                Sex = Convert.ToString(idr["Sex"]).Trim(),
                                Age = Convert.ToInt32(idr["Age"]),
                                UserIDNum = Convert.ToString(idr["UserIDNum"]).Trim(),
                                RoleCName = Convert.ToString(idr["RoleCName"]).Trim(),
                                RoleID = Convert.ToInt32(idr["RoleID"]),
                                DepCName = Convert.ToString(idr["DepCName"]).Trim(),
                                DepID = Convert.ToInt32(idr["DepID"]),
                                UserJob = Convert.ToString(idr["UserJob"]).Trim(),
                                AddressPhone = Convert.ToString(idr["AddressPhone"]).Trim(),
                                CompanyPhone = Convert.ToString(idr["CompanyPhone"]).Trim(),
                                CellPhone = Convert.ToString(idr["CellPhone"]).Trim(),
                                Email = Convert.ToString(idr["Email"]).Trim(),
                                IPAddress = Convert.ToString(idr["IPAddress"]).Trim(),
                                CargoPermisID = Convert.ToString(idr["CargoPermisID"]),
                                CargoPermisName = Convert.ToString(idr["CargoPermisName"]),
                                HouseID = Convert.ToInt32(idr["HouseID"]),
                                HouseName = Convert.ToString(idr["HouseName"]),
                                LastLoginTime = string.IsNullOrEmpty(Convert.ToString(idr["LastLoginTime"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["LastLoginTime"]),
                                DelFlag = Convert.ToString(idr["DelFlag"]).Trim(),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"])
                            });
                        }
                    }
                }

                resHT["rows"] = result;

                string strCount = @"Select Count(*) as TotalCount from Tbl_SysUser Where (1=1)";
                if (!string.IsNullOrEmpty(entity.DelFlag))
                {
                    strCount += " and DelFlag = '" + entity.DelFlag + "'";
                }
                //以中文名称为查询条件
                if (!string.IsNullOrEmpty(entity.LoginName))
                {
                    strCount += " and LoginName like '%" + entity.LoginName + "%'";
                }

                if (!entity.DepID.Equals(0))
                {
                    strCount += " and DepID = " + entity.DepID + "";
                }
                if (!entity.RoleID.Equals(0))
                {
                    strCount += " and RoleID = " + entity.RoleID + "";
                }
                if (!string.IsNullOrEmpty(entity.CellPhone))
                {
                    strCount += " and CellPhone like '%" + entity.CellPhone + "%'";
                }
                using (DbCommand cmd = conn.GetSqlStringCommond(strCount))
                {
                    using (DataTable idrCount = conn.ExecuteDataTable(cmd))
                    {
                        if (idrCount.Rows.Count > 0)
                        {
                            resHT["total"] = Convert.ToInt32(idrCount.Rows[0]["TotalCount"]);
                        }
                    }

                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
            return resHT;
        }
        /// <summary>
        /// 新增用户数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>1:用户名已经存在</returns>
        public int AddUsers(SystemUserEntity entity)
        {
            if (IsExistUser(entity))
            {
                return 1;
            }
            entity.EnSafe();
            string strSQL = @"INSERT INTO Tbl_SysUser(LoginName,LoginPwd,UserName,Sex,Age ,RoleID ,DepID,UserJob,AddressPhone,CompanyPhone,UserIDNum ,CellPhone ,Email ,";
            if ((entity.LastLoginTime.ToString("yyyy-MM-dd") != "0001-01-01" && entity.LastLoginTime.ToString("yyyy-MM-dd") != "1900-01-01"))
            {
                strSQL += " LastLoginTime ,";
            }
            strSQL += "IPAddress ,DelFlag,CargoPermisID,CargoPermisName,HouseID,HouseName)";
            strSQL += " VALUES ";
            strSQL += "(@LoginName ,@LoginPwd ,@UserName ,@Sex ,@Age ,@RoleID,@DepID,@UserJob ,@AddressPhone ,@CompanyPhone,@UserIDNum ,@CellPhone ,@Email ,";

            if ((entity.LastLoginTime.ToString("yyyy-MM-dd") != "0001-01-01" && entity.LastLoginTime.ToString("yyyy-MM-dd") != "1900-01-01"))
            {
                strSQL += " @LastLoginTime ,";
            }
            strSQL += "@IPAddress,@DelFlag,@CargoPermisID,@CargoPermisName,@HouseID,@HouseName)";

            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@LoginPwd", DbType.String, entity.LoginPwd);
                    conn.AddInParameter(cmd, "@UserName", DbType.String, entity.UserName);
                    conn.AddInParameter(cmd, "@LoginName", DbType.String, entity.LoginName);
                    conn.AddInParameter(cmd, "@Sex", DbType.String, entity.Sex);
                    conn.AddInParameter(cmd, "@Age", DbType.Int32, entity.Age);
                    conn.AddInParameter(cmd, "@UserIDNum", DbType.String, entity.UserIDNum);
                    conn.AddInParameter(cmd, "@RoleID", DbType.Int32, entity.RoleID);
                    conn.AddInParameter(cmd, "@UserJob", DbType.String, entity.UserJob);
                    conn.AddInParameter(cmd, "@DepID", DbType.Int32, entity.DepID);
                    conn.AddInParameter(cmd, "@AddressPhone", DbType.String, entity.AddressPhone);
                    conn.AddInParameter(cmd, "@CompanyPhone", DbType.String, entity.CompanyPhone);
                    conn.AddInParameter(cmd, "@CellPhone", DbType.String, entity.CellPhone);
                    conn.AddInParameter(cmd, "@Email", DbType.String, entity.Email);

                    if ((entity.LastLoginTime.ToString("yyyy-MM-dd") != "0001-01-01" && entity.LastLoginTime.ToString("yyyy-MM-dd") != "1900-01-01"))
                    {
                        conn.AddInParameter(cmd, "@LastLoginTime", DbType.DateTime, entity.LastLoginTime);
                    }
                    conn.AddInParameter(cmd, "@IPAddress", DbType.String, entity.IPAddress);
                    conn.AddInParameter(cmd, "@DelFlag", DbType.String, entity.DelFlag);
                    conn.AddInParameter(cmd, "@CargoPermisID", DbType.String, entity.CargoPermisID);
                    conn.AddInParameter(cmd, "@CargoPermisName", DbType.String, entity.CargoPermisName);
                    conn.AddInParameter(cmd, "@HouseID", DbType.Int32, entity.HouseID);
                    conn.AddInParameter(cmd, "@HouseName", DbType.String, entity.HouseName);
                    conn.ExecuteNonQuery(cmd);

                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
            return 0;
        }
        /// <summary>
        /// 删除用户数据
        /// </summary>
        /// <param name="entity"></param>
        public void DelUsers(List<SystemUserEntity> entity)
        {
            try
            {
                foreach (var it in entity)
                {
                    string strSQL = @"UPDATE Tbl_SysUser SET DelFlag='1' WHERE UserID=@UserID";
                    if (it.DelFlag.Equals("1"))//彻底删除
                    {
                        strSQL = @"Delete from Tbl_SysUser where UserID=@UserID ";
                    }
                    using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                    {
                        conn.AddInParameter(cmd, "@UserID", DbType.Int32, it.UserID);
                        conn.ExecuteNonQuery(cmd);
                    }
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// 按UserID查询用户数据
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public SystemUserEntity GetUserByID(int UserID)
        {
            SystemUserEntity result = new SystemUserEntity();
            try
            {
                string strSQL = @"SELECT * from Tbl_SysUser Where UserID=@UserID";
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@UserID", DbType.Int32, UserID);
                    using (DataTable dt = conn.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            result = new SystemUserEntity
                            {
                                UserID = Convert.ToInt32(idr["UserID"]),
                                LoginName = Convert.ToString(idr["LoginName"]).Trim(),
                                UserName = Convert.ToString(idr["UserName"]).Trim(),
                                Sex = Convert.ToString(idr["Sex"]).Trim(),
                                Age = Convert.ToInt32(idr["Age"]),
                                UserIDNum = Convert.ToString(idr["UserIDNum"]).Trim(),
                                RoleID = Convert.ToInt32(idr["RoleID"]),

                                DepID = Convert.ToInt32(idr["DepID"]),
                                AddressPhone = Convert.ToString(idr["AddressPhone"]).Trim(),
                                CompanyPhone = Convert.ToString(idr["CompanyPhone"]).Trim(),
                                CellPhone = Convert.ToString(idr["CellPhone"]).Trim(),
                                Email = Convert.ToString(idr["Email"]).Trim(),
                                IPAddress = Convert.ToString(idr["IPAddress"]).Trim(),
                                CargoPermisID = Convert.ToString(idr["CargoPermisID"]).Trim(),
                                CargoPermisName = Convert.ToString(idr["CargoPermisName"]).Trim(),
                                HouseID = Convert.ToInt32(idr["HouseID"]),
                                HouseName = Convert.ToString(idr["HouseName"]).Trim(),
                                LastLoginTime = string.IsNullOrEmpty(Convert.ToString(idr["LastLoginTime"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["LastLoginTime"]),
                                DelFlag = Convert.ToString(idr["DelFlag"]).Trim(),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"])
                            };
                        }
                    }
                }
                return result;
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateUsers(SystemUserEntity entity)
        {
            try
            {
                entity.EnSafe();
                string strSQL = @"UPDATE Tbl_SysUser SET LoginName=@LoginName,";
                if (!string.IsNullOrEmpty(entity.LoginPwd))
                {
                    strSQL += @"LoginPwd=@LoginPwd,";
                }
                strSQL += @"UserName=@UserName,Sex=@Sex,Age=@Age,UserIDNum=@UserIDNum,RoleID=@RoleID,UserJob=@UserJob,DepID=@DepID,AddressPhone=@AddressPhone,CompanyPhone=@CompanyPhone,CellPhone=@CellPhone,Email=@Email,";
                strSQL += @"IPAddress=@IPAddress,DelFlag=@DelFlag,OP_DATE=@OP_DATE,CargoPermisID=@CargoPermisID,CargoPermisName=@CargoPermisName,HouseID=@HouseID,HouseName=@HouseName WHERE UserID=@UserID";

                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@LoginName", DbType.String, entity.LoginName);
                    if (!string.IsNullOrEmpty(entity.LoginPwd))
                    {
                        conn.AddInParameter(cmd, "@LoginPwd", DbType.String, entity.LoginPwd);
                    };
                    conn.AddInParameter(cmd, "@UserName", DbType.String, entity.UserName);
                    conn.AddInParameter(cmd, "@Sex", DbType.String, entity.Sex);
                    conn.AddInParameter(cmd, "@Age", DbType.Int32, entity.Age);
                    conn.AddInParameter(cmd, "@UserIDNum", DbType.String, entity.UserIDNum);
                    conn.AddInParameter(cmd, "@RoleID", DbType.Int32, entity.RoleID);
                    conn.AddInParameter(cmd, "@UserJob", DbType.String, entity.UserJob);

                    conn.AddInParameter(cmd, "@DepID", DbType.Int32, entity.DepID);
                    conn.AddInParameter(cmd, "@AddressPhone", DbType.String, entity.AddressPhone);
                    conn.AddInParameter(cmd, "@CompanyPhone", DbType.String, entity.CompanyPhone);
                    conn.AddInParameter(cmd, "@CellPhone", DbType.String, entity.CellPhone);
                    conn.AddInParameter(cmd, "@Email", DbType.String, entity.Email);
                    conn.AddInParameter(cmd, "@IPAddress", DbType.String, entity.IPAddress);
                    conn.AddInParameter(cmd, "@DelFlag", DbType.String, entity.DelFlag);
                    conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                    conn.AddInParameter(cmd, "@UserID", DbType.Int32, entity.UserID);
                    conn.AddInParameter(cmd, "@CargoPermisID", DbType.String, entity.CargoPermisID);
                    conn.AddInParameter(cmd, "@CargoPermisName", DbType.String, entity.CargoPermisName);
                    conn.AddInParameter(cmd, "@HouseID", DbType.Int32, entity.HouseID);
                    conn.AddInParameter(cmd, "@HouseName", DbType.String, entity.HouseName);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>1:用户名不存在</returns>
        public int UpdateUserPwd(SystemUserEntity entity)
        {
            try
            {
                if (!IsExistUser(entity))
                {
                    return 1;
                }
                string strSQL = @"UPDATE Tbl_SysUser SET LoginPwd=@LoginPwd";
                strSQL += " WHERE (1=1) ";
                if (!entity.UserID.Equals(0))
                {
                    strSQL += " and UserID=@UserID";

                }
                if (!string.IsNullOrEmpty(entity.LoginName))
                {
                    strSQL += " and LoginName=@LoginName";
                }
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@LoginName", DbType.String, entity.LoginName);
                    conn.AddInParameter(cmd, "@LoginPwd", DbType.String, entity.LoginPwd);
                    conn.AddInParameter(cmd, "@UserID", DbType.Int32, entity.UserID);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return 0;
        }
        /// <summary>
        /// 判断用户名是否存在 true:表示存在
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>true:表示存在</returns>
        public bool IsExistUser(SystemUserEntity entity)
        {
            string strQ = @"Select LoginName from Tbl_SysUser Where LoginName=@LoginName and DelFlag=0";
            using (DbCommand cmdQ = conn.GetSqlStringCommond(strQ))
            {
                conn.AddInParameter(cmdQ, "@LoginName", DbType.String, entity.LoginName);
                using (DataTable idr = conn.ExecuteDataTable(cmdQ))
                {
                    if (idr == null)
                    {
                        return false;
                    }
                    if (idr.Rows.Count <= 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// 查询所有用户名
        /// </summary>
        /// <returns></returns>
        public List<SystemUserEntity> QueryALLUser()
        {
            List<SystemUserEntity> result = new List<SystemUserEntity>();
            string strSQL = @"select LoginName,UserName,CellPhone from Tbl_SysUser where DelFlag=0";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = conn.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idrCount in dt.Rows)
                        {
                            result.Add(new SystemUserEntity
                            {
                                LoginName = Convert.ToString(idrCount["LoginName"]).Trim(),
                                CellPhone = Convert.ToString(idrCount["CellPhone"]).Trim(),
                                UserName = Convert.ToString(idrCount["UserName"]).Trim()
                            });
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        /// <summary>
        /// 通过登陆账号查询用户姓名
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public SystemUserEntity ReturnUserName(string loginName)
        {
            SystemUserEntity result = new SystemUserEntity();

            try
            {
                string strSQL = @"select UserName,UserID,LoginName,HouseID,CargoPermisID,HouseName,CargoPermisName from Tbl_SysUser Where LoginName=@LoginName";
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@LoginName", DbType.String, loginName);
                    using (DataTable dt = conn.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            result.UserID = Convert.ToInt32(idr["UserID"]);
                            result.LoginName = Convert.ToString(idr["LoginName"]).Trim();
                            result.UserName = Convert.ToString(idr["UserName"]).Trim();
                            result.HouseID = Convert.ToInt32(idr["HouseID"]);
                            result.HouseName = Convert.ToString(idr["HouseName"]).Trim();
                            result.CargoPermisID = Convert.ToString(idr["CargoPermisID"]).Trim();
                            result.CargoPermisName = Convert.ToString(idr["CargoPermisName"]).Trim();
                        }
                    }
                }
                return result;
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 根据部门ID查询该部门下的所有用户
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<SystemUserEntity> GetUserByDepID(string DepID)
        {

            List<SystemUserEntity> result = new List<SystemUserEntity>();
            string strSQL = @"select LoginName,UserName from Tbl_SysUser where DelFlag=0";
            if (!string.IsNullOrEmpty(DepID)) { strSQL += " and DepID in (" + DepID + ")"; }
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = conn.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idrCount in dt.Rows)
                        {
                            result.Add(new SystemUserEntity
                            {
                                LoginName = Convert.ToString(idrCount["LoginName"]).Trim(),
                                UserName = Convert.ToString(idrCount["UserName"]).Trim()
                            });
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        /// <summary>
        /// 通过部门代码查询该部门下的所有用户
        /// </summary>
        /// <param name="DepCode"></param>
        /// <returns></returns>
        public List<SystemUserEntity> GetUserByDepCode(string DepCode, int HouseID)
        {

            List<SystemUserEntity> result = new List<SystemUserEntity>();
            string strSQL = @"select a.LoginName,a.UserName,a.CellPhone from Tbl_SysUser as a left join Tbl_SysOrganize as b on a.DepID=b.ID where a.DelFlag='0'";
            if (!string.IsNullOrEmpty(DepCode)) { strSQL += " and b.Code = '" + DepCode + "'"; }
            if (!HouseID.Equals(0)) { strSQL += " and a.HouseID = " + HouseID + ""; }
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = conn.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idrCount in dt.Rows)
                        {
                            result.Add(new SystemUserEntity
                            {
                                LoginName = Convert.ToString(idrCount["LoginName"]).Trim(),
                                CellPhone = Convert.ToString(idrCount["CellPhone"]).Trim(),
                                UserName = Convert.ToString(idrCount["UserName"]).Trim()
                            });
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        /// <summary>
        /// 根据分公司代码查询该公司下面所有员工
        /// </summary>
        /// <param name="DepCode"></param>
        /// <returns></returns>
        public List<SystemUserEntity> GetUserByDepCode(string DepCode)
        {

            List<SystemUserEntity> result = new List<SystemUserEntity>();
            string strSQL = @"select * from Tbl_SysUser where DepID in (select ID from Tbl_SysOrganize where ParentID=(select ID from Tbl_SysOrganize where Code=@Code)) and DelFlag=0";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@Code", DbType.String, DepCode);
                    using (DataTable dt = conn.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idrCount in dt.Rows)
                        {
                            result.Add(new SystemUserEntity
                            {
                                LoginName = Convert.ToString(idrCount["LoginName"]).Trim(),
                                CellPhone = Convert.ToString(idrCount["CellPhone"]).Trim(),
                                UserName = Convert.ToString(idrCount["UserName"]).Trim()
                            });
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        /// <summary>
        /// 查找最大的登陆ID
        /// </summary>
        /// <returns></returns>
        public int GetMaxUserLoginID()
        {

            int result = 0;
            string strSQL = @"select MAX(LoginName) as LoginName from Tbl_SysUser";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = conn.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idrCount in dt.Rows)
                        {
                            result = Convert.ToInt32(idrCount["LoginName"]);
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        #endregion
        #region 组织架构操作方法集合
        /// <summary>
        /// 查询组织架构
        /// </summary>
        /// <returns></returns>
        public List<SystemOrganizeEntity> SystemOrganizeQuery()
        {
            List<SystemOrganizeEntity> result = new List<SystemOrganizeEntity>();
            string strSQL = @"SELECT * FROM Tbl_SysOrganize";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = conn.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idrCount in dt.Rows)
                        {
                            result.Add(new SystemOrganizeEntity
                            {
                                ID = Convert.ToInt32(idrCount["ID"]),
                                Name = Convert.ToString(idrCount["Name"]).Trim(),
                                ParentID = Convert.ToInt32(idrCount["ParentID"]),
                                Sort = Convert.ToInt32(idrCount["Sort"]),
                                Code = Convert.ToString(idrCount["Code"]).Trim(),
                                Boss = Convert.ToString(idrCount["Boss"]).Trim(),
                                Remark = Convert.ToString(idrCount["Remark"]).Trim(),
                                OP_DATE = Convert.ToDateTime(idrCount["OP_DATE"])
                            });
                        }
                    }
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
            return result;
        }
        /// <summary>
        /// 查询组织架构
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<SystemOrganizeEntity> SystemOrganizeQuery(SystemOrganizeEntity entity)
        {
            List<SystemOrganizeEntity> result = new List<SystemOrganizeEntity>();
            string strSQL = @"with Dept as(select id,Name,Code,ParentID,Sort,Boss,Remark,OP_DATE from Tbl_SysOrganize where 1=1 ";
            if (entity.ID != 0)
            {
                strSQL += " and ID=" + entity.ID;
            }
            if (!string.IsNullOrEmpty(entity.Name))
            {
                strSQL += " and Name like'%" + entity.Name + "%'";
            }
            if (!string.IsNullOrEmpty(entity.Code))
            {
                strSQL += " and Code='" + entity.Code + "'";
            }
            if (!string.IsNullOrEmpty(entity.Boss))
            {
                strSQL += " and Boss like'%" + entity.Boss + "%'";
            }
            if (entity.ParentID != 0)
            {
                strSQL += " and ParentID=" + entity.ParentID;
            }
            strSQL += " union all select HrmDept.id,HrmDept.Name,HrmDept.Code,HrmDept.ParentID,HrmDept.Sort,HrmDept.Boss,HrmDept.Remark,HrmDept.OP_DATE from Tbl_SysOrganize HrmDept,Dept where HrmDept.ParentID=Dept.id ) select * from Dept";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = conn.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idrCount in dt.Rows)
                        {
                            result.Add(new SystemOrganizeEntity
                            {
                                ID = Convert.ToInt32(idrCount["ID"]),
                                Name = Convert.ToString(idrCount["Name"]).Trim(),
                                ParentID = Convert.ToInt32(idrCount["ParentID"]),
                                Sort = Convert.ToInt32(idrCount["Sort"]),
                                Code = Convert.ToString(idrCount["Code"]).Trim(),
                                Boss = Convert.ToString(idrCount["Boss"]).Trim(),
                                Remark = Convert.ToString(idrCount["Remark"]).Trim(),
                                OP_DATE = Convert.ToDateTime(idrCount["OP_DATE"])
                            });
                        }
                    }
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
            return result;
        }
        /// <summary>
        /// 删除组织架构
        /// </summary>
        /// <param name="entity"></param>
        public void SystemOrganizeDel(List<SystemOrganizeEntity> entity)
        {
            try
            {
                foreach (var it in entity)
                {
                    string strSQL = @"DELETE FROM Tbl_SysOrganize WHERE ID=@ID";
                    using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                    {
                        conn.AddInParameter(cmd, "@ID", DbType.Int32, it.ID);
                        conn.ExecuteNonQuery(cmd);
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }

        /// <summary>
        /// 新增组织架构
        /// </summary>
        /// <param name="entity"></param>
        public void SystemOrganizeAdd(SystemOrganizeEntity entity)
        {
            string strSQL = @"INSERT INTO Tbl_SysOrganize(Name,Code,Boss,Sort,ParentID,Remark) VALUES  (@Name,@Code,@Boss,@Sort,@ParentID,@Remark)";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@Name", DbType.String, entity.Name.Trim());
                    conn.AddInParameter(cmd, "@Code", DbType.String, entity.Code.ToUpper().Trim());
                    conn.AddInParameter(cmd, "@Boss", DbType.String, entity.Boss.Trim());
                    conn.AddInParameter(cmd, "@Sort", DbType.Int32, entity.Sort);
                    conn.AddInParameter(cmd, "@ParentID", DbType.Int32, entity.ParentID);
                    conn.AddInParameter(cmd, "@Remark", DbType.String, entity.Remark.Trim());
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 修改组织架构
        /// </summary>
        /// <param name="entity"></param>
        public void SystemOrganizeUpdate(SystemOrganizeEntity entity)
        {
            string strSQL = @"UPDATE Tbl_SysOrganize SET Name=@Name,Code=@Code,Remark=@Remark,Boss=@Boss WHERE ID=@ID";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@ID", DbType.Int32, entity.ID);
                    conn.AddInParameter(cmd, "@Name", DbType.String, entity.Name.Trim());
                    conn.AddInParameter(cmd, "@Code", DbType.String, entity.Code.ToUpper().Trim());
                    conn.AddInParameter(cmd, "@Boss", DbType.String, entity.Boss.Trim());
                    conn.AddInParameter(cmd, "@Remark", DbType.String, entity.Remark.Trim());
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 判断是否存在相同的组织架构代码
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool isExistOrganizeCode(SystemOrganizeEntity entity)
        {
            string strQ = @"Select Code from Tbl_SysOrganize Where Code=@Code";
            using (DbCommand cmdQ = conn.GetSqlStringCommond(strQ))
            {
                conn.AddInParameter(cmdQ, "@Code", DbType.String, entity.Code);
                using (DataTable idr = conn.ExecuteDataTable(cmdQ))
                {
                    if (idr == null)
                    {
                        return false;
                    }
                    if (idr.Rows.Count <= 0)
                    {
                        return false;
                    }
                }
            }
            return true;
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
            userInfo = new SystemUserEntity();
            string isSSO = string.Empty;
            string systemPwd = string.Empty;
            string userEncryPwd = string.Empty;
            string delflag = string.Empty;
            DataTable idr = new DataTable();
            try
            {
                string strSQL = @"Select b.CName,b.IsAdmin,c.Name as DepName,c.Remark as OrgRemark,a.* from Tbl_SysUser as a left join Tbl_SysRole as b on a.RoleID=b.RoleID left join Tbl_SysOrganize as c on a.DepID=c.ID Where a.DelFlag=0 and a.LoginName=@USERID";

                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@USERID", DbType.String, UserID);
                    idr = conn.ExecuteDataTable(cmd);
                    if (idr == null)
                    {
                        return 1;
                    }
                    if (idr.Rows.Count < 1)
                    {
                        return 1;
                    }
                    foreach (DataRow drr in idr.Rows)
                    {
                        userInfo.LoginName = Convert.ToString(drr["LoginName"]).Trim();//登陆账号
                        userInfo.UserName = Convert.ToString(drr["UserName"]).Trim();//员工姓名
                        userInfo.UserID = Convert.ToInt32(drr["UserID"]);
                        userInfo.IsAdmin = Convert.ToString(drr["IsAdmin"]).Trim();//是否管理员
                        userInfo.IsModifyInNum = Convert.ToString(drr["IsModifyInNum"]).Trim();//是否能修改库存
                        userInfo.AddressPhone = Convert.ToString(drr["AddressPhone"]).Trim();
                        userInfo.UserIDNum = Convert.ToString(drr["UserIDNum"]).Trim();
                        userInfo.RoleCName = Convert.ToString(drr["CName"]).Trim();//角色名称
                        userInfo.RoleID = Convert.ToInt32(drr["RoleID"]);
                        userInfo.UserJob = Convert.ToString(drr["UserJob"]).Trim();
                        userInfo.CellPhone = Convert.ToString(drr["CellPhone"]).Trim();
                        userInfo.DepCName = Convert.ToString(drr["DepName"]).Trim();
                        userInfo.CargoPermisID = Convert.ToString(drr["CargoPermisID"]).Trim();
                        userInfo.CargoPermisName = Convert.ToString(drr["CargoPermisName"]).Trim();
                        userInfo.HouseID = Convert.ToInt32(drr["HouseID"]);
                        userInfo.HouseName = Convert.ToString(drr["HouseName"]).Trim();
                        userInfo.OrgRemark = Convert.ToString(drr["OrgRemark"]).Trim();//组织架构的备注
                        userInfo.DepID = Convert.ToInt32(drr["DepID"]);
                        //userInfo.PickTitle = Convert.ToString(drr["PickTitle"]).Trim();
                        //userInfo.SendTitle = Convert.ToString(drr["SendTitle"]).Trim();
                        systemPwd = Convert.ToString(drr["LoginPwd"]).Trim();
                    }
                    List<HouseEntity> cargoList = new List<HouseEntity>();
                    if (!string.IsNullOrEmpty(userInfo.CargoPermisID))
                    {
                        string[] perArr = userInfo.CargoPermisID.Split(',');
                        string[] nameArr = userInfo.CargoPermisName.Split(',');
                        for (int i = 0; i < perArr.Length; i++)
                        {
                            if (!string.IsNullOrEmpty(perArr[i]))
                            {
                                cargoList.Add(new HouseEntity
                                {
                                    ID = Convert.ToInt32(perArr[i]),
                                    Name = Convert.ToString(nameArr[i])
                                });
                            }
                        }
                    }
                    userInfo.CargoList = cargoList;
                    userEncryPwd = EncodePassword(UserPWD);
                    if (systemPwd != userEncryPwd)
                    {
                        return 3;
                    }
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
            return 0;
        }
        /// <summary>
        /// 加密密码返回加密后的密文
        /// 【1】前两位与第七，八位进行替换
        /// 【2】转换成字节数据，将salt添加至未位
        /// 【3】用SHA256加密，返回
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static string EncodePassword(string password)
        {
            string passOne = password.Substring(0, 2);
            string passTwo = password.Substring(6, 2);
            string passThree = password.Substring(2, 4);
            string passFour = password.Substring(8, password.Length - 8);
            string result = passTwo + passThree + passOne + passFour;

            byte[] pass = Encoding.Unicode.GetBytes(result);
            //byte[] saltByte = Convert.FromBase64String(salt);
            //byte[] dst = new byte[pass.Length + saltByte.Length];
            byte[] returnPassword = null;

            //Buffer.BlockCopy(pass, 0, dst, 0, pass.Length);
            //Buffer.BlockCopy(saltByte, 0, dst, pass.Length, saltByte.Length);

            SHA256 sha = SHA256Cng.Create();
            //SHA256 sha = new SHA256Managed();
            returnPassword = sha.ComputeHash(pass);
            sha.Clear();

            return Convert.ToBase64String(returnPassword);
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public string DecrypStrByHash(string src)
        {
            if (src.Length < 40) return "";
            byte[] SrcBytes = System.Convert.FromBase64String(src.Substring(16));
            byte[] RiKey = System.Text.Encoding.ASCII.GetBytes(src.Substring(0, 16).ToCharArray());
            byte[] InitialText = new byte[SrcBytes.Length];
            RijndaelManaged rv = new RijndaelManaged();
            MemoryStream ms = new MemoryStream(SrcBytes);
            CryptoStream cs = new CryptoStream(ms, rv.CreateDecryptor(RiKey, RiKey), CryptoStreamMode.Read);
            try
            {
                cs.Read(InitialText, 0, InitialText.Length);
            }
            finally
            {
                ms.Close();
                cs.Close();
            }
            System.Text.StringBuilder Result = new System.Text.StringBuilder();
            for (int i = 0; i < InitialText.Length; ++i) if (InitialText[i] > 0) Result.Append((char)InitialText[i]);
            return Result.ToString();
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
            List<LogEntity> result = new List<LogEntity>();

            Hashtable resHT = new Hashtable();
            try
            {
                //(" + pNum + "*(" + pIndex + "-1)

                string strSQL = @" SELECT TOP " + pNum + " * from (select ROW_NUMBER() OVER (ORDER BY OP_DATE DESC) AS RowNumber, * from Tbl_SysLog where (1=1)";
                if (!string.IsNullOrEmpty(entity.UserID))
                {
                    strSQL += " and UserID = '" + entity.UserID + "'";
                }
                //操作类型
                if (!string.IsNullOrEmpty(entity.Operate))
                {
                    strSQL += " and Operate ='" + entity.Operate + "'";
                }
                if (!string.IsNullOrEmpty(entity.Moudle))
                {
                    strSQL += " and Moudle ='" + entity.Moudle + "'";
                }
                if (!string.IsNullOrEmpty(entity.Status))
                {
                    strSQL += " and Status ='" + entity.Status + "'";
                }
                //备注
                if (!string.IsNullOrEmpty(entity.Memo))
                {
                    strSQL += " and Memo like '%" + entity.Memo + "%'";
                }
                //操作日期范围
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and OP_DATE>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and OP_DATE<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                strSQL += " ) as A where RowNumber > (" + pNum + "* (" + pIndex + "-1))";
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            result.Add(new LogEntity
                            {
                                BID = Convert.ToInt64(idr["Batch_ID"]),
                                IPAddress = Convert.ToString(idr["IPAddress"]).Trim(),
                                UserID = Convert.ToString(idr["UserID"]).Trim(),
                                Status = Convert.ToString(idr["Status"]),
                                Moudle = Convert.ToString(idr["Moudle"]).Trim(),
                                NvgPage = Convert.ToString(idr["NvgPage"]).Trim(),
                                Operate = Convert.ToString(idr["Operate"]).Trim(),
                                Memo = Convert.ToString(idr["Memo"]).Trim(),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"])
                            });
                        }
                    }
                }

                resHT["rows"] = result;

                string strCount = @"Select Count(*) as TotalCount from Tbl_SysLog Where (1=1)";

                if (!string.IsNullOrEmpty(entity.UserID))
                {
                    strCount += " and UserID = '" + entity.UserID + "'";
                }
                //操作类型
                if (!string.IsNullOrEmpty(entity.Operate))
                {
                    strCount += " and Operate ='" + entity.Operate + "'";
                }
                if (!string.IsNullOrEmpty(entity.Moudle))
                {
                    strCount += " and Moudle ='" + entity.Moudle + "'";
                }
                if (!string.IsNullOrEmpty(entity.Status))
                {
                    strCount += " and Status ='" + entity.Status + "'";
                }
                //备注
                if (!string.IsNullOrEmpty(entity.Memo))
                {
                    strCount += " and Memo like '%" + entity.Memo + "%'";
                }
                //操作日期范围
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strCount += " and OP_DATE>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strCount += " and OP_DATE<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                using (DbCommand cmd = conn.GetSqlStringCommond(strCount))
                {
                    using (DataTable idrCount = conn.ExecuteDataTable(cmd))
                    {
                        if (idrCount.Rows.Count > 0)
                        {
                            resHT["total"] = Convert.ToInt32(idrCount.Rows[0]["TotalCount"]);
                        }
                    }

                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
            return resHT;
        }

        /// <summary>
        /// 通过ID获取日志详细信息
        /// </summary>
        /// <param name="bid"></param>
        /// <returns></returns>
        public LogEntity GetLogByID(Int64 bid)
        {
            LogEntity result = new LogEntity();
            try
            {
                string strSQL = @"Select * from Tbl_SysLog where Batch_ID=@Batch_ID";

                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(command, "@Batch_ID", DbType.Int64, bid);
                    using (DataTable dt = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            result = new LogEntity
                            {
                                BID = Convert.ToInt64(idr["Batch_ID"]),
                                IPAddress = Convert.ToString(idr["IPAddress"]).Trim(),
                                UserID = Convert.ToString(idr["UserID"]).Trim(),
                                Status = Convert.ToString(idr["Status"]),
                                Moudle = Convert.ToString(idr["Moudle"]).Trim(),
                                NvgPage = Convert.ToString(idr["NvgPage"]).Trim(),
                                Operate = Convert.ToString(idr["Operate"]).Trim(),
                                Memo = Convert.ToString(idr["Memo"]).Trim(),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"])
                            };
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        #endregion
    }
}
