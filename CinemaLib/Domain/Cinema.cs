using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaLib.Domain;

    public class Cinema : Base

    {
    public string salaId { get; init; }

    public double Guadagno { get; init; }

    public ICollection<SalaCinematografica> SaleCinematografiche { get; set; } = null!;
    
    }

