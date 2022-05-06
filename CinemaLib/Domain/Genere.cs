using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaLib.Domain;

    public class Genere : Base
    {
        public string GenereFilm { get; set; }

        public ICollection<Film> Films { get; set; }

    }

