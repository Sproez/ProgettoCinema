namespace CinemaLib.Domain;

public class Cinema : Base

    {
    public string salaId { get; init; } = null!;

    public double Guadagno { get; init; }

    public ICollection<SalaCinematografica> SaleCinematografiche { get; set; } = null!;
    
    }

