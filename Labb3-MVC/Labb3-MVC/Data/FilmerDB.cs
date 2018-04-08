using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Labb3MVC.Models;

namespace Labb3MVC.Data
{
    public class FilmerDB
    {
        public static void Initialize(Labb3MVCContext context)
        {
            if (context.Filmer.Any())
            {
                return;
            }

            var Salonger = new Salong[]
            {
                new Salong {MaxKapacitet = 49},
                new Salong {MaxKapacitet=101}
            };
            foreach (Salong s in Salonger)
            {
                context.Salong.Add(s);
            }
            context.SaveChanges();

            var filmer = new Filmer[]
            {
                new Filmer {Namn="Star Wars Episode IV: A New Hope", ShowTime="16:00", Biljetter= Salonger[0].MaxKapacitet, Salong= context.Salong.FirstOrDefault(x => x.Id == 1)},
                new Filmer {Namn="The Lord Of The Rings: The Fellowship of the Ring", ShowTime="18:00", Biljetter= Salonger[0].MaxKapacitet, Salong= context.Salong.FirstOrDefault(x => x.Id == 1) },
                new Filmer {Namn="Star Wars Episode IV: A New Hope", ShowTime="16:00", Biljetter= Salonger[1].MaxKapacitet, Salong= context.Salong.FirstOrDefault(x => x.Id == 2)},
                new Filmer {Namn="The Lord Of The Rings: The Fellowship of the Ring", ShowTime="18:00", Biljetter= Salonger[1].MaxKapacitet, Salong= context.Salong.FirstOrDefault(x => x.Id == 2)},
                new Filmer {Namn="Star Wars Episode V: The Empire Strikes Back", ShowTime="21:00", Biljetter= Salonger[0].MaxKapacitet, Salong= context.Salong.FirstOrDefault(x => x.Id == 1)},
                new Filmer {Namn="The Lord Of The Rings: The Two Towers", ShowTime="23:30", Biljetter= Salonger[0].MaxKapacitet, Salong= context.Salong.FirstOrDefault(x => x.Id == 1)},
                new Filmer {Namn="Star Wars Episode V: The Empire Strikes Back", ShowTime="21:00", Biljetter= Salonger[1].MaxKapacitet, Salong= context.Salong.FirstOrDefault(x => x.Id == 2)},
                new Filmer {Namn="The Lord Of The Rings: The Two Towers", ShowTime="23:30", Biljetter= Salonger[1].MaxKapacitet, Salong= context.Salong.FirstOrDefault(x => x.Id == 2)}
            };
            foreach(Filmer f in filmer)
            {
                context.Filmer.Add(f);
            }
            context.SaveChanges();
        }
    }
}
