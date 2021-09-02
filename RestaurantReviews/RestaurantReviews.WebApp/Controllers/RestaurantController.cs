using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantReviews.Domain;
using RestaurantReviews.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantReviews.WebApp.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly IRepository _repo;

        public RestaurantController(IRepository repo)
        {
            _repo = repo;
        }
        // GET: RestaurantController
        public IActionResult Index()
        {
            return View(_repo.AllRestaurants());
        }
        [HttpPost]
        public IActionResult Edit(Restaurant restaurant, string name, int zipcode)
        {
            List<Restaurant> restaurants = _repo.AllRestaurants();
            if (restaurants.Contains(restaurant))
            {
                restaurant.Zipcode = zipcode;
                restaurant.Name = name;
                return RedirectToAction("Details", restaurant.Id);
            }
            else
            {
                return RedirectToAction("Details", restaurant.Id);
            }
        }
        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Details()
        {
            return View();
        }

        [HttpPost]
        // GET: RestaurantController/Details/5
        public IActionResult Details(int id)
        {
            Restaurant rest = new Restaurant { };
            List<Restaurant> restaurants = _repo.AllRestaurants();
            foreach (Restaurant r in restaurants)
            {
                if (r.Id == id)
                    rest = r;
            }
            // bad: should have a repo implementation to just get one note
            return View(rest);
        }

        // GET: RestaurantController/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: RestaurantController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreatedRestaurant viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var restaurant = new Restaurant { Name = viewModel.Name, Zipcode = viewModel.Zipcode, Rating = viewModel.Rating };
            try
            {
                _repo.AddRestaurant(restaurant);
            }
            catch (InvalidOperationException e)
            {
                ModelState.AddModelError(key: "Text", errorMessage: e.Message);
                ModelState.AddModelError(key: "", errorMessage: "general errors could go up here");
                return View(viewModel);
            }

            return RedirectToAction("Details", new { id = restaurant.Id });
        }

        //action method for edit, for
    }
}
