using sellnbuyapps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Data;

namespace sellnbuyapps.Controllers
{
    public class ProjectDetailsController : Controller
    {
        // GET: ProjectDetails
        [HttpGet]
        public ActionResult Projects(ProjectDetails details, int? page)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("../Login/Login");
            }
            else
            {
                int pageSize = 8;
                int pageIndex = 1;
                pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
                IPagedList<ProjectDetails> pdetails = null;
                ProjectDetails sb = new ProjectDetails();
                List<ProjectDetails> objProjectDetails = new List<ProjectDetails>();
                objProjectDetails = details.ProjectListBind();
                sb.pds = objProjectDetails;
                pdetails = objProjectDetails.ToPagedList(pageIndex, pageSize);
                if (objProjectDetails.Count == 0)
                {
                    ViewBag.Message = "No Project Found";
                }
                return View(pdetails);
            }
        }

        public ActionResult SingleProduct(int? Id, ProjectDetails details)
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
                }
            }
            return View(details);

        }

        [HttpGet]
        public ActionResult Search(string Query, ProjectDetails details, int? page)
        {
            int pageSize = 8;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<ProjectDetails> pdetails = null;
            ProjectDetails sb = new ProjectDetails();
            List<ProjectDetails> objProjectDetails = new List<ProjectDetails>();
            objProjectDetails = details.SearchProjectListBind();
            sb.pds = objProjectDetails;
            pdetails = objProjectDetails.ToPagedList(pageIndex, pageSize);
            if (objProjectDetails.Count == 0)
            {
                ViewBag.Message = "Oops! No Result Found";
            }
            return View(pdetails);
        }
    }
}