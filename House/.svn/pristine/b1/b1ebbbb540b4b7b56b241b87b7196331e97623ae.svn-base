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
using House.DataAccess.Properties;

namespace House.DataAccess
{
    public class GenericDatabase : Database
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericDatabase"/> class with a connection string and 
        /// a provider factory.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="dbProviderFactory">The provider factory.</param>
        public GenericDatabase(string connectionString, DbProviderFactory dbProviderFactory
        )
            : base(connectionString, dbProviderFactory)
        {
        }

        /// <summary>
        /// This operation is not supported in this class.
        /// </summary>
        /// <param name="discoveryCommand">The <see cref="DbCommand"/> to do the discovery.</param>
        /// <remarks>There is no generic way to do it, the operation is not implemented for <see cref="GenericDatabase"/>.</remarks>
        /// <exception cref="NotSupportedException">Thrown whenever this method is called.</exception>
        protected override void DeriveParameters(DbCommand discoveryCommand)
        {
            throw new NotSupportedException("Parameter discovery is not supported for connections using GenericDatabase. You must specify the parameters explicitly, or configure the connection to use a type deriving from Database that supports parameter discovery.");
        }
    }
}
