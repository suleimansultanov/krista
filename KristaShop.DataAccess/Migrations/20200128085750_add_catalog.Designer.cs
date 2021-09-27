﻿// <auto-generated />
using System;
using KristaShop.DataAccess.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KristaShop.DataAccess.Migrations
{
    [DbContext(typeof(KristaShopDbContext))]
    [Migration("20200128085750_add_catalog")]
    partial class add_catalog
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("KristaShop.DataAccess.Entities.AuthorizationLink", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("binary(16)");

                    b.Property<DateTime?>("login_date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("random_code")
                        .IsRequired()
                        .HasColumnType("varchar(64) CHARACTER SET utf8mb4")
                        .HasMaxLength(64);

                    b.Property<DateTime>("record_time_stamp")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("user_id")
                        .HasColumnType("binary(16)");

                    b.Property<DateTime?>("valid_to")
                        .HasColumnType("datetime(6)");

                    b.HasKey("id");

                    b.ToTable("authorization_link");
                });

            modelBuilder.Entity("KristaShop.DataAccess.Entities.Catalog", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("binary(16)");

                    b.Property<string>("description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("meta_description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("meta_keywords")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("meta_title")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("varchar(64) CHARACTER SET utf8mb4")
                        .HasMaxLength(64);

                    b.Property<int>("order_form")
                        .HasColumnType("int");

                    b.Property<string>("uri")
                        .IsRequired()
                        .HasColumnType("varchar(64) CHARACTER SET utf8mb4")
                        .HasMaxLength(64);

                    b.HasKey("id");

                    b.ToTable("catalogs");
                });

            modelBuilder.Entity("KristaShop.DataAccess.Entities.Category", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("binary(16)");

                    b.Property<bool>("is_visible")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("id");

                    b.ToTable("dict_category");
                });

            modelBuilder.Entity("KristaShop.DataAccess.Entities.Feedback", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("binary(16)");

                    b.Property<string>("email")
                        .HasColumnType("varchar(64) CHARACTER SET utf8mb4")
                        .HasMaxLength(64);

                    b.Property<string>("message")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("person")
                        .IsRequired()
                        .HasColumnType("varchar(64) CHARACTER SET utf8mb4")
                        .HasMaxLength(64);

                    b.Property<string>("phone")
                        .IsRequired()
                        .HasColumnType("varchar(64) CHARACTER SET utf8mb4")
                        .HasMaxLength(64);

                    b.Property<DateTime>("record_time_stamp")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("user_id")
                        .HasColumnType("binary(16)");

                    b.Property<DateTime?>("view_time_stamp")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("viewed")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("id");

                    b.ToTable("feedbacks");
                });

            modelBuilder.Entity("KristaShop.DataAccess.Entities.MenuContent", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("binary(16)");

                    b.Property<string>("body")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("layout")
                        .IsRequired()
                        .HasColumnType("varchar(64) CHARACTER SET utf8mb4")
                        .HasMaxLength(64);

                    b.Property<string>("meta_description")
                        .HasColumnType("varchar(500) CHARACTER SET utf8mb4")
                        .HasMaxLength(500);

                    b.Property<string>("meta_keywords")
                        .HasColumnType("varchar(500) CHARACTER SET utf8mb4")
                        .HasMaxLength(500);

                    b.Property<string>("meta_title")
                        .HasColumnType("varchar(500) CHARACTER SET utf8mb4")
                        .HasMaxLength(500);

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("varchar(64) CHARACTER SET utf8mb4")
                        .HasMaxLength(64);

                    b.Property<string>("url")
                        .IsRequired()
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.HasKey("id");

                    b.ToTable("menu_contents");
                });

            modelBuilder.Entity("KristaShop.DataAccess.Entities.MenuItem", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("binary(16)");

                    b.Property<string>("action_name")
                        .IsRequired()
                        .HasColumnType("varchar(64) CHARACTER SET utf8mb4")
                        .HasMaxLength(64);

                    b.Property<string>("controller_name")
                        .IsRequired()
                        .HasColumnType("varchar(64) CHARACTER SET utf8mb4")
                        .HasMaxLength(64);

                    b.Property<string>("icon")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("menu_type")
                        .HasColumnType("int");

                    b.Property<int>("order")
                        .HasColumnType("int");

                    b.Property<string>("parameters")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("varchar(64) CHARACTER SET utf8mb4")
                        .HasMaxLength(64);

                    b.Property<string>("url")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.HasKey("id");

                    b.ToTable("menu_items");
                });

            modelBuilder.Entity("KristaShop.DataAccess.Entities.Setting", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("binary(16)");

                    b.Property<string>("key")
                        .IsRequired()
                        .HasColumnType("varchar(64) CHARACTER SET utf8mb4")
                        .HasMaxLength(64);

                    b.Property<string>("value")
                        .IsRequired()
                        .HasColumnType("varchar(1000) CHARACTER SET utf8mb4")
                        .HasMaxLength(1000);

                    b.HasKey("id");

                    b.ToTable("dict_settings");
                });

            modelBuilder.Entity("KristaShop.DataAccess.Entities.UrlAccess", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("binary(16)");

                    b.Property<string>("access_groups_json")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("acl")
                        .HasColumnType("int");

                    b.Property<string>("denied_groups_json")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("url")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("id");

                    b.ToTable("url_access");
                });
#pragma warning restore 612, 618
        }
    }
}