

using System;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DTC_Dental.Models
{
	public class DentalContext : DbContext
	{
        public DentalContext(DbContextOptions<DentalContext> options)
            : base(options)
        { }

        public DbSet<Dentist> Dentists { get; set; } = null!;
        public DbSet<Service> Services { get; set; } = null!;
        public DbSet<AppointmentType> AppointmentTypes { get; set; } = null!;
        public DbSet<Appointment> Appointments { get; set; } = null!;
        public DbSet<Patient> Patients { get; set; } = null!;
        public DbSet<Visit> Visits { get; set; } = null!;
        public DbSet<CompletedService> CompletedServices { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompletedService>()
                .HasKey(cs => new { cs.ServiceID, cs.VisitID }); //Creates composite primary key property for completed services

            modelBuilder.Entity<Patient>()
               .HasOne(p => p.HOH)           // Configure the relationship for the HOH (Head of Household)
               .WithMany(h => h.Dependents)   // Configure the Dependents collection
               .HasForeignKey(p => p.PatientHOHID)  // Specify the foreign key property
               .OnDelete(DeleteBehavior.Restrict);  // Specify the delete behavior if necessary (e.g., Prevent cascade delete)

            // Define foreign key for Dentist
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Dentist)
                .WithMany() // Assuming a Dentist can have multiple Appointments
                .HasForeignKey(a => a.DentistID)
                .IsRequired();

            // Define foreign key for Patient
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany() // Assuming a Patient can have multiple Appointments
                .HasForeignKey(a => a.PatientID)
                .IsRequired();

            // Define foreign key for AppointmentType
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.AppointmentType)
                .WithMany() // Assuming an AppointmentType can have multiple Appointments
                .HasForeignKey(a => a.AppointmentTypeID)
                .IsRequired();

            modelBuilder.Entity<AppointmentType>().HasData(
                new AppointmentType
                {
                    AppointmentTypeID = 1,
                    AppointmentName = "Cosmetic Consultation - Adult",
                    Description = "Initial consultation with adult patient to discuss cosmetic dentistry options.",
                    Duration = 30
                },
                new AppointmentType
                {
                    AppointmentTypeID = 2,
                    AppointmentName = "Cosmetic Consultation - Child",
                    Description = "Initial consultation with child patient to discuss cosmetic dentistry options. ",
                    Duration = 30
                },
                new AppointmentType
                {
                    AppointmentTypeID = 3,
                    AppointmentName = "Cosmetic Consultation - Teen",
                    Description = "Initial consultation with teen patient to discuss cosmetic dentistry options. ",
                    Duration = 30
                },
                new AppointmentType
                {
                    AppointmentTypeID = 4,
                    AppointmentName = "Cosmetic Procedure - Adult",
                    Description = "Cosmetic dentistry procedure for adult patient.",
                    Duration = 120
                },
                new AppointmentType
                {
                    AppointmentTypeID = 5,
                    AppointmentName = "Cosmetic Procedure - Child",
                    Description = "Cosmetic dentistry procedure for child patient.",
                    Duration = 120
                },
                new AppointmentType
                {
                    AppointmentTypeID = 6,
                    AppointmentName = "Cosmetic Procedure - Teen",
                    Description = "Cosmetic dentistry procedure for teen patient.",
                    Duration = 120
                },
                new AppointmentType
                {
                    AppointmentTypeID = 7,
                    AppointmentName = "Endodontic Procedure - Adult",
                    Description = "Painless root canal therapy for adults.",
                    Duration = 90
                },
                new AppointmentType
                {
                    AppointmentTypeID = 8,
                    AppointmentName = "Endodontic Procedure - Child",
                    Description = "Painless root canal therapy for children.",
                    Duration = 90
                },
                new AppointmentType
                {
                    AppointmentTypeID = 9,
                    AppointmentName = "Endodontic Procedure - Teen",
                    Description = "Painless root canal therapy for teens.",
                    Duration = 90
                },
                new AppointmentType
                {
                    AppointmentTypeID = 10,
                    AppointmentName = "New Patient - Adult",
                    Description = "Meet new patient with general dental check-up including x-rays and teeth cleaning.",
                    Duration = 30
                },
                new AppointmentType
                {
                    AppointmentTypeID = 11,
                    AppointmentName = "New Patient - Child",
                    Description = "Meet new patient with general dental check-up including x-rays and teeth cleaning.",
                    Duration = 30
                },
                new AppointmentType
                {
                    AppointmentTypeID = 12,
                    AppointmentName = "New Patient - Teen",
                    Description = "Meet new patient with general dental check-up including x-rays and teeth cleaning.",
                    Duration = 30
                },
                new AppointmentType
                {
                    AppointmentTypeID = 13,
                    AppointmentName = "Periodontal Treatment - Adult",
                    Description = "Treatment (both preventative or restorative) for gum diseases.",
                    Duration = 60
                },
                new AppointmentType
                {
                    AppointmentTypeID = 14,
                    AppointmentName = "Periodontal Treatment - Child",
                    Description = "Treatment (both preventative or restorative) for gum diseases.",
                    Duration = 60
                },
                new AppointmentType
                {
                    AppointmentTypeID = 15,
                    AppointmentName = "Periodontal Treatment - Teen",
                    Description = "Treatment (both preventative or restorative) for gum diseases.",
                    Duration = 60
                },
                new AppointmentType
                {
                    AppointmentTypeID = 16,
                    AppointmentName = "Preventative Care - Adult",
                    Description = "General preventative care for an adult patient. The appointment will include x-rays\\54 teeth cleaning\\54 and general oral hygiene advising.",
                    Duration = 60
                },
                new AppointmentType
                {
                    AppointmentTypeID = 17,
                    AppointmentName = "Preventative Care - Child",
                    Description = "General preventative care for a child patient. The appointment will include x-rays\\54 teeth cleaning\\54 and general oral hygiene advising.",
                    Duration = 60
                },
                new AppointmentType
                {
                    AppointmentTypeID = 18,
                    AppointmentName = "Preventative Care - Teen",
                    Description = "General preventative care for a teen patient. The appointment will include x-rays\\54 teeth cleaning\\54 and general oral hygiene advising.",
                    Duration = 60
                },
                new AppointmentType
                {
                    AppointmentTypeID = 19,
                    AppointmentName = "Prosthodontic Care - Adult",
                    Description = "Restoration and/or replacement of missing or damaged teeth for adults.",
                    Duration = 60
                },
                new AppointmentType
                {
                    AppointmentTypeID = 20,
                    AppointmentName = "Prosthodontic Care - Child",
                    Description = "Restoration and/or replacement of missing or damaged teeth for children.",
                    Duration = 60
                },
                new AppointmentType
                {
                    AppointmentTypeID = 21,
                    AppointmentName = "Prosthodontic Care - Teen",
                    Description = "Restoration and/or replacement of missing or damaged teeth for teens.",
                    Duration = 60
                }
            );

            modelBuilder.Entity<Service>().HasData(
                new Service
                {
                    ServiceID = 1,
                    Description = "X-Ray",
                    Cost = 250
                },
                new Service
                {
                    ServiceID = 2,
                    Description = "Drill Cavity",
                    Cost = 350
                },
                new Service
                {
                    ServiceID = 3,
                    Description = "Crown",
                    Cost = 750
                },
                new Service
                {
                    ServiceID = 4,
                    Description = "Fill Cavity",
                    Cost = 250
                },
                new Service
                {
                    ServiceID = 5,
                    Description = "Extract Tooth",
                    Cost = 500
                },
                new Service
                {
                    ServiceID = 6,
                    Description = "Root Canal",
                    Cost = 1500
                },
                new Service
                {
                    ServiceID = 7,
                    Description = "Tooth Whitening",
                    Cost = 350
                },
                new Service
                {
                    ServiceID = 8,
                    Description = "Dental Implant",
                    Cost = 2500
                },
                new Service
                {
                    ServiceID = 9,
                    Description = "Dentures",
                    Cost = 4500
                },
                new Service
                {
                    ServiceID = 10,
                    Description = "Anesthetic",
                    Cost = 250
                },
                new Service
                {
                    ServiceID = 11,
                    Description = "Cleaning",
                    Cost = 150
                },
                new Service
                {
                    ServiceID = 12,
                    Description = "Pediatric Dental Counseling",
                    Cost = 350
                },
                new Service
                {
                    ServiceID = 13,
                    Description = "Dental Exam",
                    Cost = 500
                },
                new Service
                {
                    ServiceID = 14,
                    Description = "Dental Screening",
                    Cost = 250
                },
                new Service
                {
                    ServiceID = 15,
                    Description = "Flouride Treatment",
                    Cost = 275
                }
            );
            
            modelBuilder.Entity<Dentist>().HasData(
                new Dentist
                {
                    DentistID = 13340,
                    FirstName = "Michael",
                    LastName = "Jordan",
                    HireDate = DateTime.Parse("2022-06-22") 
                },
                new Dentist
                {
                    DentistID = 38345,
                    FirstName = "Magic",
                    LastName = "Johnson",
                    HireDate = DateTime.Parse("2022-05-13")
                },
                new Dentist
                {
                    DentistID = 18631,
                    FirstName = "Larry",
                    LastName = "Bird",
                    HireDate = DateTime.Parse("2022-01-11") 
                },
                new Dentist
                {
                    DentistID = 17381,
                    FirstName = "Hakeem",
                    LastName = "Olajuwon",
                    HireDate = DateTime.Parse("2022-04-04")
                },
                new Dentist
                {
                    DentistID = 43377,
                    FirstName ="Julius",
                    LastName = "Erving",
                    HireDate = DateTime.Parse("2023-07-02") 
                }
            );
            
            modelBuilder.Entity<Patient>().HasData(
                new Patient
                {
                    PatientID = 88306,
                    FirstName = "Lebron",
                    LastName = "James",
                    Address = "890 W. Bald Hill St.",
                    City = "Los Angeles",
                    State = "California",
                    Zip = "90001",
                    Phone = "213-200-9784",
                    Email = "KingJames23@gmail.com",
                    SSN = "637-91-4375",
                    DOB = "December 30, 1984",
                    Minor = false,
                    PatientHOHID = null,
                },
                new Patient
                {
                    PatientID = 91959,
                    FirstName = "Savannah",
                    LastName = "James",
                    Address = "890 W. Bald Hill St.",
                    City = "Los Angeles",
                    State = "California",
                    Zip = "90001",
                    Phone = "213-206-3371",
                    Email = "SavJames3@gmail.com",
                    SSN = "587-91-4946",
                    DOB = "March 11, 1990",
                    Minor = false,
                    PatientHOHID = 88306
                },
                new Patient
                {
                    PatientID = 56397,
                    FirstName = "Bronny",
                    LastName = "James",
                    Address = "890 W. Bald Hill St.",
                    City = "Los Angeles",
                    State = "California",
                    Zip = "90001",
                    Phone = "213-202-8978",
                    Email = "BronnyBuckets@gmail.com",
                    SSN = "653-90-4532",
                    DOB = "October 6, 2006",
                    Minor = true,
                    PatientHOHID = 88306
                },
                new Patient
                {
                    PatientID = 34437,
                    FirstName = "Bryce",
                    LastName = "James",
                    Address = "890 W. Bald Hill St.",
                    City = "Los Angeles",
                    State = "California",
                    Zip = "90001",
                    Phone = "213-219-8612",
                    Email = "BryceMaximus@yahoo.com",
                    SSN = "657-76-2220",
                    DOB = "June 14, 2007",
                    Minor = true,
                    PatientHOHID = 88306
                },
                new Patient
                {
                    PatientID = 66062,
                    FirstName = "Nikola",
                    LastName = "Jokic",
                    Address = "586 Silver Spear Street",
                    City = "Houston",
                    State = "Texas",
                    Zip = "77020",
                    Phone = "303-200-0132",
                    Email = "TheJoker15@yahoo.com",
                    SSN = "097-97-8353",
                    DOB = "February 19, 1995",
                    Minor = false,
                    PatientHOHID = null
                },
                new Patient
                {
                    PatientID = 41469,
                    FirstName = "Natalija",
                    LastName = "Jokic",
                    Address = "586 Silver Spear Street",
                    City = "Houston",
                    State = "Texas",
                    Zip = "77020",
                    Phone = "281-200-7859",
                    Email = "NatlijaJockic04@yahoo.com",
                    SSN = "389-93-4116",
                    DOB = "February 9, 2002",
                    Minor = false,
                    PatientHOHID = 66062
                },
                new Patient
                {
                    PatientID = 78740,
                    FirstName = "Alperen",
                    LastName = "Jokic",
                    Address = "586 Silver Spear Street",
                    City = "Houston",
                    State = "Texas",
                    Zip = "77020",
                    Phone = "281-204-7433",
                    Email = "BabyJokic28@gmail.com",
                    SSN = "746-07-9191",
                    DOB = "July 25, 2012",
                    Minor = true,
                    PatientHOHID = 66062
                },
                new Patient
                {
                    PatientID = 55746,
                    FirstName = "Steph",
                    LastName = "Curry",
                    Address = "89 Sulphur Springs Drive",
                    City = "San Francisco",
                    State = "California",
                    Zip = "94016",
                    Phone = "415-200-9230",
                    Email = "ChefCurry30@yahoo.com",
                    SSN = "151-58-7392",
                    DOB = "March 14, 1988",
                    Minor = false,
                    PatientHOHID = null
                },
                new Patient
                {
                    PatientID = 89349,
                    FirstName = "Kevin",
                    LastName = "Durant",
                    Address = "3201 W Dolores Rd.",
                    City = "Phoenix",
                    State = "Arizona",
                    Zip = "85001",
                    Phone = "480-200-2534",
                    Email = "SlimReaper@gmail.com",
                    SSN = "814-06-9775",
                    DOB = "September 29, 1988",
                    Minor = false,
                    PatientHOHID = null
                },
                new Patient
                {
                    PatientID = 74739,
                    FirstName = "Kawhi",
                    LastName = "Leonard",
                    Address = "7034 Beacon Ave.",
                    City = "Los Angeles",
                    State = "California",
                    Zip = "90013",
                    Phone = "213-204-7048",
                    Email = "TheKlaw02@yahoo.com",
                    SSN = "906-40-9355",
                    DOB = "June 29, 1991",
                    Minor = false,
                    PatientHOHID = null
                },
                new Patient
                {
                    PatientID = 67508,
                    FirstName = "Jabari",
                    LastName = "Smith Jr.",
                    Address = "808 Greenview Street",
                    City = "Houston",
                    State = "Texas",
                    Zip = "77356",
                    Phone = "281-254-6991",
                    Email = "JabariSmith1@yahoo.com",
                    SSN = "487-35-6778",
                    DOB = "May 13, 2003",
                    Minor = false,
                    PatientHOHID = null
                },
                new Patient
                {
                    PatientID = 86034,
                    FirstName = "James",
                    LastName = "Harden",
                    Address = "588 Purple Finch Street",
                    City = "Houston",
                    State = "Texas",
                    Zip = "77084",
                    Phone = "281-293-0296",
                    Email = "Thebeard13@gmail.com",
                    SSN = "526-38-6029",
                    DOB = "August 26, 1989",
                    Minor = false,
                    PatientHOHID = null
                },
                new Patient
                {
                    PatientID = 94954,
                    FirstName = "Russell",
                    LastName = "Westbrook",
                    Address = "9859 South Beach Court",
                    City = "Los Angeles",
                    State = "California",
                    Zip = "90001",
                    Phone = "213-237-5406",
                    Email = "MrTripleDouble@yahoo.com",
                    SSN = "238-55-1419",
                    DOB = "November 12, 1988",
                    Minor = false,
                    PatientHOHID = null
                },
                new Patient
                {
                    PatientID = 10013,
                    FirstName = "Troy",
                    LastName = "Thompson",
                    Address = "388 Woodside Street",
                    City = "Houston",
                    State = "Texas",
                    Zip = "77044",
                    Phone = "281-123-4567",
                    Email = "TroyThompson123@gmail.com",
                    SSN = "438-28-9051",
                    DOB = "November 15, 1977",
                    Minor = false,
                    PatientHOHID = null
                },
                new Patient
                {
                    PatientID = 42021,
                    FirstName = "Ausar",
                    LastName = "Thompson",
                    Address = "388 Woodside Street",
                    City = "Houston",
                    State = "Texas",
                    Zip = "77044",
                    Phone = "281-377-3441",
                    Email = "AusurXLNC@gmail.com",
                    SSN = "275-77-1974",
                    DOB = "January 30, 2014",
                    Minor = true,
                    PatientHOHID = 10013
                },
                new Patient
                {
                    PatientID = 36956,
                    FirstName = "Amen",
                    LastName = "Thompson",
                    Address = "388 Woodside Street",
                    City = "Houston",
                    State = "Texas",
                    Zip = "77044",
                    Phone = "281-268-5959",
                    Email = "AmenXLNC@gmail.com",
                    SSN = "508-44-2426",
                    DOB = "January 30, 2014",
                    Minor = true,
                    PatientHOHID = 10013
                },
                new Patient
                {
                    PatientID = 68064,
                    FirstName = "Jayson",
                    LastName = "Tatum",
                    Address = "1810 Rainy Day Drive",
                    City = "Boston",
                    State = "Massachusetts",
                    Zip = "02111",
                    Phone = "339-205-4305",
                    Email = "JTceltics0@gmail.com",
                    SSN = "713022929",
                    DOB = "March 3, 1998",
                    Minor = false,
                    PatientHOHID = null
                }
            );
            
            modelBuilder.Entity<Visit>().HasData(
                new Visit
                {
                    VisitID = 1001,
                    DentistID = 43377,
                    PatientID = 10013,
                    VisitDate = DateTime.Parse("2021-03-18")
                },
                new Visit
                {
                    VisitID = 1002,
                    DentistID = 17381,
                    PatientID = 67508,
                    VisitDate = DateTime.Parse("2021-07-29")
                },
                new Visit
                {
                    VisitID = 1003,
                    DentistID = 18631,
                    PatientID = 68064,
                    VisitDate = DateTime.Parse("2021-08-02")
                },
                new Visit
                {
                    VisitID = 1004,
                    DentistID = 38345,
                    PatientID = 91959,
                    VisitDate = DateTime.Parse("2021-10-11")
                },
                new Visit
                {
                    VisitID = 1005,
                    DentistID = 13340,
                    PatientID = 66062,
                    VisitDate = DateTime.Parse("2021-11-27")
                },
                new Visit
                {
                    VisitID = 1006,
                    DentistID = 18631,
                    PatientID = 42021,
                    VisitDate = DateTime.Parse("2021-12-27")
                },
                new Visit
                {
                    VisitID = 1007,
                    DentistID = 43377,
                    PatientID = 94954,
                    VisitDate = DateTime.Parse("2023-01-17")
                },
                new Visit
                {
                    VisitID = 1008,
                    DentistID = 43377,
                    PatientID = 86034,
                    VisitDate = DateTime.Parse("2023-01-17")
                },
                new Visit
                {
                    VisitID = 1009,
                    DentistID = 17381,
                    PatientID = 55746,
                    VisitDate = DateTime.Parse("2023-05-16")
                },
                new Visit
                {
                    VisitID = 1010,
                    DentistID = 38345,
                    PatientID = 34437,
                    VisitDate = DateTime.Parse("2023-09-09")
                }
            );
            
            modelBuilder.Entity<CompletedService>().HasData(
                new CompletedService
                {
                    VisitID = 1001,
                    ServiceID = 3
                },
                new CompletedService
                {
                    VisitID = 1002,
                    ServiceID = 7
                },
                new CompletedService
                {
                    VisitID = 1003,
                    ServiceID = 11
                },
                new CompletedService
                {
                    VisitID = 1004,
                    ServiceID = 1
                },
                new CompletedService
                {
                    VisitID = 1005,
                    ServiceID = 5
                },
                new CompletedService
                {
                    VisitID = 1006,
                    ServiceID = 15

                },
                new CompletedService
                {
                    VisitID = 1007,
                    ServiceID = 8
                },
                new CompletedService
                {
                    VisitID = 1008,
                    ServiceID = 4

                },
                new CompletedService
                {
                    VisitID = 1009,
                    ServiceID = 1
                },
                new CompletedService
                {
                    VisitID = 1010,
                    ServiceID = 2
                }
            );
            
            modelBuilder.Entity<Appointment>().HasData(
                new Appointment
                {
                    AppointmentID = 101,
                    DentistID = 38345,
                    PatientID = 55746,
                    AppointmentTypeID = 7,
                    AppointmentDate = DateTime.Parse("2023-11-12"),
                    StartTime = new TimeSpan(12, 0, 0)
                },
                new Appointment
                {
                    AppointmentID = 102,
                    DentistID = 43377,
                    PatientID = 34437,
                    AppointmentTypeID = 3,
                    AppointmentDate = DateTime.Parse("2023-11-27"),
                    StartTime = new TimeSpan(8, 0, 0)
                },
                new Appointment
                {
                    AppointmentID = 103,
                    DentistID = 13340,
                    PatientID = 74739,
                    AppointmentTypeID = 13,
                    AppointmentDate = DateTime.Parse("2023-12-10"),
                    StartTime = new TimeSpan(10, 30, 0)
                },
                new Appointment
                {
                    AppointmentID = 104,
                    DentistID = 18631,
                    PatientID = 36956,
                    AppointmentTypeID = 17,
                    AppointmentDate = DateTime.Parse("2023-12-21"),
                    StartTime = new TimeSpan(15, 30, 0)
                },
                new Appointment
                {
                    AppointmentID = 105,
                    DentistID = 13340,
                    PatientID = 88306,
                    AppointmentTypeID = 1,
                    AppointmentDate = DateTime.Parse("2024-01-03"),
                    StartTime = new TimeSpan(11, 0, 0)
                },
                new Appointment
                {
                    AppointmentID = 106,
                    DentistID = 17381,
                    PatientID = 94954,
                    AppointmentTypeID = 19,
                    AppointmentDate = DateTime.Parse("2024-01-17"),
                    StartTime = new TimeSpan(12, 30, 0)
                },
                new Appointment
                {
                    AppointmentID = 107,
                    DentistID = 18631,
                    PatientID = 41469,
                    AppointmentTypeID = 10,
                    AppointmentDate = DateTime.Parse("2024-02-23"),
                    StartTime = new TimeSpan(9, 0, 0)
                },
                new Appointment
                {
                    AppointmentID = 108,
                    DentistID = 18631,
                    PatientID = 78740,
                    AppointmentTypeID = 11,
                    AppointmentDate = DateTime.Parse("2024-02-23"),
                    StartTime = new TimeSpan(10, 0, 0)
                },
                new Appointment
                {
                    AppointmentID = 109,
                    DentistID = 38345,
                    PatientID = 89349,
                    AppointmentTypeID = 13,
                    AppointmentDate = DateTime.Parse("2024-03-12"),
                    StartTime = new TimeSpan(12, 0, 0)
                },
                new Appointment
                {
                    AppointmentID = 110,
                    DentistID = 43377,
                    PatientID = 56397,
                    AppointmentTypeID = 21,
                    AppointmentDate = DateTime.Parse("2024-04-01"),
                    StartTime = new TimeSpan(13, 0, 0)
                }
            );
            
        }
    }
}

