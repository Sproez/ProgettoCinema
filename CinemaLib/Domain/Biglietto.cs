namespace CinemaLib.Domain;

public abstract class Biglietto : Base
    {
    public string idPosto { get; set; } = null!;

    public abstract double Sconto();

    public readonly double Costo = 10;

    public double Prezzo()
    {

        return Costo - (Costo/100 * Sconto());
    }

    public int salaId { get; set; }

    public int SpettatoreId { get; set; }

    public Spettatore Cliente { get; set; } = null!;
    public SalaCinematografica SaleCinematografiche { get; set; } = null!;

}

