using System;
using System.Linq;
using DutchTreat.Data;
using DutchTreat.Services;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DutchTreat.Controllers {
    public class AppController : Controller {
        private readonly IMailService mailService;
        private readonly IDutchRepository _repository;

        public AppController (IMailService mailService, IDutchRepository repository) {
            this._repository = repository;

            this.mailService = mailService;
        }

        public IActionResult Index () {
            return View ();
        }

        [HttpGet ("contact")]
        public IActionResult Contact () {
            ViewBag.Title = "Contact Us";
            return View ();
        }

        [HttpPost ("contact")]
        public IActionResult Contact (ContactViewModel model) {
            if (ModelState.IsValid) {
                // send email
                mailService.SendMail ("mark@markwetzel.com", model.Subject, $"From: {model.Email}, Message: {model.Message}");
                ViewBag.UserMessage = "Mail Sent";
                // Clear fields
                ModelState.Clear ();
            } else {
                // errors
            }

            return View ();
        }

        [HttpGet ("about")]
        public IActionResult About () {
            ViewBag.Title = "About Us";
            return View ();
        }

        [HttpGet ("shop")]
        public IActionResult Shop () {
            var results = _repository.GetAllProducts ();

            return View (results);
        }
    }
}