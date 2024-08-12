using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ayniyat.Models.Entities
{
    public class Daire
    {
        public Daire()
        {
            Subeler=new List<Sube>();
        }
        public int Id { get; set; }
        public required string Ad { get; set; }

        public ICollection<Sube> Subeler { get; set; }
    }
}
