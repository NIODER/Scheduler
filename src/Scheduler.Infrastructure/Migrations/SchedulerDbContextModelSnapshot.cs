﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Scheduler.Infrastructure.Persistance;

#nullable disable

namespace Scheduler.Infrastructure.Migrations
{
    [DbContext(typeof(SchedulerDbContext))]
    partial class SchedulerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Scheduler.Domain.FinancialPlanAggregate.FinancialPlan", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("character varying(120)");

                    b.HasKey("Id");

                    b.ToTable("FinancialPlans");
                });

            modelBuilder.Entity("Scheduler.Domain.GroupAggregate.Group", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("character varying(120)");

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("Scheduler.Domain.ProblemAggregate.Problem", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Deadline")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<Guid?>("GroupId")
                        .HasColumnType("uuid");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("character varying(120)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Problems");
                });

            modelBuilder.Entity("Scheduler.Domain.UserAggregate.Entities.FriendsInvite", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("AddressieId")
                        .HasColumnType("uuid");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<Guid>("SenderId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AddressieId");

                    b.HasIndex("SenderId");

                    b.ToTable("FriendsInvites", (string)null);
                });

            modelBuilder.Entity("Scheduler.Domain.UserAggregate.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("character varying(120)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<int>("Settings")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Scheduler.Domain.UserAggregate.ValueObjects.UserFriend", b =>
                {
                    b.Property<Guid>("UserId1")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId2")
                        .HasColumnType("uuid");

                    b.HasKey("UserId1", "UserId2");

                    b.HasIndex("UserId2");

                    b.ToTable("UserFriend");
                });

            modelBuilder.Entity("Scheduler.Domain.FinancialPlanAggregate.FinancialPlan", b =>
                {
                    b.OwnsMany("Scheduler.Domain.FinancialPlanAggregate.ValueObjects.Charge", "Charges", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uuid");

                            b1.Property<string>("ChargeName")
                                .IsRequired()
                                .HasMaxLength(120)
                                .HasColumnType("character varying(120)");

                            b1.Property<DateTime>("Created")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<string>("Description")
                                .IsRequired()
                                .HasMaxLength(1000)
                                .HasColumnType("character varying(1000)");

                            b1.Property<long>("ExpireDays")
                                .HasColumnType("bigint");

                            b1.Property<Guid>("FinancialPlanId")
                                .HasColumnType("uuid");

                            b1.Property<decimal?>("MaximalCost")
                                .HasColumnType("numeric");

                            b1.Property<decimal>("MinimalCost")
                                .HasColumnType("numeric");

                            b1.Property<int>("Priority")
                                .HasColumnType("integer");

                            b1.Property<bool>("Repeat")
                                .HasColumnType("boolean");

                            b1.HasKey("Id");

                            b1.HasIndex("FinancialPlanId");

                            b1.ToTable("Charge");

                            b1.WithOwner()
                                .HasForeignKey("FinancialPlanId");
                        });

                    b.Navigation("Charges");
                });

            modelBuilder.Entity("Scheduler.Domain.GroupAggregate.Group", b =>
                {
                    b.OwnsMany("Scheduler.Domain.GroupAggregate.Entities.GroupInvite", "Invites", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("CreatorId")
                                .HasColumnType("uuid");

                            b1.Property<DateTime?>("Expire")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<Guid>("GroupId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Message")
                                .IsRequired()
                                .HasMaxLength(500)
                                .HasColumnType("character varying(500)");

                            b1.Property<int>("Permissions")
                                .HasColumnType("integer");

                            b1.Property<long?>("Usages")
                                .HasColumnType("bigint");

                            b1.HasKey("Id");

                            b1.HasIndex("GroupId");

                            b1.ToTable("GroupInvite");

                            b1.WithOwner()
                                .HasForeignKey("GroupId");
                        });

                    b.OwnsMany("Scheduler.Domain.FinancialPlanAggregate.ValueObjects.FinancialPlanId", "FinancialPlanIds", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b1.Property<int>("Id"));

                            b1.Property<Guid>("GroupId")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("Value")
                                .HasColumnType("uuid")
                                .HasColumnName("FinancialPlanId");

                            b1.HasKey("Id");

                            b1.HasIndex("GroupId");

                            b1.ToTable("Groups_FinancialPlanIds");

                            b1.WithOwner()
                                .HasForeignKey("GroupId");
                        });

                    b.OwnsMany("Scheduler.Domain.ProblemAggregate.ValueObjects.ProblemId", "ProblemIds", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b1.Property<int>("Id"));

                            b1.Property<Guid>("GroupId")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("Value")
                                .HasColumnType("uuid")
                                .HasColumnName("ProblemId");

                            b1.HasKey("Id");

                            b1.HasIndex("GroupId");

                            b1.ToTable("Groups_ProblemIds");

                            b1.WithOwner()
                                .HasForeignKey("GroupId");
                        });

                    b.OwnsMany("Scheduler.Domain.GroupAggregate.ValueObjects.GroupUser", "Users", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("GroupId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Permissions")
                                .HasColumnType("integer");

                            b1.HasKey("UserId", "GroupId");

                            b1.HasIndex("GroupId");

                            b1.ToTable("GroupUser");

                            b1.WithOwner()
                                .HasForeignKey("GroupId");
                        });

                    b.Navigation("FinancialPlanIds");

                    b.Navigation("Invites");

                    b.Navigation("ProblemIds");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Scheduler.Domain.UserAggregate.Entities.FriendsInvite", b =>
                {
                    b.HasOne("Scheduler.Domain.UserAggregate.User", null)
                        .WithMany("ReceivedFriendsInvites")
                        .HasForeignKey("AddressieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Scheduler.Domain.UserAggregate.User", null)
                        .WithMany("SendedFriendsInvites")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Scheduler.Domain.UserAggregate.User", b =>
                {
                    b.OwnsMany("Scheduler.Domain.GroupAggregate.ValueObjects.GroupId", "GroupIds", b1 =>
                        {
                            b1.Property<Guid>("Value")
                                .HasColumnType("uuid")
                                .HasColumnName("GroupId");

                            b1.Property<Guid>("UserId")
                                .HasColumnType("uuid");

                            b1.HasKey("Value");

                            b1.HasIndex("UserId");

                            b1.ToTable("GroupId");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsMany("Scheduler.Domain.FinancialPlanAggregate.ValueObjects.FinancialPlanId", "FinancialPlanIds", b1 =>
                        {
                            b1.Property<Guid>("Value")
                                .HasColumnType("uuid")
                                .HasColumnName("FinancialPlanId");

                            b1.Property<Guid>("UserId")
                                .HasColumnType("uuid");

                            b1.HasKey("Value");

                            b1.HasIndex("UserId");

                            b1.ToTable("Users_FinancialPlanIds");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsMany("Scheduler.Domain.ProblemAggregate.ValueObjects.ProblemId", "ProblemIds", b1 =>
                        {
                            b1.Property<Guid>("Value")
                                .HasColumnType("uuid")
                                .HasColumnName("ProblemId");

                            b1.Property<Guid>("UserId")
                                .HasColumnType("uuid");

                            b1.HasKey("Value");

                            b1.HasIndex("UserId");

                            b1.ToTable("Users_ProblemIds");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsMany("Scheduler.Domain.UserAggregate.ValueObjects.UserId", "BlackListUserIds", b1 =>
                        {
                            b1.Property<Guid>("Value")
                                .HasColumnType("uuid")
                                .HasColumnName("BlockedUserId");

                            b1.Property<Guid>("UserId")
                                .HasColumnType("uuid");

                            b1.HasKey("Value");

                            b1.HasIndex("UserId");

                            b1.ToTable("UserId");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("BlackListUserIds");

                    b.Navigation("FinancialPlanIds");

                    b.Navigation("GroupIds");

                    b.Navigation("ProblemIds");
                });

            modelBuilder.Entity("Scheduler.Domain.UserAggregate.ValueObjects.UserFriend", b =>
                {
                    b.HasOne("Scheduler.Domain.UserAggregate.User", "User1")
                        .WithMany("ReceivedUserFriends")
                        .HasForeignKey("UserId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Scheduler.Domain.UserAggregate.User", "User2")
                        .WithMany("InitiatedUserFriends")
                        .HasForeignKey("UserId2")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User1");

                    b.Navigation("User2");
                });

            modelBuilder.Entity("Scheduler.Domain.UserAggregate.User", b =>
                {
                    b.Navigation("InitiatedUserFriends");

                    b.Navigation("ReceivedFriendsInvites");

                    b.Navigation("ReceivedUserFriends");

                    b.Navigation("SendedFriendsInvites");
                });
#pragma warning restore 612, 618
        }
    }
}
