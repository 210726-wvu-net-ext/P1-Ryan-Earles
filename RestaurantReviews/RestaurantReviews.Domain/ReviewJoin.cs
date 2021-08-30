using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviews.Domain
{
    public class ReviewJoin
    {
        public ReviewJoin() { }
        public ReviewJoin(int restaurantid, int reviewid, int userid)
        {
            this.RestaurantId = restaurantid;
            this.ReviewId = reviewid;
            this.UserId = userid;
        }
        public ReviewJoin(int id, int restaurantid, int reviewid, int userid) : this(restaurantid, reviewid, userid)
        {
            this.Id = id;
        }
        //look into how this is set up
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public int ReviewId { get; set; }
        public int UserId { get; set; }
    }
}
