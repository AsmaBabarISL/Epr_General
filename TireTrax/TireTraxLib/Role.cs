using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace TireTraxLib
{
   public class Role
    {
       public static void ActiveDeactiveRole(int roleid, int lookuptypeId)
       {
           try
           {
               using (DbManager db = DbManager.GetDbManager())
               {
                   List<SqlParameter> prams = new List<SqlParameter>();
                   prams.Add(db.MakeInParam("@roleId", SqlDbType.Int, 0, roleid));
                   prams.Add(db.MakeInParam("@lookuptypeId", SqlDbType.Int, 0, lookuptypeId));

                   db.GetDataSet("up_ActiveDeactiveRoles", prams.ToArray());
               }
           }
           catch (Exception exp)
           {
               new SqlLog().InsertSqlLog(0, "Role.ActiveDeactiveRole", exp);
           }
       }
    }
}
