namespace Client.Controllers
{
    using Client.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;

    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ILogger<CategoryController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            // Logic to get category details by id
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