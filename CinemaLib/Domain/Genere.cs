namespace CinemaLib.Domain;

public class Genere : Base
    {
        public string GenereFilm { get; set; } = null!;

    public ICollection<Film> Films { get; set; } = null!;

}

