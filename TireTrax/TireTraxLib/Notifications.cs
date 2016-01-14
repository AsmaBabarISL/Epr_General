using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TireTraxLib
{
   public class Notifications
    {
        private int _intNotificationId;

        public int IntNotificationId
        {
            get { return _intNotificationId; }
            set { _intNotificationId = value; }
        }
        private int _intFromUserId;
        private int _intFromOrganizationId;

        public int IntFromOrganizationId
        {
            get { return _intFromOrganizationId; }
            set { _intFromOrganizationId = value; }
        }
        public int IntFromUserId
        {
            get { return _intFromUserId; }
            set { _intFromUserId = value; }
        }
        private int _intToOrganizationId;

        public int IntToOrganizationId
        {
            get { return _intToOrganizationId; }
            set { _intToOrganizationId = value; }
        }
        private int _intToUserId;

        public int IntToUserId
        {
            get { return _intToUserId; }
            set { _intToUserId = value; }
        }
        private DateTime _dtmDateCreated;

        public DateTime DtmDateCreated
        {
            get { return _dtmDateCreated; }
            set { _dtmDateCreated = value; }
        }
        private DateTime _dtmDateReaded;

        public DateTime DtmDateReaded
        {
            get { return _dtmDateReaded; }
            set { _dtmDateReaded = value; }
        }
        private string _vchNotificationText;

        public string VchNotificationText
        {
            get { return _vchNotificationText; }
            set { _vchNotificationText = value; }
        }
        private int _intSourceId;

        public int IntSourceId
        {
            get { return _intSourceId; }
            set { _intSourceId = value; }
        }
        private int _intParentNotificationId;

        public int IntParentNotificationId
        {
            get { return _intParentNotificationId; }
            set { _intParentNotificationId = value; }
        }
        private bool _bitIsReaded;

        public bool BitIsReaded
        {
            get { return _bitIsReaded; }
            set { _bitIsReaded = value; }
        }
        private bool _bitIsActive;

        public bool BitIsActive
        {
            get { return _bitIsActive; }
            set { _bitIsActive = value; }
        }

         public Notifications()
        {

        }
         public Notifications(int notificationid)
        {
            Load(notificationid);
        }
        private void Load(int notificationid)
        {
            IDataReader reader = null;
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    var prams = new SqlParameter[1];
                    prams[0] = db.MakeInParam("@intNotificationId", SqlDbType.Int, 0, notificationid);
                    reader = db.GetDataReader("up_Notification_getById", prams);
                    if (reader.Read())
                        Load(reader);
                }
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "Notification.Load", e);
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
                _intNotificationId = Conversion.ParseDBNullInt(reader["intNotificationId"]);

                _intFromOrganizationId = Conversion.ParseDBNullInt(reader["intFromOrganizationId"]);
                _intFromUserId = Conversion.ParseDBNullInt(reader["intFromUserId"]);
               
                _intToOrganizationId = Conversion.ParseDBNullInt(reader["intToOrganizationId"]);
                _intToUserId = Conversion.ParseDBNullInt(reader["intToUserId"]);

                _dtmDateCreated = Conversion.ParseDBNullDateTime(reader["dtmDateCreated"]);
                _dtmDateReaded = Conversion.ParseDBNullDateTime(reader["dtmDateReaded"]);

                _vchNotificationText = Conversion.ParseDBNullString(reader["vchNotificationText"]);

                _bitIsReaded = Conversion.ParseDBNullBool(reader["bitIsReaded"]);
                _bitIsActive = Conversion.ParseDBNullBool(reader["bitIsActive"]);
                _intSourceId = Conversion.ParseDBNullInt(reader["intSourceId"]);
               
                _intParentNotificationId = Conversion.ParseDBNullInt(reader["intParentNotificationId"]);


            }
            catch (Exception ex)
            {

                new SqlLog().InsertSqlLog(0, "Notifications.Load", ex);
            }


        }


        public static DataSet getNotifications(int FromUserId, int ToUserId,int FromorganizationId,int ToOrganizationId, bool Onlyisreaded, bool onlyactive)
        {
            DataSet ds = null;


            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    var prams = new SqlParameter[6];
                    prams[0] = db.MakeInParam("@intFromUserId", SqlDbType.Int, 4, FromUserId);
                    prams[1] = db.MakeInParam("@intToUserId", SqlDbType.Int, 4, ToUserId);
                    prams[2] = db.MakeInParam("@intToOrganizationId", SqlDbType.Int, 4, ToOrganizationId);
                    prams[3] = db.MakeInParam("@intFromOrganizationId", SqlDbType.Int, 4, FromorganizationId);
                    prams[4] = db.MakeInParam("@bitOnlyIsReaded", SqlDbType.Bit, 4, Onlyisreaded);
                    prams[5] = db.MakeInParam("@bitOnlyActive", SqlDbType.Bit, 4, onlyactive);
                    ds = db.GetDataSet("up_getNotifications", prams);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                         return ds;
                    }
                }
            }
            catch (Exception exp)
            {
                new SqlLog().InsertSqlLog(0, "Notifications.getNotifications", exp);
            }
            return ds;
        }
        public static DataSet getAllNotifications(int ToUserId, int ToOrganizationId, bool isReaded, bool onlyactive,int pagesize, int pageId)
        {
            DataSet ds = null;


            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    var prams = new SqlParameter[6];
                  
                  
                    prams[0] = db.MakeInParam("@intToOrganizationId", SqlDbType.Int, 4, ToOrganizationId);
                    prams[1] = db.MakeInParam("@intToUserId", SqlDbType.Int, 4, ToUserId);
                    prams[2] = db.MakeInParam("@bitIsReaded", SqlDbType.Bit, 4, isReaded);
                    prams[3] = db.MakeInParam("@bitOnlyActive", SqlDbType.Bit, 4, onlyactive);
                    prams[4] = db.MakeInParam("@intPageid", SqlDbType.Int, 4, pageId);
                    prams[5] = db.MakeInParam("@intPageSize", SqlDbType.Int, 4, pagesize);
                    ds = db.GetDataSet("up_getAllNotifications", prams);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        return ds;
                    }
                }
            }
            catch (Exception exp)
            {
                new SqlLog().InsertSqlLog(0, "Notifications.getAllNotifications", exp);
            }
            return ds;
        }
        public static DataSet getAllNotificationsReadUnread(int ToUserId, int ToOrganizationId,  bool onlyactive, int pagesize, int pageId)
        {
            DataSet ds = null;


            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    var prams = new SqlParameter[5];


                    prams[0] = db.MakeInParam("@intToOrganizationId", SqlDbType.Int, 4, ToOrganizationId);
                    prams[1] = db.MakeInParam("@intToUserId", SqlDbType.Int, 4, ToUserId);
                    prams[2] = db.MakeInParam("@bitOnlyActive", SqlDbType.Bit, 4, onlyactive);
                    prams[3] = db.MakeInParam("@intPageid", SqlDbType.Int, 4, pageId);
                    prams[4] = db.MakeInParam("@intPageSize", SqlDbType.Int, 4, pagesize);
                    ds = db.GetDataSet("up_getAllNotificationsReadUnRead", prams);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        return ds;
                    }
                }
            }
            catch (Exception exp)
            {
                new SqlLog().InsertSqlLog(0, "Notifications.getAllNotifications", exp);
            }
            return ds;
        }
    

        public  int InsertUpdate()
        {
            int intNotificationId;
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {

                    prm.Add(DB.MakeInParam("@intNotificationId", SqlDbType.Int, 4, _intNotificationId));
                    prm.Add(DB.MakeInParam("@intFromOrganizationId", SqlDbType.Int, 4, _intFromOrganizationId));
                    prm.Add(DB.MakeInParam("@intFromUserId", SqlDbType.Int, 4, _intFromUserId));
                    prm.Add(DB.MakeInParam("@intToOrganizationId", SqlDbType.Int, 4, _intToOrganizationId));
                    prm.Add(DB.MakeInParam("@intToUserId", SqlDbType.Int, 4, _intToUserId));
                    prm.Add(DB.MakeInParam("@intSourceId", SqlDbType.Int, 4, _intSourceId));
                    prm.Add(DB.MakeInParam("@intParentNotificationId", SqlDbType.Int, 4, _intParentNotificationId));

                    prm.Add(DB.MakeInParam("@dtmDateCreated", SqlDbType.DateTime, 0, _dtmDateCreated));
                    if(_dtmDateReaded==DateTime.MinValue)
                        prm.Add(DB.MakeInParam("@dtmDateReaded", SqlDbType.DateTime, 0, DBNull.Value));
                    else
                    prm.Add(DB.MakeInParam("@dtmDateReaded", SqlDbType.DateTime, 0, _dtmDateReaded));

                    prm.Add(DB.MakeInParam("@vchNotificationText", SqlDbType.NVarChar, 1000, _vchNotificationText));

                    prm.Add(DB.MakeInParam("@bitIsReaded", SqlDbType.Bit, 0, _bitIsReaded));
                    prm.Add(DB.MakeInParam("@bitIsActive", SqlDbType.Bit, 0, _bitIsActive));

                    intNotificationId = DB.RunProc("up_Notification_InsertUpdate", prm.ToArray());
                    intNotificationId = Conversion.ParseInt(prm.Last<SqlParameter>().Value);

                }
            }
            catch (Exception ex)
            {
                intNotificationId = 0;
                new SqlLog().InsertSqlLog(0, "Notifications.insertupdate", ex);
            }
            return intNotificationId;

        }
                
    }
}
