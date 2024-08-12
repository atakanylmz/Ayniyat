using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ayniyat.Models.Entities
{
    public class Kullanici
    {
        public Kullanici()
        {
            Zimmetler = new List<Zimmet>();
        }
        public int Id { get; set; }

        public required string Ad { get; set; }
        public required string  Soyad { get; set; }
        public string? Eposta { get; set; }
        public string? Unvan { get; set; }
        public bool Aktifmi { get; set; }

        public int RolId { get; set; }
        public Rol? Rol { get; set; }

        public int SubeId { get; set; }
        public Sube? Sube { get; set; }
        public ICollection<Zimmet> Zimmetler { get; set; }
    }
}
