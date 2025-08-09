//===============================================================================
// Microsoft patterns & practices Enterprise Library
// Data Access Application Block
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Data.OleDb;
using System.Data.Odbc;
using House.DataAccess.Sql;

namespace House.DataAccess
{
    /// <summary>
    /// <para>Represents a factory for creating named instances of <see cref="Database"/> objects.</para>
    /// </summary>
    public class DatabaseProviderFactory : IDisposable
    {
        private DbProviderFactory m_dbProviderFactory;

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="DatabaseProviderFactory"/> class 
        /// with the default configuration source.</para>
        /// </summary>
        public DatabaseProviderFactory()
        {
            m_dbProviderFactory = SqlClientFactory.Instance;
        }

        public DatabaseProviderFactory(string dbProviderName)
        {
            switch (dbProviderName)
            {
                case "System.Data.SqlClient":
                    m_dbProviderFactory = SqlClientFactory.Instance;
                    break;
                case "System.Data.OleDb":
                    m_dbProviderFactory = OleDbFactory.Instance;
                    break;
                case "System.Data.Odbc":
                    m_dbProviderFactory = OdbcFactory.Instance;
                    break;
                default:
                    m_dbProviderFactory = SqlClientFactory.Instance;
                    break;
            }
        }

        public Database CreateDefault()
        {
            string defaultConn = House.DataAccess.Properties.Resources.ConnDeploySql2008;

            return new SqlDatabase(defaultConn);
        }

        public Database Create(string name)
        {
            string defaultConn = House.DataAccess.Properties.Resources.ConnDeploySql2008;
            return new SqlDatabase(defaultConn);
        }

        #region IDisposable Members

        public void Dispose()
        {

        }

        #endregion


    }
}
