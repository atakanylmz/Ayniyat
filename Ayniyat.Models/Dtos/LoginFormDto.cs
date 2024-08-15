using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ayniyat.Models.Dtos
{
    public class LoginFormDto
    {
        public required string KullaniciAdi { get; set; }
        public required string Parola{ get; set; }

    }
}
