using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RefactoryExam1.Models
{
    public class DriversContext : DbContext
    {
        public DriversContext(DbContextOptions<DriversContext> options) : base(options)
        {
        }

        public DbSet<Drivers> Drivers { get; set; }
    }

    public class Drivers
    {
        public int id { get; set; }
        public string full_name { get; set; }
        public string phone_number { get; set; }
        public long created_at { get; set; }
        public long updated_at { get; set; }
    }
}
