using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Helpers;

namespace Model
{
    public class FitOut
    {
        [Key]
        public int Id { get; set; }

        public double Cost { get; set; }

        public string FitOutBy { get; set; }

        public string Area { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? StartDate { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? EndDate { get; set; }

        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        //[NotMapped]
        //public string WarrantyStatus
        //{
        //    get;
        //}
    }
}
