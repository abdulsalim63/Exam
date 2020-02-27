using System;
using Microsoft.EntityFrameworkCore;

namespace RefactoryExam1.Models
{
    public class OrderItemsContext : DbContext
    {
        public OrderItemsContext(DbContextOptions<OrderItemsContext> options) : base(options)
        {
        }

        public DbSet<OrderItems> OrderItems { get; set; }
    }

    public class OrderItems
    {
        public int id { get; set; }
        public int order_id { get; set; }
        public int product_id { get; set; }
        public int quantity { get; set; }
    }
}
