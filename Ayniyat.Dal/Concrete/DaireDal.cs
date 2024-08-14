using Ayniyat.Dal.Abstract;
using Ayniyat.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ayniyat.Dal.Concrete
{
    public class DaireDal : GenericDal<Daire>, IDaireDal
    {
        public DaireDal(DefaultDbContext context) : base(context)
        {
        }




    }
}
