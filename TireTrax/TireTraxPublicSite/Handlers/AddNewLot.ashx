<%@ WebHandler Language="C#" Class="AddNewLot" %>

using System;
using System.Web;
using TireTraxLib;
using System.Text;

public class AddNewLot : IHttpHandler {
    string UserOrganizationId, loginMemberId, outputType; // incomming values
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/xml";
        if (context.Request["UserOrganizationId"] != null)
            UserOrganizationId = Convert.ToString(context.Request["UserOrganizationId"]);
        else
        {
            context.Response.Write("<TireTrax><ErrorCode>601</ErrorCode><Message> Please provide User Organization Id.</Message></TireTrax>");
            return;
        }
        if (context.Request["loginMemberId"] != null)
            loginMemberId = Convert.ToString(context.Request["loginMemberId"]);
        else
        {
            context.Response.Write("<TireTrax><ErrorCode>602</ErrorCode><Message> Please provide Login Member Id.</Message></TireTrax>");
            return;
        }

       
        if (context.Request["OutputType"] != null)
            outputType = Convert.ToString(context.Request["OutputType"]);
        else
        {
            context.Response.Write("<TireTrax><ErrorCode>600</ErrorCode><Message> Please provide OutputType</Message></TireTrax>");
            return;
        }
        if (((!string.IsNullOrEmpty(UserOrganizationId)) && (!string.IsNullOrEmpty(loginMemberId))))
        {
            int lotid; string barcodeImageUrl, LotNumber,barcodeImageFileName; //returning values;
            OrganizationInfo oi = new OrganizationInfo(Convert.ToInt32(UserOrganizationId));
            int id = 0;
            TireTraxLib.Lots lot = new TireTraxLib.Lots();
            string lotNum = Guid.NewGuid().ToString().Substring(0, 6);
            LotNumber = "Lot# " + lotNum;
            lot.LotNumber = lotNum;//txtLotNmber.Text;
            lot.Quantity = 0;
            lot.OrganizationId = Convert.ToInt32(UserOrganizationId);// Convert.ToInt32(ddlStakeholder.SelectedValue);
            lot.DateCreated = DateTime.Now;//DateTime.Parse(txtdate.Text.ToString());
            
            lot.IsActive = true;
            lot.SpaceId = 1;
            lot.UserID = Convert.ToInt32(loginMemberId);// currentUserInfo.UserId;
            lot.RoleID = oi.RoleId;// currentUserInfo.RoleId;
            lot.IsCompleted = false;


            BarCode br = new BarCode();
            br.DateCreated = DateTime.Now.ToShortDateString();
            br.OrganizationID = oi.OrganizationId;
            br.BarCodeNumber = GenerateLotSerialNumber(oi.OrganizationId);
            Guid g = Guid.NewGuid();
            barcodeImageFileName = "";
            string str = g.ToString().Replace("-", "");
            if (GenerateLotBarCodeImage(str,context))
            {
                barcodeImageFileName = str + ".gif";
               barcodeImageUrl = String.Format("/Images/temp/{0}.Gif", str);
               // imgLotBarcode.Visible = true;
            }
            if (System.IO.File.Exists(context.Server.MapPath(String.Format("/Images/temp/{0}", barcodeImageFileName))))
            {
                br.Image = System.IO.File.ReadAllBytes(context.Server.MapPath(String.Format("/Images/temp/{0}", barcodeImageFileName)));

            }
            lot.BarCodeId = BarCode.Insert(br);

          
             //   lot.SubLot =Convert.ToBoolean( isSubLot);
           
            string serialNumber = Lots.insertLot(lot, out id, str);
            lotid = id;

            string output = "";
            if (string.IsNullOrEmpty(lotid.ToString()) || lotid <= 0)
            {

                output += ("<TireTrax><ErrorCode>604</ErrorCode> <Message>Error occured.Lot cannot be added.</Message></TireTrax>");
            }
            else
            {
                output += ("<TireTrax><Message>Lot added successfully</Message><Token>" + lotid.ToString() + "</Token></TireTrax>");

            }
            showOutput(context, output);

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
    private string GenerateLotSerialNumber(int orgId)
    {
        StringBuilder SerialNumber = new StringBuilder(255);
        System.Data.DataSet ds = OrganizationInfo.GetCountryAndStateCodeByOrganizationId(orgId);//string.IsNullOrEmpty(Session["OrganizationId"].ToString()) ? Convert.ToInt32(ddlStakeholder.SelectedValue) : Convert.ToInt32(Session["OrganizationId"]));

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
    private bool GenerateLotBarCodeImage(string g,HttpContext context)
    {
        try
        {
            //if (hdnLotBarCodeImageFileName.Value != "")
            //{
            //    if (System.IO.File.Exists(context.Server.MapPath(String.Format("/Images/temp/{0}", hdnLotBarCodeImageFileName.Value))))
            //    {
            //        System.IO.File.Delete(context.Server.MapPath(String.Format("/Images/temp/{0}", hdnLotBarCodeImageFileName.Value)));
            //    }
            //    hdnLotBarCodeImageFileName.Value = "";
            //}
            string Code = g;// new Guid().ToString().Replace("-", "");//txtLotNmber.Text;

            System.Drawing.Bitmap oBitmap = new System.Drawing.Bitmap((Code.Length * 30), 110);

            System.Drawing.Graphics oGraphics = System.Drawing.Graphics.FromImage(oBitmap);
            oGraphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.White), 0, 0, (Code.Length * 30), 110);

            System.Drawing.Text.PrivateFontCollection pfc = new System.Drawing.Text.PrivateFontCollection();
            pfc.AddFontFile(context.Server.MapPath("/Font/IDAutomationHC39M.ttf"));
            System.Drawing.Font oFont = new System.Drawing.Font(pfc.Families[0], 18);

            oGraphics.DrawString("*" + Code + "*", oFont, new System.Drawing.SolidBrush(System.Drawing.Color.Black), 20, 10);

            oBitmap.Save(context.Server.MapPath(String.Format("/Images/temp/{0}.Gif", g)), System.Drawing.Imaging.ImageFormat.Gif);

            oBitmap.Dispose();
            oGraphics.Dispose();
            oFont.Dispose();
            pfc.Dispose();

            oBitmap = null;
            oGraphics = null;
            oFont = null;
            pfc = null;
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(Convert.ToInt32(loginMemberId), "GenerateBarCodeImageAndBytes", ex);
            return false;
        }

        return true;
    }
    public bool IsReusable {
        get {
            return false;
        }
    }

}