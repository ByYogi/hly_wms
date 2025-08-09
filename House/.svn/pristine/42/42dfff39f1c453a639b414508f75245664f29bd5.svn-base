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
using System.Configuration;

namespace House.DataAccess
{
	/// <summary>
	/// Contains factory methods for creating Database objects.
	/// </summary>
	public static class DatabaseFactory
	{
		/// <summary>
		/// Method for invoking a default Database object. Reads default settings
		/// from the ConnectionSettings.config file.
		/// </summary>
		/// <example>
		/// <code>
		/// Database dbSvc = DatabaseFactory.CreateDatabase();
		/// </code>
		/// </example>
		/// <returns>Database</returns>
		/// <exception cref="System.Configuration.ConfigurationException">
		/// <para>An error occured while reading the configuration.</para>
		/// </exception>
		public static Database CreateDatabase()
		{
            DatabaseProviderFactory defaultProvider = new DatabaseProviderFactory();
            return defaultProvider.CreateDefault();
		}

		/// <summary>
		/// Method for invoking a specified Database service object.  Reads service settings
		/// from the ConnectionSettings.config file.
		/// </summary>
		/// <example>
		/// <code>
		/// Database dbSvc = DatabaseFactory.CreateDatabase("SQL_Customers");
		/// </code>
		/// </example>
		/// <param name="name">configuration key for database service</param>
		/// <returns>Database</returns>
		/// <exception cref="System.Configuration.ConfigurationException">
		/// <para><paramref name="name"/> is not defined in configuration.</para>
		/// <para>- or -</para>
		/// <para>An error exists in the configuration.</para>
		/// <para>- or -</para>
		/// <para>An error occured while reading the configuration.</para>        
		/// </exception>
		/// <exception cref="System.Reflection.TargetInvocationException">
		/// <para>The constructor being called throws an exception.</para>
		/// </exception>
		public static Database CreateDatabase(string name)
		{
            if (String.IsNullOrEmpty(name)) throw new ArgumentNullException("name");

		    return InnerCreateDatabase(name);
		}

	    
        private static Database InnerCreateDatabase(string name)
	    {
	        try
	        {
                DatabaseProviderFactory defaultProvider = new DatabaseProviderFactory();
                return defaultProvider.Create(name);
	        }
	        catch (ActivationException)
	        {
	            throw;
	        }
	    }
	}

}
