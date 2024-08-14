using Ayniyat.Dal.Abstract;
using Ayniyat.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ayniyat.Dal.Concrete
{
    public class RolDal : GenericDal<Rol>, IRolDal
    {
        public RolDal(DefaultDbContext context) : base(context)
        {
        }
    }
}
