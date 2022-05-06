using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaLib.Domain;

    public record Cinema
    (
        string Name
    )

    {
    public int Id { get; init; }

    public List<SalaCinematografica> SalaCinematografiche { get; set; } = null!;
    
    }

