﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NexosTest.DAL.Contexts;

namespace NexosTest.DAL.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NexosTest.Entities.Entities.Doctor", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("especialidad")
                        .IsRequired();

                    b.Property<string>("hospitalResidente")
                        .IsRequired();

                    b.Property<string>("nombreCompleto")
                        .IsRequired();

                    b.Property<string>("numeroCredencial")
                        .IsRequired()
                        .HasMaxLength(12);

                    b.HasKey("id");

                    b.ToTable("Doctores");
                });

            modelBuilder.Entity("NexosTest.Entities.Entities.Paciente", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("codigoPostal");

                    b.Property<int?>("doctorid");

                    b.Property<string>("nombreCompleto")
                        .IsRequired();

                    b.Property<string>("numeroSeguro")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.Property<string>("telefonoContacto")
                        .IsRequired();

                    b.HasKey("id");

                    b.HasIndex("doctorid");

                    b.ToTable("Pacientes");
                });

            modelBuilder.Entity("NexosTest.Entities.Entities.Paciente", b =>
                {
                    b.HasOne("NexosTest.Entities.Entities.Doctor", "doctor")
                        .WithMany("Pacientes")
                        .HasForeignKey("doctorid");
                });
#pragma warning restore 612, 618
        }
    }
}
