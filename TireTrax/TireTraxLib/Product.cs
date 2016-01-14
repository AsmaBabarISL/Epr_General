using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TireTraxLib
{
    public class Product
    {

        #region Products
        private int _productId;
        public int ProductId
        {
            get { return _productId; }
            set { _productId = value; }
        }

        public int ProductSubCategory { get; set; }

        private int _productCategoryId;
        public int ProductCategoryId
        {
            get { return _productCategoryId; }
            set { _productCategoryId = value; }
        }

        private int _barcodeId;
        public int BarCodeId
        {
            get { return _barcodeId; }
            set { _barcodeId = value; }
        }

        private DateTime _dateCreated;
        public DateTime DateCreated
        {
            get { return _dateCreated; }
            set { _dateCreated = value; }
        }

        private int _createdById;
        public int CreatedByID
        {
            get { return _createdById; }
            set { _createdById = value; }
        }

        private string _sizeNumber;
        public string SizeNumber
        {
            get { return _sizeNumber; }
            set { _sizeNumber = value; }
        }

        private int _productShape;
        public int ProductShape
        {
            get { return _productShape; }
            set { _productShape = value; }
        }

        private int _productSize;
        public int ProductSize
        {
            get { return _productSize; }
            set { _productSize = value; }
        }

        private int _productMaterial;
        public int ProductMaterial
        {
            get { return _productMaterial; }
            set { _productMaterial = value; }
        }

        private int _brandId;
        public int BrandId
        {
            get { return _brandId; }
            set { _brandId = value; }
        }

        private string _productField1;
        public string ProductField1
        {
            get { return _productField1; }
            set { _productField1 = value; }
        }

        private string _productField2;
        public string ProductField2
        {
            get { return _productField2; }
            set { _productField2 = value; }
        }

        private string _productField3;
        public string ProductField3
        {
            get { return _productField3; }
            set { _productField3 = value; }
        }
        private string _WeekCode;
        public string WeekCode
        {
            get { return _WeekCode; }
            set { _WeekCode = value; }
        }
        private string _yearCode;
        public string YearCode
        {
            get { return _yearCode; }
            set { _yearCode = value; }
        }
        private int _langaugeId;
        public int LangaugeId
        {
            get { return _langaugeId; }
            set { _langaugeId = value; }
        }
        #endregion

        #region BarCode

        private byte[] _Image;

        public byte[] Image
        {
            get { return _Image; }
            set { _Image = value; }
        }

        private string _SerialNumber;

        public string SerialNumber
        {
            get { return _SerialNumber; }
            set { _SerialNumber = value; }
        }

        private int _OrganizationId;

        public int OrganizationId
        {
            get { return _OrganizationId; }
            set { _OrganizationId = value; }
        }

        private int _Tx_BarCode;

        public int Tx_BarCode
        {
            get { return _Tx_BarCode; }
            set { _Tx_BarCode = value; }
        }


        #endregion

        #region Constructors

        public Product()
        { }

        public Product(int Productid)
        {
            load(Productid);
        }

        #endregion

        #region Class Methods

        private void load(int Productid)
        {
            IDataReader reader = null;
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    var prams = new SqlParameter[1];
                    prams[0] = db.MakeInParam("@ProductId", SqlDbType.Int, 0, Productid);
                    reader = db.GetDataReader("Up_GetProductByProductId", prams);
                    if (reader.Read())
                        load(reader);
                }
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "Product.load", e);
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
                ProductId = Conversion.ParseDBNullInt(reader["Productid"]);
                ProductCategoryId = Conversion.ParseDBNullInt(reader["Productcategoryid"]);
                BarCodeId = Conversion.ParseDBNullInt(reader["BarCodeId"]);
                DateCreated = Conversion.ParseDBNullDateTime(reader["DateCreated"]);
                CreatedByID = Conversion.ParseDBNullInt(reader["CreatedById"]);
                SizeNumber = Conversion.ParseDBNullString(reader["SizeNumber"]);
                ProductShape = Conversion.ParseDBNullInt(reader["ProductShape"]);
                ProductSize = Conversion.ParseDBNullInt(reader["ProductSize"]);
                ProductMaterial = Conversion.ParseDBNullInt(reader["ProductMaterial"]);
                BrandId = Conversion.ParseDBNullInt(reader["BrandId"]);
                ProductField1 = Conversion.ParseDBNullString(reader["ProductField1"]);
                ProductField2 = Conversion.ParseDBNullString(reader["ProductField2"]);
                ProductField3 = Conversion.ParseDBNullString(reader["ProductField3"]);
                WeekCode = Conversion.ParseDBNullString(reader["MonthCode"]);
                YearCode = Conversion.ParseDBNullString(reader["YearCode"]);
                LangaugeId = Conversion.ParseDBNullInt(reader["LangaugeId"]);
                ProductSubCategory = Conversion.ParseDBNullInt(reader["Productsubcategoryid"]);
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Product.load", ex);
            }
        }

        public static DataSet GetProductNames()
        {
            DataSet ds = null;
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    ds = DB.GetDataSet("Up_GetProductNames");
                }

            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Product.GetProductNames", ex);
            }
            return ds;
        }

        public static int InsertProdut(Product objProduct)
        {
            List<SqlParameter> prams = new List<SqlParameter>();
            int ProductId = 0;

            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    //prams.Add(DB.MakeInParam("@BarCodeId ",SqlDbType.Int,4,objProduct.BarCodeId));
                    prams.Add(DB.MakeInParam("@DateCreated", SqlDbType.DateTime, 8, objProduct.DateCreated));
                    prams.Add(DB.MakeInParam("@TX_BarCodeId", SqlDbType.Int, 4, objProduct.Tx_BarCode));
                    prams.Add(DB.MakeInParam("@CreatedById", SqlDbType.Int, 4, objProduct.CreatedByID));
                    prams.Add(DB.MakeInParam("@ProductShape", SqlDbType.Int, 4, objProduct.ProductShape));
                    prams.Add(DB.MakeInParam("@ProductSize", SqlDbType.Int, 4, objProduct.ProductSize));
                    prams.Add(DB.MakeInParam("@ProductMaterial", SqlDbType.Int, 4, objProduct.ProductMaterial));
                    prams.Add(DB.MakeInParam("@BrandId", SqlDbType.Int, 4, objProduct.BrandId));
                    if (string.IsNullOrEmpty(objProduct.ProductField1))
                        prams.Add(DB.MakeInParam("@ProductField1", SqlDbType.NVarChar, 10, DBNull.Value));
                    else
                        prams.Add(DB.MakeInParam("@ProductField1", SqlDbType.NVarChar, 10, objProduct.ProductField1));
                    if (string.IsNullOrEmpty(objProduct.ProductField2))
                        prams.Add(DB.MakeInParam("@ProductField2", SqlDbType.NVarChar, 10, DBNull.Value));
                    else
                        prams.Add(DB.MakeInParam("@ProductField2", SqlDbType.NVarChar, 10, objProduct.ProductField2));
                    if (string.IsNullOrEmpty(objProduct.ProductField3))
                        prams.Add(DB.MakeInParam("@ProductField3", SqlDbType.NVarChar, 10, DBNull.Value));
                    else
                        prams.Add(DB.MakeInParam("@ProductField3", SqlDbType.NVarChar, 10, objProduct.ProductField3));

                    prams.Add(DB.MakeInParam("@LangaugeId", SqlDbType.Int, 4, objProduct.LangaugeId));
                    prams.Add(DB.MakeInParam("@image", SqlDbType.Image, -1, objProduct.Image));
                    prams.Add(DB.MakeInParam("@OrganizationId", SqlDbType.Int, 4, objProduct.OrganizationId));
                    prams.Add(DB.MakeInParam("@SerialNumber", SqlDbType.NVarChar, 200, objProduct.SerialNumber));
                    prams.Add(DB.MakeInParam("@ProductCatId", SqlDbType.Int, 4, objProduct.ProductCategoryId));
                    prams.Add(DB.MakeInParam("@ProductsubCatId", SqlDbType.Int, 4, objProduct.ProductSubCategory));
                    ProductId = DB.RunProc("up_Product_Insert", prams.ToArray());
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Product.InsertProduct", ex);
            }

            return ProductId;
        }

        public static DataSet GetAllProductsByLotId(int LotId)
        {
            DataSet ds = null;
            List<SqlParameter> prams = new List<SqlParameter>();

            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@LotId", SqlDbType.Int, 4, LotId));
                    ds = db.GetDataSet("up_GetProductsByLotId", prams.ToArray());
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Product.cs GetAllProductsByLotId", ex);
            }

            return ds;
        }

        public static DataSet GetAllProductsByLoadId(int LoadId)
        {
            DataSet ds = null;
            List<SqlParameter> prams = new List<SqlParameter>();

            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@LoadId", SqlDbType.Int, 4, LoadId));
                    ds = db.GetDataSet("up_GetProductsByLoadId", prams.ToArray());
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Product.cs GetAllProductsByLoadId", ex);
            }

            return ds;
        }

        public static DataSet GetProductBrandByBrandId(int BrandId)
        {
            DataSet ds = null;
            List<SqlParameter> prams = new List<SqlParameter>();

            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@BrandId", SqlDbType.Int, 4, BrandId));
                    ds = db.GetDataSet("[up_GetProductBrandByBrandId]", prams.ToArray());
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Products.cs GetProductBrandByBrandId", ex);
            }

            return ds;
        }

        public static int UpdateProductInventory(Product objProduct, int LotId)
        {
            int ProductId = 0;
            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@DateCreated", SqlDbType.DateTime, 8, objProduct.DateCreated));

                    prams.Add(db.MakeInParam("@ProductShape", SqlDbType.Int, 4, objProduct.ProductShape));
                    prams.Add(db.MakeInParam("@ProductSize", SqlDbType.Int, 4, objProduct.ProductSize));
                    prams.Add(db.MakeInParam("@ProductMaterial ", SqlDbType.Int, 4, objProduct.ProductMaterial));
                    if (string.IsNullOrEmpty(objProduct.ProductField1))
                        prams.Add(db.MakeInParam("@ProductField1", SqlDbType.NVarChar, 10, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@ProductField1", SqlDbType.NVarChar, 10, objProduct.ProductField1));
                    if (string.IsNullOrEmpty(objProduct.ProductField2))
                        prams.Add(db.MakeInParam("@ProductField2", SqlDbType.NVarChar, 10, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@ProductField2", SqlDbType.NVarChar, 10, objProduct.ProductField2));
                    if (string.IsNullOrEmpty(objProduct.ProductField3))
                        prams.Add(db.MakeInParam("@ProductField3", SqlDbType.NVarChar, 10, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@ProductField3", SqlDbType.NVarChar, 10, objProduct.ProductField3));
                    prams.Add(db.MakeInParam("@LangaugeId", SqlDbType.Int, 4, objProduct.LangaugeId));
                    prams.Add(db.MakeInParam("@ProductId", SqlDbType.Int, 4, objProduct.ProductId));
                    prams.Add(db.MakeInParam("@BrandId", SqlDbType.Int, 4, objProduct.BrandId));
                    prams.Add(db.MakeInParam("@LotId", SqlDbType.Int, 4, LotId));

                    ProductId = db.RunProc("up_Product_Update", prams.ToArray());
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Product.cs UpdateProductInventory", ex);
            }

            return ProductId;
        }

        public static int UpdateProductInventoryForRecieveTypeLoad(Product objProduct)
        {
            int ProductId = 0;
            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@SizeNumber", SqlDbType.NVarChar, 10, objProduct.SizeNumber));
                    prams.Add(db.MakeInParam("@ProductShape", SqlDbType.Int, 4, objProduct.ProductShape));
                    prams.Add(db.MakeInParam("@ProductSize", SqlDbType.Int, 4, objProduct.ProductSize));
                    prams.Add(db.MakeInParam("@ProductMaterial ", SqlDbType.Int, 4, objProduct.ProductMaterial));
                    if (string.IsNullOrEmpty(objProduct.ProductField1))
                        prams.Add(db.MakeInParam("@ProductField1", SqlDbType.NVarChar, 10, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@ProductField1", SqlDbType.NVarChar, 10, objProduct.ProductField1));
                    if (string.IsNullOrEmpty(objProduct.ProductField2))
                        prams.Add(db.MakeInParam("@ProductField2", SqlDbType.NVarChar, 10, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("@ProductField2", SqlDbType.NVarChar, 10, objProduct.ProductField2));
                    if (string.IsNullOrEmpty(objProduct.ProductField3))
                        prams.Add(db.MakeInParam("@ProductField3", SqlDbType.NVarChar, 10, DBNull.Value));
                    else
                        prams.Add(db.MakeInParam("", SqlDbType.NVarChar, 10, objProduct.ProductField3));
                    prams.Add(db.MakeInParam("@MonthCode", SqlDbType.NVarChar, 2, objProduct.WeekCode));
                    prams.Add(db.MakeInParam("@YearCode", SqlDbType.NVarChar, 4, objProduct.YearCode));
                    prams.Add(db.MakeInParam("@LangaugeId", SqlDbType.Int, 4, objProduct.LangaugeId));
                    prams.Add(db.MakeInParam("@ProductId", SqlDbType.Int, 4, objProduct.ProductId));
                    prams.Add(db.MakeInParam("@BrandId", SqlDbType.Int, 4, objProduct.BrandId));

                    ProductId = db.RunProc("up_Product_UpdateForLoadReceiving", prams.ToArray());
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Product.cs UpdateProductInventory", ex);
            }

            return ProductId;
        }

        public static DataSet GetProductDetailById(int productId)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@productId", SqlDbType.Int, 4, productId));
                    ds = db.GetDataSet("up_GetProductDetail", prams.ToArray());
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Product.cs GetProductDetailById", ex);
            }
            return ds;
        }

        public static bool UpdateProductById(int productId, string size, string shape, string material, string brand)
        {
            int id = 0;
            bool flag = false;
            List<SqlParameter> prams = new List<SqlParameter>();
            try
            {
                using (DbManager db = DbManager.GetDbManager())
                {
                    prams.Add(db.MakeInParam("@productId", SqlDbType.Int, 4, productId));
                    prams.Add(db.MakeInParam("@sizeValue", SqlDbType.VarChar, 20, size));
                    prams.Add(db.MakeInParam("@shapeValue", SqlDbType.VarChar, 20, shape));
                    prams.Add(db.MakeInParam("@materialValue", SqlDbType.VarChar, 20, material));
                    prams.Add(db.MakeInParam("@brandValue", SqlDbType.VarChar, 20, brand));
                    id = db.RunProc("up_UpdateProduct", prams.ToArray());
                    if (id == 1)
                        flag = true;
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Product.cs UpdateProductById", ex);
            }
            return flag;
        }

        public static DataSet getBarcodeByProductId(int productId)
        {
            DataSet ds = null;

            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {

                    prm.Add(DB.MakeInParam("@ProductId", SqlDbType.Int, 0, productId));
                    ds = DB.GetDataSet("up_getLotBarcodebyProductId", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Product.cs getBarcodeByProductId", ex);
            }
            return ds;
        }

        public static DataSet getAllProductsByLotId(int LotId, int CatId,int PageNo, int pageSize, out int count)
        {
            DataSet ds = null;

            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@intPageNo", SqlDbType.Int, 0, PageNo));
                    prm.Add(DB.MakeInParam("@intPageSize", SqlDbType.Int, 0, pageSize));
                    prm.Add(DB.MakeInParam("@intLotId", SqlDbType.Int, 0, LotId));
                    prm.Add(DB.MakeInParam("@CategoryId", SqlDbType.Int, 0, CatId));
                    prm.Add(DB.MakeReturnParam(SqlDbType.Int, 4));
                    ds = DB.GetDataSet("up_getAllProductsbyLotId", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        count = Conversion.ParseInt(prm.Last<SqlParameter>().Value);
                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Products.cs getAllProductsByLotId", ex);
            }
            count = 0;
            return ds;
        }

        public static DataSet getProductNameById(int catid)
        {
            DataSet ds = null;
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@CategoryId", SqlDbType.Int, 0, catid));
                    ds = DB.GetDataSet("Up_getProductNameById", prm.ToArray());
                }
            }
            catch (Exception ex)
            {

                new SqlLog().InsertSqlLog(0, "Products.cs getProductNameById", ex);
            }
            return ds;
        }

        public static DataSet GetProductsByLotIds(int PageSize, int PageNo, string LotIds, out int Count)
        {
            DataSet ds = null;
            Count = 0;

            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {

                    prm.Add(DB.MakeInParam("@intPageNo", SqlDbType.Int, 0, PageNo));
                    prm.Add(DB.MakeInParam("@intPageSize", SqlDbType.Int, 0, PageSize));
                    prm.Add(DB.MakeInParam("@LotIds", SqlDbType.VarChar, 0, LotIds));
                    prm.Add(DB.MakeReturnParam(SqlDbType.Int, 4));
                    ds = DB.GetDataSet("GetProductsByLotIds", prm.ToArray());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        Count = Conversion.ParseInt(prm.Last<SqlParameter>().Value);
                        return ds;
                    }

                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Product.cs GetProductsByLotIds", ex);
            }
            return ds;
        }

        public static DataSet getSizecodeForProductIds(string ProductIds)
        {
            DataSet ds = null;
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@ProductIdArray", SqlDbType.NVarChar, 500, ProductIds));

                    ds = DB.GetDataSet("GetProductCombination", prm.ToArray());
                }

            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Product.cs Get_SizeCodeForProductId", ex);
            }
            return ds;
        }

        public static DataSet GetProducts(int CatId)
        {
            DataSet ds = null;
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@LookupTypeID", SqlDbType.Int, 4, CatId));
                    ds = DB.GetDataSet("Get_productCategory_details", prm.ToArray());
                }

            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Product.cs GetProducts", ex);
            }
            return ds;
        }

        public static void InsertProductTypes(int OrgId, int CatId, string SubCatIds)
        {
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@OrganizationId", SqlDbType.Int, 0, OrgId));
                    prm.Add(DB.MakeInParam("@ProductCategoryid", SqlDbType.Int, 0, CatId));
                    prm.Add(DB.MakeInParam("@Productsubcategoryid", SqlDbType.VarChar, 250, SubCatIds));

                    int ReturnValue = DB.RunProc("Up_ProductSubCategory", prm.ToArray());
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Product.cs InsertProductTypes", ex);
            }
        }

        public static DataSet GetSelectedSubCategory(int OrgId, int catid)
        {
            DataSet ds = null;
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@OrgId", SqlDbType.Int, 0, OrgId));
                    prm.Add(DB.MakeInParam("@CatId", SqlDbType.Int, 0, catid));
                    ds = DB.GetDataSet("GET_ORGPRODUCTCATEGORY", prm.ToArray());
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Product.cs GetSelectedSubCategory", ex);
            }
            return ds;
        }

        public static int InsertBrand(string BrandName, int CountryId, int CatId)
        {
            List<SqlParameter> prams = new List<SqlParameter>();
            int ReturnValue = 0;

            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prams.Add(DB.MakeInParam("@CountryId", SqlDbType.Int, 4, CountryId));
                    prams.Add(DB.MakeInParam("@BrandName", SqlDbType.VarChar, 100, BrandName));
                    prams.Add(DB.MakeInParam("@CategoryId", SqlDbType.Int, 4, CatId));

                    ReturnValue = DB.RunProc("up_Brands_Insert", prams.ToArray());
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Product.InsertProduct", ex);
            }

            return ReturnValue;
        }

        public static DataSet GetAllSubCategories(int CatId)
        {
            DataSet ds = null;
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@CategoryId", SqlDbType.Int, 0, CatId));
                    ds = DB.GetDataSet("up_GetProductSubCategoryNames", prm.ToArray());
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Product.cs GetAllSubCategories", ex);
            }
            return ds;
        }

        public static DataSet GetProductSubTypeName(int ProductId, string SubTypeName)
        {
            DataSet ds = null;
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@ProductId", SqlDbType.Int, 0, ProductId));
                    prm.Add(DB.MakeInParam("@SubTypeName", SqlDbType.VarChar, 100, SubTypeName));
                    ds = DB.GetDataSet("GetProductSubTypeName", prm.ToArray());
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Product.cs GetProductSubTypeName", ex);
            }
            return ds;
        }

        public static int InsertUpdateSize(int LanguageId, string SizeName, int CatId, DateTime Date, int SubCatId, int Sizeid)
        {
            int result = 0;
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@LanguageId", SqlDbType.Int, 0, LanguageId));
                    prm.Add(DB.MakeInParam("@SizeName", SqlDbType.VarChar, 100, SizeName));
                    prm.Add(DB.MakeInParam("@CatId", SqlDbType.Int, 0, CatId));
                    if (SubCatId == 0)
                        prm.Add(DB.MakeInParam("@SubCatId", SqlDbType.Int, 0, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@SubCatId", SqlDbType.Int, 0, SubCatId));
                    prm.Add(DB.MakeInParam("@Date", SqlDbType.DateTime, 0, Date));
                    prm.Add(DB.MakeInParam("@Sizeid", SqlDbType.Int, 0, Sizeid));
                    result = DB.RunProc("Up_InsertUpdateSize", prm.ToArray());
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Product.cs InsertUpdateSize", ex);
            }
            return result;
        }

        public static int InsertUpdateShape(int LanguageId, string ShapeName, int CatId, DateTime Date, int SubCatId, int Shapeid)
        {
            int result = 0;
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@LanguageId", SqlDbType.Int, 0, LanguageId));
                    prm.Add(DB.MakeInParam("@ShapeName", SqlDbType.VarChar, 100, ShapeName));
                    prm.Add(DB.MakeInParam("@CatId", SqlDbType.Int, 0, CatId));
                    if (SubCatId == 0)
                        prm.Add(DB.MakeInParam("@SubCatId", SqlDbType.Int, 0, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@SubCatId", SqlDbType.Int, 0, SubCatId));
                    prm.Add(DB.MakeInParam("@Date", SqlDbType.DateTime, 0, Date));
                    prm.Add(DB.MakeInParam("@Shapeid", SqlDbType.Int, 0, Shapeid));
                    result = DB.RunProc("Up_InsertUpdateShape", prm.ToArray());
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Product.cs InsertUpdateShape", ex);
            }
            return result;
        }

        public static int InsertUpdateMaterial(int LanguageId, string MaterialName, int CatId, DateTime Date, int SubCatId, int Materialid)
        {
            int result = 0;
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@LanguageId", SqlDbType.Int, 0, LanguageId));
                    prm.Add(DB.MakeInParam("@MaterialName", SqlDbType.VarChar, 100, MaterialName));
                    prm.Add(DB.MakeInParam("@CatId", SqlDbType.Int, 0, CatId));
                    if (SubCatId == 0)
                        prm.Add(DB.MakeInParam("@SubCatId", SqlDbType.Int, 0, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@SubCatId", SqlDbType.Int, 0, SubCatId));
                    prm.Add(DB.MakeInParam("@Date", SqlDbType.DateTime, 0, Date));
                    prm.Add(DB.MakeInParam("@Materialid", SqlDbType.Int, 0, Materialid));
                    result = DB.RunProc("Up_InsertUpdateMaterial", prm.ToArray());
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Product.cs InsertUpdateMaterial", ex);
            }
            return result;
        }

        public static int InsertUpdateBrand(int LanguageId, string BrandName, int CatId, DateTime Date, int SubCatId, int Brandid)
        {
            int result = 0;
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@LanguageId", SqlDbType.Int, 0, LanguageId));
                    prm.Add(DB.MakeInParam("@BrandName", SqlDbType.VarChar, 100, BrandName));
                    prm.Add(DB.MakeInParam("@CatId", SqlDbType.Int, 0, CatId));
                    if (SubCatId == 0)
                        prm.Add(DB.MakeInParam("@SubCatId", SqlDbType.Int, 0, DBNull.Value));
                    else
                        prm.Add(DB.MakeInParam("@SubCatId", SqlDbType.Int, 0, SubCatId));
                    prm.Add(DB.MakeInParam("@Date", SqlDbType.DateTime, 0, Date));
                    prm.Add(DB.MakeInParam("@Brandid", SqlDbType.Int, 0, Brandid));
                    result = DB.RunProc("Up_InsertUpdateBrand", prm.ToArray());
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Product.cs InsertUpdateBrand", ex);
            }
            return result;
        }

        public static int UpdateOrgProductCategory(int OrgId, string CatIds)
        {
            int result = 0;
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@OrgId", SqlDbType.Int, 0, OrgId));
                    prm.Add(DB.MakeInParam("@CatIds", SqlDbType.VarChar, 250, CatIds));

                    result = DB.RunProc("Up_UpdateOrgProductCategory", prm.ToArray());
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Product.cs UpdateOrgProductCategory", ex);
            }
            return result;
        }

        public static void ApproveProductCategory(int CategoryId)
        {
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {
                    prm.Add(DB.MakeInParam("@CategoryId", SqlDbType.Int, 0, CategoryId));
                    

                    DB.RunProc("up_ApproveProductCategory", prm.ToArray());
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Product.cs ApproveProductCategory", ex);
            }
        }

        public static object GetProductsForApproval()
        {
            DataSet ds = null;
            List<SqlParameter> prm = new List<SqlParameter>();
            try
            {
                using (DbManager DB = DbManager.GetDbManager())
                {

                    ds = DB.GetDataSet("GetProductsForApproval");
                }
            }
            catch (Exception ex)
            {
                new SqlLog().InsertSqlLog(0, "Product.cs GetProductsForApproval", ex);
            }
            return ds;
        }

        #endregion




       
    }
}
