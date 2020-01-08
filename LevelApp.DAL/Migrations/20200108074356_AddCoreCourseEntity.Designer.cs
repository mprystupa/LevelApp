﻿// <auto-generated />
using System;
using LevelApp.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LevelApp.DAL.Migrations
{
    [DbContext(typeof(LevelAppContext))]
    [Migration("20200108074356_AddCoreCourseEntity")]
    partial class AddCoreCourseEntity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("LevelApp.DAL.Models.Core.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime?>("DateCreatedUtc");

                    b.Property<DateTime?>("DateDeletedUtc");

                    b.Property<DateTime?>("DateModifiedUtc");

                    b.Property<int?>("DeletedBy");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("FirstName")
                        .HasMaxLength(30);

                    b.Property<string>("LastName")
                        .HasMaxLength(30);

                    b.Property<int?>("ModifiedBy");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("CoreAppUser");
                });

            modelBuilder.Entity("LevelApp.DAL.Models.Core.AppUserLesson", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("LessonId");

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime?>("DateCreatedUtc");

                    b.Property<DateTime?>("DateDeletedUtc");

                    b.Property<DateTime?>("DateModifiedUtc");

                    b.Property<int?>("DeletedBy");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsFavourite");

                    b.Property<int?>("ModifiedBy");

                    b.Property<int>("Status");

                    b.HasKey("UserId", "LessonId");

                    b.HasAlternateKey("Id", "LessonId", "UserId");

                    b.HasIndex("LessonId");

                    b.ToTable("CoreAppUserLesson");
                });

            modelBuilder.Entity("LevelApp.DAL.Models.Core.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AuthorId");

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime?>("DateCreatedUtc");

                    b.Property<DateTime?>("DateDeletedUtc");

                    b.Property<DateTime?>("DateModifiedUtc");

                    b.Property<int?>("DeletedBy");

                    b.Property<string>("Description")
                        .HasMaxLength(500);

                    b.Property<int?>("ModifiedBy");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("TagList")
                        .HasMaxLength(1000);

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("CoreCourse");
                });

            modelBuilder.Entity("LevelApp.DAL.Models.Core.Lesson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AuthorId");

                    b.Property<string>("Content");

                    b.Property<int?>("CourseId");

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime?>("DateCreatedUtc");

                    b.Property<DateTime?>("DateDeletedUtc");

                    b.Property<DateTime?>("DateModifiedUtc");

                    b.Property<int?>("DeletedBy");

                    b.Property<string>("Description")
                        .HasMaxLength(500);

                    b.Property<int?>("ModifiedBy");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("CourseId");

                    b.ToTable("CoreLesson");
                });

            modelBuilder.Entity("LevelApp.DAL.Models.Core.AppUserLesson", b =>
                {
                    b.HasOne("LevelApp.DAL.Models.Core.Lesson", "Lesson")
                        .WithMany("AppUserLessons")
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LevelApp.DAL.Models.Core.AppUser", "User")
                        .WithMany("AppUserLessons")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LevelApp.DAL.Models.Core.Course", b =>
                {
                    b.HasOne("LevelApp.DAL.Models.Core.AppUser", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LevelApp.DAL.Models.Core.Lesson", b =>
                {
                    b.HasOne("LevelApp.DAL.Models.Core.AppUser", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LevelApp.DAL.Models.Core.Course", "Course")
                        .WithMany("Lessons")
                        .HasForeignKey("CourseId");
                });
#pragma warning restore 612, 618
        }
    }
}
