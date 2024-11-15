﻿// <auto-generated />
using System;
using Assesment.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Assesment.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Assesment.Models.Post", b =>
                {
                    b.Property<int>("PostID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PostID"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DatePosted")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("PostID");

                    b.HasIndex("UserID");

                    b.ToTable("Posts");

                    b.HasData(
                        new
                        {
                            PostID = 1,
                            Content = "This is a post about C# basics.",
                            DatePosted = new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Introduction to C#",
                            UserID = 1
                        },
                        new
                        {
                            PostID = 2,
                            Content = "This post covers ASP.NET Core fundamentals.",
                            DatePosted = new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "ASP.NET Core Overview",
                            UserID = 1
                        },
                        new
                        {
                            PostID = 3,
                            Content = "JavaScript tips for beginners.",
                            DatePosted = new DateTime(2024, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "JavaScript Tips",
                            UserID = 2
                        });
                });

            modelBuilder.Entity("Assesment.Models.UserProfile", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"));

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.ToTable("UserProfiles");

                    b.HasData(
                        new
                        {
                            UserID = 1,
                            DateOfBirth = new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "johndoe@example.com",
                            FirstName = "John",
                            LastName = "Doe"
                        },
                        new
                        {
                            UserID = 2,
                            DateOfBirth = new DateTime(1992, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "janesmith@example.com",
                            FirstName = "Jane",
                            LastName = "Smith"
                        });
                });

            modelBuilder.Entity("Assesment.Models.Post", b =>
                {
                    b.HasOne("Assesment.Models.UserProfile", "User")
                        .WithMany("Posts")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Assesment.Models.UserProfile", b =>
                {
                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}
