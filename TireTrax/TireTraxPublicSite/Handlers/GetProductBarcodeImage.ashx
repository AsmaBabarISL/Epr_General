<%@ WebHandler Language="C#" Class="GetProductBarcodeImage" %>

using System;
using System.Web;
using TireTraxLib;
using System.Data;

public class GetProductBarcodeImage : IHttpHandler {

    private int LotID = 0;
    private int ProductId = 0;
    HttpContext globalContext = null;
    public void ProcessRequest (HttpContext context) {
        globalContext = context;
        if (context.Request["LotID"] == null && context.Request["TireID"] == null)
        {
            context.Response.Write("<Epr><ErrorCode>601</ErrorCode><Message> Please provide Lot/Tire Id</Message></Epr>");
            return;
        }
        if (context.Request["LotID"] != null)
        {
            LotID = Conversion.ParseInt(context.Request.QueryString["LotID"]);
            DataSet ds = Lots.getBarcodeByLotId(LotID);
            Byte[] bytes = (Byte[])ds.Tables[0].Rows[0]["BarcodeImage"];
            context.Response.Buffer = true;
            context.Response.Charset = "";
            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            context.Response.ContentType = "image/gif";
            context.Response.AddHeader("content-disposition", "attachment;filename=" + ds.Tables[0].Rows[0]["SerialNumber"]);
            context.Response.BinaryWrite(bytes);
            context.Response.Flush();
            context.Response.End();
        }
        else if (context.Request["TireID"] != null)
        {
            ProductId = Conversion.ParseInt(context.Request.QueryString["TireID"]);
            DataSet ds = Product.getBarcodeByProductId(ProductId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                Byte[] bytes = (Byte[])ds.Tables[0].Rows[0]["BarcodeImage"];
                context.Response.Buffer = true;
                context.Response.Charset = "";
                context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                context.Response.ContentType = "image/gif";
                context.Response.AddHeader("content-disposition", "attachment;filename=" + ds.Tables[0].Rows[0]["SerialNumber"]);
                context.Response.BinaryWrite(bytes);
                context.Response.Flush();
                context.Response.End();
            }
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}