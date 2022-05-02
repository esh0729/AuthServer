<%@ WebHandler Language="C#" Class="Command" %>

using System;
using System.Web;

using LitJson;

public class Command : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        HttpRequest request = context.Request;

        JsonData responseData = null;
        string cmd = request.QueryString["cmd"];

        try
        {
            CommandHandler commandHandler = null;

            switch (cmd)
            {
                case "systemInfo": commandHandler = new SystemInfoCommandHandler(); break;
                case "metadata": commandHandler = new MetaDataCommandHandler(); break;
                case "login": commandHandler = new LoginCommandHandler(); break;

                default:
                    throw new Exception("Invalid Command");
            }

            responseData = commandHandler.OnRequest(request, cmd);
        }
        catch (Exception ex)
        {
            LogUtil.Error(cmd, ex);

            responseData = JsonUtil.CreateObject();
            responseData["returnCode"] = -1;
            responseData["error"] = ex.Message;
        }

        context.Response.ContentType = "text/plain";
        context.Response.Write(responseData.ToJson());
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}