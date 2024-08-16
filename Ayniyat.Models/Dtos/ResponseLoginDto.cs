using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ayniyat.Models.Dtos
{
    public class ResponseLoginDto
    {
        public int KullaniciId { get; set; }
        public required string Eposta { get; set; }
        public required string Token { get; set; }
        public int RolId { get; set; }

        public required string Ad { get; set; }
        public required string Soyad { get; set; }
    }
}
