﻿using System;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Threading.Tasks;
using System.Data.Common;

namespace LittleBitPass
{
	/// <summary>
	/// This class provides the connection to the database and handles queries to return a datareader.
	/// </summary>
	public class DbConnector
	{
		// Singleton, prevent multiple DB connections.
		public static readonly DbConnector Instance = new DbConnector();

		internal MySqlConnection Connection;

		DbConnector() {
			// Connection string can be found in the Web.config file
			Connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["DbConnString"].ConnectionString);
			Connection.Open();
		}

		/// <summary>
		/// Runs the query sync against the database and returns a datareader.
		/// </summary>
		/// <returns>A datareader containing the result of the query.</returns>
		/// <param name="query">Query.</param>
		public MySqlDataReader RunQuery(string query) {
			var cmd = new MySqlCommand(query, Connection);
			return cmd.ExecuteReader();
		}

		/// <summary>
		/// Runs the query sync against the database and returns a datareader.
		/// </summary>
		/// <returns>A task containing a datareader to access the result of the query.</returns>
		/// <param name="query">Query.</param>
		public Task<DbDataReader> RunQueryAsync(string query) {
			var cmd = new MySqlCommand(query, Connection);
			return cmd.ExecuteReaderAsync();
		}
	}
}