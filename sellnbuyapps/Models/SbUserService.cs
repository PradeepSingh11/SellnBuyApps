using System;
using System.Data;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Security.Cryptography;

namespace sellnbuyapps.Models
{
    public class SbUserService
    {
        private Boolean status;
        public int ID { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("Email Id")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailId { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Mobile Number")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string MobileNumber { get; set; }

        [DisplayName("Company Url")]
        public string CompanyURL { get; set; }

        [DisplayName("Profile Picture")]
        public string ProfilePicture { get; set; }
        public Boolean Status
        {
            get
            {
                return status;
            }
            set
            {
                status = true;
            }
        }
        public string ModifiedOn { get; set; }

        public Boolean AddSb_User()
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] arrParameters = new System.Data.SqlClient.SqlParameter[]
                {

new System.Data.SqlClient.SqlParameter("@FirstName",  System.Data.DbType.String),
new System.Data.SqlClient.SqlParameter("@LastName",  System.Data.DbType.String),
new System.Data.SqlClient.SqlParameter("@EmailId",  System.Data.DbType.String),
new System.Data.SqlClient.SqlParameter("@Password",  System.Data.DbType.String),
new System.Data.SqlClient.SqlParameter("@MobileNumber",  System.Data.DbType.String),
new System.Data.SqlClient.SqlParameter("@ProfilePicture",  System.Data.DbType.String),
new System.Data.SqlClient.SqlParameter("@CompanyURL",  System.Data.DbType.String),
new System.Data.SqlClient.SqlParameter("@Status",  System.Data.DbType.Boolean),

                };

                arrParameters[0].Value = FirstName;
                arrParameters[1].Value = LastName;
                arrParameters[2].Value = EmailId;
                arrParameters[3].Value = Password;
                arrParameters[4].Value = MobileNumber;
                arrParameters[5].Value = ProfilePicture;
                arrParameters[6].Value = CompanyURL;
                arrParameters[7].Value = Status;

                string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"].ToString();
                int i = SqlHelper.ExecuteNonQuery(sqlConnectionString, System.Data.CommandType.StoredProcedure, "Sb_User_AddRecord", arrParameters);
                if (i >= 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Boolean UpdateSb_User()
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] arrParameters = new System.Data.SqlClient.SqlParameter[]
                {

new System.Data.SqlClient.SqlParameter("@FirstName",  System.Data.DbType.String),
new System.Data.SqlClient.SqlParameter("@LastName",  System.Data.DbType.String),
new System.Data.SqlClient.SqlParameter("@EmailId",  System.Data.DbType.String),
new System.Data.SqlClient.SqlParameter("@MobileNumber",  System.Data.DbType.String),
new System.Data.SqlClient.SqlParameter("@ProfilePicture",  System.Data.DbType.String),
new System.Data.SqlClient.SqlParameter("@CompanyURL",  System.Data.DbType.String),
new System.Data.SqlClient.SqlParameter("@Status",  System.Data.DbType.Boolean),
new System.Data.SqlClient.SqlParameter("@Id",  System.Data.DbType.Int32),

                };
                arrParameters[0].Value = FirstName;
                arrParameters[1].Value = LastName;
                arrParameters[2].Value = EmailId;
                arrParameters[3].Value = MobileNumber;
                arrParameters[4].Value = ProfilePicture;
                arrParameters[5].Value = CompanyURL;
                arrParameters[6].Value = Status;
                arrParameters[7].Value = ID;

                string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"].ToString();
                int i = SqlHelper.ExecuteNonQuery(sqlConnectionString, System.Data.CommandType.StoredProcedure, "Sb_User_UpdateRecord", arrParameters);
                if (i >= 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetData_Sb_User()
        {
            try
            {
                string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"].ToString();
                DataSet ds = SqlHelper.ExecuteDataset(sqlConnectionString, System.Data.CommandType.StoredProcedure, "Sb_User_GetData");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetByID_Sb_User()
        {
            try
            {
                string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"].ToString();
                System.Data.SqlClient.SqlParameter[] arrParameters = new System.Data.SqlClient.SqlParameter[]
                {
new System.Data.SqlClient.SqlParameter("@ID",  System.Data.DbType.Int32)
                };
                arrParameters[0].Value = ID;

                DataSet ds = SqlHelper.ExecuteDataset(sqlConnectionString, System.Data.CommandType.StoredProcedure, "Sb_User_GetDataById", arrParameters);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Boolean DeleteByID_Sb_User()
        {
            try
            {

                System.Data.SqlClient.SqlParameter[] arrParameters = new System.Data.SqlClient.SqlParameter[]
                {
new System.Data.SqlClient.SqlParameter("@ID",  System.Data.DbType.Int32)
                };
                arrParameters[0].Value = ID;
                string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"].ToString();
                int i = SqlHelper.ExecuteNonQuery(sqlConnectionString, System.Data.CommandType.StoredProcedure, "Sb_User_DeleteById", arrParameters);
                if (i >= 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Boolean IsEmailExists()
        {
            try
            {
                bool IsExist = false;
                System.Data.SqlClient.SqlParameter[] arrParameters = new System.Data.SqlClient.SqlParameter[]
               {

        new System.Data.SqlClient.SqlParameter("@ID",  System.Data.DbType.Int32),
        new System.Data.SqlClient.SqlParameter("@EmailId",  System.Data.DbType.String),

               };

                arrParameters[0].Value = ID;
                arrParameters[1].Value = EmailId;

                string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"].ToString();
                IsExist = Convert.ToBoolean(SqlHelper.ExecuteScalar(sqlConnectionString, System.Data.CommandType.StoredProcedure, "sb_User_IsEmailExists", arrParameters));
                return IsExist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetEncryptedValue(string encryptpassword)
        {
            string str2 = "";
            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
            UTF8Encoding encoding = new UTF8Encoding();
            foreach (byte num in provider.ComputeHash(encoding.GetBytes(encryptpassword)))
            {
                str2 = str2 + num.ToString();
            }
            return str2;
        }
    }
}