using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;

public partial class InvoiceReport : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int OrgId = Conversion.ParseInt(Request.QueryString["OrgId"].ToString());
            int InvoiceId = Conversion.ParseInt(Request.QueryString["InvoiceNum"].ToString());
            LoadReport(InvoiceId, OrgId);
        }
    }

    private void LoadReport(int InvoiceId, int OrganizationId)
    {
        rptVwrInvoiceReport.ProcessingMode = ProcessingMode.Local;
        rptVwrInvoiceReport.LocalReport.ReportPath = Server.MapPath("~/InvoiceReport.rdlc");
        DataTable dt = OrganizationInfo.GetLogoPath(OrganizationId);

        string str = "~/img/Logo-epr.png";
        if (dt != null && dt.Rows.Count > 0)
        {
            str = ConfigurationManager.AppSettings["LogoUploadLocation"] + dt.Rows[0]["vchLogoPath"].ToString();
            
        }

        string imagePath = new Uri(Server.MapPath(str)).AbsoluteUri;


        
        ReportDataSource dsInvoice = new ReportDataSource("DataSet2", Invoice.GetInvoiceForReport(InvoiceId).Tables[0]);
        ReportDataSource dsDeliveries;
        if (CatId == (int)ProductCategory.Tire)
        {
            dsDeliveries = new ReportDataSource("DataSet1", Invoice.getInvoiceTiresInfo(OrganizationId, Conversion.ParseString(InvoiceId)).Tables[0]);
        }
        else
        {
            dsDeliveries = new ReportDataSource("DataSet1", Invoice.GetInvoiceProductInfo(OrganizationId, Conversion.ParseInt(InvoiceId)).Tables[0]);
        }
       
        rptVwrInvoiceReport.LocalReport.DataSources.Clear();
        rptVwrInvoiceReport.LocalReport.EnableExternalImages = true;
        ReportParameter rptParam = new ReportParameter("ImagePath", imagePath);
        rptVwrInvoiceReport.LocalReport.SetParameters(rptParam);

        rptVwrInvoiceReport.LocalReport.DataSources.Add(dsInvoice);
        rptVwrInvoiceReport.LocalReport.DataSources.Add(dsDeliveries);
       // rptVwrInvoiceReport.LocalReport.DataSources.Add(dsLogoPath);
        //this.rptVwrInvoiceReport.ShowToolBar = false;

    }
}