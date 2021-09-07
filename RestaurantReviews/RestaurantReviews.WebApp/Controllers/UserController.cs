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
    public class UserController : Controller
    {
        private readonly IRepository _repo;

        public UserController(IRepository repo)
        {
            _repo = repo;
        }
        // GET: RestaurantController
        public IActionResult Index()
        {
            return View(_repo.AllUsers());
        }
        [HttpPost]
        public IActionResult Edit(User viewModel)
        {
            var restaurant = new Restaurant { };
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            try
            {
                _repo.EditUser(viewModel.Id, viewModel, true);
                Log.Debug("User has been edited! " + viewModel.Name);
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
        public User UserReturn(int id)
        {
            List<User> users = _repo.AllUsers();
            foreach (User r in users)
            {
                if (r.Id == id)
                {
                    return r;
                }
            }
            throw new Exception("This user does not exist.");

        }
        [HttpGet]
        [HttpPost]
        public IActionResult Delete(User viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            try
            {
                _repo.EditUser(viewModel.Id, viewModel, false);
                Log.Debug("User has been deleted! " + viewModel.Name);
            }
            catch (Exception e)
            {

                ModelState.AddModelError(key: "Text", errorMessage: e.Message);
                ModelState.AddModelError(key: "", errorMessage: $"{viewModel.Id} is not a valid viewmodel");
                return View();
            }
            return View("Details", viewModel);
        }

        // GET: RestaurantController/Details/5
            public IActionResult Details(User user)
        {
            List<User> users = _repo.AllUsers();
            foreach (User r in users)
            {
                if (r.Id == user.Id)
                {
                    return View(user);
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
        public IActionResult Create(CreatedUser viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var user = new User { Name = viewModel.Name, Id = viewModel.Id, Password = viewModel.Password, isAdmin = viewModel.isAdmin };
            try
            {
                _repo.AddUser(user);
                Log.Debug("User has been created! " + viewModel.Name);
            }
            catch (InvalidOperationException e)
            {
                ModelState.AddModelError(key: "Text", errorMessage: e.Message);
                ModelState.AddModelError(key: "", errorMessage: "general errors could go up here");
                return View(viewModel);
            }

            return RedirectToAction("Details", new { id = user.Id });
        }

        //action method for edit, for
    }
}
