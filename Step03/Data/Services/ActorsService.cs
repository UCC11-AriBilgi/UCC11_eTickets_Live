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
            throw new NotImplementedException();
        }

        public Actor Update(int id, Actor actor)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }






    }
}
