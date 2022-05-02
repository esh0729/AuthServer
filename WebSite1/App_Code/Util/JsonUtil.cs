using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using LitJson;

/// <summary>
/// JsonUtil의 요약 설명입니다.
/// </summary>
public class JsonUtil
{
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Static member functions

	public static JsonData CreateObject()
	{
		JsonData jsonData = new JsonData();
		jsonData.SetJsonType(JsonType.Object);

		return jsonData;
	}

	public static JsonData CreateArray()
	{
		JsonData jsonData = new JsonData();
		jsonData.SetJsonType(JsonType.Array);

		return jsonData;
	}
}