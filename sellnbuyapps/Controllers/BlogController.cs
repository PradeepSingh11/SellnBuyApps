using Microsoft.ApplicationBlocks.Data;
using PagedList;
using sellnbuyapps.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sellnbuyapps.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog
        public ActionResult Index(Home user, int? page)
        {
            int pageSize = 6;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<Home> pdetails = null;
            Home sb = new Home();
            List<Home> objHome = new List<Home>();
            objHome = user.ProjectListBind();
            sb.pds = objHome;
            pdetails = objHome.ToPagedList(pageIndex, pageSize);
            if(objHome.Count == 0)
            {
                ViewBag.Message = "No Record Found";
            }
            return View(pdetails);
        }

        [HttpGet]
        public ActionResult SinglePost(int? Id, Home details)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("../Login/Login");
            }
            else
            {
                DataSet ds = new DataSet();
                ds = details.GetByProductID_SingleProjectDetails();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    details.projectname = ds.Tables[0].Rows[0]["ProjectName"].ToString();
                    details.cost = Convert.ToInt32(ds.Tables[0].Rows[0]["Cost"].ToString());
                    details.features = ds.Tables[0].Rows[0]["Features"].ToString();
                    details.description = ds.Tables[0].Rows[0]["Description"].ToString();
                    details.technologyname = ds.Tables[0].Rows[0]["TechnologyName"].ToString();
                    details.screenshot = ds.Tables[0].Rows[0]["Screenshot"].ToString();
                    details.authorfirstname = ds.Tables[0].Rows[0]["FirstName"].ToString();
                    details.authorlastname = ds.Tables[0].Rows[0]["LastName"].ToString();
                    details.profilepicture = ds.Tables[0].Rows[0]["ProfilePicture"].ToString();
                }

                string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"].ToString();

                System.Data.SqlClient.SqlParameter[] arrParameters = new System.Data.SqlClient.SqlParameter[]
               {
new System.Data.SqlClient.SqlParameter("@Id",  System.Data.DbType.Int32)
               };
                arrParameters[0].Value = Id;

                string comments = SqlHelper.ExecuteScalar(sqlConnectionString, System.Data.CommandType.Text, "select count(Comment) as Comment from sb_ProjectComments where ProjectId=@Id", arrParameters).ToString();

                details.comments = Convert.ToInt32(comments);
                details.rating = SqlHelper.ExecuteScalar(sqlConnectionString, System.Data.CommandType.Text, "select count(Rating) as Rating from sb_ProjectComments where ProjectId=@Id", arrParameters).ToString();
            }
            return View(details);
        }

        [HttpPost]
        public ActionResult SinglePost(int Id, Home details)
        {
            Home objHome = new Home();
            DataSet ds = new DataSet();
            ds = details.GetUserId();
            objHome.Id = details.Id;
            objHome.postcomment = details.postcomment;
            objHome.createdby = Convert.ToInt32(ds.Tables[0].Rows[0]["Id"].ToString());
            bool IsInserted = false;
            IsInserted = objHome.Add_Comment();
            if (IsInserted)
            {
                ViewBag.Message = "Information has been saved successfully";
            }
            return RedirectToAction("Index");
        }

        public ActionResult AboutUs()
        {
            return View();
        }
    }
}