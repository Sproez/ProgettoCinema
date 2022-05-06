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

    public DateTime DataNascita { get; set; }

    public Biglietto Biglietto { get; set; }

}