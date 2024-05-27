using eTickets.Models;

namespace eTickets.Data.Interfaces
{
    public interface IActorsService
    {
        // 20
        IEnumerable<Actor> GetAll();
        Actor GetById(int id);

        void Add(Actor actor);

        Actor Update(int id, Actor actor);
        void Delete(int id);
    }
}
