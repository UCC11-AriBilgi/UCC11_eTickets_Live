using eTickets.Data;
using eTickets.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    public class MoviesController : Controller
    {
        // injecting
        // 16
        //private readonly AppDbContext _context;

        //public MoviesController(AppDbContext context)
        //{
        //    _context = context;
        //}
        private readonly IMoviesService _service;

        public MoviesController(IMoviesService service)
        {
            _service= service;
        }



        public async Task<IActionResult> Index()
        {
            //var movieData = _context.Movies.ToList(); // VT den veri okunuyor
            // 17.1
            // Normal .ToList yapısıyla sadece Movie tablosundaki verileri çekiyoruz. Ama bize View ekranında Cinema adını da göstermemiz gerekiyor. Bu yüzden Include yapısı kullanıyoruz.
            
            //var movieData = _context.Movies.Include(c => c.Cinema).ToList(); // VT den veri okunuyor

            var movieData=await _service.GetAllAsync(n=> n.Cinema); // ilişkilendirmeye girecek modelimi parametre olarak gönderiyoruz


            return View(movieData);
        }
    }
}
