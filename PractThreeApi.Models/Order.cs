using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PractThreeApi.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Note { get; set; }
        public int? DisountAmount { get; set; }
        public StatusType StatusType { get; set; }
        public double TotalAmount { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerContactNo { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

    }

    public enum StatusType
    {
        Open = 1,
        Draft = 2,
        Shipped = 3,
        Paid = 4
    }
}
