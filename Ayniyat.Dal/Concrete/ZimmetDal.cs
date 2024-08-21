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
    public class ZimmetDal : GenericDal<Zimmet>, IZimmetDal
    {
        public ZimmetDal(DefaultDbContext context) : base(context)
        {
        }

        public async Task<List<Zimmet>> ZimmetListesiGetir(ZimmetAraKriterDto kriter)
        {
            List<Zimmet> list = new List<Zimmet>();

            if (kriter.KullaniciId == 0)//zimmet bazlı liste
            {
                if (kriter.SubeId == 0) //tüm şubeler için
                {
                    list = kriter.Tarih.HasValue ?
                        //tarih var
                        (await _context.Zimmetler.Where(x=>
                            x.KayitTarihi<kriter.Tarih&&
                            (!x.KaldirilmaTarihi.HasValue||x.KaldirilmaTarihi.Value>kriter.Tarih))
                        .ToListAsync())
                        :
                        //tarih yok
                        (await _context.Zimmetler.ToListAsync());//hiç bir kriter yok
                }
                else//belli belli bir şube için
                {
                    list = kriter.Tarih.HasValue ?
                        //tarih var
                       (await _context.Zimmetler.Where(x =>
                           x.SubeId==kriter.SubeId &&
                           x.KayitTarihi < kriter.Tarih && 
                           (!x.KaldirilmaTarihi.HasValue || x.KaldirilmaTarihi.Value > kriter.Tarih))
                       .ToListAsync())
                       :
                       //tarih yok
                       (await _context.Zimmetler.Where(x=> x.SubeId == kriter.SubeId).ToListAsync());

                }
            }
            else//kullanıcaya ait zimmet listesi
            {

                list = kriter.Tarih.HasValue ?
                   //tarih var
                   (await _context.Zimmetler.Where(x =>
                       x.KullaniciId == kriter.KullaniciId &&
                       x.KayitTarihi < kriter.Tarih &&
                       (!x.KaldirilmaTarihi.HasValue || x.KaldirilmaTarihi.Value > kriter.Tarih))
                   .ToListAsync())
                   :
                   //tarih yok
                   (await _context.Zimmetler.Where(x => x.KullaniciId == kriter.KullaniciId).ToListAsync());
            }


            return list;
        }
    }
}
