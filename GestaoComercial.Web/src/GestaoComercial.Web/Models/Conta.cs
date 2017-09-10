using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoComercial.Web.Models
{
    public class Conta
    {
        public Conta() { }

        [Display(Name = "Código")]
        public int ContaID { get; set; }
        public int ClienteID { get; set; }
        public bool Status { get; set; }

        public Cliente Cliente { get; set; }
        public ICollection<Compra> Compras { get; set; }
    }
}
