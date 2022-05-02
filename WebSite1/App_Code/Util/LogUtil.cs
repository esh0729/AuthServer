using System;
using System.IO;
using System.Text;

/// <summary>
/// LogUtil의 요약 설명입니다.
/// </summary>
public class LogUtil
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Constants

    private const string kTimeStringFormat = "[yyyy'-'MM'-'dd' 'HH':'mm':'ss,fff]";

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Static member functions

    //
    // INFO
    //

    public static void Info(string sCommandName, string sMessage)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(DateTimeOffset.Now.ToString(kTimeStringFormat));
        sb.Append(" | ");
        sb.Append("INFO");
        sb.Append(" | ");
        sb.Append(sCommandName);
        sb.Append(" : ");
        sb.Append(sMessage);

        WriteLog(sb);
    }

    //
    // WARN
    //

    public static void Warn(string sCommandName, string sMessage)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(DateTimeOffset.Now.ToString(kTimeStringFormat));
        sb.Append(" | ");
        sb.Append("WARN");
        sb.Append(" | ");
        sb.Append(sCommandName);
        sb.Append(" : ");
        sb.Append(sMessage);

        WriteLog(sb);
    }

    //
    // ERROR
    //

    public static void Error(string sCommandName, string sMessage)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(DateTimeOffset.Now.ToString(kTimeStringFormat));
        sb.Append(" | ");
        sb.Append("ERROR");
        sb.Append(" | ");
        sb.Append(sCommandName);
        sb.Append(" : ");
        sb.Append(sMessage);

        WriteLog(sb);
    }

    public static void Error(string sCommandName, Exception ex)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(DateTimeOffset.Now.ToString(kTimeStringFormat));
        sb.Append(" | ");
        sb.Append("ERROR");
        sb.Append(" | ");
        sb.Append(sCommandName);
        sb.Append(" : ");
        sb.Append(ex.Message);
        sb.AppendLine();
        sb.Append(ex.StackTrace);

        WriteLog(sb);
    }

    //
    //
    //

    private static void WriteLog(StringBuilder sb)
    {
        string sPath = AppDomain.CurrentDomain.BaseDirectory + @"\Error";
        DirectoryInfo di = new DirectoryInfo(sPath);

        if (!di.Exists)
            di.Create();

        string sDate = DateTimeOffset.Now.ToString("yyyy-MM-dd");
        string sFilePath = sPath + @"\" + sDate + ".txt";

        if (!File.Exists(sFilePath))
        {
            using (StreamWriter writer = File.CreateText(sFilePath))
            {
                writer.WriteLine(sb.ToString());
            }
        }
        else
        {
            using (StreamWriter writer = File.AppendText(sFilePath))
            {
                writer.WriteLine(sb.ToString());
            }
        }
    }
}