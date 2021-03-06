﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using persistence_relational;

namespace persistence_relational.Migrations
{
    [DbContext(typeof(WvuFootballDataContext))]
    [Migration("20200706183558_initialCommit")]
    partial class initialCommit
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("models.Player", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PositionId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("PositionId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("models.PlayerRoster", b =>
                {
                    b.Property<string>("PlayerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RosterId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("PlayerId", "RosterId");

                    b.HasIndex("RosterId");

                    b.ToTable("PlayerRosters");
                });

            modelBuilder.Entity("models.Position", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Abbreviation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsOffense")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("models.Roster", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Conference")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Rosters");
                });

            modelBuilder.Entity("models.Player", b =>
                {
                    b.HasOne("models.Position", "Position")
                        .WithMany()
                        .HasForeignKey("PositionId");
                });

            modelBuilder.Entity("models.PlayerRoster", b =>
                {
                    b.HasOne("models.Player", "Player")
                        .WithMany("PlayerRosters")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("models.Roster", "Roster")
                        .WithMany("PlayerRosters")
                        .HasForeignKey("RosterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
