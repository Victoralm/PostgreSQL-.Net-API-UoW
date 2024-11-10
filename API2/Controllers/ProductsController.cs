using API2.Entities;
using API2.Repositories.Interfaces;
using API2.UnitOfWork.Implementations;
using API2.UnitOfWork.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API2.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public ProductsController(ILogger<ProductsController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _unitOfWork.Products.GetProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var item = await _unitOfWork.Products.GetById(id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(Product product)
        {
            if (ModelState.IsValid)
            {
                product.Id = Guid.NewGuid();

                await _unitOfWork.Products.Add(product);
                await _unitOfWork.CompleteAsync();

                return CreatedAtAction("Get", new { product.Id }, product);
            }

            return new JsonResult("Somethign Went wrong") { StatusCode = 500 };
        }
    }
}
