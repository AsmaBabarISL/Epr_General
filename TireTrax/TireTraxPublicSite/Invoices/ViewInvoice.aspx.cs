using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;
using System.Data;
using System.Text;
using SelectPdf;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Diagnostics;

public partial class Invoices_ViewInvoice : BasePage
{
    //int month = 0;
    //int year = 0;


    public int CurPageNum
    {
        get
        {
            if (Request.QueryString["p"] != null)
                return Conversion.ParseInt(Request.QueryString["p"]);
            else
                return 1;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        #region Permission
        GetPermission(ResourceType.Invoices, ref canView, ref canAdd, ref canUpdate, ref canDelete);
        if (!canView)
        {
            Response.Redirect("error");
        }
        else if (!canAdd)
        {
            addInvoicenote.Visible = false;
        }
        #endregion

        ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", String.Format("SetHeaderMenu('liBA','{0}');", ResourceMgr.GetMessage("Invoices")), true);

        //ScriptManager.RegisterStartupScript(this, GetType(), "AddDataPicker", "SetDatePicket();", true);
        if (!IsPostBack)
        {
            lblEmailMsg.Visible = false;
            lblEmailMsg.Text = string.Empty;
            InvoiceInfo(1);
            System.Web.UI.WebControls.ListItem li1 = new System.Web.UI.WebControls.ListItem();
            li1.Text = "--Select--";
            li1.Value = "0";
            ddlYear.Items.Add(li1);
            int fromYear = 2012;
            for (int year = fromYear; year <= DateTime.Now.Year; year++)
            {
                System.Web.UI.WebControls.ListItem li = new System.Web.UI.WebControls.ListItem();
                li.Text = year.ToString();
                li.Value = year.ToString();
                ddlYear.Items.Add(li);
            }
        }

        if (TotalItemsR > 0)
        {
            pgrAggregate.DrawPager(CurrentPageR, this.TotalItemsR, pageSize, MaxPagesToShow);
            pgrLoad.DrawPager(CurrentPageR, TotalItemsR, pageSize, MaxPagesToShow);
        }
        else
        {
            pgrAggregate.DrawPager(CurrentPageR, this.TotalItemsR, pageSize, MaxPagesToShow);
            pgrLoad.DrawPager(CurrentPageR, TotalItemsR, pageSize, MaxPagesToShow);
        }
    }
    #region Button Events

    /// <summary>
    /// search the specific record
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnInvoiceSearch_Click(object sender, EventArgs e)
    {
        InvoiceInfo(1);
    }
    protected void btnDeliveryDetailBack_Click(object sender, EventArgs e)
    {
        //dvMainLoad.Visible = false;
    }

    #endregion
    #region Load Function

    /// <summary>
    /// use for paging
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    protected override bool OnBubbleEvent(object source, EventArgs args)
    {
        if (this.pgrLoad.Equals(source))
        {
            CommandEventArgs cmdArgs = (CommandEventArgs)args;
            CurrentPage = Convert.ToInt32(cmdArgs.CommandArgument);

            this.InvoiceInfo(CurrentPage);
        }

        if (this.pgrAggregate.Equals(source))
        {
            CommandEventArgs cmdArgs = (CommandEventArgs)args;
            CurrentPageR = Convert.ToInt32(cmdArgs.CommandArgument);

            this.LoadAggregateInvoices(CurrentPageR);
        }

        return base.OnBubbleEvent(source, args);
    }
    /// <summary>
    /// use to load the Main grid from database 
    /// </summary>
    /// <param name="pageNo"></param>
    protected void InvoiceInfo(int pageNo)
    {
        try
        {
            //pageSize = 10;
            gvInvoicesinfo.PageSize = pageSize;
            CurrentPageR = pageNo;
            DateTime frmDate = string.IsNullOrEmpty(txtFrmDate.Text) ? DateTime.MinValue : Convert.ToDateTime(txtFrmDate.Text);
            DateTime toDate = string.IsNullOrEmpty(txtToDate.Text) ? DateTime.MinValue : Convert.ToDateTime(txtToDate.Text);
            int status = Convert.ToInt32(ddlStatus.SelectedValue);
            int count = 0;
            //DataSet ds = Invoice.getAllInvoices(UserOrganizationId, pageNo, pageSize, out count, frmDate, toDate, txtOrganizationName.Text, txtInvoiceNo.Text, CatId, status);
            gvInvoicesinfo.DataSource = Invoice.getAllInvoices(UserOrganizationId, pageNo, pageSize, out count, frmDate, toDate, txtOrganizationName.Text, txtInvoiceNo.Text, CatId, status);
            gvInvoicesinfo.DataBind();

            this.TotalItemsR = count;
            this.pgrLoad.DrawPager(pageNo, TotalItemsR, pageSize, MaxPagesToShow);
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "ViewDeliveryNotes.DeliveryInfo", ex);
        }

    }
    /// <summary>
    /// use to show the Delivery detail in popup
    /// </summary>
    /// <param name="DeliveryID"></param>
    public void LoadPopInfobyInvoiceId(int InvoiceID)
    {
        try
        {
            Invoice objInvoice = new Invoice(InvoiceID);
            lblTo.Text = Conversion.ParseString(objInvoice.OrganizationForName);
            lblInvoiceNumber.Text = Conversion.ParseString(objInvoice.InvoiceNumber);
            lblinvoiceDate.Text = Conversion.ParseString(objInvoice.InvoiceDate.ToShortDateString());
            //lblInvoiceAmount.Text = objInvoice.InvoiceAmount.ToString("C");
            lblDate.Text = Conversion.ParseString(objInvoice.DueDate.ToShortDateString());
            lblOrganizationId.Text = objInvoice.Organizationid.ToString();
            lblInvoiceId.Text = InvoiceID.ToString();
            DataSet ds = Invoice.GetInvoiceForReport(objInvoice.InvoiceID);
            DataTable dt = ds.Tables[0];
            DataRow dr = dt.Rows[0];
            lblOrgForAddress.Text = dr["OrgForAddress"].ToString();
            lblOrgForPhone.Text = dr["OrganizationForPhone"].ToString();
            lblOrgForEmail.Text = dr["OrganizationForEmail"].ToString();
            lblOrgAddress.Text = dr["OrgAddress"].ToString();
            lblOrgPhone.Text = dr["OrganizationPhone"].ToString();
            lblOrgEmail.Text = dr["OrganizationEmail"].ToString();

            DataSet ds2;
            if (CatId == (int)ProductCategory.Tire)
            {
                ds2 = Invoice.getInvoiceTiresInfo(objInvoice.Organizationid, Conversion.ParseString(InvoiceID));
                gvInvoicesDetails.DataSource = ds2;
                gvInvoicesDetails.DataBind();
            }
            else
            {
                ds2 = Invoice.GetInvoiceDetailsByInvoiceId(InvoiceID);
                gvInvoiceDetailsForProduct.DataSource = ds2;
                gvInvoiceDetailsForProduct.DataBind();
            }

            DataRow dr2 = ds2.Tables[0].Rows[0];
            lblDeliveryName.Text = dr2["DeliveryName"].ToString();
            int subtotal = 0;
            for (int index = 0; index < ds2.Tables[0].Rows.Count; index++)
            {
                if (ds2.Tables[0].Rows[index]["AggrAmount"] != DBNull.Value)
                    subtotal += Convert.ToInt32(ds2.Tables[0].Rows[index]["AggrAmount"]);
            }
            lblSubTotal.Text = subtotal.ToString();
            lblBalance.Text = "0";
            lblTotal.Text = (Convert.ToInt32(lblSubTotal.Text) + Convert.ToInt32(lblBalance.Text)).ToString();
        }
        catch (Exception ex)
        {

            new SqlLog().InsertSqlLog(0, "ViewDeliveryNotes.LoadPopInfobyInvoiceId", ex);
        }
    }
    public void LoadPaymentDetailPopInfobyInvoiceId(int InvoiceID)
    {

        Payment payment = Payment.GetPaymentDetailByInvoiceId(InvoiceID);
        if (payment.PaymentId != 0)
        {
            lblPaymentId.Text = payment.PaymentId.ToString();
            //lblPaymentTypeId.Text = payment.PaymentTypeId;
            lblCheckNumber.Text = payment.CheckNumber;
            lblPaymentDate.Text = payment.PaymentDate.ToShortDateString();
            //lblStatus.Text = payment.Status;
            lblBalanceAmount.Text = payment.BalanceAmount.ToString();
            //lblCreatedBy.Text = payment.CreatedBy;
            //lblDateCreated.Text = payment.DateCreated;
            //lblModifiedBy.Text = payment.ModifiedBy;
            //lblDateModified.Text = payment.DateModified;
            lblPaymentDetail.Visible = false;
            frmPaymentDetail.Visible = true;
        }
        else
        {
            frmPaymentDetail.Visible = false;
            lblPaymentDetail.Visible = true;
        }

    }
    protected void btnInvoiceDetailBack_Click(object sender, EventArgs e)
    {
        dvInvoicepopup.Visible = false;
    }

