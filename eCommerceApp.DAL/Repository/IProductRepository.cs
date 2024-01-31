﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceApp.DAL.Models;

namespace eCommerceApp.DAL.Repository
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        public Task<double> GetProductprice(Guid id);

    }
}
