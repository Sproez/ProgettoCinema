namespace CinemaLib.Domain;

public class Film : Base
    {
    public string Titolo { get; set; } = null!;

    public string Autore { get; set; } = null!;

    public string Genere { get; set; } = null!;

    public string Produttore { get; set; } = null!;

    public int Durata { get; set; }

    public int CinemaRoomId { get; set; }

    public ICollection<SalaCinematografica> saleCinematografiche { get; set; } = null!;
}

