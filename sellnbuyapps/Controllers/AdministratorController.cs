using PagedList;
using sellnbuyapps.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace sellnbuyapps.Controllers
{
    public class AdministratorController : Controller
    {
        // GET: Administrator
        public ActionResult Index(sbAdministrator admin)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Administrator");
            }
            else
            {
                ViewBag.TotalUser = admin.countTotalUser();
                ViewBag.TotalProjects = admin.countTotalProjects();
                return View(admin);
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(sbAdministrator login)
        {
            try
            {
                SbUserService objUserService = new SbUserService();
                if (!string.IsNullOrEmpty(login.EmailId))
                {
                    sbAdministrator objAdministrator = new sbAdministrator();
                    objAdministrator.EmailId = login.EmailId;
                    string password = login.Password;
                    objAdministrator.Password = objUserService.GetEncryptedValue(password);

                    DataSet ds = objAdministrator.Sb_Login();

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Session["username"] = ds.Tables[0].Rows[0]["Name"].ToString();
                        return RedirectToAction("../Administrator/Index");
                    }
                    else
                    {
                        ViewBag.Message = "Wrong Email or Password entered";
                    }
                }
            }
            catch (Exception ee)
            {
                ViewBag.Message = "Error Occurred";
            }
            return View(login);
        }

        public ActionResult AllUsers(sbAdministrator admin, int? page)
        {
            try
            {
                if (Session["username"] == null)
                {
                    return RedirectToAction("Login", "Administrator");
                }
                else
                {
                    int pageSize = 6;
                    int pageIndex = 1;
                    pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
                    IPagedList<sbAdministrator> pdetails = null;
                    sbAdministrator sb = new sbAdministrator();
                    List<sbAdministrator> objsbAdministrator = new List<sbAdministrator>();
                    objsbAdministrator = admin.AllUsersBind();
                    sb.pds = objsbAdministrator;
                    pdetails = objsbAdministrator.ToPagedList(pageIndex, pageSize);
                    if (objsbAdministrator.Count == 0)
                    {
                        ViewBag.Message = "Oops! No Record Found";
                    }
                    return View(pdetails);
                }

            }
            catch (Exception ee)
            {
                ViewBag.Message = "Error Occure";
                return View();
            }
        }

        public ActionResult DeleteUser(int? id, sbAdministrator admin)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = admin.GetByID_Sb_User();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    admin.FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
                    admin.LastName = ds.Tables[0].Rows[0]["LastName"].ToString();
                    admin.EmailId = ds.Tables[0].Rows[0]["EmailId"].ToString();
                    admin.Password = ds.Tables[0].Rows[0]["Password"].ToString();
                    admin.MobileNumber = ds.Tables[0].Rows[0]["MobileNumber"].ToString();
                    admin.CompanyURL = ds.Tables[0].Rows[0]["CompanyURL"].ToString();
                    //admin.IsSeller = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsSeller"].ToString());
                    admin.Status = Convert.ToBoolean(ds.Tables[0].Rows[0]["Status"].ToString());
                    if (Convert.ToString(ds.Tables[0].Rows[0]["ModifiedOn"]) == "")
                    {
                        admin.ModifiedOn = "";
                    }
                    else
                    {
                        admin.ModifiedOn = ds.Tables[0].Rows[0]["ModifiedOn"].ToString();
                    }
                }
                return View(admin);
            }
            catch (Exception ee)
            {
                ViewBag.Message = "Error Occure";
                return View();
            }
        }

        [HttpPost]
        public ActionResult DeleteUser(int id, sbAdministrator admin)
        {
            try
            {
                if (id != 0)
                {
                    sbAdministrator objsbAdministrator = new sbAdministrator();
                    objsbAdministrator.ID = id;
                    bool IsDeleted = false;
                    IsDeleted = objsbAdministrator.DeleteByID_Sb_User();
                    if (IsDeleted)
                    {
                        ViewBag.Message = "Information has been deleted successfully";
                    }
                }
                return RedirectToAction("AllUsers");
            }
            catch (Exception ee)
            {
                ViewBag.Message = "Error Occurred";
                return View();
            }
        }

        [HttpGet]
        public ActionResult UserDetails(int id, sbAdministrator admin)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = admin.GetByID_Sb_User();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    admin.FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
                    admin.LastName = ds.Tables[0].Rows[0]["LastName"].ToString();
                    admin.EmailId = ds.Tables[0].Rows[0]["EmailId"].ToString();
                    admin.Password = ds.Tables[0].Rows[0]["Password"].ToString();
                    admin.MobileNumber = ds.Tables[0].Rows[0]["MobileNumber"].ToString();
                    admin.CompanyURL = ds.Tables[0].Rows[0]["CompanyURL"].ToString();
                    //admin.IsSeller = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsSeller"].ToString());
                    admin.Status = Convert.ToBoolean(ds.Tables[0].Rows[0]["Status"].ToString());
                    if (Convert.ToString(ds.Tables[0].Rows[0]["ModifiedOn"]) == "")
                    {
                        admin.ModifiedOn = "";
                    }
                    else
                    {
                        admin.ModifiedOn = ds.Tables[0].Rows[0]["ModifiedOn"].ToString();
                    }
                }
                return View(admin);
            }
            catch (Exception ee)
            {
                ViewBag.Message = "Error Occure";
                return View();
            }
        }

        public ActionResult AllProjects(sbProjectDetailsService sbProject, int? page)
        {
            try
            {
                if (Session["username"] == null)
                {
                    return RedirectToAction("Login", "Administrator");
                }
                else
                {
                    int pageSize = 6;
                    int pageIndex = 1;
                    pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
                    IPagedList<sbProjectDetailsService> pdetails = null;
                    sbProjectDetailsService sb = new sbProjectDetailsService();
                    List<sbProjectDetailsService> objsbProjectDetailsService = new List<sbProjectDetailsService>();
                    objsbProjectDetailsService = sbProject.AllProjectsForAdmin();
                    sb.pds = objsbProjectDetailsService;
                    pdetails = objsbProjectDetailsService.ToPagedList(pageIndex, pageSize);
                    if (objsbProjectDetailsService.Count == 0)
                    {
                        ViewBag.Message = "Oops! No Record Found";
                    }
                    return View(pdetails);
                }
            }
            catch (Exception ee)
            {
                ViewBag.Message = "Error Occure";
                return View();
            }

        }

        public ActionResult DeleteProject(int? id, sbProjectDetailsService sbProject)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = sbProject.GetByProductID_Sb_ProjectDetails();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    sbProject.ProjectCategory = ds.Tables[0].Rows[0]["CategoryName"].ToString();
                    sbProject.ProjectName = ds.Tables[0].Rows[0]["ProjectName"].ToString();
                    sbProject.Cost = Convert.ToInt32(ds.Tables[0].Rows[0]["Cost"].ToString());
                    sbProject.Features = ds.Tables[0].Rows[0]["Features"].ToString();
                    sbProject.Description = ds.Tables[0].Rows[0]["Description"].ToString();
                    sbProject.TechnologyName = ds.Tables[0].Rows[0]["TechnologyName"].ToString();
                    sbProject.Demo = Convert.ToBoolean(ds.Tables[0].Rows[0]["Demo"].ToString());
                    sbProject.IsCustomizable = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsCustomizable"].ToString());
                    sbProject.Screenshot = ds.Tables[0].Rows[0]["Screenshot"].ToString();
                    sbProject.IsFree = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsFree"].ToString());
                    sbProject.CreatedByFirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
                    sbProject.CreatedByLastName = ds.Tables[0].Rows[0]["LastName"].ToString();
                    sbProject.CreatedByProfilePicture = ds.Tables[0].Rows[0]["ProfilePicture"].ToString();
                    sbProject.Status = Convert.ToBoolean(ds.Tables[0].Rows[0]["Status"].ToString());
                }
                return View(sbProject);
            }
            catch (Exception ee)
            {
                ViewBag.Message = "Error Occure";
                return View();
            }

        }

        [HttpPost]
        public ActionResult DeleteProject(int id, sbProjectDetailsService sbProject)
        {
            try
            {
                if (id != 0)
                {
                    sbProjectDetailsService objProjectDetailsService = new sbProjectDetailsService();
                    objProjectDetailsService.Id = id;
                    bool IsDeleted = false;
                    IsDeleted = objProjectDetailsService.DeleteByID_Sb_ProjectDetails();
                    if (IsDeleted)
                    {
                        ViewBag.Message = "Project has been deleted successfully";
                    }
                }
                return RedirectToAction("AllProjects", "Administrator");
            }
            catch (Exception ee)
            {
                ViewBag.Message = "Error Occurred";
                return View();
            }
        }

        public ActionResult ViewProject(int id, sbProjectDetailsService sbProject)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = sbProject.GetByProductID_Sb_ProjectDetails();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    sbProject.ProjectCategory = ds.Tables[0].Rows[0]["CategoryName"].ToString();
                    sbProject.ProjectName = ds.Tables[0].Rows[0]["ProjectName"].ToString();
                    sbProject.Cost = Convert.ToInt32(ds.Tables[0].Rows[0]["Cost"].ToString());
                    sbProject.Features = ds.Tables[0].Rows[0]["Features"].ToString();
                    sbProject.Description = ds.Tables[0].Rows[0]["Description"].ToString();
                    sbProject.TechnologyName = ds.Tables[0].Rows[0]["TechnologyName"].ToString();
                    sbProject.Demo = Convert.ToBoolean(ds.Tables[0].Rows[0]["Demo"].ToString());
                    sbProject.IsCustomizable = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsCustomizable"].ToString());
                    sbProject.Screenshot = ds.Tables[0].Rows[0]["Screenshot"].ToString();
                    sbProject.IsFree = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsFree"].ToString());
                    sbProject.CreatedByFirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
                    sbProject.CreatedByLastName = ds.Tables[0].Rows[0]["LastName"].ToString();
                    sbProject.CreatedByProfilePicture = ds.Tables[0].Rows[0]["ProfilePicture"].ToString();
                    sbProject.Status = Convert.ToBoolean(ds.Tables[0].Rows[0]["Status"].ToString());
                }
                return View(sbProject);
            }
            catch (Exception ee)
            {
                ViewBag.Message = "Error Occure";
                return View();
            }

        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(sbAdministrator chngPass)
        {
            try
            {
                SbUserService objUserService = new SbUserService();
                if (!string.IsNullOrEmpty(chngPass.EmailId))
                {
                    sbAdministrator objsbsbAdministrator = new sbAdministrator();

                    objsbsbAdministrator.EmailId = chngPass.EmailId;
                    objsbsbAdministrator.Password = objUserService.GetEncryptedValue(chngPass.Password);
                    objsbsbAdministrator.NewPassword = objUserService.GetEncryptedValue(chngPass.NewPassword);
                    //objsbsbAdministrator.Name = Session["username"].ToString();
                    bool IsUpdated = false;
                    IsUpdated = objsbsbAdministrator.UpdateSb_User();
                    if (IsUpdated)
                    {
                        ViewBag.Message = "Password changed successfully";
                    }
                    else
                    {
                        ViewBag.Message = "Wrong password entered ";
                    }
                    ModelState.Clear();
                    ModelState.Remove("EmailId");
                    chngPass.EmailId = "";
                    ModelState.Remove("Password");
                    chngPass.Password = "";
                    ModelState.Remove("Password");
                    chngPass.NewPassword = "";
                }
                return View(chngPass);
            }
            catch (Exception ee)
            {
                ViewBag.Message = "Error Occure";
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            FormsAuthentication.SignOut();

            this.Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            this.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            this.Response.Cache.SetNoStore();

            return RedirectToAction("Login", "Administrator");
        }

        [HttpGet]
        public ActionResult ProjectSearch(string Query, sbProjectDetailsService sbProject, int? page)
        {
            try
            {
                int pageSize = 6;
                int pageIndex = 1;
                pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
                IPagedList<sbProjectDetailsService> pdetails = null;
                sbProjectDetailsService sb = new sbProjectDetailsService();
                List<sbProjectDetailsService> objsbProjectDetailsService = new List<sbProjectDetailsService>();
                sb.Query = sbProject.Query;
                objsbProjectDetailsService = sbProject.SearchAllProjectsForAdmin();
                sb.pds = objsbProjectDetailsService;
                pdetails = objsbProjectDetailsService.ToPagedList(pageIndex, pageSize);
                if (objsbProjectDetailsService.Count == 0)
                {
                    ViewBag.Message = "Oops! No Project Found";
                }
                return View(pdetails);
            }
            catch (Exception ee)
            {
                ViewBag.Message = "Error Occure";
                return View();
            }
        }

        [HttpGet]
        public ActionResult UserSearch(string query, sbAdministrator admin, int? page)
        {
            try
            {
                int pageSize = 6;
                int pageIndex = 1;
                pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
                IPagedList<sbAdministrator> pdetails = null;
                sbAdministrator sb = new sbAdministrator();
                List<sbAdministrator> objsbAdministrator = new List<sbAdministrator>();
                sb.Query = admin.Query;
                objsbAdministrator = admin.SearchAllUsersBind();
                sb.pds = objsbAdministrator;
                pdetails = objsbAdministrator.ToPagedList(pageIndex, pageSize);
                if (objsbAdministrator.Count == 0)
                {
                    ViewBag.Message = "Oops! No User Found";
                }
                return View(pdetails);
            }
            catch (Exception ee)
            {
                ViewBag.Message = "Error Occure";
                return View();
            }
        }

    }
}