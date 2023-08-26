using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PerkyRabbit.Models.Entities
{
    public class Expense
    {
        [Key]
        public int Id { set; get; }

        [Required]
        public DateTime FirstDate { set; get; }

        [Required]
        public DateTime EndDate { set; get; }

        [Required]
        public double Expenditure { set; get; }

        [ForeignKey("Catagory")]
        public int Catagory_Id { set; get; }
        public virtual Catagory Catagory { get; set; }
    }
}