using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace sellnbuyapps.Models
{
    public class Home
    {
        public int ProjectCategoryId { get; set; }
        public int Quantity { get; set; }
        public int Id { get; set; }

        public string screenshot { get; set; }

        public string projectname { get; set; }

        public string features { get; set; }

        public string technologyname { get; set; }

        public int comments { get; set; }

        public string rating { get; set; }

        public decimal cost { get; set; }

        public string description { get; set; }

        public string authorfirstname { get; set; }
        public string authorlastname { get; set; }
        public string profilepicture { get; set; }

        public string postcomment { get; set; }

        public int createdby { get; set; }

        public string Query { get; set; }

       public List<Home> pds { get; set; }

        public DataSet GetData_Sb_ProjectDetails()
        {
            try
            {
                string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"].ToString();
                DataSet ds = SqlHelper.ExecuteDataset(sqlConnectionString, System.Data.CommandType.StoredProcedure, "Sb_ProjectDetails_forUsers");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Home> ProjectListBind()
        {
            string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"].ToString();
            DataSet ds = SqlHelper.ExecuteDataset(sqlConnectionString, System.Data.CommandType.StoredProcedure, "Sb_ProjectDetails_forUsers");
            List<Home> lst = new List<Home>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    lst.Add(new Home
                    {
                        Id = Convert.ToInt32(item["Id"]),
                        // ProjectCategory = Convert.ToString(item["CategoryName"]),
                        projectname = Convert.ToString(item["ProjectName"]),
                        screenshot = Convert.ToString(item["Screenshot"]),
                        features = Convert.ToString(item["Features"]),
                        technologyname = Convert.ToString(item["TechnologyName"]),
                        cost = Convert.ToInt32(item["Cost"]),
                        authorfirstname = Convert.ToString(item["FirstName"]),
                        authorlastname = Convert.ToString(item["LastName"]),
                        profilepicture = Convert.ToString(item["ProfilePicture"]),
                        description = Convert.ToString(item["Description"]),
                    });
                }

            }
            return lst;
        }

        public List<Home> SearchProjectListBind()
        {
            string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"].ToString();
            System.Data.SqlClient.SqlParameter[] arrParameters = new System.Data.SqlClient.SqlParameter[]
               {

new System.Data.SqlClient.SqlParameter("@Query",  System.Data.DbType.String),
               };
            arrParameters[0].Value = Query;

            DataSet ds = SqlHelper.ExecuteDataset(sqlConnectionString, System.Data.CommandType.StoredProcedure, "Sb_SearchProjectDetails_forUsers", arrParameters);
            List<Home> lst = new List<Home>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    lst.Add(new Home
                    {
                        Id = Convert.ToInt32(item["Id"]),
                        // ProjectCategory = Convert.ToString(item["CategoryName"]),
                        projectname = Convert.ToString(item["ProjectName"]),
                        screenshot = Convert.ToString(item["Screenshot"]),
                        features = Convert.ToString(item["Features"]),
                        technologyname = Convert.ToString(item["TechnologyName"]),
                        cost = Convert.ToInt32(item["Cost"]),
                        authorfirstname = Convert.ToString(item["FirstName"]),
                        authorlastname = Convert.ToString(item["LastName"]),
                        profilepicture = Convert.ToString(item["ProfilePicture"]),
                    });
                }
            }
            return lst;
        }

        public List<Home> SearchProjectsBasedOnCategory()
        {
            string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"].ToString();
            System.Data.SqlClient.SqlParameter[] arrParameters = new System.Data.SqlClient.SqlParameter[]
               {

new System.Data.SqlClient.SqlParameter("@Query",  System.Data.DbType.String),
               };
            arrParameters[0].Value = Query;

            DataSet ds = SqlHelper.ExecuteDataset(sqlConnectionString, System.Data.CommandType.StoredProcedure, "sp_CategorySearchForHome", arrParameters);
            List<Home> lst = new List<Home>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    lst.Add(new Home
                    {
                        Id = Convert.ToInt32(item["Id"]),
                        // ProjectCategory = Convert.ToString(item["CategoryName"]),
                        projectname = Convert.ToString(item["ProjectName"]),
                        screenshot = Convert.ToString(item["Screenshot"]),
                        features = Convert.ToString(item["Features"]),
                        technologyname = Convert.ToString(item["TechnologyName"]),
                        cost = Convert.ToInt32(item["Cost"]),
                        authorfirstname = Convert.ToString(item["FirstName"]),
                        authorlastname = Convert.ToString(item["LastName"]),
                        profilepicture = Convert.ToString(item["ProfilePicture"]),
                    });
                }
            }
            return lst;
        }

        public List<Home> SearchProjectsBasedOnTechnology()
        {
            string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"].ToString();
            System.Data.SqlClient.SqlParameter[] arrParameters = new System.Data.SqlClient.SqlParameter[]
               {

new System.Data.SqlClient.SqlParameter("@Query",  System.Data.DbType.String),
               };
            arrParameters[0].Value = Query;

            DataSet ds = SqlHelper.ExecuteDataset(sqlConnectionString, System.Data.CommandType.StoredProcedure, "sp_TechnologySearchForHome", arrParameters);
            List<Home> lst = new List<Home>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    lst.Add(new Home
                    {
                        Id = Convert.ToInt32(item["Id"]),
                        // ProjectCategory = Convert.ToString(item["CategoryName"]),
                        projectname = Convert.ToString(item["ProjectName"]),
                        screenshot = Convert.ToString(item["Screenshot"]),
                        features = Convert.ToString(item["Features"]),
                        technologyname = Convert.ToString(item["TechnologyName"]),
                        cost = Convert.ToInt32(item["Cost"]),
                        authorfirstname = Convert.ToString(item["FirstName"]),
                        authorlastname = Convert.ToString(item["LastName"]),
                        profilepicture = Convert.ToString(item["ProfilePicture"]),
                    });
                }
            }
            return lst;
        }
        public DataSet GetByProductID_SingleProjectDetails()
        {
            try
            {
                string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"].ToString();

                System.Data.SqlClient.SqlParameter[] arrParameters = new System.Data.SqlClient.SqlParameter[]
                {
new System.Data.SqlClient.SqlParameter("@Id",  System.Data.DbType.Int32)
                };
                arrParameters[0].Value = Id;

                DataSet ds = SqlHelper.ExecuteDataset(sqlConnectionString, System.Data.CommandType.StoredProcedure, "Sb_ProjectDetails_GetSingleProductDataByProductId", arrParameters);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Home> getprojectbyProjectId()
        {
            string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"].ToString();

            System.Data.SqlClient.SqlParameter[] arrParameters = new System.Data.SqlClient.SqlParameter[]
            {
new System.Data.SqlClient.SqlParameter("@Id",  System.Data.DbType.Int32)
            };
            arrParameters[0].Value = Id;

            DataSet ds = SqlHelper.ExecuteDataset(sqlConnectionString, System.Data.CommandType.StoredProcedure, "Sb_ProjectDetails_GetSingleProductDataByProductId", arrParameters);
            List<Home> lst = new List<Home>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    lst.Add(new Home
                    {
                        Id = Convert.ToInt32(item["Id"]),
                        // ProjectCategory = Convert.ToString(item["CategoryName"]),
                        projectname = Convert.ToString(item["ProjectName"]),
                        screenshot = Convert.ToString(item["Screenshot"]),
                        features = Convert.ToString(item["Features"]),
                        technologyname = Convert.ToString(item["TechnologyName"]),
                        cost = Convert.ToInt32(item["Cost"]),
                    });
                }
            }
            return lst;
        }
        public Boolean Add_Comment()
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] arrParameters = new System.Data.SqlClient.SqlParameter[]
                {

new System.Data.SqlClient.SqlParameter("@ProjectId",  System.Data.DbType.Int32),
new System.Data.SqlClient.SqlParameter("@Comment",  System.Data.DbType.String),
new System.Data.SqlClient.SqlParameter("@CreatedBy",  System.Data.DbType.Int32),
                };
                arrParameters[0].Value = Id;
                arrParameters[1].Value = postcomment;
                arrParameters[2].Value = createdby;
                string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"].ToString();
                int i = SqlHelper.ExecuteNonQuery(sqlConnectionString, System.Data.CommandType.StoredProcedure, "sp_PostComment", arrParameters);
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

        public DataSet GetUserId()
        {
            string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"].ToString();
            System.Data.SqlClient.SqlParameter[] arrParameters = new System.Data.SqlClient.SqlParameter[]
               {

new System.Data.SqlClient.SqlParameter("@EmailId",  System.Data.DbType.String),
               };
            arrParameters[0].Value = HttpContext.Current.Session["username"].ToString();
            DataSet ds = SqlHelper.ExecuteDataset(sqlConnectionString, System.Data.CommandType.StoredProcedure, "sp_User_GetUserDetails", arrParameters);
            return ds;
        }
    }
}