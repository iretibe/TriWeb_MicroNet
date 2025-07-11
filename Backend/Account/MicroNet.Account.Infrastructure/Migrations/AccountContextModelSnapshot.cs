﻿// <auto-generated />
using System;
using MicroNet.Account.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MicroNet.Account.Infrastructure.Migrations
{
    [DbContext(typeof(AccountContext))]
    partial class AccountContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("account")
                .HasAnnotation("ProductVersion", "9.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MicroNet.Account.Core.Entities.AccountTermination", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("AccountTerminations", "account");
                });

            modelBuilder.Entity("MicroNet.Account.Core.Entities.AccountTransfer", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DestinationBranch")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("AccountTransfers", "account");
                });

            modelBuilder.Entity("MicroNet.Account.Core.Entities.Withdrawal", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Network")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentMode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Reference")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.Property<string>("WalletNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Withdrawals", "account");
                });

            modelBuilder.Entity("MicroNet.Account.Core.Entities.AccountTermination", b =>
                {
                    b.OwnsOne("MicroNet.Account.Core.ValueObjects.AuditInfo", "AuditInfo", b1 =>
                        {
                            b1.Property<Guid>("AccountTerminationId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime?>("ApprovedAt")
                                .HasColumnType("datetime2");

                            b1.Property<string>("ApprovedBy")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<DateTime>("CreatedAt")
                                .HasColumnType("datetime2");

                            b1.Property<string>("CreatedBy")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<DateTime>("DeletedAt")
                                .HasColumnType("datetime2");

                            b1.Property<string>("DeletedBy")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<DateTime>("UpdatedAt")
                                .HasColumnType("datetime2");

                            b1.Property<string>("UpdatedBy")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("AccountTerminationId");

                            b1.ToTable("AccountTerminations", "account");

                            b1.WithOwner()
                                .HasForeignKey("AccountTerminationId");
                        });

                    b.OwnsOne("MicroNet.Account.Core.ValueObjects.AccountDetails", "TerminatedAccount", b1 =>
                        {
                            b1.Property<Guid>("AccountTerminationId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("AccountName")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("AccountNumber")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<decimal>("Balance")
                                .HasColumnType("decimal(18,2)");

                            b1.Property<string>("BranchName")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("AccountTerminationId");

                            b1.ToTable("AccountTerminations", "account");

                            b1.WithOwner()
                                .HasForeignKey("AccountTerminationId");
                        });

                    b.Navigation("AuditInfo")
                        .IsRequired();

                    b.Navigation("TerminatedAccount")
                        .IsRequired();
                });

            modelBuilder.Entity("MicroNet.Account.Core.Entities.AccountTransfer", b =>
                {
                    b.OwnsOne("MicroNet.Account.Core.ValueObjects.AuditInfo", "AuditInfo", b1 =>
                        {
                            b1.Property<Guid>("AccountTransferId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime?>("ApprovedAt")
                                .HasColumnType("datetime2");

                            b1.Property<string>("ApprovedBy")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<DateTime>("CreatedAt")
                                .HasColumnType("datetime2");

                            b1.Property<string>("CreatedBy")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<DateTime>("DeletedAt")
                                .HasColumnType("datetime2");

                            b1.Property<string>("DeletedBy")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<DateTime>("UpdatedAt")
                                .HasColumnType("datetime2");

                            b1.Property<string>("UpdatedBy")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("AccountTransferId");

                            b1.ToTable("AccountTransfers", "account");

                            b1.WithOwner()
                                .HasForeignKey("AccountTransferId");
                        });

                    b.OwnsOne("MicroNet.Account.Core.ValueObjects.AccountDetails", "SourceAccount", b1 =>
                        {
                            b1.Property<Guid>("AccountTransferId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("AccountName")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("AccountNumber")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<decimal>("Balance")
                                .HasColumnType("decimal(18,2)");

                            b1.Property<string>("BranchName")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("AccountTransferId");

                            b1.ToTable("AccountTransfers", "account");

                            b1.WithOwner()
                                .HasForeignKey("AccountTransferId");
                        });

                    b.Navigation("AuditInfo")
                        .IsRequired();

                    b.Navigation("SourceAccount")
                        .IsRequired();
                });

            modelBuilder.Entity("MicroNet.Account.Core.Entities.Withdrawal", b =>
                {
                    b.OwnsOne("MicroNet.Account.Core.ValueObjects.AuditInfo", "AuditInfo", b1 =>
                        {
                            b1.Property<Guid>("WithdrawalId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime?>("ApprovedAt")
                                .HasColumnType("datetime2");

                            b1.Property<string>("ApprovedBy")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<DateTime>("CreatedAt")
                                .HasColumnType("datetime2");

                            b1.Property<string>("CreatedBy")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<DateTime>("DeletedAt")
                                .HasColumnType("datetime2");

                            b1.Property<string>("DeletedBy")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<DateTime>("UpdatedAt")
                                .HasColumnType("datetime2");

                            b1.Property<string>("UpdatedBy")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("WithdrawalId");

                            b1.ToTable("Withdrawals", "account");

                            b1.WithOwner()
                                .HasForeignKey("WithdrawalId");
                        });

                    b.Navigation("AuditInfo")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
