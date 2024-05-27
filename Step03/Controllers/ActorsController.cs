﻿using eTickets.Data;
using eTickets.Data.Interfaces;
using eTickets.Models;
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
        public async Task<IActionResult> Index()
        {
            //var actorsData = _service.GetAllAsync(); // Artık Service yapısı kullanılıyor.

            //23
            var actorsData = await _service.GetAllAsync(); // Artık Service yapısı kullanılıyor.

            return View(actorsData);
        }

        // 24
        // Get: Actors/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost] // View dan gelen bilgileri yakala
        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Bio")] Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }

            await _service.AddAsync(actor);

            return RedirectToAction(nameof(Index));

        }

    }
}