    protected void btnPaymentDetailCancel_Click(object sender, EventArgs e)
    {
        dvPaymentDetail.Visible = false;
    }
    #endregion
    #region Grid Events
    protected void gvInvoicesinfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "InvoiceInfo")
        {
            dvInvoicepopup.Visible = true;
            LoadPopInfobyInvoiceId(Convert.ToInt32(e.CommandArgument));
        }
        if (e.CommandName == "PaymentDetail")
        {
            dvPaymentDetail.Visible = true;
            LoadPaymentDetailPopInfobyInvoiceId(Convert.ToInt32(e.CommandArgument));
        }
        else if (e.CommandName == "Send")
        {
            bool result = Invoice.Invoice_IsSend(Convert.ToInt32(e.CommandArgument));
            InvoiceInfo(1);
        }
        else if (e.CommandName == "Paid")
        {
            bool result = Invoice.Invoice_IsPaid(Convert.ToInt32(e.CommandArgument));
            InvoiceInfo(1);
        }
        else if (e.CommandName == "EditInvoice")
        {
            dvInvoicePaymentPopup.Visible = true;
            LoadPaymentPopup(Convert.ToInt32(e.CommandArgument));

            LoadBankAccountInfo(UserInfo.GetCurrentUserInfo().UserId);
            InvoiceInfo(1);
        }
        else if (e.CommandName == "PaidInFull")
        {
            int invoiceId = Conversion.ParseInt(e.CommandArgument);
            DataSet ds = Invoice.GetInvoiceForReport(invoiceId);

            Payment objPayment = new Payment();
            
            objPayment.Active = true;
            objPayment.PaymentAmount = Conversion.ParseDecimal(ds.Tables[0].Rows[0]["InvoiceAmount"]);
            objPayment.CheckNumber = txtCheckNumber.Text;
            objPayment.CreatedBy = UserInfo.GetCurrentUserInfo().UserId;
            objPayment.DateCreated = DateTime.Now;
            objPayment.PaymentDate = DateTime.Now;
            objPayment.InvoiceId = invoiceId;
            objPayment.BalanceAmount = 0;
            int isPaid = 1;
            if (Payment.InsertPayment(objPayment, isPaid))
            {
                dvInvoicePaymentPopup.Visible = false;
                //lblSuccess.Text = "Invoice Paid!";
                //lblSuccess.Visible = true;
                SendNotification(invoiceId);
                ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOutInline();", true);
            }

            InvoiceInfo(1);

        }



    }
    private void LoadBankAccountInfo(int UserId)
    {
        try
        {
            DataSet ds = BankAccounts.GetPrimaryBankAccount(UserId);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                LoadBankAccountInfo(ds.Tables[0]);
            }
            else
            {
                lblNoPrimaryBankAccountOrCreditCard.Text = "Please Make a primary Bank Account and then proceed with the payments";
                lblNoPrimaryBankAccountOrCreditCard.Visible = true;
                ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOutInline();", true);
                dvInvoicePaymentPopup.Visible = false;
            }
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "ViewInvoice.LoadBankAccountInfo", ex);
        }
    }
    private void LoadBankAccountInfo(DataTable dt)
    {
        try
        {
            string accountNumber = dt.Rows[0]["vchAccountNumber"].ToString();
            accountNumber = string.Concat("".PadLeft(accountNumber.Length, '*'), accountNumber.Substring(accountNumber.Length - 4));

            lblAccountNumber.Text = accountNumber;
            lblAccountTitle.Text = dt.Rows[0]["vchAccountTitle"].ToString();
            lblBankName.Text = dt.Rows[0]["vchBankName"].ToString();
            lblBranckName.Text = dt.Rows[0]["vchBranch"].ToString();
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "ViewInvoice.LoadBankAccountInfo", ex);
        }

    }
    private void LoadPaymentPopup(int InvoiceId)
    {
        try
        {
            //Utils.GetLookUpData<DropDownList>(ref ddlPaymentMode, LookUps.PaymentType);
            Invoice objInvoice = new Invoice(InvoiceId);
            hdnInvoiceId.Value = InvoiceId.ToString();
            lblInvoicePaymentId.Text = InvoiceId.ToString();
            lblInvoicePaymentDueDate.Text = objInvoice.DueDate.ToString("MM/dd/yyyy");
            lblInvoicePaymentAmount.Text = objInvoice.InvoiceAmount.ToString("0.0");
            txtValidation.Text = objInvoice.InvoiceAmount.ToString("0.0");
            lblInvoicePaymentDate.Text = objInvoice.DateCreated.ToString("MM/dd/yyyy");
            lblInvoicePaymentBalance.Text = objInvoice.BalanceAmount.ToString();
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "ViewInvoice.LoadPaymentPopup", ex);
        }
    }
    protected void btnInvoicePaymentCancel_Click(object sender, EventArgs e)
    {
        dvInvoicePaymentPopup.Visible = false;
    }
    protected void btnInvoicePaymentCheck_Click(object sender, EventArgs e)
    {
        if (IsGroupValid("Check"))
        {
            Payment payment = Payment.GetPaymentDetailByInvoiceId(Conversion.ParseInt(hdnInvoiceId.Value.ToString()));
            if (payment != null && payment.PaymentId != null)
            {
                UpdateCheckInfo(payment);
            }
            else
                InsertCheckInfo();
            InvoiceInfo(1);
            txtCheckNumber.Text = "";
            txtPaymentAmount.Text = "";
            dvInvoicePaymentPopup.Visible = false;
        }

    }

    private void UpdateCheckInfo(Payment payment)
    {
        try
        {
            int invoiceId = Conversion.ParseInt(hdnInvoiceId.Value.ToString());
            payment.CheckNumber = txtCheckNumber.Text;
            payment.DateModified = DateTime.Now;
            payment.BalanceAmount = payment.BalanceAmount - Convert.ToDecimal(txtPaymentAmount.Text);
            payment.InvoiceId = invoiceId;
            int isPaid = 0;
            if (payment.BalanceAmount == 0)
            {
                isPaid = 1;
            }
            if (Payment.UpdatePayment(payment, isPaid))
            {
                dvInvoicePaymentPopup.Visible = false;
                lblSuccess.Text = "Invoice Paid!";
                lblSuccess.Visible = true;
                SendNotification(invoiceId);
                ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOutInline();", true);
            }
        }
        catch(Exception ex)
        {

        }
    }
    #region Validation
    protected bool IsGroupValid(string sValidationGroup)
    {
        foreach (BaseValidator validator in Page.Validators)
        {
            if (validator.ValidationGroup == sValidationGroup)
            {
                bool fValid = validator.IsValid;
                if (fValid)
                {
                    validator.Validate();
                    fValid = validator.IsValid;
                    validator.IsValid = true;
                }
                if (!fValid)
                    return false;
            }

        }
        return true;
    }
    #endregion

    private void InsertCheckInfo()
    {
        try
        {
            Payment objPayment = new Payment();
            int invoiceId = Conversion.ParseInt(hdnInvoiceId.Value.ToString());
            objPayment.Active = true;
            objPayment.PaymentAmount = Conversion.ParseDecimal(txtPaymentAmount.Text);
            objPayment.CheckNumber = txtCheckNumber.Text;
            objPayment.CreatedBy = UserInfo.GetCurrentUserInfo().UserId;
            objPayment.DateCreated = System.DateTime.Now;
            objPayment.DateModified = DateTime.Now;
            objPayment.PaymentDate = System.DateTime.Now;
            objPayment.InvoiceId = Conversion.ParseInt(lblInvoicePaymentId.Text);
            objPayment.BalanceAmount = Convert.ToDecimal(lblInvoicePaymentAmount.Text) - Convert.ToDecimal(txtPaymentAmount.Text);
            int isPaid = 0;
            if (objPayment.BalanceAmount == 0)
            {
                isPaid = 1;
            }
            if (Payment.InsertPayment(objPayment,isPaid))
            {
                dvInvoicePaymentPopup.Visible = false;
                //lblSuccess.Text = "Invoice Paid!";
                //lblSuccess.Visible = true;
                SendNotification(invoiceId);
                ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOutInline();", true);
            }
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "ViewInvoice.InsertCheckInfo", ex);
        }
    }
    private void SendNotification(int invoiceId)
    {

        try
        {
            Invoice objInvoice = new Invoice(invoiceId);

            Notifications objNotifications = new Notifications();
            objNotifications.BitIsActive = true;
            objNotifications.BitIsReaded = false;
            objNotifications.DtmDateCreated = DateTime.Now;
            objNotifications.DtmDateReaded = DateTime.MinValue;
            objNotifications.IntFromOrganizationId = UserOrganizationId;
            objNotifications.IntFromUserId = 0;
            objNotifications.IntNotificationId = 0;
            objNotifications.IntParentNotificationId = 0;
            objNotifications.IntSourceId = 0;
            objNotifications.IntToOrganizationId = objInvoice.Organizationid;
            objNotifications.IntToUserId = 0;
            objNotifications.VchNotificationText = "Invoice with Invoice Number " + objInvoice.InvoiceNumber + " has been paid by " + objInvoice.OrganizationForName;
            objNotifications.InsertUpdate();
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "ViewInvoice.SendNotification", ex);
        }
    }
    //For converting backcolor of today's paid invoices in gridview
    protected void gvInvoicesinfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (DataBinder.Eval(e.Row.DataItem, "PaymentDate") != DBNull.Value)
            {
                DateTime dt = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "PaymentDate"));
                if (dt == DateTime.Today)
                {
                    e.Row.BackColor = System.Drawing.Color.LightCyan;
                }

            }

            if (currentUserInfo.OrganizationType.ToString().Trim().ToLower() == "stewardship" && currentUserInfo.SubRoleName.ToString().Trim().ToLower() == "admin")
            {
                Label lnkRejected = (Label)e.Row.FindControl("lnkRejected");
                if (lnkRejected.Visible)
                {
                    LinkButton ImgbtnEditInvoice = (LinkButton)e.Row.FindControl("ImgbtnEditInvoice");
                    LinkButton ImgbtnPaidFull = (LinkButton)e.Row.FindControl("ImgbtnPaidFull");
                    ImgbtnEditInvoice.Visible = true;
                    ImgbtnPaidFull.Visible = true;
                }
            }
        }
    }


    //protected void gvInvoicesinfo_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        LinkButton imgApproved = (LinkButton)e.Row.FindControl("lnkApproved");
    //        LinkButton imgRejected = (LinkButton)e.Row.FindControl("lnkRejected");
    //        //LinkButton imgPending = (LinkButton)e.Row.FindControl("lnkPending");
    //        //LinkButton imgbtnSend = (LinkButton)e.Row.FindControl("lnkbtnSend");
    //        //LinkButton imgbtnPaid = (LinkButton)e.Row.FindControl("lnkbtnPaid");



    //        if (Conversion.ParseDBNullInt(DataBinder.Eval(e.Row.DataItem, "Organizationid")) == UserOrganizationId)
    //        {
    //            if (Conversion.ParseDBNullBool(DataBinder.Eval(e.Row.DataItem, "IsSent")) == false)
    //            {
    //                imgApproved.Visible = false;
    //                imgRejected.Visible = false;
    //                //imgPending.Visible = true;
    //                //imgbtnSend.Visible = true;
    //                //imgbtnPaid.Visible = false;

    //                //imgbtnEditLoad.Visible = false;


    //            }




    //            //else if (Conversion.ParseDBNullBool(DataBinder.Eval(e.Row.DataItem, "IsSent")) == true)
    //            //{
    //            //    imgApproved.Visible = false;
    //            //    imgRejected.Visible = true;
    //            //    imgPending.Visible = false;
    //            //    imgbtnEditLoad.Visible = false;
    //            //}
    //            //else
    //            //{
    //            //    imgApproved.Visible = false;
    //            //    imgRejected.Visible = false;
    //            //    imgPending.Visible = true;
    //            //}
    //        }
    //        else
    //        {
    //            if (Conversion.ParseDBNullInt(DataBinder.Eval(e.Row.DataItem, "OrganizationForToId")) == UserOrganizationId)
    //            {
    //                //e.Row.BackColor = System.Drawing.Color.LightBlue;
    //                if (Conversion.ParseDBNullBool(DataBinder.Eval(e.Row.DataItem, "IsPaid")) == true)
    //                {
    //                    imgApproved.Visible = true;
    //                    imgRejected.Visible = false;
    //                    //imgPending.Visible = false;
    //                    //imgbtnSend.Visible = false;
    //                    //imgbtnPaid.Visible = false;

    //                }
    //                else
    //                {

    //                    //imgbtnPaid.Visible = true;
    //                }
    //            }

    //        }


    //    }
    //}

    #endregion
    #region Print

    private void PrintDiv(string invoiceNumber)
    {
        string baseUrl = HttpContext.Current.Server.MapPath("/");

        StringBuilder sb = new StringBuilder();

        sb.Append("<table style=\"width: 75%;margin-top: 110px;font-size:xx-large;\" align=\"center\"><tr style=\"width:100%;border-bottom:solid 1px;\">");
        sb.Append(String.Format("<td style=\"width: 50%; border-right: solid 1px; border-bottom: 1px solid; border-left: 1px solid; font-weight: bold; border-top: 1px solid;\">{0} </td>", ResourceMgr.GetMessage("To")));
        sb.Append("<td style=\"width: 50%; border-right: solid 1px; border-bottom: 1px solid;border-top:1px solid;\">");
        sb.Append(lblTo.Text + "</td></tr>");


        sb.Append("<tr style=\"width:100%;border-bottom:solid 1px;\">");
        sb.Append(String.Format("<td style=\"width: 50%; border-right: solid 1px; border-bottom: 1px solid; border-left: 1px solid; font-weight: bold; \">{0}</td>", ResourceMgr.GetMessage("Invoice Number")));
        sb.Append(String.Format("<td style=\"width: 50%; border-right: solid 1px; border-bottom: 1px solid;\">{0}</td>", lblInvoiceNumber.Text));
        sb.Append("</tr>");

        sb.Append("<tr style=\"width:100%;border-bottom:solid 1px;\">");
        sb.Append(String.Format("<td style=\"width: 50%; border-right: solid 1px; border-bottom: 1px solid; border-left: 1px solid; font-weight: bold; \">{0}</td>", ResourceMgr.GetMessage("Invoice Date")));
        sb.Append(String.Format("<td style=\"width: 50%; border-right: solid 1px; border-bottom: 1px solid;\">{0}</td>", lblinvoiceDate.Text));
        sb.Append("</tr>");

        sb.Append("<tr style=\"width:100%;border-bottom:solid 1px;\">");
        sb.Append(String.Format("<td style=\"width: 50%; border-right: solid 1px; border-bottom: 1px solid; border-left: 1px solid; font-weight: bold; \">{0}</td>", ResourceMgr.GetMessage("Invoice Amount")));
        //sb.Append(String.Format("<td style=\"width: 50%; border-right: solid 1px; border-bottom: 1px solid;\">{0}</td>", lblInvoiceAmount.Text));
        sb.Append("</tr>");

        sb.Append("<tr style=\"width:100%;border-bottom:solid 1px;\">");
        sb.Append(String.Format("<td style=\"width: 50%; border-right: solid 1px; border-bottom: 1px solid; border-left: 1px solid; font-weight: bold; \">{0}</td>", ResourceMgr.GetMessage("Due Date")));
        sb.Append(String.Format("<td style=\"width: 50%; border-right: solid 1px; border-bottom: 1px solid;\">{0}</td>", lblDate.Text));
        sb.Append("</tr>");

        sb.Append("</table>");


        HtmlToPdf converter = new HtmlToPdf();

        converter.Options.PdfPageSize = PdfPageSize.A4;
        converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;

        //PdfDocument doc = converter.ConvertHtmlString(sb.ToString());
        //doc.Save(Response, true, string.Format("{0}.pdf", invoiceNumber));
        //doc.Close();
    }

    #endregion



    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Response.Redirect(String.Format("~/InvoiceReport.aspx?OrgId={0}&InvoiceNum={1}", lblOrganizationId.Text, lblInvoiceId.Text));
    }

    private void PrintInvoice(string invoiceNumber)
    {
        int InvoiceId = Conversion.ParseInt(invoiceNumber);
        Invoice objInvoice = new Invoice(InvoiceId);
        string InFavorOf = OrganizationInfo.GetOrgLegalNameByOrgId(objInvoice.Organizationid);

        StringBuilder sb = new StringBuilder();

        sb.Append("<table style=\"background-color: #f6f6f6; width: 100%; font-family: Arial, sans-serif; font-size:13px; line-height:18px; color:#222;\"");

        sb.Append("<tr>");
        sb.Append("<td vertical-align: top;></td>");
        sb.Append("<td vertical-align: top;width=\"600\" style=\"display: block; max-width: 600px; margin: 0 auto; clear: both;\">");

        sb.Append("<div style=\"max-width: 600px; margin: 0 auto; display: block; padding: 20px;\">");

        sb.Append("<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" style=\"  background: #fff; border: 1px solid #e9e9e9; border-radius: 3px;\">");
        sb.Append("<tr>");

        sb.Append("<td vertical-align: top; style=\"text-align:center; padding:20px;\">");

        sb.Append("<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\">");

        sb.Append("<tr>");
        sb.Append("<td vertical-align: top; style=\"padding:0 0 20px;\">");
        sb.Append(String.Format("<h2 style=\"font-size: 24px; font-weight:normal;\">Invoice No. {0}</h2>", invoiceNumber));
        sb.Append("</td>");
        sb.Append("</tr>");

        sb.Append("<tr>");
        sb.Append("<td vertical-align: top; style=\"padding:0 0 20px;\">");
        sb.Append("<table style=\"margin: 40px auto; text-align: left; width: 80%;\">");
        sb.Append("<tr>");
        sb.Append(String.Format("<td vertical-align: top;>Invoice #101055<br>Invoice Date: {0} <br /> </td>", objInvoice.InvoiceDate.ToShortDateString()));
        sb.Append("</tr>");

        sb.Append("<tr>");
        sb.Append("<td vertical-align: top;>");

        sb.Append("<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\">");

        sb.Append("<tr>");
        sb.Append("<td vertical-align: top; style=\"border-top: #eee 1px solid; padding: 5px 0;\">To</td>");
        sb.Append(String.Format("<td vertical-align: top; style=\"border-top: #eee 1px solid; padding: 5px 0; text-align:right;\">{0}</td>", objInvoice.OrganizationForName));
        sb.Append("</tr>");

        sb.Append("<tr>");
        sb.Append("<td vertical-align: top; style=\"border-top: #eee 1px solid; padding: 5px 0;\">From</td>");
        sb.Append(String.Format("<td vertical-align: top; style=\"border-top: #eee 1px solid; padding: 5px 0; text-align:right;\">{0}</td>", InFavorOf));
        sb.Append("</tr>");

        sb.Append("<tr>");
        sb.Append("<td vertical-align: top; style=\"border-top: #eee 1px solid; padding: 5px 0;\">Invoice Amount</td>");
        sb.Append(String.Format("<td vertical-align: top; style=\"border-top: #eee 1px solid; padding: 5px 0; text-align:right;\">{0}</td>", objInvoice.InvoiceAmount.ToString("C")));
        sb.Append("</tr>");

        sb.Append("<tr>");
        sb.Append("<td vertical-align: top; style=\"border-top: #eee 1px solid; padding: 5px 0;\">Due Date</td>");
        sb.Append(String.Format("<td vertical-align: top; style=\"border-top: #eee 1px solid; padding: 5px 0; text-align:right;\">{0}</td>", objInvoice.DueDate.ToShortDateString()));
        sb.Append("</tr>");

        //sb.Append("<tr>");
        //sb.Append("<td vertical-align: top; width=\"70%\" style=\"border-top: 2px solid #333; border-bottom: 2px solid #333; font-weight: 700; text-align:right;\">Total</td>");
        //sb.Append(String.Format(" <td vertical-align: top; style=\"border-top: 2px solid #333; border-bottom: 2px solid #333; font-weight: 700; text-align:right;\">{0}</td>","$6.00"));
        //sb.Append("</tr>");

        sb.Append("</table></td</tr></table></td></tr>");

        sb.Append("<tr>");
        sb.Append("<td vertical-align: top; style=\"padding: 0 0 20px;\">");
        sb.Append("EPR Technology Solutions © 2014-2015");

        sb.Append("</td></tr></table></td></tr></table><div></div></div></td><td vertical-align: top;></td></tr></table>");


        HtmlToPdf converter = new HtmlToPdf();

        converter.Options.PdfPageSize = PdfPageSize.A10;
        converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;

        //PdfDocument doc = converter.ConvertHtmlString(sb.ToString());
        //doc.Save(Response, false, string.Format("{0}.pdf", invoiceNumber));
        //doc.Close();
    }

    //private void LoadReport(int InvoiceId,int OrganizationId)
    //{
    //    rptVwrInvoiceReport.ProcessingMode = ProcessingMode.Local;
    //    rptVwrInvoiceReport.LocalReport.ReportPath = Server.MapPath("~/InvoiceReport.rdlc");

    //    ReportDataSource dsInvoice = new ReportDataSource("DataSet2",Invoice.GetInvoiceForReport(InvoiceId).Tables[0]);
    //    ReportDataSource dsDeliveries = new ReportDataSource("DataSet1", Invoice.getInvoiceTiresInfo(OrganizationId, Conversion.ParseString(InvoiceId)).Tables[0]);
    //    rptVwrInvoiceReport.LocalReport.DataSources.Clear();
    //    rptVwrInvoiceReport.LocalReport.DataSources.Add(dsInvoice);
    //    rptVwrInvoiceReport.LocalReport.DataSources.Add(dsDeliveries);

    //}
    protected void addInvoicenote_Click(object sender, EventArgs e)
    {
        AggrInvoices1.Visible = true;
    }
    protected void lnkCancelAggrInvoices1_Click(object sender, EventArgs e)
    {
        AggrInvoices1.Visible = false;
        Response.Redirect("invoices");
    }

    protected void lnkSearchAggrInvoices1_Click(object sender, EventArgs e)
    {
        if (ddlYear.SelectedIndex == 0)
        {
            lblYear.Visible = true;
        }
        else
        {
            //month = Convert.ToInt32(ddlMonth.SelectedValue);
            //year = Convert.ToInt32(ddlYear.SelectedValue);

            Session["month"] = ddlMonth.SelectedValue;
            Session["year"] = ddlYear.SelectedValue;


            lblYear.Visible = false;
            AggrInvoices2.Visible = true;
            AggrInvoices1.Visible = false;
            LoadAggregateInvoices(1);
        }
    }
    protected void lnkBackAggrInvoices2_Click(object sender, EventArgs e)
    {
        AggrInvoices2.Visible = false;
        AggrInvoices1.Visible = true;
    }
    protected void lnkCloseAggrInvoices2_Click(object sender, EventArgs e)
    {
        AggrInvoices2.Visible = false;
        Response.Redirect("invoices");
    }


    protected void LoadAggregateInvoices(int pageNo)
    {
        try
        {
            int total;
            //int month = Convert.ToInt32(ddlMonth.SelectedValue);
            //int year = Convert.ToInt32(ddlYear.SelectedValue);
            DataSet ds = Invoice.GetAggregateInvoiceForPublic(pageNo, pageSize, UserOrganizationId, CatId, Convert.ToInt32(Session["month"]), Convert.ToInt32(Session["year"]), out total);
            gvAggrInvoice.DataSource = ds;
            gvAggrInvoice.DataBind();
            if (ds != null & ds.Tables[0].Rows.Count > 0)
            {
                lnkPrint.Visible = true;
                //lnkEmail.Visible = true;
                opendvSendEmail.Visible = true;
                int amount = Convert.ToInt32(ds.Tables[0].Rows[0]["Total_Amount"]);
                string TotalAmount = String.Format("{0:C}", amount);
                lblTotalAmount.Text = "Total Amount of selected period: " + TotalAmount;
                TotalItemsR = total;
                pgrAggregate.DrawPager(pageNo, this.TotalItemsR, pageSize, MaxPagesToShow);
                if (gvAggrInvoice.Rows.Count != 0)
                {
                    LinkButton lnkBtn = (LinkButton)pgrAggregate.FindControl("Button_" + pageNo.ToString());
                    if (lnkBtn != null)
                    {
                        lnkBtn.Font.Bold = true;
                    }

                }
            }
            else
            {
                lnkPrint.Visible = false;
                //lnkEmail.Visible = false;
                opendvSendEmail.Visible = false;
                pgrAggregate.DrawPager(0, 0, pageSize, MaxPagesToShow);
                lblTotalAmount.Text = string.Empty;
            }
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "Invoice.btnSendEmail_Click", ex);
        }

    }
    protected void lnkPrint_Click(object sender, EventArgs e)
    {
        Response.Redirect(String.Format("~/InvoiceAggregate.aspx?OrgId={0}&CatId={1}&Month={2}&Year={3}", UserOrganizationId, CatId, Session["month"], Session["year"]));
    }
    protected void lnkEmail_Click(object sender, EventArgs e)
    {
        dvSendEmail.Visible = true;
    }
    protected void btnSendEmail_Click(object sender, EventArgs e)
    {
        try
        {

            string emailaddress = txtEmail.Text.Trim();


            //Converting datatable to pdf

            DataTable dt = Invoice.GetAggregateInvoiceForPublicReport(UserOrganizationId, Convert.ToInt32(Session["month"]), Convert.ToInt32(Session["year"])).Tables[0];
            string[] selectedColumns = new[] { "InvoiceNumber", "OrganizationForTo", "InvoiceAmount", "InvoiceDate", "DueDate", "IsPaid" };
            DataTable dtt = new DataView(dt).ToTable(false, selectedColumns);
            int totalAmount = Convert.ToInt32(dt.Rows[0]["Total_Amount"]);
            string Total_Amount = String.Format("{0:C}", totalAmount);
            string AddressFrom = dt.Rows[0]["AddressFrom"].ToString();
            string EmailFrom = dt.Rows[0]["EmailFrom"].ToString();
            string NameFrom = dt.Rows[0]["NameFrom"].ToString();
            string PhoneFrom = dt.Rows[0]["OrganizationFromPhone"].ToString();
            ExportToPdf(dtt, Total_Amount, AddressFrom, EmailFrom, NameFrom, PhoneFrom);

            //DownloadPDF();
            //Converting datatable to pdf

            //Sending to given email address
            MailMessage mailMsg = new MailMessage();
            SmtpClient smtpClient = new SmtpClient(ConfigurationManager.AppSettings["smtpServer"]);
            smtpClient.Port = 587;
            smtpClient.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["smtpUser"].ToString(), ConfigurationManager.AppSettings["smtpPw"].ToString());
            mailMsg.Subject = "PDF of invoices";
            mailMsg.Body = "Your pdf attached";
            mailMsg.To.Add(emailaddress);
            mailMsg.From = new MailAddress("bilal@intelligentsiasoftware.com", "EPRTS");
            mailMsg.Attachments.Add(new Attachment(Server.MapPath("~/SendMail/InvoicePDF.pdf")));
            smtpClient.Send(mailMsg);
            txtEmail.Text = string.Empty;
            dvSendEmail.Style.Add("display", "none");

            lblEmailMsg.Visible = true;
            lblEmailMsg.Text = "Email sent successfully. You will recieve an email on given Email-address in a while.";
            lblEmailMsg.CssClass = "custom-absolute-alert alert-success";
            ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
        }
        catch (Exception ex)
        {
            lblEmailMsg.Visible = true;
            lblEmailMsg.Text = "Email not sent. Please try again after 5-10 minutes.";
            lblEmailMsg.CssClass = "custom-absolute-alert alert-danger";
            ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
            dvSendEmail.Style.Add("display", "block");

            new SqlLog().InsertSqlLog(0, "Invoice.btnSendEmail_Click", ex);
        }

    }
    protected void btnCancelSend_Click(object sender, EventArgs e)
    {
        dvSendEmail.Visible = false;
    }




    protected void ExportToPdf(DataTable dt, string TotalAmount, string AddressFrom, string EmailFrom, string NameFrom, string PhoneFrom)
    {
        Document document = new Document();


        if (File.Exists(Server.MapPath("~/SendMail/InvoicePDF.pdf")))
        {
            //For Closing pdf if it is open
            Process[] processes = Process.GetProcessesByName("AcroRd32");
            for (int i = 0; i < processes.Length; i++)
            {
                if (processes[i].MainWindowTitle.Contains("InvoicePDF"))
                {
                    processes[i].CloseMainWindow();
                }
            }
            //For Closing pdf if it is open

            //Deleting after closing the open file
            File.Delete(Server.MapPath("~/SendMail/InvoicePDF.pdf"));
        }
        //Creating file with same name in same folder
        PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(Server.MapPath("~/SendMail/InvoicePDF.pdf"), FileMode.Create));
        document.Open();

        //document.Add(new Phrase("Aggregative Invoices" + "\n\n", iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, Font.BOLD)));
        Paragraph Par = new Paragraph("Aggregative Invoices");
        Par.Font = iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES_ROMAN, 1);
        Par.Alignment = Element.ALIGN_CENTER;
        document.Add(Par);
        document.Add(new Phrase("\n\n", iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES_ROMAN, 6)));
        iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 5);

        PdfPTable table = new PdfPTable(5);
        float[] widths = new float[] { 4f, 4f, 4f, 4f, 4f };

        table.SetWidths(widths);

        table.WidthPercentage = 100;
        PdfPCell cell = new PdfPCell(new Phrase("Products"));

        cell.Colspan = dt.Columns.Count;

        foreach (DataColumn c in dt.Columns)
        {
            table.AddCell(new Phrase(c.ColumnName, iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES_ROMAN, 6, Font.BOLD)));
        }

        foreach (DataRow r in dt.Rows)
        {
            if (dt.Rows.Count > 0)
            {
                table.AddCell(new Phrase(r[0].ToString(), font5));
                table.AddCell(new Phrase(r[1].ToString(), font5));
                table.AddCell(new Phrase(r[2].ToString(), font5));
                table.AddCell(new Phrase(r[3].ToString(), font5));
                table.AddCell(new Phrase(r[4].ToString(), font5));
            }
        }
        document.Add(table);

        //document.Add(new Phrase("\n\nTotal Amount : ", iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES_ROMAN, 6, Font.BOLD)));
        document.Add(new Phrase("\n", iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES_ROMAN, 6)));
        Paragraph Para1 = new Paragraph("Total Amount : " + TotalAmount);
        Para1.Font = iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES_ROMAN, 2);
        Para1.Alignment = Element.ALIGN_RIGHT;
        document.Add(Para1);

        //Paragraph Para2 = new Paragraph("Total Amount 678678: ");
        //Para2.Font = iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES_ROMAN, 6, Font.BOLD);
        //Para2.Alignment = Element.ALIGN_RIGHT;
        //document.Add(Para2);

        //document.Add(new Phrase(TotalAmount + "\n", iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES_ROMAN, 6)));
        document.Add(new Phrase(NameFrom + "\n", iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES_ROMAN, 6)));
        document.Add(new Phrase(EmailFrom + "\n", iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES_ROMAN, 6)));
        document.Add(new Phrase(PhoneFrom + "\n", iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES_ROMAN, 6)));
        document.Add(new Phrase(AddressFrom + "\n", iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES_ROMAN, 6)));
        document.Close();

    }

    private void DownloadPDF()
    {
        DataSet dsResult = Invoice.GetAggregateInvoiceForPublicReport(UserOrganizationId, Convert.ToInt32(Session["month"]), Convert.ToInt32(Session["year"]));

        StringWriter stringWrite = new StringWriter();
        HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        GridView grdGridView = new GridView();
        if (dsResult != null && dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
        {
            Response.Clear();
            Response.AddHeader("content-disposition",
            string.Format("attachment;filename={0}.pdf", "InvoicePDF"));
            //Response.Charset = "";
            Response.ContentType = "application/pdf";

            grdGridView = new GridView();
            grdGridView.AllowPaging = false;
            grdGridView.DataSource = dsResult.Tables[0];
            grdGridView.DataBind();

            htmlWrite = new HtmlTextWriter(stringWrite);
            grdGridView.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());
            Response.End();
        }
    }
    protected void btnSendEmail2_Click(object sender, EventArgs e)
    {
        try
        {
            string emailaddress = UserInfo.GetAdminEmail(UserOrganizationId).Tables[0].Rows[0][0].ToString() == null ? string.Empty : UserInfo.GetAdminEmail(UserOrganizationId).Tables[0].Rows[0][0].ToString();
            DataTable dt = Invoice.GetAggregateInvoiceForPublicReport(UserOrganizationId, Convert.ToInt32(Session["month"]), Convert.ToInt32(Session["year"])).Tables[0];
            if (dt != null && dt.Rows.Count > 0 && emailaddress != string.Empty)
            {
                string[] selectedColumns = new[] { "InvoiceNumber", "InvoiceAmount", "InvoiceDate", "DueDate", "IsPaid" };
                DataTable dtt = new DataView(dt).ToTable(false, selectedColumns);
                int totalAmount = Convert.ToInt32(dt.Rows[0]["Total_Amount"]);
                string Total_Amount = String.Format("{0:C}", totalAmount);
                string AddressFrom = dt.Rows[0]["AddressFrom"].ToString();
                string EmailFrom = dt.Rows[0]["EmailFrom"].ToString();
                string NameFrom = dt.Rows[0]["NameFrom"].ToString();
                string PhoneFrom = dt.Rows[0]["OrganizationFromPhone"].ToString();
                ExportToPdf(dtt, Total_Amount, AddressFrom, EmailFrom, NameFrom, PhoneFrom);

                ////DownloadPDF();
                ////Converting datatable to pdf

                //Sending to given email address
                MailMessage mailMsg = new MailMessage();
                SmtpClient smtpClient = new SmtpClient(ConfigurationManager.AppSettings["smtpServer"]);
                smtpClient.Port = 587;
                smtpClient.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["smtpUser"].ToString(), ConfigurationManager.AppSettings["smtpPw"].ToString());
                mailMsg.Subject = "PDF of invoices";
                mailMsg.Body = "Your pdf attached";
                mailMsg.To.Add(emailaddress);
                mailMsg.From = new MailAddress("bilal@intelligentsiasoftware.com", "EPRTS");
                mailMsg.Attachments.Add(new Attachment(Server.MapPath("~/SendMail/InvoicePDF.pdf")));
                //smtpClient.Send(mailMsg);
                txtEmail.Text = string.Empty;
                dvSendEmail.Style.Add("display", "none");

                lblEmailMsg.Visible = true;
                lblEmailMsg.Text = "Email sent successfully. You will recieve an email on given Email-address in a while.";
                lblEmailMsg.CssClass = "custom-absolute-alert alert-success";
                ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
            }
            else
            {
                lblEmailMsg.Visible = true;
                lblEmailMsg.Text = "Error occured.";
                lblEmailMsg.CssClass = "custom-absolute-alert alert-danger";
                ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
                dvSendEmail.Style.Add("display", "none");
                AggrInvoices1.Visible = false;
                AggrInvoices2.Visible = false;
                InvoiceInfo(1);
            }
        }
        catch (Exception ex)
        {
            lblEmailMsg.Visible = true;
            lblEmailMsg.Text = "Email not sent. Please try again after 5-10 minutes.";
            lblEmailMsg.CssClass = "custom-absolute-alert alert-danger";
            ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOut();", true);
            dvSendEmail.Style.Add("display", "block");

            new SqlLog().InsertSqlLog(0, "Invoice.btnSendEmail2_Click", ex);
        }
    }
}