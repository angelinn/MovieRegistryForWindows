using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using DataAccess;

namespace DataAccess.Migrations
{
    [DbContext(typeof(MovieRegistryContext))]
    partial class MovieRegistryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348");

            modelBuilder.Entity("DataAccess.Entities.Episode", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Season");

                    b.Property<int>("Serie");

                    b.HasKey("ID");
                });

            modelBuilder.Entity("DataAccess.Entities.Movie", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ImdbID");

                    b.Property<string>("Title");

                    b.Property<int>("Year");

                    b.HasKey("ID");
                });

            modelBuilder.Entity("DataAccess.Entities.Record", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("EpisodeID");

                    b.Property<bool>("IsSeries");

                    b.Property<int?>("MovieID");

                    b.Property<DateTime?>("SeenAt");

                    b.Property<int?>("UserID");

                    b.HasKey("ID");
                });

            modelBuilder.Entity("DataAccess.Entities.WindowsUser", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("ID");
                });

            modelBuilder.Entity("DataAccess.Entities.Record", b =>
                {
                    b.HasOne("DataAccess.Entities.Episode")
                        .WithMany()
                        .HasForeignKey("EpisodeID");

                    b.HasOne("DataAccess.Entities.Movie")
                        .WithMany()
                        .HasForeignKey("MovieID");

                    b.HasOne("DataAccess.Entities.WindowsUser")
                        .WithMany()
                        .HasForeignKey("UserID");
                });
        }
    }
}
