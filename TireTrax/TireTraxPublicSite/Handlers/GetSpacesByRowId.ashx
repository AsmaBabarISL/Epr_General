<%@ WebHandler Language="C#" Class="GetSpacesByRowId" %>

using System;
using System.Web;
using TireTraxLib;
using System.Data;

public class GetSpacesByRowId : IHttpHandler {
    public static string RowId, outputType;
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/xml";
        RowId = outputType = string.Empty;
        if (context.Request["RowId"] != null)
            RowId = Convert.ToString(context.Request["RowId"]);
        else
        {
            context.Response.Write("<TireTrax><ErrorCode>601</ErrorCode><Message> Please provide Row Id.</Message></TireTrax>");
            return;
        }
        if (context.Request["OutputType"] != null)
            outputType = Convert.ToString(context.Request["OutputType"]);
        else
        {
            context.Response.Write("<TireTrax><ErrorCode>600</ErrorCode><Message> Please provide Output Type.</Message></TireTrax>");
            return;
        }

        getLotSpacesByRowId(context);
        
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
    private void getLotSpacesByRowId(HttpContext context)
    {
        DataSet ds = null;
        if (context.Request["RowId"] != null)
        {
            ds = LotSpace.getLotSpacebyRowId(Conversion.ParseInt(RowId));

        }
        string output = string.Empty;
        if (ds != null && ds.Tables.Count > 0)
        {
            output+=(ConvertDataSetToXML(ds));
        }
        else
        {

            output += ("<TireTrax><ErrorCode>602</ErrorCode> <Message>No data found.</Message></TireTrax>");
        }
        showOutput(context, output);
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