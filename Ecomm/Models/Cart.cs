using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ecomm.Models
{
    [Table("Cart")]
    public class Cart
    {
        [Key]
        public int CId { get; set; }
        public int PId { get; set; }
        public int UId { get; set; }
    }
}
