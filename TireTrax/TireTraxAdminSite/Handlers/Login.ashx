<%@ WebHandler Language="C#" Class="Login" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using TireTraxLib;

public class Login : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        //context.Response.Write("Hello World");
        string key1 = context.Request["Login"];
        if (!string.IsNullOrEmpty(context.Request["Login"]))
        {
            string loginText = Convert.ToString(context.Request["Login"]);
            string result = GetResult(loginText);
            context.Response.Write(result);
        }
        else
        {
            context.Response.Write("No Result");
        }

      
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

    public string GetResult(string loginText)
    {
        DataTable dt = UserInfo.CheckUserLogin(loginText);
        string result = Convert.ToString(dt.Rows[0][0]);
        return result;
    }

}