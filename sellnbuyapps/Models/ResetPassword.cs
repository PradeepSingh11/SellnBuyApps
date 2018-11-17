using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Web;

namespace sellnbuyapps.Models
{
    public class ResetPassword
    {
        private string intId;
        private string strNewPassword;
        private string strConfirmPassword;
        private string strEmailId;


        [Required]
        public string ResetCode { get; set; }
        public string Id
        {
            get
            {
                return intId;
            }
            set
            {
                intId = value;
            }
        }

        [Required(ErrorMessage = "New password required", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [DisplayName("New Password")]
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

        [DataType(DataType.Password)]
        [DisplayName("Confirm Password")]
        [Compare("NewPassword", ErrorMessage = "New password and confirm password does not match")]
        public string ConfirmPassword
        {
            get
            {
                return strConfirmPassword;
            }
            set
            {
                strConfirmPassword = value;
            }
        }

        [Required]
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
        public Boolean UpdatePasswordSb_User()
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
    }
}