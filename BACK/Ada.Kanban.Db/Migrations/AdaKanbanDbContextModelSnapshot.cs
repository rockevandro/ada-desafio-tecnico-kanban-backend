﻿// <auto-generated />
using System;
using Ada.Kanban.Db.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Ada.Kanban.Db.Migrations
{
    [DbContext(typeof(AdaKanbanDbContext))]
    partial class AdaKanbanDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Ada.Kanban.Db.Entities.Card", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Conteudo")
                        .HasColumnType("text");

                    b.Property<string>("Lista")
                        .HasColumnType("text");

                    b.Property<string>("Titulo")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Cards");
                });
#pragma warning restore 612, 618
        }
    }
}
