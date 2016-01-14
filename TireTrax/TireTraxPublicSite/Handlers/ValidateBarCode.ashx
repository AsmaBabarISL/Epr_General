<%@ WebHandler Language="C#"  Class="ValidateBarCode" %>


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TireTraxLib;


/// <summary>
/// Summary description for ValidateBarCode
/// </summary>
public class ValidateBarCode : IHttpHandler
{
    string barCode = string.Empty;
    string latitude = string.Empty;
    string longitude = string.Empty;
    string crypID = string.Empty;
    string deviceID = string.Empty;
    string deviceType = string.Empty;
    string Description = string.Empty;
    string outputType = string.Empty;

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/xml";

        if (context.Request["bcode"] != null)
            barCode = Convert.ToString(context.Request["bcode"]);
        else
        {
            context.Response.Write("<TireTrax><ErrorCode>601</ErrorCode><Message> Please provide Barcode.</Message></TireTrax>");
            return;
        }

        if (context.Request["lat"] != null)
            latitude = Convert.ToString(context.Request["lat"]);
        else
        {
            context.Response.Write("<TireTrax><ErrorCode>602</ErrorCode><Message> Please provide Latitude.</Message></TireTrax>");
            return;
        }

        if (context.Request["long"] != null)
            longitude = Convert.ToString(context.Request["long"]);
        else
        {
            context.Response.Write("<TireTrax><ErrorCode>603</ErrorCode><Message> Please provide Longitude.</Message></TireTrax>");
            return;
        }

        if (context.Request["crypID"] != null)
            crypID = Convert.ToString(context.Request["crypID"]);
        else
        {
            context.Response.Write("<TireTrax><ErrorCode>604</ErrorCode><Message> Please provide Cryp Id.</Message></TireTrax>");
            return;
        }

        if (context.Request["deviceID"] != null)
            deviceID = Convert.ToString(context.Request["deviceID"]);
        else
        {
            context.Response.Write("<TireTrax><ErrorCode>605</ErrorCode><Message> Please provide Devide Id.</Message></TireTrax>");
            return;
        }

        if (context.Request["deviceType"] != null)
            deviceType = Convert.ToString(context.Request["deviceType"]);
        else
        {
            context.Response.Write("<TireTrax><ErrorCode>606</ErrorCode><Message> Please provide Device Type.</Message></TireTrax>");
            return;
        }

        if (context.Request["desc"] != null)
            Description = Convert.ToString(context.Request["desc"]);
        else
        {
            context.Response.Write("<TireTrax><ErrorCode>607</ErrorCode><Message> Please provide Description.</Message></TireTrax>");
            return;
        }
        if (context.Request["OutputType"] != null)
            outputType = Convert.ToString(context.Request["OutputType"]);
        else
        {
            context.Response.Write("<TireTrax><ErrorCode>608</ErrorCode><Message> Please provide Output Type.</Message></TireTrax>");
            return;
        }

        string output = string.Empty;
        BarCode bc = new BarCode(barCode);
        if (bc != null)
        {
            if (bc.BarCodeID > 0)
            {
                UserInfo ui = new UserInfo(crypID, true);
                if (ui != null)
                {
                    if (ui.UserId > 0)
                    {
                        BarCode.InsertBarCode(bc, ui.UserId, latitude, longitude, deviceID, deviceType, Description);
                        output += ("<BarCode><Message>1</Message></BarCode>");
                    }
                    else
                    {
                        output += ("<TireTrax><ErrorCode>609</ErrorCode><Message> User is not Authenticated.</Message></TireTrax>");
                    }
                }
                else
                {
                    output += ("<TireTrax><ErrorCode>609</ErrorCode><Message> User is not Authetnicated.</Message></TireTrax>");
                }
            }
            else
            {
                output += ("<TireTrax><ErrorCode>610</ErrorCode><Message> Invalid Bar Code.</Message></TireTrax>");
            }
        }
        else
        {
            output += ("<TireTrax><ErrorCode>611</ErrorCode><Message>Invalid Bar Code.</Message></TireTrax>");
        }
        showOutput(context: context, outputtext: output);
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
