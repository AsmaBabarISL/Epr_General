<%@ WebHandler Language="C#" Class="GetAllBarCodes" %>


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Xml.Serialization;
using TireTraxLib;

/// <summary>
/// Summary description for GetAllBarCodes
/// </summary>
public class GetAllBarCodes : IHttpHandler
{
    private string UserID = string.Empty;
    string outputType = string.Empty;
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/xml";
        if (context.Request["userID"] != null)
            UserID = Convert.ToString(context.Request["userID"]);
        else
        {
            context.Response.Write("<TireTrax><ErrorCode>601</ErrorCode><Message> Please provide User Id.</Message></TireTrax>");
            return;
        }
          if (context.Request["OutputType"] != null)
            outputType = Convert.ToString(context.Request["OutputType"]);
          else
          {
              context.Response.Write("<TireTrax><ErrorCode>600</ErrorCode><Message> Please provide Output Type.</Message></TireTrax>");
              return;
          }
        UserInfo ui = new UserInfo(UserID, true);
        List<BarCode> barCodeList = GetBarCodes(Conversion.ParseInt(UserID));
        if (outputType == "xml")
        {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            XmlSerializer xs = new XmlSerializer(typeof(List<BarCode>));
            xs.Serialize(context.Response.OutputStream, barCodeList, ns);
        }
        else if (outputType == "json")
        {
            string strJson = Newtonsoft.Json.JsonConvert.SerializeObject(barCodeList, Newtonsoft.Json.Formatting.Indented);
            context.Response.Write(strJson);
        }
        else
        {
            context.Response.Write("<TireTrax><ErrorCode>600</ErrorCode> <Message>Please provide output type.</Message></TireTrax>");
        }
    }

    private List<BarCode> GetBarCodes(int UserID)
    {
        DataTable table = BarCode.GetBarCodesByUserID(UserID);
        List<BarCode> dataList = new List<BarCode>();
        foreach (DataRow row in table.Rows)
        {
            BarCode bc = new BarCode();   
            bc.BarCodeNumber = Convert.ToString(row["BarCodeNumber"]);
       
            dataList.Add(bc);
        }
        return dataList;
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}
