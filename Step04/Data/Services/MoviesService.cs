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


        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            // Üzerine gelen id bilgisine göreilgili Movie bilgisini okuyan metot.

            var movieDetails = await _context.Movies
                            .Include(c => c.Cinema)
                            .Include(p => p.Producer)
                            .Include(acmo => acmo.Actors_Movies)
                            .ThenInclude(a => a.Actor)
                            .FirstOrDefaultAsync(n => n.Id == id);

            return movieDetails;
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

        public async Task AddNewMovieAsync(NewMovieVM movie)
        {
            // VT ye yeni bir Movie ekleme kısmı

            var newMovie = new Movie()
            {
                Name = movie.Name,
                Description = movie.Description,
                Price = movie.Price,
                ImageURL = movie.ImageURL,
                CinemaId = movie.CinemaId,
                StartDate = movie.StartDate,
                EndDate = movie.EndDate,
                MovieCategory = movie.MovieCategory,
                ProducerId = movie.ProducerId
            };

            await _context.Movies.AddAsync(newMovie);

            await _context.SaveChangesAsync(); // VT ye movie bilgisi eklendi.

            // Movie de oynamış olan aktorlerin de VT ye eklenmesi lazım.

            foreach (var actorId in movie.ActorIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = newMovie.Id, // Yukarda otomatik olarak oluştu.
                    ActorId = actorId
                };

                await _context.Actors_Movies.AddAsync(newActorMovie);
            }

            await _context.SaveChangesAsync();


        }

        public async Task UpdateMovieAsync(NewMovieVM movie)
        {
            // İlgili bir Movie nin güncelleme bilgileri

            var dbMovie = await _context.Movies.FirstOrDefaultAsync(n => n.Id == movie.Id);

            if (dbMovie != null) // Yani bir kayıt gelmişse
            {
                dbMovie.Id = movie.Id;
                dbMovie.Name = movie.Name;
                dbMovie.Description = movie.Description;
                dbMovie.Price = movie.Price;
                dbMovie.ImageURL = movie.ImageURL;
                dbMovie.CinemaId = movie.CinemaId;
                dbMovie.StartDate = movie.StartDate;
                dbMovie.EndDate = movie.EndDate;
                dbMovie.MovieCategory = movie.MovieCategory;
                dbMovie.ProducerId = movie.ProducerId;

                await _context.SaveChangesAsync();

            }

            // Filmdeki oynayan actor bilgilerinin de güncellenmesi gerekiyor.
            // Hepsini tek tek güncellemek yerine.. önce filmde oynayan actorlerin tümünü bir silersin.ardından ekleme işlemini yaparsın

            // Remove existing actors
            var existingActorsDb = _context.Actors_Movies.Where(n => n.MovieId == movie.Id).ToList(); // eski bilgiler

            _context.Actors_Movies.RemoveRange(existingActorsDb);

            await _context.SaveChangesAsync();

            // Add Movie Actors

            foreach (var actorId in movie.ActorIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = movie.Id, 
                    ActorId = actorId
                };

                await _context.Actors_Movies.AddAsync(newActorMovie);
            }

            await _context.SaveChangesAsync();
        }
    }
}
