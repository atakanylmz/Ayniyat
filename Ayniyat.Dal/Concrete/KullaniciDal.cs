using Ayniyat.Dal.Abstract;
using Ayniyat.Models.Dtos;
using Ayniyat.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ayniyat.Models.Utilities.Enums;

namespace Ayniyat.Dal.Concrete
{
    public class KullaniciDal : GenericDal<Kullanici>, IKullaniciDal
    {
        public KullaniciDal(DefaultDbContext context) : base(context)
        {
        }

        public async Task<Kullanici?> Getir(string kullaniciAdi)
        {
            try
            {
                var eposta = kullaniciAdi + "@kgm.gov.tr";
                return await _context.Kullanicilar.Include(x => x.Rol).FirstOrDefaultAsync(x => x.Eposta == eposta);
            }
            catch (Exception)
            {
                return null;
            }
           
        }

        public async Task<List<Kullanici>> ListeGetir(KullaniciAraKriterDto kriterDto)
        {
            List<Kullanici> kullaniciListesi;
            if (!kriterDto.SubeId.HasValue)
            {
                 kullaniciListesi =string.IsNullOrWhiteSpace(kriterDto.AraText)?
                    await _context.Kullanicilar.Include(x => x.Zimmetler).Include(x=>x.Sube).ToListAsync():
                    await _context.Kullanicilar.Include(x => x.Zimmetler).Include(x => x.Sube).Where(x => x.Ad.Contains(kriterDto.AraText)|| x.Soyad.Contains(kriterDto.AraText)).ToListAsync();
            }
            else
            {
                kullaniciListesi = string.IsNullOrWhiteSpace(kriterDto.AraText) ?
                   await _context.Kullanicilar.Include(x => x.Zimmetler).Include(x => x.Sube).Where(x=>x.SubeId==kriterDto.SubeId.Value).ToListAsync() :
                   await _context.Kullanicilar.Include(x => x.Zimmetler).Include(x => x.Sube).Where(x =>x.SubeId==kriterDto.SubeId.Value && (x.Ad.Contains(kriterDto.AraText)|| x.Soyad.Contains(kriterDto.AraText))).ToListAsync();
            }
            return kullaniciListesi.Where(x=>x.Aktifmi==kriterDto.Aktifmi).ToList();
        }

        public async Task<Kullanici?> SubeyleGetir(int id)
        {
            return await _context.Kullanicilar.Include(x=>x.Sube).ThenInclude(x=>x.Daire).FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<Kullanici?> SubeMuduruGetir(int subeId)
        {
            return await _context.Kullanicilar.Include(x => x.Sube).ThenInclude(x => x.Daire).FirstOrDefaultAsync(x => x.SubeId==subeId&&x.RolId==(int)ERole.Mudur);
        }
    }
}
