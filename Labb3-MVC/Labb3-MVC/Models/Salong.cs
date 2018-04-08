using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labb3MVC.Models
{
    public class Salong
    {
        public int Id { get; set; }
        public int MaxKapacitet { get; set; }

        public IList<Filmer> Filmers { get; set; }
    }
}
