using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaLib.Domain;

    public record Film()
    {
    public string Titolo { get; set; } = null!;

    public string Autore { get; set; } = null!;

    public string Genere { get; set; } = null!;

    public string Produttore { get; set; } = null!;

    public int Durata { get; set; }
}

