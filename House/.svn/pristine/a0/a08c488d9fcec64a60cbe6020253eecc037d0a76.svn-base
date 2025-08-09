using House.DataAccess;
using House.Entity.Cargo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;


namespace House.Manager.Cargo
{
    /// <summary>
    /// 微信企业号数据操作类
    /// </summary>
    public class QiyeManager
    {
        private SqlHelper conn = new SqlHelper();
        #region 部门管理
        /// <summary>
        /// 部门查询
        /// </summary>
        /// <param name="pIndex">当前页数</param>
        /// <param name="pNum">每页多少条数据</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="strCountWhere">查询总数的查询条件</param>
        /// <returns></returns>
        public Hashtable queryDepart(int pIndex, int pNum, QyDepartEntity entity)
        {
            List<QyDepartEntity> result = new List<QyDepartEntity>();
            Hashtable resHT = new Hashtable();
            try
            {
                string strSQL = @" SELECT TOP " + pNum + " * from (select ROW_NUMBER() OVER (ORDER BY OP_DATE DESC) AS RowNumber,* FROM Tbl_QY_Depart WHERE (1=1)";
                //以名称为查询条件
                if (!string.IsNullOrEmpty(entity.Name)) { strSQL += " and Name like '%" + entity.Name + "%'"; }
                strSQL += " ) as A where RowNumber > (" + pNum + "* (" + pIndex + "-1))";
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dd = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dd.Rows)
                        {
                            result.Add(new QyDepartEntity
                            {
                                Id = Convert.ToInt32(idr["Id"]),
                                Name = Convert.ToString(idr["Name"]).Trim(),
                                Parentid = Convert.ToInt32(idr["Parentid"]),
                                DepOrder = Convert.ToString(idr["DepOrder"]),
                                LeaderID = Convert.ToInt32(idr["LeaderID"]),
                                Leader = Convert.ToString(idr["Leader"]),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"])
                            });
                        }
                    }
                }
                resHT["rows"] = result;

