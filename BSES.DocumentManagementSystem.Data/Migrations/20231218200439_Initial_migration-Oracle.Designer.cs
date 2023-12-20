﻿// <auto-generated />
using System;
using BSES.DocumentManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;

#nullable disable

namespace BSES.DocumentManagementSystem.Data.Migrations
{
    [DbContext(typeof(DMSDBContext))]
    [Migration("20231218200439_Initial_migration-Oracle")]
    partial class Initial_migrationOracle
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            OracleModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BSES.DocumentManagementSystem.Data.AccessLog", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(19)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ID"));

                    b.Property<int>("ActionTaken")
                        .HasColumnType("NUMBER(10)");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<string>("CreatedUserID")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("DocumentID")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<int>("RecordStatusCode")
                        .HasColumnType("NUMBER(10)");

                    b.Property<DateTime>("UpdatedDateTime")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<string>("UpdatedUserID")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.HasKey("ID");

                    b.ToTable("AccessLogs");
                });

            modelBuilder.Entity("BSES.DocumentManagementSystem.Data.Document", b =>
                {
                    b.Property<string>("DocumentID")
                        .HasColumnType("NVARCHAR2(450)");

                    b.Property<int?>("Category")
                        .HasColumnType("NUMBER(10)");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<string>("CreatedUserID")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<int>("DocumentAccessScope")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("DocumentName")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("DocumentPath")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<int?>("DocumentType")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int>("DocumentVersion")
                        .HasColumnType("NUMBER(10)");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("NUMBER(1)");

                    b.Property<int>("RecordStatusCode")
                        .HasColumnType("NUMBER(10)");

                    b.Property<DateTime>("UpdatedDateTime")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<string>("UpdatedUserID")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("Users")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<long>("Year")
                        .HasColumnType("NUMBER(19)");

                    b.HasKey("DocumentID");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("BSES.DocumentManagementSystem.Data.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"));

                    b.Property<string>("CompanyCode")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<string>("CreatedUserID")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<int>("RecordStatusCode")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("SecretKey")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<DateTime>("UpdatedDateTime")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<string>("UpdatedUserID")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<int>("UserAccessScope")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<int>("UserRight")
                        .HasColumnType("NUMBER(10)");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}