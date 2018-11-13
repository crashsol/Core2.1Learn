using GraphQL.Types;
using LearnGraph.Movies.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearnGraph.Movies.Schema
{
  public  class ActorType: ObjectGraphType<Actor>
    {
        public ActorType()
        {
            Name = "ActorType";
            Description = "ActorDescription";

            Field(b => b.Id);
            Field(b => b.Name);
        }
    }
}
