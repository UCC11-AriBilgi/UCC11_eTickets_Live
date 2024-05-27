using eTickets.Data.Interfaces;
using eTickets.Models;

namespace eTickets.Data.Services
{
    public class ActorsService : IActorsService
    {
        private readonly AppDbContext _context;
        public ActorsService(AppDbContext context) 
        {
            _context = context;
        }

        // 21
        public IEnumerable<Actor> GetAll()
        {
            var actors = _context.Actors.ToList();

            return actors;
        }

        public Actor GetById(int id)
        {
            var actor = _context.Actors.FirstOrDefault(a => a.Id == id);

            return actor;
        }

       public void Add(Actor actor)
        {
            _context.Actors.Add(actor);
            _context.SaveChanges(); // lazım ki değişiklikler VT ye yerleşsin
        }

        public Actor Update(int id, Actor actor)
        {
            _context.Update(actor);

            _context.SaveChanges();

            return actor;
        }

        public void Delete(int id)
        {
            var result=_context.Actors.FirstOrDefault(a=> a.Id == id);

            _context.Actors.Remove(result);

            _context.SaveChanges();
        }






    }
}
