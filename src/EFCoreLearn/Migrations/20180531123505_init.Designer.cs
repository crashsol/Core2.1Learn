﻿// <auto-generated />
using System;
using EFCoreLearn.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EFCoreLearn.Migrations
{
    [DbContext(typeof(TestDbcontext))]
    [Migration("20180531123505_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-rtm-30799");

            modelBuilder.Entity("EFCoreLearn.Models.Blog", b =>
                {
                    b.Property<int>("BlogId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Url");

                    b.HasKey("BlogId");

                    b.ToTable("Blog");

                    b.HasData(
                        new { BlogId = 1, Name = "1", Url = "www" }
                    );
                });

            modelBuilder.Entity("EFCoreLearn.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Id");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("EFCoreLearn.Models.Post", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BlogId");

                    b.Property<string>("Content");

                    b.Property<string>("Title");

                    b.HasKey("PostId");

                    b.HasIndex("BlogId");

                    b.ToTable("Post");

                    b.HasData(
                        new { PostId = 1, BlogId = 1, Content = "123", Title = "123" },
                        new { PostId = 2, BlogId = 1, Content = "123", Title = "123" },
                        new { PostId = 3, BlogId = 1, Content = "123", Title = "123" }
                    );
                });

            modelBuilder.Entity("EFCoreLearn.Models.Order", b =>
                {
                    b.OwnsOne("EFCoreLearn.Models.Address", "OrderAddress", b1 =>
                        {
                            b1.Property<int?>("OrderId");

                            b1.Property<string>("City");

                            b1.Property<string>("Street");

                            b1.ToTable("Order");

                            b1.HasOne("EFCoreLearn.Models.Order")
                                .WithOne("OrderAddress")
                                .HasForeignKey("EFCoreLearn.Models.Address", "OrderId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("EFCoreLearn.Models.Post", b =>
                {
                    b.HasOne("EFCoreLearn.Models.Blog")
                        .WithMany("Posts")
                        .HasForeignKey("BlogId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}