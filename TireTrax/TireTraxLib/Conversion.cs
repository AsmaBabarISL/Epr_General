using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Web.UI;
using System.Data;
using System.Configuration;
using System.Xml;
using System.IO;
namespace TireTraxLib
{
    public class Conversion
    {


        public static List<string> SplitEmailAddressBySemicolon(string sAddress)
        {
            List<string> lstAddress = new List<string>();

            try
            {
                if (string.IsNullOrEmpty(sAddress) == false)
                {
                    foreach (string add in sAddress.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        lstAddress.Add(add);
                    }
                }
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "Conversion.SplitEmailAddressBySemicolon", e);
            }

            return lstAddress;
        }

        public static void SplitEmailAddressBySemicolon(string sAddress, MailMessage mailMsg, string sType)
        {
            try
            {
                if (string.IsNullOrEmpty(sAddress) == false)
                {
                    switch (sType.ToLower())
                    {
                        case "to":
                            foreach (string add in sAddress.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
                            {
                                mailMsg.To.Add(new MailAddress(add));
                            }
                            break;
                        case "cc":
                            foreach (string add in sAddress.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
                            {
                                mailMsg.CC.Add(new MailAddress(add));
                            }
                            break;
                        case "bcc":
                            foreach (string add in sAddress.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
                            {
                                mailMsg.Bcc.Add(new MailAddress(add));
                            }
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "Conversion.SplitEmailAddressBySemicolon", e);
            }
        }

        public static string RemoveWhiteSpace(string sData)
        {
            if (sData.IndexOf(" ") > 0)
            {
                string[] sArr = sData.Split(new char[] { ' ' });
                string sText = string.Empty;
                foreach (string s in sArr)
                {
                    sText = sText + s;
                }

                return sText;
            }
            else
                return sData;
        }

        public static int GetModuleId(string sQueryStringVal)
        {
            try
            {
                if (string.IsNullOrEmpty(sQueryStringVal))
                    return 0;

                return int.Parse(sQueryStringVal);
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "Conversion.GetModuleId", e);
            }

            return 0;
        }

        public static int GetQueryString(string strValue)
        {
            try
            {
                int iValue = 0;
                if (HttpContext.Current.Request[strValue] != null)
                    iValue = Conversion.ParseInt(HttpContext.Current.Request[strValue].ToString());

                return iValue;
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "Conversion.GetQueryString", e);
            }

            return 0;
        }

        public static string GetQueryStringValue(string strValue)
        {
            try
            {
                if (HttpContext.Current.Request[strValue] != null)
                    return HttpContext.Current.Request[strValue];
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "Conversion.GetQueryStringValue", e);
            }

            return string.Empty;
        }

        public static string GetQueryStringVal(object obj)
        {
            try
            {
                if (obj != null)
                    return obj.ToString();
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "Conversion.GetQueryStringVal", e);
            }

            return string.Empty;
        }

        public static int ParseInt(object obj)
        {
            obj = CleanNumber(obj);
            try
            {
                int iValue = 0;
                if (obj != null)
                    int.TryParse(obj.ToString(), out iValue);

                return iValue;
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "Conversion.ParseInt", e);
            }

