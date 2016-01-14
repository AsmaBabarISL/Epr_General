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


public partial class InvoiceAggregateAdmin : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int Month = Conversion.ParseInt(Request.QueryString["Month"].ToString());
            int Year = Conversion.ParseInt(Request.QueryString["Year"].ToString());
            LoadReport(Month, Year);
        }
    }

    private void LoadReport(int Month, int Year)
    {
        rptVwrInvoiceReport.ProcessingMode = ProcessingMode.Local;
        rptVwrInvoiceReport.LocalReport.ReportPath = Server.MapPath("~/InvoiceAggregateAdmin.rdlc");
        DataTable dt = OrganizationInfo.GetLogoPath(UserOrganizationId);

        string str = "~/img/Logo-epr.png";
        if (dt != null && dt.Rows.Count > 0)
            str = ConfigurationManager.AppSettings["LogoUploadLocation"] + dt.Rows[0]["vchLogoPath"].ToString();

        string imagePath = new Uri(Server.MapPath(str)).AbsoluteUri;
        ReportDataSource ds = new ReportDataSource("Aggregate1", Invoice.GetAggregateInvoiceForAdminReport(Month, Year).Tables[0]);


        rptVwrInvoiceReport.LocalReport.DataSources.Clear();
        rptVwrInvoiceReport.LocalReport.EnableExternalImages = true;
        //ReportParameter rptParam = new ReportParameter("ImagePath", imagePath);
        //rptVwrInvoiceReport.LocalReport.SetParameters(rptParam);
        rptVwrInvoiceReport.LocalReport.DataSources.Add(ds);
        //this.rptVwrInvoiceReport.ShowToolBar = false;
    }
}