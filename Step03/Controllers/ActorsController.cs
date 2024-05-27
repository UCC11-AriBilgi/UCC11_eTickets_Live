using eTickets.Data;
using eTickets.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    public class ActorsController : Controller // Controller sınıfından  inherit
    {
        // 16 - injecting
        //private readonly AppDbContext _context;

        //public ActorsController(AppDbContext context)
        //{
        //    _context = context;    
        //}

        // 22
        private readonly IActorsService _service;

        public ActorsController(IActorsService service)
        {
                _service = service;
        }

        // 17
        //public IActionResult Index()
        //{
        //    var actorData=_context.Actors.ToList(); // VT den veri okunuyor

        //    return View(actorData);
        //}

        // 22
        public IActionResult Index()
        {
            var actorsData = _service.GetAll(); // Artık Service yapısı kullanılıyor.

            return View(actorsData);
        }
    }
}
