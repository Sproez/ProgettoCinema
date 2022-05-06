namespace CinemaLib.Domain;

public class BigliettoAnziano : Biglietto
{

    public override double Sconto()
    {
        return 10;
    }
}

