<%@ WebHandler Language="C#" Class="GetAllTireOutComeTypes" %>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Xml.Serialization;
using TireTraxLib;
public class GetAllTireOutComeTypes : IHttpHandler {
    string outputType = string.Empty;
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        if (context.Request["OutputType"] != null)
            outputType = Convert.ToString(context.Request["OutputType"]);
        else
        {
            context.Response.Write("<TireTrax><ErrorCode>600</ErrorCode><Message> Please provide Output Type.</Message></TireTrax>");
            return;
        }

        showAllTireOutComeTypes(context);
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
    private void showAllTireOutComeTypes(HttpContext context)
    {

        DataSet ds = Tire.getAllTireOutComeTypes();
        string output = string.Empty;
        if (ds != null && ds.Tables.Count > 0)
        {
            output += (ConvertDataSetToXML(ds));
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