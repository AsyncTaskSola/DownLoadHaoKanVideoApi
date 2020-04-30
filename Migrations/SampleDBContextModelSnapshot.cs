﻿// <auto-generated />
using System;
using DownLoadHaoKanVideoAPI.Dbdata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DownLoadHaoKanVideoAPI.Migrations
{
    [DbContext(typeof(SampleDBContext))]
    partial class SampleDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3");

            modelBuilder.Entity("DownLoadHaoKanVideoAPI.Entity.Employee", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<sbyte?>("Gender")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<sbyte?>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("Emplyees");
                });
#pragma warning restore 612, 618
        }
    }
}