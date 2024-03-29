﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskApplication.Data;

#nullable disable

namespace TaskApplication.Migrations
{
    [DbContext(typeof(TaskApplicationDbContext))]
    [Migration("20221028155243_Guid")]
    partial class Guid
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.10");

            modelBuilder.Entity("TaskApplication.Data.Models.Todo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT")
                        .HasColumnName("created_at");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("description");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("INTEGER")
                        .HasColumnName("is_completed");

                    b.Property<bool>("IsImportant")
                        .HasColumnType("INTEGER")
                        .HasColumnName("is_important");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.ToTable("Todos");
                });
#pragma warning restore 612, 618
        }
    }
}
