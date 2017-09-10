using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestaoComercial.Web.Models
{
    public class Cliente
    {

        public Cliente()
        {

        }

        public Cliente(int ClienteID, string Nome, DateTime DataCadastro)
        {
            this.ClienteID = ClienteID;
            this.Nome = Nome;
            this.DataCadastro = DataCadastro;
        }

        [Display(Name = "Código")]
        public int ClienteID { get; set; }
        public string Nome { get; set; }

        [Display(Name = "Data de Cadastro")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataCadastro { get; set; }

        public ICollection<Conta> Contas { get; set; }

    }
}
