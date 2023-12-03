﻿// <auto-generated />
using System;
using BSES.DocumentManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BSES.DocumentManagementSystem.Data.Migrations
{
    [DbContext(typeof(DMSDBContext))]
    partial class DMSDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BSES.DocumentManagementSystem.Data.AccessLog", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ID"));

                    b.Property<int>("ActionTaken")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedUserID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DocumentID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RecordStatusCode")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedUserID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("AccessLogs");
                });

            modelBuilder.Entity("BSES.DocumentManagementSystem.Data.Document", b =>
                {
                    b.Property<string>("DocumentID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("Category")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedUserID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DocumentAccessScope")
                        .HasColumnType("int");

                    b.Property<string>("DocumentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DocumentPath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DocumentType")
                        .HasColumnType("int");

                    b.Property<int>("DocumentVersion")
                        .HasColumnType("int");

                    b.Property<int>("RecordStatusCode")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedUserID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Users")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Year")
                        .HasColumnType("bigint");

                    b.HasKey("DocumentID");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("BSES.DocumentManagementSystem.Data.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"));

                    b.Property<string>("CompanyCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedUserID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RecordStatusCode")
                        .HasColumnType("int");

                    b.Property<string>("SecretKey")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedUserID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserAccessScope")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserRight")
                        .HasColumnType("int");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
