using Ayniyat.Dal.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ayniyat.Dal.Concrete
{
    public abstract class GenericDal<T> : IGenericDal<T> where T : class
    {
        protected DefaultDbContext _context;
        private DbSet<T> _dbSet;

        public GenericDal(DefaultDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async Task<T?> Ekle(T entity)
        {
            try
            {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
            }
            catch (Exception e)
            {

                throw;
            }
         
        }

        public async Task<T?> Getir(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task Guncelle(T entity)
        {
           var guncellenenEntity=_context.Entry(entity);
            guncellenenEntity.State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task ListeEkle(List<T> entities)
        {
            _dbSet.AddRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task ListeGuncelle(List<T> entities)
        {
            _dbSet.UpdateRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task Sil(int id)
        {
            var mevcutEntity=await Getir(id);
            if (mevcutEntity == null)
            {
                throw new InvalidOperationException("Kayıt bulunamadı");
            }
            _dbSet.Remove(mevcutEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<T>> TumunuGetir()
        {
            return await _dbSet.ToListAsync();
        }
    }
}
