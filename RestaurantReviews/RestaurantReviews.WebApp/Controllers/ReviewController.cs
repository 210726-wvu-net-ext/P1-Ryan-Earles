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
    public class ReviewController : Controller
    {
        private readonly IRepository _repo;

        public ReviewController(IRepository repo)
        {
            _repo = repo;
        }
        // GET: RestaurantController
        public IActionResult Index()
        {
            return View(_repo.AllReviews());
        }

        // GET: RestaurantController/Details/5
        public IActionResult Details(int id)
        {
            return View(_repo.AllReviews().First(x => x.Id == id));
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
        public IActionResult Create(CreatedReview viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var review = new Review { Title = viewModel.Title, Body = viewModel.Body, Rating = viewModel.Rating };
            try
            {
                _repo.AddReview(review);
            }
            catch (InvalidOperationException e)
            {
                ModelState.AddModelError(key: "Text", errorMessage: e.Message);
                ModelState.AddModelError(key: "", errorMessage: "general errors could go up here");
                return View(viewModel);
            }

            return RedirectToAction("Details", new { id = review.Id });
        }
    }
}
