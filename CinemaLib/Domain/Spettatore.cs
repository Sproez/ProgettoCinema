using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaLib.Domain;

public class Spettatore : Base
{
    public string idSpettatore { get; set; } = null!;

    public string Nome { get; set; } = null!;

    public string Cognome { get; set; } = null!;

    public DateTime DataNascita { get; set; }

    public Biglietto idBiglietto { get; set; }

    //public bool Anziano { get; set; }

    public bool Anziano
    {
        get => Anziano;
        set => Anziano = Geriatria();

    }

    //public bool Bambino { get; set; } 

    public bool Bambino
    {
        get => Bambino;
        set => Bambino = Burlo();

    }

    public Biglietto biglietto { get; set; }

    public bool Geriatria()
    {
        return DataNascita.AddYears(70) < DateTime.Now;
    }

    public bool Burlo()
    {
        return DataNascita.AddYears(5) > DateTime.Now;

    }

}