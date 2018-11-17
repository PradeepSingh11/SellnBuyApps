
using sellnbuyapps.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace sellnbuyapps.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login login)
        {
            try
            {
                SbUserService objUserservice = new SbUserService();
                if (!string.IsNullOrEmpty(login.EmailId))
                {
                    Login objLogin = new Login();
                    objLogin.EmailId = login.EmailId;
                    string password = login.Password;
                    objLogin.Password = objUserservice.GetEncryptedValue(password);
                    DataSet ds = objLogin.GetData_Sb_User();

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Session["username"] = ds.Tables[0].Rows[0]["EmailId"].ToString();
                        Session["Name"] = ds.Tables[0].Rows[0]["FirstName"].ToString();
                        Session["ProfilePicture"] = ds.Tables[0].Rows[0]["ProfilePicture"].ToString();
                        return RedirectToAction("../Seller/Index");
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

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(Login login)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("../Login/Login");
            }
            else
            {
                SbUserService objUserService = new SbUserService();
                if (string.IsNullOrEmpty(login.EmailId))
                {
                    ModelState.AddModelError("EmailId", "Please enter Email Id");
                }
                if (string.IsNullOrEmpty(login.OldPassword))
                {
                    ModelState.AddModelError("OldPassword", "Please enter old password");
                }
                if (string.IsNullOrEmpty(login.NewPassword))
                {
                    ModelState.AddModelError("NewPassword", "Please enter new password");
                }
                if (!ModelState.IsValid)
                {
                    if (!string.IsNullOrEmpty(login.EmailId))
                    {
                        Login objsbLoginService = new Login();

                        objsbLoginService.EmailId = login.EmailId;
                        objsbLoginService.OldPassword = objUserService.GetEncryptedValue(login.OldPassword);
                        DataSet ds = objsbLoginService.GetEmailId_Sb_User();

                        if (Convert.ToInt32(ds.Tables[0].Rows[0]["Column1"].ToString()) == 1)
                        {
                            objsbLoginService.EmailId = login.EmailId;
                            objsbLoginService.NewPassword = objUserService.GetEncryptedValue(login.NewPassword);

                            bool IsUpdated = false;
                            IsUpdated = objsbLoginService.UpdateSb_User();
                            if (IsUpdated)
                            {
                                ViewBag.Message = "Password changed successfully";
                            }
                            else
                            {
                                ViewBag.Message = "Error occured please try after sometime";
                            }
                        }
                        if (Convert.ToInt32(ds.Tables[0].Rows[0]["Column1"].ToString()) == 0)
                        {
                            ViewBag.Message = "Invalid EmailId";
                        }
                        if (Convert.ToInt32(ds.Tables[0].Rows[0]["Column1"].ToString()) == -1)
                        {
                            ViewBag.Message = "Invalid EmailId or password";
                        }
                    }
                }
                ModelState.Clear();
                ModelState.Remove("EmailId");
                login.EmailId = "";
                ModelState.Remove("Password");
                login.OldPassword = "";
                ModelState.Remove("Password");
                login.NewPassword = "";
                return View(login);
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

            return RedirectToAction("Login", "Login");
        }
    }
}