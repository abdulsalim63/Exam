using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RefactoryExam1.Models
{
    public class CustomersContext : DbContext
    {
        public CustomersContext(DbContextOptions<CustomersContext> options) : base(options)
        {
        }

        public DbSet<Customers> Customers { get; set; }
    }

    public class Customers
    {
        public int id { get; set; }
        public string full_name { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string phone_number { get; set; }
        public long created_at { get; set; }
        public long updated_at { get; set; }
    }
}
