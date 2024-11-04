﻿// <auto-generated />
using System;
using KupujemProdajemWebApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace KupujemProdajemWebApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241031130823_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("KupujemProdajemWebApp.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("KupujemProdajemWebApp.Models.Advertisement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<int?>("AdvertisementCategoryId")
                        .HasColumnType("int");

                    b.Property<int>("AdvertisementCondition")
                        .HasColumnType("int");

                    b.Property<int?>("AdvertisementGroupId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("DeliveryType")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsFixedPrice")
                        .HasColumnType("bit");

                    b.Property<bool>("IsReplacement")
                        .HasColumnType("bit");

                    b.Property<int?>("Likes")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<int?>("Viewers")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("AdvertisementCategoryId");

                    b.HasIndex("AdvertisementGroupId");

                    b.HasIndex("UserId");

                    b.ToTable("Advertisements");
                });

            modelBuilder.Entity("KupujemProdajemWebApp.Models.AdvertisementCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AdvertisementCategories");
                });

            modelBuilder.Entity("KupujemProdajemWebApp.Models.AdvertisementGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AdvertisementCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AdvertisementCategoryId");

                    b.ToTable("AdvertisementGroups");
                });

            modelBuilder.Entity("KupujemProdajemWebApp.Models.Favorite", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AdvertisementId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AdvertisementId");

                    b.HasIndex("UserId");

                    b.ToTable("Favorites");
                });

            modelBuilder.Entity("KupujemProdajemWebApp.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("KupujemProdajemWebApp.Models.Advertisement", b =>
                {
                    b.HasOne("KupujemProdajemWebApp.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KupujemProdajemWebApp.Models.AdvertisementCategory", null)
                        .WithMany("Advertisements")
                        .HasForeignKey("AdvertisementCategoryId");

                    b.HasOne("KupujemProdajemWebApp.Models.AdvertisementGroup", null)
                        .WithMany("Advertisements")
                        .HasForeignKey("AdvertisementGroupId");

                    b.HasOne("KupujemProdajemWebApp.Models.User", null)
                        .WithMany("Advertisements")
                        .HasForeignKey("UserId");

                    b.Navigation("Address");
                });

            modelBuilder.Entity("KupujemProdajemWebApp.Models.AdvertisementGroup", b =>
                {
                    b.HasOne("KupujemProdajemWebApp.Models.AdvertisementCategory", "AdvertisementCategory")
                        .WithMany("AdvertisementGroups")
                        .HasForeignKey("AdvertisementCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AdvertisementCategory");
                });

            modelBuilder.Entity("KupujemProdajemWebApp.Models.Favorite", b =>
                {
                    b.HasOne("KupujemProdajemWebApp.Models.Advertisement", "Advertisement")
                        .WithMany("Favorites")
                        .HasForeignKey("AdvertisementId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("KupujemProdajemWebApp.Models.User", "User")
                        .WithMany("Favorites")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Advertisement");

                    b.Navigation("User");
                });

            modelBuilder.Entity("KupujemProdajemWebApp.Models.User", b =>
                {
                    b.HasOne("KupujemProdajemWebApp.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("KupujemProdajemWebApp.Models.Advertisement", b =>
                {
                    b.Navigation("Favorites");
                });

            modelBuilder.Entity("KupujemProdajemWebApp.Models.AdvertisementCategory", b =>
                {
                    b.Navigation("AdvertisementGroups");

                    b.Navigation("Advertisements");
                });

            modelBuilder.Entity("KupujemProdajemWebApp.Models.AdvertisementGroup", b =>
                {
                    b.Navigation("Advertisements");
                });

            modelBuilder.Entity("KupujemProdajemWebApp.Models.User", b =>
                {
                    b.Navigation("Advertisements");

                    b.Navigation("Favorites");
                });
#pragma warning restore 612, 618
        }
    }
}