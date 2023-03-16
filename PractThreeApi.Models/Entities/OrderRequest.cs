using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PractThreeApi.Models.Entities
{
    public class OrderRequest
    {
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerContactNo { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
    }
}
