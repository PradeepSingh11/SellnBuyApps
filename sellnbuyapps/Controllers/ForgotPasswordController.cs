using sellnbuyapps.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace sellnbuyapps.Controllers
{
    public class ForgotPasswordController : Controller
    {
        // GET: ForgotPassword
        
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(sbForgotPassword password)
        {
            try
            {
                sbForgotPassword objsbForgotPassword = new sbForgotPassword();

                objsbForgotPassword.EmailId = password.EmailId;
                DataSet ds = objsbForgotPassword.GetEmailId_Sb_User();
                if (Convert.ToInt32(ds.Tables[0].Rows[0]["Column1"].ToString()) == 1)
                {
                    string resetCode = Guid.NewGuid().ToString();
                    SendVerificationLinkEmail(objsbForgotPassword.EmailId, resetCode, "ResetPassword");

                    objsbForgotPassword.ResetPasswordCode = resetCode;

                    bool IsInserted = false;
                    IsInserted = objsbForgotPassword.AddResetCode();
                   
                    //crypto objcrypto = new crypto();
                    //string resetCode = objcrypto.EnCrypt(objsbForgotPassword.EmailId);
                    //SendVerificationLinkEmail(objsbForgotPassword.EmailId, resetCode, "ResetPassword");
                    //objsbForgotPassword.EmailId = resetCode;
                    ViewBag.Message = "Reset password link has been sent to your email id.";
                }
                else
                {
                    ViewBag.Message = "This email is not registered";
                }
                ModelState.Clear();
                ModelState.Remove("EmailId");
                password.EmailId = "";
                return View(password);
            }
            catch
            {
                ViewBag.Message = "Error Occure";
                return ViewBag.Message;
            }
           
        }

        [NonAction]
        public void SendVerificationLinkEmail(string emailID, string activationCode, string emailFor = "ResetPassword")
        {
            var verifyUrl = "/ForgotPassword/" + emailFor + "/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("support@starappsolutions.com", "Reset Password");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "raj@manoj@123";

            string subject = "";
            string body = "";
            if (emailFor == "VerifyAccount")
            {
                subject = "Your account is successfully created!";
                body = "<br /><br />We are excited to tell you that your Dotnet Awesome account is" +
                    " successfully created. Please click on the below link to verify your account" +
                    " <br/><br/><a href='" + link + "'>" + link + "</a> ";
            }
            else if (emailFor == "ResetPassword")
            {
                subject = "Reset Password";
                body = "Hi,<br/><br/>We got request for reset your account password. Please click on the below link to reset your password" +
                    "<br/><br/><a href=" + link + ">Reset Password link</a>";
            }

            var smtp = new SmtpClient
            {
                Host = "starappsolutions.com",
                Port = 25,
                EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }

        [HttpGet]
        public ActionResult ResetPassword(string id)
        {
            if (id != null)
            {
                ResetPassword model = new ResetPassword();
                model.ResetCode = id;
                return View(model);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult ResetPassword(string id, ResetPassword resetPassword)
        {
            try
            {
                
                SbUserService objUserService = new SbUserService();
                ResetPassword objsbUserService = new ResetPassword();
                sbForgotPassword objForgotPassword = new sbForgotPassword();
                objForgotPassword.ResetPasswordCode = id;
                DataSet ds = objForgotPassword.GetEmailIdBasedOnResetCode();
                objsbUserService.EmailId = ds.Tables[0].Rows[0]["EmailId"].ToString();
                objsbUserService.NewPassword = objUserService.GetEncryptedValue(resetPassword.NewPassword);
                objsbUserService.ConfirmPassword = objUserService.GetEncryptedValue(resetPassword.ConfirmPassword);
                
                bool IsUpdated = false;
                IsUpdated = objsbUserService.UpdatePasswordSb_User();
                if (IsUpdated)
                {
                    ViewBag.Message = "Password changed successfully";
                }
                else
                {
                    return HttpNotFound();
                }
            }
            catch(Exception ee)
            {
                ViewBag.Message = "Error occured please try after sometime";
            }
            ModelState.Clear();
            ModelState.Remove("Password");
            resetPassword.NewPassword = "";
            ModelState.Remove("Password");
            resetPassword.ConfirmPassword = "";
            return View();
           // return RedirectToAction("../Login/Login");
        }
    }
}