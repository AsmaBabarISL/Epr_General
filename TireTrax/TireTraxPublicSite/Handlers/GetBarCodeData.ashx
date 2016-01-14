<%@ WebHandler Language="C#"  Class="GetBarCodeData" %>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Xml.Serialization;
using TireTraxLib;


    /// <summary>
    /// Summary description for GetBarCodeData
    /// </summary>
    public class GetBarCodeData : IHttpHandler
    {

        private string barcode = string.Empty;
        string outputType = string.Empty;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/xml";
            if (context.Request["bCode"] != null)
                barcode = Convert.ToString(context.Request["bCode"]);
            else
            {
                context.Response.Write("<TireTrax><ErrorCode>601</ErrorCode><Message> Please provide Bar Code.</Message></TireTrax>");
                return;
            }
            if (context.Request["OutputType"] != null)
                outputType = Convert.ToString(context.Request["OutputType"]);
            else
            {
                context.Response.Write("<TireTrax><ErrorCode>600</ErrorCode><Message> Please provide Output Type.</Message></TireTrax>");
                return;
            }
            List<BarCode> barCodeList = GetBarCodes(barcode);
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

        private List<BarCode> GetBarCodes(string BarCodeValue)
        {
            DataTable table = BarCode.GetBarCodesByValue(BarCodeValue);
            List<BarCode> dataList = new List<BarCode>();
            foreach (DataRow row in table.Rows)
            {
                BarCode bc = new BarCode();
                bc.BarCodeID = Convert.ToInt32(row["intBarcodeID"]);
                bc.Latitude = Convert.ToString(row["latitude"]);
                bc.Longitude = Convert.ToString(row["longitude"]);
                bc.DeviceID = Convert.ToString(row["vchDeviceID"]);
                bc.DeviceType = Convert.ToString(row["vchDeviceType"]);
                bc.BarCodeNumber = Convert.ToString(row["BarCodeNumber"]);
                bc.Description = Convert.ToString(row["vchDescription"]);
                bc.DateCreated = Convert.ToDateTime(row["dtmDateTime"]).ToString("f");
                bc.PreviousTime = Utils.GetMediaAgeString(Conversion.ParseDateTime(Conversion.ParseString(row["dtmDateTime"])));
                //bc.Location = Utils.GetlatLongFormattedTireTrax(bc.Latitude, bc.Longitude);
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
