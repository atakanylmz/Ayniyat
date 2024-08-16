using Ayniyat.Models.Dtos;
using Ayniyat.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ayniyat.Dal.Abstract
{
    public interface IKullaniciDal:IGenericDal<Kullanici>
    {
        Task<Kullanici?> Getir(string kullaniciAdi);

        Task<List<Kullanici>> ListeGetir(KullaniciAraKriterDto kriterDto);
    }

}
