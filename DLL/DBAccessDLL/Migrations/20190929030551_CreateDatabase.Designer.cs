﻿// <auto-generated />
using System;
using DBAccessDLL.EF.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DBAccessDLL.Migrations
{
    [DbContext(typeof(ExamContext))]
    [Migration("20190929030551_CreateDatabase")]
    partial class CreateDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity("DBAccessDLL.EF.Entity.Account", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Qing_CreateTime")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<DateTime?>("Qing_DeleteTime");

                    b.Property<bool>("Qing_IsDelete")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<long>("Qing_Sequence")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0L);

                    b.Property<DateTime>("Qing_UpdateTime")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<long>("Qing_Version")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0L);

                    b.Property<string>("avatar");

                    b.Property<string>("email");

                    b.Property<string>("introduction");

                    b.Property<string>("name");

                    b.Property<string>("password");

                    b.Property<string>("phone");

                    b.Property<int>("sex");

                    b.Property<string>("username");

                    b.HasKey("id");

                    b.ToTable("Account");
                });
#pragma warning restore 612, 618
        }
    }
}
