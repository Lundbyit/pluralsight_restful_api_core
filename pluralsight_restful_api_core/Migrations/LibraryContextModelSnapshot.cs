﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using pluralsight_restful_api_core.Entities;

namespace pluralsight_restful_api_core.Migrations
{
    [DbContext(typeof(LibraryContext))]
    partial class LibraryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("pluralsight_restful_api_core.Entities.Author", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("DateOfBirth");

                    b.Property<DateTimeOffset?>("DateOfDeath");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("pluralsight_restful_api_core.Entities.Book", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AuthorId");

                    b.Property<string>("Description")
                        .HasMaxLength(500);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("pluralsight_restful_api_core.Entities.Book", b =>
                {
                    b.HasOne("pluralsight_restful_api_core.Entities.Author", "Author")
                        .WithMany("Books")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
