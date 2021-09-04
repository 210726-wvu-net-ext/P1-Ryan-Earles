using RestaurantReviews.DataAccess.Entities;
using RestaurantReviews.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviews.DataAccess
{
    public class Repository : IRepository
    {
        private RearlesDBContext _context;
        public Repository(RearlesDBContext context)
        {
            _context = context;
        }

        public List<Domain.Restaurant> AllRestaurants()
        {
            return _context.Restaurants.Select(
                Restaurant => new Domain.Restaurant(Restaurant.Name, Restaurant.Id, Restaurant.Zipcode, Restaurant.Rating) //id name, zipcode, rating, 
            ).ToList();
        }
        public List<Domain.Review> AllReviews()
        {
            return _context.Reviews.Select(
                Review => new Domain.Review(Review.Id, Review.Title, Review.Body, Review.Rating) //id, title, body, rating
            ).ToList();
        }
        public List<Domain.User> AllUsers()
        {
            return _context.Users.Select(
                User => new Domain.User(User.Id, User.Name, User.Username, User.Password, User.IsAdmin) //id, name, username, password, isadmin
            ).ToList();
        }
        public List<Domain.ReviewJoin> AllReviewJoin()
        {
            return _context.ReviewJoins.Select(
                ReviewJoin => new Domain.ReviewJoin(ReviewJoin.RestaurantId, ReviewJoin.ReviewId, ReviewJoin.UserId) //id, name, username, password, isadmin
            ).ToList();
        }
        public Domain.Restaurant AddRestaurant(Domain.Restaurant restaurant)
        {
            _context.Restaurants.Add(
                new Entities.Restaurant
                {
                    Id = restaurant.Id,
                    Name = restaurant.Name,
                    Zipcode = restaurant.Zipcode,
                    Rating = restaurant.Rating,
                }
            );
            _context.SaveChanges();

            return restaurant;
        }
        public Domain.Review AddReview(Domain.Review review)
        {
            _context.Reviews.Add(
                new Entities.Review
                {
                    Id = review.Id,
                    Title = review.Title,
                    Body = review.Body,
                    Rating = review.Rating,
                }
            );
            _context.SaveChanges();

            return review;
        }
        public Domain.User AddUser(Domain.User user) //id, name, username, password, isadmin
        {
            _context.Users.Add(
                new Entities.User
                {
                    Id = user.Id,
                    Name = user.Name,
                    Username = user.Username,
                    Password = user.Password,
                    IsAdmin = user.isAdmin
                }
            );
            _context.SaveChanges();

            return user;
        }
        public Domain.ReviewJoin AddReviewJoin(Domain.ReviewJoin reviewjoin) //id, name, username, password, isadmin
        {
            _context.ReviewJoins.Add(
                new Entities.ReviewJoin
                {
                    Id = reviewjoin.Id,
                    RestaurantId = reviewjoin.RestaurantId,
                    ReviewId = reviewjoin.ReviewId,
                    UserId = reviewjoin.UserId
                }
            );
            _context.SaveChanges();

            return reviewjoin;
        }
        public void EditRestaurant(int id, Domain.Restaurant restaurant)
        {
            Entities.Restaurant foundRestaurant = _context.Restaurants.FirstOrDefault(
                foundRestaurant => foundRestaurant.Id == id);
            foundRestaurant.Rating = restaurant.Rating;
            foundRestaurant.Zipcode = restaurant.Zipcode;
            foundRestaurant.Name = restaurant.Name;
            _context.SaveChanges();

        }
        
    }
}
