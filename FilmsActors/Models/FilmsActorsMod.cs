using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsActors.Models
{
    public class FilmsActorsMod
    {
        public Guid ID { get; set; }
        public int FilmID { get; set; }
        public int ActorID { get; set; }
    }
}
