using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ayniyat.Models.Dtos
{
    public class ZimmetAraKriterDto
    {
        public int KullaniciId { get; set; }
        public int SubeId { get; set; }

        public bool KaldirilanlariGoster { get; set; }

        private DateTime? tarih;

        public DateTime? Tarih
        {
            get {
                if (tarih.HasValue)
                {
                    return tarih.Value.Date.AddDays(1);
                }

                return tarih; }
            set { tarih = value; }
        }

    }
}
