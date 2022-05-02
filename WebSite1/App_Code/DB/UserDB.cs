using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// UserDB의 요약 설명입니다.
/// </summary>
public class UserDB
{
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Static member functions

	public static int AddUser(SqlConnection conn, SqlTransaction trans, Guid userId, string sAccessSecret)
	{
		SqlCommand sc = new SqlCommand();
		sc.Connection = conn;
		sc.Transaction = trans;
		sc.CommandType = CommandType.StoredProcedure;
		sc.CommandText = "uspApi_AddUser";
		sc.Parameters.Add("@userId", SqlDbType.UniqueIdentifier).Value = userId;
		sc.Parameters.Add("@sAccessSecret", SqlDbType.VarChar, 100).Value = sAccessSecret;
		sc.Parameters.Add("ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

		sc.ExecuteNonQuery();

		return (int)sc.Parameters["ReturnValue"].Value;
	}

	public static int AddGuestUser(SqlConnection conn, SqlTransaction trans, Guid guestId, Guid userId)
	{
		SqlCommand sc = new SqlCommand();
		sc.Connection = conn;
		sc.Transaction = trans;
		sc.CommandType = CommandType.StoredProcedure;
		sc.CommandText = "uspApi_AddGuestUser";
		sc.Parameters.Add("@guestId", SqlDbType.UniqueIdentifier).Value = guestId;
		sc.Parameters.Add("@userId", SqlDbType.UniqueIdentifier).Value = userId;
		sc.Parameters.Add("ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

		sc.ExecuteNonQuery();

		return (int)sc.Parameters["ReturnValue"].Value;
	}

	public static DataRow GuestUser(SqlConnection conn, SqlTransaction trans, Guid guestId)
	{
		SqlCommand sc = new SqlCommand();
		sc.Connection = conn;
		sc.Transaction = trans;
		sc.CommandType = CommandType.StoredProcedure;
		sc.CommandText = "uspApi_GuestUser";
		sc.Parameters.Add("@guestId", SqlDbType.UniqueIdentifier).Value = guestId;

		DataTable dt = new DataTable();

		SqlDataAdapter sda = new SqlDataAdapter();
		sda.SelectCommand = sc;
		sda.Fill(dt);

		return dt.Rows.Count > 0 ? dt.Rows[0] : null;
	}

	public static DataRow GameConfig(SqlConnection conn, SqlTransaction trans)
	{
		SqlCommand sc = new SqlCommand();
		sc.Connection = conn;
		sc.Transaction = trans;
		sc.CommandType = CommandType.StoredProcedure;
		sc.CommandText = "uspApi_GameConfig";

		DataTable dt = new DataTable();

		SqlDataAdapter sda = new SqlDataAdapter();
		sda.SelectCommand = sc;
		sda.Fill(dt);

		return dt.Rows.Count > 0 ? dt.Rows[0] : null;
	}

	public static DataRowCollection Characters(SqlConnection conn, SqlTransaction trans)
	{
		SqlCommand sc = new SqlCommand();
		sc.Connection = conn;
		sc.Transaction = trans;
		sc.CommandType = CommandType.StoredProcedure;
		sc.CommandText = "uspApi_Characters";

		DataTable dt = new DataTable();

		SqlDataAdapter sda = new SqlDataAdapter();
		sda.SelectCommand = sc;
		sda.Fill(dt);

		return dt.Rows;
	}

	public static DataRowCollection Continents(SqlConnection conn, SqlTransaction trans)
	{
		SqlCommand sc = new SqlCommand();
		sc.Connection = conn;
		sc.Transaction = trans;
		sc.CommandType = CommandType.StoredProcedure;
		sc.CommandText = "uspApi_Continents";

		DataTable dt = new DataTable();

		SqlDataAdapter sda = new SqlDataAdapter();
		sda.SelectCommand = sc;
		sda.Fill(dt);

		return dt.Rows;
	}

	public static DataRow SystemSetting(SqlConnection conn, SqlTransaction trans)
	{
		SqlCommand sc = new SqlCommand();
		sc.Connection = conn;
		sc.Transaction = trans;
		sc.CommandType = CommandType.StoredProcedure;
		sc.CommandText = "uspApi_SystemSetting";

		DataTable dt = new DataTable();

		SqlDataAdapter sda = new SqlDataAdapter();
		sda.SelectCommand = sc;
		sda.Fill(dt);

		return dt.Rows.Count > 0 ? dt.Rows[0] : null;
	}

	public static DataRowCollection GameServers(SqlConnection conn, SqlTransaction trans)
	{
		SqlCommand sc = new SqlCommand();
		sc.Connection = conn;
		sc.Transaction = trans;
		sc.CommandType = CommandType.StoredProcedure;
		sc.CommandText = "uspApi_GameServers";

		DataTable dt = new DataTable();

		SqlDataAdapter sda = new SqlDataAdapter();
		sda.SelectCommand = sc;
		sda.Fill(dt);

		return dt.Rows;
	}

	public static DataRowCollection CharacterActions(SqlConnection conn, SqlTransaction trans)
	{
		SqlCommand sc = new SqlCommand();
		sc.Connection = conn;
		sc.Transaction = trans;
		sc.CommandType = CommandType.StoredProcedure;
		sc.CommandText = "uspApi_CharacterActions";

		DataTable dt = new DataTable();

		SqlDataAdapter sda = new SqlDataAdapter();
		sda.SelectCommand = sc;
		sda.Fill(dt);

		return dt.Rows;
	}
}