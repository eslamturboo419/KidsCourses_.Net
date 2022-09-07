using KProj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace KProj.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }
 
     
        


        [HttpGet]
        public ActionResult ContactUs()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ContactUs(GmailClass gmailClass)
        {
            if (ModelState.IsValid)
            {
                SendVirtifcationCode(gmailClass.YourEmail, gmailClass.FName, gmailClass.LName, gmailClass.Message);
                ViewBag.MessageGmail = "Ok Send";
                Thread.Sleep(1000);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [NonAction]
        public void SendVirtifcationCode(string YourEmail, string Name, string PhoneNumber, string Message)
        {


            var FromEmail = new MailAddress("eslamturboo419@gmail.com", "turbooo");
            var ToEmail = new MailAddress("eslamturboo419@gmail.com");
            var FromEmailPass = "amramr987654321";



            string Subject = "from   //// " + Name + "   " + PhoneNumber;
            string Body = "<br/><br/><br/><br/> " + Message + " <br/><br/>";


            var stmp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential(FromEmail.Address, FromEmailPass)
            };

            using (var message = new MailMessage(FromEmail, ToEmail)
            {
                Subject = Subject,
                Body = Body,
                IsBodyHtml = true
            })

                try
                {
                    stmp.Send(message);
                }
                catch (Exception)
                {
                    throw;
                }


        }

    }
}