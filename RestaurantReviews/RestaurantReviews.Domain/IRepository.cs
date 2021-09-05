using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviews.Domain
{
    public interface IRepository
    {
        Restaurant AddRestaurant(Restaurant restaurant);
        Review AddReview(Review review);
        ReviewJoin AddReviewJoin(ReviewJoin reviewjoin);
        User AddUser(User user);
        List<Restaurant> AllRestaurants();
        List<ReviewJoin> AllReviewJoin();
        List<Review> AllReviews();
        List<User> AllUsers();
        void EditRestaurant(int id, Domain.Restaurant restaurant);
        void EditUser(int id, Domain.User user);
        void EditReview(int id, Domain.Review review);
        //Restaurant FindRestaurant(string name);
    }
}
