﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using eshopBackend.DAL;

#nullable disable

namespace eshopBackend.DAL.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("eshopBackend.DAL.Entities.CartEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("DeliveryAddress")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("DeliveryType")
                        .HasColumnType("int");

                    b.Property<bool>("Finalized")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("LastEdit")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("PaymentDetails")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("PaymentType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("eshopBackend.DAL.Entities.CategoryEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = new Guid("ae679a8f-b546-6bed-f4ea-6d6db5af4ce2"),
                            Description = "Corporis adipisci minus dolor.",
                            ImageUrl = "data:image/svg+xml;charset=UTF-8,%3Csvg%20xmlns%3D%22http%3A%2F%2Fwww.w3.org%2F2000%2Fsvg%22%20version%3D%221.1%22%20baseProfile%3D%22full%22%20width%3D%22200%22%20height%3D%22100%22%3E%3Crect%20width%3D%22100%25%22%20height%3D%22100%25%22%20fill%3D%22grey%22%2F%3E%3Ctext%20x%3D%22100%22%20y%3D%2250%22%20font-size%3D%2220%22%20alignment-baseline%3D%22middle%22%20text-anchor%3D%22middle%22%20fill%3D%22white%22%3E200x100%3C%2Ftext%3E%3C%2Fsvg%3E",
                            Name = "SUV"
                        },
                        new
                        {
                            Id = new Guid("f43713a2-c0e5-95ac-5f7e-7756c4011f02"),
                            Description = "Et deserunt eum rerum rem eligendi dolores rem.",
                            ImageUrl = "data:image/svg+xml;charset=UTF-8,%3Csvg%20xmlns%3D%22http%3A%2F%2Fwww.w3.org%2F2000%2Fsvg%22%20version%3D%221.1%22%20baseProfile%3D%22full%22%20width%3D%22200%22%20height%3D%22100%22%3E%3Crect%20width%3D%22100%25%22%20height%3D%22100%25%22%20fill%3D%22grey%22%2F%3E%3Ctext%20x%3D%22100%22%20y%3D%2250%22%20font-size%3D%2220%22%20alignment-baseline%3D%22middle%22%20text-anchor%3D%22middle%22%20fill%3D%22white%22%3E200x100%3C%2Ftext%3E%3C%2Fsvg%3E",
                            Name = "Convertible"
                        },
                        new
                        {
                            Id = new Guid("3cf83c5f-f47b-a569-8972-feddf673c2fa"),
                            Description = "Vitae dolore quia maxime.",
                            ImageUrl = "data:image/svg+xml;charset=UTF-8,%3Csvg%20xmlns%3D%22http%3A%2F%2Fwww.w3.org%2F2000%2Fsvg%22%20version%3D%221.1%22%20baseProfile%3D%22full%22%20width%3D%22200%22%20height%3D%22100%22%3E%3Crect%20width%3D%22100%25%22%20height%3D%22100%25%22%20fill%3D%22grey%22%2F%3E%3Ctext%20x%3D%22100%22%20y%3D%2250%22%20font-size%3D%2220%22%20alignment-baseline%3D%22middle%22%20text-anchor%3D%22middle%22%20fill%3D%22white%22%3E200x100%3C%2Ftext%3E%3C%2Fsvg%3E",
                            Name = "Cargo Van"
                        },
                        new
                        {
                            Id = new Guid("b5753391-d6a1-fbd9-6a91-9c2581e90017"),
                            Description = "Eius saepe voluptas dolor et excepturi accusantium dolor officia eum.",
                            ImageUrl = "data:image/svg+xml;charset=UTF-8,%3Csvg%20xmlns%3D%22http%3A%2F%2Fwww.w3.org%2F2000%2Fsvg%22%20version%3D%221.1%22%20baseProfile%3D%22full%22%20width%3D%22200%22%20height%3D%22100%22%3E%3Crect%20width%3D%22100%25%22%20height%3D%22100%25%22%20fill%3D%22grey%22%2F%3E%3Ctext%20x%3D%22100%22%20y%3D%2250%22%20font-size%3D%2220%22%20alignment-baseline%3D%22middle%22%20text-anchor%3D%22middle%22%20fill%3D%22white%22%3E200x100%3C%2Ftext%3E%3C%2Fsvg%3E",
                            Name = "Coupe"
                        },
                        new
                        {
                            Id = new Guid("f7965c9d-f3a4-5e3f-4fe3-a73dd294c27c"),
                            Description = "Molestiae eum qui nisi ducimus.",
                            ImageUrl = "data:image/svg+xml;charset=UTF-8,%3Csvg%20xmlns%3D%22http%3A%2F%2Fwww.w3.org%2F2000%2Fsvg%22%20version%3D%221.1%22%20baseProfile%3D%22full%22%20width%3D%22200%22%20height%3D%22100%22%3E%3Crect%20width%3D%22100%25%22%20height%3D%22100%25%22%20fill%3D%22grey%22%2F%3E%3Ctext%20x%3D%22100%22%20y%3D%2250%22%20font-size%3D%2220%22%20alignment-baseline%3D%22middle%22%20text-anchor%3D%22middle%22%20fill%3D%22white%22%3E200x100%3C%2Ftext%3E%3C%2Fsvg%3E",
                            Name = "Passenger Van"
                        });
                });

            modelBuilder.Entity("eshopBackend.DAL.Entities.ManufacturerEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LogoUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Origin")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Manufacturers");

                    b.HasData(
                        new
                        {
                            Id = new Guid("e0bd03e2-6c6b-3ff3-5d46-68730563367d"),
                            Description = "Suscipit molestias optio.",
                            LogoUrl = "data:image/svg+xml;charset=UTF-8,%3Csvg%20xmlns%3D%22http%3A%2F%2Fwww.w3.org%2F2000%2Fsvg%22%20version%3D%221.1%22%20baseProfile%3D%22full%22%20width%3D%22200%22%20height%3D%22100%22%3E%3Crect%20width%3D%22100%25%22%20height%3D%22100%25%22%20fill%3D%22grey%22%2F%3E%3Ctext%20x%3D%22100%22%20y%3D%2250%22%20font-size%3D%2220%22%20alignment-baseline%3D%22middle%22%20text-anchor%3D%22middle%22%20fill%3D%22white%22%3E200x100%3C%2Ftext%3E%3C%2Fsvg%3E",
                            Name = "Polestar",
                            Origin = "Madagascar"
                        },
                        new
                        {
                            Id = new Guid("cebefc35-26df-b6c4-394c-876584e412fa"),
                            Description = "Qui porro sequi.",
                            LogoUrl = "data:image/svg+xml;charset=UTF-8,%3Csvg%20xmlns%3D%22http%3A%2F%2Fwww.w3.org%2F2000%2Fsvg%22%20version%3D%221.1%22%20baseProfile%3D%22full%22%20width%3D%22200%22%20height%3D%22100%22%3E%3Crect%20width%3D%22100%25%22%20height%3D%22100%25%22%20fill%3D%22grey%22%2F%3E%3Ctext%20x%3D%22100%22%20y%3D%2250%22%20font-size%3D%2220%22%20alignment-baseline%3D%22middle%22%20text-anchor%3D%22middle%22%20fill%3D%22white%22%3E200x100%3C%2Ftext%3E%3C%2Fsvg%3E",
                            Name = "Fiat",
                            Origin = "Ghana"
                        },
                        new
                        {
                            Id = new Guid("79ac3464-4a9d-28c5-3e48-ec30a74fdc6b"),
                            Description = "Aut quidem eaque ut aut.",
                            LogoUrl = "data:image/svg+xml;charset=UTF-8,%3Csvg%20xmlns%3D%22http%3A%2F%2Fwww.w3.org%2F2000%2Fsvg%22%20version%3D%221.1%22%20baseProfile%3D%22full%22%20width%3D%22200%22%20height%3D%22100%22%3E%3Crect%20width%3D%22100%25%22%20height%3D%22100%25%22%20fill%3D%22grey%22%2F%3E%3Ctext%20x%3D%22100%22%20y%3D%2250%22%20font-size%3D%2220%22%20alignment-baseline%3D%22middle%22%20text-anchor%3D%22middle%22%20fill%3D%22white%22%3E200x100%3C%2Ftext%3E%3C%2Fsvg%3E",
                            Name = "Fiat",
                            Origin = "Heard Island and McDonald Islands"
                        },
                        new
                        {
                            Id = new Guid("defa5417-8f86-bbb9-3261-35d66720b29a"),
                            Description = "Ea sunt itaque possimus eligendi vitae magnam asperiores dolor.",
                            LogoUrl = "data:image/svg+xml;charset=UTF-8,%3Csvg%20xmlns%3D%22http%3A%2F%2Fwww.w3.org%2F2000%2Fsvg%22%20version%3D%221.1%22%20baseProfile%3D%22full%22%20width%3D%22200%22%20height%3D%22100%22%3E%3Crect%20width%3D%22100%25%22%20height%3D%22100%25%22%20fill%3D%22grey%22%2F%3E%3Ctext%20x%3D%22100%22%20y%3D%2250%22%20font-size%3D%2220%22%20alignment-baseline%3D%22middle%22%20text-anchor%3D%22middle%22%20fill%3D%22white%22%3E200x100%3C%2Ftext%3E%3C%2Fsvg%3E",
                            Name = "Bugatti",
                            Origin = "Moldova"
                        },
                        new
                        {
                            Id = new Guid("7a31119b-2fc3-a6bb-8ff0-c31b3aefbb32"),
                            Description = "Nulla voluptas incidunt fuga quidem quia recusandae.",
                            LogoUrl = "data:image/svg+xml;charset=UTF-8,%3Csvg%20xmlns%3D%22http%3A%2F%2Fwww.w3.org%2F2000%2Fsvg%22%20version%3D%221.1%22%20baseProfile%3D%22full%22%20width%3D%22200%22%20height%3D%22100%22%3E%3Crect%20width%3D%22100%25%22%20height%3D%22100%25%22%20fill%3D%22grey%22%2F%3E%3Ctext%20x%3D%22100%22%20y%3D%2250%22%20font-size%3D%2220%22%20alignment-baseline%3D%22middle%22%20text-anchor%3D%22middle%22%20fill%3D%22white%22%3E200x100%3C%2Ftext%3E%3C%2Fsvg%3E",
                            Name = "Toyota",
                            Origin = "Paraguay"
                        });
                });

            modelBuilder.Entity("eshopBackend.DAL.Entities.ProductEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("CartEntityId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("CategoryId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid?>("ManufacturerId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("Price")
                        .HasColumnType("double");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.Property<double>("Weight")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("CartEntityId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ManufacturerId");

                    b.ToTable("Products");

                    b.HasDiscriminator<string>("Discriminator").HasValue("ProductEntity");

                    b.UseTphMappingStrategy();

                    b.HasData(
                        new
                        {
                            Id = new Guid("0018c1ac-4184-7a6d-dca3-41fbc996bec3"),
                            CategoryId = new Guid("ae679a8f-b546-6bed-f4ea-6d6db5af4ce2"),
                            Description = "Sed non qui fugit fuga.",
                            ImageUrl = "data:image/svg+xml;charset=UTF-8,%3Csvg%20xmlns%3D%22http%3A%2F%2Fwww.w3.org%2F2000%2Fsvg%22%20version%3D%221.1%22%20baseProfile%3D%22full%22%20width%3D%22200%22%20height%3D%22100%22%3E%3Crect%20width%3D%22100%25%22%20height%3D%22100%25%22%20fill%3D%22grey%22%2F%3E%3Ctext%20x%3D%22100%22%20y%3D%2250%22%20font-size%3D%2220%22%20alignment-baseline%3D%22middle%22%20text-anchor%3D%22middle%22%20fill%3D%22white%22%3E200x100%3C%2Ftext%3E%3C%2Fsvg%3E",
                            ManufacturerId = new Guid("e0bd03e2-6c6b-3ff3-5d46-68730563367d"),
                            Name = "Aston Martin",
                            Price = 181.0,
                            Stock = 1,
                            Weight = 10.0
                        },
                        new
                        {
                            Id = new Guid("33380030-dcdf-7c9f-8731-6e6469266552"),
                            CategoryId = new Guid("f43713a2-c0e5-95ac-5f7e-7756c4011f02"),
                            Description = "Voluptates qui optio sed cupiditate atque ut soluta tenetur.",
                            ImageUrl = "data:image/svg+xml;charset=UTF-8,%3Csvg%20xmlns%3D%22http%3A%2F%2Fwww.w3.org%2F2000%2Fsvg%22%20version%3D%221.1%22%20baseProfile%3D%22full%22%20width%3D%22200%22%20height%3D%22100%22%3E%3Crect%20width%3D%22100%25%22%20height%3D%22100%25%22%20fill%3D%22grey%22%2F%3E%3Ctext%20x%3D%22100%22%20y%3D%2250%22%20font-size%3D%2220%22%20alignment-baseline%3D%22middle%22%20text-anchor%3D%22middle%22%20fill%3D%22white%22%3E200x100%3C%2Ftext%3E%3C%2Fsvg%3E",
                            ManufacturerId = new Guid("cebefc35-26df-b6c4-394c-876584e412fa"),
                            Name = "Cadillac",
                            Price = 252.0,
                            Stock = 4,
                            Weight = 1.0
                        },
                        new
                        {
                            Id = new Guid("ca31899e-661e-cd75-0fb6-072c9a7de7f2"),
                            CategoryId = new Guid("3cf83c5f-f47b-a569-8972-feddf673c2fa"),
                            Description = "Id laborum sequi voluptatem officiis a nesciunt qui.",
                            ImageUrl = "data:image/svg+xml;charset=UTF-8,%3Csvg%20xmlns%3D%22http%3A%2F%2Fwww.w3.org%2F2000%2Fsvg%22%20version%3D%221.1%22%20baseProfile%3D%22full%22%20width%3D%22200%22%20height%3D%22100%22%3E%3Crect%20width%3D%22100%25%22%20height%3D%22100%25%22%20fill%3D%22grey%22%2F%3E%3Ctext%20x%3D%22100%22%20y%3D%2250%22%20font-size%3D%2220%22%20alignment-baseline%3D%22middle%22%20text-anchor%3D%22middle%22%20fill%3D%22white%22%3E200x100%3C%2Ftext%3E%3C%2Fsvg%3E",
                            ManufacturerId = new Guid("79ac3464-4a9d-28c5-3e48-ec30a74fdc6b"),
                            Name = "Chevrolet",
                            Price = 423.0,
                            Stock = 5,
                            Weight = 0.0
                        },
                        new
                        {
                            Id = new Guid("ec03e807-a436-7b6d-bf79-763ada38e138"),
                            CategoryId = new Guid("b5753391-d6a1-fbd9-6a91-9c2581e90017"),
                            Description = "Distinctio est minima repellendus earum adipisci sequi et.",
                            ImageUrl = "data:image/svg+xml;charset=UTF-8,%3Csvg%20xmlns%3D%22http%3A%2F%2Fwww.w3.org%2F2000%2Fsvg%22%20version%3D%221.1%22%20baseProfile%3D%22full%22%20width%3D%22200%22%20height%3D%22100%22%3E%3Crect%20width%3D%22100%25%22%20height%3D%22100%25%22%20fill%3D%22grey%22%2F%3E%3Ctext%20x%3D%22100%22%20y%3D%2250%22%20font-size%3D%2220%22%20alignment-baseline%3D%22middle%22%20text-anchor%3D%22middle%22%20fill%3D%22white%22%3E200x100%3C%2Ftext%3E%3C%2Fsvg%3E",
                            ManufacturerId = new Guid("defa5417-8f86-bbb9-3261-35d66720b29a"),
                            Name = "Nissan",
                            Price = 95.0,
                            Stock = 1,
                            Weight = 57.0
                        },
                        new
                        {
                            Id = new Guid("21102296-5e27-42a4-1c80-8564d80502a8"),
                            CategoryId = new Guid("f7965c9d-f3a4-5e3f-4fe3-a73dd294c27c"),
                            Description = "Sit quod possimus quae aut sed adipisci et qui non.",
                            ImageUrl = "data:image/svg+xml;charset=UTF-8,%3Csvg%20xmlns%3D%22http%3A%2F%2Fwww.w3.org%2F2000%2Fsvg%22%20version%3D%221.1%22%20baseProfile%3D%22full%22%20width%3D%22200%22%20height%3D%22100%22%3E%3Crect%20width%3D%22100%25%22%20height%3D%22100%25%22%20fill%3D%22grey%22%2F%3E%3Ctext%20x%3D%22100%22%20y%3D%2250%22%20font-size%3D%2220%22%20alignment-baseline%3D%22middle%22%20text-anchor%3D%22middle%22%20fill%3D%22white%22%3E200x100%3C%2Ftext%3E%3C%2Fsvg%3E",
                            ManufacturerId = new Guid("7a31119b-2fc3-a6bb-8ff0-c31b3aefbb32"),
                            Name = "Polestar",
                            Price = 236.0,
                            Stock = 2,
                            Weight = 58.0
                        });
                });

            modelBuilder.Entity("eshopBackend.DAL.Entities.ReviewEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid?>("ProductEntityId")
                        .HasColumnType("char(36)");

                    b.Property<double>("Stars")
                        .HasColumnType("double");

                    b.Property<string>("User")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ProductEntityId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("eshopBackend.DAL.Entities.ProductInCartEntity", b =>
                {
                    b.HasBaseType("eshopBackend.DAL.Entities.ProductEntity");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("ProductInCartEntity");
                });

            modelBuilder.Entity("eshopBackend.DAL.Entities.ProductEntity", b =>
                {
                    b.HasOne("eshopBackend.DAL.Entities.CartEntity", null)
                        .WithMany("Products")
                        .HasForeignKey("CartEntityId");

                    b.HasOne("eshopBackend.DAL.Entities.CategoryEntity", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.HasOne("eshopBackend.DAL.Entities.ManufacturerEntity", "Manufacturer")
                        .WithMany()
                        .HasForeignKey("ManufacturerId");

                    b.Navigation("Category");

                    b.Navigation("Manufacturer");
                });

            modelBuilder.Entity("eshopBackend.DAL.Entities.ReviewEntity", b =>
                {
                    b.HasOne("eshopBackend.DAL.Entities.ProductEntity", null)
                        .WithMany("Reviews")
                        .HasForeignKey("ProductEntityId");
                });

            modelBuilder.Entity("eshopBackend.DAL.Entities.CartEntity", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("eshopBackend.DAL.Entities.ProductEntity", b =>
                {
                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
