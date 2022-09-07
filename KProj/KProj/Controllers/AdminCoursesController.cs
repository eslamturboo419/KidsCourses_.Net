using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KProj.Models.Classes;

namespace KProj.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminCoursesController : Controller
    {
        private KidsDBEntities db = new KidsDBEntities();


        public ActionResult Index()
        {
            var val = db.Cours.ToList();
            return View(val);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public  ActionResult Create(Cour cours, HttpPostedFileBase Image)
        {

            if(ModelState.IsValid)
            {
                if(cours.Image !=null)
                {
                    string PDFName = System.IO.Path.GetFileName(Image.FileName);
                    string PDFName2 = DateTime.Now.ToString("yymmss") + PDFName;
                    string physicalPath = Server.MapPath("~/Content/ImageUploaded/" + PDFName2);
                    Image.SaveAs(physicalPath);
                    cours.Image = PDFName2;
                } 


                db.Cours.Add(cours);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cours);
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var val = db.Cours.Where(x => x.Id == id).FirstOrDefault();
            if(val==null)
            {
                return View("Error");
            }

            return View(val);
        }


        [HttpPost]
        public ActionResult Edit(Cour cours, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                var old = db.Cours.Where(x => x.Id == cours.Id).FirstOrDefault();


                if (cours.Image != null)
                {
                    string PDFName = System.IO.Path.GetFileName(Image.FileName);
                    string PDFName2 = DateTime.Now.ToString("yymmss") + PDFName;
                    string physicalPath = Server.MapPath("~/Content/ImageUploaded/" + PDFName2);
                    Image.SaveAs(physicalPath);
                    cours.Image = PDFName2;
                }
              if(cours.Image ==null)
                {
                    cours.Image = old.Image;
                }
                
                old.NameOfCourse = cours.NameOfCourse;
                old.TimeOfCourse = cours.TimeOfCourse;
                old.Trainer = cours.Trainer;
                old.TypeCourse = cours.TypeCourse;

              //  db.Entry(cours).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(cours);
        }



        public JsonResult Delete(int? EmployeeId)
        {
            bool result = false;
            if (EmployeeId == null)
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            Cour s = db.Cours.Where(x => x.Id == EmployeeId).SingleOrDefault();
            if (s != null)
            {
                db.Cours.Remove(s);
                db.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }



    }
}