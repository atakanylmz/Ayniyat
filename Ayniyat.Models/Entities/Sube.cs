using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ayniyat.Models.Entities
{
    public class Sube
    {
        public Sube()
        {
            Kullanicilar = new List<Kullanici>();
            Zimmetler = new List<Zimmet>();
        }
        public int Id { get; set; }
        public required string Ad { get; set; }

        public int DaireId { get; set; }
        public  Daire? Daire { get; set; }
        public ICollection<Kullanici> Kullanicilar { get; set; }
        public ICollection<Zimmet> Zimmetler { get; set; }
    }
}
