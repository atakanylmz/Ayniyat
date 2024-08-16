using Ayniyat.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ayniyat.Models.Dtos
{
    public class KullaniciListeOgeDto
    {
        public int Id { get; set; }

        public required string Ad { get; set; }
        public required string Soyad { get; set; }
        public string? Eposta { get; set; }

        public required string SubeAd { get; set; }
        public string? Unvan { get; set; }
        public required string Aktifmi { get; set; }

        public int AyniyatSayisi { get; set; }

    }
}
