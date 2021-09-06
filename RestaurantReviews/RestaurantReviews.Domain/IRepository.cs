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
        void EditRestaurant(int id, Domain.Restaurant restaurant, bool check);
        void EditUser(int id, Domain.User user, bool check);
        void EditReview(int id, Domain.Review review, bool check);
        //Restaurant FindRestaurant(string name);
    }
}
