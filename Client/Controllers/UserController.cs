namespace Client.Controllers
{
    using Client.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;

    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }   

        public IActionResult Edit(int id)
        {
            // Logic to get category details by id for editing
            return View();
        }

        
    }
}