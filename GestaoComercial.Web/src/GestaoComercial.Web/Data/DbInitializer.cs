using GestaoComercial.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoComercial.Web.Data
{
    public class DbInitializer
    {
        public static void Initialize(GestaoComercialContext context)
        {
            if (context.Cliente.Any())
            {
                return;   // DB has been seeded
            }

            //CLIENTES
            var clientes = new Cliente[] {
                new Cliente { Nome = "ELTON COSTA", DataCadastro = DateTime.Parse("2017-01-01") },
                new Cliente { Nome = "MATEUS COSTA", DataCadastro = DateTime.Parse("2017-01-01") },
                new Cliente { Nome = "LETICIA BATISTA", DataCadastro = DateTime.Parse("2017-01-01") }
            };

            foreach (Cliente s in clientes)
            {
                context.Cliente.Add(s);
            }
            context.SaveChanges();

            //CONTAS
            var contas = new Conta[] {
                new Conta { Status = true,  ClienteID = clientes.Single( c => c.Nome.Contains("ELTON")).ClienteID},
                new Conta { Status = true,  ClienteID = clientes.Single( c => c.Nome.Contains("LETICIA")).ClienteID}
            };

            foreach (Conta s in contas)
            {
                context.Conta.Add(s);
            }
            context.SaveChanges();

            //COMPRAS
            var compras = new Compra[] {
                new Compra { ContaID = contas.Single( c => c.Cliente.Nome.Contains("ELTON")).ContaID, Valor = 100.00 }
            };

            foreach (Compra s in compras)
            {
                context.Compra.Add(s);
            }
            context.SaveChanges();

        }
    }
}
