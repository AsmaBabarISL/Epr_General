using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace TireTraxLib
{
   public  class LotSpace
    {
        private int _laneId;

        public int LaneId
        {
            get { return _laneId; }
            set { _laneId = value; }
        }

        private string _laneName;

        public string LaneName
        {
            get { return _laneName; }
            set { _laneName = value; }
        }
        private string _spaceName;

        public string SpaceName
        {
            get { return _spaceName; }
            set { _spaceName = value; }
        }
        private DateTime _dateCreated;

        public DateTime DateCreated
        {
            get { return _dateCreated; }
            set { _dateCreated = value; }
        }

        private Boolean _bitActive;

        public Boolean BitActive
        {
            get { return _bitActive; }
            set { _bitActive = value; }
        }

        private int _spaceId;

        public int SpaceId
        {
            get { return _spaceId; }
            set { _spaceId = value; }
        }

        private int _quantity;

        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }



        public LotSpace()
        {


        }


        public LotSpace(int laneId)
        {

            Load(laneId);

        }

        private void Load(int laneId)
        {
            IDataReader reader = null;
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    var prams = new SqlParameter[1];
                    prams[0] = db.MakeInParam("@intLaneId", SqlDbType.Int, 0, laneId);
                    reader = db.GetDataReader("up_getLotSpaceInfoBySpaceId", prams);
                    if (reader.Read())
                        Load(reader);
                }
            }
            catch (Exception e)
            { 
                new SqlLog().InsertSqlLog(0, "LotsSpace.Load", e);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }
        private void Load(IDataReader reader)
        {
            try
            {
                _laneId = Conversion.ParseDBNullInt(reader["intLaneId"]);
                _laneName = Conversion.ParseDBNullString(reader["nvchLaneName"]);
                _dateCreated = Conversion.ParseDBNullDateTime(reader["dtamDateCreated"]);
                _bitActive = Conversion.ParseDBNullBool(reader["bitActive"]);
                _spaceId = Conversion.ParseDBNullInt(reader["intSpaceId"]);
                _quantity = Conversion.ParseDBNullInt(reader["intQuantity"]);
                _spaceName = Conversion.ParseDBNullString(reader["vchSpaceName"]);
                

            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "LotSpace.Load", ex);
            }
        }


        public static DataSet getLane(int pageId, int pageSize, out int iTotalrows, int spaceId)
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
                    prm.Add(DB.MakeInParam("@intspaceid", SqlDbType.Int, 0, spaceId));

                    prm.Add(DB.MakeReturnParam(SqlDbType.Int, 4));
                    ds = DB.GetDataSet("up_getLane", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        iTotalrows = Conversion.ParseInt(prm.Last<SqlParameter>().Value);
                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "LotSpace GetLane", ex);
            }
            return ds;
        }

        public static DataSet getLotSpacebyRowId(int spaceId)
        {
            DataSet ds = null;

            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@intSpaceID", SqlDbType.Int, 0, spaceId));


                    ds = DB.GetDataSet("Up_Lane_getLaneLookUp", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "LotSpace getLotSpacebyRowId", ex);
            }
            return ds;
        }


        public static void deleteLane(int laneId)
        {
            DataSet ds = null;

            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@intlaneid", SqlDbType.Int, 0, laneId));


                    ds = DB.GetDataSet("up_DeleteLanesInfo", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {


                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "LotSpace DeleteLane", ex);
            }

        }


        public static void updateLaneInfo(int laneid, int spaceid, String lanename)//,int quantity
        {
            DataSet ds = null;

            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@intlaneid", SqlDbType.Int, 0, laneid));
                    prm.Add(DB.MakeInParam("@lanename", SqlDbType.NVarChar, 500, lanename));
                    prm.Add(DB.MakeInParam("@spaceid", SqlDbType.Int, 0, spaceid));
                    //prm.Add(DB.MakeInParam("@intquantity", SqlDbType.Int, 0, quantity));

                    ds = DB.GetDataSet("up_LaneUpdate", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {


                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "LotSpace Update Lane Info", ex);
            }

        }



        public static int insertLane(String lanename, int spaceid)
        {
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {

                    prm.Add(DB.MakeInParam("@intspaceid", SqlDbType.Int, 0, spaceid));
                    prm.Add(DB.MakeInParam("@lanename", SqlDbType.NVarChar, 500, lanename));
                    //prm.Add(DB.MakeInParam("@intquantity", SqlDbType.Int, 0, quantity));


                    int Id = DB.RunProc("add_laneinfo", prm.ToArray());

                    if (Id > 0)
                    {
                        return Id;
                    }
                }
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "LOTSpace Lane Insert", e);
            }
            return 0;
        }


    }
}
