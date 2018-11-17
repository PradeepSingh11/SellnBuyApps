using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace sellnbuyapps.Models
{
    public class Login
    {
        private int intID;
        private string strEmailId;
        private string strOldPassword;
        private string strNewPassword;
        private string strPassword;

        public int ID
        {
            get
            {
                return intID;
            }
            set
            {
                intID = value;
            }
        }

        [DisplayName("Email Id")]
        [EmailAddress(ErrorMessage = "Please enter valid Email")]
        [Required(ErrorMessage = "Please enter email id")]
        public string EmailId
        {
            get
            {
                return strEmailId;
            }
            set
            {
                strEmailId = value;
            }
        }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter password")]
        public string Password
        {
            get
            {
                return strPassword;
            }
            set
            {
                strPassword = value;
            }
        }

        [DataType(DataType.Password)]
        [DisplayName("Old Password")]
        //[StringLength(15, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 15 digits")]
        public string OldPassword
        {
            get
            {
                return strOldPassword;
            }
            set
            {
                strOldPassword = value;
            }
        }
        [DataType(DataType.Password)]
        [DisplayName("New Password")]
        //[StringLength(15, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 15 digits")]
        public string NewPassword
        {
            get
            {
                return strNewPassword;
            }
            set
            {
                strNewPassword = value;
            }
        }
        public DataSet GetEmailId_Sb_User()
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] arrParameters = new System.Data.SqlClient.SqlParameter[]
              {
        new System.Data.SqlClient.SqlParameter("@EmailId",  System.Data.DbType.String),
        new System.Data.SqlClient.SqlParameter("@Password",  System.Data.DbType.String),

              };

                arrParameters[0].Value = EmailId;
                arrParameters[1].Value = OldPassword;

                string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"].ToString();
                DataSet ds = SqlHelper.ExecuteDataset(sqlConnectionString, System.Data.CommandType.StoredProcedure, "Sb_User_IfExistEmail_ChangePassword", arrParameters);
                return ds;
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
new System.Data.SqlClient.SqlParameter("@EmailId",  System.Data.DbType.String),
new System.Data.SqlClient.SqlParameter("@Password",  System.Data.DbType.String),

                };

                arrParameters[0].Value = EmailId;
                arrParameters[1].Value = NewPassword;

                string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"].ToString();
                int i = SqlHelper.ExecuteNonQuery(sqlConnectionString, System.Data.CommandType.StoredProcedure, "Sb_User_ChangePassword", arrParameters);
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
                System.Data.SqlClient.SqlParameter[] arrParameters = new System.Data.SqlClient.SqlParameter[]
               {
new System.Data.SqlClient.SqlParameter("@Email",  System.Data.DbType.String),
new System.Data.SqlClient.SqlParameter("@Password",  System.Data.DbType.String),
               };

                arrParameters[0].Value = EmailId;
                arrParameters[1].Value = Password;

                string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"].ToString();
                DataSet ds = SqlHelper.ExecuteDataset(sqlConnectionString, System.Data.CommandType.StoredProcedure, "sp_User_LoginDetails", arrParameters);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}