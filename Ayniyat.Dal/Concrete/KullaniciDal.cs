using Ayniyat.Dal.Abstract;
using Ayniyat.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ayniyat.Dal.Concrete
{
    public class KullaniciDal : GenericDal<Kullanici>, IKullaniciDal
    {
        public KullaniciDal(DefaultDbContext context) : base(context)
        {
        }

        public async Task<Kullanici?> Getir(string kullaniciAdi)
        {
            var eposta = kullaniciAdi + "@kgm.gov.tr";
            return await _context.Kullanicilar.FirstOrDefaultAsync(x => x.Eposta == eposta);
        }
    }
}
