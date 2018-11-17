using Microsoft.ApplicationBlocks.Data;
using PagedList;
using sellnbuyapps.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace sellnbuyapps.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index(Home user, int? page)
        {
            int pageSize = 12;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<Home> pdetails = null;
            Home sb = new Home();
            List<Home> objHome = new List<Home>();
            objHome = user.ProjectListBind();
            sb.pds = objHome;
            pdetails = objHome.ToPagedList(pageIndex, pageSize);
            if (objHome.Count == 0)
            {
                ViewBag.Message = "No Record Found";
            }
            return View(pdetails);
        }

        [HttpGet]
        public ActionResult Search(string Query, Home user, int? page)
        {
            if (Query == "")
            {
                return RedirectToAction("../Home/Index");
            }
            else
            {
                int pageSize = 12;
                int pageIndex = 1;
                pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
                IPagedList<Home> pdetails = null;
                Home sb = new Home();
                List<Home> objHome = new List<Home>();
                ViewBag.SearchResult = Query;
                sb.Query = user.Query;
                objHome = user.SearchProjectListBind();
                sb.pds = objHome;
                if (objHome.Count == 0)
                {
                    ViewBag.Message = "Oops! Result Not Found";
                }
                pdetails = objHome.ToPagedList(pageIndex, pageSize);
                return View(pdetails);
            }
        }

        public ActionResult SingleProject(int? Id, Home details)
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
            return View(details);
        }

        public ActionResult AboutUs()
        {
            return View();
        }

        public ActionResult HTShop()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CategorySearch(string Query, Home user, int? page)
        {
            int pageSize = 12;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<Home> pdetails = null;
            Home sb = new Home();
            List<Home> objHome = new List<Home>();
            ViewBag.SearchResult = Query;
            sb.Query = user.Query;
            objHome = user.SearchProjectsBasedOnCategory();
            sb.pds = objHome;
            if (objHome.Count == 0)
            {
                ViewBag.Message = "Oops! Result Not Found";
            }
            pdetails = objHome.ToPagedList(pageIndex, pageSize);
            return View(pdetails);
        }

        [HttpGet]
        public ActionResult TechnologySearch(string Query, Home user, int? page)
        {
            int pageSize = 12;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<Home> pdetails = null;
            Home sb = new Home();
            List<Home> objHome = new List<Home>();
            ViewBag.SearchResult = Query;
            sb.Query = user.Query;
            objHome = user.SearchProjectsBasedOnTechnology();
            sb.pds = objHome;
            if (objHome.Count == 0)
            {
                ViewBag.Message = "Oops! Result Not Found";
            }
            pdetails = objHome.ToPagedList(pageIndex, pageSize);
            return View(pdetails);
        }
        [HttpGet]
        public ActionResult Cart()
        {
            if (Session["cart"] == null)
            {
                ViewBag.Check = "No item in cart";
                return View();
            }
            else
            {
                return View();
            }

        }

        public ActionResult Buy(int Id, Home home)
        {
            DataSet ds = new DataSet();
            if (Session["cart"] == null)
            {
                ds = home.GetByProductID_SingleProjectDetails();
                List<Home> cart = new List<Home>();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    cart.Add(new Home
                    {
                        Id = Convert.ToInt32(ds.Tables[0].Rows[0]["Id"]),
                        projectname = Convert.ToString(ds.Tables[0].Rows[0]["ProjectName"]),
                        screenshot = Convert.ToString(ds.Tables[0].Rows[0]["Screenshot"]),
                        features = Convert.ToString(ds.Tables[0].Rows[0]["Features"]),
                        technologyname = Convert.ToString(ds.Tables[0].Rows[0]["TechnologyName"]),
                        cost = Convert.ToInt32(ds.Tables[0].Rows[0]["Cost"]),
                    });
                }
                Session["cart"] = cart;
            }
            else
            {
                ds = home.GetByProductID_SingleProjectDetails();
                List<Home> cart = (List<Home>)Session["cart"];
                int index = isExist(Id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        cart.Add(new Home
                        {
                            Id = Convert.ToInt32(ds.Tables[0].Rows[0]["Id"]),
                            projectname = Convert.ToString(ds.Tables[0].Rows[0]["ProjectName"]),
                            screenshot = Convert.ToString(ds.Tables[0].Rows[0]["Screenshot"]),
                            features = Convert.ToString(ds.Tables[0].Rows[0]["Features"]),
                            technologyname = Convert.ToString(ds.Tables[0].Rows[0]["TechnologyName"]),
                            cost = Convert.ToInt32(ds.Tables[0].Rows[0]["Cost"]),
                        });
                    }
                }
                Session["cart"] = cart;
            }
            return RedirectToAction("../Home/Cart");
        }

        public ActionResult Remove(int Id)
        {
            List<Home> cart = (List<Home>)Session["cart"];
            int index = isExist(Id);
            cart.RemoveAt(index);
            Session["cart"] = cart;
            if (Session["cart"] == null)
            {
                return View();
            }
            return RedirectToAction("../Home/Cart");
        }

        private int isExist(int Id)
        {
            List<Home> cart = (List<Home>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].Id.Equals(Id))
                    return i;
            return -1;
        }

    }
}