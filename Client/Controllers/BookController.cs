namespace Client.Controllers
{
    using Client.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;

    public class BookController : Controller
    {
        private readonly ILogger<BookController> _logger;

        public BookController(ILogger<BookController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            // Logic to get book details by id
            return View();
        }
    }
}