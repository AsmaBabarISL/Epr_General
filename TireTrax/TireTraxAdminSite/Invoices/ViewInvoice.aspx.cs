using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using TireTraxLib;
using System.Data;
//using ICSharpCode;
using System.IO;
using System.Net.Mail;
using System.Configuration;
using System.Net;
//using SelectPdf;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Diagnostics;

public partial class Invoices_ViewInvoice : BasePage
{
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

        ClientScript.RegisterStartupScript(GetType(), "SetHeaderMenu", "SetHeaderMenu('liAccountManagement');", true);
        if (!IsPostBack)
        {
            lblEmailMsg.Visible = false;
            lblEmailMsg.Text = string.Empty;
            if (Request.QueryString["p"] == null)
                Session["InvoiceStatus"] = string.Empty;
            else
                if (Session["InvoiceStatus"] != string.Empty && Session["InvoiceStatus"] != null)
                    ddlStatus.Items.FindByValue(Session["InvoiceStatus"].ToString()).Selected = true;


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
            InvoiceInfo(CurPageNum);
            GridPaging();
            //int date = DateTime.Now.Day;
            //if (date == 1)
            //{
            //    btnSendEmail2_Click(null, null);
            //}
        }
        else
        {
            lblEmailMsg.Visible = false;
            lblEmailMsg.Text = string.Empty;
        }
        if (TotalItemsR > 0)
        {
            pgrAggregate.DrawPager(CurrentPageR, this.TotalItemsR, pageSize, MaxPagesToShow);
        }
        else
        {
            pgrAggregate.DrawPager(CurrentPageR, this.TotalItemsR, pageSize, MaxPagesToShow);
        }
        lblSuccess.Visible = false;
    }
    #region Button Events

    protected void btnaddInvoicenote_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Invoices/addinvoice.aspx");
    }
    protected void btnInvoiceSearch_Click(object sender, EventArgs e)
    {
        InvoiceInfo(1);
        GridPaging();
    }
    protected void btnDeliveryDetailBack_Click(object sender, EventArgs e)
    {
        //dvMainLoad.Visible = false;
    }
    protected void btnInvoicePaymentCancel_Click(object sender, EventArgs e)
    {
        dvInvoicePaymentPopup.Visible = false;
    }
    protected void btnPaymentDetailCancel_Click(object sender, EventArgs e)
    {
        dvPaymentDetail.Visible = false;
    }
    protected void btnInvoicePaymentCreditCard_Click(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(lblCardExpiryDate.Text))
            {
                if (!(Conversion.ParseInt(lblCardExpiryDate.Text.Split('/')[1]) < System.DateTime.Now.Year))
                {
                    if (!(Conversion.ParseInt(lblCardExpiryDate.Text.Split('/')[0]) < System.DateTime.Now.Month))
                    {
                        dvInvoicePaymentPopup.Visible = false;
                        lblSuccess.Text = "Card should have valid expiry. Please add credit card with valid expiry";
                        lblSuccess.Visible = true;
                        lblSuccess.CssClass = "alert-danger custom-absolute-alert";
                        ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOutInline();", true);
                    }
                }
            }
            if (IsGroupValid("CreditCard"))
            {
                InsertCreditCardPayments();
            }
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "ViewInvoice.btnInvoicePaymentCreditCard_Click", ex);
        }

    }
    protected void btnInvoicePaymentCheck_Click(object sender, EventArgs e)
    {
        if (IsGroupValid("Check"))
        {
            InsertCheckInfo();
        }

    }
    protected void btnPrintInvoice_Click(object sender, EventArgs e)
    {
        Response.Redirect(String.Format("~/InvoiceReport.aspx?OrgId={0}&InvoiceNum={1}", lblOrganizationId.Text, lblInvoiceID.Text));
    }

    protected void ddlPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            switch (ddlPaymentMode.SelectedItem.Text.Trim())
            {
                case "Check":
                    dvPaymentModeCheck.Visible = true;
                    dvCreditCard.Visible = false;

                    LoadBankAccountInfo(UserInfo.GetCurrentUserInfo().UserId);

                    btnInvoicePaymentCheck.Visible = true;
                    btnInvoicePaymentCreditCard.Visible = false;
                    break;
                case "CreditCard":
                    dvCreditCard.Visible = true;
                    dvPaymentModeCheck.Visible = false;
                    int userid = UserInfo.GetCurrentUserInfo().UserId;
                    LoadCreditCardInfo(UserInfo.GetCurrentUserInfo().UserId);

                    btnInvoicePaymentCheck.Visible = false;
                    btnInvoicePaymentCreditCard.Visible = true;
                    break;
                default:
                    dvPaymentModeCheck.Visible = false;
                    dvCreditCard.Visible = false;
                    btnInvoicePaymentCheck.Visible = false;
                    btnInvoicePaymentCreditCard.Visible = false;
                    break;
            }

        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "ViewInvoice.ddlPaymentMode_SelectedIndexChanged", ex);
        }
    }

    #endregion
    #region Load Function

    /// <summary>
    /// use for paging
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    /// <returns></returns>

    /// <summary>
    /// use to load the Main grid from database 
    /// </summary>
    /// <param name="pageNo"></param>
    protected void InvoiceInfo(int pageNo)
    {
        try
        {
            pageSize = Conversion.ParseInt(ddlPageSize.SelectedValue);
            gvInvoicesinfo.PageSize = pageSize;
            CurrentPageR = pageNo;
            DateTime frmDate = string.IsNullOrEmpty(txtFrmDate.Text) ? DateTime.MinValue : Convert.ToDateTime(txtFrmDate.Text);
            DateTime toDate = string.IsNullOrEmpty(txtToDate.Text) ? DateTime.MinValue : Convert.ToDateTime(txtToDate.Text);
            //int status = Convert.ToInt32(ddlStatus.SelectedValue);
            int status = Session["InvoiceStatus"] == string.Empty ? 2 : Convert.ToInt32(Session["InvoiceStatus"]);
            gvInvoicesinfo.DataSource = Invoice.getAllInvoicesAdmin(UserOrganizationId, CurrentPageR, pageSize, out totalRows, frmDate, toDate, txtOrganizationName.Text, txtInvoiceNo.Text, CatId, status);
            gvInvoicesinfo.DataBind();

        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "ViewInvoiceAdmin.InvoiceInfo", ex);
        }

    }
    /// <summary>
    /// use to show the Delivery detail in popup
    /// </summary>
    /// <param name="DeliveryID"></param>
    public void LoadPopInfobyInvoiceId(int InvoiceID)
    {


        Invoice objInvoice = new Invoice(InvoiceID);
        lblTo.Text = Conversion.ParseString(objInvoice.OrganizationForName);
        lblInvoiceID.Text = Conversion.ParseString(objInvoice.InvoiceID);
        lblinvoiceDate.Text = Conversion.ParseString(objInvoice.InvoiceDate.ToShortDateString());
        lblInvoiceAmount.Text = objInvoice.InvoiceAmount.ToString("C");
        lblDate.Text = Conversion.ParseString(objInvoice.DueDate.ToShortDateString());
        lblFrom.Text = OrganizationInfo.GetOrgLegalNameByOrgId(objInvoice.Organizationid);
        lblOrganizationId.Text = objInvoice.Organizationid.ToString();

        gvInvoicesDetails.DataSource = Invoice.getInvoiceTiresInfo(objInvoice.Organizationid, Conversion.ParseString(InvoiceID));
        gvInvoicesDetails.DataBind();



        //try
        //{

        //    DataSet ds = Invoice.getDeliveryInvoices(UserOrganizationId, _deliveryIds);

        //    gvInvoicesinfo.DataSource = ds;
        //    gvInvoicesinfo.DataBind();

        //    string[] loads = objDelivery.LoadIds.Split(',');
        //    lblTotalLoads.Text = Conversion.ParseString(loads.Length);
        //    int count = 0;
        //    gvAllTire.DataSource = Loads.getTiresInfoByLoadIds(objDelivery.LoadIds, 1, 0, out count);
        //    gvAllTire.DataBind();
        //    lblLoadTireCount.Text = Conversion.ParseString(count);
        //}
        //catch (Exception ex)
        //{
        //}

    }

    public void LoadPaymentDetailPopInfobyInvoiceId(int InvoiceID)
    {

        try
        {
            Payment payment = Payment.GetPaymentDetailByInvoiceId(InvoiceID);
            if (payment.PaymentId != 0)
            {
                lblPaymentId.Text = payment.PaymentId.ToString();
                //lblPaymentTypeId.Text = payment.PaymentTypeId;
                if (!string.IsNullOrEmpty(payment.CheckNumber))
                {
                    lblCheckNumber.Text = payment.CheckNumber;
                    lblForCheckorCreditCard.Text = String.Format("{0}", ResourceMgr.GetMessage("Check Number"));
                }
                else
                {
                    lblForCheckorCreditCard.Text = String.Format("{0}", ResourceMgr.GetMessage("Credit Card Number"));
                    lblCheckNumber.Text = payment.CreditCardNumber;
                }

                lblPaymentDate.Text = payment.PaymentDate.ToShortDateString();
                //lblStatus.Text = payment.Status;
                //lblBalanceAmount.Text = payment.BalanceAmount.ToString();
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
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "ViewInvoiceAdmin.LoadPaymentDetailPopInfobyInvoiceId", ex);
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

    private void LoadCreditCardInfo(int UserId)
    {
        try
        {
            DataSet ds = CreditCard.GetPrimaryCreditCardInfo(UserId);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                LoadCreditCardInfo(ds.Tables[0]);
            }
            else
            {
                lblNoPrimaryBankAccountOrCreditCard.Text = "Please Make a primary Credit Card and then proceed with the payments";
                lblNoPrimaryBankAccountOrCreditCard.Visible = true;

                dvInvoicePaymentPopup.Visible = false;
            }


        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "ViewInvoice.LoadCreditCardInfo", ex);
        }
    }

    private void LoadCreditCardInfo(DataTable dataTable)
    {
        try
        {
            string creditCardNumber = dataTable.Rows[0]["vchCardNumber"].ToString();
            hdnCreditCardNumber.Value = creditCardNumber;

            creditCardNumber = string.Concat("".PadLeft(creditCardNumber.Length, '*'), creditCardNumber.Substring(creditCardNumber.Length - 4));

            lblCreditCardNumber.Text = creditCardNumber;
            lblCardName.Text = dataTable.Rows[0]["vchCreditCardName"].ToString();
            lblCardExpiryDate.Text = dataTable.Rows[0]["ExpirationDate"].ToString();
            lblCardType.Text = dataTable.Rows[0]["CardType"].ToString();
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "ViewInvoice.LoadCreditCardInfo", ex);
        }
    }

    private void LoadPaymentPopup(int InvoiceId)
    {
        try
        {
            Utils.GetLookUpData<DropDownList>(ref ddlPaymentMode, LookUps.PaymentType);
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

    protected void btnInvoiceDetailBack_Click(object sender, EventArgs e)
    {
        dvInvoicepopup.Visible = false;
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
            GridPaging();
        }
        else if (e.CommandName == "Paid")
        {
            bool result = Invoice.Invoice_IsPaid(Convert.ToInt32(e.CommandArgument));
            InvoiceInfo(1);
            GridPaging();
        }
        else if (e.CommandName == "Pay")
        {
            dvInvoicePaymentPopup.Visible = true;
            LoadPaymentPopup(Convert.ToInt32(e.CommandArgument));
            InvoiceInfo(1);
            GridPaging();
        }
    }

    //protected void gvInvoicesinfo_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        LinkButton imgApproved = (LinkButton)e.Row.FindControl("lnkApproved");
    //        LinkButton imgRejected = (LinkButton)e.Row.FindControl("lnkRejected");
    //        LinkButton imgPending = (LinkButton)e.Row.FindControl("lnkPending");
    //        //LinkButton imgbtnSend = (LinkButton)e.Row.FindControl("lnkbtnSend");
    //        //LinkButton imgbtnPaid = (LinkButton)e.Row.FindControl("lnkbtnPaid");


    //        if (Conversion.ParseDBNullInt(DataBinder.Eval(e.Row.DataItem, "Organizationid")) == UserOrganizationId)
    //        {
    //            if (Conversion.ParseDBNullBool(DataBinder.Eval(e.Row.DataItem, "IsSent")) == false)
    //            {
    //                imgApproved.Visible = false;
    //                imgRejected.Visible = false;
    //                imgPending.Visible = true;
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
    //                    imgPending.Visible = false;
    //                    //imgbtnSend.Visible = false;
    //                    //imgbtnPaid.Visible = false;

    //                }
    //                //else
    //                //{

    //                //    imgbtnPaid.Visible = true;
    //                //}
    //            }

    //        }


    //    }
    //}




    #endregion
    #region GridPaging

    protected void GridPaging()
    {
        try
        {
            int startRecordNumber = (CurPageNum - 1) * pageSize + 1;
            int endRecordNumber = startRecordNumber + gvInvoicesinfo.Rows.Count - 1;

            if (gvInvoicesinfo.Rows.Count == 0)
                startRecordNumber = 0;

            int totalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(totalRows) / Convert.ToDecimal(pageSize)));
            lblPagingLeft.Text = "Showing " + startRecordNumber + " to " + endRecordNumber + " of " + totalRows;
            StringBuilder sb = new StringBuilder();
            sb.Append(@"<div class='Pages'><div class='Paginator'>");
            Pagination pagingstring = new Pagination();
            pagingstring.CurPage = CurPageNum;
            pagingstring.BaseUrl = Request.Url.GetLeftPart(UriPartial.Path).ToString();
            pagingstring.TotalRows = totalRows;
            pagingstring.PerPage = pageSize;
            pagingstring.PrevLink = "&lt; Prev";
            pagingstring.NextLink = "Next &gt;";
            pagingstring.LastLink = "Last &gt;";
            pagingstring.FirstLink = "&lt; First";
            sb.Append(pagingstring.GetPageLinks());
            sb.Append(@"</div></div>");
            ltrlPaging.Text = sb.ToString();
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "adminViewinvoices.GridPaging", ex);
        }
    }

    protected void GridPaging(int CurrentPageNumber)
    {
        try
        {
            int startRecordNumber = (CurrentPageNumber - 1) * pageSize + 1;
            int endRecordNumber = startRecordNumber + gvInvoicesinfo.Rows.Count - 1;

            if (gvInvoicesinfo.Rows.Count == 0)
                startRecordNumber = 0;

            int totalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(totalRows) / Convert.ToDecimal(pageSize)));
            lblPagingLeft.Text = "Showing " + startRecordNumber + " to " + endRecordNumber + " of " + totalRows;
            StringBuilder sb = new StringBuilder();
            sb.Append(@"<div class='Pages'><div class='Paginator'>");
            Pagination pagingstring = new Pagination();
            pagingstring.CurPage = CurrentPageNumber;
            pagingstring.BaseUrl = Request.Url.GetLeftPart(UriPartial.Path).ToString();
            pagingstring.TotalRows = totalRows;
            pagingstring.PerPage = pageSize;
            pagingstring.PrevLink = "&lt; Prev";
            pagingstring.NextLink = "Next &gt;";
            pagingstring.LastLink = "Last &gt;";
            pagingstring.FirstLink = "&lt; First";
            sb.Append(pagingstring.GetPageLinks());
            sb.Append(@"</div></div>");
            ltrlPaging.Text = sb.ToString();
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "adminViewinvoices.GridPaging", ex);
        }
    }

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        pageSize = Conversion.ParseInt(ddlPageSize.SelectedValue);
        InvoiceInfo(1);
        GridPaging(1);
    }

    #endregion
    #region Payments via Check or Credit Card

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
            objPayment.DateModified = DateTime.MinValue;
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
                lblSuccess.Text = "Invoice Paid!";
                lblSuccess.Visible = true;
                SendNotification(invoiceId);
                ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOutInline();", true);
            }
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "ViewInvoice.InsertCheckInfo", ex);
        }





    }

    private void InsertCreditCardPayments()
    {
        try
        {
            Payment objPayment = new Payment();

            int invoiceId = Conversion.ParseInt(hdnInvoiceId.Value.ToString());
            objPayment.Active = true;
            objPayment.PaymentAmount = Conversion.ParseDecimal(txtAmountCreditCard.Text);
            objPayment.CreditCardNumber = hdnCreditCardNumber.Value;
            objPayment.CreatedBy = UserInfo.GetCurrentUserInfo().UserId;
            objPayment.DateCreated = System.DateTime.Now;
            objPayment.DateModified = DateTime.MinValue;
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
                lblSuccess.Text = "Invoice Paid!";
                lblSuccess.Visible = true;
                SendNotification(invoiceId);
                ScriptManager.RegisterStartupScript(this, GetType(), "fadeOut", "fadeOutInline();", true);
            }
        }
        catch (Exception ex)
        {
            new SqlLog().InsertSqlLog(0, "ViewInvoice.InsertCreditCardPayments", ex);
        }

    }

    #endregion
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
    #region Notifications

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

    #endregion
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["InvoiceStatus"] = ddlStatus.SelectedValue;
        Response.Redirect("~/Invoices/ViewInvoice.aspx?p=1");
    }





    protected void addInvoicenote_Click(object sender, EventArgs e)
    {
        AggrInvoices1.Visible = true;
    }
    protected void lnkCancelAggrInvoices1_Click(object sender, EventArgs e)
    {
        AggrInvoices1.Visible = false;
        Response.Redirect("~/Invoices/ViewInvoice.aspx");
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
        Response.Redirect("~/Invoices/ViewInvoice.aspx");
    }


    protected void LoadAggregateInvoices(int pageNo)
    {
        int total;
        //int month = Convert.ToInt32(ddlMonth.SelectedValue);
        //int year = Convert.ToInt32(ddlYear.SelectedValue);
        DataSet ds = Invoice.GetAggregateInvoiceForAdmin(pageNo, pageSize, Convert.ToInt32(Session["month"]), Convert.ToInt32(Session["year"]), out total);
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

    protected override bool OnBubbleEvent(object source, EventArgs args)
    {

        if (this.pgrAggregate.Equals(source))
        {
            CommandEventArgs cmdArgs = (CommandEventArgs)args;
            CurrentPageR = Convert.ToInt32(cmdArgs.CommandArgument);

            this.LoadAggregateInvoices(CurrentPageR);
        }

        return base.OnBubbleEvent(source, args);
    }

    protected void lnkPrint_Click(object sender, EventArgs e)
    {
        Response.Redirect(String.Format("~/InvoiceAggregateAdmin.aspx?Month={0}&Year={1}", Session["month"], Session["year"]));
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

            DataTable dt = Invoice.GetAggregateInvoiceForAdminReport(Convert.ToInt32(Session["month"]), Convert.ToInt32(Session["year"])).Tables[0];
            string[] selectedColumns = new[] { "InvoiceNumber", "OrganizationForTo", "InvoiceAmount", "InvoiceDate", "DueDate", "IsPaid" };
            DataTable dtt = new DataView(dt).ToTable(false, selectedColumns);
            int totalAmount = Convert.ToInt32(dt.Rows[0]["Total_Amount"]);
            string Total_Amount = String.Format("{0:C}", totalAmount);
            ExportToPdf(dtt, Total_Amount);

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
            dvSendEmail.Style.Add("visibility", "none");

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

            new SqlLog().InsertSqlLog(0, "Invoiceadmin.btnSendEmail_Click", ex);
        }

    }
    protected void btnCancelSend_Click(object sender, EventArgs e)
    {
        dvSendEmail.Visible = false;
    }




    protected void ExportToPdf(DataTable dt, string TotalAmount)
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

            //Delete after closing the open file
            File.Delete(Server.MapPath("~/SendMail/InvoicePDF.pdf"));
        }
        //Creating file with same name in same folder
        PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(Server.MapPath("~/SendMail/InvoicePDF.pdf"), FileMode.Create));
        document.Open();

        document.Add(new Phrase("Aggregative Invoices", iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES_ROMAN, 6, Font.BOLD)));

        iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 5);

        PdfPTable table = new PdfPTable(6);
        float[] widths = new float[] { 4f, 4f, 4f, 4f, 4f, 4f };

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
                table.AddCell(new Phrase(r[5].ToString(), font5));
            }
        }
        //table.AddCell(new Phrase(r[5].ToString(), font5));
        document.Add(table);
        document.Add(new Phrase("Total Amount" + TotalAmount, iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES_ROMAN, 6)));
        document.Close();

    }

    private void DownloadPDF()
    {
        DataSet dsResult = Invoice.GetAggregateInvoiceForAdminReport(Convert.ToInt32(Session["month"]), Convert.ToInt32(Session["year"]));

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
            string emailaddress = UserInfo.GetAdminEmailAdmin(LoginMemberId).Tables[0].Rows[0][0].ToString();
            if (emailaddress.Contains("wajid"))
                emailaddress = "momer@intelligentsiasoftware.com";
            DataTable dt = Invoice.GetAggregateInvoiceForAdminReport(Convert.ToInt32(Session["month"]), Convert.ToInt32(Session["year"])).Tables[0];
            string[] selectedColumns = new[] { "InvoiceNumber", "OrganizationForTo", "InvoiceAmount", "InvoiceDate", "DueDate", "IsPaid" };
            DataTable dtt = new DataView(dt).ToTable(false, selectedColumns);
            int totalAmount = Convert.ToInt32(dt.Rows[0]["Total_Amount"]);
            string Total_Amount = String.Format("{0:C}", totalAmount);
            ExportToPdf(dtt, Total_Amount);

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

            new SqlLog().InsertSqlLog(0, "Invoiceadmin.btnSendEmail2_Click", ex);
        }
    }
}