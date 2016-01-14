<%@ WebHandler Language="C#" Class="AddnewTyre" %>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TireTraxLib;

public class AddnewTyre : IHttpHandler {
    public string CBarCode, DotPlant, DotSize, DotBrand, DotWeek, DotYear, Brand2Name, LoginMemberId, LanguageId, UserOrganizationId, TireClassId, TireStateId, TireRecyleId
        , BarCodeImageFileName, spaceId, laneId, lotId, outputType;

    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/xml";
        CBarCode = DotPlant = DotSize = DotBrand = DotWeek = DotYear = Brand2Name = LoginMemberId = LanguageId = UserOrganizationId = TireClassId = TireStateId = TireRecyleId
    = BarCodeImageFileName = spaceId = laneId = lotId = outputType = string.Empty;
        if (context.Request["CBarCode"] != null)
            CBarCode = Convert.ToString(context.Request["CBarCode"]);
        else
        {
            context.Response.Write("<TireTrax><ErrorCode>601</ErrorCode><Message> Please provide CBarCode.</Message></TireTrax>");
            return;
        }
        if (context.Request["DotPlant"] != null)
            DotPlant = Convert.ToString(context.Request["DotPlant"]);
        else
        {
            context.Response.Write("<TireTrax><ErrorCode>602</ErrorCode><Message> Please provide Dot Plant.</Message></TireTrax>");
            return;
        }
        if (context.Request["DotSize"] != null)
            DotSize = Convert.ToString(context.Request["DotSize"]);
        else
        {
            context.Response.Write("<TireTrax><ErrorCode>603/ErrorCode><Message> Please provide Dot size.</Message></TireTrax>");
            return;
        }
        if (context.Request["DotBrand"] != null)
            DotBrand = Convert.ToString(context.Request["DotBrand"]);
        else
        {
            context.Response.Write("<TireTrax><ErrorCode>604</ErrorCode><Message> Please provide Dot Brand.</Message></TireTrax>");
            return;
        }
        if (context.Request["DotWeek"] != null)
            DotWeek = Convert.ToString(context.Request["DotWeek"]);
        else
        {
            context.Response.Write("<TireTrax><ErrorCode>605</ErrorCode><Message> Please provide Dot Week.</Message></TireTrax>");
            return;
        }
        if (context.Request["DotYear"] != null)
            DotYear = Convert.ToString(context.Request["DotYear"]);
        else
        {
            context.Response.Write("<TireTrax><ErrorCode>606</ErrorCode><Message> Please provide Dot Year.</Message></TireTrax>");
            return;
        }
        if (context.Request["Brand2Name"] != null)
            Brand2Name = Convert.ToString(context.Request["Brand2Name"]);
        else
        {
            context.Response.Write("<TireTrax><ErrorCode>607</ErrorCode><Message> Please provide Brand 2 Name.</Message></TireTrax>");
            return;
        }
        if (context.Request["LoginMemberId"] != null)
            LoginMemberId = Convert.ToString(context.Request["LoginMemberId"]);
        else
        {
            context.Response.Write("<TireTrax><ErrorCode>608</ErrorCode><Message> Please provide Login Member Id.</Message></TireTrax>");
            return;
        }
        if (context.Request["LanguageId"] != null)
            LanguageId = Convert.ToString(context.Request["LanguageId"]);
        else
        {
            context.Response.Write("<TireTrax><ErrorCode>609</ErrorCode><Message> Please provide Language Id.</Message></TireTrax>");
            return;
        }
        if (context.Request["UserOrganizationId"] != null)
            UserOrganizationId = Convert.ToString(context.Request["UserOrganizationId"]);
        else
        {
            context.Response.Write("<TireTrax><ErrorCode>610</ErrorCode><Message> Please provide User Organization Id.</Message></TireTrax>");
            return;
        }
        if (context.Request["TireClassId"] != null)
            TireClassId = Convert.ToString(context.Request["TireClassId"]);
        else
        {
            context.Response.Write("<TireTrax><ErrorCode>611</ErrorCode><Message> Please provide Tire class Id.</Message></TireTrax>");
            return;
        }
        if (context.Request["TireStateId"] != null)
            TireStateId = Convert.ToString(context.Request["TireStateId"]);
        else
        {
            context.Response.Write("<TireTrax><ErrorCode>612</ErrorCode><Message> Please provide Tire State Id.</Message></TireTrax>");
            return;
        }
        if (context.Request["TireRecyleId"] != null)
            TireRecyleId = Convert.ToString(context.Request["TireRecyleId"]);
        else
        {
            context.Response.Write("<TireTrax><ErrorCode>613</ErrorCode><Message> Please provide Tire Recycle Id.</Message></TireTrax>");
            return;
        }
      
