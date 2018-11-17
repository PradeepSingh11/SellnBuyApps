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
    public class ContactUs
    {
        private string strName;
        private string strEmailId;
        private string strSubject;
        private string strComments;

        [Required(ErrorMessage ="Please enter your name")]
        public string Name
        {
            get
            {
                return strName;
            }
            set
            {
                strName = value;
            }
        }

        [DisplayName("Email Id")]
        [Required(ErrorMessage = "Please enter your email")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
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

        [DisplayName("Subject")]
        [Required(ErrorMessage = "Please enter Subject")]
        public string Subject
        {
            get
            {
                return strSubject;
            }
            set
            {
                strSubject = value;
            }
        }

        [DisplayName("Message")]
        [Required(ErrorMessage = "Please enter your Message")]
        public string Comments
        {
            get
            {
                return strComments;
            }
            set
            {
                strComments = value;
            }
        }
        /// <summary>
        /// add contactus in database
        /// </summary>
        /// <returns></returns>
        public Boolean AddSb_ContactUs()
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] arrParameters = new System.Data.SqlClient.SqlParameter[]
                {

     new System.Data.SqlClient.SqlParameter("@Name",  System.Data.DbType.String),
     new System.Data.SqlClient.SqlParameter("@EmailId",  System.Data.DbType.String),
     new System.Data.SqlClient.SqlParameter("@Subject",  System.Data.DbType.String),
     new System.Data.SqlClient.SqlParameter("@Comments",  System.Data.DbType.String),

                };

                arrParameters[0].Value = Name;
                arrParameters[1].Value = EmailId;
                arrParameters[2].Value = Subject;
                arrParameters[3].Value = Comments;

                string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"].ToString();
                int i = SqlHelper.ExecuteNonQuery(sqlConnectionString, System.Data.CommandType.StoredProcedure, "Sb_ContactUs_AddRecord", arrParameters);
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