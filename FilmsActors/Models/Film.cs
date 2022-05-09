using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsActors.Models
{
    public class Film
    {
        public int ID { get; set; }
        public int PosRating { get; set; }
        public int NegRating { get; set; }
        public string Title { get; set; }
        public string Info { get; set; }
        public string ReleaseDate { get; set; }
        public int PosRated { get; set; }
        public int NegRated { get; set; }
        public int IsRated { get; set; }


        Film()
        {

        }
    }
}
