using eTickets.Data;
using eTickets.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers
{
    public class CinemasController : Controller
    {
        // injecting
        // 16
        //private readonly AppDbContext _context;
        private readonly ICinemasService _service;
        public CinemasController(ICinemasService service)
        {
            _service= service;
        }
        public IActionResult Index()
        {
            var cinemaData = _service.GetAllAsync();

            return View(cinemaData);
        }
    }
}
