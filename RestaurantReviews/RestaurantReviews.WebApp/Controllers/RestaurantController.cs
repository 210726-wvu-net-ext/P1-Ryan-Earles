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

        // GET: RestaurantController/Details/5
        public IActionResult Details(int id)
        {
            return View(_repo.AllRestaurants().First(x => x.Id == id));
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
    }
}
