<%@ WebHandler Language="C#" Class="AddTireToLoad" %>

using System;
using System.Web;
using TireTraxLib;

public class AddTireToLoad : IHttpHandler {

    public string LoadId, BarCode, LoginMemberId, outputType;

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/xml";
        LoadId = BarCode = LoginMemberId = outputType=string.Empty;
        if (context.Request["LoadId"] != null)
            LoadId = Convert.ToString(context.Request["LoadId"]);
        else
        {
            context.Response.Write("<TireTrax><ErrorCode>601</ErrorCode><Message> Please provide Load Id.</Message></TireTrax>");
            return;
        }
        if (context.Request["BarCode"] != null)
            BarCode = Convert.ToString(context.Request["BarCode"]);
        else
        {
            context.Response.Write("<TireTrax><ErrorCode>602</ErrorCode><Message> Please provide Barcode.</Message></TireTrax>");
            return;
        }
        if (context.Request["LoginMemberId"] != null)
            LoginMemberId = Convert.ToString(context.Request["LoginMemberId"]);
        else
        {
            context.Response.Write("<TireTrax><ErrorCode>603</ErrorCode><Message> Please provide Login Member Id.</Message></TireTrax>");
            return;
        }
        if (context.Request["OutputType"] != null)
            outputType = Convert.ToString(context.Request["OutputType"]);
        else
        {
            context.Response.Write("<TireTrax><ErrorCode>600</ErrorCode><Message> Please provide Output Type.</Message></TireTrax>");
            return;
        }
         addnewTire(context);
    }
    private void addnewTire(HttpContext context)
    {
      bool isAdded=  Loads.AddLoadTiresbyBarcode(Convert.ToInt32(LoadId),BarCode,Conversion.ParseInt(LoginMemberId));
      string output = "";
        if (isAdded)
        {
            output+=("<TireTrax><Message>Tire moved successfully</Message><Token>1</Token></TireTrax>");
        }
        else
        {

            output += ("<TireTrax><ErrorCode>604</ErrorCode> <Message>Tire already exists in load.</Message></TireTrax>");
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
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}