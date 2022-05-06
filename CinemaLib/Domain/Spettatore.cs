using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaLib.Domain;

public class Spettatore
{
    public string idSpettatore { get; set; } = null!;

    public string Nome { get; set; } = null!;

    public string Cognome { get; set; } = null!;

    public DateTime DataNasita { get; set; }

    public Biglietto Biglietto { get; set; }

    public static bool Minorenne (DateTime bornIn)
    {
        return (bornIn.AddYears(18) < DateTime.Now);
    }

    public static bool Anziano (DateTime bornIn)
    {
        return (bornIn.AddYears(70) >= DateTime.Now);
    }

    public static bool Bambino (DateTime bornIn)
    {
        return (bornIn.AddYears(5) <= DateTime.Now);
    }
}