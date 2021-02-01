﻿// <auto-generated />
using System;
using FoodVault.Modules.Storage.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FoodVault.Modules.Storage.Infrastructure.Database.Migrations
{
    [DbContext(typeof(StorageContext))]
    [Migration("20210201131053_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("FoodVault.Framework.Application.FileUploads.FileUpload", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ContentType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ExpirationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Extension")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RelativeFileLocation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Size")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("UploadTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("FileUploads");
                });

            modelBuilder.Entity("FoodVault.Framework.Infrastructure.InternalCommands.InternalCommand", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CommandType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Payload")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ProcessedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("InternalCommands");
                });

            modelBuilder.Entity("FoodVault.Framework.Infrastructure.Outbox.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EventType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Payload")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ProcessedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RaisingTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("OutboxMessages");
                });

            modelBuilder.Entity("FoodVault.Modules.Storage.Domain.FoodStorages.FoodStorage", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("_isDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("IsDeleted");

                    b.HasKey("Id");

                    b.ToTable("FoodStorages");
                });

            modelBuilder.Entity("FoodVault.Modules.Storage.Domain.Products.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Barcode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Brand")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ImageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("FoodVault.Modules.Storage.Domain.FoodStorages.FoodStorage", b =>
                {
                    b.OwnsMany("FoodVault.Modules.Storage.Domain.FoodStorages.StoredProduct", "StoredProducts", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime?>("ExpirationDate")
                                .HasColumnType("datetime2");

                            b1.Property<Guid>("FoodStorageId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid?>("ProductId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Quantity")
                                .HasColumnType("int");

                            b1.HasKey("Id");

                            b1.HasIndex("FoodStorageId");

                            b1.ToTable("StoredProducts");

                            b1.WithOwner()
                                .HasForeignKey("FoodStorageId");
                        });

                    b.Navigation("StoredProducts");
                });
#pragma warning restore 612, 618
        }
    }
}
