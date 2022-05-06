namespace CinemaLib.Domain;

public class SalaCinematografica : Base
    {
    public string CinemaId { get; set; } = null!;

    public int MaxSpettatori { get; set; }

    public int PostiOccupati { get; set; }

    public string FilmId { get; set; } = null!;

    public Film Film { get; set; } = null!;

    public Cinema Cinema { get; set; } = null!;

    public ICollection<Biglietto> Biglietti { get; set; } = null!;
}

