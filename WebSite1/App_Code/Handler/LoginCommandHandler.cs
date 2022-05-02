using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// LoginCommandHandler의 요약 설명입니다.
/// </summary>
public class LoginCommandHandler : CommandHandler
{
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Member variables

	private Guid m_id = Guid.Empty;

	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Member functions

	protected override void Parse()
	{
		base.Parse();

		string sId = m_request.QueryString["id"];
		if (string.IsNullOrEmpty(sId))
			throw new Exception("Parsing Error1.(id)");

		if (!Guid.TryParse(sId, out m_id))
			throw new Exception("Parsing Error2.(id)");
	}

	protected override JsonData Process()
	{
		SqlConnection conn = null;
		SqlTransaction trans = null;

		try
		{
			conn = Util.OpenUserDBConnection();
			trans = conn.BeginTransaction();

			//
			// 게스트사용자 정보
			//

			DataRow drGuestUser = UserDB.GuestUser(conn, trans, m_id);

			AccessToken accessToken = new AccessToken();

			if (drGuestUser != null)
				accessToken.Init(drGuestUser);
			else
			{
				//
				// 해당ID의 유저가 없을경우 생성
				//

				accessToken.Init();

				if (UserDB.AddUser(conn, trans, accessToken.userId, accessToken.accessSecret) != 0)
					throw new Exception("AddUser Failed");

				if (UserDB.AddGuestUser(conn, trans, m_id, accessToken.userId) != 0)
					throw new Exception("AddGuestUser Failed");
			}

			JsonData response = JsonUtil.CreateObject();
			response["accessToken"] = accessToken.CreateToken();

			//
			//
			//

			Util.Commit(ref trans);
			Util.Close(ref conn);

			//
			//
			//

			return response;
		}
		finally
		{
			if (conn != null)
			{
				if (trans != null)
					Util.Rollback(ref trans);

				Util.Close(ref conn);
			}
		}
	}
}