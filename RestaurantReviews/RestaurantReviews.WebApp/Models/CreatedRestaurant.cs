using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using RestaurantReviews.Domain;

namespace RestaurantReviews.WebApp.Models
{
    public class CreatedRestaurant //: IValidatableObject
    {
        public int Id { get; }
        public string Name { get; set; }
        public int Zipcode { get; set; }
        public decimal Rating { get; set; }
    }
}
