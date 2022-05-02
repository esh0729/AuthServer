using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// RandomUtil의 요약 설명입니다.
/// </summary>
public class RandomUtil
{
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Static member variables

	private static Random s_random = new Random();
	private static Object m_syncObject = new object();

	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Member functions

	public static int Next(int nMaxValue)
	{
		lock (m_syncObject)
		{
			return s_random.Next(nMaxValue);
		}
	}
}