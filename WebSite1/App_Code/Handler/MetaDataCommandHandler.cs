using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// MetaDataCommandHandler의 요약 설명입니다.
/// </summary>
public class MetaDataCommandHandler : CommandHandler
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
			// 시스템세팅 조회
			//

			DataRow drSystemSetting = UserDB.SystemSetting(conn, null);
			int nMetaDataVersion = Convert.ToInt32(drSystemSetting["metaDataVersion"]);

			//
			// 메타데이터 버전 확인및 데이터 로드
			//

			string sMetaData = MetaDataManager.GetMetaData(nMetaDataVersion);

			JsonData response = JsonUtil.CreateObject();
			response["metaData"] = sMetaData;

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