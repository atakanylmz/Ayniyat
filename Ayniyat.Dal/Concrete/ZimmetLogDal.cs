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
    public class ZimmetLogDal : GenericDal<ZimmetLog>, IZimmetLogDal
    {
        public ZimmetLogDal(DefaultDbContext context) : base(context)
        {
        }

        public async Task<List<ZimmetLog>> ZimmetLoglariGetir(int zimmetId)
        {
           return await _context.ZimmetLoglari.Where(x=>x.ZimmetId == zimmetId).ToListAsync();
        }
    }
}
