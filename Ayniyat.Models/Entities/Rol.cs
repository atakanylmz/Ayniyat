using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ayniyat.Models.Entities
{
    public class Rol
    {
        public Rol()
        {
            Kullanicilar = new List<Kullanici>();
        }
        public int Id { get; set; }
        public required string Ad { get; set; }

        public string? Aciklama { get; set; }
        public ICollection<Kullanici> Kullanicilar { get; set; }
    }
}
