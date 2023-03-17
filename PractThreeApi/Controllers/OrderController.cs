using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PractThreeApi.DataAccess.Repository.IRepository;
using PractThreeApi.Models.ViewModel;
using PractThreeApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace PractThreeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Microsoft.AspNetCore.Identity.UserManager<IdentityUser> _userManager;

        public OrderController(IUnitOfWork unitOfWork, Microsoft.AspNetCore.Identity.UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("AddOrderNew")]
        public async Task<IActionResult> PostOrder(RequestOrderitem requestOrderitem)
        {
            var userdata = await _userManager.FindByNameAsync(requestOrderitem.username);
            //For Single Product
            // var Product = _unitOfWork.Product.GetFirstorDefault(x => x.ProductId == requestOrderitem.productId);
            //  var total = Product.Price * requestOrderitem.Quantity;
            var T = 0;
            if (userdata == null)
            {
                foreach (var x in requestOrderitem.orderVMs)
                {
                    var Product = _unitOfWork.Product.GetFirstorDefault(data => data.ProductId == x.PId);
                    var price = Product.Price * x.PQuentity;
                    T = T + price;
                }
                var obj2 = new Order()
                {
                    Note = "hello",
                    TotalAmount = T,
                    // StatusType = "Approved",
                    StatusType = Models.StatusType.Open,
                    CustomerName = requestOrderitem.username,
                    CustomerEmail = requestOrderitem.email,
                    CustomerContactNo = requestOrderitem.PhoneNo,
                    IsActive = true,
                };
                _unitOfWork.Order.Add(obj2);
                _unitOfWork.Save();
                foreach (var item in requestOrderitem.orderVMs)
                {
                    var Product = _unitOfWork.Product.GetFirstorDefault(data => data.ProductId == item.PId);
                    var obj1 = new OrderItems()
                    {
                        ProductId = item.PId,
                        OrderId = obj2.OrderId,
                        IsActive = true,
                        Price = Product.Price,
                        Quantity = item.PQuentity,
                    };
                    _unitOfWork.OrderItem.Add(obj1);
                    _unitOfWork.Save();
                }
                return Ok(new ResponseOrderItem() { Message = "New user And Order Added" });
            }
            // var T = 0;
            foreach (var x in requestOrderitem.orderVMs)
            {
                var Product = _unitOfWork.Product.GetFirstorDefault(data => data.ProductId == x.PId);
                var price = Product.Price * x.PQuentity;
                T = T + price;
            }
            var obj = new Order()
            {
                Note = "hello",
                TotalAmount = T,
                // StatusType = "Approved",
                StatusType = Models.StatusType.Open,
                CustomerName = requestOrderitem.username,
                CustomerEmail = requestOrderitem.email,
                CustomerContactNo = requestOrderitem.PhoneNo,
                IsActive = true,
            };
            _unitOfWork.Order.Add(obj);
            _unitOfWork.Save();
            foreach (var item in requestOrderitem.orderVMs)
            {
                var Product = _unitOfWork.Product.GetFirstorDefault(data => data.ProductId == item.PId);
                var obj1 = new OrderItems()
                {
                    ProductId = item.PId,
                    OrderId = obj.OrderId,
                    IsActive = true,
                    Price = Product.Price,
                    Quantity = item.PQuentity,
                };
                _unitOfWork.OrderItem.Add(obj1);
                _unitOfWork.Save();

            }
            return Ok(new ResponseOrderItem() { Message = "Add Item Successfully...." });
        }

        //[HttpPost]
        //[Route("AddOrder")]
        //public async Task<IActionResult> PostOrder(RequestOrderitem requestOrderitem)
        //{
        //    var userdata = await _userManager.FindByNameAsync(requestOrderitem.username);
        //    var totalamount = requestOrderitem.Price * requestOrderitem.Quantity;

        //        var check = _unitOfWork.Order.GetFirstorDefault(x => x.CustomerName == requestOrderitem.username);
        //        var orderdata = new Order();
        //        if (check == null)
        //        {             

        //            orderdata.CustomerName = userdata.UserName;
        //            orderdata.CustomerEmail = userdata.Email;
        //            orderdata.CustomerContactNo = "12345";
        //            orderdata.StatusType = StatusType.Open.ToString();
        //            orderdata.IsActive = true;
        //            orderdata.DisountAmount = 0;
        //            orderdata.Note = "";
        //            orderdata.TotalAmount = totalamount;


        //            _unitOfWork.Order.Add(orderdata);
        //            _unitOfWork.Save();

        //            var orderitemdata = new OrderItems()
        //            {
        //                Quantity = requestOrderitem.Quantity,
        //                OrderId = orderdata.OrderId,
        //                ProductId = requestOrderitem.productId,
        //                Price = requestOrderitem.Price,
        //                IsActive = true
        //            };

        //            _unitOfWork.OrderItem.Add(orderitemdata);
        //            _unitOfWork.Save();
        //        }
        //        else
        //        {
        //            var orderid = _unitOfWork.Order.GetFirstorDefault(x => x.CustomerName == requestOrderitem.username);
        //            var orderitemdata = new OrderItems()
        //            {
        //                Quantity = requestOrderitem.Quantity,
        //                OrderId = orderid.OrderId,
        //                ProductId = requestOrderitem.productId,
        //            };

        //            _unitOfWork.OrderItem.Add(orderitemdata);
        //            _unitOfWork.Save();
        //        }
        //    return Ok(new ResponseOrderItem() { Message = "Add Item Successfully...." });
        //}


        // Get Order Details By id and Show All orderitem And Product

        [Authorize]
        [HttpGet]
        [Route("GetOrderById")]
        public async Task<IActionResult> GetOrderbyId(int id)
        {
            try
            {
                var data = _unitOfWork.Order.GetFirstorDefault(x => x.OrderId == id);
                if (data != null)
                {
                    var idData = (from order in _unitOfWork.Order.GetAll()
                                  join
                                oi in _unitOfWork.OrderItem.GetAll() on
                                order.OrderId equals oi.OrderId
                                  where order.OrderId == id
                                  select new { oi }).ToList();

                    return Ok(idData);
                }
                else
                {
                    return Ok(new ResponseOrderItem() { Message = "Record Not Found...." });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        // update Order Status
        [Authorize]
        [HttpPut]
        [Route("updatestatusById")]
        public async Task<IActionResult> putstatusbyId(int id, Models.StatusType status)
        {

            var idData = _unitOfWork.Order.GetFirstorDefault(x => x.OrderId == id);
            if (idData.IsActive != false && idData.StatusType != Models.StatusType.Shipped /*StatusType.Shipped.ToString()*/)
            {
                // idData.StatusType = status.ToString();
                idData.StatusType = status;

                _unitOfWork.Order.Update(idData);
                _unitOfWork.Save();
            }

            return Ok(idData);
        }

        // update Order Active or inactive
        [Authorize]
        [HttpPut]
        [Route("updateOrderStatus")]
        public async Task<IActionResult> PutActive(int id)
        {

            var idData = _unitOfWork.Order.GetFirstorDefault(x => x.OrderId == id);
            if (idData != null)
            {
                if (idData.IsActive == false)
                {
                    idData.IsActive = true;
                }
                else
                {
                    idData.IsActive = false;
                }
                _unitOfWork.Order.Update(idData);
                _unitOfWork.Save();
                return Ok(idData);
            }
            return Ok(new ResponseOrderItem() { Message = "Record Not Found...." });

        }

        // Remove order&OrderItem By OrderId
        [Authorize]
        [HttpDelete]
        [Route("DeleteOrder")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var orderitem = _unitOfWork.OrderItem.GetAll();
            var deletelist = orderitem.Where(oitem => oitem.OrderId == id).ToList();

            var orderdata = _unitOfWork.Order.GetFirstorDefault(x => x.OrderId == id);
            if (orderdata != null)
            {
                orderdata.TotalAmount = 0;
                _unitOfWork.Order.Remove(orderdata);

                _unitOfWork.OrderItem.RemoveRange(deletelist);
                _unitOfWork.Save();

                return Ok(new ResponseOrderItem() { Message = "Delete Item Successfully...." });
            }
            else
            {
                return Ok(new ResponseOrderItem() { Message = "Record Not Found...." });
            }

        }

        //Update OrderItem Quantity
        [Authorize]
        [HttpPut]
        [Route("UpdateOrderQuantity")]
        public async Task<IActionResult> UpdateQuantity(int Quantity, int orderItemId)
        {
            var upQuantity = _unitOfWork.OrderItem.GetFirstorDefault(x => x.OrderItemId == orderItemId);

            if (Quantity >= 1 && upQuantity.IsActive != false)
            {
                upQuantity.Quantity = Quantity;
                _unitOfWork.OrderItem.Update(upQuantity);

                var data = upQuantity.Quantity * upQuantity.Price;
                var updatedTotal = _unitOfWork.Order.GetFirstorDefault(x => x.OrderId == upQuantity.OrderId);
                updatedTotal.TotalAmount = data;

                _unitOfWork.Order.Update(updatedTotal);
                _unitOfWork.Save();


            }
            else
            {
                return Ok(new ResponseOrderItem() { Message = "minimum Quantity Is must be 1" });
            }

            return Ok(new ResponseOrderItem() { Message = "Update Quantity Successfully...." });
        }

        // Get Details by different Filters
        [Authorize]
        [HttpGet]
        [Route("FilteringGetOrder")]
        public async Task<IActionResult> GetOrder(string? statustype, string? Name_or_Email, DateTime? fromdate, DateTime? todate)
        {

            if (statustype == null && Name_or_Email == null && fromdate != null && todate == null)
            {
                var datewise = _unitOfWork.Order.GetAll();

                var finaldata = datewise.Where(x => x.OrderDate >= fromdate).ToList();
                if (finaldata.Count == 0)
                {
                    return BadRequest("Data Not Found...");
                }
                return Ok(finaldata);
            }

            if (statustype == null && Name_or_Email == null && fromdate != null && todate != null)
            {
                var datewise = _unitOfWork.Order.GetAll();

                var finaldata = datewise.Where(x => x.OrderDate >= fromdate && x.OrderDate <= todate).ToList();
                if (finaldata.Count == 0)
                {
                    return BadRequest("Data Not Found...");
                }
                return Ok(finaldata);
            }


            //if (statustype != null && Name_or_Email == null)
            //{
            //    var order = _unitOfWork.Order.GetFirstorDefault(x => x.StatusType == statustype);
            //    return Ok(order);
            //}

            if (statustype == null && Name_or_Email != null)
            {
                var order = _unitOfWork.Order.GetFirstorDefault(x => x.CustomerName == Name_or_Email);

                if (order == null)
                {
                    var data_a = _unitOfWork.Order.GetFirstorDefault(x => x.CustomerEmail == Name_or_Email);
                    return Ok(data_a);
                }
                return Ok(order);
            }

            var data = _unitOfWork.Order.GetAll();
            var filterdata = data.Select(x => new { x.OrderId, x.Note, x.StatusType, x.CreatedOn });
            return Ok(filterdata);
        }
    }
}
