using System;
using DutchTreat.Services;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DutchTreat.Controllers {
    public class AppController : Controller {
        private readonly IMailService mailService;

        public AppController (IMailService mailService) {
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

        public IActionResult About () {
            ViewBag.Title = "About Us";
            return View ();
        }
    }
}