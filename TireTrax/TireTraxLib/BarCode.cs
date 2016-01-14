using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;
using Zen.Barcode;
using System.Drawing;
using System.Web;


namespace TireTraxLib
{
    public class BarCode
    {

        private int _barcodeid;
        [XmlElement("BarCodeID")]
        public int BarCodeID
        {
            get { return _barcodeid; }
            set { _barcodeid = value; }
        }

        private string _barcodeNumber;
        [XmlElement("BarCodeNumber")]
        public string BarCodeNumber
        {
            get { return _barcodeNumber; }
            set { _barcodeNumber = value; }
        }

        private bool _isused;
        [XmlElement("IsUsed")]
        public bool IsUsed
        {
            get { return _isused; }
            set { _isused = value; }
        }

        private bool _isActive;
        [XmlElement("IsActive")]
        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }

        private string _datecreated;
        [XmlElement("DateCreated")]
        public string DateCreated
        {
            get { return _datecreated; }
            set { _datecreated = value; }
        }

        public int _organizationId;
        [XmlElement("OrganizationID")]
        public int OrganizationID
        {
            get { return _organizationId; }
            set { _organizationId = value; }
        }

        private string _latitude;
        [XmlElement("Latitude")]
        public string Latitude
        {
            get { return _latitude; }
            set { _latitude = value; }
        }

