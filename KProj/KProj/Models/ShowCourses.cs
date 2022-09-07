using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KProj.Models
{
    public class ShowCourses
    {
        public int Id { get; set; }
        public string NameOfCourse { get; set; }
        public string Image { get; set; }
        public string Trainner { get; set; }
        public double TimeOfCourse { get; set; }
        public Nullable<double> Rating { get; set; }
        public int NumberOfLessons { get; set; }
        public double NumberOfUserReviews { get; set; }



    }
}