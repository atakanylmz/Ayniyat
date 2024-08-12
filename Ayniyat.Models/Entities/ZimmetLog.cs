using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ayniyat.Models.Entities
{
    public class ZimmetLog
    {
        public int Id { get; set; }
        public int? StokNo { get; set; }
        public string? TasinirNo { get; set; }
        public required string MalzemeAd { get; set; }
        public string? EnvanterNo { get; set; }
        public string? Birim { get; set; }
        public int Miktar { get; set; }
        public string? SeriNo { get; set; }
        public string? Model { get; set; }

        public DateTime IslemTarihi { get; set; }

        public string? Aciklama { get; set; }

        public int ZimmetId { get; set; }
        public Zimmet? Zimmet { get; set; }
    }
}
