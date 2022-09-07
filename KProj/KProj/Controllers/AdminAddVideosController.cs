using KProj.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KProj.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminAddVideosController : Controller
    {
        private KidsDBEntities db = new KidsDBEntities();

         [HttpGet]
        public ActionResult AddVideo(int courseid)
        {
            TempData["courseid"] = courseid;
            TempData.Keep("courseid");
            return View();
        }

        [HttpPost]
        public ActionResult AddVideo(VideosUploaded uploaded )
        {
            if(ModelState.IsValid)
            {

                uploaded.Course_Id = Convert.ToInt32(TempData["courseid"].ToString());
                TempData.Clear();

                db.VideosUploadeds.Add(uploaded);
                db.SaveChanges();
                return RedirectToAction("AddVideo", new { courseid = uploaded.Course_Id });

            }
            
            return View(uploaded) ;
        }



    }
}