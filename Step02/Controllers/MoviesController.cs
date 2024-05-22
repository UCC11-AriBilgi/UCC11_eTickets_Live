using eTickets.Data;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers
{
    public class MoviesController : Controller
    {
        // injecting
        // 16
        private readonly AppDbContext _context;

        public MoviesController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var movieData = _context.Movies.ToList(); // VT den veri okunuyor

            return View(movieData);
        }
    }
}
