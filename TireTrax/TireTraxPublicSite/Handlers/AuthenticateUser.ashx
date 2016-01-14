<%@ WebHandler Language="C#" Class="Login" %>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TireTraxLib;


/// <summary>
/// Summary description for Login
/// </summary>
public class Login : IHttpHandler
{
    string login = string.Empty;
    string pass = string.Empty;
    string SSID = string.Empty;
    string outputType = string.Empty;
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/xml";

        if (context.Request["login"] != null)
            login = Convert.ToString(context.Request["login"]);
        else
        {
            context.Response.Write("<TireTrax><ErrorCode>601</ErrorCode><Message> Please provide Login text.</Message></TireTrax>");
            return;
        }
        if (context.Request["SSID"] != null)
            SSID = Convert.ToString(context.Request["SSID"]);
        else
        {
            context.Response.Write("<TireTrax><ErrorCode>602</ErrorCode><Message> Please provide SSID.</Message></TireTrax>");
            return;
        }

        if (context.Request["pass"] != null)
            pass = Convert.ToString(context.Request["pass"]);
        else
        {
            context.Response.Write("<TireTrax><ErrorCode>603</ErrorCode><Message> Please provide Password.</Message></TireTrax>");
            return;
        }
        if (context.Request["OutputType"] != null)
            outputType = Convert.ToString(context.Request["OutputType"]);
        else
        {
            context.Response.Write("<TireTrax><ErrorCode>600</ErrorCode><Message> Please provide Output Type.</Message></TireTrax>");
            return;
        }
        UserInfo ui = UserInfo.AuthenticateMember(login, Encryption.Encrypt(pass), Convert.ToInt32(SSID));
        string output = string.Empty;
        if (ui != null)
        {
            if (ui.UserId > 0)
            {
                output += ("<TireTrax><Message>Authenticated successfully</Message><Token>" + ui.UserId + "</Token><UserName>" + ui.Login + "</UserName>" + "<OrginzationId>" + ui.OrganizationId + "</OrginzationId></TireTrax>");
            }
            else
            {

                output += ("<TireTrax><ErrorCode>604</ErrorCode> <Message>Authentication Failure.</Message></TireTrax>");
            }
        }
        else
        {

            output += ("<TireTrax><ErrorCode>604</ErrorCode> <Message>Authentication Failure.</Message></TireTrax>");
        }
        showOutput(context, output);


    }
    private void showOutput(HttpContext context, string outputtext)
    {
        if (outputType == "xml")
        {
            context.Response.ContentType = "text/xml";
            context.Response.Write(outputtext);
        }
        else if (outputType == "json")
        {
            context.Response.ContentType = "text/json";
            byte[] encodedString = System.Text.Encoding.UTF8.GetBytes(outputtext);

            // Put the byte array into a stream and rewind it to the beginning
            System.IO.MemoryStream ms = new System.IO.MemoryStream(encodedString);
            ms.Flush();
            ms.Position = 0;

            // Build the XmlDocument from the MemorySteam of UTF-8 encoded bytes
            System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
            xmlDoc.Load(ms); ;
            string jsonText = Newtonsoft.Json.JsonConvert.SerializeXmlNode(xmlDoc);
            context.Response.Write(jsonText);
        }
        else
        {
            context.Response.Write("<TireTrax><ErrorCode>600</ErrorCode> <Message>Please provide output type.</Message></TireTrax>");
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}

