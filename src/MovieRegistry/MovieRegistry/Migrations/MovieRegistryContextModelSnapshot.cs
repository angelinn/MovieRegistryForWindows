using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MovieRegistry.Models;

namespace MovieRegistry.Migrations
{
    [DbContext(typeof(MovieRegistryContext))]
    partial class MovieRegistryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431");

            modelBuilder.Entity("MovieRegistry.Models.Entities.Episode", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Season");

                    b.Property<int>("Serie");

                    b.HasKey("ID");

                    b.ToTable("Episodes");
                });

            modelBuilder.Entity("MovieRegistry.Models.Entities.Movie", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ImdbID");

                    b.Property<string>("Title");

                    b.Property<int>("Year");

                    b.HasKey("ID");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("MovieRegistry.Models.Entities.Record", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("EpisodeID");

                    b.Property<bool>("IsSeries");

                    b.Property<int?>("MovieID");

                    b.Property<DateTime?>("SeenAt");

                    b.Property<int?>("UserID");

                    b.HasKey("ID");

                    b.HasIndex("EpisodeID");

                    b.HasIndex("MovieID");

                    b.HasIndex("UserID");

                    b.ToTable("Records");
                });

            modelBuilder.Entity("MovieRegistry.Models.Entities.WindowsUser", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MovieRegistry.Models.Entities.Record", b =>
                {
                    b.HasOne("MovieRegistry.Models.Entities.Episode", "Episode")
                        .WithMany("Records")
                        .HasForeignKey("EpisodeID");

                    b.HasOne("MovieRegistry.Models.Entities.Movie", "Movie")
                        .WithMany("Records")
                        .HasForeignKey("MovieID");

                    b.HasOne("MovieRegistry.Models.Entities.WindowsUser", "User")
                        .WithMany("Records")
                        .HasForeignKey("UserID");
                });
        }
    }
}
