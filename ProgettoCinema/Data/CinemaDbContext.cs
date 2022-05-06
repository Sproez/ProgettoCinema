using CinemaLib.Domain;
using Microsoft.EntityFrameworkCore;

namespace ProgettoCinema.ClientWeb.Data;

public class CinemaDbContext : DbContext
{
    public DbSet<Biglietto> Biglietti { get; set; }
    public DbSet<Cinema> Cinemas { get; set; }
    public DbSet<Film> Movies { get; set; }
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
        film.Property(f => f.CinemaRoomId).IsRequired();

        film
            .HasOne(f => f.Genere)
            .WithMany(fg => fg.Film)
            .OnDelete(DeleteBehavior.NoAction)
            .HasForeignKey(f => f.FilmGenreId);

        var filmGenre = modelBuilder.Entity<Genere>();
        filmGenre.HasKey(fg => fg.Id);
        filmGenre.Property(fg => fg.GenereFilm).IsRequired();

        var cinemaRoom = modelBuilder.Entity<SalaCinematografica>();
        cinemaRoom.HasKey(cr => cr.Id);
        cinemaRoom.Property(cr => cr.Cinema).IsRequired();
        cinemaRoom.Property(cr => cr.CinemaId).IsRequired();
        cinemaRoom.Property(cr => cr.MaxSpettatori).IsRequired();
        cinemaRoom.Property(cr => cr.PostiOccupati);

        cinemaRoom
            .HasOne(cr => cr.Cinema)
            .WithMany(c => c.SaleCinematografiche)
            .OnDelete(DeleteBehavior.NoAction)
            .HasForeignKey(cr => cr.CinemaId);
        cinemaRoom
            .HasOne(cr => cr.Film)
            .WithMany(f => f.saleCinematografiche)
            .OnDelete(DeleteBehavior.NoAction)
            .HasForeignKey(cr => cr.FilmId);

        var person = modelBuilder.Entity<Spettatore>();
        person.HasKey(p => p.Id);
        person.Property(p => p.Id).IsRequired();
        person.Property(p => p.Cognome).IsRequired();
        person.Property(p => p.DataNascita).IsRequired();
        person.Property(p => p.Anziano).IsRequired();
        person.Property(p => p.Bambino).IsRequired();
        person.Property(p => p.biglietto).IsRequired();
    }
}

