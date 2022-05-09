using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsActors.Models
{
    public class Actor
    {
        public int ID { get; set; }
        public int PosRating { get; set; }
        public int NegRating { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Description { get; set; }
        public int PosRated { get; set; }
        public int NegRated { get; set; }
        public int IsRated { get; set; }

        public Actor()
        {

        }
    }
}
