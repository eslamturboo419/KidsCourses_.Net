using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KProj.Models;
using KProj.Models.Classes;

namespace KProj.Controllers
{
    [Authorize(Roles = "Admin,Teacher,Child")]
    public class AccountController : Controller
    {
        private KidsDBEntities db = new KidsDBEntities();

        public ActionResult PersonalData(string Email)
        {

            //if (Email.ToLower() != User.Identity.Name.ToLower())
            //{
            //    return View("Error");
            //}
            Email = User.Identity.Name;
            PersonalDataEdit data =  db.UserDatas.Where(x => x.Email == Email).Select(x=> new  PersonalDataEdit { F_Name = x.F_Name , L_Name = x.L_Name  , Address=x.Address , Email=User.Identity.Name , Birthday=x.Birthday  ,HiddenString=User.Identity.Name ,OldPassword = x.Password  ,id= x.Id } )
                .FirstOrDefault();

            if (data == null)
            {
                return View("Error");
            }


            return View(data);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DataEdit(PersonalDataEdit dataEdit)
        {
            if(ModelState.IsValid)
            {
                UserData data = db.UserDatas.Where(x=>x.Id==dataEdit.id&& x.Email==User.Identity.Name).FirstOrDefault();
               
                data.F_Name = dataEdit.F_Name;
                data.L_Name = dataEdit.L_Name;
                data.Email = User.Identity.Name;
                data.Address = dataEdit.Address;
                data.Birthday = dataEdit.Birthday;

                db.Entry(data).State = EntityState.Modified;
                db.SaveChanges();
                TempData["mesage"] = "Update Ok";
                TempData.Keep();
            }
            return RedirectToAction("PersonalData", "Account", new { Email = User.Identity.Name });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DataPassword(PersonalDataEdit dataEdit)
        {
            if(ModelState.IsValid)
            {
                UserData data = db.UserDatas.Where(x => x.Id == dataEdit.id && x.Email == User.Identity.Name).FirstOrDefault();


                db.Configuration.ValidateOnSaveEnabled = false; // confirm password

                data.Password = dataEdit.NewPassword;
                db.SaveChanges();
                TempData["mesage"] = "Update Ok";
                TempData.Keep();
            }
            return RedirectToAction("PersonalData", "Account", new { Email = User.Identity.Name });
        }




    }
}