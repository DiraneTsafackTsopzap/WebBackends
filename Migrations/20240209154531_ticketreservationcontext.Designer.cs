﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using webapp.DataContext;

namespace webapp.Migrations
{
    [DbContext(typeof(TicketReservationContext))]
    [Migration("20240209154531_ticketreservationcontext")]
    partial class ticketreservationcontext
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("webapp.Models.Benutzer", b =>
                {
                    b.Property<Guid>("BenutzerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("boolean");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("BenutzerId");

                    b.ToTable("benutzer");
                });

            modelBuilder.Entity("webapp.Models.Bus", b =>
                {
                    b.Property<Guid>("BusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("AbfahrtsZeit")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("BusCategory")
                        .HasColumnType("integer");

                    b.Property<string>("BusName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("EndZiel")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("FahrerId")
                        .HasColumnType("uuid");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("KlimaAnlage")
                        .HasColumnType("boolean");

                    b.Property<string>("StartZiel")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Unterhaltung")
                        .HasColumnType("boolean");

                    b.HasKey("BusId");

                    b.HasIndex("FahrerId")
                        .IsUnique();

                    b.ToTable("bus");
                });

            modelBuilder.Entity("webapp.Models.Fahrer", b =>
                {
                    b.Property<Guid>("FahrerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Age")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FahrerName")
                        .HasColumnType("text");

                    b.Property<string>("FahrerVorName")
                        .HasColumnType("text");

                    b.Property<string>("Image")
                        .HasColumnType("text");

                    b.HasKey("FahrerId");

                    b.ToTable("fahrer");
                });

            modelBuilder.Entity("webapp.Models.Reservation", b =>
                {
                    b.Property<Guid>("ReservationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BenutzerId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("BusId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsReserved")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("ReservationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("SitzplatzId")
                        .HasColumnType("uuid");

                    b.HasKey("ReservationId");

                    b.HasIndex("BenutzerId");

                    b.HasIndex("BusId");

                    b.HasIndex("SitzplatzId");

                    b.ToTable("reservation");
                });

            modelBuilder.Entity("webapp.Models.Sitzplatz", b =>
                {
                    b.Property<Guid>("SitzplatzId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BusId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsVerfugbar")
                        .HasColumnType("boolean");

                    b.Property<int>("Nummer")
                        .HasColumnType("integer");

                    b.HasKey("SitzplatzId");

                    b.HasIndex("BusId");

                    b.ToTable("sitzplatz");
                });

            modelBuilder.Entity("webapp.Models.Bus", b =>
                {
                    b.HasOne("webapp.Models.Fahrer", "Fahrer")
                        .WithOne("Bus")
                        .HasForeignKey("webapp.Models.Bus", "FahrerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("webapp.Models.Reservation", b =>
                {
                    b.HasOne("webapp.Models.Benutzer", "Benutzer")
                        .WithMany("ListesReservations")
                        .HasForeignKey("BenutzerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("webapp.Models.Bus", "Bus")
                        .WithMany()
                        .HasForeignKey("BusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("webapp.Models.Sitzplatz", "Sitzplatz")
                        .WithMany()
                        .HasForeignKey("SitzplatzId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("webapp.Models.Sitzplatz", b =>
                {
                    b.HasOne("webapp.Models.Bus", "Bus")
                        .WithMany()
                        .HasForeignKey("BusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
