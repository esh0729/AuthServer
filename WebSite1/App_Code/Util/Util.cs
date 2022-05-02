using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// DBUtil의 요약 설명입니다.
/// </summary>
public class Util
{
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Member functions

	public static SqlConnection OpenDBConnection(string sDBPath)
	{
		SqlConnection conn = new SqlConnection(sDBPath);
		conn.Open();

		return conn;
	}

	public static SqlConnection OpenUserDBConnection()
	{
		return OpenDBConnection(ConfigurationManager.AppSettings["userDbConnection"]);
	}

	public static void Close(ref SqlConnection conn)
	{
		conn.Close();
		conn = null;
	}

	public static void Commit(ref SqlTransaction trans)
	{
		trans.Commit();
		trans = null;
	}

	public static void Rollback(ref SqlTransaction trans)
	{
		trans.Rollback();
		trans = null;
	}
}