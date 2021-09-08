using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantReviews.Domain;
using RestaurantReviews.WebApp.Models;
using Serilog;
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
        public IActionResult Edit(Restaurant viewModel)
        {
            var restaurant =  new Restaurant { };
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            try
            {
                _repo.EditRestaurant(viewModel.Id, viewModel, true);
                Log.Debug("Restaurant has been edited! " + viewModel.Name);
            }
            catch (Exception e)
            {

                ModelState.AddModelError(key: "Text", errorMessage: e.Message);
                ModelState.AddModelError(key: "", errorMessage: $"{viewModel.Id} is not a valid viewmodel");
                return View();
            }
            return View("Details", viewModel);
        }
        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }
        public IActionResult Delete(Restaurant viewModel)
        {
            Restaurant restaurant = viewModel;
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            try
            {
                _repo.EditRestaurant(viewModel.Id, viewModel, false);
                Log.Debug("Restaurant has been deleted! " + viewModel.Name);
            }
            catch (Exception e)
            {

                ModelState.AddModelError(key: "Text", errorMessage: e.Message);
                ModelState.AddModelError(key: "", errorMessage: $"{viewModel.Id} is not a valid viewmodel");
                return View();
            }
            return View("Details", restaurant);
        }
        public Restaurant RestaurantReturn(int id)
        {
            List<Restaurant> restaurants = _repo.AllRestaurants();
            foreach (Restaurant r in restaurants)
            {
                if (r.Id == id)
                {
                    return r;
                }
            }
            throw new Exception("This restaurant does not exist.");

        }
 
        // GET: RestaurantController/Details/5
        public IActionResult Details(Restaurant restaurant)
        {
            List<Restaurant> restaurants = _repo.AllRestaurants();
            foreach (Restaurant r in restaurants)
            {
                if (r.Id == restaurant.Id)
                {
                    r.Name = restaurant.Name;
                    r.Rating = restaurant.Rating;
                    r.Zipcode = restaurant.Zipcode;
                    Console.WriteLine($"Id is {r.Id}");
                    Console.WriteLine($"Name is {r.Name}");
                    Console.WriteLine($"Rating is {r.Rating}");
                    Console.WriteLine($" Zipcode is {r.Zipcode}");
                    return View(restaurant);

                }
            }
            // bad: should have a repo implementation to just get one note
            return View();
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
            var restaurant = new Restaurant { Name = viewModel.Name, Id = viewModel.Id, Zipcode = viewModel.Zipcode, Rating = viewModel.Rating };
            try
            {
                _repo.AddRestaurant(restaurant);
                Log.Debug("Restaurant has been added! " + viewModel.Name);
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
