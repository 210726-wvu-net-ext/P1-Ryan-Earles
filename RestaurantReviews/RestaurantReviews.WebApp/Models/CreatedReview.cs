using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using RestaurantReviews.Domain;

namespace RestaurantReviews.WebApp.Models
{
    public class CreatedReview
    {
        public int Id { get; }
        public string Title { get; set; }
        public string Body { get; set; }
        public decimal Rating { get; set; }
    }
}
