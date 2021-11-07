﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_services_ielectric.Domain.Models;
using web_services_ielectric.Extensions;

namespace web_services_ielectric.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Technician> Technicians { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<SpareRequest> SpareRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            // START PERSON //
            // Constraints
            builder.Entity<Person>().ToTable("Persons");
            builder.Entity<Person>().HasKey(p => p.Id);
            builder.Entity<Person>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Person>().Property(p => p.Names).IsRequired().HasMaxLength(30);
            builder.Entity<Person>().Property(p => p.LastNames).IsRequired().HasMaxLength(30);
            builder.Entity<Person>().Property(p => p.CellphoneNumber).IsRequired();
            builder.Entity<Person>().Property(p => p.Address).IsRequired().HasMaxLength(50);
            builder.Entity<Person>().Property(p => p.Email).IsRequired();
            builder.Entity<Person>().Property(p => p.Password).IsRequired();
            // END PERSON //

            // START CLIENT //
            // Constraints
            builder.Entity<Client>().ToTable("Clients");

            //Relationships
            builder.Entity<Client>().HasMany(p => p.Appointments)
                .WithOne(p => p.Client)
                .HasForeignKey(p => p.ClientId);

            //Example data
            builder.Entity<Client>().HasData(
                new Client
                {
                    Id = 1, 
                    Names = "Jose",
                    LastNames = "Feliciano",
                    Address = "North Avenue",
                    CellphoneNumber = 987879219,
                    Email = "f.felicianos@gmail.com",
                    Password = "Feliciano123"
                }
            );
            // END CLIENT //

            // START TECHNICIAN //
            //Constraints
            builder.Entity<Technician>().ToTable("Technicians");

            //Relationships
            builder.Entity<Technician>().HasMany(p => p.Appointments)
                .WithOne(p => p.Technician)
                .HasForeignKey(p => p.TechnicianId);

            builder.Entity<Technician>().HasMany(p => p.Reports)
                .WithOne(p => p.Technician)
                .HasForeignKey(p => p.TechnicianId);

            builder.Entity<Technician>().HasMany(p => p.SpareRequests)
                .WithOne(p => p.Technician)
                .HasForeignKey(p => p.TechnicianId);

            //Example
            builder.Entity<Technician>().HasData(
                new Technician
                {
                    Id = 2, 
                    Names = "Estefano Sebastian",
                    LastNames = "Bran Zapata",
                    Address = "Los Angeles",
                    CellphoneNumber = 987899219,
                    Email = "sebas@gmail.com",
                    Password = "Sebas123"
                }
            );
            // END TECHNICIAN //

            // START ANNOUNCEMENT //
            // Constraints
            builder.Entity<Announcement>().ToTable("Announcements");
            builder.Entity<Announcement>().HasKey(p => p.Id);
            builder.Entity<Announcement>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Announcement>().Property(p => p.Title).IsRequired().HasMaxLength(30);
            builder.Entity<Announcement>().Property(p => p.Description).IsRequired();
            builder.Entity<Announcement>().Property(p => p.Content).IsRequired();
            builder.Entity<Announcement>().Property(p => p.UrlToImage).IsRequired();
            builder.Entity<Announcement>().Property(p => p.TypeOfAnnouncement).IsRequired();
            builder.Entity<Announcement>().Property(p => p.Visible).IsRequired();
            // END ANNOUNCEMENT //


            // START APPOINTMENT //
            // Constraints
            builder.Entity<Appointment>().ToTable("Appointments");
            builder.Entity<Appointment>().HasKey(p => p.Id);
            builder.Entity<Appointment>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Appointment>().Property(p => p.DateReserve).IsRequired();
            builder.Entity<Appointment>().Property(p => p.Hour).IsRequired();
            builder.Entity<Appointment>().Property(p => p.Done).IsRequired();
            builder.Entity<Appointment>().Property(p => p.ClientId).IsRequired();
            builder.Entity<Appointment>().Property(p => p.TechnicianId).IsRequired();
            
            //Relationships
            builder.Entity<Appointment>().HasMany(p => p.Reports)
                .WithOne(p => p.Appointment)
                .HasForeignKey(p => p.AppointmentId);
            
            builder.Entity<Appointment>().HasMany(p => p.SpareRequests)
                .WithOne(p => p.Appointment)
                .HasForeignKey(p => p.AppointmentId);

            //Example
            builder.Entity<Appointment>().HasData(
                new Appointment { 
                    Id = 1, 
                    DateReserve = "10-12-2021",
                    DateAttention = "10-12-2021",
                    Hour = "15:00",
                    ClientId = 1,
                    TechnicianId = 2,
                    Done = false
                }
            );
            // END APPOINTMENT //


            // START REPORT //
            // Constraints
            builder.Entity<Report>().ToTable("Reports");
            builder.Entity<Report>().HasKey(p => p.Id);
            builder.Entity<Report>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Report>().Property(p => p.Observation).IsRequired().HasMaxLength(100);
            builder.Entity<Report>().Property(p => p.Diagnosis).IsRequired().HasMaxLength(300);
            builder.Entity<Report>().Property(p => p.RepairDescription).IsRequired().HasMaxLength(300);
            builder.Entity<Report>().Property(p => p.ImagePath).IsRequired();
            builder.Entity<Report>().Property(p => p.Date).IsRequired();
            builder.Entity<Report>().Property(p => p.AppointmentId).IsRequired();
            builder.Entity<Report>().Property(p => p.TechnicianId).IsRequired();
            
            //Example
            builder.Entity<Report>().HasData(
                new Report()
                {
                    Id = 1,
                    Observation = "The microwave smell bad",
                    Diagnosis = "A component in the microchip is burned",
                    RepairDescription = "I replaced the microchip successfully",
                    ImagePath = "https://google.com/images",
                    Date = "10-12-24",
                    TechnicianId = 2,
                    AppointmentId = 1
                }
            );
            // END REPORT //
            
            // START SPARE REQUEST
            // Constraints
            builder.Entity<SpareRequest>().ToTable("SpareRequests");
            builder.Entity<SpareRequest>().HasKey(p => p.Id);
            builder.Entity<SpareRequest>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<SpareRequest>().Property(p => p.Description).IsRequired().HasMaxLength(300);;
            builder.Entity<SpareRequest>().Property(p => p.Date).IsRequired();
            builder.Entity<SpareRequest>().Property(p => p.ImagePath).IsRequired();
            builder.Entity<SpareRequest>().Property(p => p.AppointmentId).IsRequired();
            builder.Entity<SpareRequest>().Property(p => p.TechnicianId).IsRequired();

            builder.Entity<SpareRequest>().HasData(
                new SpareRequest()
                {
                    Id = 1,
                    Description = "We need buy these pieces: #1, #2, #3...",
                    Date = "10-12-24",
                    ImagePath = "https://google.com/images",
                    AppointmentId = 1,
                    TechnicianId = 2
                }
            );
            // END SPARE REQUEST
            
            // Apply Snake Case Naming Convention to All Objects
            builder.UseSnakeCaseNamingConvention();
        }
    }
}