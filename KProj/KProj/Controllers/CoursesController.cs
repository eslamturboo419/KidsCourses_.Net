using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ImageResizer;
using KProj.Models;
using KProj.Models.Classes;
using PagedList;

namespace KProj.Controllers
{
    [Authorize(Roles = "Admin,Teacher,Child")]
    public class CoursesController : Controller
    {

        private KidsDBEntities db = new KidsDBEntities();

        public ActionResult Index(int? Page , string Search)
        {

           // (xx.NameOfProject.StartsWith(Search) || (xx.NameOfProject.Contains(Search)) || Search == null));

            var x  = db.Cours.Select(y=>new ShowCourses {  Id = y.Id , Image = y.Image , NameOfCourse=y.NameOfCourse , NumberOfLessons= y.VideosUploadeds.Where(u=>u.Course_Id==y.Id ).Count() ,  /*Rating = Double.Parse( db.NewProcAvg(y.Id).ToString() ) , */ Rating = db.Reviews .Where(r => r.Course_Id == y.Id).Average(r => r.Rating)
            , TimeOfCourse =y.TimeOfCourse.Value , Trainner= y.Trainer   ,NumberOfUserReviews = db.Reviews.Where(r => r.Course_Id == y.Id).Count()
           }  ).Where(y=>y.NameOfCourse.StartsWith(Search) || (y.NameOfCourse.Contains(Search)) || Search==null )  .OrderBy(r => Guid.NewGuid());

            ViewBag.count = x.Count();


            return View( x.ToList().ToPagedList(Page ?? 1, 9));
        }


        // for auto complete "Final Project"
        public JsonResult GetAllData(string term)  // must the name is " term" because jquery 
        {
            List<string> lis = db.Cours.Where(x => x.NameOfCourse.StartsWith(term)).Select(y => y.NameOfCourse).Distinct().ToList();

            return Json(lis, JsonRequestBehavior.AllowGet);

        }

        //public ActionResult ResizeSettingsssss()
        //{
        //    ResizeSettings resizeSetting = new ResizeSettings
        //    {
        //        Width = 150,
        //        Height = 100 ,
        //        MaxWidth = 1000,
        //        Format = "png"
        //    };

        //    return View();
        //}


        public ActionResult DetailsCourse(int id)
        {
            var val = db.Cours.Where(x => x.Id == id).FirstOrDefault();

            var avg = db.Reviews.Where(x => x.Course_Id == id).FirstOrDefault();


            return View(val);
        }

        public ActionResult ShowVideos(int id)
        {
            var videos = db.VideosUploadeds.Where(x => x.Course_Id == id).ToList();

            return View(videos);
        }



        public ActionResult Review( float rating , int CourseId)
        {
          var usid = db.UserDatas.Where(x => x.Email == User.Identity.Name).FirstOrDefault().Id;

            // check for this user and this course id " not review two times "

            var thisReview = db.Reviews.Where(x => x.Course_Id == CourseId).FirstOrDefault();

            if(thisReview.Course_Id==CourseId && thisReview.User_Id==usid )
            {
                ModelState.AddModelError("ErrorAganin", "You Can not Add Two Reviews");

                return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());

            }



            Review review = new Review();
            review.datePost = DateTime.Now;
            review.Rating = rating;
            review.User_Id =usid;
            review.Course_Id = CourseId;

            db.Reviews.Add(review);
            db.SaveChanges();

            return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());

        }


    }
}