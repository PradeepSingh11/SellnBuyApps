using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace sellnbuyapps.Models
{
    public class sbForgotPassword
    {
        private int intId;
        private string strEmailId;
        public string ResetPasswordCode
        {
            get;set;
        }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please enter Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]*\\.([a-z]{2,4})$", ErrorMessage = "Please enter a valid e-mail adress")]
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
        public int Id
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
        public DataSet GetEmailId_Sb_User()
        {
            try
            {

                System.Data.SqlClient.SqlParameter[] arrParameters = new System.Data.SqlClient.SqlParameter[]
                {
               new System.Data.SqlClient.SqlParameter("@EmailId",  System.Data.DbType.String),
                };
                arrParameters[0].Value = EmailId;
                string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"].ToString();
                DataSet ds = SqlHelper.ExecuteDataset(sqlConnectionString, System.Data.CommandType.StoredProcedure, "Sb_User_GetEmailId", arrParameters);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetEmailIdBasedOnResetCode()
        {
            System.Data.SqlClient.SqlParameter[] arrParameters = new System.Data.SqlClient.SqlParameter[]
               {
                    new System.Data.SqlClient.SqlParameter("@ResetCode",  System.Data.DbType.String),
               };

            arrParameters[0].Value = ResetPasswordCode;

            string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"].ToString();
            DataSet ds = SqlHelper.ExecuteDataset(sqlConnectionString, System.Data.CommandType.StoredProcedure, "Sb_GetEmailIdBasedOnResetCode", arrParameters);
            return ds;
        }

        public Boolean AddResetCode()
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] arrParameters = new System.Data.SqlClient.SqlParameter[]
                {

new System.Data.SqlClient.SqlParameter("@ResetCode",  System.Data.DbType.String),
new System.Data.SqlClient.SqlParameter("@EmailId",  System.Data.DbType.String),
                };

                arrParameters[0].Value = ResetPasswordCode;
                arrParameters[1].Value = EmailId;

                string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"].ToString();
                int i = SqlHelper.ExecuteNonQuery(sqlConnectionString, System.Data.CommandType.StoredProcedure, "Sb_User_AddResetCode", arrParameters);
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