using sellnbuyapps.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace sellnbuyapps.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registration(SbUserService sbUser, HttpPostedFileBase fileUpload)
        {
            try
            {
                SbUserService objsbUserService = new SbUserService();
                if (string.IsNullOrEmpty(sbUser.FirstName))
                {
                    ModelState.AddModelError("FirstName", "First Name is required");
                }
                if (string.IsNullOrEmpty(sbUser.LastName))
                {
                    ModelState.AddModelError("LastName", "Last Name is required");
                }
                if (string.IsNullOrEmpty(sbUser.EmailId))
                {
                    ModelState.AddModelError("EmailId", "Email is required");
                }
                if (string.IsNullOrEmpty(sbUser.Password))
                {
                    ModelState.AddModelError("Password", "Password is required");
                }
                if (string.IsNullOrEmpty(sbUser.MobileNumber))
                {
                    ModelState.AddModelError("MobileNumber", "Mobile Number is required");
                }
                if (fileUpload == null)
                {
                    ModelState.AddModelError("ProfilePicture", "Profile Picture is required");
                }
                if (!string.IsNullOrEmpty(sbUser.EmailId))
                {
                    objsbUserService.EmailId = Convert.ToString(sbUser.EmailId);

                    bool IsExist = false;
                    IsExist = objsbUserService.IsEmailExists();
                    if (IsExist)
                    {
                        ViewBag.Message = "Email already Registered";
                        return View();
                    }
                }
                if (fileUpload != null)
                {
                    System.Drawing.Image image = System.Drawing.Image.FromStream(fileUpload.InputStream, true, true);
                    string[] sAllowedExt = new string[] { ".jpg", ".gif", ".jpeg" };

                    if (!sAllowedExt.Contains(fileUpload.FileName.Substring(fileUpload.FileName.LastIndexOf('.'))))
                    {
                        ModelState.AddModelError("ProfilePicture", "Please upload Your Image of type: " + string.Join(", ", sAllowedExt));
                        return View();
                    }
                    else if (image.Width != image.Height)
                    {
                        ModelState.AddModelError("ProfilePicture", "Please select image having same height and width");
                        return View();
                    }
                }
                
                if (ModelState.IsValid)
                {
                    if (fileUpload != null)
                    {

                        string fileName = Path.GetFileName(fileUpload.FileName);
                        string path = "/ProfilePicture/" + fileName;
                        fileUpload.SaveAs(Server.MapPath(path));
                        objsbUserService.FirstName = sbUser.FirstName;
                        objsbUserService.LastName = sbUser.LastName;
                        objsbUserService.EmailId = sbUser.EmailId;
                        objsbUserService.Password = objsbUserService.GetEncryptedValue(sbUser.Password);
                        objsbUserService.MobileNumber = sbUser.MobileNumber;
                        objsbUserService.CompanyURL = sbUser.CompanyURL;
                        //objsbUserService.IsSeller = sbUser.IsSeller;
                        objsbUserService.ProfilePicture = path;
                        objsbUserService.Status = sbUser.Status;

                        bool IsInserted = false;
                        IsInserted = objsbUserService.AddSb_User();
                        if (IsInserted)
                        {
                            ViewBag.Message = "Information has been saved successfully";
                        }
                        ModelState.Clear();
                        ModelState.Remove("FirstName");
                        sbUser.FirstName = "";
                        ModelState.Remove("LastName");
                        sbUser.LastName = "";
                        ModelState.Remove("EmailId");
                        sbUser.EmailId = "";
                        ModelState.Remove("Password");
                        sbUser.Password = "";
                        ModelState.Remove("MobileNumber");
                        sbUser.MobileNumber = "";
                        ModelState.Remove("CompanyURL");
                        sbUser.CompanyURL = "";
                        ModelState.Remove("ProfilePicture");
                        sbUser.ProfilePicture = null;
                        //ModelState.Remove("IsSeller");
                        //sbUser.IsSeller = false;
                    }
                }
                return View(sbUser);

            }
            catch (Exception ee)
            {
                ViewBag.Message = "Error Occurred";
                return View();
            }

        }

        [HttpGet]
        public ActionResult Update(int? id, SbUserService sbUser)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = sbUser.GetByID_Sb_User();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    sbUser.FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
                    sbUser.LastName = ds.Tables[0].Rows[0]["LastName"].ToString();
                    sbUser.EmailId = ds.Tables[0].Rows[0]["EmailId"].ToString();
                    sbUser.MobileNumber = ds.Tables[0].Rows[0]["MobileNumber"].ToString();
                    sbUser.CompanyURL = ds.Tables[0].Rows[0]["CompanyURL"].ToString();
                    sbUser.ProfilePicture = ds.Tables[0].Rows[0]["ProfilePicture"].ToString();
                    TempData["PreviousImage"] = sbUser.ProfilePicture;
                }
                return View(sbUser);
            }
            catch (Exception ee)
            {
                ViewBag.Message = "Error Occured";
                return ViewBag.Message;
            }

        }

        [HttpPost]
        public ActionResult Update(int id, SbUserService sbUser, HttpPostedFileBase profilepicture)
        {
            if (id != 0)
            {
                try
                {
                    SbUserService objsbUserService = new SbUserService();
                    //System.Drawing.Image image = System.Drawing.Image.FromStream(profilepicture.InputStream, true, true);
                    //string[] sAllowedExt = new string[] { ".jpg", ".gif", ".jpeg" };

                    //if (!sAllowedExt.Contains(profilepicture.FileName.Substring(profilepicture.FileName.LastIndexOf('.'))))
                    //{
                    //    ModelState.AddModelError("ProfilePicture", "Please upload Your Image of type: " + string.Join(", ", sAllowedExt));
                    //    return View();
                    //}
                    //else if (image.Width != image.Height)
                    //{
                    //    ModelState.AddModelError("ProfilePicture", "Please select image having ratio 1:1");
                    //    return View();
                    //}

                    if (string.IsNullOrEmpty(sbUser.FirstName))
                    {
                        ModelState.AddModelError("FirstName", "First Name is required");
                    }
                    if (string.IsNullOrEmpty(sbUser.LastName))
                    {
                        ModelState.AddModelError("LastName", "Last Name is required");
                    }
                    if (string.IsNullOrEmpty(sbUser.EmailId))
                    {
                        ModelState.AddModelError("EmailId", "Email is required");
                    }
                    if (string.IsNullOrEmpty(sbUser.MobileNumber))
                    {
                        ModelState.AddModelError("MobileNumber", "Mobile Number is required");
                    }
                    if (!string.IsNullOrEmpty(sbUser.FirstName))
                    {
                        string path = null;
                        if (profilepicture != null)
                        {
                            string fileName = Path.GetFileName(profilepicture.FileName);
                            path = "/ProfilePicture/" + fileName;
                            profilepicture.SaveAs(Server.MapPath(path));
                        }
                        objsbUserService.ID = id;
                        objsbUserService.FirstName = sbUser.FirstName;
                        objsbUserService.LastName = sbUser.LastName;
                        objsbUserService.EmailId = sbUser.EmailId;
                        objsbUserService.MobileNumber = sbUser.MobileNumber;
                        if (path != null)
                        {
                            objsbUserService.ProfilePicture = path;
                        }
                        else
                        {
                            objsbUserService.ProfilePicture = TempData["PreviousImage"].ToString();
                        }
                        objsbUserService.CompanyURL = sbUser.CompanyURL;
                        objsbUserService.Status = sbUser.Status;

                        bool IsUpdated = false;
                        IsUpdated = objsbUserService.UpdateSb_User();
                        if (IsUpdated)
                        {
                            ViewBag.Message = "Information has been updated successfully";
                        }
                    }
                }
                catch (Exception ee)
                {
                    ViewBag.Message = "Error Occurred";
                }
            }
            return View(sbUser);
        }

        public ActionResult Delete(int? id, SbUserService sbUser)
        {
            DataSet ds = new DataSet();
            ds = sbUser.GetByID_Sb_User();
            if (ds.Tables[0].Rows.Count > 0)
            {
                sbUser.FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
                sbUser.LastName = ds.Tables[0].Rows[0]["LastName"].ToString();
                sbUser.EmailId = ds.Tables[0].Rows[0]["EmailId"].ToString();
                sbUser.Password = ds.Tables[0].Rows[0]["Password"].ToString();
                sbUser.MobileNumber = ds.Tables[0].Rows[0]["MobileNumber"].ToString();
                sbUser.CompanyURL = ds.Tables[0].Rows[0]["CompanyURL"].ToString();
                //sbUser.IsSeller = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsSeller"].ToString());
                sbUser.Status = Convert.ToBoolean(ds.Tables[0].Rows[0]["Status"].ToString());
                if (Convert.ToString(ds.Tables[0].Rows[0]["ModifiedOn"]) == "")
                {
                    sbUser.ModifiedOn = "";
                }
                else
                {
                    sbUser.ModifiedOn = ds.Tables[0].Rows[0]["ModifiedOn"].ToString();
                }
            }
            return View(sbUser);

        }

        [HttpPost]
        public ActionResult Delete(int id, SbUserService sbUser)
        {
            if (id != 0)
            {
                try
                {
                    SbUserService objsbUserService = new SbUserService();
                    objsbUserService.ID = id;
                    bool IsDeleted = false;
                    IsDeleted = objsbUserService.DeleteByID_Sb_User();
                    if (IsDeleted)
                    {
                        ViewBag.Message = "Information has been deleted successfully";
                    }
                }
                catch (Exception ee)
                {
                    ViewBag.Message = "Error Occurred:" + ee;
                }

            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(int id, SbUserService sbUser)
        {
            DataSet ds = new DataSet();
            ds = sbUser.GetByID_Sb_User();
            if (ds.Tables[0].Rows.Count > 0)
            {
                sbUser.FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
                sbUser.LastName = ds.Tables[0].Rows[0]["LastName"].ToString();
                sbUser.EmailId = ds.Tables[0].Rows[0]["EmailId"].ToString();
                sbUser.Password = ds.Tables[0].Rows[0]["Password"].ToString();
                sbUser.MobileNumber = ds.Tables[0].Rows[0]["MobileNumber"].ToString();
                sbUser.CompanyURL = ds.Tables[0].Rows[0]["CompanyURL"].ToString();
                //sbUser.IsSeller = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsSeller"].ToString());
                sbUser.Status = Convert.ToBoolean(ds.Tables[0].Rows[0]["Status"].ToString());
                if (Convert.ToString(ds.Tables[0].Rows[0]["ModifiedOn"]) == "")
                {
                    sbUser.ModifiedOn = "";
                }
                else
                {
                    sbUser.ModifiedOn = ds.Tables[0].Rows[0]["ModifiedOn"].ToString();
                }
            }
            return View(sbUser);

        }

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
    }
}