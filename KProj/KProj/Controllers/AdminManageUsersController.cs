using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KProj.Models.Classes;

namespace KProj.Controllers
{

    [Authorize(Roles = "Admin")]
    public class AdminManageUsersController : Controller
    {
        private KidsDBEntities db = new KidsDBEntities(); 
      
        public ActionResult Index()
        {
           var val =  db.UserDatas.Where(x=>x.Email !=User.Identity.Name) .ToList();
            return View(val);
        }

        public ActionResult Create()
        {
            ViewBag.Type_Id = new SelectList(db.Types, "Id", "Name");
            return View();
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(  UserData userData)
        {
            if (ModelState.IsValid)
            {
                userData.IsEmailVerified = true;
                db.UserDatas.Add(userData);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Type_Id = new SelectList(db.Types, "Id", "Name", userData.Type_Id);
            return View(userData);
        }

        
        public ActionResult Edit(int id)
        {
           
            UserData userData = db.UserDatas.Find(id);
            if (userData == null)
            {
                return HttpNotFound();
            }
            ViewBag.Type_Id = new SelectList(db.Types, "Id", "Name", userData.Type_Id);
            return View(userData);
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(  UserData userData)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userData).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Type_Id = new SelectList(db.Types, "Id", "Name", userData.Type_Id);
            return View(userData);
        }




        public JsonResult Delete(int? EmployeeId)
        {
            bool result = false;
            if (EmployeeId == null)
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            var s = db.UserDatas.Where(x => x.Id == EmployeeId).FirstOrDefault();
            if (s != null)
            {
                db.UserDatas.Remove(s);
                db.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }



    }
}