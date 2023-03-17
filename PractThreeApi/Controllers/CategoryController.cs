using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PractThreeApi.DataAccess.Repository.IRepository;
using PractThreeApi.Models;

namespace PractThreeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;


        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        [HttpGet]
        public IEnumerable<Category> Get()
        {
            return _unitOfWork.Category.GetAll();
        }


        [Route("Product")]
        [HttpGet]
        public async Task<IActionResult> GetProduct()
        {

            var data = _unitOfWork.Product.GetAll();
            var productlist = data.Select(x => new { x.ProductId, x.Name, x.Quantity, x.Price, x.Description });

            return Ok(productlist);

        }
    }
}
