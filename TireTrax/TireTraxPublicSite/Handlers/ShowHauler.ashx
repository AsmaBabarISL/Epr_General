<%@ WebHandler Language="C#" Class="ShowHauler" %>

using System;
using System.Web;
using TireTraxLib;
using System.Data;


public class ShowHauler : IHttpHandler {
    public string LoginMemberId, UserOrganizationId, outputType; 
    public void ProcessRequest (HttpContext context) {
        
        LoginMemberId = UserOrganizationId = outputType = string.Empty;
        if (context.Request["LoginMemberId"] != null)
            LoginMemberId = Convert.ToString(context.Request["LoginMemberId"]);
        else
        {
            context.Response.Write("<TireTrax><ErrorCode>601</ErrorCode><Message> Please provide Login Member Id.</Message></TireTrax>");
            return;
        }
        if (context.Request["UserOrganizationId"] != null)
            UserOrganizationId = Convert.ToString(context.Request["UserOrganizationId"]);
        else
        {
            context.Response.Write("<TireTrax><ErrorCode>602</ErrorCode><Message> Please provide User Organization Id.</Message></TireTrax>");
            return;
        }
        if (context.Request["OutputType"] != null)
            outputType = Convert.ToString(context.Request["OutputType"]);
        else
        {
            context.Response.Write("<TireTrax><ErrorCode>600</ErrorCode><Message> Please provide Output Type</Message></TireTrax>");
            return;
        }
        try
        {
            showHauler(context);
        }
        catch
        {
            context.Response.ContentType = "text/xml";
            context.Response.Write("<TireTrax><ErrorCode>603</ErrorCode> <Message>No data found.</Message></TireTrax>");
        }
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
    private void showHauler(HttpContext context)
    {
        UserInfo userobj = new UserInfo(Conversion.ParseInt(LoginMemberId));
        DataSet ds;
        if (userobj.RoleName.Trim().ToString() == "Stewardship")
        {
             ds = Lots.GetAllHaulerbyOrganizationId(Conversion.ParseInt(UserOrganizationId));
            
        }
        else
        {
             ds = Lots.GetOrganizationsbyStewardship(Conversion.ParseInt(UserOrganizationId),357);
           
        }
        string output = string.Empty;
        if (ds != null && ds.Tables.Count > 0&& ds.Tables[0].Rows.Count>0)
        {
            output+=(ConvertDataSetToXML(ds));
        }
        else
        {

            output += ("<TireTrax><ErrorCode>603</ErrorCode> <Message>No data found.</Message></TireTrax>");
        }
        showOutput(context: context, outputtext: output);
    }
    public string ConvertDataSetToXML(System.Data.DataSet xmlDS)
    {
        xmlDS.DataSetName = "TireTrax";
        System.IO.MemoryStream stream = null;
        System.Xml.XmlTextWriter writer = null;
        try
        {
            stream = new System.IO.MemoryStream();
            // Load the XmlTextReader from the stream
            writer = new System.Xml.XmlTextWriter(stream, System.Text.Encoding.Unicode);
            // Write to the file with the WriteXml method.
            xmlDS.WriteXml(writer);
            int count = (int)stream.Length;
            byte[] arr = new byte[count];
            stream.Seek(0, System.IO.SeekOrigin.Begin);
            stream.Read(arr, 0, count);
            System.Text.UnicodeEncoding utf = new System.Text.UnicodeEncoding();
            return utf.GetString(arr).Trim();
        }
        catch
        {
            return String.Empty;
        }
        finally
        {
            if (writer != null) writer.Close();
        }
    }
    public bool IsReusable {
        get {
            return false;
        }
    }

}