using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LearnGraph.Movies.Models;
using System.Linq;

namespace LearnGraph.Movies.Services
{
    public class ActorService : IActorService
    {
        public IList<Actor> _actors { get; set; }
        public ActorService()
        {
            _actors = new List<Actor>
            {
                new Actor
                {
                    Id =1,
                    Name="Actor1"
                },
                 new Actor
                {
                    Id =2,
                    Name="Actor2"
                },
                  new Actor
                {
                    Id =3,
                    Name="Actor3"
                },
                   new Actor
                {
                    Id =4,
                    Name="Actor4"
                },

                    new Actor
                {
                    Id =5,
                    Name="Actor5"
                },
            };
        }
        public Task<IEnumerable<Actor>> GetAllAsync()
        {
            return Task.FromResult(_actors.AsEnumerable());
        }

        public Task<Actor> GetByIdAsync(int id)
        {
            return Task.FromResult(_actors.SingleOrDefault(b => b.Id == id));
        }
    }
}
