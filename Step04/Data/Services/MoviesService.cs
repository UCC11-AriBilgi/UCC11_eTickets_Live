using eTickets.Data.Base;
using eTickets.Data.Interfaces;
using eTickets.Models;
using eTickets.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Services
{
    public class MoviesService : EntityBaseRepository<Movie>,IMoviesService
    {
        private readonly AppDbContext _context;
        public MoviesService(AppDbContext context) : base(context)
        {
            _context = context;
        }


        public Task<Movie> GetMovieByIdAsync(int id)
        {
            throw new NotImplementedException();
        }


        public async Task<NewMovieDropdownsVM> GetNewMovieDropdownsValues()
        {
            // Viewlarımızda gözükecek olan ve seçim yapabileceğimiz dropdow listelerimizin oluşturulması
            var response = new NewMovieDropdownsVM()
            {
                Actors = await _context.Actors.OrderBy(a => a.FullName).ToListAsync(),
                Cinemas = await _context.Cinemas.OrderBy(c => c.Name).ToListAsync(),
                Producers = await _context.Producers.OrderBy(p => p.FullName).ToListAsync()
            };

            return response;
        }

        public Task AddNewMovieAsync(NewMovieVM movie)
        {
            throw new NotImplementedException();
        }

        public Task UpdateMovieAsync(NewMovieVM movie)
        {
            throw new NotImplementedException();
        }
    }
}
