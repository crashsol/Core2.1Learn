﻿// <auto-generated />
using System;
using EFCoreLearn.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EFCoreLearn.Migrations
{
    [DbContext(typeof(TestDbcontext))]
    partial class TestDbcontextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EFCoreLearn.Models.Blog", b =>
                {
                    b.Property<int>("BlogId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasMaxLength(80);

                    b.Property<string>("Url")
                        .HasMaxLength(80);

                    b.HasKey("BlogId");

                    b.ToTable("Blog");

                    b.HasData(
                        new { BlogId = 1, Name = "1", Url = "www" }
                    );
                });

            modelBuilder.Entity("EFCoreLearn.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasMaxLength(80);

                    b.Property<string>("Test1")
                        .HasMaxLength(80);

                    b.Property<string>("Test2")
                        .HasMaxLength(120);

                    b.Property<Guid>("TestGuid")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NEWID()");

                    b.Property<decimal>("ddd")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("EFCoreLearn.Models.Post", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BlogId");

                    b.Property<string>("Content")
                        .HasMaxLength(80);

                    b.Property<string>("Title")
                        .HasMaxLength(80);

                    b.HasKey("PostId");

                    b.HasIndex("BlogId");

                    b.ToTable("Post");

                    b.HasData(
                        new { PostId = 1, BlogId = 1, Content = "123", Title = "123" },
                        new { PostId = 2, BlogId = 1, Content = "123", Title = "123" },
                        new { PostId = 3, BlogId = 1, Content = "123", Title = "123" }
                    );
                });

            modelBuilder.Entity("EFCoreLearn.Models.SellDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BuyerId");

                    b.Property<string>("Name")
                        .HasMaxLength(80);

                    b.Property<int?>("SellerId");

                    b.HasKey("Id");

                    b.HasIndex("BuyerId");

                    b.HasIndex("SellerId");

                    b.ToTable("SellDetail");

                    b.HasData(
                        new { Id = 1, BuyerId = 1, SellerId = 2 }
                    );
                });

            modelBuilder.Entity("EFCoreLearn.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasMaxLength(80);

                    b.Property<string>("Name")
                        .HasMaxLength(80);

                    b.HasKey("Id");

                    b.ToTable("User");

                    b.HasData(
                        new { Id = 1, Name = "1" },
                        new { Id = 2, Name = "2" }
                    );
                });

            modelBuilder.Entity("EFCoreLearn.Models.Order", b =>
                {
                    b.OwnsOne("EFCoreLearn.Models.Address", "Address", b1 =>
                        {
                            b1.Property<int?>("OrderId");

                            b1.Property<string>("City")
                                .HasMaxLength(80);

                            b1.Property<string>("Street")
                                .HasMaxLength(80);

                            b1.ToTable("Address");

                            b1.HasOne("EFCoreLearn.Models.Order")
                                .WithOne("Address")
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

            modelBuilder.Entity("EFCoreLearn.Models.SellDetail", b =>
                {
                    b.HasOne("EFCoreLearn.Models.User", "Buyer")
                        .WithMany()
                        .HasForeignKey("BuyerId");

                    b.HasOne("EFCoreLearn.Models.User", "Seller")
                        .WithMany()
                        .HasForeignKey("SellerId");
                });
#pragma warning restore 612, 618
        }
    }
}