                string strCount = @"Select Count(*) as TotalCount from Tbl_QY_Depart Where (1=1)";
                //以名称为查询条件
                if (!string.IsNullOrEmpty(entity.Name)) { strCount += " and Name like '%" + entity.Name + "%'"; }
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
        /// 部门查询
        /// </summary>
        /// <returns></returns>
        public List<QyDepartEntity> queryDepart()
        {
            List<QyDepartEntity> result = new List<QyDepartEntity>();
            Hashtable resHT = new Hashtable();
            try
            {
                string strSQL = @"select id,Name,Parentid,DepOrder,ISNULL(Boss,'') as Boss,ISNULL(BossID,'-1') as BossID,ISNULL(Leader,'') as Leader,ISNULL(LeaderID,'-1') as LeaderID,OP_DATE from Tbl_QY_Depart order by id";
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dd = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dd.Rows)
                        {
                            result.Add(new QyDepartEntity
                            {
                                Id = Convert.ToInt32(idr["Id"]),
                                Name = Convert.ToString(idr["Name"]).Trim(),
                                Parentid = Convert.ToInt32(idr["Parentid"]),
                                DepOrder = Convert.ToString(idr["DepOrder"]),
                                BossID = Convert.ToInt32(idr["BossID"]),
                                Boss = Convert.ToString(idr["Boss"]),
                                LeaderID = Convert.ToInt32(idr["LeaderID"]),
                                Leader = Convert.ToString(idr["Leader"]),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"])
                            });
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        /// <summary>
        /// 删除部门数据
        /// </summary>
        /// <param name="entity"></param>
        public void DelQYDepart(List<QyDepartEntity> entity)
        {
            try
            {
                foreach (var it in entity)
                {
                    string strSQL = @"Delete from Tbl_QY_Depart where Id=@Id ";

                    using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                    {
                        conn.AddInParameter(cmd, "@Id", DbType.Int32, it.Id);
                        conn.ExecuteNonQuery(cmd);
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 保存部门数据
        /// </summary>
        /// <param name="entity"></param>
        public void AddQYDepart(QyDepartEntity entity)
        {
            string strSQL = @"INSERT INTO Tbl_QY_Depart(Id,Name,Parentid,DepOrder) VALUES (@Id,@Name,@Parentid,@DepOrder)";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@Name", DbType.String, entity.Name);
                    conn.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
                    conn.AddInParameter(cmd, "@Parentid", DbType.Int32, entity.Parentid);
                    conn.AddInParameter(cmd, "@DepOrder", DbType.String, entity.DepOrder);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }

        }
        /// <summary>
        /// 修改部门
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateQYDepart(QyDepartEntity entity)
        {
            string strSQL = @"UPDATE Tbl_QY_Depart set Name=@Name WHERE Id=@Id";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@Name", DbType.String, entity.Name);
                    conn.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 通过部门ID查询部门数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public QyDepartEntity QueryDepartByID(QyDepartEntity entity)
        {
            QyDepartEntity result = new QyDepartEntity();
            string strSQL = @"SELECT * FROM Tbl_QY_Depart Where Id=@Id";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
                    using (DataTable dd = conn.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idr in dd.Rows)
                        {
                            result.Name = Convert.ToString(idr["Name"]).Trim();
                            result.Id = Convert.ToInt32(idr["Id"]);
                            result.Parentid = Convert.ToInt32(idr["Parentid"]);
                            result.DepOrder = Convert.ToString(idr["DepOrder"]);
                            result.OP_DATE = Convert.ToDateTime(idr["OP_DATE"]);
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        /// <summary>
        /// 根据父ID查询所有部门数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<QyDepartEntity> QueryDepartList(QyDepartEntity entity)
        {
            List<QyDepartEntity> resList = new List<QyDepartEntity>();
            string strSQL = @"with depart as(select * from Tbl_QY_Depart where Id = @Parentid union all select d.* from depart c inner join Tbl_QY_Depart d on c.Id = d.Parentid)select * from depart";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@Parentid", DbType.Int32, entity.Parentid);
                    using (DataTable dd = conn.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idr in dd.Rows)
                        {
                            QyDepartEntity result = new QyDepartEntity();
                            result.Name = Convert.ToString(idr["Name"]).Trim();
                            result.Id = Convert.ToInt32(idr["Id"]);
                            result.Parentid = Convert.ToInt32(idr["Parentid"]);
                            result.DepOrder = Convert.ToString(idr["DepOrder"]);
                            result.OP_DATE = Convert.ToDateTime(idr["OP_DATE"]);
                            resList.Add(result);
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return resList;
        }
        /// <summary>
        /// 根据部门ID查询所有部门人员
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<QyUserEntity> QueryDepartAllUserList(QyUserEntity entity)
        {
            List<QyUserEntity> resList = new List<QyUserEntity>();
            string strSQL = @"select * from Tbl_QY_User where CHARINDEX(','+'" + entity.Department + "'+',' , ','+Department+',') > 0";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dd = conn.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idr in dd.Rows)
                        {
                            QyUserEntity result = new QyUserEntity();
                            result.UserID = Convert.ToString(idr["UserID"]).Trim();
                            result.WxName = Convert.ToString(idr["WxName"]).Trim();
                            resList.Add(result);
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return resList;
        }
        /// <summary>
        /// 查询多个部门的所有人员
        /// </summary>
        /// <param name="departStr"></param>
        /// <returns></returns>
        public List<QyUserEntity> QueryDepartAllUserList(string departStr)
        {
            List<QyUserEntity> resList = new List<QyUserEntity>();
            string[] depart = departStr.Split(',');
            string strSQL = @"select * from Tbl_QY_User where 1=2 ";
            foreach (var item in depart)
            {
                strSQL += " or CHARINDEX(','+" + item + "+',' , ','+Department+',') > 0 ";
            }
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dd = conn.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idr in dd.Rows)
                        {
                            QyUserEntity result = new QyUserEntity();
                            result.UserID = Convert.ToString(idr["UserID"]).Trim();
                            result.WxName = Convert.ToString(idr["WxName"]).Trim();
                            resList.Add(result);
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return resList;
        }
        /// <summary>
        /// 获取所有组织架构
        /// </summary>
        /// <returns></returns>
        public List<QyDepartEntity> QueryDepartList()
        {
            List<QyDepartEntity> resList = new List<QyDepartEntity>();
            string strSQL = @"SELECT * FROM Tbl_QY_Depart";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dd = conn.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idr in dd.Rows)
                        {
                            QyDepartEntity result = new QyDepartEntity();
                            result.Name = Convert.ToString(idr["Name"]).Trim();
                            result.Id = Convert.ToInt32(idr["Id"]);
                            result.Parentid = Convert.ToInt32(idr["Parentid"]);
                            result.DepOrder = Convert.ToString(idr["DepOrder"]);
                            result.OP_DATE = Convert.ToDateTime(idr["OP_DATE"]);
                            resList.Add(result);
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return resList;
        }
        /// <summary>
        /// 同步部门数据 
        /// </summary>
        /// <param name="entity"></param>
        public void SyncDepart(List<QyDepartEntity> entity)
        {
            truncateTable("Tbl_QY_Depart");
            UpdateUserleader();
            foreach (var it in entity)
            {

                string strSQL = @"INSERT INTO Tbl_QY_Depart(Id,Name,Parentid,DepOrder,Boss,BossID) VALUES (@Id,@Name,@Parentid,@DepOrder,@Boss,@BossID)";
                try
                {
                    using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                    {
                        conn.AddInParameter(cmd, "@Name", DbType.String, it.Name);
                        conn.AddInParameter(cmd, "@Id", DbType.Int32, it.Id);
                        conn.AddInParameter(cmd, "@Parentid", DbType.Int32, it.Parentid);
                        conn.AddInParameter(cmd, "@DepOrder", DbType.String, it.DepOrder);
                        conn.AddInParameter(cmd, "@BossID", DbType.Int32, it.BossID);
                        conn.AddInParameter(cmd, "@Boss", DbType.String, string.IsNullOrEmpty(it.Boss) ? "" : it.Boss);
                        conn.ExecuteNonQuery(cmd);
                    }
                    strSQL = "update Tbl_QY_User set Isleader=1 where UserID=@UserID";
                    using (DbCommand cmd2 = conn.GetSqlStringCommond(strSQL))
                    {
                        conn.AddInParameter(cmd2, "@UserID", DbType.String, it.BossID);
                        conn.ExecuteNonQuery(cmd2);
                    }
                }
                catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            }
            UpdateLeaderData();
        }
        /// <summary>
        /// 更新所有成员上级状态
        /// </summary>
        private void UpdateUserleader()
        {
            string strSQL = @"update Tbl_QY_User set Isleader=0";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 更新部门上级领导
        /// </summary>
        private void UpdateLeaderData()
        {
            string strSQL = @"select * from Tbl_QY_Depart";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dt = conn.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            strSQL = @"select * from Tbl_QY_Depart where Id=" + Convert.ToInt32(idr["Parentid"]);
                            using (DbCommand cmd2 = conn.GetSqlStringCommond(strSQL))
                            {
                                using (DataTable dt2 = conn.ExecuteDataTable(cmd2))
                                {
                                    foreach (DataRow idr2 in dt2.Rows)
                                    {
                                        if (!string.IsNullOrEmpty(Convert.ToString(idr2["BossID"])) && !string.IsNullOrEmpty(Convert.ToString(idr2["Boss"])))
                                        {
                                            strSQL = @"update Tbl_QY_Depart set LeaderID=@LeaderID,Leader=@Leader where Id=@Id";

                                            using (DbCommand cmd3 = conn.GetSqlStringCommond(strSQL))
                                            {
                                                conn.AddInParameter(cmd3, "@LeaderID", DbType.Int32, Convert.ToInt32(idr2["BossID"]));
                                                conn.AddInParameter(cmd3, "@Leader", DbType.String, Convert.ToString(idr2["Boss"]));
                                                conn.AddInParameter(cmd3, "@Id", DbType.Int32, Convert.ToInt32(idr["Id"]));
                                                conn.ExecuteNonQuery(cmd3);
                                            }
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 清空数据表
        /// </summary>
        private void truncateTable(string table)
        {
            string strSQL = @"truncate table " + table;

            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.ExecuteNonQuery(cmd);
            }
        }

        #endregion
        #region 用户管理
        /// <summary>
        /// 同步用户数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="type">同步状态1=同步指定成员，0=同步全部成员</param>
        /// 同步全部成员时删除其余数据
        public void SyncUser(List<QyUserEntity> entity, int type)
        {
            //truncateTable("Tbl_QY_User");
            string UserIDStr = "";
            try
            {
                foreach (var it in entity)
                {
                    UserIDStr += "'" + it.UserID + "'" + ",";
                    it.EnSafe();
                    string strSQL = @"select * from Tbl_QY_User WHERE UserID=@UserID";

                    using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                    {
                        conn.AddInParameter(cmd, "@UserID", DbType.String, it.UserID);
                        using (DataTable dt = conn.ExecuteDataTable(cmd))
                        {
                            if (dt.Rows.Count > 0)
                            {
                                strSQL = @"UPDATE Tbl_QY_User set OpenID=@OpenID,Department=@Department,MainDepartment=@MainDepartment,AvatarSmall=@AvatarSmall,AvatarBig=@AvatarBig WHERE UserID=@UserID";
                                using (DbCommand cmdUpdate = conn.GetSqlStringCommond(strSQL))
                                {
                                    conn.AddInParameter(cmdUpdate, "@UserID", DbType.String, it.UserID);
                                    conn.AddInParameter(cmdUpdate, "@OpenID", DbType.String, it.OpenID);
                                    conn.AddInParameter(cmdUpdate, "@WxName", DbType.String, it.WxName);
                                    conn.AddInParameter(cmdUpdate, "@weixinID", DbType.String, it.weixinID);
                                    conn.AddInParameter(cmdUpdate, "@CellPhone", DbType.String, it.CellPhone);
                                    if (!string.IsNullOrEmpty(it.Department))
                                    {
                                        conn.AddInParameter(cmdUpdate, "@Department", DbType.String, it.Department);
                                    }
                                    conn.AddInParameter(cmdUpdate, "@MainDepartment", DbType.String, it.MainDepartment);
                                    conn.AddInParameter(cmdUpdate, "@Position", DbType.String, it.Position);
                                    conn.AddInParameter(cmdUpdate, "@Gender", DbType.String, it.Gender);
                                    conn.AddInParameter(cmdUpdate, "@Email", DbType.String, it.Email);
                                    conn.AddInParameter(cmdUpdate, "@AvatarSmall", DbType.String, it.AvatarSmall);
                                    conn.AddInParameter(cmdUpdate, "@AvatarBig", DbType.String, it.AvatarBig);
                                    conn.ExecuteNonQuery(cmdUpdate);
                                }
                            }
                            else
                            {
                                strSQL = @"INSERT INTO Tbl_QY_User(UserID,OpenID,WxName,weixinID,CellPhone,Department,MainDepartment,Position,Gender,Email,AvatarSmall,AvatarBig) VALUES (@UserID,@OpenID,@WxName,@weixinID,@CellPhone,@Department,@MainDepartment,@Position,@Gender,@Email,@AvatarSmall,@AvatarBig)";
                                using (DbCommand cmdInsert = conn.GetSqlStringCommond(strSQL))
                                {
                                    conn.AddInParameter(cmdInsert, "@UserID", DbType.String, it.UserID);
                                    conn.AddInParameter(cmdInsert, "@OpenID", DbType.String, it.OpenID);
                                    conn.AddInParameter(cmdInsert, "@WxName", DbType.String, it.WxName);
                                    conn.AddInParameter(cmdInsert, "@weixinID", DbType.String, it.weixinID);
                                    conn.AddInParameter(cmdInsert, "@CellPhone", DbType.String, it.CellPhone);
                                    if (!string.IsNullOrEmpty(it.Department))
                                    {
                                        conn.AddInParameter(cmdInsert, "@Department", DbType.String, it.Department);
                                    }
                                    conn.AddInParameter(cmdInsert, "@MainDepartment", DbType.String, it.MainDepartment);
                                    conn.AddInParameter(cmdInsert, "@Position", DbType.String, it.Position);
                                    conn.AddInParameter(cmdInsert, "@Gender", DbType.String, it.Gender);
                                    conn.AddInParameter(cmdInsert, "@Email", DbType.String, it.Email);
                                    conn.AddInParameter(cmdInsert, "@AvatarSmall", DbType.String, it.AvatarSmall);
                                    conn.AddInParameter(cmdInsert, "@AvatarBig", DbType.String, it.AvatarBig);
                                    conn.ExecuteNonQuery(cmdInsert);
                                }
                            }
                        }
                    }
                    #region 2020-12-15更改为不包含此成员时新增
                    ////string strSQL = @"INSERT INTO Tbl_QY_User(UserID,OpenID,WxName,weixinID,CellPhone,Department,Position,Gender,Email,AvatarSmall,AvatarBig) VALUES (@UserID,@OpenID,@WxName,@weixinID,@CellPhone,@Department,@Position,@Gender,@Email,@AvatarSmall,@AvatarBig)";
                    //string strSQL = @"UPDATE Tbl_QY_User set OpenID=@OpenID,Department=@Department,AvatarSmall=@AvatarSmall,AvatarBig=@AvatarBig WHERE UserID=@UserID";

                    //using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                    //{
                    //    conn.AddInParameter(cmd, "@UserID", DbType.String, it.UserID);
                    //    conn.AddInParameter(cmd, "@OpenID", DbType.String, it.OpenID);
                    //    //conn.AddInParameter(cmd, "@WxName", DbType.String, it.WxName);
                    //    //conn.AddInParameter(cmd, "@weixinID", DbType.String, it.weixinID);
                    //    //conn.AddInParameter(cmd, "@CellPhone", DbType.String, it.CellPhone);
                    //    if (!string.IsNullOrEmpty(it.Department))
                    //    {
                    //        conn.AddInParameter(cmd, "@Department", DbType.String, it.Department);
                    //    }
                    //    //conn.AddInParameter(cmd, "@Position", DbType.String, it.Position);
                    //    //conn.AddInParameter(cmd, "@Gender", DbType.String, it.Gender);
                    //    //conn.AddInParameter(cmd, "@Email", DbType.String, it.Email);
                    //    conn.AddInParameter(cmd, "@AvatarSmall", DbType.String, it.AvatarSmall);
                    //    conn.AddInParameter(cmd, "@AvatarBig", DbType.String, it.AvatarBig);
                    //    conn.ExecuteNonQuery(cmd);
                    //}
                    #endregion
                }
                if (type == 0)
                {
                    UserIDStr = UserIDStr.Substring(0, UserIDStr.Length - 1);
                    string strSQL = @"delete from Tbl_QY_User WHERE UserID not in (" + UserIDStr + ")";
                    using (DbCommand cmdDelete = conn.GetSqlStringCommond(strSQL))
                    {
                        conn.ExecuteNonQuery(cmdDelete);
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 查询微信用户数据
        /// </summary>
        /// <param name="pIndex"></param>
        /// <param name="pNum"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Hashtable queryUser(int pIndex, int pNum, QyUserEntity entity)
        {
            List<QyUserEntity> result = new List<QyUserEntity>();
            Hashtable resHT = new Hashtable();
            try
            {
                string strSQL = @" SELECT TOP " + pNum + " * from (select ROW_NUMBER() OVER (ORDER BY us.OP_DATE DESC) AS RowNumber,us.*,isnull(ho.Name,'') as HouseName  FROM Tbl_QY_User us left join Tbl_Cargo_House ho on us.HouseID=ho.HouseID WHERE (1=1)";
                //以微信名称为查询条件
                if (!string.IsNullOrEmpty(entity.WxName)) { strSQL += " and us.WxName like '%" + entity.WxName + "%'"; }
                if (!string.IsNullOrEmpty(entity.Department)) { strSQL += " and us.Department like '%" + entity.Department + "%'"; }
                strSQL += " ) as A where RowNumber > (" + pNum + "* (" + pIndex + "-1))";
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dd = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dd.Rows)
                        {
                            string MainDepartmentName = string.Empty;
                            QyDepartEntity MainDepartment = QueryDepartByID(new QyDepartEntity { Id = Convert.ToInt32(idr["MainDepartment"]) });
                            MainDepartmentName = MainDepartment.Name;
                            string depName = string.Empty;
                            string[] dep = Convert.ToString(idr["Department"]).Split(',');
                            if (dep.Length > 0)
                            {
                                for (int i = 0; i < dep.Length; i++)
                                {
                                    QyDepartEntity depart = QueryDepartByID(new QyDepartEntity { Id = Convert.ToInt32(dep[i]) });
                                    depName += depart.Name + ",";
                                }
                                depName = depName.Substring(0, depName.Length - 1);
                            }
                            string tagName = string.Empty;
                            if (!string.IsNullOrEmpty(Convert.ToString(idr["Tag"])))
                            {
                                string[] tag = Convert.ToString(idr["Tag"]).Split(',');
                                if (tag.Length > 0)
                                {
                                    for (int j = 0; j < tag.Length; j++)
                                    {
                                        if (string.IsNullOrEmpty(Convert.ToString(tag[j]))) { continue; }
                                        QyTagEntity tagen = QueryTagByID(new QyTagEntity { Id = Convert.ToInt32(tag[j]) });
                                        tagName += tagen.Name + ",";
                                    }
                                    tagName = tagName.Substring(0, tagName.Length - 1);
                                }
                            }

                            result.Add(new QyUserEntity
                            {
                                UserID = Convert.ToString(idr["UserID"]),
                                OpenID = Convert.ToString(idr["OpenID"]).Trim(),
                                WxName = Convert.ToString(idr["WxName"]),
                                weixinID = Convert.ToString(idr["weixinID"]),
                                CellPhone = Convert.ToString(idr["CellPhone"]),
                                MainDepartment = Convert.ToInt32(idr["MainDepartment"]),
                                MainDepartmentName = MainDepartmentName,
                                Department = Convert.ToString(idr["Department"]),
                                DepartName = depName,
                                Tag = Convert.ToString(idr["Tag"]),
                                TagName = tagName,
                                Position = Convert.ToString(idr["Position"]),
                                Gender = Convert.ToString(idr["Gender"]),
                                Email = Convert.ToString(idr["Email"]),
                                AvatarSmall = Convert.ToString(idr["AvatarSmall"]),
                                AvatarBig = Convert.ToString(idr["AvatarBig"]),
                                Telephone = Convert.ToString(idr["Telephone"]),
                                Enable = Convert.ToString(idr["Enable"]),
                                English_name = Convert.ToString(idr["English_name"]),
                                QR_code = Convert.ToString(idr["QR_code"]),
                                //HouseID = Convert.ToInt32(idr["HouseID"]),
                                HouseID = string.IsNullOrEmpty(idr["HouseID"].ToString()) ? 0 : Convert.ToInt32(idr["HouseID"]),
                                CheckHouseID = string.IsNullOrEmpty(idr["CheckHouseID"].ToString()) ? "" : Convert.ToString(idr["CheckHouseID"]),
                                HouseName = Convert.ToString(idr["HouseName"]),
                                CheckHouseName = Convert.ToString(idr["CheckHouseName"]),
                                CheckRole = Convert.ToString(idr["CheckRole"]),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"])
                            });
                        }
                    }
                }
                resHT["rows"] = result;

                string strCount = @"Select Count(*) as TotalCount from Tbl_QY_User Where (1=1)";
                //以名称为查询条件
                if (!string.IsNullOrEmpty(entity.WxName)) { strCount += " and WxName like '%" + entity.WxName + "%'"; }
                if (!string.IsNullOrEmpty(entity.Department)) { strCount += " and Department like '%" + entity.Department + "%'"; }
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
        /// 获取所有微信用户数据
        /// </summary>
        /// <returns></returns>
        public List<QyUserEntity> QueryAllUserList()
        {
            List<QyUserEntity> resList = new List<QyUserEntity>();
            string strSQL = @"SELECT * FROM Tbl_QY_User";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dd = conn.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idr in dd.Rows)
                        {
                            QyUserEntity result = new QyUserEntity();
                            result.UserID = Convert.ToString(idr["UserID"]);
                            result.OpenID = Convert.ToString(idr["OpenID"]).Trim();
                            result.WxName = Convert.ToString(idr["WxName"]);
                            result.weixinID = Convert.ToString(idr["weixinID"]);
                            result.CellPhone = Convert.ToString(idr["CellPhone"]);
                            result.MainDepartment = Convert.ToInt32(idr["MainDepartment"]);
                            result.Department = Convert.ToString(idr["Department"]);
                            result.Tag = Convert.ToString(idr["Tag"]);
                            result.Position = Convert.ToString(idr["Position"]);
                            result.Gender = Convert.ToString(idr["Gender"]);
                            result.Email = Convert.ToString(idr["Email"]);
                            result.AvatarSmall = Convert.ToString(idr["AvatarSmall"]);
                            result.AvatarBig = Convert.ToString(idr["AvatarBig"]);
                            result.Telephone = Convert.ToString(idr["Telephone"]);
                            result.Enable = Convert.ToString(idr["Enable"]);
                            result.English_name = Convert.ToString(idr["English_name"]);
                            result.QR_code = Convert.ToString(idr["QR_code"]);
                            result.HouseID = string.IsNullOrEmpty(idr["HouseID"].ToString()) ? 0 : Convert.ToInt32(idr["HouseID"]);
                            result.CheckRole = Convert.ToString(idr["CheckRole"]);
                            result.OP_DATE = Convert.ToDateTime(idr["OP_DATE"]);
                            resList.Add(result);
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return resList;
        }
        /// <summary>
        /// 删除微信用户
        /// </summary>
        /// <param name="entity"></param>
        public void DelQYUser(List<QyUserEntity> entity)
        {
            try
            {
                foreach (var it in entity)
                {
                    string strSQL = @"Delete from Tbl_QY_User where UserID=@UserID ";

                    using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                    {
                        conn.AddInParameter(cmd, "@UserID", DbType.String, it.UserID);
                        conn.ExecuteNonQuery(cmd);
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 保存微信用户数据
        /// </summary>
        /// <param name="entity"></param>
        public void AddQYUser(QyUserEntity entity)
        {
            entity.EnSafe();
            string strSQL = @"INSERT INTO Tbl_QY_User(CheckHouseName,CheckHouseID,UserID,OpenID,WxName,weixinID,CellPhone,Department,MainDepartment,Tag,Position,Gender,Email,AvatarSmall,AvatarBig,HouseID,CheckRole) VALUES (@CheckHouseName,@CheckHouseID,@UserID,@OpenID,@WxName,@weixinID,@CellPhone,@Department,@MainDepartment,@Tag,@Position,@Gender,@Email,@AvatarSmall,@AvatarBig,@HouseID,@CheckRole)";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@UserID", DbType.String, entity.UserID);
                    conn.AddInParameter(cmd, "@OpenID", DbType.String, entity.OpenID);
                    conn.AddInParameter(cmd, "@WxName", DbType.String, entity.WxName);
                    conn.AddInParameter(cmd, "@weixinID", DbType.String, entity.weixinID);
                    conn.AddInParameter(cmd, "@CellPhone", DbType.String, entity.CellPhone);
                    conn.AddInParameter(cmd, "@Department", DbType.String, entity.Department);
                    conn.AddInParameter(cmd, "@MainDepartment", DbType.String, entity.MainDepartment);
                    conn.AddInParameter(cmd, "@Tag", DbType.String, entity.Tag);
                    conn.AddInParameter(cmd, "@Position", DbType.String, entity.Position);
                    conn.AddInParameter(cmd, "@Gender", DbType.String, entity.Gender);
                    conn.AddInParameter(cmd, "@Email", DbType.String, entity.Email);
                    conn.AddInParameter(cmd, "@AvatarSmall", DbType.String, entity.AvatarSmall);
                    conn.AddInParameter(cmd, "@AvatarBig", DbType.String, entity.AvatarBig);
                    conn.AddInParameter(cmd, "@HouseID", DbType.Int32, entity.HouseID);
                    conn.AddInParameter(cmd, "@CheckRole", DbType.String, entity.CheckRole);
                    conn.AddInParameter(cmd, "@CheckHouseID", DbType.String, entity.CheckHouseID);
                    conn.AddInParameter(cmd, "@CheckHouseName", DbType.String, entity.CheckHouseName);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }

        }
        /// <summary>
        /// 修改微信用户
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateQYUser(QyUserEntity entity)
        {
            string strSQL = @"UPDATE Tbl_QY_User set WxName=@WxName,Department=@Department,Tag=@Tag,CellPhone=@CellPhone,Email=@Email,Position=@Position,HouseID=@HouseID,CheckRole=@CheckRole,CheckHouseID=@CheckHouseID,CheckHouseName=@CheckHouseName WHERE UserID=@UserID";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@Position", DbType.String, entity.Position);
                    conn.AddInParameter(cmd, "@Email", DbType.String, entity.Email);
                    conn.AddInParameter(cmd, "@CellPhone", DbType.String, entity.CellPhone);
                    conn.AddInParameter(cmd, "@Department", DbType.String, entity.Department);
                    conn.AddInParameter(cmd, "@Tag", DbType.String, entity.Tag);
                    conn.AddInParameter(cmd, "@WxName", DbType.String, entity.WxName);
                    conn.AddInParameter(cmd, "@UserID", DbType.String, entity.UserID);
                    conn.AddInParameter(cmd, "@HouseID", DbType.Int32, entity.HouseID);
                    conn.AddInParameter(cmd, "@CheckRole", DbType.String, entity.CheckRole);
                    conn.AddInParameter(cmd, "@CheckHouseID", DbType.String, entity.CheckHouseID);
                    conn.AddInParameter(cmd, "@CheckHouseName", DbType.String, entity.CheckHouseName);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 根据UserID判断是否存在用户
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public bool IsExistQYUser(string userID)
        {
            string strSQL = @"select UserID from Tbl_QY_User where UserID=@UserID";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@UserID", DbType.String, userID);
                    using (DataTable idr = conn.ExecuteDataTable(cmd))
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
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return true;
        }
        /// <summary>
        /// 查询微信用户数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public QyUserEntity QueryUser(QyUserEntity entity)
        {
            QyUserEntity result = new QyUserEntity();
            try
            {
                string strSQL = @"select * from Tbl_QY_User where UserID=@UserID";
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(command, "@UserID", DbType.String, entity.UserID);
                    using (DataTable dd = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dd.Rows)
                        {
                            result.UserID = Convert.ToString(idr["UserID"]);
                            result.WxName = Convert.ToString(idr["WxName"]);
                            result.AvatarSmall = Convert.ToString(idr["AvatarSmall"]);
                            result.AvatarBig = Convert.ToString(idr["AvatarBig"]);
                            result.OP_DATE = Convert.ToDateTime(idr["OP_DATE"]);
                            result.OpenID = Convert.ToString(idr["OpenID"]);
                            result.weixinID = Convert.ToString(idr["weixinID"]);
                            result.CellPhone = Convert.ToString(idr["CellPhone"]);
                            result.Department = Convert.ToString(idr["Department"]);
                            result.Tag = Convert.ToString(idr["Tag"]);
                            result.Position = Convert.ToString(idr["Position"]);
                            result.Gender = Convert.ToString(idr["Gender"]);
                            result.Email = Convert.ToString(idr["Email"]);
                            result.Telephone = Convert.ToString(idr["Telephone"]);
                            result.Enable = Convert.ToString(idr["Enable"]);
                            result.English_name = Convert.ToString(idr["English_name"]);
                            result.QR_code = Convert.ToString(idr["QR_code"]);
                            result.CheckRole = Convert.ToString(idr["CheckRole"]);
                            result.CheckHouseID = Convert.ToString(idr["CheckHouseID"]);
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
        /// 查询微信企业用户的信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public QyUserStatisEntity QueryWXQyUserStatis(QyUserStatisEntity entity)
        {
            QyUserStatisEntity result = new QyUserStatisEntity();
            //查找微信用户信息
            QyUserEntity user = QueryUser(new QyUserEntity { UserID = entity.UserID });
            QyUserStatisEntity qus = QueryStatis(entity);
            result.UserID = user.UserID;
            result.WxName = user.WxName;
            result.AvatarSmall = user.AvatarSmall;
            if (!string.IsNullOrEmpty(qus.UserID))
            {
                result.TodayPiece = qus.TodayPiece;
                result.TodayCharge = qus.TodayCharge;
                result.MonthPiece = qus.MonthPiece;
                result.MonthCharge = qus.MonthCharge;
            }
            return result;
        }
        /// <summary>
        /// 统计该微信用户的每日订单和每月订单数
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public QyUserStatisEntity QueryStatis(QyUserStatisEntity entity)
        {
            QyUserStatisEntity result = new QyUserStatisEntity();
            try
            {
                string strSQL = @"select a.SaleManID,ISNULL(a.TodayCharge,0) as TodayCharge,ISNULL(a.TodayPiece,0) as TodayPiece,
ISNULL(b.MonthPiece,0) as MonthPiece,ISNULL(b.MonthCharge,0) as MonthCharge from (select SaleManID,SUM(Piece) as TodayPiece,SUM(TotalCharge) as TodayCharge from Tbl_Cargo_Order where SaleManID=@SaleManID ";

                strSQL += " and CreateDate>='" + entity.OP_DATE.ToString("yyyy-MM-dd") + "'";
                strSQL += " and CreateDate<'" + entity.OP_DATE.AddDays(1).ToString("yyyy-MM-dd") + "'";

                strSQL += " group by SaleManID )as a left join (select SaleManID,SUM(Piece) as MonthPiece,SUM(TotalCharge) as MonthCharge from Tbl_Cargo_Order where SaleManID=@SaleManID ";

                strSQL += " and CreateDate>='" + entity.OP_DATE.AddDays(1 - DateTime.Now.Day).ToString("yyyy-MM-dd") + "'";
                strSQL += " and CreateDate<'" + entity.OP_DATE.AddDays(1).ToString("yyyy-MM-dd") + "'";

                strSQL += "  group by SaleManID) as b on a.SaleManID=b.SaleManID ";

                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@SaleManID", DbType.String, entity.UserID);

                    using (DataTable dt = conn.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idr in dt.Rows)
                        {
                            result.UserID = Convert.ToString(idr["SaleManID"]);
                            result.TodayPiece = Convert.ToInt32(idr["TodayPiece"]);
                            result.TodayCharge = Convert.ToDecimal(idr["TodayCharge"]);
                            result.MonthPiece = Convert.ToInt32(idr["MonthPiece"]);
                            result.MonthCharge = Convert.ToDecimal(idr["MonthCharge"]);
                        }
                    }
                }
                //DateTime.Now.AddDays(1 - DateTime.Now.Day)
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }

            return result;
        }
        /// <summary>
        /// 查询所有企业用户数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<QyUserEntity> QueryUserList(QyUserEntity entity)
        {
            List<QyUserEntity> result = new List<QyUserEntity>();
            string strSQL = "Select * From Tbl_QY_User Where (1=1)";
            if (!string.IsNullOrEmpty(entity.CheckRole)) { strSQL += " and CHARINDEX(','+'" + entity.CheckRole + "'+',' , ','+CheckRole+',') > 0"; }
            if (!string.IsNullOrEmpty(entity.Department)) { strSQL += " and CHARINDEX(','+'" + entity.Department + "'+',' , ','+Department+',') > 0"; }
            if (!string.IsNullOrEmpty(entity.CheckHouseID)) { strSQL += " and CHARINDEX(','+'" + entity.CheckHouseID + "'+',' , ','+CheckHouseID+',') > 0"; }
            if (!entity.HouseID.Equals(0)) { strSQL += " and HouseID=" + entity.HouseID; }
            using (DbCommand command = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dd = conn.ExecuteDataTable(command))
                {
                    foreach (DataRow idr in dd.Rows)
                    {
                        result.Add(new QyUserEntity
                        {
                            UserID = Convert.ToString(idr["UserID"]),
                            OpenID = Convert.ToString(idr["OpenID"]),
                            WxName = Convert.ToString(idr["WxName"]),
                            CellPhone = Convert.ToString(idr["CellPhone"]),
                            AvatarBig = Convert.ToString(idr["AvatarBig"])
                        });
                    }
                }
            }
            return result;
        }
        #endregion
        #region 标签管理

        /// <summary>
        /// 同步标签列表数据
        /// </summary>
        /// <param name="entity"></param>
        public void SyncTag(List<QyTagEntity> entity)
        {
            //truncateTable("Tbl_QY_Tag");
            foreach (var it in entity)
            {
                //string strSQL = @"INSERT INTO Tbl_QY_Tag(Id,Name) VALUES (@Id,@Name)";
                string strSQL = @"UPDATE Tbl_QY_Tag set Name=@Name where Id=@Id";
                try
                {
                    using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                    {
                        conn.AddInParameter(cmd, "@Name", DbType.String, it.Name);
                        conn.AddInParameter(cmd, "@Id", DbType.Int32, it.Id);
                        conn.ExecuteNonQuery(cmd);
                    }
                }
                catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            }
        }
        /// <summary>
        /// 保存标签
        /// </summary>
        /// <param name="entity"></param>
        public void AddQYTag(QyTagEntity entity)
        {
            string strSQL = @"INSERT INTO Tbl_QY_Tag(Id,Name,TagType,HouseID) VALUES (@Id,@Name,@TagType,@HouseID)";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@TagType", DbType.String, entity.TagType);
                    conn.AddInParameter(cmd, "@Name", DbType.String, entity.Name);
                    conn.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
                    conn.AddInParameter(cmd, "@HouseID", DbType.Int32, entity.HouseID);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 修改标签
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateQYTag(QyTagEntity entity)
        {
            string strSQL = @"UPDATE Tbl_QY_Tag set Name=@Name,TagType=@TagType,HouseID=@HouseID WHERE Id=@Id";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@TagType", DbType.String, entity.TagType);
                    conn.AddInParameter(cmd, "@Name", DbType.String, entity.Name);
                    conn.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
                    conn.AddInParameter(cmd, "@HouseID", DbType.Int32, entity.HouseID);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }

        }
        /// <summary>
        /// 查询标签
        /// </summary>
        /// <param name="pIndex"></param>
        /// <param name="pNum"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Hashtable queryTag(int pIndex, int pNum, QyTagEntity entity)
        {
            List<QyTagEntity> result = new List<QyTagEntity>();
            Hashtable resHT = new Hashtable();
            try
            {
                string strSQL = @" SELECT TOP " + pNum + " * from (select ROW_NUMBER() OVER (ORDER BY a.OP_DATE DESC) AS RowNumber,a.*,b.Name as HouseName FROM Tbl_QY_Tag as a left join Tbl_Cargo_House as b on a.HouseID=b.HouseID WHERE (1=1)";
                //以名称为查询条件
                if (!string.IsNullOrEmpty(entity.Name)) { strSQL += " and a.Name like '%" + entity.Name + "%'"; }
                if (!string.IsNullOrEmpty(entity.CargoPermisID))
                {
                    strSQL += " and a.HouseID = " + entity.CargoPermisID + "";
                }
                strSQL += " ) as A where RowNumber > (" + pNum + "* (" + pIndex + "-1))";
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dd = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dd.Rows)
                        {
                            result.Add(new QyTagEntity
                            {
                                Id = Convert.ToInt32(idr["Id"]),
                                Name = Convert.ToString(idr["Name"]).Trim(),
                                TagType = Convert.ToString(idr["TagType"]),
                                HouseID = Convert.ToInt32(idr["HouseID"]),
                                HouseName = Convert.ToString(idr["HouseName"]),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"])
                            });
                        }
                    }
                }
                resHT["rows"] = result;

                string strCount = @"Select Count(*) as TotalCount from Tbl_QY_Tag Where (1=1)";
                //以名称为查询条件
                if (!string.IsNullOrEmpty(entity.Name)) { strCount += " and Name like '%" + entity.Name + "%'"; }
                if (!string.IsNullOrEmpty(entity.CargoPermisID))
                {
                    strCount += " and HouseID = " + entity.CargoPermisID + "";
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
        /// 查询标签
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<QyTagEntity> QueryTagList(QyTagEntity entity)
        {
            List<QyTagEntity> resList = new List<QyTagEntity>();
            string strSQL = @"SELECT * FROM Tbl_QY_Tag Where (1=1) ";
            if (!string.IsNullOrEmpty(entity.TagType)) { strSQL += " and TagType='" + entity.TagType + "'"; }
            if (!entity.HouseID.Equals(0)) { strSQL += " and HouseID=" + entity.HouseID + ""; }
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dd = conn.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idr in dd.Rows)
                        {
                            QyTagEntity result = new QyTagEntity();
                            result.Name = Convert.ToString(idr["Name"]).Trim();
                            result.TagType = Convert.ToString(idr["TagType"]);
                            result.Id = Convert.ToInt32(idr["Id"]);
                            result.HouseID = Convert.ToInt32(idr["HouseID"]);
                            resList.Add(result);
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return resList;
        }
        /// <summary>
        /// 根据ID查询标签
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public QyTagEntity QueryTagByID(QyTagEntity entity)
        {
            QyTagEntity result = new QyTagEntity();
            string strSQL = @"SELECT * FROM Tbl_QY_Tag Where Id=@Id";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
                    using (DataTable dd = conn.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idr in dd.Rows)
                        {
                            result.Name = Convert.ToString(idr["Name"]).Trim();
                            result.Id = Convert.ToInt32(idr["Id"]);
                            result.TagType = Convert.ToString(idr["TagType"]);
                            result.HouseID = Convert.ToInt32(idr["HouseID"]);
                            result.OP_DATE = Convert.ToDateTime(idr["OP_DATE"]);
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        /// <summary>
        /// 删除标签
        /// </summary>
        /// <param name="entity"></param>
        public void DelQYTag(List<QyTagEntity> entity)
        {
            try
            {
                foreach (var it in entity)
                {
                    string strSQL = @"Delete from Tbl_QY_Tag where Id=@Id ";
                    using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                    {
                        conn.AddInParameter(cmd, "@Id", DbType.String, it.Id);
                        conn.ExecuteNonQuery(cmd);
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        #endregion
        #region 企业号配置管理
        /// <summary>
        /// 查询企业号配置信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public QyConfigEntity QueryQyConfig(QyConfigEntity entity)
        {
            QyConfigEntity result = new QyConfigEntity();
            string strSQL = @"SELECT * FROM Tbl_QY_Config Where QYKind=@QYKind";
            if (!string.IsNullOrEmpty(entity.WorkClass)) { strSQL += " and WorkClass=@WorkClass "; }
            if (!string.IsNullOrEmpty(entity.SendType)) { strSQL += " and SendType=@SendType "; }
            if (!string.IsNullOrEmpty(entity.AgentID)) { strSQL += " and AgentID=@AgentID "; }
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    if (!string.IsNullOrEmpty(entity.WorkClass))
                    {
                        conn.AddInParameter(cmd, "@WorkClass", DbType.String, entity.WorkClass);
                    }
                    if (!string.IsNullOrEmpty(entity.SendType))
                    {
                        conn.AddInParameter(cmd, "@SendType", DbType.String, entity.SendType);
                    }
                    if (!string.IsNullOrEmpty(entity.AgentID))
                    {
                        conn.AddInParameter(cmd, "@AgentID", DbType.String, entity.AgentID);
                    }
                    conn.AddInParameter(cmd, "@QYKind", DbType.String, entity.QYKind);
                    using (DataTable dd = conn.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idr in dd.Rows)
                        {
                            result.ID = Convert.ToInt32(idr["ID"]);
                            result.SendType = Convert.ToString(idr["SendType"]);
                            result.WorkClass = Convert.ToString(idr["WorkClass"]);
                            result.AgentID = Convert.ToString(idr["AgentID"]);
                            result.AppSecret = Convert.ToString(idr["AppSecret"]);
                            result.QYKind = Convert.ToString(idr["QYKind"]);
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        /// <summary>
        /// 查询企业号配置数据
        /// </summary>
        /// <param name="pIndex"></param>
        /// <param name="pNum"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Hashtable QueryQyConfig(int pIndex, int pNum, QyConfigEntity entity)
        {
            List<QyConfigEntity> result = new List<QyConfigEntity>();
            Hashtable resHT = new Hashtable();
            try
            {
                string strSQL = @" SELECT TOP " + pNum + " * from (select ROW_NUMBER() OVER (ORDER BY OP_DATE DESC) AS RowNumber,* FROM Tbl_QY_Config WHERE (1=1)";
                //以名称为查询条件
                if (!string.IsNullOrEmpty(entity.WorkClass)) { strSQL += " and WorkClass = '" + entity.WorkClass + "'"; }
                if (!string.IsNullOrEmpty(entity.QYKind)) { strSQL += " and QYKind = '" + entity.QYKind + "'"; }
                strSQL += " ) as A where RowNumber > (" + pNum + "* (" + pIndex + "-1))";
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dd = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dd.Rows)
                        {
                            result.Add(new QyConfigEntity
                            {
                                ID = Convert.ToInt32(idr["ID"]),
                                SendType = Convert.ToString(idr["SendType"]).Trim(),
                                WorkClass = Convert.ToString(idr["WorkClass"]),
                                AgentID = Convert.ToString(idr["AgentID"]),
                                AppSecret = Convert.ToString(idr["AppSecret"]),
                                QYKind = Convert.ToString(idr["QYKind"]),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"])
                            });
                        }
                    }
                }
                resHT["rows"] = result;

                string strCount = @"Select Count(*) as TotalCount from Tbl_QY_Config Where (1=1)";
                //以名称为查询条件
                if (!string.IsNullOrEmpty(entity.WorkClass)) { strCount += " and WorkClass = '" + entity.WorkClass + "'"; }
                if (!string.IsNullOrEmpty(entity.QYKind)) { strCount += " and QYKind = '" + entity.QYKind + "'"; }
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
        /// 新增企业号配置数据
        /// </summary>
        /// <param name="entity"></param>
        public void AddQYConfig(QyConfigEntity entity)
        {
            string strSQL = @"INSERT INTO Tbl_QY_Config(SendType,WorkClass,AgentID,AppSecret,QYKind) VALUES (@SendType,@WorkClass,@AgentID,@AppSecret,@QYKind)";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@SendType", DbType.String, entity.SendType);
                    conn.AddInParameter(cmd, "@WorkClass", DbType.String, entity.WorkClass);
                    conn.AddInParameter(cmd, "@AgentID", DbType.String, entity.AgentID);
                    conn.AddInParameter(cmd, "@AppSecret", DbType.String, entity.AppSecret);
                    conn.AddInParameter(cmd, "@QYKind", DbType.String, entity.QYKind);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }

        }
        /// <summary>
        /// 删除企业微信配置
        /// </summary>
        /// <param name="entity"></param>
        public void DelQYConfig(List<QyConfigEntity> entity)
        {
            foreach (var it in entity)
            {
                string strSQL = @"Delete from Tbl_QY_Config where ID=@ID ";
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@ID", DbType.String, it.ID);
                    conn.ExecuteNonQuery(cmd);
                }
            }
        }
        #endregion
        #region 订单价格修改
        /// <summary>
        /// 修改订单价格申请
        /// </summary>
        /// <param name="entity"></param>
        public long AddOrderUpdatePrice(QyOrderUpdatePriceEntity entity)
        {
            entity.EnSafe();
            long DID = 0;
            string strSQL = "INSERT INTO Tbl_QY_OrderUpdatePrice(OrderID,OrderNo,HouseID,ApplyID,ApplyName,ApplyDate,Reason,CheckID,CheckName,ApplyStatus,OrderType,SaleManID,SaleManName,OrderCheckType) VALUES (@OrderID,@OrderNo,@HouseID,@ApplyID,@ApplyName,@ApplyDate,@Reason,@CheckID,@CheckName,@ApplyStatus,@OrderType,@SaleManID,@SaleManName,@OrderCheckType) SELECT @@IDENTITY";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@OrderID", DbType.Int64, entity.OrderID);
                conn.AddInParameter(cmd, "@OrderNo", DbType.String, entity.OrderNo);
                conn.AddInParameter(cmd, "@HouseID", DbType.Int32, entity.HouseID);
                conn.AddInParameter(cmd, "@ApplyID", DbType.String, entity.ApplyID);
                conn.AddInParameter(cmd, "@ApplyName", DbType.String, entity.ApplyName);
                conn.AddInParameter(cmd, "@ApplyDate", DbType.DateTime, entity.ApplyDate);
                conn.AddInParameter(cmd, "@Reason", DbType.String, entity.Reason);
                conn.AddInParameter(cmd, "@CheckID", DbType.String, entity.CheckID);
                conn.AddInParameter(cmd, "@CheckName", DbType.String, entity.CheckName);
                conn.AddInParameter(cmd, "@ApplyStatus", DbType.String, entity.ApplyStatus);
                conn.AddInParameter(cmd, "@OrderType", DbType.String, entity.OrderType);
                conn.AddInParameter(cmd, "@SaleManID", DbType.String, entity.SaleManID);
                conn.AddInParameter(cmd, "@SaleManName", DbType.String, entity.SaleManName);
                conn.AddInParameter(cmd, "@OrderCheckType", DbType.String, entity.OrderCheckType);
                //conn.ExecuteNonQuery(cmd);
                DID = Convert.ToInt64(conn.ExecuteScalar(cmd));
            }
            //新增明细
            AddOrderUpdateGoods(entity.UpdatePriceGoodsList, DID);
            return DID;
        }
        /// <summary>
        /// 新增 订单修改价格申请明细
        /// </summary>
        /// <param name="entity"></param>
        public void AddOrderUpdateGoods(List<QyOrderUpdateGoodsEntity> entity, long UID)
        {
            foreach (var it in entity)
            {
                it.EnSafe();
                string strSQL = "INSERT INTO Tbl_QY_OrderUpdateGoods(OID,OrderID,ShelvesID,OrderNum,OrderPrice,ModifyPrice,ProductID,ContainerCode) VALUES (@OID,@OrderID,@ShelvesID,@OrderNum,@OrderPrice,@ModifyPrice,@ProductID,@ContainerCode)";
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@OID", DbType.Int64, UID);
                    conn.AddInParameter(cmd, "@OrderID", DbType.Int64, it.OrderID);
                    conn.AddInParameter(cmd, "@ShelvesID", DbType.Int64, it.ShelvesID);
                    conn.AddInParameter(cmd, "@OrderNum", DbType.Int32, it.OrderNum);
                    conn.AddInParameter(cmd, "@OrderPrice", DbType.Decimal, it.OrderPrice);
                    conn.AddInParameter(cmd, "@ModifyPrice", DbType.Decimal, it.ModifyPrice);
                    conn.AddInParameter(cmd, "@ProductID", DbType.Int64, it.ProductID);
                    conn.AddInParameter(cmd, "@ContainerCode", DbType.String, it.ContainerCode);
                    conn.ExecuteNonQuery(cmd);
                }
            }
        }
        /// <summary>
        /// 审批
        /// </summary>
        /// <param name="entity"></param>
        public void CheckUpdatePrice(QyOrderUpdatePriceEntity entity)
        {
            entity.EnSafe();
            string strSQL = "Update Tbl_QY_OrderUpdatePrice set CheckID=@CheckID,CheckName=@CheckName,CheckTime=@CheckTime,CheckResult=@CheckResult,ApplyStatus=@ApplyStatus Where OID=@OID";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@OID", DbType.Int64, entity.OID);
                conn.AddInParameter(cmd, "@CheckID", DbType.String, entity.CheckID);
                conn.AddInParameter(cmd, "@CheckName", DbType.String, entity.CheckName);
                conn.AddInParameter(cmd, "@CheckTime", DbType.DateTime, entity.CheckTime);
                conn.AddInParameter(cmd, "@CheckResult", DbType.String, entity.CheckResult);
                conn.AddInParameter(cmd, "@ApplyStatus", DbType.String, entity.ApplyStatus);
                conn.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 根据订单ID查询该订单的产品数据
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public List<QyOrderUpdateGoodsEntity> queryOrderUpdateGoodsByOID(long orderID)
        {
            List<QyOrderUpdateGoodsEntity> result = new List<QyOrderUpdateGoodsEntity>();
            try
            {
                string strSQL = @"select a.OID,a.OrderID,a.ShelvesID,a.OrderNum,a.OrderPrice,a.ModifyPrice,b.Title,c.Batch,c.CostPrice,c.FinalCostPrice,c.TypeID from Tbl_QY_OrderUpdateGoods as a left join Tbl_Cargo_Shelves as b on a.ShelvesID=b.ID left join Tbl_Cargo_Product as c on a.ProductID=c.ProductID where a.OID=@OID";

                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@OID", DbType.Int64, orderID);
                    using (DataTable dd = conn.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idr in dd.Rows)
                        {
                            result.Add(new QyOrderUpdateGoodsEntity
                            {
                                OID = Convert.ToInt64(idr["OID"]),
                                OrderID = Convert.ToInt64(idr["OrderID"]),
                                ShelvesID = Convert.ToInt64(idr["ShelvesID"]),
                                OrderNum = Convert.ToInt32(idr["OrderNum"]),
                                OrderPrice = Convert.ToDecimal(idr["OrderPrice"]),
                                ModifyPrice = Convert.ToDecimal(idr["ModifyPrice"]),
                                CostPrice = Convert.ToDecimal(idr["CostPrice"]),
                                FinalCostPrice = Convert.ToDecimal(idr["FinalCostPrice"]),
                                TypeID = Convert.ToInt32(idr["TypeID"]),
                                Title = Convert.ToString(idr["Title"]),
                                Batch = Convert.ToString(idr["Batch"])
                            });
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        /// <summary>
        /// 根据订单ID查询该订单的产品数据
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public List<QyOrderUpdateGoodsEntity> queryOrderUpdateGoodsByOIDComputer(long orderID)
        {
            List<QyOrderUpdateGoodsEntity> result = new List<QyOrderUpdateGoodsEntity>();
            try
            {
                string strSQL = @"select a.OID,a.OrderNum,a.OrderPrice,a.ModifyPrice,c.ProductID,c.Batch,c.Specs,c.Figure,c.Model,c.GoodsCode,c.LoadIndex,c.SpeedLevel,c.TypeID,c.UnitPrice,c.CostPrice,c.TaxCostPrice,c.FinalCostPrice,b.TypeName,b.ParentID as TypeParentID from Tbl_QY_OrderUpdateGoods as a inner join Tbl_Cargo_Product as c on a.ProductID=c.ProductID left join Tbl_Cargo_ProductType as b on c.TypeID=b.TypeID where a.OID=@OID";

                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@OID", DbType.Int64, orderID);
                    using (DataTable dd = conn.ExecuteDataTable(cmd))
                    {
                        foreach (DataRow idr in dd.Rows)
                        {
                            result.Add(new QyOrderUpdateGoodsEntity
                            {
                                OID = Convert.ToInt64(idr["OID"]),
                                TypeID = Convert.ToInt32(idr["TypeID"]),
                                TypeParentID = Convert.ToInt32(idr["TypeParentID"]),
                                ProductID = Convert.ToInt64(idr["ProductID"]),
                                LoadIndex = Convert.ToString(idr["LoadIndex"]),
                                OrderNum = Convert.ToInt32(idr["OrderNum"]),
                                OrderPrice = Convert.ToDecimal(idr["OrderPrice"]),
                                ModifyPrice = Convert.ToDecimal(idr["ModifyPrice"]),
                                UnitPrice = Convert.ToDecimal(idr["UnitPrice"]),
                                CostPrice = Convert.ToDecimal(idr["CostPrice"]),
                                TaxCostPrice = Convert.ToDecimal(idr["TaxCostPrice"]),
                                FinalCostPrice = Convert.ToDecimal(idr["FinalCostPrice"]),
                                Specs = Convert.ToString(idr["Specs"]),
                                Figure = Convert.ToString(idr["Figure"]),
                                Model = Convert.ToString(idr["Model"]),
                                GoodsCode = Convert.ToString(idr["GoodsCode"]),
                                SpeedLevel = Convert.ToString(idr["SpeedLevel"]),
                                TypeName = Convert.ToString(idr["TypeName"]),
                                Title = Convert.ToString(idr["TypeName"]) + Convert.ToString(idr["Specs"]) + " " + Convert.ToString(idr["Figure"]) + " " + Convert.ToString(idr["LoadIndex"]) + Convert.ToString(idr["SpeedLevel"]),
                                Batch = Convert.ToString(idr["Batch"])
                            });
                        }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        /// <summary>
        /// 查询修改订单申请数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public QyOrderUpdatePriceEntity QueryOrderUpdatePriceEntity(QyOrderUpdatePriceEntity entity)
        {
            QyOrderUpdatePriceEntity result = new QyOrderUpdatePriceEntity();
            string strSQL = "Select * from Tbl_QY_OrderUpdatePrice Where (1=1)";
            if (!entity.OID.Equals(0)) { strSQL += " and OID=" + entity.OID; }
            if (!entity.OrderID.Equals(0)) { strSQL += " and OrderID=" + entity.OrderID; }
            if (!string.IsNullOrEmpty(entity.OrderNo)) { strSQL += " and OrderNo='" + entity.OrderNo + "'"; }
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dd = conn.ExecuteDataTable(cmd))
                {
                    foreach (DataRow idr in dd.Rows)
                    {
                        result.OrderID = Convert.ToInt64(idr["OrderID"]);
                        result.OID = Convert.ToInt64(idr["OID"]);
                        result.HouseID = Convert.ToInt32(idr["HouseID"]);
                        result.OrderNo = Convert.ToString(idr["OrderNo"]);
                        result.ApplyID = Convert.ToString(idr["ApplyID"]);
                        result.ApplyName = Convert.ToString(idr["ApplyName"]);
                        result.ApplyStatus = Convert.ToString(idr["ApplyStatus"]);
                        result.ApplyDate = Convert.ToDateTime(idr["ApplyDate"]);
                        result.CheckID = Convert.ToString(idr["CheckID"]);
                        result.CheckName = Convert.ToString(idr["CheckName"]);
                        result.CheckResult = Convert.ToString(idr["CheckResult"]);
                        result.CheckTime = string.IsNullOrEmpty(Convert.ToString(idr["CheckTime"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["CheckTime"]);
                        result.OrderType = Convert.ToString(idr["OrderType"]);
                        result.Reason = Convert.ToString(idr["Reason"]);
                        result.SaleManName = Convert.ToString(idr["SaleManName"]);
                        result.SaleManID = Convert.ToString(idr["SaleManID"]);
                        result.OrderCheckType = Convert.ToString(idr["OrderCheckType"]);
                        result.OP_DATE = Convert.ToDateTime(idr["OP_DATE"]);
                    }
                }
            }
            return result;
        }
        #endregion
        /// <summary>
        /// 查询未发货的订单信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<CargoOrderEntity> QueryUnSendOrderInfo(CargoOrderEntity entity)
        {
            List<CargoOrderEntity> result = new List<CargoOrderEntity>();
            string strSQL = "select a.HouseID,e.Name as HouseName,a.OrderNo,a.Dep,a.Dest,a.AcceptPeople,a.AcceptUnit,a.CreateAwb,a.Piece,a.CreateDate,a.LogisID From Tbl_Cargo_Order as a inner join Tbl_Cargo_OrderGoods as b on a.OrderNo=b.OrderNo inner join Tbl_Cargo_Product as c on b.ProductID=c.ProductID inner join Tbl_Cargo_ProductType as d on c.TypeID=d.TypeID inner join Tbl_Cargo_House as e on a.HouseID=e.HouseID where d.ParentID in (1,10) ";
            if (!string.IsNullOrEmpty(entity.CargoPermisID))
            {
                strSQL += " and a.HouseID in (" + entity.CargoPermisID + ")";
            }
            //制单日期范围
            if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
            {
                strSQL += " and a.CreateDate>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
            }
            if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
            {
                strSQL += " and a.CreateDate<'" + entity.EndDate.ToString("yyyy-MM-dd") + "'";
            }
            //2021-11-25添加and PostponeShip=1过滤掉延迟推送的订单
            if (!string.IsNullOrEmpty(entity.PostponeShip))
            {
                strSQL += " and a.PostponeShip='" + entity.PostponeShip + "'";
            }
            if (!string.IsNullOrEmpty(entity.BelongHouse))
            {
                strSQL += " and a.BelongHouse='" + entity.BelongHouse + "'";
            }
            if (!string.IsNullOrEmpty(entity.OrderModel))
            {
                strSQL += " and a.OrderModel='" + entity.OrderModel + "'";
            }
            strSQL += " and (a.AwbStatus=0 or a.AwbStatus=1) and a.TrafficType=0 group by a.HouseID,e.Name,a.OrderNo,a.Dep,a.Dest,a.AcceptPeople,a.AcceptUnit,a.CreateAwb,a.Piece,a.CreateDate,a.LogisID order by e.Name ";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dd = conn.ExecuteDataTable(cmd))
                {
                    foreach (DataRow idr in dd.Rows)
                    {
                        result.Add(new CargoOrderEntity
                        {
                            HouseID = Convert.ToInt32(idr["HouseID"]),
                            HouseName = Convert.ToString(idr["HouseName"]),
                            OrderNo = Convert.ToString(idr["OrderNo"]),
                            Dep = Convert.ToString(idr["Dep"]),
                            Dest = Convert.ToString(idr["Dest"]),
                            AcceptPeople = Convert.ToString(idr["AcceptPeople"]),
                            AcceptUnit = Convert.ToString(idr["AcceptUnit"]),
                            CreateAwb = Convert.ToString(idr["CreateAwb"]),
                            Piece = Convert.ToInt32(idr["Piece"]),
                            LogisID = Convert.ToInt32(idr["LogisID"]),
                            CreateDate = Convert.ToDateTime(idr["CreateDate"])
                        });
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 查询配置推送数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<QyPushConfigEntity> QueryPushConfigList(QyPushConfigEntity entity)
        {
            List<QyPushConfigEntity> result = new List<QyPushConfigEntity>();
            string strSQL = "select * From Tbl_QY_PushConfig Where QYKind=@QYKind";
            if (!string.IsNullOrEmpty(entity.PushType))
            {
                strSQL += " and PushType='" + entity.PushType + "'";
            }
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@QYKind", DbType.String, entity.QYKind);
                using (DataTable dd = conn.ExecuteDataTable(cmd))
                {
                    foreach (DataRow idr in dd.Rows)
                    {
                        result.Add(new QyPushConfigEntity
                        {
                            PID = Convert.ToInt32(idr["PID"]),
                            QYKind = Convert.ToString(idr["QYKind"]),
                            HouseID = Convert.ToInt32(idr["HouseID"]),
                            PushType = Convert.ToString(idr["PushType"]),
                            PushPerson = Convert.ToString(idr["PushPerson"])
                        });

                    }
                }
            }
            return result;
        }
        #region 群聊管理
        /// <summary>
        /// 新增群聊
        /// </summary>
        /// <param name="entity"></param>
        public void AddGroupChat(QyGroupChatEntity entity)
        {
            string strSQL = "Insert into Tbl_QY_GroupChat(ChatID,HouseID,ChatType,ChatName,Owner) Values (@ChatID,@HouseID,@ChatType,@ChatName,@Owner)";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                //conn.AddInParameter(cmd, "@ID", DbType.Int32, entity.ID);
                conn.AddInParameter(cmd, "@ChatID", DbType.String, entity.ChatID);
                conn.AddInParameter(cmd, "@HouseID", DbType.Int32, entity.HouseID);
                conn.AddInParameter(cmd, "@ChatType", DbType.String, entity.ChatType);
                conn.AddInParameter(cmd, "@ChatName", DbType.String, entity.ChatName);
                conn.AddInParameter(cmd, "@Owner", DbType.String, entity.Owner);
                conn.ExecuteNonQuery(cmd);
            }
        }
        /// <summary>
        /// 查询群聊数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public QyGroupChatEntity QueryGroupChatEntity(QyGroupChatEntity entity)
        {
            QyGroupChatEntity result = new QyGroupChatEntity();
            string strSQL = "Select * from Tbl_QY_GroupChat Where (1=1) ";
            if (!entity.HouseID.Equals(0)) { strSQL += " and HouseID=" + entity.HouseID; }
            if (!string.IsNullOrEmpty(entity.ChatType)) { strSQL += " and ChatType='" + entity.ChatType + "'"; }
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dt = conn.ExecuteDataTable(cmd))
                {
                    foreach (DataRow idr in dt.Rows)
                    {
                        result.ID = Convert.ToInt32(idr["ID"]);
                        result.HouseID = Convert.ToInt32(idr["HouseID"]);
                        result.ChatID = Convert.ToString(idr["ChatID"]);
                        result.ChatName = Convert.ToString(idr["ChatName"]);
                        result.ChatType = Convert.ToString(idr["ChatType"]);
                        result.Owner = Convert.ToString(idr["Owner"]);
                        result.OP_DATE = Convert.ToDateTime(idr["OP_DATE"]);
                    }
                }
            }
            return result;
        }
        #endregion

        #region 考勤管理
        /// <summary>
        /// 查询考勤数据
        /// </summary>
        /// <param name="pIndex"></param>
        /// <param name="pNum"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Hashtable QueryCheckinData(int pIndex, int pNum, QYCheckinDataEntity entity)
        {
            List<QYCheckinDataEntity> result = new List<QYCheckinDataEntity>();
            Hashtable resHT = new Hashtable();

            try
            {
                string strSQL = @" SELECT TOP " + pNum + " * from (select ROW_NUMBER() OVER (ORDER BY a.CheckinTime DESC) AS RowNumber,c.id as DepartmentID,c.Name as DepartmentName,b.WxName as UserName, a.* from Tbl_QY_CheckinData a inner join Tbl_QY_User b on a.UserID=b.UserID inner join Tbl_QY_Depart c on case when(charindex(',',Department)=0)then Department else substring(Department,0,charindex(',',Department)) end=c.Id WHERE (1=1)";

                if (!string.IsNullOrEmpty(entity.CheckinType))
                {
                    strSQL += " and a.CheckinType = '" + entity.CheckinType + "'";
                }
                if (!string.IsNullOrEmpty(entity.ExceptionType))
                {
                    strSQL += " and a.ExceptionType = '" + entity.ExceptionType + "'";
                }
                if (!string.IsNullOrEmpty(entity.UserName))
                {
                    strSQL += " and b.WxName like '%" + entity.UserName + "%'";
                }
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.CheckinTime>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.CheckinTime<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
                }
                strSQL += ") as A where RowNumber > (" + pNum + "* (" + pIndex + "-1))";

                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dd = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dd.Rows)
                        {
                            result.Add(new QYCheckinDataEntity
                            {
                                ID = Convert.ToInt32(idr["ID"]),
                                UserID = Convert.ToString(idr["UserID"]),
                                UserName = Convert.ToString(idr["UserName"]),
                                DepartmentID = Convert.ToString(idr["DepartmentID"]),
                                DepartmentName = Convert.ToString(idr["DepartmentName"]),
                                GroupName = Convert.ToString(idr["GroupName"]),
                                CheckinType = Convert.ToString(idr["CheckinType"]),
                                ExceptionType = Convert.ToString(idr["ExceptionType"]),
                                CheckinTime = Convert.ToDateTime(idr["CheckinTime"]),
                                LocationTitle = Convert.ToString(idr["LocationTitle"]),
                                LocationDetail = Convert.ToString(idr["LocationDetail"]),
                                WifiName = Convert.ToString(idr["WifiName"]),
                                Notes = Convert.ToString(idr["Notes"]),
                                WifiMac = Convert.ToString(idr["WifiMac"]),
                                MediaIDs = Convert.ToString(idr["MediaIDs"]),
                                MediaPath = Convert.ToString(idr["MediaPath"]),
                                Lat = Convert.ToInt32(idr["Lat"]),
                                Lng = Convert.ToInt32(idr["Lng"]),
                                DeviceID = Convert.ToString(idr["DeviceID"]),
                                SchCheckinTime = Convert.ToDateTime(idr["SchCheckinTime"]),
                                GroupID = Convert.ToInt32(idr["GroupID"]),
                                TimelineID = Convert.ToInt32(idr["TimelineID"])
                            });
                        }
                    }
                }
                resHT["rows"] = result;

                string strCount = @"Select Count(*) as TotalCount from Tbl_QY_CheckinData as a Where (1=1)";

                if (!string.IsNullOrEmpty(entity.CheckinType))
                {
                    strSQL += " and a.CheckinType = '" + entity.CheckinType + "'";
                }
                if (!string.IsNullOrEmpty(entity.ExceptionType))
                {
                    strSQL += " and a.ExceptionType = '" + entity.ExceptionType + "'";
                }
                if (!string.IsNullOrEmpty(entity.UserID))
                {
                    strSQL += " and a.UserID = '" + entity.UserID + "'";
                }
                if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.CheckinTime>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
                }
                if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
                {
                    strSQL += " and a.CheckinTime<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
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
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
            return resHT;
        }
        /// <summary>
        /// 查询导出考勤
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<QYCheckinDataEntity> QueryCheckinList(QYCheckinDataEntity entity)
        {
            List<QYCheckinDataEntity> result = new List<QYCheckinDataEntity>();
            string strSQL = @"select c.id as DepartmentID,c.Name as DepartmentName,b.WxName as UserName, a.* from Tbl_QY_CheckinData a inner join Tbl_QY_User b on a.UserID=b.UserID inner join Tbl_QY_Depart c on case when(charindex(',',Department)=0)then Department else substring(Department,0,charindex(',',Department)) end=c.Id WHERE (1=1)";

            if (!string.IsNullOrEmpty(entity.CheckinType))
            {
                strSQL += " and a.CheckinType = '" + entity.CheckinType + "'";
            }
            if (!string.IsNullOrEmpty(entity.ExceptionType))
            {
                strSQL += " and a.ExceptionType = '" + entity.ExceptionType + "'";
            }
            if (!string.IsNullOrEmpty(entity.UserName))
            {
                strSQL += " and b.WxName like '%" + entity.UserName + "%'";
            }
            if ((entity.StartDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.StartDate.ToString("yyyy-MM-dd") != "1900-01-01"))
            {
                strSQL += " and a.CheckinTime>='" + entity.StartDate.ToString("yyyy-MM-dd") + "'";
            }
            if ((entity.EndDate.ToString("yyyy-MM-dd") != "0001-01-01" && entity.EndDate.ToString("yyyy-MM-dd") != "1900-01-01"))
            {
                strSQL += " and a.CheckinTime<'" + entity.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "'";
            }
            using (DbCommand command = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dd = conn.ExecuteDataTable(command))
                {
                    foreach (DataRow idr in dd.Rows)
                    {
                        result.Add(new QYCheckinDataEntity
                        {
                            ID = Convert.ToInt32(idr["ID"]),
                            UserID = Convert.ToString(idr["UserID"]),
                            UserName = Convert.ToString(idr["UserName"]),
                            DepartmentID = Convert.ToString(idr["DepartmentID"]),
                            DepartmentName = Convert.ToString(idr["DepartmentName"]),
                            GroupName = Convert.ToString(idr["GroupName"]),
                            CheckinType = Convert.ToString(idr["CheckinType"]),
                            ExceptionType = Convert.ToString(idr["ExceptionType"]),
                            CheckinTime = Convert.ToDateTime(idr["CheckinTime"]),
                            LocationTitle = Convert.ToString(idr["LocationTitle"]),
                            LocationDetail = Convert.ToString(idr["LocationDetail"]),
                            WifiName = Convert.ToString(idr["WifiName"]),
                            Notes = Convert.ToString(idr["Notes"]),
                            WifiMac = Convert.ToString(idr["WifiMac"]),
                            MediaIDs = Convert.ToString(idr["MediaIDs"]),
                            MediaPath = Convert.ToString(idr["MediaPath"]),
                            Lat = Convert.ToInt32(idr["Lat"]),
                            Lng = Convert.ToInt32(idr["Lng"]),
                            DeviceID = Convert.ToString(idr["DeviceID"]),
                            SchCheckinTime = Convert.ToDateTime(idr["SchCheckinTime"]),
                            GroupID = Convert.ToInt32(idr["GroupID"]),
                            TimelineID = Convert.ToInt32(idr["TimelineID"])
                        });
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 同步考勤记录
        /// </summary>
        /// <param name="entity"></param>
        public void SyncCheckinData(List<QYCheckinDataEntity> entity)
        {
            ArrayList timeList = new ArrayList();
            foreach (var item in entity)
            {
                if (!timeList.Contains(item.CheckinTime.ToString("yyyy-MM-dd")))
                {
                    timeList.Add(item.CheckinTime.ToString("yyyy-MM-dd"));
                }
            }
            string time = "";
            foreach (string item in timeList)
            {
                time += " (CheckinTime>='" + item + "' and CheckinTime<'" + Convert.ToDateTime(item).AddDays(1).ToString("yyyy-MM-dd") + "') or";
            }
            time = time.TrimEnd('r');
            time = time.TrimEnd('o');

            string strSQL = "delete from Tbl_QY_CheckinData  where 1=1 and" + time; ;
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.ExecuteNonQuery(cmd);
            }
            foreach (var item in entity)
            {

                strSQL = @"INSERT INTO Tbl_QY_CheckinData(UserID,GroupName,CheckinType,ExceptionType,CheckinTime,LocationTitle,LocationDetail,WifiName,Notes,WifiMac,MediaIDs,MediaPath,Lat,Lng,DeviceID,SchCheckinTime,GroupID,TimelineID) VALUES (@UserID,@GroupName,@CheckinType,@ExceptionType,@CheckinTime,@LocationTitle,@LocationDetail,@WifiName,@Notes,@WifiMac,@MediaIDs,@MediaPath,@Lat,@Lng,@DeviceID,@SchCheckinTime,@GroupID,@TimelineID)";
                try
                {
                    using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                    {
                        conn.AddInParameter(cmd, "@UserID", DbType.String, item.UserID);
                        conn.AddInParameter(cmd, "@GroupName", DbType.String, item.GroupName);
                        conn.AddInParameter(cmd, "@CheckinType", DbType.String, item.CheckinType);
                        conn.AddInParameter(cmd, "@ExceptionType", DbType.String, item.ExceptionType);
                        conn.AddInParameter(cmd, "@CheckinTime", DbType.DateTime, item.CheckinTime);
                        conn.AddInParameter(cmd, "@LocationTitle", DbType.String, item.LocationTitle);
                        conn.AddInParameter(cmd, "@LocationDetail", DbType.String, item.LocationDetail);
                        conn.AddInParameter(cmd, "@WifiName", DbType.String, item.WifiName);
                        conn.AddInParameter(cmd, "@Notes", DbType.String, item.Notes);
                        conn.AddInParameter(cmd, "@WifiMac", DbType.String, item.WifiMac);
                        conn.AddInParameter(cmd, "@MediaIDs", DbType.String, item.MediaIDs);
                        conn.AddInParameter(cmd, "@MediaPath", DbType.String, item.MediaPath);
                        conn.AddInParameter(cmd, "@Lat", DbType.Int32, item.Lat);
                        conn.AddInParameter(cmd, "@Lng", DbType.Int32, item.Lng);
                        conn.AddInParameter(cmd, "@DeviceID", DbType.String, item.DeviceID);
                        conn.AddInParameter(cmd, "@SchCheckinTime", DbType.DateTime, item.SchCheckinTime);
                        conn.AddInParameter(cmd, "@GroupID", DbType.Int32, item.GroupID);
                        conn.AddInParameter(cmd, "@TimelineID", DbType.Int32, item.TimelineID);
                        conn.ExecuteNonQuery(cmd);
                    }
                }
                catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            }
        }
        public void SyncCheckinDayReport(List<QYCheckinDayReportEntity> entity)
        {
            ArrayList timeList = new ArrayList();
            foreach (var item in entity)
            {
                if (!timeList.Contains(item.Date))
                {
                    timeList.Add(item.Date.ToString());
                }
            }
            string time = "";
            foreach (string item in timeList)
            {
                time += " (Date='" + item + "') or";
            }
            time = time.TrimEnd('r');
            time = time.TrimEnd('o');

            string strSQL = "delete from Tbl_QY_CheckinDayReport where 1=1 and" + time; ;
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.ExecuteNonQuery(cmd);
            }

            foreach (var item in entity)
            {
                item.EnSafe();
                string dayReportID = "";
                strSQL = @"INSERT INTO Tbl_QY_CheckinDayReport(Date,RecordType,Name,NameEx,DepartsName,UserID,Groupid,GroupName,ScheduleId,ScheduleName,WorkSec,OffWorkSec,DayType,CheckinCount,RegularWorkSec,StandardWorkSec,EarliestTime,LastestTime,LateCount,LateDuration,LeaveEarlyCount,LeaveEarlyDuration,AbsenteeismCount,AbsenteeismDuration,AbsenceCount,LocationAbnormalCount,EquipmentAbnormalCount,OtStatus,OtDuration,ExceptionDuration,AnnualLeaveCount,AnnualLeaveDuration,AnnualLeaveTimeType,CompassionateLeaveCount,CompassionateLeaveDuration,CompassionateLeaveTimeType,SickLeaveCount,SickLeaveDuration,SickLeaveTimeType,CompensatoryLeaveCount,CompensatoryLeaveDuration,CompensatoryLeaveTimeType,MarriageHolidayCount,MarriageHolidayDuration,MarriageHolidayTimeType,MaternityLeaveCount,MaternityLeaveDuration,MaternityLeaveTimeType,PaternityLeaveCount,PaternityLeaveDuration,PaternityLeaveTimeType,OtherLeaveCount,OtherLeaveDuration,OtherLeaveTimeType,CardReplacementCount,BusinessCount,BusinessDuration,BusinessTimeType,EgressCount,EgressDuration,EgressTimeType,FieldCount,FieldDuration,FieldTimeType,OP_DATE) VALUES(@Date,@RecordType,@Name,@NameEx,@DepartsName,@UserID,@Groupid,@GroupName,@ScheduleId,@ScheduleName,@WorkSec,@OffWorkSec,@DayType,@CheckinCount,@RegularWorkSec,@StandardWorkSec,@EarliestTime,@LastestTime,@LateCount,@LateDuration,@LeaveEarlyCount,@LeaveEarlyDuration,@AbsenteeismCount,@AbsenteeismDuration,@AbsenceCount,@LocationAbnormalCount,@EquipmentAbnormalCount,@OtStatus,@OtDuration,@ExceptionDuration,@AnnualLeaveCount,@AnnualLeaveDuration,@AnnualLeaveTimeType,@CompassionateLeaveCount,@CompassionateLeaveDuration,@CompassionateLeaveTimeType,@SickLeaveCount,@SickLeaveDuration,@SickLeaveTimeType,@CompensatoryLeaveCount,@CompensatoryLeaveDuration,@CompensatoryLeaveTimeType,@MarriageHolidayCount,@MarriageHolidayDuration,@MarriageHolidayTimeType,@MaternityLeaveCount,@MaternityLeaveDuration,@MaternityLeaveTimeType,@PaternityLeaveCount,@PaternityLeaveDuration,@PaternityLeaveTimeType,@OtherLeaveCount,@OtherLeaveDuration,@OtherLeaveTimeType,@CardReplacementCount,@BusinessCount,@BusinessDuration,@BusinessTimeType,@EgressCount,@EgressDuration,@EgressTimeType,@FieldCount,@FieldDuration,@FieldTimeType,@OP_DATE)SELECT @@IDENTITY";
                try
                {
                    using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                    {
                        conn.AddInParameter(cmd, "@Date", DbType.String, item.Date);
                        conn.AddInParameter(cmd, "@RecordType", DbType.Int32, item.RecordType);
                        conn.AddInParameter(cmd, "@Name", DbType.String, item.Name);
                        conn.AddInParameter(cmd, "@NameEx", DbType.String, item.NameEx);
                        conn.AddInParameter(cmd, "@DepartsName", DbType.String, item.DepartsName);
                        conn.AddInParameter(cmd, "@UserID", DbType.String, item.UserID);
                        conn.AddInParameter(cmd, "@Groupid", DbType.Int32, item.Groupid);
                        conn.AddInParameter(cmd, "@GroupName", DbType.String, item.GroupName);
                        conn.AddInParameter(cmd, "@ScheduleId", DbType.Int32, item.ScheduleId);
                        conn.AddInParameter(cmd, "@ScheduleName", DbType.String, item.ScheduleName);
                        conn.AddInParameter(cmd, "@WorkSec", DbType.Int32, item.WorkSec);
                        conn.AddInParameter(cmd, "@OffWorkSec", DbType.Int32, item.OffWorkSec);
                        conn.AddInParameter(cmd, "@DayType", DbType.Int32, item.DayType);
                        conn.AddInParameter(cmd, "@CheckinCount", DbType.Int32, item.CheckinCount);
                        conn.AddInParameter(cmd, "@RegularWorkSec", DbType.Int32, item.RegularWorkSec);
                        conn.AddInParameter(cmd, "@StandardWorkSec", DbType.Int32, item.StandardWorkSec);
                        conn.AddInParameter(cmd, "@EarliestTime", DbType.Int32, item.EarliestTime);
                        conn.AddInParameter(cmd, "@LastestTime", DbType.Int32, item.LastestTime);
                        conn.AddInParameter(cmd, "@LateCount", DbType.Int32, item.LateCount);
                        conn.AddInParameter(cmd, "@LateDuration", DbType.Int32, item.LateDuration);
                        conn.AddInParameter(cmd, "@LeaveEarlyCount", DbType.Int32, item.LeaveEarlyCount);
                        conn.AddInParameter(cmd, "@LeaveEarlyDuration", DbType.Int32, item.LeaveEarlyDuration);
                        conn.AddInParameter(cmd, "@AbsenteeismCount", DbType.Int32, item.AbsenteeismCount);
                        conn.AddInParameter(cmd, "@AbsenteeismDuration", DbType.Int32, item.AbsenteeismDuration);
                        conn.AddInParameter(cmd, "@AbsenceCount", DbType.Int32, item.AbsenceCount);
                        conn.AddInParameter(cmd, "@LocationAbnormalCount", DbType.Int32, item.LocationAbnormalCount);
                        conn.AddInParameter(cmd, "@EquipmentAbnormalCount", DbType.Int32, item.EquipmentAbnormalCount);
                        conn.AddInParameter(cmd, "@OtStatus", DbType.Int32, item.OtStatus);
                        conn.AddInParameter(cmd, "@OtDuration", DbType.Int32, item.OtDuration);
                        conn.AddInParameter(cmd, "@ExceptionDuration", DbType.Int32, item.ExceptionDuration);
                        conn.AddInParameter(cmd, "@AnnualLeaveCount", DbType.Int32, item.AnnualLeaveCount);
                        conn.AddInParameter(cmd, "@AnnualLeaveDuration", DbType.Int32, item.AnnualLeaveDuration);
                        conn.AddInParameter(cmd, "@AnnualLeaveTimeType", DbType.Int32, item.AnnualLeaveTimeType);
                        conn.AddInParameter(cmd, "@CompassionateLeaveCount", DbType.Int32, item.CompassionateLeaveCount);
                        conn.AddInParameter(cmd, "@CompassionateLeaveDuration", DbType.Int32, item.CompassionateLeaveDuration);
                        conn.AddInParameter(cmd, "@CompassionateLeaveTimeType", DbType.Int32, item.CompassionateLeaveTimeType);
                        conn.AddInParameter(cmd, "@SickLeaveCount", DbType.Int32, item.SickLeaveCount);
                        conn.AddInParameter(cmd, "@SickLeaveDuration", DbType.Int32, item.SickLeaveDuration);
                        conn.AddInParameter(cmd, "@SickLeaveTimeType", DbType.Int32, item.SickLeaveTimeType);
                        conn.AddInParameter(cmd, "@CompensatoryLeaveCount", DbType.Int32, item.CompensatoryLeaveCount);
                        conn.AddInParameter(cmd, "@CompensatoryLeaveDuration", DbType.Int32, item.CompensatoryLeaveDuration);
                        conn.AddInParameter(cmd, "@CompensatoryLeaveTimeType", DbType.Int32, item.CompensatoryLeaveTimeType);
                        conn.AddInParameter(cmd, "@MarriageHolidayCount", DbType.Int32, item.MarriageHolidayCount);
                        conn.AddInParameter(cmd, "@MarriageHolidayDuration", DbType.Int32, item.MarriageHolidayDuration);
                        conn.AddInParameter(cmd, "@MarriageHolidayTimeType", DbType.Int32, item.MarriageHolidayTimeType);
                        conn.AddInParameter(cmd, "@MaternityLeaveCount", DbType.Int32, item.MaternityLeaveCount);
                        conn.AddInParameter(cmd, "@MaternityLeaveDuration", DbType.Int32, item.MaternityLeaveDuration);
                        conn.AddInParameter(cmd, "@MaternityLeaveTimeType", DbType.Int32, item.MaternityLeaveTimeType);
                        conn.AddInParameter(cmd, "@PaternityLeaveCount", DbType.Int32, item.PaternityLeaveCount);
                        conn.AddInParameter(cmd, "@PaternityLeaveDuration", DbType.Int32, item.PaternityLeaveDuration);
                        conn.AddInParameter(cmd, "@PaternityLeaveTimeType", DbType.Int32, item.PaternityLeaveTimeType);
                        conn.AddInParameter(cmd, "@OtherLeaveCount", DbType.Int32, item.OtherLeaveCount);
                        conn.AddInParameter(cmd, "@OtherLeaveDuration", DbType.Int32, item.OtherLeaveDuration);
                        conn.AddInParameter(cmd, "@OtherLeaveTimeType", DbType.Int32, item.OtherLeaveTimeType);
                        conn.AddInParameter(cmd, "@CardReplacementCount", DbType.Int32, item.CardReplacementCount);
                        conn.AddInParameter(cmd, "@BusinessCount", DbType.Int32, item.BusinessCount);
                        conn.AddInParameter(cmd, "@BusinessDuration", DbType.Int32, item.BusinessDuration);
                        conn.AddInParameter(cmd, "@BusinessTimeType", DbType.Int32, item.BusinessTimeType);
                        conn.AddInParameter(cmd, "@EgressCount", DbType.Int32, item.EgressCount);
                        conn.AddInParameter(cmd, "@EgressDuration", DbType.Int32, item.EgressDuration);
                        conn.AddInParameter(cmd, "@EgressTimeType", DbType.Int32, item.EgressTimeType);
                        conn.AddInParameter(cmd, "@FieldCount", DbType.Int32, item.FieldCount);
                        conn.AddInParameter(cmd, "@FieldDuration", DbType.Int32, item.FieldDuration);
                        conn.AddInParameter(cmd, "@FieldTimeType", DbType.Int32, item.FieldTimeType);
                        conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                        dayReportID = Convert.ToString(conn.ExecuteScalar(cmd));
                        item.spInfo.DayReportID = Convert.ToInt32(dayReportID);
                    }
                    if (item.spInfo.SpNumber != null)
                    {
                        SyncCheckinDayReportSpInfo(item.spInfo);
                    }
                }
                catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            }
        }

        public void SyncCheckinDayReportSpInfo(QYCheckinDayReportSpInfo spInfo)
        {
            string strSQL = "delete from Tbl_QY_CheckinDayReportSpInfo  where 1=1 and SpNumber=" + spInfo.SpNumber; ;
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.ExecuteNonQuery(cmd);
            }
            strSQL = @"INSERT INTO Tbl_QY_CheckinDayReportSpInfo(DayReportID,SpNumber,TitleLang,TitleText,DescriptionLang,DescriptionText,OP_DATE) VALUES (@DayReportID,@SpNumber,@TitleLang,@TitleText,@DescriptionLang,@DescriptionText,@OP_DATE)";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@DayReportID", DbType.Int32, spInfo.DayReportID);
                    conn.AddInParameter(cmd, "@SpNumber", DbType.String, spInfo.SpNumber);
                    conn.AddInParameter(cmd, "@TitleLang", DbType.String, spInfo.TitleLang);
                    conn.AddInParameter(cmd, "@TitleText", DbType.String, spInfo.TitleText);
                    conn.AddInParameter(cmd, "@DescriptionLang", DbType.String, spInfo.DescriptionLang);
                    conn.AddInParameter(cmd, "@DescriptionText", DbType.String, spInfo.DescriptionText);
                    conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }

        }
        /// <summary>
        /// 同步月度汇总数据
        /// </summary>
        /// <param name="entity"></param>
        public void SyncCheckinReport(List<QYCheckinMonthlyReportEntity> entity)
        {
            ArrayList timeList = new ArrayList();
            foreach (var item in entity)
            {
                if (!timeList.Contains(item.ReportTime))
                {
                    timeList.Add(item.ReportTime);
                }
            }
            string time = "";
            foreach (string item in timeList)
            {
                time += " (ReportTime='" + item + "') or";
            }
            time = time.TrimEnd('r');
            time = time.TrimEnd('o');

            string strSQL = "delete from Tbl_QY_CheckinMonthlyReport  where 1=1 and" + time; ;
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.ExecuteNonQuery(cmd);
            }

            foreach (var item in entity)
            {
                strSQL = @"INSERT INTO Tbl_QY_CheckinMonthlyReport(RecordType,Name,NameEx,UserID,WorkDays,RegularDays,ExceptDays,RegularWorkSec,StandardWorkSec,LateCount,LateDuration,LeaveEarlyCount,LeaveEarlyDuration,AbsenceCount,AbsenteeismCount,AbsenteeismDuration,LocationAbnormalCount,EquipmentAbnormalCount,AnnualLeaveCount,AnnualLeaveDuration,AnnualLeaveTimeType,CompassionateLeaveCount,CompassionateLeaveDuration,CompassionateLeaveTimeType,SickLeaveCount,SickLeaveDuration,SickLeaveTimeType,CompensatoryLeaveCount,CompensatoryLeaveDuration,CompensatoryLeaveTimeType,MarriageHolidayCount,MarriageHolidayDuration,MarriageHolidayTimeType,MaternityLeaveCount,MaternityLeaveDuration,MaternityLeaveTimeType,PaternityLeaveCount,PaternityLeaveDuration,PaternityLeaveTimeType,OtherLeaveCount,OtherLeaveDuration,OtherLeaveTimeType,CardReplacementCount,BusinessCount,BusinessDuration,BusinessTimeType,EgressCount,EgressDuration,EgressTimeType,FieldCount,FieldDuration,FieldTimeType,WorkdayOverSec,HolidaysOverSec,RestdaysOverSec,ReportTime,OP_DATE) VALUES (@RecordType,@Name,@NameEx,@UserID,@WorkDays,@RegularDays,@ExceptDays,@RegularWorkSec,@StandardWorkSec,@LateCount,@LateDuration,@LeaveEarlyCount,@LeaveEarlyDuration,@AbsenceCount,@AbsenteeismCount,@AbsenteeismDuration,@LocationAbnormalCount,@EquipmentAbnormalCount,@AnnualLeaveCount,@AnnualLeaveDuration,@AnnualLeaveTimeType,@CompassionateLeaveCount,@CompassionateLeaveDuration,@CompassionateLeaveTimeType,@SickLeaveCount,@SickLeaveDuration,@SickLeaveTimeType,@CompensatoryLeaveCount,@CompensatoryLeaveDuration,@CompensatoryLeaveTimeType,@MarriageHolidayCount,@MarriageHolidayDuration,@MarriageHolidayTimeType,@MaternityLeaveCount,@MaternityLeaveDuration,@MaternityLeaveTimeType,@PaternityLeaveCount,@PaternityLeaveDuration,@PaternityLeaveTimeType,@OtherLeaveCount,@OtherLeaveDuration,@OtherLeaveTimeType,@CardReplacementCount,@BusinessCount,@BusinessDuration,@BusinessTimeType,@EgressCount,@EgressDuration,@EgressTimeType,@FieldCount,@FieldDuration,@FieldTimeType,@WorkdayOverSec,@HolidaysOverSec,@RestdaysOverSec,@ReportTime,@OP_DATE)";
                try
                {
                    using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                    {
                        conn.AddInParameter(cmd, "@RecordType", DbType.Int32, item.RecordType);
                        conn.AddInParameter(cmd, "@Name", DbType.String, item.Name);
                        conn.AddInParameter(cmd, "@NameEx", DbType.String, item.NameEx);
                        conn.AddInParameter(cmd, "@UserID", DbType.String, item.UserID);
                        conn.AddInParameter(cmd, "@WorkDays", DbType.Int32, item.WorkDays);
                        conn.AddInParameter(cmd, "@RegularDays", DbType.Int32, item.RegularDays);
                        conn.AddInParameter(cmd, "@ExceptDays", DbType.Int32, item.ExceptDays);
                        conn.AddInParameter(cmd, "@RegularWorkSec", DbType.Int32, item.RegularWorkSec);
                        conn.AddInParameter(cmd, "@StandardWorkSec", DbType.Int32, item.StandardWorkSec);
                        conn.AddInParameter(cmd, "@LateCount", DbType.Int32, item.LateCount);
                        conn.AddInParameter(cmd, "@LateDuration", DbType.Int32, item.LateDuration);
                        conn.AddInParameter(cmd, "@LeaveEarlyCount", DbType.Int32, item.LeaveEarlyCount);
                        conn.AddInParameter(cmd, "@LeaveEarlyDuration", DbType.Int32, item.LeaveEarlyDuration);
                        conn.AddInParameter(cmd, "@AbsenceCount", DbType.Int32, item.AbsenceCount);
                        conn.AddInParameter(cmd, "@AbsenteeismCount", DbType.Int32, item.AbsenteeismCount);
                        conn.AddInParameter(cmd, "@AbsenteeismDuration", DbType.Int32, item.AbsenteeismDuration);
                        conn.AddInParameter(cmd, "@LocationAbnormalCount", DbType.Int32, item.LocationAbnormalCount);
                        conn.AddInParameter(cmd, "@EquipmentAbnormalCount", DbType.Int32, item.EquipmentAbnormalCount);
                        conn.AddInParameter(cmd, "@AnnualLeaveCount", DbType.Int32, item.AnnualLeaveCount);
                        conn.AddInParameter(cmd, "@AnnualLeaveDuration", DbType.Int32, item.AnnualLeaveDuration);
                        conn.AddInParameter(cmd, "@AnnualLeaveTimeType", DbType.Int32, item.AnnualLeaveTimeType);
                        conn.AddInParameter(cmd, "@CompassionateLeaveCount", DbType.Int32, item.CompassionateLeaveCount);
                        conn.AddInParameter(cmd, "@CompassionateLeaveDuration", DbType.Int32, item.CompassionateLeaveDuration);
                        conn.AddInParameter(cmd, "@CompassionateLeaveTimeType", DbType.Int32, item.CompassionateLeaveTimeType);
                        conn.AddInParameter(cmd, "@SickLeaveCount", DbType.Int32, item.SickLeaveCount);
                        conn.AddInParameter(cmd, "@SickLeaveDuration", DbType.Int32, item.SickLeaveDuration);
                        conn.AddInParameter(cmd, "@SickLeaveTimeType", DbType.Int32, item.SickLeaveTimeType);
                        conn.AddInParameter(cmd, "@CompensatoryLeaveCount", DbType.Int32, item.CompensatoryLeaveCount);
                        conn.AddInParameter(cmd, "@CompensatoryLeaveDuration", DbType.Int32, item.CompensatoryLeaveDuration);
                        conn.AddInParameter(cmd, "@CompensatoryLeaveTimeType", DbType.Int32, item.CompensatoryLeaveTimeType);
                        conn.AddInParameter(cmd, "@MarriageHolidayCount", DbType.Int32, item.MarriageHolidayCount);
                        conn.AddInParameter(cmd, "@MarriageHolidayDuration", DbType.Int32, item.MarriageHolidayDuration);
                        conn.AddInParameter(cmd, "@MarriageHolidayTimeType", DbType.Int32, item.MarriageHolidayTimeType);
                        conn.AddInParameter(cmd, "@MaternityLeaveCount", DbType.Int32, item.MaternityLeaveCount);
                        conn.AddInParameter(cmd, "@MaternityLeaveDuration", DbType.Int32, item.MaternityLeaveDuration);
                        conn.AddInParameter(cmd, "@MaternityLeaveTimeType", DbType.Int32, item.MaternityLeaveTimeType);
                        conn.AddInParameter(cmd, "@PaternityLeaveCount", DbType.Int32, item.PaternityLeaveCount);
                        conn.AddInParameter(cmd, "@PaternityLeaveDuration", DbType.Int32, item.PaternityLeaveDuration);
                        conn.AddInParameter(cmd, "@PaternityLeaveTimeType", DbType.Int32, item.PaternityLeaveTimeType);
                        conn.AddInParameter(cmd, "@OtherLeaveCount", DbType.Int32, item.OtherLeaveCount);
                        conn.AddInParameter(cmd, "@OtherLeaveDuration", DbType.Int32, item.OtherLeaveDuration);
                        conn.AddInParameter(cmd, "@OtherLeaveTimeType", DbType.Int32, item.OtherLeaveTimeType);
                        conn.AddInParameter(cmd, "@CardReplacementCount", DbType.Int32, item.CardReplacementCount);
                        conn.AddInParameter(cmd, "@BusinessCount", DbType.Int32, item.BusinessCount);
                        conn.AddInParameter(cmd, "@BusinessDuration", DbType.Int32, item.BusinessDuration);
                        conn.AddInParameter(cmd, "@BusinessTimeType", DbType.Int32, item.BusinessTimeType);
                        conn.AddInParameter(cmd, "@EgressCount", DbType.Int32, item.EgressCount);
                        conn.AddInParameter(cmd, "@EgressDuration", DbType.Int32, item.EgressDuration);
                        conn.AddInParameter(cmd, "@EgressTimeType", DbType.Int32, item.EgressTimeType);
                        conn.AddInParameter(cmd, "@FieldCount", DbType.Int32, item.FieldCount);
                        conn.AddInParameter(cmd, "@FieldDuration", DbType.Int32, item.FieldDuration);
                        conn.AddInParameter(cmd, "@FieldTimeType", DbType.Int32, item.FieldTimeType);
                        conn.AddInParameter(cmd, "@WorkdayOverSec", DbType.Int32, item.WorkdayOverSec);
                        conn.AddInParameter(cmd, "@HolidaysOverSec", DbType.Int32, item.HolidaysOverSec);
                        conn.AddInParameter(cmd, "@RestdaysOverSec", DbType.Int32, item.RestdaysOverSec);
                        conn.AddInParameter(cmd, "@ReportTime", DbType.String, item.ReportTime);
                        conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                        conn.ExecuteNonQuery(cmd);
                    }
                }
                catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            }
        }
        /// <summary>
        /// 查询月度汇总数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<QYCheckinMonthlyReportEntity> QueryCheckinReportData(QYCheckinMonthlyReportEntity entity)
        {
            List<QYCheckinMonthlyReportEntity> result = new List<QYCheckinMonthlyReportEntity>();
            string strSQL = "select de.Name as DepartmentName,cmr.* from Tbl_QY_CheckinMonthlyReport cmr inner join Tbl_QY_User us on cmr.UserID=us.UserID inner join Tbl_QY_Depart de on us.MainDepartment=de.Id where 1=1";
            if (!string.IsNullOrEmpty(entity.ReportTime))
            {
                strSQL += " and cmr.ReportTime = '" + entity.ReportTime + "'";
            }
            if (!string.IsNullOrEmpty(entity.UserID))
            {
                strSQL += " and cmr.UserID like '%" + entity.UserID + "%'";
            }
            if (!string.IsNullOrEmpty(entity.Name))
            {
                strSQL += " and cmr.Name like '%" + entity.Name + "%'";
            }
            strSQL += ") cmr";
            var DateSum = new List<string>();
            for (var date = Convert.ToDateTime(entity.StartDate); date <= Convert.ToDateTime(entity.EndDate); date = date.AddDays(1))
            {
                DateSum.Add(date.ToString("yyyy-MM-dd"));
            }
            int day = 1;
            string infos = "";
            foreach (var item in DateSum)
            {
                infos += "ISNULL(info" + day + ",'')+'|'+";
                strSQL += " LEFT JOIN( select ltrim(rtrim( case when b.考勤状态 is not null then b.考勤状态 else '' end + case when b.缺卡状态 is not null then ' '+b.缺卡状态 else '' end + case when b.地点状态 is not null then ' '+b.地点状态 else '' end + case when b.设备状态 is not null then ' '+b.设备状态 else '' end + case when b.事假状态 is not null then ' '+b.事假状态 else '' end + case when b.病假状态 is not null then ' '+b.病假状态 else '' end + case when b.调休假状态 is not null then ' '+b.调休假状态 else '' end + case when b.婚假状态 is not null then ' '+b.婚假状态 else '' end + case when b.产假状态 is not null then ' '+b.产假状态 else '' end + case when b.陪产假状态 is not null then ' '+b.陪产假状态 else '' end + case when b.其它假状态 is not null then ' '+b.其它假状态 else '' end)) as info" + day + ",b.UserID from( SELECT case when a.上班状态='上班' and a.WorkSec>0 and a.WorkSec+660>a.EarliestTime and a.OffWorkSec<=a.LastestTime  or a.WorkSec=0 and 30600+660>a.EarliestTime and 64800<=a.LastestTime  then '正常' when a.上班状态='上班' and a.LateCount>0 then '迟到' +cast((a.LateDuration)/60 as varchar)+'分钟' when a.上班状态='上班' and a.LeaveEarlyCount>0 then '早退' +cast((a.LeaveEarlyDuration)/60 as varchar)+'分钟' when a.上班状态='上班' and a.AbsenteeismCount>0 then '旷工' +cast((a.AbsenteeismDuration)/60 as varchar)+'分钟' when a.上班状态='休息' then '休息' end as 考勤状态, case when a.上班状态='上班' and a.AbsenceCount>0 then '缺卡' +cast(a.AbsenceCount as varchar)+'次' end as 缺卡状态, case when a.上班状态='上班' and a.LocationAbnormalCount>0 then '地点异常' +cast(a.LocationAbnormalCount as varchar)+'次' end as 地点状态, case when a.上班状态='上班' and a.EquipmentAbnormalCount>0 then '设备异常' +cast(a.EquipmentAbnormalCount as varchar)+'次' end as 设备状态, case when a.上班状态='上班' and a.CompassionateLeaveCount>0 and CompassionateLeaveTimeType=0 then '事假' +cast(round(cast((a.CompassionateLeaveDuration*1.0/60/60/24) as float),2) as varchar)+'天' when a.上班状态='上班' and a.CompassionateLeaveCount>0 and CompassionateLeaveTimeType=1 then '事假' +cast(round(cast((a.CompassionateLeaveDuration*1.0/60/60) as float),2) as varchar)+'小时' end as 事假状态, case when a.上班状态='上班' and a.SickLeaveCount>0 and SickLeaveTimeType=0 then '病假' +cast(round(cast((a.SickLeaveDuration*1.0/60/60/24) as float),2) as varchar)+'天' when a.上班状态='上班' and a.SickLeaveCount>0 and SickLeaveTimeType=1 then '病假' +cast(round(cast((a.SickLeaveDuration*1.0/60/60) as float),2) as varchar)+'小时' end as 病假状态, case when a.上班状态='上班' and a.CompensatoryLeaveCount>0 and CompensatoryLeaveTimeType=0 then '调休假' +cast(round(cast((a.CompensatoryLeaveDuration*1.0/60/60/24) as float),2) as varchar)+'天' when a.上班状态='上班' and a.CompensatoryLeaveCount>0 and CompensatoryLeaveTimeType=1 then '调休假' +cast(round(cast((a.CompensatoryLeaveDuration*1.0/60/60) as float),2) as varchar)+'小时' end as 调休假状态, case when a.上班状态='上班' and a.MarriageHolidayCount>0 and MarriageHolidayTimeType=0 then '婚假' +cast(round(cast((a.MarriageHolidayDuration*1.0/60/60/24) as float),2) as varchar)+'天' when a.上班状态='上班' and a.MarriageHolidayCount>0 and MarriageHolidayTimeType=1 then '婚假' +cast(round(cast((a.MarriageHolidayDuration*1.0/60/60) as float),2) as varchar)+'小时' end as 婚假状态, case when a.上班状态='上班' and a.MaternityLeaveCount>0 and MaternityLeaveTimeType=0 then '产假' +cast(round(cast((a.MaternityLeaveDuration*1.0/60/60/24) as float),2) as varchar)+'天' when a.上班状态='上班' and a.MaternityLeaveCount>0 and MaternityLeaveTimeType=1 then '产假' +cast(round(cast((a.MaternityLeaveDuration*1.0/60/60) as float),2) as varchar)+'小时' end as 产假状态, case when a.上班状态='上班' and a.PaternityLeaveCount>0 and PaternityLeaveTimeType=0 then '陪产假' +cast(round(cast((a.PaternityLeaveDuration*1.0/60/60/24) as float),2) as varchar)+'天' when a.上班状态='上班' and a.PaternityLeaveCount>0 and PaternityLeaveTimeType=1 then '陪产假' +cast(round(cast((a.PaternityLeaveDuration*1.0/60/60) as float),2) as varchar)+'小时' end as 陪产假状态, case when a.上班状态='上班' and a.OtherLeaveCount>0 and OtherLeaveTimeType=0 then '其它假' +cast(round(cast((a.OtherLeaveDuration*1.0/60/60/24) as float),2) as varchar)+'天' when a.上班状态='上班' and a.OtherLeaveCount>0 and OtherLeaveTimeType=1 then '其它假' +cast(round(cast((a.OtherLeaveDuration*1.0/60/60) as float),2) as varchar)+'小时' end as 其它假状态, a.UserID FROM( select CASE WHEN ScheduleId=0 AND WorkSec>0and OffWorkSec>0 THEN '上班' WHEN ScheduleId=0 AND WorkSec=0and OffWorkSec=0 THEN '休息' WHEN ScheduleId>0 AND ScheduleName='上班' THEN '上班'when ScheduleId>0 AND ScheduleName='休息' THEN '休息' END AS 上班状态, * FROM Tbl_QY_CheckinDayReport WHERE Date='" + item + "') a)b) dr" + day + " on cmr.UserID=dr" + day + ".UserID";
                day++;
            }
            infos = infos.TrimEnd('+');
            strSQL = "select " + infos + "as infos,* from (" + strSQL;

            using (DbCommand command = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dd = conn.ExecuteDataTable(command))
                {
                    foreach (DataRow idr in dd.Rows)
                    {
                        result.Add(new QYCheckinMonthlyReportEntity
                        {
                            ID = Convert.ToInt32(idr["ID"]),
                            RecordType = Convert.ToInt32(idr["RecordType"]),
                            Name = Convert.ToString(idr["Name"]).Trim(),
                            NameEx = Convert.ToString(idr["NameEx"]).Trim(),
                            UserID = Convert.ToString(idr["UserID"]).Trim(),
                            DepartmentName = Convert.ToString(idr["DepartmentName"]).Trim(),
                            WorkDays = Convert.ToInt32(idr["WorkDays"]),
                            RegularDays = Convert.ToInt32(idr["RegularDays"]),
                            ExceptDays = Convert.ToInt32(idr["ExceptDays"]),
                            RegularWorkSec = Convert.ToInt32(idr["RegularWorkSec"]),
                            StandardWorkSec = Convert.ToInt32(idr["StandardWorkSec"]),
                            LateCount = Convert.ToInt32(idr["LateCount"]),
                            LateDuration = Convert.ToInt32(idr["LateDuration"]),
                            LeaveEarlyCount = Convert.ToInt32(idr["LeaveEarlyCount"]),
                            LeaveEarlyDuration = Convert.ToInt32(idr["LeaveEarlyDuration"]),
                            AbsenceCount = Convert.ToInt32(idr["AbsenceCount"]),
                            AbsenteeismCount = Convert.ToInt32(idr["AbsenteeismCount"]),
                            AbsenteeismDuration = Convert.ToInt32(idr["AbsenteeismDuration"]),
                            LocationAbnormalCount = Convert.ToInt32(idr["LocationAbnormalCount"]),
                            EquipmentAbnormalCount = Convert.ToInt32(idr["EquipmentAbnormalCount"]),
                            AnnualLeaveCount = Convert.ToInt32(idr["AnnualLeaveCount"]),
                            AnnualLeaveDuration = Convert.ToInt32(idr["AnnualLeaveDuration"]),
                            AnnualLeaveTimeType = Convert.ToInt32(idr["AnnualLeaveTimeType"]),
                            CompassionateLeaveCount = Convert.ToInt32(idr["CompassionateLeaveCount"]),
                            CompassionateLeaveDuration = Convert.ToInt32(idr["CompassionateLeaveDuration"]),
                            CompassionateLeaveTimeType = Convert.ToInt32(idr["CompassionateLeaveTimeType"]),
                            SickLeaveCount = Convert.ToInt32(idr["SickLeaveCount"]),
                            SickLeaveDuration = Convert.ToInt32(idr["SickLeaveDuration"]),
                            SickLeaveTimeType = Convert.ToInt32(idr["SickLeaveTimeType"]),
                            CompensatoryLeaveCount = Convert.ToInt32(idr["CompensatoryLeaveCount"]),
                            CompensatoryLeaveDuration = Convert.ToInt32(idr["CompensatoryLeaveDuration"]),
                            CompensatoryLeaveTimeType = Convert.ToInt32(idr["CompensatoryLeaveTimeType"]),
                            MarriageHolidayCount = Convert.ToInt32(idr["MarriageHolidayCount"]),
                            MarriageHolidayDuration = Convert.ToInt32(idr["MarriageHolidayDuration"]),
                            MarriageHolidayTimeType = Convert.ToInt32(idr["MarriageHolidayTimeType"]),
                            MaternityLeaveCount = Convert.ToInt32(idr["MaternityLeaveCount"]),
                            MaternityLeaveDuration = Convert.ToInt32(idr["MaternityLeaveDuration"]),
                            MaternityLeaveTimeType = Convert.ToInt32(idr["MaternityLeaveTimeType"]),
                            PaternityLeaveCount = Convert.ToInt32(idr["PaternityLeaveCount"]),
                            PaternityLeaveDuration = Convert.ToInt32(idr["PaternityLeaveDuration"]),
                            PaternityLeaveTimeType = Convert.ToInt32(idr["PaternityLeaveTimeType"]),
                            OtherLeaveCount = Convert.ToInt32(idr["OtherLeaveCount"]),
                            OtherLeaveDuration = Convert.ToInt32(idr["OtherLeaveDuration"]),
                            OtherLeaveTimeType = Convert.ToInt32(idr["OtherLeaveTimeType"]),
                            CardReplacementCount = Convert.ToInt32(idr["CardReplacementCount"]),
                            BusinessCount = Convert.ToInt32(idr["BusinessCount"]),
                            BusinessDuration = Convert.ToInt32(idr["BusinessDuration"]),
                            BusinessTimeType = Convert.ToInt32(idr["BusinessTimeType"]),
                            EgressCount = Convert.ToInt32(idr["EgressCount"]),
                            EgressDuration = Convert.ToInt32(idr["EgressDuration"]),
                            EgressTimeType = Convert.ToInt32(idr["EgressTimeType"]),
                            FieldCount = Convert.ToInt32(idr["FieldCount"]),
                            FieldDuration = Convert.ToInt32(idr["FieldDuration"]),
                            FieldTimeType = Convert.ToInt32(idr["FieldTimeType"]),
                            WorkdayOverSec = Convert.ToInt32(idr["WorkdayOverSec"]),
                            HolidaysOverSec = Convert.ToInt32(idr["HolidaysOverSec"]),
                            ReportTime = Convert.ToString(idr["ReportTime"]),
                            OP_DATE = Convert.ToDateTime(idr["OP_DATE"]),
                            infos = Convert.ToString(idr["infos"]),
                            info1 = idr.Table.Columns.Contains("info1") ? Convert.ToString(idr["info1"]) : null,
                            info2 = idr.Table.Columns.Contains("info2") ? Convert.ToString(idr["info2"]) : null,
                            info3 = idr.Table.Columns.Contains("info3") ? Convert.ToString(idr["info3"]) : null,
                            info4 = idr.Table.Columns.Contains("info4") ? Convert.ToString(idr["info4"]) : null,
                            info5 = idr.Table.Columns.Contains("info5") ? Convert.ToString(idr["info5"]) : null,
                            info6 = idr.Table.Columns.Contains("info6") ? Convert.ToString(idr["info6"]) : null,
                            info7 = idr.Table.Columns.Contains("info7") ? Convert.ToString(idr["info7"]) : null,
                            info8 = idr.Table.Columns.Contains("info8") ? Convert.ToString(idr["info8"]) : null,
                            info9 = idr.Table.Columns.Contains("info9") ? Convert.ToString(idr["info9"]) : null,
                            info10 = idr.Table.Columns.Contains("info10") ? Convert.ToString(idr["info10"]) : null,
                            info11 = idr.Table.Columns.Contains("info11") ? Convert.ToString(idr["info11"]) : null,
                            info12 = idr.Table.Columns.Contains("info12") ? Convert.ToString(idr["info12"]) : null,
                            info13 = idr.Table.Columns.Contains("info13") ? Convert.ToString(idr["info13"]) : null,
                            info14 = idr.Table.Columns.Contains("info14") ? Convert.ToString(idr["info14"]) : null,
                            info15 = idr.Table.Columns.Contains("info15") ? Convert.ToString(idr["info15"]) : null,
                            info16 = idr.Table.Columns.Contains("info16") ? Convert.ToString(idr["info16"]) : null,
                            info17 = idr.Table.Columns.Contains("info17") ? Convert.ToString(idr["info17"]) : null,
                            info18 = idr.Table.Columns.Contains("info18") ? Convert.ToString(idr["info18"]) : null,
                            info19 = idr.Table.Columns.Contains("info19") ? Convert.ToString(idr["info19"]) : null,
                            info20 = idr.Table.Columns.Contains("info20") ? Convert.ToString(idr["info20"]) : null,
                            info21 = idr.Table.Columns.Contains("info21") ? Convert.ToString(idr["info21"]) : null,
                            info22 = idr.Table.Columns.Contains("info22") ? Convert.ToString(idr["info22"]) : null,
                            info23 = idr.Table.Columns.Contains("info23") ? Convert.ToString(idr["info23"]) : null,
                            info24 = idr.Table.Columns.Contains("info24") ? Convert.ToString(idr["info24"]) : null,
                            info25 = idr.Table.Columns.Contains("info25") ? Convert.ToString(idr["info25"]) : null,
                            info26 = idr.Table.Columns.Contains("info26") ? Convert.ToString(idr["info26"]) : null,
                            info27 = idr.Table.Columns.Contains("info27") ? Convert.ToString(idr["info27"]) : null,
                            info28 = idr.Table.Columns.Contains("info28") ? Convert.ToString(idr["info28"]) : null,
                            info29 = idr.Table.Columns.Contains("info29") ? Convert.ToString(idr["info29"]) : null,
                            info30 = idr.Table.Columns.Contains("info30") ? Convert.ToString(idr["info30"]) : null,
                            info31 = idr.Table.Columns.Contains("info31") ? Convert.ToString(idr["info31"]) : null
                        });
                    }
                }
            }
            return result;
        }

        public DataTable QueryCheckinReportData1(QYCheckinMonthlyReportEntity entity)
        {
            DataTable dt = null;
            DateTime sDT = entity.StartDate;
            DateTime eDT = entity.EndDate;

            return dt;
        }
        #endregion
        #region 打卡规则
        /// <summary>
        /// 查询打卡规则
        /// </summary>
        /// <param name="pIndex"></param>
        /// <param name="pNum"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Hashtable QueryCheckinRule(int pIndex, int pNum, CheckinRuleEntity entity)
        {
            List<CheckinRuleEntity> result = new List<CheckinRuleEntity>();
            Hashtable resHT = new Hashtable();
            try
            {
                string strSQL = @" SELECT TOP " + pNum + " * from( select ROW_NUMBER() OVER (ORDER BY cr.OP_DATE DESC) AS RowNumber,cr.* ,crt.ID as crtID,crt.WorkWeek,crt.ToWorkTime, crt.OffWorkTime,crt.ToWorkStartTime,crt.ToWorkEndTime,crt.OffWorkStartTime,crt.OffWorkEndTime,crt.BreakTimeType, crt.BreakStartTime,crt.BreakFinishTime,crt.FlexibleWorkType,crt.AllowLateTime,crt.AllowLeaveTime,crt.EarlyArrivalDeparture ,crt.LateArrivalDeparture,crt.LateOffToWorkType,crt.LeaveLateTime,crt.ArriveLateTime,crt.OffWorkCheckinType, crp.ID as crpID,crp.CheckinUser from tbl_QY_CheckinRule cr inner join tbl_QY_CheckinRuleTime crt on crt.RuleID=cr.ID inner join tbl_QY_CheckinRulePersonnel crp on crp.RuleID=cr.ID WHERE (1=1)";
                //以微信名称为查询条件
                if (!string.IsNullOrEmpty(entity.RuleName))
                {
                    strSQL += " and cr.RuleName like '%" + entity.RuleName + "%'";
                }
                if (!entity.RuleType.Equals(-1))
                {
                    strSQL += " and cr.RuleType=" + entity.RuleType;
                }
                if (!string.IsNullOrEmpty(entity.CheckinLocation))
                {
                    strSQL += " and cr.CheckinLocation like '%" + entity.CheckinLocation + "%'";
                }
                if (entity.DelFlag != -1)
                {
                    strSQL += " and DelFlag=" + entity.DelFlag;
                }
                strSQL += " ) as A where RowNumber > (" + pNum + "* (" + pIndex + "-1))";
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    using (DataTable dd = conn.ExecuteDataTable(command))
                    {
                        foreach (DataRow idr in dd.Rows)
                        {
                            result.Add(new CheckinRuleEntity
                            {
                                ID = Convert.ToInt32(idr["ID"]),
                                RuleName = Convert.ToString(idr["RuleName"]),
                                RuleType = Convert.ToInt32(idr["RuleType"]),
                                CheckinLocation = Convert.ToString(idr["CheckinLocation"]),
                                CheckinCoordinate = Convert.ToString(idr["CheckinCoordinate"]),
                                CheckinScope = Convert.ToInt32(idr["CheckinScope"]),
                                ScopeOuterType = Convert.ToInt32(idr["ScopeOuterType"]),
                                AdditionalType = Convert.ToInt32(idr["AdditionalType"]),
                                AdditionalTime = Convert.ToInt32(idr["AdditionalTime"]),
                                AdditionalCount = Convert.ToInt32(idr["AdditionalCount"]),
                                DelFlag = Convert.ToInt32(idr["DelFlag"]),
                                RuleTimeID = Convert.ToInt32(idr["crtID"]),
                                WorkWeek = Convert.ToString(idr["WorkWeek"]),
                                ToWorkTime = Convert.ToString(idr["ToWorkTime"]),
                                OffWorkTime = Convert.ToString(idr["OffWorkTime"]),
                                ToWorkStartTime = Convert.ToString(idr["ToWorkStartTime"]),
                                OffWorkStartTime = Convert.ToString(idr["OffWorkStartTime"]),
                                OffWorkEndTime = Convert.ToString(idr["OffWorkEndTime"]),
                                BreakTimeType = Convert.ToInt32(idr["BreakTimeType"]),
                                BreakStartTime = Convert.ToString(idr["BreakStartTime"]),
                                BreakFinishTime = Convert.ToString(idr["BreakFinishTime"]),
                                FlexibleWorkType = Convert.ToInt32(idr["FlexibleWorkType"]),
                                AllowLateTime = Convert.ToInt32(idr["AllowLateTime"]),
                                AllowLeaveTime = Convert.ToInt32(idr["AllowLeaveTime"]),
                                EarlyArrivalDeparture = Convert.ToInt32(idr["EarlyArrivalDeparture"]),
                                LateArrivalDeparture = Convert.ToInt32(idr["LateArrivalDeparture"]),
                                LateOffToWorkType = Convert.ToInt32(idr["LateOffToWorkType"]),
                                LeaveLateTime = Convert.ToInt32(idr["LeaveLateTime"]),
                                ArriveLateTime = Convert.ToInt32(idr["ArriveLateTime"]),
                                OffWorkCheckinType = Convert.ToInt32(idr["OffWorkCheckinType"]),
                                RulePersonnelID = Convert.ToInt32(idr["crpID"]),
                                //CheckinDepart = Convert.ToString(idr["CheckinDepart"]),
                                CheckinUser = Convert.ToString(idr["CheckinUser"]),
                                OP_DATE = Convert.ToDateTime(idr["OP_DATE"])
                            });
                        }
                    }
                }
                resHT["rows"] = result;

                string strCount = @"Select Count(*) as TotalCount from tbl_QY_CheckinRule cr inner join tbl_QY_CheckinRuleTime crt on crt.RuleID=cr.ID inner join tbl_QY_CheckinRulePersonnel crp on crp.RuleID=cr.ID Where (1=1)";
                //以名称为查询条件
                if (!string.IsNullOrEmpty(entity.RuleName))
                {
                    strCount += " and cr.RuleName like '%" + entity.RuleName + "%'";
                }
                if (!entity.RuleType.Equals(-1))
                {
                    strCount += " and cr.RuleType=" + entity.RuleType;
                }
                if (!string.IsNullOrEmpty(entity.CheckinLocation))
                {
                    strCount += " and cr.CheckinLocation like '%" + entity.CheckinLocation + "%'";
                }
                if (entity.DelFlag != -1)
                {
                    strCount += " and DelFlag=" + entity.DelFlag;
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
        public long AddCheckinRule(CheckinRuleEntity entity)
        {
            entity.EnSafe();
            Int64 did = 0;
            string strSQL = @"INSERT INTO Tbl_QY_CheckinRule(RuleName,RuleType,CheckinLocation,CheckinCoordinate,CheckinScope,ScopeOuterType,AdditionalType,AdditionalTime,AdditionalCount,OP_DATE) VALUES (@RuleName,@RuleType,@CheckinLocation,@CheckinCoordinate,@CheckinScope,@ScopeOuterType,@AdditionalType,@AdditionalTime,@AdditionalCount,@OP_DATE)SELECT @@IDENTITY";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@RuleName", DbType.String, entity.RuleName.Trim());
                    conn.AddInParameter(cmd, "@RuleType", DbType.Int32, entity.RuleType);
                    conn.AddInParameter(cmd, "@CheckinLocation", DbType.String, entity.CheckinLocation.Trim());
                    conn.AddInParameter(cmd, "@CheckinCoordinate", DbType.String, entity.CheckinCoordinate.Trim());
                    conn.AddInParameter(cmd, "@CheckinScope", DbType.Int32, entity.CheckinScope);
                    conn.AddInParameter(cmd, "@ScopeOuterType", DbType.Int32, entity.ScopeOuterType);
                    conn.AddInParameter(cmd, "@AdditionalType", DbType.Int32, entity.AdditionalType);
                    conn.AddInParameter(cmd, "@AdditionalTime", DbType.Int32, entity.AdditionalTime);
                    conn.AddInParameter(cmd, "@AdditionalCount", DbType.Int32, entity.AdditionalCount);
                    conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                    //conn.ExecuteNonQuery(cmd);
                    did = Convert.ToInt64(conn.ExecuteScalar(cmd));
                }
                return did;
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        public long AddCheckinRuleTime(CheckinRuleEntity entity)
        {
            entity.EnSafe();
            Int64 did = 0;
            string strSQL = @"INSERT INTO Tbl_QY_CheckinRuleTime(RuleID,WorkWeek,ToWorkTime,OffWorkTime,ToWorkStartTime,ToWorkEndTime,OffWorkStartTime,OffWorkEndTime,BreakTimeType,BreakStartTime,BreakFinishTime,FlexibleWorkType,AllowLateTime,AllowLeaveTime,EarlyArrivalDeparture,LateArrivalDeparture,LateOffToWorkType,LeaveLateTime,ArriveLateTime,OffWorkCheckinType) VALUES (@RuleID,@WorkWeek,@ToWorkTime,@OffWorkTime,@ToWorkStartTime,@ToWorkEndTime,@OffWorkStartTime,@OffWorkEndTime,@BreakTimeType,@BreakStartTime,@BreakFinishTime,@FlexibleWorkType,@AllowLateTime,@AllowLeaveTime,@EarlyArrivalDeparture,@LateArrivalDeparture,@LateOffToWorkType,@LeaveLateTime,@ArriveLateTime,@OffWorkCheckinType)SELECT @@IDENTITY";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@RuleID", DbType.Int32, entity.RuleID);
                    conn.AddInParameter(cmd, "@WorkWeek", DbType.String, entity.WorkWeek.Trim());
                    conn.AddInParameter(cmd, "@ToWorkTime", DbType.String, entity.ToWorkTime.Trim());
                    conn.AddInParameter(cmd, "@OffWorkTime", DbType.String, entity.OffWorkTime.Trim());
                    conn.AddInParameter(cmd, "@ToWorkStartTime", DbType.String, entity.ToWorkStartTime.Trim());
                    conn.AddInParameter(cmd, "@ToWorkEndTime", DbType.String, entity.ToWorkEndTime.Trim());
                    conn.AddInParameter(cmd, "@OffWorkStartTime", DbType.String, entity.OffWorkStartTime.Trim());
                    conn.AddInParameter(cmd, "@OffWorkEndTime", DbType.String, entity.OffWorkEndTime.Trim());
                    conn.AddInParameter(cmd, "@BreakTimeType", DbType.Int32, entity.BreakTimeType);
                    conn.AddInParameter(cmd, "@BreakStartTime", DbType.String, entity.BreakStartTime.Trim());
                    conn.AddInParameter(cmd, "@BreakFinishTime", DbType.String, entity.BreakFinishTime.Trim());
                    conn.AddInParameter(cmd, "@FlexibleWorkType", DbType.Int32, entity.FlexibleWorkType);
                    conn.AddInParameter(cmd, "@AllowLateTime", DbType.Int32, entity.AllowLateTime);
                    conn.AddInParameter(cmd, "@AllowLeaveTime", DbType.Int32, entity.AllowLeaveTime);
                    conn.AddInParameter(cmd, "@EarlyArrivalDeparture", DbType.Int32, entity.EarlyArrivalDeparture);
                    conn.AddInParameter(cmd, "@LateArrivalDeparture", DbType.Int32, entity.LateArrivalDeparture);
                    conn.AddInParameter(cmd, "@LateOffToWorkType", DbType.Int32, entity.LateOffToWorkType);
                    conn.AddInParameter(cmd, "@LeaveLateTime", DbType.Int32, entity.LeaveLateTime);
                    conn.AddInParameter(cmd, "@ArriveLateTime", DbType.Int32, entity.ArriveLateTime);
                    conn.AddInParameter(cmd, "@OffWorkCheckinType", DbType.Int32, entity.OffWorkCheckinType);
                    //conn.ExecuteNonQuery(cmd);
                    did = Convert.ToInt64(conn.ExecuteScalar(cmd));
                }
                return did;
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        public long AddCheckinRulePersonnel(CheckinRuleEntity entity)
        {
            entity.EnSafe();
            Int64 did = 0;
            string strSQL = @"INSERT INTO Tbl_QY_CheckinRulePersonnel(RuleID,CheckinUser) VALUES (@RuleID,@CheckinUser)SELECT @@IDENTITY";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@RuleID", DbType.Int32, entity.RuleID);
                    conn.AddInParameter(cmd, "@CheckinUser", DbType.String, entity.CheckinUser.Trim());
                    //conn.ExecuteNonQuery(cmd);
                    did = Convert.ToInt64(conn.ExecuteScalar(cmd));
                }
                return did;
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 修改考勤规则
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateCheckinRule(CheckinRuleEntity entity)
        {
            entity.EnSafe();
            string strSQL = @"UPDATE Tbl_QY_CheckinRule SET RuleName =@RuleName,RuleType =@RuleType,CheckinLocation =@CheckinLocation,CheckinCoordinate =@CheckinCoordinate,CheckinScope =@CheckinScope,ScopeOuterType =@ScopeOuterType,AdditionalType =@AdditionalType,AdditionalTime =@AdditionalTime,AdditionalCount =@AdditionalCount,OP_DATE =@OP_DATE WHERE ID=@ID";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@RuleName", DbType.String, entity.RuleName.Trim());
                    conn.AddInParameter(cmd, "@RuleType", DbType.Int32, entity.RuleType);
                    conn.AddInParameter(cmd, "@CheckinLocation", DbType.String, entity.CheckinLocation.Trim());
                    conn.AddInParameter(cmd, "@CheckinCoordinate", DbType.String, entity.CheckinCoordinate.Trim());
                    conn.AddInParameter(cmd, "@CheckinScope", DbType.Int32, entity.CheckinScope);
                    conn.AddInParameter(cmd, "@ScopeOuterType", DbType.Int32, entity.ScopeOuterType);
                    conn.AddInParameter(cmd, "@AdditionalType", DbType.Int32, entity.AdditionalType);
                    conn.AddInParameter(cmd, "@AdditionalTime", DbType.Int32, entity.AdditionalTime);
                    conn.AddInParameter(cmd, "@AdditionalCount", DbType.Int32, entity.AdditionalCount);
                    conn.AddInParameter(cmd, "@OP_DATE", DbType.DateTime, DateTime.Now);
                    conn.AddInParameter(cmd, "@ID", DbType.Int32, entity.ID);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        public void UpdateCheckinRuleTime(CheckinRuleEntity entity)
        {
            entity.EnSafe();
            string strSQL = @"UPDATE Tbl_QY_CheckinRuleTime SET RuleID=@RuleID,WorkWeek=@WorkWeek,ToWorkTime=@ToWorkTime,OffWorkTime=@OffWorkTime,ToWorkStartTime=@ToWorkStartTime,ToWorkEndTime=@ToWorkEndTime,OffWorkStartTime=@OffWorkStartTime,OffWorkEndTime=@OffWorkEndTime,BreakTimeType=@BreakTimeType,BreakStartTime=@BreakStartTime,BreakFinishTime=@BreakFinishTime,FlexibleWorkType=@FlexibleWorkType,AllowLateTime=@AllowLateTime,AllowLeaveTime=@AllowLeaveTime,EarlyArrivalDeparture=@EarlyArrivalDeparture,LateArrivalDeparture=@LateArrivalDeparture,LateOffToWorkType=@LateOffToWorkType,LeaveLateTime=@LeaveLateTime,ArriveLateTime=@ArriveLateTime,OffWorkCheckinType=@OffWorkCheckinType WHERE ID=@ID";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@RuleID", DbType.Int32, entity.RuleID);
                    conn.AddInParameter(cmd, "@WorkWeek", DbType.String, entity.WorkWeek.Trim());
                    conn.AddInParameter(cmd, "@ToWorkTime", DbType.String, entity.ToWorkTime.Trim());
                    conn.AddInParameter(cmd, "@OffWorkTime", DbType.String, entity.OffWorkTime.Trim());
                    conn.AddInParameter(cmd, "@ToWorkStartTime", DbType.String, entity.ToWorkStartTime.Trim());
                    conn.AddInParameter(cmd, "@ToWorkEndTime", DbType.String, entity.ToWorkEndTime.Trim());
                    conn.AddInParameter(cmd, "@OffWorkStartTime", DbType.String, entity.OffWorkStartTime.Trim());
                    conn.AddInParameter(cmd, "@OffWorkEndTime", DbType.String, entity.OffWorkEndTime.Trim());
                    conn.AddInParameter(cmd, "@BreakTimeType", DbType.Int32, entity.BreakTimeType);
                    conn.AddInParameter(cmd, "@BreakStartTime", DbType.String, entity.BreakStartTime.Trim());
                    conn.AddInParameter(cmd, "@BreakFinishTime", DbType.String, entity.BreakFinishTime.Trim());
                    conn.AddInParameter(cmd, "@FlexibleWorkType", DbType.Int32, entity.FlexibleWorkType);
                    conn.AddInParameter(cmd, "@AllowLateTime", DbType.Int32, entity.AllowLateTime);
                    conn.AddInParameter(cmd, "@AllowLeaveTime", DbType.Int32, entity.AllowLeaveTime);
                    conn.AddInParameter(cmd, "@EarlyArrivalDeparture", DbType.Int32, entity.EarlyArrivalDeparture);
                    conn.AddInParameter(cmd, "@LateArrivalDeparture", DbType.Int32, entity.LateArrivalDeparture);
                    conn.AddInParameter(cmd, "@LateOffToWorkType", DbType.Int32, entity.LateOffToWorkType);
                    conn.AddInParameter(cmd, "@LeaveLateTime", DbType.Int32, entity.LeaveLateTime);
                    conn.AddInParameter(cmd, "@ArriveLateTime", DbType.Int32, entity.ArriveLateTime);
                    conn.AddInParameter(cmd, "@OffWorkCheckinType", DbType.Int32, entity.OffWorkCheckinType);
                    conn.AddInParameter(cmd, "@ID", DbType.Int32, entity.RuleTimeID);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        public void UpdateCheckinRulePersonnel(CheckinRuleEntity entity)
        {
            entity.EnSafe();
            string strSQL = @"UPDATE Tbl_QY_CheckinRulePersonnel SET RuleID =@RuleID,CheckinUser =@CheckinUser WHERE ID=@ID";
            try
            {
                using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(cmd, "@CheckinUser", DbType.String, entity.CheckinUser.Trim());
                    conn.AddInParameter(cmd, "@RuleID", DbType.Int32, entity.RuleID);
                    conn.AddInParameter(cmd, "@ID", DbType.Int32, entity.RulePersonnelID);
                    conn.ExecuteNonQuery(cmd);
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 删除考勤规则
        /// </summary>
        /// <param name="entity"></param>
        public void DeleteCheckinRule(List<CheckinRuleEntity> entity)
        {
            try
            {
                foreach (var it in entity)
                {
                    string strSQL = @"update Tbl_QY_CheckinRule set DelFlag=1 where ID=@ID ";

                    using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                    {
                        conn.AddInParameter(cmd, "@ID", DbType.String, it.ID);
                        conn.ExecuteNonQuery(cmd);
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 删除考勤时间
        /// </summary>
        /// <param name="entity"></param>
        public void DeleteCheckinRuleTime(List<CheckinRuleEntity> entity)
        {
            try
            {
                foreach (var it in entity)
                {
                    string strSQL = @"update Tbl_QY_CheckinRuleTime set DelFlag=1 where RuleID=@ID ";

                    using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                    {
                        conn.AddInParameter(cmd, "@ID", DbType.String, it.ID);
                        conn.ExecuteNonQuery(cmd);
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        /// <summary>
        /// 删除考勤人员
        /// </summary>
        /// <param name="entity"></param>
        public void DeleteCheckinRulePersonnel(List<CheckinRuleEntity> entity)
        {
            try
            {
                foreach (var it in entity)
                {
                    string strSQL = @"update Tbl_QY_CheckinRulePersonnel set DelFlag=1 where RuleID=@ID ";

                    using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
                    {
                        conn.AddInParameter(cmd, "@ID", DbType.String, it.ID);
                        conn.ExecuteNonQuery(cmd);
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
        }
        #endregion
        #region
        public CargoExpenseEntity QueryExpenseCheckEntity(CargoExpenseEntity entity)
        {
            CargoExpenseEntity result = new CargoExpenseEntity();
            string strSQL = "Select * from Tbl_Cargo_Expense Where (1=1)";
            if (!entity.ExID.Equals(0)) { strSQL += " and ExID=" + entity.ExID; }
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                using (DataTable dd = conn.ExecuteDataTable(cmd))
                {
                    foreach (DataRow idr in dd.Rows)
                    {
                        result.ExID = Convert.ToInt64(idr["ExID"]);
                        result.ExName = Convert.ToString(idr["ExName"]);
                        result.ExDepart = Convert.ToString(idr["ExDepart"]);
                        result.ExDate = Convert.ToDateTime(idr["ExDate"]);
                        result.ReceiveName = Convert.ToString(idr["ReceiveName"]);
                        result.ReceiveNumber = Convert.ToString(idr["ReceiveNumber"]);
                        result.ChargeType = Convert.ToString(idr["ChargeType"]);
                        result.ExCharge = Convert.ToDecimal(idr["ExCharge"]);
                        result.OperaID = Convert.ToString(idr["OperaID"]);
                        result.OperaName = Convert.ToString(idr["OperaName"]);
                        result.ExpenseDate = Convert.ToDateTime(idr["ExpenseDate"]);
                        result.Status = Convert.ToString(idr["Status"]);
                        result.DenyReason = Convert.ToString(idr["DenyReason"]);
                        result.CheckStatus = Convert.ToString(idr["CheckStatus"]);
                        result.Reason = Convert.ToString(idr["Reason"]);
                        result.NextCheckID = Convert.ToString(idr["NextCheckID"]);
                        result.NextCheckName = Convert.ToString(idr["NextCheckName"]);
                        result.UserID = Convert.ToString(idr["UserID"]);
                        result.UserName = Convert.ToString(idr["UserName"]);
                        result.HouseID = Convert.ToInt32(idr["HouseID"]);
                        result.ExType = Convert.ToString(idr["ExType"]);
                        result.ClientName = Convert.ToString(idr["ClientName"]);
                        result.ClientID = Convert.ToInt32(idr["ClientID"]);
                        result.CheckTime = string.IsNullOrEmpty(Convert.ToString(idr["CheckTime"])) ? Convert.ToDateTime("0001-01-01") : Convert.ToDateTime(idr["CheckTime"]);
                    }
                }
            }
            return result;
        }
        public void CheckExpense(CargoExpenseEntity entity)
        {
            entity.EnSafe();
            string strSQL = "Update Tbl_Cargo_Expense set NextCheckID=@NextCheckID,NextCheckName=@NextCheckName,CheckTime=@CheckTime,DenyReason=@DenyReason,Status=@Status Where ExID=@ExID";
            using (DbCommand cmd = conn.GetSqlStringCommond(strSQL))
            {
                conn.AddInParameter(cmd, "@ExID", DbType.Int64, entity.ExID);
                conn.AddInParameter(cmd, "@NextCheckID", DbType.String, entity.NextCheckID);
                conn.AddInParameter(cmd, "@NextCheckName", DbType.String, entity.NextCheckName);
                conn.AddInParameter(cmd, "@CheckTime", DbType.DateTime, entity.CheckTime);
                conn.AddInParameter(cmd, "@DenyReason", DbType.String, entity.DenyReason);
                conn.AddInParameter(cmd, "@Status", DbType.String, entity.Status);
                conn.ExecuteNonQuery(cmd);
            }
        }
        public string QueryWeixinOpenIDByUserID(string UserID)
        {
            string result = string.Empty;
            try
            {
                string strSQL = @"select top 1 OpenID from Tbl_QY_User where UserID=@UserID";
                using (DbCommand command = conn.GetSqlStringCommond(strSQL))
                {
                    conn.AddInParameter(command, "@UserID", DbType.String, UserID);
                    using (DataTable dd = conn.ExecuteDataTable(command))
                    {
                        if (dd.Rows.Count > 0) { result = Convert.ToString(dd.Rows[0]["OpenID"]); }
                    }
                }
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message); }
            return result;
        }
        #endregion
        #region 采购审批查询

        #endregion
    }
}
