using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using LitJson;

/// <summary>
/// CommandHandler의 요약 설명입니다.
/// </summary>
public abstract class CommandHandler
{
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Member variables

	protected HttpRequest m_request = null;

	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Member functions

	public JsonData OnRequest(HttpRequest request, string sCommandName)
	{
		m_request = request;

		JsonData response = JsonUtil.CreateObject();

		try
		{
			Parse();

			JsonData data = Process();

			response["returnCode"] = 0;
			response["response"] = data;
		}
		catch (Exception ex)
		{
			LogUtil.Error(sCommandName, ex);

			response["returnCode"] = 1;
			response["error"] = ex.Message;
		}

		return response;
	}

	protected virtual void Parse()
	{
	}

	protected abstract JsonData Process();

	protected bool GetParameter(string sKey, out string sParameter)
	{
		sParameter = m_request.QueryString[sKey];

		if (sParameter == null)
			return false;

		return true;
	}

	protected bool GetParameter(string sKey, out int nParameter)
	{
		return int.TryParse(m_request.QueryString[sKey], out nParameter);
	}
}