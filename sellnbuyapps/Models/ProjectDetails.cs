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
    public class ProjectDetails
    {
        public int Id { get; set; }

        public string screenshot { get; set; }

        public string projectname { get; set; }

        public string features { get; set; }

        public string technologyname { get; set; }

        public int comments { get; set; }

        public string rating { get; set; }

        public int cost { get; set; }

        public string description { get; set; }

        public string Query { get; set; }

        public List<ProjectDetails> pds { get; set; }

        public DataSet GetByID_Sb_ProjectDetails()
        {
            try
            {
                string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"].ToString();

                SqlConnection con = new SqlConnection(sqlConnectionString);
                SqlCommand command = new SqlCommand("select Id from sb_User where EmailId ='" + HttpContext.Current.Session["username"].ToString() + "'", con);
                command.Connection.Open();
                int userId = Convert.ToInt32(command.ExecuteScalar());
                command.Connection.Close();

                System.Data.SqlClient.SqlParameter[] arrParameters = new System.Data.SqlClient.SqlParameter[]
                {
new System.Data.SqlClient.SqlParameter("@Id",  System.Data.DbType.Int32)
                };
                arrParameters[0].Value = userId;

                DataSet ds = SqlHelper.ExecuteDataset(sqlConnectionString, System.Data.CommandType.StoredProcedure, "Sb_GetProjectDetails", arrParameters);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProjectDetails> ProjectListBind()
        {
            string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"].ToString();

            SqlConnection con = new SqlConnection(sqlConnectionString);
            SqlCommand command = new SqlCommand("select Id from sb_User where EmailId ='" + HttpContext.Current.Session["username"].ToString() + "'", con);
            command.Connection.Open();
            int userId = Convert.ToInt32(command.ExecuteScalar());
            command.Connection.Close();

            System.Data.SqlClient.SqlParameter[] arrParameters = new System.Data.SqlClient.SqlParameter[]
            {
new System.Data.SqlClient.SqlParameter("@Id",  System.Data.DbType.Int32)
            };
            arrParameters[0].Value = userId;

            DataSet ds = SqlHelper.ExecuteDataset(sqlConnectionString, System.Data.CommandType.StoredProcedure, "Sb_ProjectDetails_GetDataById", arrParameters);
            List<ProjectDetails> lst = new List<ProjectDetails>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    lst.Add(new ProjectDetails
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

        public List<ProjectDetails> SearchProjectListBind()
        {
            string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"].ToString();

            SqlConnection con = new SqlConnection(sqlConnectionString);
            SqlCommand command = new SqlCommand("select Id from sb_User where EmailId ='" + HttpContext.Current.Session["username"].ToString() + "'", con);
            command.Connection.Open();
            int userId = Convert.ToInt32(command.ExecuteScalar());
            command.Connection.Close();

            System.Data.SqlClient.SqlParameter[] arrParameters = new System.Data.SqlClient.SqlParameter[]
            {
new System.Data.SqlClient.SqlParameter("@Id",  System.Data.DbType.Int32),
new System.Data.SqlClient.SqlParameter("@Query",  System.Data.DbType.String),
            };
            arrParameters[0].Value = userId;
            arrParameters[1].Value = Query;
            DataSet ds = SqlHelper.ExecuteDataset(sqlConnectionString, System.Data.CommandType.StoredProcedure, "Sb_SearchProjectDetails_GetDataById", arrParameters);
            List<ProjectDetails> lst = new List<ProjectDetails>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    lst.Add(new ProjectDetails
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
    }
}