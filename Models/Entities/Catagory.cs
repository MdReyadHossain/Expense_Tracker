using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PerkyRabbit.Models.Entities
{
    public class Catagory
    {
        [Key]
        public int Id { set; get; }

        [Required]
        public string Name { set; get; }

        public bool Status { set; get; }

        public ICollection<Expense> Expenses { set; get; }
        public Catagory()
        {
            Expenses = new List<Expense>();
        }
    }
}