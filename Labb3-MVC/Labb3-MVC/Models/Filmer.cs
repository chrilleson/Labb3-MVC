using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Labb3MVC.Models
{
    public class Filmer
    {
        public int Id { get; set; }

        public string Namn { get; set; }

        [Range(0,50,ErrorMessage ="Du kan inte boka så många biljetter")]
        public int Biljetter { get; set; }

        [DisplayName("Time")]
        public string ShowTime { get; set; }

        [DisplayName("Salong")]
        public Salong Salong { get; set; }

        [NotMapped]
        [DisplayName("Biljetter 1-12")]
        [Range(1,12,ErrorMessage ="Du kan max boka 12 biljetter")]
        public int BiljettValidator { get; set; }
    }
}
