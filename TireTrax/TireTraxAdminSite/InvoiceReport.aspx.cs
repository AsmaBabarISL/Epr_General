using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;

public partial class InvoiceReport : System.Web.UI.Page
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

        ReportDataSource dsInvoice = new ReportDataSource("DataSet2", Invoice.GetInvoiceForReport(InvoiceId).Tables[0]);
        ReportDataSource dsDeliveries = new ReportDataSource("DataSet1", Invoice.getInvoiceTiresInfo(OrganizationId, Conversion.ParseString(InvoiceId)).Tables[0]);
        rptVwrInvoiceReport.LocalReport.DataSources.Clear();
        rptVwrInvoiceReport.LocalReport.DataSources.Add(dsInvoice);
        rptVwrInvoiceReport.LocalReport.DataSources.Add(dsDeliveries);

    }
}