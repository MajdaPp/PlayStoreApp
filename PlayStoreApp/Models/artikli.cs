using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PlayStoreApp.Models
{
    public class artikli
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public int Cijena { get; set; }
        public string Proizvođač { get; set; }
        public string Dostupnost { get; set; }
    }
}
