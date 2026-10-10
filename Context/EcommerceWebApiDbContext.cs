using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce_Web_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Web_Api.Context
{
    public class EcommerceWebApiDbContext : DbContext
    {
        public EcommerceWebApiDbContext(DbContextOptions<EcommerceWebApiDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
    }
}