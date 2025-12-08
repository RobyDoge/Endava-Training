using System;
using System.Collections.Generic;
using AirportTool.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace AirportTool.Infrastructure.Context;

public partial class AirlineBookingContext : DbContext
{
    public AirlineBookingContext()
    {
    }

    public AirlineBookingContext(DbContextOptions<AirlineBookingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aircraft> Aircrafts { get; set; }

    public virtual DbSet<Airline> Airlines { get; set; }

    public virtual DbSet<Airport> Airports { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<BookingStatus> BookingStatuses { get; set; }

    public virtual DbSet<Currency> Currencies { get; set; }

    public virtual DbSet<FareClass> FareClasses { get; set; }

    public virtual DbSet<Flight> Flights { get; set; }

    public virtual DbSet<FlightSchedule> FlightSchedules { get; set; }

    public virtual DbSet<FlightScheduleStatus> FlightScheduleStatuses { get; set; }

    public virtual DbSet<Gate> Gates { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            throw new Exception("Connection string not configured");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aircraft>(entity =>
        {
            entity.HasIndex(e => e.TailName, "UQ_Aircraft_TailName").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Model).HasMaxLength(60);
            entity.Property(e => e.TailName).HasMaxLength(10);

            entity.HasOne(d => d.AirlineOwner).WithMany(p => p.Aircraft)
                .HasForeignKey(d => d.AirlineOwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Aircrafts_Airlines");
        });

        modelBuilder.Entity<Airline>(entity =>
        {
            entity.HasIndex(e => e.Iatacode, "UQ_Airline_IATACode").IsUnique();

            entity.Property(e => e.Iatacode)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("IATACode");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Airport>(entity =>
        {
            entity.HasIndex(e => e.Iatacode, "UQ_Airport_IATACode").IsUnique();

            entity.Property(e => e.City).HasMaxLength(80);
            entity.Property(e => e.Country).HasMaxLength(80);
            entity.Property(e => e.Iatacode)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasColumnName("IATACode");
            entity.Property(e => e.Name).HasMaxLength(120);
            entity.Property(e => e.Timezone).HasMaxLength(64);
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasIndex(e => e.ConfirmationCode, "UQ_Booking_ConfirmationCode").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ConfirmationCode).HasMaxLength(8);
            entity.Property(e => e.CreatedUtc).HasDefaultValueSql("(getutcdate())", "DF_Bookings_CreatedUtc");
            entity.Property(e => e.PassengerEmail).HasMaxLength(120);
            entity.Property(e => e.PassengerFullName).HasMaxLength(120);

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.Status)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Bookings_BookingStatus");

            entity.HasOne(d => d.Ticket).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.TicketId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Bookings_Tickets");
        });

        modelBuilder.Entity<BookingStatus>(entity =>
        {
            entity.ToTable("BookingStatus");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description).HasMaxLength(50);
        });

        modelBuilder.Entity<Currency>(entity =>
        {
            entity.HasKey(e => e.Code);

            entity.Property(e => e.Code)
                .HasMaxLength(3)
                .IsFixedLength();
            entity.Property(e => e.Description).HasMaxLength(50);
        });

        modelBuilder.Entity<FareClass>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PK_FareClass");

            entity.Property(e => e.Code).HasMaxLength(2);
            entity.Property(e => e.Description).HasMaxLength(50);
        });

        modelBuilder.Entity<Flight>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.FlightNumber).HasMaxLength(8);
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_Flights_IsActive");

            entity.HasOne(d => d.Airline).WithMany(p => p.Flights)
                .HasForeignKey(d => d.AirlineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Flights_Airlines");

            entity.HasOne(d => d.DefaultAircraft).WithMany(p => p.Flights)
                .HasForeignKey(d => d.DefaultAircraftId)
                .HasConstraintName("FK_Flights_Aircrafts");

            entity.HasOne(d => d.DestinationAirport).WithMany(p => p.FlightDestinationAirports)
                .HasForeignKey(d => d.DestinationAirportId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Table2_Airports_Destination");

            entity.HasOne(d => d.OriginAirport).WithMany(p => p.FlightOriginAirports)
                .HasForeignKey(d => d.OriginAirportId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Table2_Airports_Origin");
        });

        modelBuilder.Entity<FlightSchedule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_FlightSchedule");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Flight).WithMany(p => p.FlightSchedules)
                .HasForeignKey(d => d.FlightId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FlightSchedule_Flights");

            entity.HasOne(d => d.Gate).WithMany(p => p.FlightSchedules)
                .HasForeignKey(d => d.GateId)
                .HasConstraintName("FK_FlightSchedule_Gates");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.FlightSchedules)
                .HasForeignKey(d => d.Status)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FlightSchedule_FlightScheduleStatus");
        });

        modelBuilder.Entity<FlightScheduleStatus>(entity =>
        {
            entity.ToTable("FlightScheduleStatus");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Status).HasMaxLength(10);
        });

        modelBuilder.Entity<Gate>(entity =>
        {
            entity.HasIndex(e => new { e.Code, e.AirportId }, "UQ_Gate_AirportId_Code").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(10);

            entity.HasOne(d => d.Airport).WithMany(p => p.Gates)
                .HasForeignKey(d => d.AirportId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Gates_Airports");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.BasePrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.CurrencyCode)
                .HasMaxLength(3)
                .IsFixedLength();
            entity.Property(e => e.FareClass).HasMaxLength(2);
            entity.Property(e => e.Taxes).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TotalPrice)
                .HasComputedColumnSql("([BasePrice]+[Taxes])", true)
                .HasColumnType("decimal(11, 2)");

            entity.HasOne(d => d.CurrencyCodeNavigation).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.CurrencyCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tickets_Currencies");

            entity.HasOne(d => d.FareClassNavigation).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.FareClass)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tickets_FareClasses");

            entity.HasOne(d => d.FlightSchedule).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.FlightScheduleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tickets_FlightSchedules");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}