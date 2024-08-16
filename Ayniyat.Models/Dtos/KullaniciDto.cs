using Ayniyat.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ayniyat.Models.Dtos
{
    public class KullaniciDto
    {
        public int Id { get; set; }

        public required string Ad { get; set; }
        public required string Soyad { get; set; }
        public string? Eposta { get; set; }
        public string? Unvan { get; set; }
        public bool Aktifmi { get; set; }
        public int SubeId { get; set; }
    }
}
