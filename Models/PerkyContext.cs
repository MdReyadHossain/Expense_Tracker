using PerkyRabbit.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PerkyRabbit.Models
{
    public class PerkyContext : DbContext
    {
        public DbSet<Catagory> Catagories { get; set; }
        public DbSet<Expense> Expenses { get; set; }
    }
}