using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TireTraxLib
{
    public class LotRows
    {



        # region Properties

        private int _spaceId;
        public int SpaceId
        {
            get { return _spaceId; }
            set { _spaceId = value; }
        }

        private string _spaceName;
        public string SpaceName
        {
            get { return _spaceName; }
            set { _spaceName = value; }
        }

        private string _spaceDes;
        public string Description
        {
            get { return _spaceDes; }
            set { _spaceDes = value; }
        }

        private bool _isActive;

        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }

        private DateTime _dateCreated;
        public DateTime DateCreated
        {
            get { return _dateCreated; }
            set { _dateCreated = value; }
        }

        private int _lotId;
        public int LotId
        {
            get { return _lotId; }
            set { _lotId = value; }
        }

        #endregion


        # region Methods

        public LotRows()
        {


        }
        public LotRows(int RowId)
        {
 

        }


       

        private void loadLotRowsInfo(int RowId)
        {
            IDataReader reader = null;
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    var prams = new SqlParameter[1];
                    prams[0] = db.MakeInParam("@introwId", SqlDbType.Int, 0, RowId);
                    reader = db.GetDataReader("up_getLotRowsByRowsId", prams);
                    if (reader.Read())
                        loadLotRowsInfo(reader);
                }
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "LotRows.LotsRowsInfoLoad", e);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }



        private void loadLotRowsInfo(IDataReader reader)
        {
            try
            {
                 SpaceId= Conversion.ParseDBNullInt(reader["intSpaceId"]);
                 SpaceName = Conversion.ParseDBNullString(reader["vchSpaceName"]);
                 Description = Conversion.ParseDBNullString(reader["vchDesciption"]);
                 IsActive= Conversion.ParseDBNullBool(reader["bitActive"]);
                _dateCreated = Conversion.ParseDBNullDateTime(reader["dtmDateCreated"]);
                 LotId = Conversion.ParseDBNullInt(reader["intNewParkingLotId"]);
                
            }

            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "CreditCard.LoadCreditCardInfo", ex);
            }
        }

        public static int insertUpdate(LotRows spc, int lotId)
        {
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    if (spc.SpaceId > 0)
                        prm.Add(DB.MakeInParam("@intSpaceId", SqlDbType.Int, 4, spc.SpaceId));
                    prm.Add(DB.MakeInParam("@vchName", SqlDbType.NVarChar, 500, spc.SpaceName));
                    prm.Add(DB.MakeInParam("@vchDescription", SqlDbType.NVarChar, 500, spc.SpaceName));
                    prm.Add(DB.MakeInParam("@bitActive", SqlDbType.Bit, 1, spc.IsActive));
                    prm.Add(DB.MakeInParam("@dtmDateCreated", SqlDbType.DateTime, 8, spc.DateCreated));
                    prm.Add(DB.MakeInParam("@intLotId", SqlDbType.Int, 4, lotId));

                    int Id = DB.RunProc("up_Spaces_InsertUpdate", prm.ToArray());
                    if (Id > 0)
                    {
                        return Id;
                    }

                }
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "Row Insert", e);
            }
            return 0;
        }


      


        public static DataSet getLotSpacesByLotId(int pageId, int pageSize, out int iTotalrows, int lotId)
        {
            DataSet ds = null;
            iTotalrows = 0;
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@intPageId", SqlDbType.Int, 4, pageId));
                    prm.Add(DB.MakeInParam("@intPageSize", SqlDbType.Int, 4, pageSize));
                    prm.Add(DB.MakeInParam("@intlotId", SqlDbType.Int, 4, lotId));

                    prm.Add(DB.MakeReturnParam(SqlDbType.Int, 4));

                    ds = DB.GetDataSet("up_lots_space_GetAll", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        iTotalrows = Conversion.ParseInt(prm.Last<SqlParameter>().Value);
                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Space GetLotSpacesByLotId", ex);
            }
            return ds;
        }


        public static DataSet getLotRowsByLotId(int lotId)
        {
            DataSet ds = null;
           
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {

                    prm.Add(DB.MakeInParam("@intLotID", SqlDbType.Int, 4, lotId));

                    prm.Add(DB.MakeReturnParam(SqlDbType.Int, 4));

                    ds = DB.GetDataSet("Up_Space_getSpaceLookUp", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                       
                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "LotRows getLotRowsByLotId", ex);
            }
            return ds;
        }


       
        public static DataSet getSpacesbyLotId(int lotid)
        {
            DataSet ds = null;

            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@intlotid", SqlDbType.Int, 0, lotid));


                    ds = DB.GetDataSet("up_parkinglanding_countbyLotId", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Space GetSpace on Lotid", ex);
            }
            return ds;
        }
        
        public static void deleteSpaces(int spaceId)
        {
            DataSet ds = null;

            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@intspaceid", SqlDbType.Int, 0, spaceId));

                    ds = DB.GetDataSet("up_delete_spaces", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {


                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Space DeleteSpaces", ex);
            }

        }
       

      

        #endregion











    }
}
