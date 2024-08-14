using Ayniyat.Dal.Abstract;
using Ayniyat.Models.Entities;
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
    }
}
