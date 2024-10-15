﻿// <auto-generated />
using System;
using BussinessObject.ContextData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BussinessObject.Migrations
{
    [DbContext(typeof(BlogContext))]
    [Migration("20241014065517_BlogInit")]
    partial class BlogInit
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BussinessObject.Model.AuthModel.RoleModel", b =>
                {
                    b.Property<int>("roleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("roleId"));

                    b.Property<string>("roleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("roleId");

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            roleId = 1,
                            roleName = "Administration"
                        },
                        new
                        {
                            roleId = 2,
                            roleName = "User"
                        });
                });

            modelBuilder.Entity("BussinessObject.Model.AuthModel.UserModel", b =>
                {
                    b.Property<Guid>("userId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("birthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("fullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("gender")
                        .HasColumnType("bit");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<byte[]>("passwordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("passwordResetToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("passwordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("resetTokenExpires")
                        .HasColumnType("datetime2");

                    b.Property<int>("roleId")
                        .HasColumnType("int");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("verificationToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("verifiedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("userId");

                    b.HasIndex("roleId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("BussinessObject.Model.BlogModel.CommentModel", b =>
                {
                    b.Property<Guid>("commentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("commentText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("createAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("postId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("userId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("commentId");

                    b.HasIndex("postId");

                    b.HasIndex("userId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("BussinessObject.Model.BlogModel.PostModel", b =>
                {
                    b.Property<Guid>("postId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("imageFile")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.Property<int>("likeCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("publishAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("updatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("userId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("postId");

                    b.HasIndex("userId");

                    b.ToTable("Post");
                });

            modelBuilder.Entity("BussinessObject.Model.BlogModel.Post_Tag", b =>
                {
                    b.Property<int>("tagId")
                        .HasColumnType("int");

                    b.Property<Guid>("postId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("tagId", "postId");

                    b.HasIndex("postId");

                    b.ToTable("post_tag");
                });

            modelBuilder.Entity("BussinessObject.Model.BlogModel.TagModel", b =>
                {
                    b.Property<int>("tagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("tagId"));

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("tagId");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("BussinessObject.Model.AuthModel.UserModel", b =>
                {
                    b.HasOne("BussinessObject.Model.AuthModel.RoleModel", "role")
                        .WithMany("users")
                        .HasForeignKey("roleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("role");
                });

            modelBuilder.Entity("BussinessObject.Model.BlogModel.CommentModel", b =>
                {
                    b.HasOne("BussinessObject.Model.BlogModel.PostModel", "post")
                        .WithMany("comments")
                        .HasForeignKey("postId");

                    b.HasOne("BussinessObject.Model.AuthModel.UserModel", "user")
                        .WithMany("comments")
                        .HasForeignKey("userId");

                    b.Navigation("post");

                    b.Navigation("user");
                });

            modelBuilder.Entity("BussinessObject.Model.BlogModel.PostModel", b =>
                {
                    b.HasOne("BussinessObject.Model.AuthModel.UserModel", "user")
                        .WithMany("posts")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("BussinessObject.Model.BlogModel.Post_Tag", b =>
                {
                    b.HasOne("BussinessObject.Model.BlogModel.PostModel", "post")
                        .WithMany("post_Tags")
                        .HasForeignKey("postId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BussinessObject.Model.BlogModel.TagModel", "tag")
                        .WithMany("post_Tags")
                        .HasForeignKey("tagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("post");

                    b.Navigation("tag");
                });

            modelBuilder.Entity("BussinessObject.Model.AuthModel.RoleModel", b =>
                {
                    b.Navigation("users");
                });

            modelBuilder.Entity("BussinessObject.Model.AuthModel.UserModel", b =>
                {
                    b.Navigation("comments");

                    b.Navigation("posts");
                });

            modelBuilder.Entity("BussinessObject.Model.BlogModel.PostModel", b =>
                {
                    b.Navigation("comments");

                    b.Navigation("post_Tags");
                });

            modelBuilder.Entity("BussinessObject.Model.BlogModel.TagModel", b =>
                {
                    b.Navigation("post_Tags");
                });
#pragma warning restore 612, 618
        }
    }
}
