using Ayniyat.Dal.Abstract;
using Ayniyat.Models.Entities;
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
    }
}
