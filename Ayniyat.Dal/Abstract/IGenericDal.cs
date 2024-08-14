using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ayniyat.Dal.Abstract
{
    public interface IGenericDal<T> where T : class
    {
        Task<T?> Getir(int id);
        Task<List<T>> TumunuGetir();
        Task Sil(int id);
        Task Guncelle(T entity);
        Task<T?> Ekle(T entity);
        Task ListeEkle(List<T> entities);
        Task ListeGuncelle(List<T> entities);
    }
}
