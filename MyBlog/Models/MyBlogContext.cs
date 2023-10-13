﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MyBlog.Models;

public partial class MyBlogContext : DbContext
{
    public MyBlogContext(DbContextOptions<MyBlogContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Option> Option { get; set; }

    public virtual DbSet<Post> Post { get; set; }

    public virtual DbSet<User> User { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Option>(entity =>
        {
            entity.ToTable("option");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Value)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("value");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.ToTable("post");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Content)
                .IsRequired()
                .HasColumnName("content");
            entity.Property(e => e.FilteredContent)
                .IsRequired()
                .HasColumnName("filteredContent");
            entity.Property(e => e.OgDescription)
                .IsRequired()
                .HasColumnName("ogDescription");
            entity.Property(e => e.OgImage)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("ogImage");
            entity.Property(e => e.OgKeywords)
                .IsRequired()
                .HasColumnName("ogKeywords");
            entity.Property(e => e.OgTitle)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("ogTitle");
            entity.Property(e => e.ParentId).HasColumnName("parentId");
            entity.Property(e => e.Path)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("path");
            entity.Property(e => e.PublishDate).HasColumnName("publishDate");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("status");
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("title");
            entity.Property(e => e.UpdateDate).HasColumnName("updateDate");
            entity.Property(e => e.UserId).HasColumnName("userId");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("user");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.About)
                .IsRequired()
                .HasColumnName("about");
            entity.Property(e => e.CreateDate).HasColumnName("createDate");
            entity.Property(e => e.DisplayId)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("displayId");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Image)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("image");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("password");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.UpdateDate).HasColumnName("updateDate");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}