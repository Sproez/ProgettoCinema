using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaLib.Domain;

    public record SalaCinematografica
    (
        string Titolo,
        string Descrizione,
        DateTime? FineFilm
        )
    {
    public string CinemaId { get; set; } = null!;

    public int MaxSpettatori { get; set; }

    public string FilmId { get; set; } = null!;
}

