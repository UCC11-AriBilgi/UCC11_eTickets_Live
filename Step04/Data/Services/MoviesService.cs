using eTickets.Data.Base;
using eTickets.Data.Interfaces;
using eTickets.Models;

namespace eTickets.Data.Services
{
    public class MoviesService : EntityBaseRepository<Movie>,IMoviesService
    {
        private readonly AppDbContext _context;
        public MoviesService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public Task AddNewMovieAsync(Movie movie)
        {
            throw new NotImplementedException();
        }

        public Task<Movie> GetMovieByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateMovieAsync(Movie movie)
        {
            throw new NotImplementedException();
        }
    }
}
