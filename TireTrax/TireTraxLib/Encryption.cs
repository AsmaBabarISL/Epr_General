using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Configuration;

namespace TireTraxLib
{
    public class Encryption
    {

        static private Byte[] m_Key = new Byte[8];
        static private Byte[] m_IV = new Byte[8];


        public Encryption()
        { }

        /// <summary>
        /// Author:
        /// Description: Encrypt the given string.
        /// </summary>
        /// <param name="strOriginal">Data to be encrypted</param>
        /// <returns>Returns encrypted value</returns>
        public static string Encrypt(string strOriginal)
        {
            // Return in case of an empty string...
            if (strOriginal.Trim().Equals(string.Empty)) return string.Empty;
            string strResult = "";		// Return result
            string strKey = System.Configuration.ConfigurationManager.AppSettings["encryptKey"];

            // 1. String length cannot exceed 90Kb. Otherwise,
            //    buffer will overflow.  See point 3 for reasons
            if (strOriginal.Length > 92160)
            {
                throw new Exception("Data String too large. Keep within 90Kb.");
            }

            // 2. Generate the Keys
            if (!InitKey(strKey))
            {
                throw new Exception("Unable to generate key for encryption.");
            }

            // 3. Prepare the String
            //	The first 5 character of the string is formatted to store the
            //	actual length of the data.
            //	This is the simplest way to remember to original length of the
            //	data, without resorting to complicated computations.
            //	If anyone figure a good way to 'remember' the original length
            //	to facilite the decryption without having to use additional
            //	function parameters, pls let me know.
            strOriginal = String.Format("{0,5:00000}" + strOriginal, strOriginal.Length);

            // 4. Encrypt the Data
            byte[] rbData = new byte[strOriginal.Length];
            ASCIIEncoding aEnc = new ASCIIEncoding();
            aEnc.GetBytes(strOriginal, 0, strOriginal.Length, rbData, 0);

            DESCryptoServiceProvider descsp = new DESCryptoServiceProvider();
            ICryptoTransform desEncrypt = descsp.CreateEncryptor(m_Key, m_IV);

            // 5. Perpare the streams
            //	mOut is the output stream.
            //	mStream is the input stream.
            //	cs is the transformation stream.
            MemoryStream mStream = new MemoryStream(rbData);
            CryptoStream cs = new CryptoStream(mStream, desEncrypt,
                CryptoStreamMode.Read);
            MemoryStream mOut = new MemoryStream();

            try
            {
                // 6. Start performing the encryption
                int bytesRead;
                byte[] output = new byte[1024];

                do
                {
                    bytesRead = cs.Read(output, 0, 1024);
                    if (bytesRead != 0)
                        mOut.Write(output, 0, bytesRead);
                } while (bytesRead > 0);

                // 7. Returns the encrypted result after it is base64 encoded
                //	In this case, the actual result is converted to base64 so that
                //	it can be transported over the HTTP protocol without deformation.
                if (mOut.Length > 0)
                    strResult = Convert.ToBase64String(mOut.GetBuffer(), 0,
                        (int)mOut.Length);
            }
            catch (CryptographicException ce)
            {
                new SqlLog().InsertSqlLog(0, "Encryption.Encrypt", ce);
                strResult = ce.ToString();
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "Encryption.Encrypt", e);
                strResult = e.ToString();
            }
            finally
            {
                mOut.Close();
                cs.Close();
                mStream.Close();
            }
            return strResult;
        }

        /// <summary>
        /// Author:
        /// Description: Decrypt the given string.
        /// </summary>
        /// <param name="strEncrypted">Data to be decrypted</param>
        /// <returns>Returns decrypted value</returns>
        public static string Decrypt(string strEncrypted)
        {
            strEncrypted = strEncrypted.Replace(" ", "+");
            // Return in case of an empty string...
            if (strEncrypted.Trim().Equals(string.Empty)) return string.Empty;
            string strResult;
            string strKey = ConfigurationSettings.AppSettings["encryptKey"];

            // 1. Generate the Key used for decrypting
            if (!InitKey(strKey))
            {
                throw new Exception("Unable to generate key for decryption.");
            }

            // 2. Initialize the service provider
            DESCryptoServiceProvider descsp = new DESCryptoServiceProvider();
            ICryptoTransform desDecrypt = descsp.CreateDecryptor(m_Key, m_IV);

            // 3. Prepare the streams
            //	mOut is the output stream.
            //	cs is the transformation stream.
            MemoryStream mOut = new MemoryStream();
            CryptoStream cs = new CryptoStream(mOut, desDecrypt,
                CryptoStreamMode.Write);

            try
            {
                // 4. Remember to revert the base64 encoding into a byte array to
                //    restore the original encrypted data stream
                byte[] bPlain = Convert.FromBase64CharArray(strEncrypted.ToCharArray(),
                    0, strEncrypted.Length);

                // 5. Perform the actual decryption
                cs.Write(bPlain, 0, (int)bPlain.Length);
                cs.FlushFinalBlock();

                ASCIIEncoding aEnc = new ASCIIEncoding();
                strResult = aEnc.GetString(mOut.GetBuffer(), 0, (int)mOut.Length);

                // 6. Trim the string to return only the meaningful data
                //	Remember that in the encrypt function, the first 5 character
                //	holds the length of the actual data
                //	This is the simplest way to remember to original length of the
                //	data, without resorting to complicated computations.
                string strLen = strResult.Substring(0, 5);
                strResult = strResult.Substring(5, Convert.ToInt32(strLen));
            }
            catch (CryptographicException ce)
            {
                new SqlLog().InsertSqlLog(0, "Encryption.Decrypt", ce);
                strResult = ce.ToString();
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "Encryption.Decrypt", e);

                strResult = "Error occured";
            }
            finally
            {
                try
                {
                    cs.Close();
                    mOut.Close();
                }
                catch { }
            }
            return strResult;
        }

        /// <summary>
        /// Author:
        /// Description: Private function to generate the keys into member
        /// variables.
        /// </summary>
        /// <param name="strKey"></param>
        /// <returns></returns>
        static private bool InitKey(string strKey)
        {
            try
            {
                // Convert Key to byte array
                byte[] bp = new byte[strKey.Length];
                ASCIIEncoding aEnc = new ASCIIEncoding();
                aEnc.GetBytes(strKey, 0, strKey.Length, bp, 0);

                // Hash the key using SHA1
                SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
                byte[] bpHash = sha.ComputeHash(bp);

                int lcv;
                // Use the low 64-bits for the key value
                for (lcv = 0; lcv < 8; lcv++)
                    m_Key[lcv] = bpHash[lcv];
                for (lcv = 8; lcv < 16; lcv++)
                    m_IV[lcv - 8] = bpHash[lcv];
                return true;
            }
            catch (Exception e)
            {
                new SqlLog().InsertSqlLog(0, "Encryption.InitKey", e);
                // Error Performing Operations
                return false;
            }
        }

    }
}
