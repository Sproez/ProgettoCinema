using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaLib.Domain;

    public abstract class Biglietto
    {
    public string idPosto { get; set; } = null!;

    public abstract double Sconto();

    private readonly double Costo = 10;

    public double Prezzo()
    {

        return Costo - (Costo/100 * Sconto());
    }

    }

