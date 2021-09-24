﻿// <auto-generated />
using System;
using Anslagstavlan.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Anslagstavlan.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Anslagstavlan.Models.ChatMessageModel", b =>
                {
                    b.Property<int>("ChatMessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ChatRoomId")
                        .HasColumnType("int");

                    b.Property<int>("ChatUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ChatMessageId");

                    b.HasIndex("ChatRoomId");

                    b.HasIndex("ChatUserId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Anslagstavlan.Models.ChatRoomModel", b =>
                {
                    b.Property<int>("ChatRoomId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ChatRoomName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ChatRoomOwner")
                        .HasColumnType("int");

                    b.Property<string>("Img")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ChatRoomId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("Anslagstavlan.Models.ChatUserModel", b =>
                {
                    b.Property<int>("ChatUserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Img")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ChatUserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Anslagstavlan.Models.ChatMessageModel", b =>
                {
                    b.HasOne("Anslagstavlan.Models.ChatRoomModel", "ChatRoom")
                        .WithMany("Messages")
                        .HasForeignKey("ChatRoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Anslagstavlan.Models.ChatUserModel", "ChatUser")
                        .WithMany()
                        .HasForeignKey("ChatUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChatRoom");

                    b.Navigation("ChatUser");
                });

            modelBuilder.Entity("Anslagstavlan.Models.ChatRoomModel", b =>
                {
                    b.Navigation("Messages");
                });
#pragma warning restore 612, 618
        }
    }
}