            return 0;
        }

        public static int ParseInt(string sVal)
        {
            sVal = CleanNumber(sVal);
            try
            {
                int iValue = 0;
                if (string.IsNullOrEmpty(sVal) == false)
                    int.TryParse(sVal, out iValue);

                return iValue;
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "Conversion.ParseInt", e);
            }

            return 0;
        }

        private static dynamic CleanNumber(object val)
        {
            if (val != null)
            {
                return val.ToString().Replace(",", "");
            }

            return val;


        }

        public static bool ParseBool(object obj)
        {
            try
            {
                bool bValue = false;
                if (obj != null)
                    bool.TryParse(obj.ToString(), out bValue);

                return bValue;
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "Conversion.ParseBool", e);
            }


            return false;
        }

        public static string ParseDateTimeAsString(object obj)
        {
            try
            {
                if (obj != null)
                    return obj.ToString();
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "Conversion.ParseDateTimeAsString", e);
            }

            return "";
        }

        public static DateTime ParseDateTime(string sDateTime)
        {
            try
            {
                DateTime dt = DateTime.Now;
                if (string.IsNullOrEmpty(sDateTime) == false)
                    DateTime.TryParse(sDateTime, out dt);

                return dt;
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "Conversion.ParseDateTime", e);
            }

            return DateTime.Now;
        }




        public static string ParseString(object obj)
        {
            try
            {
                if (obj != null)
                {
                    return obj.ToString();
                }
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "Conversion.ParseString", e);
            }

            return string.Empty;
        }

        public static decimal ParseDecimal(object obj)
        {
            obj = CleanNumber(obj);
            try
            {
                decimal iValue = 0;
                if (obj != null)
                    decimal.TryParse(obj.ToString(), out iValue);

                return iValue;
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "Conversion.ParseDecimal", e);
            }

            return 0;
        }
        public static decimal ParseDecimal(string sVal)
        {
            sVal = CleanNumber(sVal);
            try
            {
                decimal iValue = 0;
                if (string.IsNullOrEmpty(sVal) == false)
                    decimal.TryParse(sVal, out iValue);

                return iValue;
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "Conversion.ParseDecimal", e);
            }

            return 0;
        }
        public static double ParseDouble(object obj)
        {
            obj = CleanNumber(obj);
            try
            {
                double iValue = 0.0;
                if (obj != null)
                    double.TryParse(obj.ToString(), out iValue);

                return iValue;
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "Conversion.ParseDouble", e);
            }

            return 0;
        }



        public static float ParseFloat(object obj)
        {
            obj = CleanNumber(obj);
            try
            {
                float fValue = 0;
                if (obj != null)
                    float.TryParse(obj.ToString(), out fValue);

                return fValue;
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "Conversion.ParseFloat", e);
            }

            return 0;
        }

        public static float? ParseFloatNullable(object obj)
        {
            obj = CleanNumber(obj);
            try
            {
                float fValue = 0;
                if (!string.IsNullOrEmpty(obj.ToString()))
                {
                    float.TryParse(obj.ToString(), out fValue);
                    return fValue;
                }
                else
                    return null;
                //if (!string.IsNullOrEmpty(obj.ToString()))
                //    return fValue;
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "Conversion.ParseFloatNullable", e);
            }

            return null;
        }
        public static int? ParseIntNullable(Object obj)
        {
            // obj = CleanNumber(obj);
            try
            {
                int fValue = 0;
                // if (obj != null)
                int.TryParse(obj.ToString(), out fValue);
                if (!string.IsNullOrEmpty(obj.ToString()))
                    return fValue;
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "Conversion.ParseIntNullable", e);
            }

            return null;
        }
        public static decimal? ParseDecimalNullable(Object obj)
        {
            // obj = CleanNumber(obj);
            try
            {
                decimal fValue = 0;
                // if (obj != null)
                decimal.TryParse(obj.ToString(), out fValue);
                if (!string.IsNullOrEmpty(obj.ToString()))
                    return fValue;
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "Conversion.ParseDecimalNullable", e);
            }

            return null;
        }
        public static double? ParseDoubleNullable(Object obj)
        {
            // obj = CleanNumber(obj);
            try
            {
                double fValue = 0;
                // if (obj != null)
                double.TryParse(obj.ToString(), out fValue);
                if (!string.IsNullOrEmpty(obj.ToString()))
                    return fValue;
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "Conversion.ParseDoubleNullable", e);
            }

            return null;
        }
        public static DateTime? ParseDateTimeNullable(object obj)
        {

            try
            {
                DateTime dt = DateTime.Now;
                if (obj != null || !string.IsNullOrEmpty(obj.ToString()))
                {
                    DateTime.TryParse(obj.ToString(), out dt);

                    return dt;
                }
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "Conversion.ParseDateTimeNullable", e);
            }

            return null;
        }

        public static bool ParseDBNullBool(object obj)
        {
            try
            {
                bool bValue = false;
                if (obj != DBNull.Value)
                    bool.TryParse(obj.ToString(), out bValue);

                return bValue;
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "Conversion.ParseDBNullBool", e);
            }

            return false;
        }
        public static bool? ParseDBNullBoolNullable(object obj)
        {
            try
            {
                bool bValue = false;
                if (obj != DBNull.Value)
                {
                    bool.TryParse(obj.ToString(), out bValue);
                    return bValue;
                }

                else
                    return null;

            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "Conversion.ParseDBNullBoolNullable", e);
            }

            return null;
        }
        public static DateTime ParseDBNullDateTime(object obj)
        {
            try
            {
                if (obj != DBNull.Value)
                    return DateTime.Parse(obj.ToString());
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "Conversion.ParseDBNullDateTime", e);
            }

            return DateTime.MinValue;
        }
        public static DateTime? ParseDBNullDateTimeNullable(object obj)
        {

            try
            {
                DateTime dt = DateTime.Now;
                if (obj != DBNull.Value)
                {
                    DateTime.TryParse(obj.ToString(), out dt);
                    if (!string.IsNullOrEmpty(obj.ToString()))
                        return dt;
                }
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "Conversion.ParseDBNullDateTimeNullable", e);
            }

            return null;
        }
        public static string ParseDBNullString(object obj)
        {
            try
            {
                if (obj != DBNull.Value)
                    return obj.ToString();
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "Conversion.ParseDBNullString", e);
            }


            return string.Empty;
        }

        public static int ParseDBNullInt(object obj)
        {
            obj = CleanNumber(obj);
            try
            {
                int iValue = 0;
                if (obj != DBNull.Value)
                    int.TryParse(obj.ToString(), out iValue);

                return iValue;
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "Conversion.ParseDBNullInt", e);
            }

            return 0;
        }
        public static int? ParseDBNullIntNullable(object obj)
        {
            obj = CleanNumber(obj);
            try
            {
                int iValue = 0;
                //  if (obj != DBNull.Value)
                int.TryParse(obj.ToString(), out iValue);
                if (!string.IsNullOrEmpty(obj.ToString()))
                    return iValue;
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "Conversion.ParseDBNullIntNullable", e);
            }

            return null;
        }

        public static double ParseDBNullDouble(object obj)
        {
            obj = CleanNumber(obj);
            try
            {
                double iValue = 0;
                if (obj != DBNull.Value)
                    double.TryParse(obj.ToString(), out iValue);

                return iValue;
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "Conversion.ParseDBNullDouble", e);
            }

            return 0;
        }
        public static double? ParseDBNullDoubleNullable(object obj)
        {
            obj = CleanNumber(obj);
            try
            {
                double iValue = 0;
                // if (obj != DBNull.Value)
                double.TryParse(obj.ToString(), out iValue);
                if (!string.IsNullOrEmpty(obj.ToString()))
                    return iValue;
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "Conversion.ParseDBNullDoubleNullable", e);
            }

            return null;
        }

        public static decimal ParseDBNullDecimal(object obj)
        {
            obj = CleanNumber(obj);
            try
            {
                decimal iValue = 0;
                if (obj != DBNull.Value)
                    decimal.TryParse(obj.ToString(), out iValue);

                return iValue;
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "Conversion.ParseDBNullDecimal", e);
            }

            return 0;
        }
        public static decimal? ParseDBNullDecimalNullable(object obj)
        {
            obj = CleanNumber(obj);
            try
            {
                decimal iValue = 0;
                // if (obj != DBNull.Value)
                decimal.TryParse(obj.ToString(), out iValue);
                if (!string.IsNullOrEmpty(obj.ToString()))
                    return iValue;
                //return iValue;
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "Conversion.ParseDBNullDecimalNullable", e);
            }

            return null;
        }

        public static bool ParseBoolGridViewData(Type t, object obj)
        {
            try
            {
                if (obj != null)
                {
                    if (t is CheckBox)
                    {
                        CheckBox cb = (CheckBox)obj;
                        return cb.Checked;
                    }
                }
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "Conversion.ParseBoolGridViewData", e);
            }

            return false;
        }



        public static string GetSelectedValue(CheckBoxList cblRoles)
        {
            string sRoles = string.Empty;

            try
            {
                if (cblRoles != null)
                {
                    foreach (ListItem item in cblRoles.Items)
                    {
                        if (item.Selected)
                        {
                            if (string.IsNullOrEmpty(sRoles) == true)
                            {
                                sRoles = item.Value;
                            }
                            else
                            {
                                sRoles += "," + item.Value;
                            }
                        }
                    }
                }

                return sRoles;
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "Conversion.GetSelectedValue", e);
            }

            return string.Empty;
        }

        public static void ClearChechBoxList(ref CheckBoxList cbl)
        {
            if (cbl != null && cbl.Items.Count > 0)
            {
                foreach (ListItem item in cbl.Items)
                {
                    if (item.Selected)
                        item.Selected = false;
                }
            }
        }

        public static void SetEventListner(ref TextBox objBox, bool date, bool deci, bool isSSN, bool isPhone)
        {
            objBox.Attributes.Add("onkeypress", "doKeypress(" + System.Convert.ToInt32(date) + "," + System.Convert.ToInt32(deci) + ");");
            objBox.Attributes.Add("onkeyup", "doKeyUp(this," + System.Convert.ToInt32(isPhone) + "," + System.Convert.ToInt32(isSSN) + ");");
            objBox.Attributes.Add("onbeforepaste", "doBeforePaste();");
            objBox.Attributes.Add("onpaste", "doPaste();");
        }

        /// <summary>
        /// This will call method while passing txt box object
        /// </summary>
        /// <param name="objBox">txt box object</param>
        /// <param name="sMethodName">method name</param>
        /// <param name="lblError">object where error msg to display</param>
        public static void SetEventListner(ref TextBox objBox, string sMethodName, string sMsgLbl)
        {
            objBox.Attributes.Add("onkeypress", sMethodName + "(this,'" + sMsgLbl + "');");
            objBox.Attributes.Add("onkeyup", sMethodName + "(this,'" + sMsgLbl + "');");
            objBox.Attributes.Add("onbeforepaste", sMethodName + "(this,'" + sMsgLbl + "');");
            objBox.Attributes.Add("onpaste", sMethodName + "(this,'" + sMsgLbl + "');");
        }

        public static void SetEventListner(ref TextBox objBox, string sMethodName)
        {
            objBox.Attributes.Add("onkeypress", sMethodName + "(this);");
            objBox.Attributes.Add("onkeyup", sMethodName + "(this);");
            objBox.Attributes.Add("onbeforepaste", sMethodName + "(this);");
            objBox.Attributes.Add("onpaste", sMethodName + "(this);");
        }

        public static void SetEventListner(ref TextBox objQty, ref TextBox objCost, ref TextBox objAmount, string sMethodName)
        {
            string sMethod = sMethodName + "('" + objQty.ClientID + "', '" + objCost.ClientID + "', '" + objAmount.ClientID + "');";

            objQty.Attributes.Add("onkeypress", sMethod);
            objQty.Attributes.Add("onkeyup", sMethod);
            objQty.Attributes.Add("onbeforepaste", sMethod);
            objQty.Attributes.Add("onpaste", sMethod);

            objCost.Attributes.Add("onkeypress", sMethod);
            objCost.Attributes.Add("onkeyup", sMethod);
            objCost.Attributes.Add("onbeforepaste", sMethod);
            objCost.Attributes.Add("onpaste", sMethod);

            objAmount.Attributes.Add("onkeypress", sMethod);
            objAmount.Attributes.Add("onkeyup", sMethod);
            objAmount.Attributes.Add("onbeforepaste", sMethod);
            objAmount.Attributes.Add("onpaste", sMethod);
        }

        public static string GetRequestFriendlyPageName(HttpRequest request)
        {
            try
            {
                if (request != null)
                {
                    string[] sArr = request.CurrentExecutionFilePath.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string sPage in sArr)
                    {
                        if (sPage.IndexOf(".aspx") > 0)
                            return sPage;
                    }
                }
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "Conversion.GetRequestFriendlyPageName", e);
            }

            return string.Empty;
        }



        public static void ClearPageControls(Control Page)
        {

            foreach (Control ctrl in Page.Controls)
            {
                //if (ctrl is HiddenField)
                //{
                //    ((HiddenField)(ctrl)).Value = "0";

                //}
                if (ctrl is DropDownList)
                {
                    DropDownList lst = (DropDownList)ctrl;
                    if (lst.Items.Count > 0)
                        ((DropDownList)(ctrl)).SelectedIndex = 0;
                }
                else if (ctrl is TextBox)
                {
                    ((TextBox)(ctrl)).Text = "";
                }
                else if (ctrl is CheckBox)
                {
                    ((CheckBox)(ctrl)).Checked = false;
                }
                else if (ctrl is CheckBoxList)
                {
                    CheckBoxList Cblist = ((CheckBoxList)(ctrl));
                    for (int i = 0; i < Cblist.Items.Count; i++)
                    {
                        Cblist.Items[i].Selected = false;

                    }
                }
                else if (ctrl is RadioButton)
                {
                    ((RadioButton)(ctrl)).Checked = false;
                }
                else
                {
                    if (ctrl.Controls.Count > 0)
                    {
                        ClearPageControls(ctrl);
                    }

                }

            }

        }

        public static void ClearPageHiddenControls(Control Page)
        {

            foreach (Control ctrl in Page.Controls)
            {
                if (ctrl is HiddenField)
                {
                    ((HiddenField)(ctrl)).Value = "0";

                }
                else
                {
                    if (ctrl.Controls.Count > 0)
                    {
                        ClearPageControls(ctrl);
                    }

                }

            }

        }




        /// <summary>
        /// This will round up the value in left direction
        /// </summary>
        /// <param name="dVal">23343.343</param>
        /// <param name="dRange">1 = 10, 2= 100, 3= 1000 etc.</param>
        /// <returns></returns>
        public static string RoundToLeftDirection(decimal dVal, decimal dRange)
        {
            try
            {
                return Math.Round((dVal / dRange), 0).ToString() + "000";
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "Conversion.RoundToLeftDirection", e);
            }
            return "0";
        }

        public static string GetCountString(List<string> lstValues)
        {
            decimal dVal = 0;

            try
            {
                if (lstValues != null && lstValues.Count > 0)
                {
                    foreach (string sVal in lstValues)
                    {
                        if (string.IsNullOrEmpty(sVal) == false)
                            dVal += ParseDecimal(sVal);
                    }

                    return ParseString(dVal);
                }
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "Conversion.GetCountString", e);
            }

            return "0";
        }

        public static string GetUnSelectedItemValue(CheckBoxList cbl)
        {
            string sData = string.Empty;

            try
            {
                if (cbl != null)
                {
                    foreach (ListItem item in cbl.Items)
                    {
                        if (!item.Selected)
                        {
                            if (string.IsNullOrEmpty(sData) == true)
                            {
                                sData = "<li>" + item.Value + "</li>";
                            }
                            else
                            {
                                sData += "<li>" + item.Value + "</li>";
                            }
                        }
                    }
                }

                return "<ol>" + sData + "</ol>";
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "Conversion.GetUnSelectedItemValue", e);
            }

            return string.Empty;
        }

        public static void CheckedItemByValue(ref CheckBoxList cbl, string sVal)
        {
            if (cbl != null && cbl.Items.Count > 0)
            {
                foreach (ListItem lItem in cbl.Items)
                {
                    if (lItem.Value.ToLower().Equals(sVal.ToLower()))
                    {

                        lItem.Selected = true;
                        lItem.Enabled = false;
                    }
                }
            }
        }

        public static void CheckedCheckBoxList(ref CheckBoxList cbl, List<string> LstValues)
        {
            if (cbl != null && cbl.Items.Count > 0)
            {
                foreach (ListItem lItem in cbl.Items)
                {
                    if (LstValues.Contains(lItem.Value))
                    {
                        lItem.Selected = true;
                    }
                }
            }
        }

        public static void CheckedItemByValue2(ref CheckBoxList cbl, string sVal, DateTime dtmDate)
        {
            if (cbl != null && cbl.Items.Count > 0)
            {
                foreach (ListItem lItem in cbl.Items)
                {
                    if (lItem.Value.ToLower().Equals(sVal.ToLower()))
                    {

                        if (dtmDate.Date < DateTime.Now.Date)
                        {
                            lItem.Selected = false;
                            lItem.Enabled = true;
                        }
                        else
                        {
                            lItem.Selected = true;
                            lItem.Enabled = false;

                        }
                    }
                }
            }
        }



        public static bool AllItemIsChecked(ref CheckBoxList cbl)
        {
            bool bResult = true;

            try
            {
                if (cbl != null && cbl.Items.Count > 0)
                {
                    foreach (ListItem lItem in cbl.Items)
                    {
                        if (!lItem.Selected)
                        {
                            return false;
                        }
                    }
                }

                return bResult;
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "Conversion.AllItemIsChecked", e);
            }

            return true;
        }



        private static string ReverseString(string sVal)
        {
            string sOutput = sVal;

            StringBuilder sb = new StringBuilder();

            try
            {
                for (int i = 1; i <= sVal.Length; i++)
                {
                    sb.Append(sVal[sVal.Length - i]);
                }

                sOutput = sb.ToString();
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "Conversion.ReverseString", e);
            }

            return sOutput;
        }


        private static int GetIndexToMove(int iSelectedItem, ListBox lbModules)
        {
            try
            {
                int iIndex = -1;

                for (int i = iSelectedItem + 1; i < lbModules.Items.Count; i++)
                {
                    if (lbModules.Items[i].Text.IndexOf("---") < 0)
                    {
                        iIndex = i;
                        break;
                    }
                }

                return iIndex;
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "Conversion.GetIndexToMove", e);
            }

            return -1;
        }




        /// <summary>
        /// This procedure is use to fill dropdown according to your requirement.
        /// </summary>
        /// <param name="ddl">this is the dropdownlist which is required to fill</param>
        /// <param name="dt">this is the table which is use to fill the dropdown</param>
        /// <returns>it return the dropdownlist</returns>
        public static void FillDropDown(DropDownList ddl, DataTable dt)
        {
            ListItem li = new ListItem(ResourceMgr.GetMessage("Select"), "0");
            ddl.Items.Insert(0, li);

            foreach (DataRow row in dt.Rows)
            {
                ListItem item = new ListItem();
                item.Value = row[0].ToString();
                item.Text = row[1].ToString();
                ddl.Items.Add(item);
            }


        }

        public static string GetDecryptQueryStringValue(string strValue)
        {
            try
            {
                if (HttpContext.Current.Request[strValue] != null)
                    return Encryption.Decrypt(HttpContext.Current.Request[strValue]);
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "Conversion.GetDecryptQueryStringValue", e);
            }

            return string.Empty;

        }
        public static string ConvertToYesNo(bool val)
        {
            if (val)
            {
                return "Yes";

            }
            else
            {

                return "No";
            }


        }
        //public static object ConvertToDBNULL(object obj) 
        //{
        //    if (obj == null)
        //    {

        //        return DBNull.Value;
        //    }
        //    else
        //    {
        //        return obj;

        //    }

        //}
    }
}
