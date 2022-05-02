using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// SystemSettingCommandHandler의 요약 설명입니다.
/// </summary>
public class SystemInfoCommandHandler : CommandHandler
{
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Member functions
	protected override JsonData Process()
	{
		SqlConnection conn = null;

		try
		{
			conn = Util.OpenUserDBConnection();

			//
			// DB 작업
			//

			DataRow drSystemSetting = UserDB.SystemSetting(conn, null);
			DataRowCollection drcGameServer = UserDB.GameServers(conn, null);

			//
			//
			//

			JsonData response = JsonUtil.CreateObject();

			//
			// 시스템세팅 조회
			//

			JsonData systemSetting = JsonUtil.CreateObject();
			systemSetting["metaDataVersion"] = Convert.ToInt32(drSystemSetting["metaDataVersion"]);

			response["systemSetting"] = systemSetting;

			//
			// 게임서버 조회
			//

			JsonData gameServers = JsonUtil.CreateArray();
			foreach (DataRow drGameServer in drcGameServer)
			{
				JsonData gameServer = JsonUtil.CreateObject();
				gameServer["gameServerId"] = Convert.ToInt32(drGameServer["gameServerId"]);
				gameServer["connectionAddress"] = Convert.ToString(drGameServer["connectionAddress"]);
				gameServer["heroNameRegex"] = Convert.ToString(drGameServer["heroNameRegex"]);

				gameServers.Add(gameServer);
			}

			response["gameServers"] = gameServers;

			//
			//
			//

			Util.Close(ref conn);

			//
			//
			//

			return response;
		}
		finally
		{
			if (conn != null)
				Util.Close(ref conn);
		}
	}
}