        if (context.Request["spaceId"] != null)
            spaceId = Convert.ToString(context.Request["spaceId"]);
        else
        {
            context.Response.Write("<TireTrax><ErrorCode>614</ErrorCode><Message> Please provide Space Id.</Message></TireTrax>");
            return;
        }
        if (context.Request["laneId"] != null)
            laneId = Convert.ToString(context.Request["laneId"]);
        else
        {
            context.Response.Write("<TireTrax><ErrorCode>615</ErrorCode><Message> Please provide Lane Id.</Message></TireTrax>");
            return;
        }
        if (context.Request["lotId"] != null)
            lotId = Convert.ToString(context.Request["lotId"]);
        else
        {
            context.Response.Write("<TireTrax><ErrorCode>616</ErrorCode><Message> Please provide Lot Id.</Message></TireTrax>");
            return;
        }
        if (context.Request["OutputType"] != null)
            outputType = Convert.ToString(context.Request["OutputType"]);
        else
        {
            context.Response.Write("<TireTrax><ErrorCode>600</ErrorCode><Message> Please provide LoadNumber</Message></TireTrax>");
            return;
        }

            generatabarcodeimagefilename();
        addnewTire(context);
    }
    private void addnewTire(HttpContext context)
    {
        Tire objInventory = new Tire();
        objInventory.C_BarCode = CBarCode == "" ? 0 : Convert.ToInt32(CBarCode);
        objInventory.TX_BarCodeId = 0;
        objInventory.DotNumber = DotPlant + DotSize + DotBrand + DotWeek + DotYear;
        objInventory.BrandId1 = 1; // Convert.ToInt32(txtBrand.Text.Trim());
        int BrandID = 0;
            string BrandName = string.Empty;
           if( string.IsNullOrEmpty(Brand2Name))
           {
           
            if (Tire.ValidateBrandCode(DotBrand, out BrandID, out BrandName))
            {
                if(!string.IsNullOrEmpty(BrandName))
                Brand2Name= BrandName;// = BrandID;
            }
           }
        objInventory.BrandId2 = BrandID; // Convert.ToInt32(txtBrand2.Text.Trim());
        objInventory.TireType = "";
        objInventory.PlantCode = DotPlant;
        objInventory.SizeNumber = DotSize;
        objInventory.MonthCode = DotWeek;
        objInventory.YearCode = DotYear;
        objInventory.DateCreated = DateTime.Now;
        objInventory.CreatedById = Conversion.ParseInt( LoginMemberId);
        objInventory.LangaugeId =Conversion.ParseInt(  LanguageId);
        objInventory.OrganizationId = Conversion.ParseInt(UserOrganizationId);//string.IsNullOrEmpty(Session["OrganizationId"].ToString()) ? Convert.ToInt32(ddlStakeholder.SelectedValue) : Convert.ToInt32(Session["OrganizationId"]);
        // objInventory.TireStateCategoryId = Convert.ToInt32(ddlTireState.SelectedValue);
        objInventory.TireClassId = Conversion.ParseInt(TireClassId);
        objInventory.TireActionId = Conversion.ParseInt(TireStateId);
        objInventory.TireOutComeID = Conversion.ParseInt(TireRecyleId);
        objInventory.SerialNumber = GenerateSerialNumber();

        //if (objInventory.TireStateCategoryId == 1)
        //    objInventory.RecycleStateId = ddlRecycleState.SelectedIndex > 0 ? Convert.ToInt32(ddlRecycleState.SelectedValue) : 0;
        //else
        //    objInventory.RetreadStateId = ddlRecycleState.SelectedIndex > 0 ? Convert.ToInt32(ddlRecycleState.SelectedValue) : 0;
       
        if (System.IO.File.Exists(context.Server.MapPath(String.Format("/Images/temp/{0}", BarCodeImageFileName))))
        {
            objInventory.Image = System.IO.File.ReadAllBytes(context.Server.MapPath(String.Format("/Images/temp/{0}", BarCodeImageFileName)));
        }
        objInventory.Space = string.Empty;
        objInventory.Lane = string.Empty;


        objInventory.TireId = Tire.addNewInventory(objInventory);
        if (System.IO.File.Exists(context.Server.MapPath(String.Format("/Images/temp/{0}", BarCodeImageFileName))))
        {
            System.IO.File.Delete(context.Server.MapPath(String.Format("/Images/temp/{0}", BarCodeImageFileName)));
        }
      BarCodeImageFileName="";
      string output = "";
        if (!Lots.insertLotsTires(Convert.ToInt32(lotId), objInventory.TireId, 0, DateTime.Now, 1, true, false, false, Convert.ToInt32(spaceId), Convert.ToInt32(laneId)))
        {

            output += ("<TireTrax><ErrorCode>617</ErrorCode> <Message>Tire not added.</Message></TireTrax>");
        }
        else
        {
            output += ("<TireTrax><Message>Tire added successfully</Message><Token>" + objInventory.TireId + "</Token></TireTrax>");
           
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
    private string GenerateSerialNumber()
    {
        string SerialNumber = "";
        System.Data.DataSet ds = OrganizationInfo.GetCountryAndStateCodeByOrganizationId(Convert.ToInt32(UserOrganizationId));//string.IsNullOrEmpty(Session["OrganizationId"].ToString()) ? Convert.ToInt32(ddlStakeholder.SelectedValue) : Convert.ToInt32(Session["OrganizationId"]));

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            SerialNumber += ds.Tables[0].Rows[0][0].ToString() + ds.Tables[0].Rows[0][1].ToString();
        }

        ds.Dispose();
        ds = null;

        SerialNumber += Guid.NewGuid().ToString().Substring(0, 3);
        SerialNumber += Guid.NewGuid().ToString().Substring(0, 5);

        return SerialNumber.ToUpper();

    }


     private string GenerateLotSerialNumber()
    {
       System.Text.StringBuilder SerialNumber = new System.Text.StringBuilder(255);
       System.Data.DataSet ds = OrganizationInfo.GetCountryAndStateCodeByOrganizationId(Conversion.ParseInt(UserOrganizationId));//string.IsNullOrEmpty(Session["OrganizationId"].ToString()) ? Convert.ToInt32(ddlStakeholder.SelectedValue) : Convert.ToInt32(Session["OrganizationId"]));

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
    protected void generatabarcodeimagefilename()
    {
       

       
            BarCode br = new BarCode();
            br.DateCreated = DateTime.Now.ToShortDateString();
            br.OrganizationID = Conversion.ParseInt(UserOrganizationId);
            br.BarCodeNumber = GenerateLotSerialNumber();

            string str = br.BarCodeNumber.ToString().Replace("-", "");


            // Guid g = Guid.NewGuid();
            if (br.GenerateLotBarCodeImage(str))
            {
                BarCodeImageFileName = str + ".gif";
             
            }
            else
            {
                //if (hdnBarCodeImageFileName.Value != "")
                //{
                //    if (System.IO.File.Exists(Server.MapPath(String.Format("/Images/temp/{0}", hdnBarCodeImageFileName.Value))))
                //    {
                //        System.IO.File.Delete(Server.MapPath(String.Format("/Images/temp/{0}", hdnBarCodeImageFileName.Value)));
                //    }
                //    hdnBarCodeImageFileName.Value = "";
                //}
                //imgBarCode.Visible = false;
            }
        }
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "SetRecycleStatebtnGenerateBarCode_Click", String.Format("ShowInventoryForm();SetRecycleState({0});", ddlTireState.SelectedValue), true);
    
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}