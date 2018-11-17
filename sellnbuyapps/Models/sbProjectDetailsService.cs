using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace sellnbuyapps.Models
{
    public class sbProjectDetailsService
    {
        private int intId;
        private int intProjectCategoryId;
        private string strProjectName;
        private int decCost;
        private string strFeatures;
        private string strDescription;
        private int intTechnology;
        private Boolean blDemo;
        private Boolean blIsCustomizable;
        private string strScreenshot;
        private Boolean blStatus = true;
        private Boolean blIsFree;
     

        public string Query { get; set; }
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

        [DisplayName("Project Category")]
        public int ProjectCategoryId
        {
            get
            {
                return intProjectCategoryId;
            }
            set
            {
                intProjectCategoryId = value;
            }
        }

        public string ProjectCategory { get; set; }

        public string TechnologyName
        {
            get; set;
        }

        [DisplayName("Project Name")]
        public string ProjectName
        {
            get
            {
                return strProjectName;
            }
            set
            {
                strProjectName = value;
            }
        }
        public int Cost
        {
            get
            {
                return decCost;
            }
            set
            {
                decCost = value;
            }
        }
        public string Features
        {
            get
            {
                return strFeatures;
            }
            set
            {
                strFeatures = value;
            }
        }
        public string Description
        {
            get
            {
                return strDescription;
            }
            set
            {
                strDescription = value;
            }
        }
        public int Technology
        {
            get
            {
                return intTechnology;
            }
            set
            {
                intTechnology = value;
            }
        }
        public Boolean Demo
        {
            get
            {
                return blDemo;
            }
            set
            {
                blDemo = value;
            }
        }

        [DisplayName("Customizable")]
        public Boolean IsCustomizable
        {
            get
            {
                return blIsCustomizable;
            }
            set
            {
                blIsCustomizable = value;
            }
        }

        [DisplayName("Upload Image")]
        public string Screenshot
        {
            get
            {
                return strScreenshot;
            }
            set
            {
                strScreenshot = value;
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

        public Boolean IsFree
        {
            get
            {
                return blIsFree;
            }
            set
            {
                blIsFree = value;
            }
        }
        public string CreatedByFirstName
        {
            get;set;
        }
        public string CreatedByLastName
        {
            get; set;
        }
        public string CreatedByProfilePicture { get; set; }
        public int CreatedId { get; set; }
        public List<sbProjectDetailsService> pds { get; set; }
        public Boolean AddSb_ProjectDetails()
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] arrParameters = new System.Data.SqlClient.SqlParameter[]
                {

new System.Data.SqlClient.SqlParameter("@ProjectCategoryId",  System.Data.DbType.Int32),
new System.Data.SqlClient.SqlParameter("@ProjectName",  System.Data.DbType.String),
new System.Data.SqlClient.SqlParameter("@Cost",  System.Data.DbType.Int32),
new System.Data.SqlClient.SqlParameter("@Features",  System.Data.DbType.String),
new System.Data.SqlClient.SqlParameter("@Description",  System.Data.DbType.String),
new System.Data.SqlClient.SqlParameter("@TechnologyId",  System.Data.DbType.Int32),
new System.Data.SqlClient.SqlParameter("@Demo",  System.Data.DbType.Boolean),
new System.Data.SqlClient.SqlParameter("@IsCustomizable",  System.Data.DbType.Boolean),
new System.Data.SqlClient.SqlParameter("@Screenshot",  System.Data.DbType.String),
new System.Data.SqlClient.SqlParameter("@Status",  System.Data.DbType.Boolean),
new System.Data.SqlClient.SqlParameter("@IsFree",  System.Data.DbType.Boolean),
new System.Data.SqlClient.SqlParameter("@CreatedId",  System.Data.DbType.Int32),
                };

                arrParameters[0].Value = ProjectCategoryId;
                arrParameters[1].Value = ProjectName;
                arrParameters[2].Value = Cost;
                arrParameters[3].Value = Features;
                arrParameters[4].Value = Description;
                arrParameters[5].Value = Technology;
                arrParameters[6].Value = Demo;
                arrParameters[7].Value = IsCustomizable;
                arrParameters[8].Value = Screenshot;
                arrParameters[9].Value = Status;
                arrParameters[10].Value = IsFree;
                arrParameters[11].Value = CreatedId;

                string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"].ToString();
                int i = SqlHelper.ExecuteNonQuery(sqlConnectionString, System.Data.CommandType.StoredProcedure, "Sb_ProjectDetails_AddRecord", arrParameters);
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
        public Boolean UpdateSb_ProjectDetails()
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] arrParameters = new System.Data.SqlClient.SqlParameter[]
                {
new System.Data.SqlClient.SqlParameter("@Id",  System.Data.DbType.Int32),
new System.Data.SqlClient.SqlParameter("@ProjectCategoryId",  System.Data.DbType.Int32),
new System.Data.SqlClient.SqlParameter("@ProjectName",  System.Data.DbType.String),
new System.Data.SqlClient.SqlParameter("@Cost",  System.Data.DbType.Int32),
new System.Data.SqlClient.SqlParameter("@Features",  System.Data.DbType.String),
new System.Data.SqlClient.SqlParameter("@Description",  System.Data.DbType.String),
new System.Data.SqlClient.SqlParameter("@TechnologyId",  System.Data.DbType.Int32),
new System.Data.SqlClient.SqlParameter("@Demo",  System.Data.DbType.Boolean),
new System.Data.SqlClient.SqlParameter("@IsCustomizable",  System.Data.DbType.Boolean),
new System.Data.SqlClient.SqlParameter("@Screenshot",  System.Data.DbType.String),
new System.Data.SqlClient.SqlParameter("@Status",  System.Data.DbType.Boolean),
new System.Data.SqlClient.SqlParameter("@IsFree",  System.Data.DbType.Boolean),
new System.Data.SqlClient.SqlParameter("@CreatedId",  System.Data.DbType.Int32),
                };
                arrParameters[0].Value = Id;
                arrParameters[1].Value = ProjectCategoryId;
                arrParameters[2].Value = ProjectName;
                arrParameters[3].Value = Cost;
                arrParameters[4].Value = Features;
                arrParameters[5].Value = Description;
                arrParameters[6].Value = Technology;
                arrParameters[7].Value = Demo;
                arrParameters[8].Value = IsCustomizable;
                arrParameters[9].Value = Screenshot;
                arrParameters[10].Value = Status;
                arrParameters[11].Value = IsFree;
                arrParameters[12].Value = CreatedId;

                string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"].ToString();
                int i = SqlHelper.ExecuteNonQuery(sqlConnectionString, System.Data.CommandType.StoredProcedure, "Sb_ProjectDetails_UpdateRecord", arrParameters);
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
        public DataSet GetData_Sb_ProjectDetails()
        {
            try
            {
                string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"].ToString();
                DataSet ds = SqlHelper.ExecuteDataset(sqlConnectionString, System.Data.CommandType.StoredProcedure, "Sb_ProjectDetails_GetData");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetByProductID_Sb_ProjectDetails()
        {
            try
            {
                string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"].ToString();

                System.Data.SqlClient.SqlParameter[] arrParameters = new System.Data.SqlClient.SqlParameter[]
                {
new System.Data.SqlClient.SqlParameter("@Id",  System.Data.DbType.Int32)
                };
                arrParameters[0].Value = Id;

                DataSet ds = SqlHelper.ExecuteDataset(sqlConnectionString, System.Data.CommandType.StoredProcedure, "Sb_ProjectDetails_GetDataByProductId", arrParameters);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetByID_Sb_ProjectDetails()
        {
            try
            {
                string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"].ToString();

                Home objHome = new Home();
                DataSet ds1 = objHome.GetUserId();
                int userId = Convert.ToInt32(ds1.Tables[0].Rows[0]["Id"].ToString());
                System.Data.SqlClient.SqlParameter[] arrParameters = new System.Data.SqlClient.SqlParameter[]
                {
new System.Data.SqlClient.SqlParameter("@Id",  System.Data.DbType.Int32)
                };
                arrParameters[0].Value = userId;

                DataSet ds = SqlHelper.ExecuteDataset(sqlConnectionString, System.Data.CommandType.StoredProcedure, "Sb_ProjectDetails_GetDataById", arrParameters);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetByID_Sb_top10ProjectDetails()
        {
            try
            {
                string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"].ToString();

                Home objHome = new Home();
                DataSet ds1 = objHome.GetUserId();
                int userId = Convert.ToInt32(ds1.Tables[0].Rows[0]["Id"].ToString());
                System.Data.SqlClient.SqlParameter[] arrParameters = new System.Data.SqlClient.SqlParameter[]
                {
new System.Data.SqlClient.SqlParameter("@Id",  System.Data.DbType.Int32)
                };
                arrParameters[0].Value = userId;

                DataSet ds = SqlHelper.ExecuteDataset(sqlConnectionString, System.Data.CommandType.StoredProcedure, "sp_DisplayTopProjects", arrParameters);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<sbProjectDetailsService> ProjectListBind()
        {
            string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"].ToString();

            Home objHome = new Home();
            DataSet ds1 = objHome.GetUserId();
            int userId = Convert.ToInt32(ds1.Tables[0].Rows[0]["Id"].ToString());

            System.Data.SqlClient.SqlParameter[] arrParameters = new System.Data.SqlClient.SqlParameter[]
            {
new System.Data.SqlClient.SqlParameter("@Id",  System.Data.DbType.Int32)
            };
            arrParameters[0].Value = userId;

            DataSet ds = SqlHelper.ExecuteDataset(sqlConnectionString, System.Data.CommandType.StoredProcedure, "Sb_ProjectDetails_GetDataById", arrParameters);
            List<sbProjectDetailsService> lst = new List<sbProjectDetailsService>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    lst.Add(new sbProjectDetailsService
                    {
                        Id = Convert.ToInt32(item["id"]),
                        ProjectCategory = Convert.ToString(item["CategoryName"]),
                        ProjectName = Convert.ToString(item["ProjectName"]),
                        Screenshot = Convert.ToString(item["Screenshot"]),
                        Features = Convert.ToString(item["Features"]),
                        TechnologyName = Convert.ToString(item["TechnologyName"]),
                        Cost = Convert.ToInt32(item["Cost"]),
                        Description = Convert.ToString(item["Description"]),


                    });
                }
            }
            return lst;
        }

        public List<sbProjectDetailsService> AllProjectsBind()
        {
            Home objHome = new Home();
            DataSet ds1 = new DataSet();
            ds1 = objHome.GetUserId();
            int createdId = Convert.ToInt32(ds1.Tables[0].Rows[0]["Id"].ToString());
            string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"].ToString();
            System.Data.SqlClient.SqlParameter[] arrParameters = new System.Data.SqlClient.SqlParameter[]
               {
new System.Data.SqlClient.SqlParameter("@Id",  System.Data.DbType.Int32)
               };
            arrParameters[0].Value = createdId;
            DataSet ds = SqlHelper.ExecuteDataset(sqlConnectionString, System.Data.CommandType.StoredProcedure, "Sb_ProjectDetails_GetData", arrParameters);
            List<sbProjectDetailsService> lst = new List<sbProjectDetailsService>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    lst.Add(new sbProjectDetailsService
                    {
                        Id = Convert.ToInt32(item["Id"]),
                        ProjectCategory = Convert.ToString(item["CategoryName"]),
                        ProjectName = Convert.ToString(item["ProjectName"]),
                        Screenshot = Convert.ToString(item["Screenshot"]),
                        TechnologyName = Convert.ToString(item["TechnologyName"]),
                        Cost = Convert.ToInt32(item["Cost"]),
                        Features = Convert.ToString(item["Features"]),
                        CreatedByFirstName = Convert.ToString(item["FirstName"]),
                        CreatedByLastName = Convert.ToString(item["LastName"]),
                        CreatedByProfilePicture = Convert.ToString(item["ProfilePicture"]),
                    });
                }
            }
            return lst;
        }

        public List<sbProjectDetailsService> SearchAllProjectsBind()
        {
            Home objHome = new Home();
            DataSet ds1 = new DataSet();
            ds1 = objHome.GetUserId();
            int createdId = Convert.ToInt32(ds1.Tables[0].Rows[0]["Id"].ToString());
            string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"].ToString();
            System.Data.SqlClient.SqlParameter[] arrParameters = new System.Data.SqlClient.SqlParameter[]
              {
new System.Data.SqlClient.SqlParameter("@Query",  System.Data.DbType.String),
new System.Data.SqlClient.SqlParameter("@Id",  System.Data.DbType.String),
              };
            arrParameters[0].Value = Query;
            arrParameters[1].Value = createdId;
            DataSet ds = SqlHelper.ExecuteDataset(sqlConnectionString, System.Data.CommandType.StoredProcedure, "Sb_SearchProjectDetails_GetData", arrParameters);
            List<sbProjectDetailsService> lst = new List<sbProjectDetailsService>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    lst.Add(new sbProjectDetailsService
                    {
                        Id = Convert.ToInt32(item["Id"].ToString()),
                        ProjectCategory = Convert.ToString(item["CategoryName"]),
                        ProjectName = Convert.ToString(item["ProjectName"]),
                        Screenshot = Convert.ToString(item["Screenshot"]),
                        TechnologyName = Convert.ToString(item["TechnologyName"]),
                        Cost = Convert.ToInt32(item["Cost"]),
                        Features = Convert.ToString(item["Features"]),
                        CreatedByFirstName = Convert.ToString(item["FirstName"]),
                        CreatedByLastName = Convert.ToString(item["LastName"]),
                        CreatedByProfilePicture = Convert.ToString(item["ProfilePicture"]),
                    });
                }
            }
            return lst;
        }

        public List<sbProjectDetailsService> ProjectCategorySearch()
        {
            Home objHome = new Home();
            DataSet ds1 = new DataSet();
            ds1 = objHome.GetUserId();
            int createdId = Convert.ToInt32(ds1.Tables[0].Rows[0]["Id"].ToString());
            string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"].ToString();
            System.Data.SqlClient.SqlParameter[] arrParameters = new System.Data.SqlClient.SqlParameter[]
              {
new System.Data.SqlClient.SqlParameter("@Query",  System.Data.DbType.String),
new System.Data.SqlClient.SqlParameter("@Id",  System.Data.DbType.String),
              };
            arrParameters[0].Value = Query;
            arrParameters[1].Value = createdId;
            DataSet ds = SqlHelper.ExecuteDataset(sqlConnectionString, System.Data.CommandType.StoredProcedure, "sp_CategorySearchForCustomer", arrParameters);
            List<sbProjectDetailsService> lst = new List<sbProjectDetailsService>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    lst.Add(new sbProjectDetailsService
                    {
                        Id = Convert.ToInt32(item["Id"].ToString()),
                        ProjectCategory = Convert.ToString(item["CategoryName"]),
                        ProjectName = Convert.ToString(item["ProjectName"]),
                        Screenshot = Convert.ToString(item["Screenshot"]),
                        TechnologyName = Convert.ToString(item["TechnologyName"]),
                        Cost = Convert.ToInt32(item["Cost"]),
                        Features = Convert.ToString(item["Features"]),
                        CreatedByFirstName = Convert.ToString(item["FirstName"]),
                        CreatedByLastName = Convert.ToString(item["LastName"]),
                        CreatedByProfilePicture = Convert.ToString(item["ProfilePicture"]),
                    });
                }
            }
            return lst;
        }

        public List<sbProjectDetailsService> ProjectTechnologySearch()
        {
            Home objHome = new Home();
            DataSet ds1 = new DataSet();
            ds1 = objHome.GetUserId();
            int createdId = Convert.ToInt32(ds1.Tables[0].Rows[0]["Id"].ToString());
            string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"].ToString();
            System.Data.SqlClient.SqlParameter[] arrParameters = new System.Data.SqlClient.SqlParameter[]
              {
new System.Data.SqlClient.SqlParameter("@Query",  System.Data.DbType.String),
new System.Data.SqlClient.SqlParameter("@Id",  System.Data.DbType.String),
              };
            arrParameters[0].Value = Query;
            arrParameters[1].Value = createdId;
            DataSet ds = SqlHelper.ExecuteDataset(sqlConnectionString, System.Data.CommandType.StoredProcedure, "sp_TechnologySearchForCustomer", arrParameters);
            List<sbProjectDetailsService> lst = new List<sbProjectDetailsService>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    lst.Add(new sbProjectDetailsService
                    {
                        Id = Convert.ToInt32(item["Id"].ToString()),
                        ProjectCategory = Convert.ToString(item["CategoryName"]),
                        ProjectName = Convert.ToString(item["ProjectName"]),
                        Screenshot = Convert.ToString(item["Screenshot"]),
                        TechnologyName = Convert.ToString(item["TechnologyName"]),
                        Cost = Convert.ToInt32(item["Cost"]),
                        Features = Convert.ToString(item["Features"]),
                        CreatedByFirstName = Convert.ToString(item["FirstName"]),
                        CreatedByLastName = Convert.ToString(item["LastName"]),
                        CreatedByProfilePicture = Convert.ToString(item["ProfilePicture"]),
                    });
                }
            }
            return lst;
        }

        public List<sbProjectDetailsService> AllProjectsForAdmin()
        {
           string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"].ToString();
            DataSet ds = SqlHelper.ExecuteDataset(sqlConnectionString, System.Data.CommandType.StoredProcedure, "Sb_ProjectDetails_GetDataForAdmin");
            List<sbProjectDetailsService> lst = new List<sbProjectDetailsService>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    lst.Add(new sbProjectDetailsService
                    {
                        Id = Convert.ToInt32(item["Id"]),
                        ProjectCategory = Convert.ToString(item["CategoryName"]),
                        ProjectName = Convert.ToString(item["ProjectName"]),
                        Screenshot = Convert.ToString(item["Screenshot"]),
                        TechnologyName = Convert.ToString(item["TechnologyName"]),
                        Cost = Convert.ToInt32(item["Cost"]),
                        Features = Convert.ToString(item["Features"]),
                        CreatedByFirstName = Convert.ToString(item["FirstName"]),
                        CreatedByLastName = Convert.ToString(item["LastName"]),
                        CreatedByProfilePicture = Convert.ToString(item["ProfilePicture"]),
                    });
                }
            }
            return lst;
        }

        public List<sbProjectDetailsService> SearchAllProjectsForAdmin()
        {
            string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"].ToString();
            System.Data.SqlClient.SqlParameter[] arrParameters = new System.Data.SqlClient.SqlParameter[]
              {
new System.Data.SqlClient.SqlParameter("@Query",  System.Data.DbType.String),
              };
            arrParameters[0].Value = Query;
            DataSet ds = SqlHelper.ExecuteDataset(sqlConnectionString, System.Data.CommandType.StoredProcedure, "Sb_SearchProjectDetails_GetDataForAdmin", arrParameters);
            List<sbProjectDetailsService> lst = new List<sbProjectDetailsService>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    lst.Add(new sbProjectDetailsService
                    {
                        Id = Convert.ToInt32(item["Id"].ToString()),
                        ProjectCategory = Convert.ToString(item["CategoryName"]),
                        ProjectName = Convert.ToString(item["ProjectName"]),
                        Screenshot = Convert.ToString(item["Screenshot"]),
                        TechnologyName = Convert.ToString(item["TechnologyName"]),
                        Cost = Convert.ToInt32(item["Cost"]),
                        Features = Convert.ToString(item["Features"]),
                        CreatedByFirstName = Convert.ToString(item["FirstName"]),
                        CreatedByLastName = Convert.ToString(item["LastName"]),
                        CreatedByProfilePicture = Convert.ToString(item["ProfilePicture"]),
                    });
                }
            }
            return lst;
        }

        public Boolean DeleteByID_Sb_ProjectDetails()
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] arrParameters = new System.Data.SqlClient.SqlParameter[]
                {
new System.Data.SqlClient.SqlParameter("@Id",  System.Data.DbType.Int32)
                };
                arrParameters[0].Value = Id;
                string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"].ToString();
                int i = SqlHelper.ExecuteNonQuery(sqlConnectionString, System.Data.CommandType.StoredProcedure, "Sb_ProjectDetails_DeleteById", arrParameters);
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

        public Boolean GetUserId()
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] arrParameters = new System.Data.SqlClient.SqlParameter[]
                {
new System.Data.SqlClient.SqlParameter("@EmailId",  System.Data.DbType.String)
                };
                arrParameters[0].Value = HttpContext.Current.Session["username"].ToString();
                string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"].ToString();
                int i = SqlHelper.ExecuteNonQuery(sqlConnectionString, System.Data.CommandType.StoredProcedure, "Sb_ProjectDetails_GetUserId", arrParameters);
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