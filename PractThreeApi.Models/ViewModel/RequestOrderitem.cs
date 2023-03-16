using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PractThreeApi.Models.ViewModel
{
    public class RequestOrderitem
    {
        [Required]
        public string username { get; set; }
        //[Required]
        //public int productId { get; set; }
        //[Required]
        //public int Quantity { get; set; }
        //[Required]
        public string PhoneNo { get; set; }
        public string email { get; set; }
        public IList<OrderVM> orderVMs { get; set; }
    }
}
