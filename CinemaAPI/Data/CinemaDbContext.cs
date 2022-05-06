using Microsoft.EntityFrameworkCore;


namespace ProgettoCinema.API.Data;

public class CinemaDbContext : DbContext
{
    public DbSet<Biglietto> Biglietti { get; set; }
    public DbSet<Cinema> Cinemas { get; set; }
    public DbSet<Film> Films { get; set; }
    public DbSet<GenereFilm> Genere { get; set; }
    public DbSet<SalaCinematografica> SaleCinematografiche { get; set; }
    public DbSet<Spettatore> Spettatori { get; set; }

    public CinemaDbContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var ticket = modelBuilder.Entity<Biglietto>();
        ticket.HasKey(t => t.Id);
        ticket.Property(t => t.Price).IsRequired();
        ticket.Property(t => t.Seat).IsRequired();

        ticket
            .HasOne(t => t.Person)
            .WithOne(p => p.Ticket)
            .OnDelete(DeleteBehavior.NoAction)
            .HasForeignKey<Biglietto>(t => t.PersonId);
        ticket
            .HasOne(t => t.CinemaRoom)
            .WithMany(cr => cr.Tickets)
            .OnDelete(DeleteBehavior.NoAction)
            .HasForeignKey(t => t.CinemaRoomId);

        var cinema = modelBuilder.Entity<Cinema>();
        cinema.HasKey(c => c.Id);
        cinema.Property(c => c.Name).IsRequired();
        cinema.Property(c => c.Profit);

        var film = modelBuilder.Entity<Film>();
        film.HasKey(f => f.Id);
        film.Property(f => f.Title).IsRequired();
        film.Property(f => f.Duration).IsRequired();
        film.Property(f => f.Author).IsRequired();
        film.Property(f => f.Producer).IsRequired();
        film.Property(f => f.FilmGenreId).IsRequired();
        film.Property(f => f.CinemaRoomId).IsRequired();

        film
            .HasOne(f => f.FilmGenre)
            .WithMany(fg => fg.Films)
            .OnDelete(DeleteBehavior.NoAction)
            .HasForeignKey(f => f.FilmGenreId);

        var filmGenre = modelBuilder.Entity<GenereFilm>();
        filmGenre.HasKey(fg => fg.Id);
        filmGenre.Property(fg => fg.FilmGenre).IsRequired();

        var cinemaRoom = modelBuilder.Entity<SalaCinematografica>();
        cinemaRoom.HasKey(cr => cr.Id);
        cinemaRoom.Property(cr => cr.Name).IsRequired();
        cinemaRoom.Property(cr => cr.CinemaId).IsRequired();
        cinemaRoom.Property(cr => cr.RoomCapacity).IsRequired();
        cinemaRoom.Property(cr => cr.OccupiedSeats);

        cinemaRoom
            .HasOne(cr => cr.Cinema)
            .WithMany(c => c.CinemaRooms)
            .OnDelete(DeleteBehavior.NoAction)
            .HasForeignKey(cr => cr.CinemaId);
        cinemaRoom
            .HasOne(cr => cr.Film)
            .WithMany(f => f.CinemaRooms)
            .OnDelete(DeleteBehavior.NoAction)
            .HasForeignKey(cr => cr.FilmId);

        var person = modelBuilder.Entity<Spettatore>();
        person.HasKey(p => p.Id);
        person.Property(p => p.Name).IsRequired();
        person.Property(p => p.Surname).IsRequired();
        person.Property(p => p.Birthdate).IsRequired();
        person.Property(p => p.OverSeventyYear).IsRequired();
        person.Property(p => p.UnderFiveYear).IsRequired();
        person.Property(p => p.TicketId).IsRequired();
    }
}