        private string _longitude;
        [XmlElement("Longitude")]
        public string Longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }

        private string _deviceID;
        [XmlElement("DeviceID")]
        public string DeviceID
        {
            get { return _deviceID; }
            set { _deviceID = value; }
        }

        private string _devicetype;
        [XmlElement("DeviceType")]
        public string DeviceType
        {
            get { return _devicetype; }
            set { _devicetype = value; }
        }

        private string _location;
        [XmlElement("Location")]
        public string Location
        {
            get { return _location; }
            set { _location = value; }
        }

        private string _description;
        [XmlElement("Description")]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        private string _previousTime;
        [XmlElement("DateAge")]
        public string PreviousTime
        {
            get { return _previousTime; }
            set { _previousTime = value; }
        }

        private byte[] _Image;

        public byte[] Image
        {
            get { return _Image; }
            set { _Image = value; }
        }


        public BarCode()
        {

        }

        public BarCode(string barCode)
        {
            Load(barCode);
        }
        public BarCode(int  barCodeId)
        {
            Load(barCodeId);
        }

        private void Load(int barcodeId)
        {
            IDataReader reader = null;
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    var prams = new SqlParameter[1];
                    prams[0] = db.MakeInParam("@intBarCodeId", SqlDbType.Int, 0, barcodeId);
                    reader = db.GetDataReader("up_getAllBarCodeInfobyBarCodeId", prams);
                    if (reader.Read())
                        load(reader);
                }
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "Loads.load", e);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }
        private void load(IDataReader reader)
        {
            try
            {

               
                _barcodeid = Conversion.ParseDBNullInt(reader["barCodeId"]);
                _barcodeNumber = Conversion.ParseDBNullString(reader["SerialNumber"]);
                _isused = Conversion.ParseDBNullBool(reader["IsUsed"]);
                _isActive = Conversion.ParseDBNullBool(reader["IsActive"]);
                _datecreated = Conversion.ParseDBNullString(reader["DateCreated"]);
                _organizationId = Conversion.ParseDBNullInt(reader["OrganizationId"]);
                
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Loads.load", ex);
            }
        }

        private void Load(string barCode)
        {
            IDataReader reader = null;
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    List<SqlParameter> prams = new List<SqlParameter>();
                    prams.Add(db.MakeInParam("@vchBarcode", SqlDbType.VarChar, 50, barCode));
                    reader = db.GetDataReader("Up_getBarCodesbyVale", prams.ToArray());
                    if (reader.Read())
                    {
                        _barcodeid = reader["TX_BarCodeId"] == System.DBNull.Value ? 0 : Convert.ToInt32(reader["TX_BarCodeId"]);
                        _barcodeNumber = reader["BarCodeNumber"] == System.DBNull.Value ? "" : Convert.ToString(reader["BarCodeNumber"]);
                        _datecreated = reader["DateCreated"] == System.DBNull.Value ? "" : Convert.ToString(reader["DateCreated"]);
                        _isActive = reader["IsActive"] == System.DBNull.Value ? false : Convert.ToBoolean(reader["IsActive"]);
                        _isused = reader["IsUsed"] == System.DBNull.Value ? false : Convert.ToBoolean(reader["IsUsed"]);
                        _organizationId = reader["OrganizationId"] == System.DBNull.Value ? 0 : Convert.ToInt32(reader["OrganizationId"]);
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "barCode.Load", ex);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }


        public static int InsertBarCode(BarCode bc, int UserID, string latitude, string longitude, string DeviceID, string DeviceType, string Description)
        {
            int LogID = 0;
            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@intBarcodeID", SqlDbType.Int, 0, bc.BarCodeID));
                    prams.Add(db.MakeInParam("@intUserID", SqlDbType.Int, 0, UserID));
                    prams.Add(db.MakeInParam("@dcmlatitude", SqlDbType.VarChar, 50, latitude));
                    prams.Add(db.MakeInParam("@dcmlongitude", SqlDbType.VarChar, 50, longitude));
                    prams.Add(db.MakeInParam("@vchDeviceID", SqlDbType.VarChar, 50, DeviceID));
                    prams.Add(db.MakeInParam("@vchDeviceType", SqlDbType.VarChar, 50, DeviceType));
                    prams.Add(db.MakeInParam("@vchDescription", SqlDbType.VarChar, 200, Description));
                    LogID = db.RunProc("Up_InsertBarCodeLog", prams.ToArray());
                }
            }
            catch (Exception ex)
            {
                LogID = 0;
                new SqlLog().InsertSqlLog(0, "BarCode.InsertBarCode", ex);
            }

            return LogID;
        }


        public static DataTable GetBarCodesByUserID(int UserID)
        {
            DataTable table = null;
            List<SqlParameter> prams = new List<SqlParameter>();
            using (DbManager db = DbManager.GetDbManager())
            {
                try
                {
                    prams.Add(db.MakeInParam("@intUserID", SqlDbType.Int, 0, UserID));
                    table = db.GetDataSet("Up_getBarcodedatabyUserID", prams.ToArray()).Tables[0];
                }
                catch (Exception ex)
                {
                    new SqlLog().InsertSqlLog(0, "BarCode.GetBarCodeByUserID", ex);
                }
            }
            return table;
        }

        public static DataTable GetBarCodesByValue(string BarCode)
        {
            DataTable table = null;
            List<SqlParameter> prams = new List<SqlParameter>();
            using (DbManager db = DbManager.GetDbManager())
            {
                try
                {
                    prams.Add(db.MakeInParam("@vchBarCode", SqlDbType.VarChar, 50, BarCode));
                    table = db.GetDataSet("Up_getBarcodedatabyValue", prams.ToArray()).Tables[0];
                }
                catch (Exception ex)
                {
                    new SqlLog().InsertSqlLog(0, "BarCode.GetBarCodesByValue", ex);
                }
            }
            return table;
        }

        public static int Insert(BarCode bar)
        {
            int LogID = 0;
            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@vchSerialNumber", SqlDbType.NVarChar, 12, bar.BarCodeNumber));
                    prams.Add(db.MakeInParam("@dtmDateCreated", SqlDbType.DateTime, 8, Convert.ToDateTime(bar.DateCreated)));
                    prams.Add(db.MakeInParam("@intOrgId", SqlDbType.Int, 4, bar.OrganizationID));
                    prams.Add(db.MakeInParam("@Image", SqlDbType.VarBinary, -1, bar.Image));

                    LogID = db.RunProc("up_InsertBarcode", prams.ToArray());
                }
            }
            catch (Exception ex)
            {
                LogID = 0;
                new SqlLog().InsertSqlLog(0, "BarCode.Insert", ex);
            }
            return LogID;
        }

        public bool GenerateLotBarCodeImage(string data)
        {
            try
            {
                //context.Response.ContentType = "image/jpeg";
                string Code = data.ToString(); //txtLotNmber.Text;

                Code128BarcodeDraw br = BarcodeDrawFactory.Code128WithChecksum;
                int size = 100;
                System.Drawing.Image img = br.Draw((data), size, 2);

                Font stringFont = new Font("Arial", 16);
                SizeF textsize = new SizeF();
                System.Drawing.Image imgText = DrawText(data, stringFont, Color.Black, Color.White, img.Width,img.Height, out textsize);


                Bitmap bitmap = new Bitmap(img.Width, img.Height + imgText.Height);
            
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.DrawImage(img, 0, 0);
                    g.DrawImage(imgText, 0, img.Height);
                }
                string strPath = HttpContext.Current.Server.MapPath("/images/temp");
                System.IO.Directory.CreateDirectory(strPath);
                bitmap.Save(HttpContext.Current.Server.MapPath(String.Format("/images/temp/{0}.Gif", data)), System.Drawing.Imaging.ImageFormat.Gif);


            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Utils GenerateLotBarCodeImage", ex);
                return false;
            }

            return true;
        }

        private System.Drawing.Image DrawText(String text, Font font, Color textColor, Color backColor, int width, int height, out SizeF textsize)
        {
            //first, create a dummy bitmap just to get a graphics object
            System.Drawing.Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);

            //measure the string to see how big the image needs to be
            textsize = drawing.MeasureString(text, font);

            //free up the dummy image and old graphics object
            img.Dispose();
            drawing.Dispose();

            //create a new image of the right size
            img = new Bitmap((int)width, (int)height);
           

            drawing = Graphics.FromImage(img);

            //paint the background
            drawing.Clear(backColor);

            //create a brush for the text
            Brush textBrush = new SolidBrush(textColor);

            drawing.DrawString(text, font, textBrush, (width - textsize.Width) / 2, 0);

            drawing.Save();

            textBrush.Dispose();
            drawing.Dispose();

            return img;

        }

        

    }
}
