using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RefactoryExam1.Models
{
    public class ProductsContext : DbContext
    {
        public ProductsContext(DbContextOptions<ProductsContext> options) : base(options)
        {
        }

        public DbSet<Products> products { get; set; }
    }

    public class Products
    {
        public int id { get; set; }
        public string name { get; set; }
        public long price { get; set; }
        public long created_at { get; set; }
        public long updated_at { get; set; }
    }
}
