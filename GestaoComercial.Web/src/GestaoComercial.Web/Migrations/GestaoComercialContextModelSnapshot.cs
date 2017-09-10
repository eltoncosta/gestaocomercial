using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using GestaoComercial.Web.Data;

namespace GestaoComercial.Web.Migrations
{
    [DbContext(typeof(GestaoComercialContext))]
    partial class GestaoComercialContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GestaoComercial.Web.Models.Cliente", b =>
                {
                    b.Property<int>("ClienteID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DataCadastro");

                    b.Property<string>("Nome");

                    b.HasKey("ClienteID");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("GestaoComercial.Web.Models.Compra", b =>
                {
                    b.Property<int>("CompraID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ContaID");

                    b.Property<bool>("Status");

                    b.Property<double>("Valor");

                    b.HasKey("CompraID");

                    b.HasIndex("ContaID");

                    b.ToTable("Compra");
                });

            modelBuilder.Entity("GestaoComercial.Web.Models.Conta", b =>
                {
                    b.Property<int>("ContaID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ClienteID");

                    b.Property<bool>("Status");

                    b.HasKey("ContaID");

                    b.HasIndex("ClienteID");

                    b.ToTable("Conta");
                });

            modelBuilder.Entity("GestaoComercial.Web.Models.Compra", b =>
                {
                    b.HasOne("GestaoComercial.Web.Models.Conta", "Conta")
                        .WithMany("Compras")
                        .HasForeignKey("ContaID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GestaoComercial.Web.Models.Conta", b =>
                {
                    b.HasOne("GestaoComercial.Web.Models.Cliente", "Cliente")
                        .WithMany("Contas")
                        .HasForeignKey("ClienteID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
