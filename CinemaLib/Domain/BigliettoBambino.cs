using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaLib.Domain;

public class BigliettoBambino : Biglietto
{
             public override double Sconto()
             {
                return 50;
             }
        
}