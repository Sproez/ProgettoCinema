using CinemaLib.Domain;
using Microsoft.EntityFrameworkCore;

namespace ProgettoCinema.ClientWeb.Data;

public class CinemaDbContext : DbContext
{
    public DbSet<Biglietto> Biglietti { get; set; }
    public DbSet<Cinema> Cinemas { get; set; }
    public DbSet<Film> Film { get; set; }
    public DbSet<Genere> GenereFilm { get; set; }
    public DbSet<SalaCinematografica> SaleCinematografiche { get; set; }
    public DbSet<Spettatore> Spettatori { get; set; }

    public CinemaDbContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var ticket = modelBuilder.Entity<Biglietto>();
        ticket.HasKey(t => t.Id);
        ticket.Property(t => t.Costo).IsRequired();
        ticket.Property(t => t.idPosto).IsRequired();

        ticket
            .HasOne(t => t.Cliente)
            .WithOne(p => p.biglietto)
            .OnDelete(DeleteBehavior.NoAction)
            .HasForeignKey<Biglietto>(t => t.Cliente);
        ticket
            .HasOne(t => t.SaleCinematografiche)
            .WithMany(cr => cr.Biglietti)
            .OnDelete(DeleteBehavior.NoAction)
            .HasForeignKey(t => t.salaId);

        var cinema = modelBuilder.Entity<Cinema>();
        cinema.HasKey(c => c.Id);
        cinema.Property(c => c.Id).IsRequired();
        cinema.Property(c => c.Guadagno);

        var film = modelBuilder.Entity<Film>();
        film.HasKey(f => f.Id);
        film.Property(f => f.Titolo).IsRequired();
        film.Property(f => f.Durata).IsRequired();
        film.Property(f => f.Autore).IsRequired();
        film.Property(f => f.Produttore).IsRequired();
        film.Property(f => f.Genere).IsRequired();
        film.Property(f => f.salaId).IsRequired();

        //film
        //    .HasOne(f => f.Genere)
        //    .WithMany(fg => fg.Film)
        //    .OnDelete(DeleteBehavior.NoAction)
        //    .HasForeignKey(f => f.Genere);

        var genere = modelBuilder.Entity<Genere>();
        genere.HasKey(fg => fg.Id);
        genere.Property(fg => fg.GenereFilm).IsRequired();

        var salaCinema = modelBuilder.Entity<SalaCinematografica>();
        salaCinema.HasKey(cr => cr.Id);
        salaCinema.Property(cr => cr.Cinema).IsRequired();
        salaCinema.Property(cr => cr.CinemaId).IsRequired();
        salaCinema.Property(cr => cr.MaxSpettatori).IsRequired();
        salaCinema.Property(cr => cr.PostiOccupati);

        salaCinema
            .HasOne(cr => cr.Cinema)
            .WithMany(c => c.SaleCinematografiche)
            .OnDelete(DeleteBehavior.NoAction)
            .HasForeignKey(cr => cr.CinemaId);
        salaCinema
            .HasOne(cr => cr.Film)
            .WithMany(f => f.saleCinematografiche)
            .OnDelete(DeleteBehavior.NoAction)
            .HasForeignKey(cr => cr.FilmId);

        var cliente = modelBuilder.Entity<Spettatore>();
        cliente.HasKey(p => p.Id);
        cliente.Property(p => p.Id).IsRequired();
        cliente.Property(p => p.Cognome).IsRequired();
        cliente.Property(p => p.DataNascita).IsRequired();
        cliente.Property(p => p.Anziano).IsRequired();
        cliente.Property(p => p.Bambino).IsRequired();
        cliente.Property(p => p.biglietto).IsRequired();
    }
}

