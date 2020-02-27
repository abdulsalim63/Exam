using System;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace RefactoryExam1.Models
{
    public class OrdersContext : DbContext
    {
        public OrdersContext(DbContextOptions<OrdersContext> options) : base(options)
        {
        }

        public DbSet<Orders> Orders { get; set; }
    }

    public class Orders
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public string Status { get; set; }
        public int driver_id { get; set; }
        public long created_at { get; set; }
        public long updated_at { get; set; }
    }
}
