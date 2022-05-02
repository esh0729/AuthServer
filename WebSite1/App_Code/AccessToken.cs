using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;
using System.Configuration;
using System.Security.Cryptography;

using LitJson;

/// <summary>
/// AccessToken의 요약 설명입니다.
/// </summary>
public class AccessToken
{
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Member variables

	private Guid m_userId = Guid.Empty;
	private string m_sAccessSecret = string.Empty;

	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Properties

	public Guid userId
	{
		get { return m_userId; }
	}

	public string accessSecret
	{
		get { return m_sAccessSecret; }
	}

	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Member functions

	public void Init(DataRow dr)
	{
		if (dr == null)
			throw new ArgumentNullException("dr");

		m_userId = (Guid)dr["userId"];
		m_sAccessSecret = Convert.ToString(dr["accessSecret"]);
	}

	public void Init()
	{
		m_userId = Guid.NewGuid();
		m_sAccessSecret = CreateAccessSecret();
	}

	private string CreateAccessSecret()
	{
		List<char> elements = s_secretElements.ToList();
		char[] accessSecret = new char[elements.Count];

		for (int i = 0; i < accessSecret.Length; i++)
		{
			int nCount = elements.Count;
			int nSelectNo = RandomUtil.Next(nCount);

			accessSecret[i] = elements[nSelectNo];
			elements.RemoveAt(nSelectNo);
		}

		return new string(accessSecret);
	}

	public string CreateToken()
	{
		JsonData payload = JsonUtil.CreateObject();
		payload["userId"] = m_userId.ToString();
		payload["accessSecret"] = m_sAccessSecret;

		//
		// 유저 데이터base64 변환
		//

		string sPayloadToBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(payload.ToJson()));

		//
		// 해싱
		//

		byte[] securityKey = Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["accessTokenSecurityKey"]);
		HMACMD5 hasher = new HMACMD5(securityKey);

		byte[] value = hasher.ComputeHash(Encoding.UTF8.GetBytes(sPayloadToBase64));
		string sSignature = Convert.ToBase64String(value);

		//
		// base64 데이터 및 해싱된 데이터 전달
		//

		StringBuilder sb = new StringBuilder();
		sb.Append(sPayloadToBase64);
		sb.Append('.');
		sb.Append(sSignature);

		return sb.ToString();
	}

	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Static member variables

	private static char[] s_secretElements = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
											   'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
											   'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
											   'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D',
											   'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N',
											   'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X',
											   'Y', 'Z' };
}