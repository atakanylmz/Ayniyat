﻿using Ayniyat.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ayniyat.Dal.Abstract
{
    public  interface IZimmetLogDal:IGenericDal<ZimmetLog>
    {
        Task<List<ZimmetLog>> ZimmetLoglariGetir(int zimmetId);

    }
}
