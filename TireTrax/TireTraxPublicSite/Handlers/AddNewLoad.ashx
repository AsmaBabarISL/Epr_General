<%@ WebHandler Language="C#" Class="AddNewLoad" %>

using System;
using System.Web;
using TireTraxLib;

public class AddNewLoad : IHttpHandler {
    public string LoadNumber,outputType, PONumber, InvoiceNumber, SealNumber, TrailerNumber, HaulerIdNumber, Weight, BillOfLandingNumber, TypeId,userOrganizationId,LoginMemberId;

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/xml";
       LoadNumber= PONumber= InvoiceNumber= SealNumber= TrailerNumber= HaulerIdNumber= Weight= BillOfLandingNumber= TypeId =userOrganizationId=LoginMemberId= string.Empty;
       if (context.Request["LoadNumber"] != null)
           LoadNumber = Convert.ToString(context.Request["LoadNumber"]);
       else
       {
           context.Response.Write("<TireTrax><ErrorCode>601</ErrorCode><Message> Please provide LoadNumber</Message></TireTrax>");
           return;
       }
       
       if (context.Request["PONumber"] != null)
           PONumber = Convert.ToString(context.Request["PONumber"]);
       else
       {
           context.Response.Write("<TireTrax><ErrorCode>603</ErrorCode><Message> Please provide PO Number.</Message></TireTrax>");
           return;
       }
       if (context.Request["InvoiceNumber"] != null)
           InvoiceNumber = Convert.ToString(context.Request["InvoiceNumber"]);
       else
       {
           context.Response.Write("<TireTrax><ErrorCode>604</ErrorCode><Message> Please provide Invoice Number.</Message></TireTrax>");
           return;
       }
       if (context.Request["SealNumber"] != null)
           SealNumber = Convert.ToString(context.Request["SealNumber"]);
       else
       {
           context.Response.Write("<TireTrax><ErrorCode>605</ErrorCode><Message> Please provide Seal Number.</Message></TireTrax>");
           return;
       }
       if (context.Request["TrailerNumber"] != null)
           TrailerNumber = Convert.ToString(context.Request["TrailerNumber"]);
       else
       {
           context.Response.Write("<TireTrax><ErrorCode>606</ErrorCode><Message> Please provide Trailer Number.</Message></TireTrax>");
           return;
       }
       if (context.Request["HaulerIdNumber"] != null)
           HaulerIdNumber = Convert.ToString(context.Request["HaulerIdNumber"]);
       else
       {
           context.Response.Write("<TireTrax><ErrorCode>607</ErrorCode><Message> Please provide HaulerId Number.</Message></TireTrax>");
           return;
       }
       if (context.Request["Weight"] != null)
           Weight = Convert.ToString(context.Request["Weight"]);
       else
       {
           context.Response.Write("<TireTrax><ErrorCode>608</ErrorCode><Message> Please provide Weight.</Message></TireTrax>");
           return;
       }
       if (context.Request["BillOfLandingNumber"] != null)
           BillOfLandingNumber = Convert.ToString(context.Request["BillOfLandingNumber"]);
       else
       {
           context.Response.Write("<TireTrax><ErrorCode>609</ErrorCode><Message> Please provide Bill of Landing Number.</Message></TireTrax>");
           return;
       }
       if (context.Request["TypeId"] != null)
           TypeId = Convert.ToString(context.Request["TypeId"]);
       else
       {
           context.Response.Write("<TireTrax><ErrorCode>610</ErrorCode><Message> Please provide type Id.</Message></TireTrax>");
           return;
       }
       if (context.Request["userOrganizationId"] != null)
           userOrganizationId = Convert.ToString(context.Request["userOrganizationId"]);
       if (context.Request["LoginMemberId"] != null)
          LoginMemberId= Convert.ToString(context.Request["LoginMemberId"]);
       if (context.Request["OutputType"] != null)
           outputType = Convert.ToString(context.Request["OutputType"]);
       else
       {
           context.Response.Write("<TireTrax><ErrorCode>600</ErrorCode><Message> Please provide Output type.</Message></TireTrax>");
           return;
       }
           addnewLoad(context);
    }
    private string GenerateLotSerialNumber(int UserOrganizationId)
    {
        System.Text.StringBuilder SerialNumber = new System.Text.StringBuilder(255);
        System.Data.DataSet ds = TireTraxLib.OrganizationInfo.GetCountryAndStateCodeByOrganizationId(UserOrganizationId);//string.IsNullOrEmpty(Session["OrganizationId"].ToString()) ? Convert.ToInt32(ddlStakeholder.SelectedValue) : Convert.ToInt32(Session["OrganizationId"]));

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            SerialNumber.Append(ds.Tables[0].Rows[0][0].ToString() + ds.Tables[0].Rows[0][1].ToString());
        }

        ds.Dispose();
        ds = null;

        SerialNumber.Append(Guid.NewGuid().ToString().Substring(0, 3));
        SerialNumber.Append(Guid.NewGuid().ToString().Substring(0, 4));
        SerialNumber.Append("L");

        return SerialNumber.ToString().ToUpper();

    }
    private void addnewLoad(HttpContext context)
    {
        TireTraxLib.BarCode br = new TireTraxLib.BarCode();
        br.DateCreated = DateTime.Now.ToShortDateString();
        br.OrganizationID = Convert.ToInt32(userOrganizationId);
        br.BarCodeNumber = GenerateLotSerialNumber(Convert.ToInt32(userOrganizationId));

        // Guid g = Guid.NewGuid();
        string str = br.BarCodeNumber.ToString().Replace("-", "");
        string barcodefilename = "";
        if (br.GenerateLotBarCodeImage(str))
        {
            barcodefilename = str + ".gif";
          //  imgLotBarcode.ImageUrl = String.Format("/images/temp/{0}.Gif", str);
          //  imgLotBarcode.Visible = true;
            //txtBrand.Focus();
        }
        if (System.IO.File.Exists(context.Server.MapPath(String.Format("/images/temp/{0}", barcodefilename))))
        {
            br.Image = System.IO.File.ReadAllBytes(context.Server.MapPath(String.Format("/images/temp/{0}", barcodefilename)));

        }
        int barcodeId = TireTraxLib.BarCode.Insert(br);
        int loadId = 0;
        loadId = TireTraxLib.Loads.InsertLoad(Conversion.ParseInt(TypeId), PONumber, InvoiceNumber, SealNumber, TrailerNumber, Conversion.ParseInt(HaulerIdNumber), Weight,BillOfLandingNumber, Conversion.ParseInt(userOrganizationId), barcodeId, str,LoadNumber,Conversion.ParseInt(LoginMemberId),357);
        string output = "";
        if (string.IsNullOrEmpty(loadId.ToString())|| loadId<=0)
        {

            output += "<TireTrax><ErrorCode>612</ErrorCode> <Message>Operation failed.</Message></TireTrax>";
        }
        else
        {
            output += "<TireTrax><Message>Load added successfully</Message><Token>" + loadId + "</Token></TireTrax>";
           
        }
        showOutput(context, output);
        
    }
    private void showOutput(HttpContext context,string outputtext)
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
