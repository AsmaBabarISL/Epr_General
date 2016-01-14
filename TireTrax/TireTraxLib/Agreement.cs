using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TireTraxLib
{
   public class Agreement
    {
        public static DataSet getAgreement(int intAgreementTypeId)
        {
            DataSet ds = null;


            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    var prams = new SqlParameter[1];
                    prams[0] = db.MakeInParam("@intTypeId", SqlDbType.Int, 4, intAgreementTypeId);
                    ds = db.GetDataSet("up_getAgreement", prams);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        return ds;
                    }
                }
            }
            catch (Exception exp)
            {
                new SqlLog().InsertSqlLog(0, "Agreement.getAgreement", exp);
            }
            return ds;
        }
    }
}
