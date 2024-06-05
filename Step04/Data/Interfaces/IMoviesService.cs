using eTickets.Data.Base;
using eTickets.Models;

namespace eTickets.Data.Interfaces
{
    // 38.1
    public interface IMoviesService : IEntityBaseRepository<Movie>
    {
        Task<Movie> GetMovieByIdAsync(int id);

        Task AddNewMovieAsync(Movie movie);

        Task UpdateMovieAsync(Movie movie);

        // dropdown.




    }
}
