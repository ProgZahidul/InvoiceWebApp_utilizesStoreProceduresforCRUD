﻿// <auto-generated />
using System;
using InvoiceWebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InvoiceWebApp.Migrations
{
    [DbContext(typeof(InvoiceContext))]
    partial class InvoiceContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("InvoiceWebApp.Models.Invoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("CardHolder")
                        .HasColumnType("bit");

                    b.Property<string>("ContactNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PassengerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("InvoiceWebApp.Models.Ticket", b =>
                {
                    b.Property<int>("TicketId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TicketId"));

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("RouteName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TicketId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("InvoiceWebApp.Models.TicketInfo", b =>
                {
                    b.Property<int>("TicketInfoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TicketInfoId"));

                    b.Property<int?>("InvoiceId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int?>("TicketId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("TicketInfoId");

                    b.HasIndex("InvoiceId");

                    b.HasIndex("TicketId");

                    b.ToTable("TicketInfos");
                });

            modelBuilder.Entity("InvoiceWebApp.Models.TicketInfo", b =>
                {
                    b.HasOne("InvoiceWebApp.Models.Invoice", "Invoice")
                        .WithMany("TicketInfos")
                        .HasForeignKey("InvoiceId");

                    b.HasOne("InvoiceWebApp.Models.Ticket", "ticket")
                        .WithMany("TicketInfos")
                        .HasForeignKey("TicketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Invoice");

                    b.Navigation("ticket");
                });

            modelBuilder.Entity("InvoiceWebApp.Models.Invoice", b =>
                {
                    b.Navigation("TicketInfos");
                });

            modelBuilder.Entity("InvoiceWebApp.Models.Ticket", b =>
                {
                    b.Navigation("TicketInfos");
                });
#pragma warning restore 612, 618
        }
    }
}
