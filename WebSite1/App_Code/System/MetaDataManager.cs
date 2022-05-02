using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;

using WebCommon;

/// <summary>
/// MetaDataCreator의 요약 설명입니다.
/// </summary>
public class MetaDataManager
{
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Static member functions

	public static string GetMetaData(int nVersion)
	{
        string sMetaData = null;

        //
        // 저장된 메타데이터가 있을경우 버전비교 데이터가 없거나 버전이 맞지 않을경우 DB에서 조회후 저장
        //

        if (!ReadMetaData(nVersion, out sMetaData))
            sMetaData = CreateMetaData(nVersion);

        return sMetaData;
	}

    private static bool ReadMetaData(int nVersion, out string sMetaData)
    {
        sMetaData = null;

        string sPath = AppDomain.CurrentDomain.BaseDirectory + @"\Meta";
        DirectoryInfo di = new DirectoryInfo(sPath);

        if (!di.Exists)
            return false;

        string sFilePath = sPath + @"\" + nVersion + ".Meta";

        if (!File.Exists(sFilePath))
            return false;

        using (StreamReader reader = File.OpenText(sFilePath))
        {
            sMetaData = reader.ReadToEnd();
        }

        return true;
    }

    private static string CreateMetaData(int nVersion)
	{
		PDMetaData metaData = MakeMetaData.CreateMetaData();
		byte[] buffer = metaData.SerializeRaw();
		string sMetaDataToBase64 = Convert.ToBase64String(buffer);

        WirteMetaData(nVersion, sMetaDataToBase64);

        return sMetaDataToBase64;
    }

    private static void WirteMetaData(int nVersion, string sMetaData)
	{
        string sPath = AppDomain.CurrentDomain.BaseDirectory + @"\Meta";
        DirectoryInfo di = new DirectoryInfo(sPath);

        if (!di.Exists)
            di.Create();

        string sFilePath = sPath + @"\" + nVersion + ".Meta";

        if (File.Exists(sFilePath))
            File.Delete(sFilePath);

        using (StreamWriter writer = File.CreateText(sFilePath))
        {
            writer.Write(sMetaData);
        }
    }
}