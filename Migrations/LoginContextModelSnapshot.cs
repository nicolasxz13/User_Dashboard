﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using User_Dashboard.Data;

#nullable disable

namespace User_Dashboard.Migrations
{
    [DbContext(typeof(LoginContext))]
    partial class LoginContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("User_Dashboard.Models.Comment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CommentText")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("MessageId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("UserCommentId")
                        .HasColumnType("int");

                    b.HasKey("CommentId");

                    b.HasIndex("MessageId");

                    b.HasIndex("UserCommentId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("User_Dashboard.Models.Message", b =>
                {
                    b.Property<int>("MessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("MessageText")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("RecipientMessageId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("UserMessageId")
                        .HasColumnType("int");

                    b.HasKey("MessageId");

                    b.HasIndex("RecipientMessageId");

                    b.HasIndex("UserMessageId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("User_Dashboard.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Last_Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("UserLevelId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserLevelId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("User_Dashboard.Models.UserLevel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Userlevel")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("UserLevels");
                });

            modelBuilder.Entity("User_Dashboard.Models.Comment", b =>
                {
                    b.HasOne("User_Dashboard.Models.Message", "Message")
                        .WithMany("Comments")
                        .HasForeignKey("MessageId");

                    b.HasOne("User_Dashboard.Models.User", "UserComment")
                        .WithMany("Comments")
                        .HasForeignKey("UserCommentId");

                    b.Navigation("Message");

                    b.Navigation("UserComment");
                });

            modelBuilder.Entity("User_Dashboard.Models.Message", b =>
                {
                    b.HasOne("User_Dashboard.Models.User", "RecipientMessage")
                        .WithMany("ReceivedMessages")
                        .HasForeignKey("RecipientMessageId");

                    b.HasOne("User_Dashboard.Models.User", "UserMessage")
                        .WithMany("Messages")
                        .HasForeignKey("UserMessageId");

                    b.Navigation("RecipientMessage");

                    b.Navigation("UserMessage");
                });

            modelBuilder.Entity("User_Dashboard.Models.User", b =>
                {
                    b.HasOne("User_Dashboard.Models.UserLevel", "UserLevel")
                        .WithMany()
                        .HasForeignKey("UserLevelId");

                    b.Navigation("UserLevel");
                });

            modelBuilder.Entity("User_Dashboard.Models.Message", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("User_Dashboard.Models.User", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Messages");

                    b.Navigation("ReceivedMessages");
                });
#pragma warning restore 612, 618
        }
    }
}
