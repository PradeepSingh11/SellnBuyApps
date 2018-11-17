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
    public class sbAdministrator
    {
        private int id;
        private string strFirstName;
        private string strLastName;
        private string strEmailId;
        private string strPassword;
        private string strMobileNumber;
        private string strCompanyURL;
        private bool blStatus = true;
        private string dtModifiedOn;

        public int ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        public string ProfilePicture { get; set; }

        [Required(ErrorMessage = "Please enter first name")]
        [DisplayName("First Name")]
        public string FirstName
        {
            get
            {
                return strFirstName;
            }
            set
            {
                strFirstName = value;
            }
        }
        [Required(ErrorMessage = "Please enter last name")]
        [DisplayName("First Name")]
        public string LastName
        {
            get
            {
                return strLastName;
            }
            set
            {
                strLastName = value;
            }
        }

        [DisplayName("Email Id")]
        [Required(ErrorMessage = "Please enter email Id")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
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

        public string MobileNumber
        {
            get
            {
                return strMobileNumber;
            }
            set
            {
                strMobileNumber = value;
            }
        }
        public string CompanyURL
        {
            get
            {
                return strCompanyURL;
            }
            set
            {
                strCompanyURL = value;
            }
        }
        public Boolean Status
        {
            get
            {
                return blStatus;
            }
            set
            {
                blStatus = value;
            }
        }
        public string ModifiedOn
        {
            get
            {
                return dtModifiedOn;
            }
            set
            {
                dtModifiedOn = value;
            }
        }

        public string Query { get; set; }

        [DisplayName("New Password")]
        [Required(ErrorMessage = "Please enter new password")]
        public string NewPassword { get; set; }

        public List<sbAdministrator> pds { get; set; }

        public List<sbAdministrator> AllUsersBind()
        {
            string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"].ToString();
            DataSet ds = SqlHelper.ExecuteDataset(sqlConnectionString, System.Data.CommandType.StoredProcedure, "Sb_User_GetData");
            List<sbAdministrator> lst = new List<sbAdministrator>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    lst.Add(new sbAdministrator
                    {
                        ID = Convert.ToInt32(item["Id"]),
                        FirstName = Convert.ToString(item["FirstName"]),
                        LastName = Convert.ToString(item["LastName"]),
                        EmailId = Convert.ToString(item["EmailId"]),
                        MobileNumber = Convert.ToString(item["MobileNumber"]),
                        CompanyURL = Convert.ToString(item["CompanyURL"]),
                        ModifiedOn = Convert.ToString(item["ModifiedOn"]),
                        ProfilePicture = Convert.ToString(item["ProfilePicture"]),
                    });
                }
            }
            return lst;
        }

      
        public List<sbAdministrator> SearchAllUsersBind()
        {
            string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"].ToString();
            System.Data.SqlClient.SqlParameter[] arrParameters = new System.Data.SqlClient.SqlParameter[]
            {

new System.Data.SqlClient.SqlParameter("@Query",  System.Data.DbType.String),
            };
            arrParameters[0].Value = Query;
            DataSet ds = SqlHelper.ExecuteDataset(sqlConnectionString, System.Data.CommandType.StoredProcedure, "Sb_User_SearchGetData",arrParameters);
            List<sbAdministrator> lst = new List<sbAdministrator>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    lst.Add(new sbAdministrator
                    {
                        ID = Convert.ToInt32(item["Id"]),
                        FirstName = Convert.ToString(item["FirstName"]),
                        LastName = Convert.ToString(item["LastName"]),
                        EmailId = Convert.ToString(item["EmailId"]),
                        MobileNumber = Convert.ToString(item["MobileNumber"]),
                        CompanyURL = Convert.ToString(item["CompanyURL"]),
                        ModifiedOn = Convert.ToString(item["ModifiedOn"]),
                        ProfilePicture = Convert.ToString(item["ProfilePicture"]),
                    });
                }
            }
            return lst;
        }
        public DataSet Sb_Login()
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
                DataSet ds = SqlHelper.ExecuteDataset(sqlConnectionString, System.Data.CommandType.StoredProcedure, "sp_Admin_Login", arrParameters);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetData_Sb_AllUsers()
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

                System.Data.SqlClient.SqlParameter[] arrParameters = new System.Data.SqlClient.SqlParameter[]
                {
new System.Data.SqlClient.SqlParameter("@ID",  System.Data.DbType.Int32)
                };
                arrParameters[0].Value = ID;
                string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"].ToString();
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
                int i = SqlHelper.ExecuteNonQuery(sqlConnectionString, System.Data.CommandType.StoredProcedure, "Sb_Admin_ChangePassword", arrParameters);
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

        public int countTotalUser()
        {
            string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"].ToString();
            int totalUsers = Convert.ToInt32(SqlHelper.ExecuteScalar(sqlConnectionString, System.Data.CommandType.StoredProcedure, "sp_countTotalUser"));
            return totalUsers;
        }

        public int countTotalProjects()
        {
            string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"].ToString();
            int totalProjects = Convert.ToInt32(SqlHelper.ExecuteScalar(sqlConnectionString, System.Data.CommandType.StoredProcedure, "sp_countTotalProjects"));
            return totalProjects;
        }
    }
}