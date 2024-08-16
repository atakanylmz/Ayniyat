using Ayniyat.Dal.Abstract;
using Ayniyat.Models.Dtos;
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
            return await _context.Kullanicilar.Include(x=>x.Rol).FirstOrDefaultAsync(x => x.Eposta == eposta);
        }

        public async Task<List<Kullanici>> ListeGetir(KullaniciAraKriterDto kriterDto)
        {
            throw new NotImplementedException();
        }
    }
}
