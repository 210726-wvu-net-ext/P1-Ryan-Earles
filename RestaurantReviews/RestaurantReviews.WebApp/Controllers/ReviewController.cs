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
        [HttpPost]
        public IActionResult Edit(Review viewModel)
        {
            var review = new Review { };
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            try
            {
                _repo.EditReview(viewModel.Id, viewModel);
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
        public Review RestaurantReturn(int id)
        {
            List<Review> review = _repo.AllReviews();
            foreach (Review r in review)
            {
                if (r.Id == id)
                {
                    return r;
                }
            }
            throw new Exception("This review does not exist.");

        }

        // GET: RestaurantController/Details/5
        public IActionResult Details(Review review)
        {
            List<Review> reviews = _repo.AllReviews();
            foreach (Review r in reviews)
            {
                if (r.Id == review.Id)
                {
                   
                    return View(review);

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
        public IActionResult Create(CreatedReview viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var review = new Review { Title = viewModel.Title, Id = viewModel.Id, Body = viewModel.Body, Rating = viewModel.Rating };
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
