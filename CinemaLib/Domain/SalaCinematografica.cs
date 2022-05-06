using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaLib.Domain;

    public class SalaCinematografica : Base
    {
    public string CinemaId { get; set; } = null!;

    public int MaxSpettatori { get; set; }

    public int PostiOccupati { get; set; }

    public string FilmId { get; set; } = null!;

    public Film Film { get; set; }

    public Cinema Cinema { get; set; }

    public ICollection<Biglietto> Biglietti { get; set; }
}

