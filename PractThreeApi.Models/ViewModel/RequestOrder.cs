using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PractThreeApi.Models.ViewModel
{
    public class RequestOrder
    {
        [Required]
        public string Note { get; set; }
        [Required]
        public double DisountAmount { get; set; }
        [Required]
        public string StatusType { get; set; }

        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string CustomerEmail { get; set; }
        [Required]
        public string CustomerContactNo { get; set; }

        [Required]
        public int Quantity { get; set; }
        [Required]
        public double Price { get; set; }

        [Required]
        public string ProductName { get; set; }
    }
}
