using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Example.MVC.Models
{
    public class MovieSearchModel
    {
        public string Title { get; set; }
        public DateTime ReleaseAt { get; set; }
        public string AbstractText { get; set; }
        public decimal DoubanRating { get; set; }
    }
}