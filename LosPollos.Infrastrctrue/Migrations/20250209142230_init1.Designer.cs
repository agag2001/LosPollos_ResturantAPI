﻿// <auto-generated />
using LosPollos.Infrastructrue.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LosPollos.Infrastructrue.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250209142230_init1")]
    partial class init1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LosPollos.Domain.Entities.Dish", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ResturantId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ResturantId");

                    b.ToTable("Dishes");
                });

            modelBuilder.Entity("LosPollos.Domain.Entities.Resturant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CantactEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CantactPhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("HasDelivery")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Resturants");
                });

            modelBuilder.Entity("LosPollos.Domain.Entities.Dish", b =>
                {
                    b.HasOne("LosPollos.Domain.Entities.Resturant", "Resturant")
                        .WithMany("Dishes")
                        .HasForeignKey("ResturantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Resturant");
                });

            modelBuilder.Entity("LosPollos.Domain.Entities.Resturant", b =>
                {
                    b.OwnsOne("LosPollos.Domain.Entities.Address", "Address", b1 =>
                        {
                            b1.Property<int>("ResturantId")
                                .HasColumnType("int");

                            b1.Property<string>("City")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("PostalCode")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Street")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ResturantId");

                            b1.ToTable("Resturants");

                            b1.WithOwner()
                                .HasForeignKey("ResturantId");
                        });

                    b.Navigation("Address");
                });

            modelBuilder.Entity("LosPollos.Domain.Entities.Resturant", b =>
                {
                    b.Navigation("Dishes");
                });
#pragma warning restore 612, 618
        }
    }
}
