using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoComercial.Web.Models
{
    public class Compra
    {

        public Compra() { }

        [Display(Name = "Código")]
        public int CompraID { get; set; }
        public int ContaID { get; set; }

        [DataType(DataType.Currency)]
        public double Valor { get; set; }
        public bool Status { get; set; }
        public Conta Conta { get; set; }

    }
}
