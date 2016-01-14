<%@ WebHandler Language="C#" Class="AddNewLoadForTransfer" %>

using System;
using System.Web;
using TireTraxLib;
public class AddNewLoadForTransfer : IHttpHandler {
    public string LotId,RowId,SpaceId, TypeId, UserOrganizationId,LoginMemberId,outputType;
    public void ProcessRequest (HttpContext context) {

        context.Response.ContentType = "text/xml";
       LotId=RowId=SpaceId = TypeId = UserOrganizationId = LoginMemberId=outputType= string.Empty;
        if (context.Request["LotId"] != null)
            LotId = Convert.ToString(context.Request["LotId"]);
        else
        {
            context.Response.Write("<TireTrax><ErrorCode>601</ErrorCode><Message> Please provide Lot Id.</Message></TireTrax>");
            return;
        }
        if (context.Request["RowId"] != null)
            RowId = Convert.ToString(context.Request["RowId"]);
        else
        {
            context.Response.Write("<TireTrax><ErrorCode>602</ErrorCode><Message> Please provide Row Id.</Message></TireTrax>");
            return;
        }
        if (context.Request["SpaceId"] != null)
            SpaceId = Convert.ToString(context.Request["SpaceId"]);
        else
        {
            context.Response.Write("<TireTrax><ErrorCode>603</ErrorCode><Message> Please provide Space Id.</Message></TireTrax>");
            return;
        }        
        if (context.Request["TypeId"] != null)
            TypeId = Convert.ToString(context.Request["TypeId"]);
        else
        {
            context.Response.Write("<TireTrax><ErrorCode>604</ErrorCode><Message> Please provide TypeId.</Message></TireTrax>");
            return;
        }
        if (context.Request["LoginMemberId"] != null)
            LoginMemberId = Convert.ToString(context.Request["LoginMemberId"]);
        else
        {
            context.Response.Write("<TireTrax><ErrorCode>605</ErrorCode><Message> Please provide Login Member Id.</Message></TireTrax>");
            return;  
        }
        if (context.Request["userOrganizationId"] != null)
            UserOrganizationId = Convert.ToString(context.Request["userOrganizationId"]);
        else
        {
            context.Response.Write("<TireTrax><ErrorCode>606</ErrorCode><Message> Please provide User Organization Id.</Message></TireTrax>");
            return;
        }
        if (context.Request["OutputType"] != null)
            outputType = Convert.ToString(context.Request["OutputType"]);
        else
        {
            context.Response.Write("<TireTrax><ErrorCode>600</ErrorCode><Message> Please provide Output Type.</Message></TireTrax>");
            return;
        }
        addnewLoadForTransfer(context);
        
        
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


    private void addnewLoadForTransfer(HttpContext context)
    {
        TireTraxLib.BarCode br = new TireTraxLib.BarCode();
        br.DateCreated = DateTime.Now.ToShortDateString();
        br.OrganizationID = Convert.ToInt32(UserOrganizationId);
        br.BarCodeNumber = GenerateLotSerialNumber(Convert.ToInt32(UserOrganizationId));

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
        string loadname = Guid.NewGuid().ToString().Substring(0, 6);
        loadId = TireTraxLib.Loads.InsertLoad(Conversion.ParseInt(TypeId), string.Empty, string.Empty, string.Empty, string.Empty, 0, string.Empty, string.Empty, Conversion.ParseInt(UserOrganizationId), 0, str, loadname, Conversion.ParseInt(LoginMemberId), Conversion.ParseInt(LotId), Conversion.ParseInt(RowId), Conversion.ParseInt(SpaceId));
        string output = "";
        if (string.IsNullOrEmpty(loadId.ToString()) || loadId <= 0)
        {

            output += ("<TireTrax><ErrorCode>607</ErrorCode> <Message>Operation failed.</Message></TireTrax>");
        }
        else
        {
            output += ("<TireTrax><Message>Load added successfully</Message><Token>" + loadId + "</Token></TireTrax>");
            
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