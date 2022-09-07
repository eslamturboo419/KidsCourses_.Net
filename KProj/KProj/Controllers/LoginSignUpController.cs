using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using KProj.Models;
using KProj.Models.Classes;

namespace KProj.Controllers
{
    [AllowAnonymous]
    public class LoginSignUpController : Controller
    {
        private KidsDBEntities db = new KidsDBEntities();

        [HttpGet]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("index", "home");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginVM login)
        {
             
            if(ModelState.IsValid)
            {
                var countUser = db.UserDatas.Where(x => x.Email == login.Email && x.Password == login.Password ).FirstOrDefault();
                if(countUser ==null)
                {
              
                    ViewBag.error = "Error Login";
                    return View(login);
                }
                else
                {
                    if (countUser.IsEmailVerified == false)
                    {
                        ViewBag.Message = "Please Confirm Your Account , Go to Your Email";
                        return View(login);
                    }
                    FormsAuthentication.SetAuthCookie(login.Email, login.RememberMe);

                    //if (countUser.Type.Name == "Admin")
                    //{
                    //    return RedirectToAction("Dash", "Dash");
                    //}

                    return RedirectToAction("PersonalData", "Account");
                }


            }

            
            return View(login);
        }

        [HttpGet]
        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("index", "home");
            }

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterVM register , int TypeUser)
        {
            bool Status = false;
            string Message = "";

            if(ModelState.IsValid)
            {

                //// Email is Already Exist
                var isexist = IsEmailExist((register.Email).ToString());
                if (isexist)
                {
                    Message = "this Email Is Already Exist";
                    ViewBag.Message = Message;
                    return View(register);
                }




                // save To DataBase       
                UserData data = new UserData();
                data.ActivationCode = Guid.NewGuid();
                data.IsEmailVerified = false;
                data.F_Name = register.FirstName;
                data.L_Name = register.LastName;
                data.Email = register.Email;
                data.Password = register.Password;
                data.Type_Id = TypeUser;

                db.UserDatas.Add(data);
                db.SaveChanges();

                // send email to user
                SendVirtifcationCode(register.Email, data.ActivationCode.ToString());
                Message = "Registeration Success" + register.Email+"Please Go to Your Email To Complete Register";
                Status = true;


            }
            else
            {
                Message = "Invalid Request";
            }


            ViewBag.Message = Message;
            ViewBag.Status = Status;
            return View(register);
        }


        // verify Account
        [HttpGet]
        public ActionResult verifyAccount(string id)
        {
          

            db.Configuration.ValidateOnSaveEnabled = false;  // to avoid confirm password dosn't match 

            var x = db.UserDatas.Where(a => a.ActivationCode == new Guid(id)).FirstOrDefault();

            if (x != null)
            {
                x.IsEmailVerified = true;
                db.SaveChanges();
                 
                return RedirectToAction("Login", "LoginSignUp");
            }
            else
            {
                ViewBag.Message = "Invalid Request";
            }



           
            return View();



        }


        // forget password
        public ActionResult ForgetPass()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgetPass(string email)
        {
            string Message = "";
            var account = db.UserDatas.Where(x => x.Email == email).FirstOrDefault();
            if (account != null)
            {
                // send email
                string restpasscode = Guid.NewGuid().ToString();

                SendVirtifcationCode(account.Email, restpasscode, "RestPassword");
                account.ResetPassword = restpasscode;

                db.Configuration.ValidateOnSaveEnabled = false; // confirm password
                db.SaveChanges();

                Message = "The Link Has Been Sent To This Email";

            }
            else
            {
                Message = "Account Not Found";
            }

            ViewBag.Message = Message;
            return View();
        }

        /// Rest Password
        [HttpGet]
        public ActionResult RestPassword(string id)
        {
            var user = db.UserDatas.Where(x => x.ResetPassword == id).FirstOrDefault();
            if(user!=null)
            {
                RestPasswordClass restPassword = new RestPasswordClass();
                restPassword.RestCode = id;
                return View(restPassword);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RestPassword(RestPasswordClass restcls)
        {
            string Message = "";
            if (ModelState.IsValid)
            {
                var user = db.UserDatas.Where(x => x.ResetPassword == restcls.RestCode).FirstOrDefault();
                if(user !=null)
                {
                    user.Password = restcls.NewPassword;
                    user.ResetPassword = "";

                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.SaveChanges();
                    Message = "New Password Is Ok Changed ";
                    Thread.Sleep(3000);
                    return RedirectToAction("Login", "LoginSignUp");
                }

            }
            else
            {
                Message = "NOTTTTTTT Ok";
            }
            ViewBag.Message = Message;
            return View(restcls);

        }




        public ActionResult logOut()
        {
            if (User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }









        [NonAction]
        public bool IsEmailExist(string email)
        {
            var x = db.UserDatas.Where(a => a.Email == email).FirstOrDefault();
            return x != null;
        }

        [NonAction]
        public void SendVirtifcationCode(string emaiId, string ActiveCode, string emailfo = "verifyAccount")
        {

            var verifyurl = "/LoginSignUp/" + emailfo + "/" + ActiveCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyurl);


            var FromEmail = new MailAddress("eslamturboo419@gmail.com", "turbooo");
            var ToEmail = new MailAddress(emaiId);
            var FromEmailPass = "amramr987654321";


            string Subject = "", Body = "";
            if (emailfo == "verifyAccount")
            {
                Subject = "Your Account Is Register To My Website";
                Body = "<br/><br/><br/><br/>  Please Click Here To Return To My Website <br/><br/>" +
                   "<a class='btn btn-primary' href='" + link + "'> " + link + "</a>";
            }
            else if (emailfo == "RestPassword")
            {
                Subject = "Reset Your Password";
                Body = "Hi , You Are Now Rest Your Password .... click here <a href='" + link + "'>  Rest Password </a>";
            }



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