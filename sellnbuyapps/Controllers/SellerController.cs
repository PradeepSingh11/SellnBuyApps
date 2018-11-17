using sellnbuyapps.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Microsoft.ApplicationBlocks.Data;

namespace sellnbuyapps.Controllers
{
    public class SellerController : Controller
    {
        // GET: Seller
        public ActionResult Index(sbProjectDetailsService sbProject, int? page)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("../Login/Login");
            }
            else
            {
                int pageSize = 12;
                int pageIndex = 1;
                pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
                IPagedList<sbProjectDetailsService> pdetails = null;
                sbProjectDetailsService sb = new sbProjectDetailsService();
                List<sbProjectDetailsService> objProjectDetails = new List<sbProjectDetailsService>();
                objProjectDetails = sbProject.AllProjectsBind();
                sb.pds = objProjectDetails;
                pdetails = objProjectDetails.ToPagedList(pageIndex, pageSize);
                if (objProjectDetails.Count == 0)
                {
                    ViewBag.Message = "No Record Found";
                }
                return View(pdetails);
            }
        }
        [HttpGet]
        public ActionResult AddProject()
        {
            string constr = ConfigurationManager.AppSettings["sqlConnectionString"].ToString();
            SqlConnection con = new SqlConnection(constr);
            SqlDataAdapter adp = new SqlDataAdapter("Select * From sb_ProjectCategoryMaster", con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            ViewBag.CategoryList = ToSelectList(dt, "Id", "CategoryName");

            SqlDataAdapter adapter = new SqlDataAdapter("Select * from sb_TechnologyMaster", con);
            DataTable dt1 = new DataTable();
            adapter.Fill(dt1);
            ViewBag.TechnologyList = ToSelectList(dt1, "Id", "TechnologyName");

            return View();
        }
        [NonAction]
        public SelectList ToSelectList(DataTable table, string valueField, string textField)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (DataRow row in table.Rows)
            {
                list.Add(new SelectListItem()
                {
                    Text = row[textField].ToString(),
                    Value = row[valueField].ToString()
                });
            }
            return new SelectList(list, "Value", "Text");
        }

        [HttpPost]
        public ActionResult AddProject(sbProjectDetailsService sbProject, HttpPostedFileBase fileUpload)
        {
            try
            {
                if (Session["username"] == null)
                {
                    return RedirectToAction("../Login/Login");
                }
                else
                {
                    System.Drawing.Image image = System.Drawing.Image.FromStream(fileUpload.InputStream, true, true);

                    string[] sAllowedExt = new string[] { ".jpg", ".gif", ".jpeg" };
                    Home objHome = new Home();
                    sbProjectDetailsService objsbProjectDetailsService = new sbProjectDetailsService();
                    if (string.IsNullOrEmpty(sbProject.ProjectCategoryId.ToString()))
                    {
                        ModelState.AddModelError("ProjectCategoryId", "Please select project category");
                    }
                    if (string.IsNullOrEmpty(sbProject.ProjectName))
                    {
                        ModelState.AddModelError("ProjectName", "Please enter project name");
                    }
                    if (string.IsNullOrEmpty(sbProject.Cost.ToString()))
                    {
                        ModelState.AddModelError("Cost", "Please enter project cost");
                    }
                    if (string.IsNullOrEmpty(sbProject.Features))
                    {
                        ModelState.AddModelError("Features", "Please enter features");
                    }
                    if (string.IsNullOrEmpty(sbProject.Description))
                    {
                        ModelState.AddModelError("Description", "Please enter description");
                    }
                    if (string.IsNullOrEmpty(sbProject.Technology.ToString()))
                    {
                        ModelState.AddModelError("Technology", "Please enter Technology");
                    }
                    if (fileUpload == null)
                    {
                        ModelState.AddModelError("Screenshot", "Please select Image file");
                    }

                    else if (!sAllowedExt.Contains(fileUpload.FileName.Substring(fileUpload.FileName.LastIndexOf('.'))))
                    {
                        ModelState.AddModelError("Screenshot", "Please upload Your Image of type: " + string.Join(", ", sAllowedExt));
                        AddProject();
                    }
                    else if (image.Width != 838 || image.Height != 486)
                    {
                        ModelState.AddModelError("Screenshot", "Please select image with size: 838 * 486");
                        AddProject();
                        return View();

                    }
                    if (ModelState.IsValid)
                    {
                        if (fileUpload != null)
                        {
                            string fileName = Path.GetFileName(fileUpload.FileName);
                            string path = "/ProjectImages/" + fileName;
                            fileUpload.SaveAs(Server.MapPath(path));
                            DataSet ds = objHome.GetUserId();
                            int userId = Convert.ToInt32(ds.Tables[0].Rows[0]["Id"].ToString());
                            objsbProjectDetailsService.ProjectCategoryId = sbProject.ProjectCategoryId;
                            objsbProjectDetailsService.ProjectName = sbProject.ProjectName;
                            objsbProjectDetailsService.Cost = sbProject.Cost;
                            objsbProjectDetailsService.Features = sbProject.Features;
                            objsbProjectDetailsService.Description = sbProject.Description;
                            objsbProjectDetailsService.Technology = sbProject.Technology;
                            objsbProjectDetailsService.Demo = sbProject.Demo;
                            objsbProjectDetailsService.IsCustomizable = sbProject.IsCustomizable;
                            objsbProjectDetailsService.Screenshot = path;
                            objsbProjectDetailsService.Status = sbProject.Status;
                            objsbProjectDetailsService.IsFree = sbProject.IsFree;
                            objsbProjectDetailsService.CreatedId = userId;

                            bool IsInserted = false;
                            IsInserted = objsbProjectDetailsService.AddSb_ProjectDetails();
                            if (IsInserted)
                            {
                                ViewBag.Message = "Information has been saved successfully";
                            }
                            ModelState.Clear();
                            ModelState.Remove("ProjectCategory");
                            sbProject.ProjectCategoryId = 0;
                            ModelState.Remove("ProjectName");
                            sbProject.ProjectName = "";
                            ModelState.Remove("Cost");
                            sbProject.Cost = 0;
                            ModelState.Remove("Features");
                            sbProject.Features = "";
                            ModelState.Remove("Description");
                            sbProject.Description = "";
                            ModelState.Remove("Technology");
                            sbProject.Technology = 0;
                            ModelState.Remove("Demo");
                            sbProject.Demo = false;
                            ModelState.Remove("IsCustomizable");
                            sbProject.IsCustomizable = false;
                            ModelState.Remove("Screenshot");
                            sbProject.Screenshot = null;
                            ModelState.Remove("Status");
                            sbProject.Status = false;
                            ModelState.Remove("IsFree");
                            sbProject.IsFree = false;

                        }
                    }
                    AddProject();
                    return View(sbProject);
                }
            }
            catch (Exception ee)
            {
                ViewBag.Message = "Error Occurred: Some fields are missing";
                AddProject();
                return View();
            }
        }

        public ActionResult UpdateProject(int? id, sbProjectDetailsService sbProject)
        {
            AddProject();
            DataSet ds = new DataSet();
            ds = sbProject.GetByProductID_Sb_ProjectDetails();
            if (ds.Tables[0].Rows.Count > 0)
            {
                sbProject.ProjectCategoryId = Convert.ToInt32(ds.Tables[0].Rows[0]["ProjectCategoryId"].ToString());
                sbProject.ProjectName = ds.Tables[0].Rows[0]["ProjectName"].ToString();
                sbProject.Cost = Convert.ToInt32(ds.Tables[0].Rows[0]["Cost"].ToString());
                sbProject.Features = ds.Tables[0].Rows[0]["Features"].ToString();
                sbProject.Description = ds.Tables[0].Rows[0]["Description"].ToString();
                sbProject.Technology = Convert.ToInt32(ds.Tables[0].Rows[0]["TechnologyId"].ToString());
                sbProject.Demo = Convert.ToBoolean(ds.Tables[0].Rows[0]["Demo"].ToString());
                sbProject.IsCustomizable = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsCustomizable"].ToString());
                sbProject.Screenshot = ds.Tables[0].Rows[0]["Screenshot"].ToString();
                TempData["PreviousImage"] = sbProject.Screenshot;
                sbProject.IsFree = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsFree"].ToString());
            }
            return View(sbProject);
        }

        [HttpPost]
        public ActionResult UpdateProject(int id, sbProjectDetailsService sbProject, HttpPostedFileBase uploadImage)
        {
            try
            {
                if (Session["username"] == null)
                {
                    return RedirectToAction("../Login/Login");
                }
                else
                {
                    if (string.IsNullOrEmpty(sbProject.ProjectCategoryId.ToString()))
                    {
                        ModelState.AddModelError("ProjectCategoryId", "Please select project category");
                    }
                    if (string.IsNullOrEmpty(sbProject.ProjectName))
                    {
                        ModelState.AddModelError("ProjectName", "Please enter project name");
                    }
                    if (string.IsNullOrEmpty(sbProject.Cost.ToString()))
                    {
                        ModelState.AddModelError("Cost", "Please enter project cost");
                    }
                    if (string.IsNullOrEmpty(sbProject.Features))
                    {
                        ModelState.AddModelError("Features", "Please enter features");
                    }
                    if (string.IsNullOrEmpty(sbProject.Description))
                    {
                        ModelState.AddModelError("Description", "Please enter description");
                    }
                    if (string.IsNullOrEmpty(sbProject.Technology.ToString()))
                    {
                        ModelState.AddModelError("Technology", "Please enter Technology");
                    }
                    if (ModelState.IsValid)
                    {
                        if (id != 0)
                        {
                            string path = null;
                            if (uploadImage != null)
                            {
                                string fileName = Path.GetFileName(uploadImage.FileName);
                                path = "/ProjectImages/" + fileName;
                                uploadImage.SaveAs(Server.MapPath(path));
                            }
                            string sqlConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"].ToString();
                            SqlConnection sqlConnection = new SqlConnection(sqlConnectionString);
                            SqlCommand command = new SqlCommand("select CreatedId from sb_ProjectDetails where Id =" + id, sqlConnection);
                            command.Connection.Open();
                            int userId = Convert.ToInt32(command.ExecuteScalar());
                            command.Connection.Close();
                            sbProjectDetailsService objsbProjectDetailsService = new sbProjectDetailsService();
                            objsbProjectDetailsService.ProjectCategoryId = sbProject.ProjectCategoryId;
                            objsbProjectDetailsService.ProjectName = sbProject.ProjectName;
                            objsbProjectDetailsService.Cost = sbProject.Cost;
                            objsbProjectDetailsService.Features = sbProject.Features;
                            objsbProjectDetailsService.Description = sbProject.Description;
                            objsbProjectDetailsService.Technology = sbProject.Technology;
                            objsbProjectDetailsService.Demo = sbProject.Demo;
                            objsbProjectDetailsService.IsCustomizable = sbProject.IsCustomizable;
                            if (path != null)
                            {
                                objsbProjectDetailsService.Screenshot = path;
                            }
                            else
                            {
                                objsbProjectDetailsService.Screenshot = TempData["PreviousImage"].ToString();
                            }
                            objsbProjectDetailsService.Status = sbProject.Status;
                            objsbProjectDetailsService.IsFree = sbProject.IsFree;
                            objsbProjectDetailsService.CreatedId = userId;
                            objsbProjectDetailsService.Id = id;

                            bool IsUpdated = false;
                            IsUpdated = objsbProjectDetailsService.UpdateSb_ProjectDetails();
                            if (IsUpdated)
                            {
                                ViewBag.msg = "Information has been updated successfully";
                            }
                            ModelState.Clear();
                            ModelState.Remove("ProjectCategory");
                            sbProject.ProjectCategoryId = 0;
                            ModelState.Remove("ProjectName");
                            sbProject.ProjectName = "";
                            ModelState.Remove("Cost");
                            sbProject.Cost = 0;
                            ModelState.Remove("Features");
                            sbProject.Features = "";
                            ModelState.Remove("Description");
                            sbProject.Description = "";
                            ModelState.Remove("Technology");
                            sbProject.Technology = 0;
                            ModelState.Remove("Demo");
                            sbProject.Demo = false;
                            ModelState.Remove("IsCustomizable");
                            sbProject.IsCustomizable = false;
                            ModelState.Remove("Screenshot");
                            sbProject.Screenshot = null;
                            ModelState.Remove("Status");
                            sbProject.Status = false;
                            ModelState.Remove("IsFree");
                            sbProject.IsFree = false;
                        }
                    }
                }
                AddProject();
                return View(sbProject);
            }
            catch (Exception ee)
            {
                ViewBag.msg = "Error Occured";
                AddProject();
                return View();
            }

        }
        public ActionResult ViewProject(int id, sbProjectDetailsService sbProject)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("../Login/Login");
            }
            else
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
                }
                return View(sbProject);
            }
        }
        public ActionResult DeleteProject(int? id, sbProjectDetailsService sbProject)
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
            }
            return View(sbProject);
        }
        [HttpPost]
        public ActionResult DeleteProject(int id, sbProjectDetailsService sbProject)
        {
            if (id != 0)
            {
                try
                {
                    sbProjectDetailsService objProjectDetailsService = new sbProjectDetailsService();
                    objProjectDetailsService.Id = id;
                    bool IsDeleted = false;
                    IsDeleted = objProjectDetailsService.DeleteByID_Sb_ProjectDetails();
                    if (IsDeleted)
                    {
                        ViewBag.Message = "Information has been deleted successfully";
                    }
                }
                catch (Exception ee)
                {
                    ViewBag.Message = "Error Occurred";
                }
            }
            return RedirectToAction("Projects", "ProjectDetails");
        }
        public ActionResult Update(Home home)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("../Login/Login");
            }
            else
            {
                DataSet ds = new DataSet();
                ds = home.GetUserId();
                int userId = Convert.ToInt32(ds.Tables[0].Rows[0]["Id"].ToString());
                return RedirectToAction("Update", "Register", new { id = userId });
            }
        }
        [HttpGet]
        public ActionResult Search(string Query, sbProjectDetailsService sbProject, int? page)
        {
            int pageSize = 12;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<sbProjectDetailsService> pdetails = null;
            sbProjectDetailsService sb = new sbProjectDetailsService();
            List<sbProjectDetailsService> objProjectDetails = new List<sbProjectDetailsService>();
            ViewBag.SearchResult = Query;
            sb.Query = sbProject.Query;
            objProjectDetails = sbProject.SearchAllProjectsBind();
            sb.pds = objProjectDetails;
            pdetails = objProjectDetails.ToPagedList(pageIndex, pageSize);
            if (objProjectDetails.Count == 0)
            {
                ViewBag.Message = "Oops! No Record Found";
            }
            return View(pdetails);
        }
        [HttpGet]
        public ActionResult ContactUs()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ContactUs(ContactUs contactus)
        {
            try
            {
                ContactUs objsbContactUs = new ContactUs();
                objsbContactUs.Name = contactus.Name;
                objsbContactUs.EmailId = contactus.EmailId;
                objsbContactUs.Subject = contactus.Subject;
                objsbContactUs.Comments = contactus.Comments;

                bool IsInserted = false;
                IsInserted = objsbContactUs.AddSb_ContactUs(); //add contacts in database
                if (IsInserted)
                {
                    ViewBag.Message = "We will contact with you shortly";
                }
            }
            catch (Exception ee)
            {
                ViewBag.Message = "Error occurred";
            }
            //Empty Controls
            ModelState.Clear();
            ModelState.Remove("Name");
            contactus.Name = "";
            ModelState.Remove("EmailId");
            contactus.EmailId = "";
            ModelState.Remove("Subject");
            contactus.Subject = "";
            ModelState.Remove("Comments");
            contactus.Comments = "";
            return View(contactus);
        }

        public ActionResult Blog(Home user, int? page)
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
                ViewBag.Message = "Comment posted successfully";
            }
            return RedirectToAction("Blog");
        }

        public ActionResult SingleProject(int? Id, Home details)
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
            }
            return View(details);
        }

        [HttpGet]
        public ActionResult AboutUs()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CategorySearch(string Query, sbProjectDetailsService sbProject, int? page)
        {
            int pageSize = 12;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<sbProjectDetailsService> pdetails = null;
            sbProjectDetailsService sb = new sbProjectDetailsService();
            List<sbProjectDetailsService> objProjectDetails = new List<sbProjectDetailsService>();
            ViewBag.SearchResult = Query;
            sb.Query = sbProject.Query;
            objProjectDetails = sbProject.ProjectCategorySearch();
            sb.pds = objProjectDetails;
            pdetails = objProjectDetails.ToPagedList(pageIndex, pageSize);
            if (objProjectDetails.Count == 0)
            {
                ViewBag.Message = "Oops! No Record Found";
            }
            return View(pdetails);
        }

        [HttpGet]
        public ActionResult TechnologySearch(string Query, sbProjectDetailsService sbProject, int? page)
        {
            int pageSize = 12;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<sbProjectDetailsService> pdetails = null;
            sbProjectDetailsService sb = new sbProjectDetailsService();
            List<sbProjectDetailsService> objProjectDetails = new List<sbProjectDetailsService>();
            ViewBag.SearchResult = Query;
            sb.Query = sbProject.Query;
            objProjectDetails = sbProject.ProjectTechnologySearch();
            sb.pds = objProjectDetails;
            pdetails = objProjectDetails.ToPagedList(pageIndex, pageSize);
            if (objProjectDetails.Count == 0)
            {
                ViewBag.Message = "Oops! No Record Found";
            }
            return View(pdetails);
        }

    }
}